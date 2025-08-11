using Eternia.Content.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternia.Content.Items.Souls.SummonerSoul
{
    public class SummonerSoul : AccessorySoul
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            ThisAccessorySoul = EAccessorySoul.Summoner;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.GetDamage(DamageClass.Summon) += PercentageIncrease; // +25% magic damage
        }
    }
}
