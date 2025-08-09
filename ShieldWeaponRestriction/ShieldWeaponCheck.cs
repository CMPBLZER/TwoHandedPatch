using RimWorld;
using Verse;
using System.Linq;

namespace ShieldWeaponRestriction
{
    public static class ShieldWeaponCheck
    {
        public static bool HasShield(Pawn pawn)
        {
            if (pawn == null) return false;

            // Проверка надетых щитов
            if (pawn.apparel?.WornApparel != null)
            {
                foreach (var apparel in pawn.apparel.WornApparel)
                {
                    if (apparel.TryGetComp<VFECore.CompShield>() != null)
                        return true;
                }
            }

            // Проверка инвентаря (щит в рюкзаке)
            if (pawn.inventory?.innerContainer != null)
            {
                foreach (var item in pawn.inventory.innerContainer)
                {
                    if (item.TryGetComp<VFECore.CompShield>() != null)
                        return true;
                }
            }

            return false;
        }

        public static bool IsWeaponUsableWithShield(ThingDef weaponDef)
        {
            var ext = weaponDef.GetModExtension<VFECore.ThingDefExtension>();
            if (ext == null) return true; // Если нет расширения — считаем, что можно
            return ext.usableWithShields;
        }
    }
}
