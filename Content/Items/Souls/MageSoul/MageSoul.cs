using Eternia.Content.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;
using Terraria.Localization;

namespace Eternia.Content.Items.Souls.MageSoul
{
    public class MageSoul : AccessorySoul
    {


        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            
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


    }
}
