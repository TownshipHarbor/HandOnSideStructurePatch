using System;
using MelonLoader;

[assembly: MelonInfo(typeof(HandOnSideStructurePatch.Core), "HandOnSideStructurePatch", HandOnSideStructurePatch.Core.Version, "CGNik, ported by Shadow", null)]
[assembly: MelonGame("Alta", "Att_Quest")]

namespace HandOnSideStructurePatch
{
    public class Core : MelonMod
    {
        internal const string Version = "1.0.0";
        const string Release = "v2";
        const uint PowerStingerHash = 60198u;
        const uint PowerHitHash = 51076u;
        const int RetryEvery = 60;
        const int GiveUpAfter = 3600;

        static MelonLogger.Instance log;
        static bool done;
        static int frames;
        static string waitingFor;

        public override void OnInitializeMelon()
        {
            log = LoggerInstance;
            LoggerInstance.Msg("Initialized.");
            if (Plat.IsServer())
            {
                done = true;
                LoggerInstance.Msg("This is a server: the patch is for players only, nothing changed.");
                return;
            }
            LoggerInstance.Msg(Plat.Name + " build of CGNik's HandOnSideStructurePatch " + Release + ".");
            Attempt("start");
        }

        public override void OnLateInitializeMelon()
        {
            Attempt("late start");
        }

        public override void OnUpdate()
        {
            if (done) return;
            frames++;
            if (frames % RetryEvery != 0) return;
            if (frames >= GiveUpAfter)
            {
                done = true;
                log.Error("Gave up after " + frames + " frames: the game never loaded " + waitingFor + ". Hand On Side has no charge sounds.");
                return;
            }
            Attempt("frame " + frames);
        }

        static void Attempt(string when)
        {
            if (done) return;
            PowerStinger stinger = Plat.As<PowerStinger>(Plat.Skill(PowerStingerHash));
            PowerHit hit = Plat.As<PowerHit>(Plat.Skill(PowerHitHash));
            string missing = stinger == null ? "Power Stinger (" + PowerStingerHash + ")" : hit == null ? "Power Hit (" + PowerHitHash + ")" : null;
            if (missing != null)
            {
                if (missing != waitingFor && when != "start") log.Msg("Waiting for " + missing + " (" + when + ").");
                waitingFor = missing;
                return;
            }
            done = true;
            try
            {
                SkillInputStructure handOnSideStructure = stinger.InputStructure;
                SkillInputStructure raiseHandStructure = hit.InputStructure;
                Plat.CopyChargeSounds(handOnSideStructure, raiseHandStructure);
                log.Msg("Hand On Side now plays Raise Hand's charge and execute sounds (" + when + "): charged " + Plat.Shared(Plat.Charged(handOnSideStructure), Plat.Charged(raiseHandStructure)) + ", executed " + Plat.Shared(Plat.Executed(handOnSideStructure), Plat.Executed(raiseHandStructure)) + ".");
            }
            catch (Exception e)
            {
                log.Error("The patch failed at " + when + ": " + e);
            }
        }
    }
}
