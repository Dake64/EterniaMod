using System;
using System.Collections.Generic;
using System.Linq;
using Eternia.Content.Players;
using Terraria;
using Terraria.ModLoader;

namespace Eternia.Content.Items;

public class EterniaGlobalItem : GlobalItem
{
    // Summon
    private static readonly List<string> SummonPositiveNames = ["Summon"];

    private static readonly List<string> SummonNegativeNames = ["Magic, Ranged, Melee"];

    // Ranged
    private static readonly List<string> RangedPositiveNames = ["Ranged"];

    private static readonly List<string> RangedNegativeNames = ["Magic, Summon, Melee"];

    // Melee
    private static readonly List<string> MeleePositiveNames = ["Melee"];

    private static readonly List<string> MeleeNegativeNames = ["Magic, Ranged, Summon"];

    // Magic
    private static readonly List<string> MagicPositiveNames = ["Magic"];
    private static readonly List<string> MagicNegativeNames = ["Ranged, Summon, Melee"];


    public override bool? UseItem(Item item, Player player)
    {
        Main.NewText(item.CountsAsClass(DamageClass.Summon));
        Main.NewText(item.CountsAsClass(DamageClass.Ranged));
        Main.NewText(item.CountsAsClass(DamageClass.Melee));
        Main.NewText(item.CountsAsClass(DamageClass.Magic));

        Main.NewText($"Item: {item.Name}");
        Main.NewText($"{item.DamageType.DisplayName}");
        Main.NewText($"{item.DamageType.Name}");
        Main.NewText($"{item.DamageType.GetType().Name}");
        Main.NewText(
            $"EquippedAccessorySoul: {Main.LocalPlayer.GetModPlayer<EterniaGlobalPlayer>().EquippedAccessorySoul}");
        Main.NewText("------------");
        if (item.OriginalDamage > 0 && item.axe == 0 && item.pick == 0 && item.hammer == 0)
            return CheckBetray(item);

        return base.UseItem(item, player);
    }

    private static bool ContainsAny(string source, List<string> keywords)
    {
        return keywords.Any(keyword => source.Contains(keyword, StringComparison.OrdinalIgnoreCase));
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
        return false;
    }
}
