using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Eternia.Content.System;

public class Eternal_System : ModSystem
    {
        private const string SaveKeySpawned = "EternalNPC_Spawned";
        public static bool SpawnedThisWorld = false;

        public override void OnWorldLoad()
        {
            SpawnedThisWorld = false; // Reset al cargar mundo (hasta leer save)
        }

        public override void OnWorldUnload()
        {
            SpawnedThisWorld = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag[SaveKeySpawned] = SpawnedThisWorld;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            SpawnedThisWorld = tag.ContainsKey(SaveKeySpawned) && tag.GetBool(SaveKeySpawned);
        }

        public override void PostWorldGen()
        {
            // Solo servidor (en single y en host MP). Evita problemas de sync
            if (Main.netMode == NetmodeID.MultiplayerClient)
                return;

            // Ya se generó en este mundo o ya existe el NPC: no dupliques
            if (SpawnedThisWorld || NPC.AnyNPCs(ModContent.NPCType<NPCs.Eternal>()))
                return;

            // Intenta colocar cerca del spawn
            TrySpawnNearWorldSpawn();

            // Marca bandera
            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Eternal>()))
                SpawnedThisWorld = true;
        }

        private void TrySpawnNearWorldSpawn()
        {
            // Punto de spawn del mundo (en tiles)
            int spawnX = Main.spawnTileX;
            int spawnY = Main.spawnTileY;

            // Busca la superficie sólida más cercana hacia abajo para colocar al NPC sobre tierra
            int y = spawnY;
            for (int i = 0; i < 400; i++)
            {
                if (WorldGen.SolidTile(spawnX, y) || WorldGen.SolidOrSlopedTile(spawnX, y))
                {
                    y--;
                    break;
                }
                y++;
            }

            // Si nos fuimos muy abajo, recorta
            y = Math.Clamp(y, 0, Main.maxTilesY - 1);

            // Offset en píxeles
            Vector2 worldPos = new Vector2(spawnX * 16 + 8, (y * 16) - 16);

            // Crea el NPC (servidor lo sincroniza)
            int npcType = ModContent.NPCType<NPCs.Eternal>();
            _ = NPC.NewNPC(new EntitySource_WorldGen(), (int)worldPos.X, (int)worldPos.Y, npcType);
        }
    }
