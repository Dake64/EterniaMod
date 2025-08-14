using System;
using Eternia.Content.Items.Souls.StarterSoul;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternia.Content.NPCs
{
    // NPC amistoso tipo Town NPC que habla y puede dar un regalo
    [AutoloadHead] // Para icono de cabeza en el mapa/guía de vivienda
    public class Eternal : ModNPC
    {
        // ================================
        // 🔧 CONFIGURACIÓN RÁPIDA (EDITA AQUÍ)
        // ================================
        public static string FirstChatMessage = "¡Bienvenido a este nuevo mundo! Toma esto para empezar tu aventura.";
        public static int GiftItemType = ModContent.ItemType<StarterSoul>(); // Cambia por el ItemID o tu ModContent.ItemType<>
        public static int GiftItemStack = 1;
        public static string RepeatChatMessage = "¡Sigue explorando! Ya te di tu regalo.";
        public static string ButtonText_Receive = "Recibir regalo";
        public static string ButtonText_Ok = "Ok";
        // ================================

        public override void SetStaticDefaults()
        {
            // Opcional: animación y comportamiento similar al Guía para no reinventar ruedas
            Main.npcFrameCount[Type] = 1;
            // No tienda, no ataque: solo diálogos/regalo
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;      // Se comporta como NPC de ciudad
            NPC.friendly = true;     // No hostil
            NPC.width = 13;
            NPC.height = 23;
            NPC.aiStyle = 7;         // AI de Town NPC
            AIType = NPCID.Guide;    // Toma la AI básica del Guía
            AnimationType = NPCID.Guide;

            NPC.damage = 0;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.knockBackResist = 0.5f;
            NPC.noGravity = false;
        }

        public override bool CanChat() => true;

        public override string GetChat()
        {
            // Si el jugador local no ha recibido regalo aún, muestra el mensaje de bienvenida
            var mp = Main.LocalPlayer.GetModPlayer<Players.Eternal_NPCPlayer>();
            if (!mp.HasReceivedEternalGift)
                return FirstChatMessage;

            return RepeatChatMessage;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            var mp = Main.LocalPlayer.GetModPlayer<Players.Eternal_NPCPlayer>();
            if (!mp.HasReceivedEternalGift)
            {
                button = ButtonText_Receive; // Botón principal
                button2 = "";                // Sin segundo botón
            }
            else
            {
                button = ButtonText_Ok;
                button2 = "";
            }
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (!firstButton)
                return;

            Player player = Main.LocalPlayer;
            var mp = player.GetModPlayer<Players.Eternal_NPCPlayer>();

            // Ya recibió regalo: solo cerrar diálogo
            if (mp.HasReceivedEternalGift)
                return;

            // Entrega del ítem (una sola vez por jugador)
            // Importante: QuickSpawnItem se invoca desde el cliente local; tML gestiona el spawn en servidor.
            // Aun así, validamos stack.
            int stack = Math.Max(1, GiftItemStack);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), GiftItemType, stack);
            mp.HasReceivedEternalGift = true;

            // Mensaje posterior opcional
            Main.npcChatText = RepeatChatMessage;
        }

        // Opcional: evitar que se generen clones por casualidad si se intentara por otras vías
        public override bool CheckActive()
        {
            // Evita despawn si ya es el único
            // Si por alguna razón hay más de uno, el sistema del mundo controla no duplicar al crear mundo.
            return true;
        }
    }
}