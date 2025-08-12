using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Eternia.Content.Players
{
    public class EterniaGlobalPlayer : ModPlayer
    {
        public EAccessorySoul EquippedAccessorySoul = EAccessorySoul.None;

        public override void ResetEffects()
        {
            EquippedAccessorySoul = EAccessorySoul.None;
        }
    }
}