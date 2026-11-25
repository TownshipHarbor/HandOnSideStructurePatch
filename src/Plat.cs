using System;
using System.Collections.Generic;
using System.Reflection;
using Alta;
using GameList = Il2CppSystem.Collections.Generic;

namespace HandOnSideStructurePatch
{
    static class Plat
    {
        internal const string Name = "Quest";

        internal static bool IsServer()
        {
            return false;
        }

        internal static T As<T>(object value) where T : Il2CppObjectBase
        {
            Il2CppObjectBase proxy = value as Il2CppObjectBase;
            return proxy == null ? null : proxy.TryCast<T>();
        }

        internal static ProfessionSkill Skill(uint hash)
        {
            HashedGeneralValue<ProfessionSkill>.CheckItems();
            GameList.Dictionary<uint, ProfessionSkill> items = HashedGeneralValue<ProfessionSkill>.items;
            return items != null && items.ContainsKey(hash) ? items[hash] : null;
        }

        internal static TransformEvent Charged(SkillInputStructure input) { return input.chargedDefaultEffect; }
        internal static TransformEvent Executed(SkillInputStructure input) { return input.executedDefaultEffect; }

        internal static void CopyChargeSounds(SkillInputStructure to, SkillInputStructure from)
        {
            to.chargedDefaultEffect = from.chargedDefaultEffect;
            to.executedDefaultEffect = from.executedDefaultEffect;
        }

        internal static string Shared(TransformEvent a, TransformEvent b)
        {
            if (a == null || b == null) return "none";
            return a.Pointer == b.Pointer ? "shared" : "different";
        }
    }
}
