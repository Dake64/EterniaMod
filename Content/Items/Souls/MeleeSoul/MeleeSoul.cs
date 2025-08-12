using Eternia.Content.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace Eternia.Content.Items.Souls.MeleeSoul
{
    public class MeleeSoul : AccessorySoul
    {

        
        public override void SetDefaults()
        {
            base.SetDefaults();
            ThisAccessorySoul = EAccessorySoul.Melee;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.GetDamage(DamageClass.Melee) += PercentageIncrease; // +25% magic damage
        }
        
    }
}
