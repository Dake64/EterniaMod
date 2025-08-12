using System;
using System.Collections.Generic;
using System.Linq;
using Eternia.Content.Players;
using Terraria;
using Terraria.ModLoader;

namespace Eternia.Content.Items;

public class EterniaGlobalItem : GlobalItem
{

    public override bool? UseItem(Item item, Player player)
    {
        if (item.OriginalDamage > 0 && item.axe == 0 && item.pick == 0 && item.hammer == 0)
            return CheckBetray(item);

        return base.UseItem(item, player);
    }

    private bool CheckBetray(Item item)
    {
        var globalPlayer = Main.LocalPlayer.GetModPlayer<EterniaGlobalPlayer>();
        var equippedAccessorySoul = globalPlayer.EquippedAccessorySoul;

        if (equippedAccessorySoul == EAccessorySoul.None)
            return true;

        var damageType = item.DamageType;
        var betrayedHisClass = false;

        var currentItemDamageTypes = item.DamageType.Name;

        var deathReason = "";
        switch (equippedAccessorySoul)
        {
            case EAccessorySoul.Melee:
                {
                    if (!item.CountsAsClass(DamageClass.Melee))
                    {
                        betrayedHisClass = true;
                        deathReason = $"{Main.LocalPlayer.name} traicionó el camino del guerrero.";
                    }

                    break;
                }
            case EAccessorySoul.Ranged:
                {
                    if (!item.CountsAsClass(DamageClass.Ranged))
                    {
                        betrayedHisClass = true;
                        deathReason = $"{Main.LocalPlayer.name} traicionó el camino del arquero.";

                    }

                    break;
                }
            case EAccessorySoul.Mage:
                {
                    if (!item.CountsAsClass(DamageClass.Magic))
                    {
                        betrayedHisClass = true;
                        deathReason = $"{Main.LocalPlayer.name} traicionó el camino del mago.";
                    }
                    break;
                }
            case EAccessorySoul.Summoner:
                {
                    if (!item.CountsAsClass(DamageClass.Summon))
                    {
                        betrayedHisClass = true;
                        deathReason = $"{Main.LocalPlayer.name} traicionó el camino del invocador.";
                    }
                    break;
                }
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (!betrayedHisClass || item.damage <= 0) return true;

        Main.LocalPlayer.KillMe(
            Terraria.DataStructures.PlayerDeathReason.ByCustomReason(deathReason),
            9999,
            0
        );
        // Terraria.Localization.NetworkText
        return false;
    }
}
