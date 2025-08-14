using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
namespace Eternia.Content.Players;

public class Eternal_NPCPlayer : ModPlayer
{
    private const string SaveKeyGift = "EternalNPC_GiftReceived";
    public bool HasReceivedEternalGift;

    public override void Initialize()
    {
        HasReceivedEternalGift = false;
    }

    public override void SaveData(TagCompound tag)
    {
        tag[SaveKeyGift] = HasReceivedEternalGift;
    }

    public override void LoadData(TagCompound tag)
    {
        HasReceivedEternalGift = tag.ContainsKey(SaveKeyGift) && tag.GetBool(SaveKeyGift);
    }

    // Seguridad extra: si se crea un personaje nuevo en un mundo existente, empezará con false (correcto)
    public override void OnEnterWorld()
    {
        // Nada más que hacer aquí; la entrega se hace al hablar con el NPC.
        // Este hook se ejecuta en cliente; la entrega se gestiona desde el NPC usando QuickSpawnItem (servidor sincroniza).
    }
}

