
using Eternia.Content.Items.Souls.MageSoul;
using Eternia.Content.Items.Souls.RangedSoul;
using Eternia.Content.Items.Souls.MeleeSoul;
using Eternia.Content.Items.Souls.SummonerSoul;
using Terraria;
using Terraria.ModLoader;


namespace Eternia.Content.AccessorySlots
{
    public class UniversalSoulSlot : ModAccessorySlot
    {
        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
        {
            return checkItem.ModItem is MeleeSoul or RangedSoul or MageSoul or SummonerSoul;
        }
        
    }
}
