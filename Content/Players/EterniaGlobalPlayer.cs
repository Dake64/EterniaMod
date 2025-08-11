using Terraria;
using Terraria.ModLoader;

namespace Eternia.Content.Players
{
    public class EterniaGlobalPlayer : ModPlayer
    {
        public EAccessorySoul EquippedAccessorySoul = EAccessorySoul.None;
        private bool punishedThisAnimation = false; // Para evitar doble muerte


        public override void ResetEffects()
        {
            EquippedAccessorySoul = EAccessorySoul.None;
            punishedThisAnimation = false;
        }


        public override bool CanUseItem(Item item)
        {
            return base.CanUseItem(item);
        }


        public override bool CanShoot(Item item)
        {
            var currentWeaponHeld = Player.HeldItem;
            var currentWeaponHeldDamageType = currentWeaponHeld.DamageType;

            if (EquippedAccessorySoul == EAccessorySoul.None)
            {
                Main.NewText("equipped soul is none");
                return base.CanShoot(item);
            }


            var damageClassName = currentWeaponHeldDamageType.GetType().Name; // Held Item Damage Name

            var MeleeNames = new List<string>() { "Melee" };
            var RangedNames = new List<string>() { "Ranged", "Bow", "Gun", "RangedDamage" };
            var MagicNames = new List<string>() { "Magic" };
            var SummonNames = new List<string>() { "Summon" };


            var betrayedHisClass = false ||
                                   (!MeleeNames.Contains(damageClassName) &&
                                    EquippedAccessorySoul == EAccessorySoul.Melee ||
                                    !RangedNames.Contains(damageClassName) &&
                                    EquippedAccessorySoul == EAccessorySoul.Ranged ||
                                    !MagicNames.Contains(damageClassName) &&
                                    EquippedAccessorySoul == EAccessorySoul.Mage ||
                                    !SummonNames.Contains(damageClassName) &&
                                    EquippedAccessorySoul == EAccessorySoul.Summoner);


            Main.NewText($"equipped soul {EquippedAccessorySoul.ToString()}");
            Main.NewText($"current weapon held damage type {currentWeaponHeldDamageType.DisplayName}");
            Main.NewText($"betrayed his class {betrayedHisClass}");


            if (betrayedHisClass)
            {
                // KILL THE PLAYER AND SEND MESSAGE

                var deathReason = currentWeaponHeldDamageType switch
                {
                    MagicDamageClass => $"{Player.name} traicionó el camino del guerrero.",
                    RangedDamageClass => $"{Player.name} traicionó el camino del guerrero.",
                    MeleeDamageClass => $"{Player.name} traicionó el camino del guerrero.",
                    SummonDamageClass => $"{Player.name} traicionó el camino del guerrero.",
                    _ => ""
                };

                Player.KillMe(Terraria.DataStructures.PlayerDeathReason.ByCustomReason(deathReason), 9999, 0);
                Main.NewText("Should be killed");
                return false;
            }

            return base.CanShoot(item);
        }
    }
}