using System;
using System.Collections.Generic;
using Eternia.Content.Players;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.Localization;

namespace Eternia.Content.Items.Souls;

public abstract class AccessorySoul : ModItem
{
    protected EAccessorySoul ThisAccessorySoul = EAccessorySoul.None;

    protected static LocalizedText SoulAccessoryTooltip { get; set; }
    protected float PercentageIncrease { get; set; } = 0.25f; // Default to 25% increase

    public override void SetStaticDefaults()
    {
        SoulAccessoryTooltip = this.GetLocalization("Tooltip");
    }

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

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var localizedTooltip = SoulAccessoryTooltip.Format(Math.Round(PercentageIncrease * 100));
        tooltips[2].Text = localizedTooltip;
        tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Using this item will kill you if you betray your class."));
    }

}
