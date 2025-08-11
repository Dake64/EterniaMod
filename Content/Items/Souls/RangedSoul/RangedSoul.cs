using Eternia.Content.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternia.Content.Items.Souls.RangedSoul
{
    public class RangedSoul : AccessorySoul
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            ThisAccessorySoul = EAccessorySoul.Ranged;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.GetDamage(DamageClass.Ranged) += 0.25f; // +25% magic damage
        }
    }
}