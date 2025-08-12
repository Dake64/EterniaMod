using Eternia.Content.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace Eternia.Content.Items.Souls.MageSoul
{
    public class MageSoul : AccessorySoul
    {

        public override void SetStaticDefaults()
        {
            SoulAccessoryTooltip = this.GetLocalization("Tooltip");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            ThisAccessorySoul = EAccessorySoul.Mage;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.GetDamage(DamageClass.Magic) += PercentageIncrease; // +25% magic damage
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var localizedTooltip = SoulAccessoryTooltip.Format(Math.Round(PercentageIncrease * 100));
            tooltips[2].Text = localizedTooltip;
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Using this item will kill you if you betray your class."));
        }
    }
}
