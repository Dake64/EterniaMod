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
    public float PercentageIncrease { get; set; } = 0.25f; // Default to 25% increase

    protected LocalizedText MageSoulTooltip { get; set; }

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.accessory = true;
        Item.rare = ItemRarityID.Yellow;
        Item.value = Item.buyPrice(0, 10);

        MageSoulTooltip = this.GetLocalization("Tooltip");
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
        base.ModifyTooltips(tooltips);
        var descriptionIndex = tooltips.FindIndex(line => line.Name == "Tooltip0");

        if (descriptionIndex > 0)
        {
            tooltips[descriptionIndex].Text = MageSoulTooltip.Format(Math.Round(PercentageIncrease * 100));
        }
        else
        {
            var description = new TooltipLine(Mod, "Tooltip", MageSoulTooltip.Value);
            tooltips.Add(description);
        }
    }
}
