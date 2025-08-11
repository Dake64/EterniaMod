using Eternia.Content.Players;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
namespace Eternia.Content.Items.Souls;

public class AccessorySoul : ModItem
{
    public EAccessorySoul ThisAccessorySoul = EAccessorySoul.None;
    
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.accessory = true;
        Item.rare = ItemRarityID.Yellow;
        Item.value = Item.buyPrice(0, 10);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        var globalPlayer = player.GetModPlayer<EterniaGlobalPlayer>();
        
        if (ThisAccessorySoul == EAccessorySoul.None)
            return;
        
        globalPlayer.EquippedAccessorySoul = ThisAccessorySoul;
    }
    
}