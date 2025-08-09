using HarmonyLib;
using RimWorld;
using Verse;

namespace ShieldWeaponRestriction
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("suored.shieldweaponrestriction");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(Pawn_EquipmentTracker))]
    [HarmonyPatch("TryAddPrimaryEquipment")]
    public static class Patch_TryAddPrimaryEquipment
    {
        static bool Prefix(Pawn_EquipmentTracker __instance, ThingWithComps newEq, ref bool __result)
        {
            var pawn = __instance.pawn;

            if (pawn != null && newEq != null)
            {
                if (ShieldWeaponCheck.HasShield(pawn) && !ShieldWeaponCheck.IsWeaponUsableWithShield(newEq.def))
                {
                    if (pawn.IsColonistPlayerControlled)
                        Messages.Message($"{pawn.LabelShort} не может экипировать {newEq.Label} с щитом.", pawn, MessageTypeDefOf.RejectInput, false);

                    __result = false;
                    return false; // отмена экипировки
                }
            }
            return true;
        }
    }
}
