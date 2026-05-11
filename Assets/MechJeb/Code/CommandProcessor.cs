using System;
using System.Collections.Generic;
using SpaceWarp2.API.Mods;

namespace MechJeb
{
    public enum Command
    {
        None,
        Info,
        Prograde,
        Retrograde,
        Normal,
        AntiNormal,
        RadialOut,
        RadialIn,
        PlanHohmann,
        PlanCircularize,
        ExecuteBurn,
        Abort
    }

    public class CommandProcessor
    {
        private Dictionary<string, Command> _commandMap = new Dictionary<string, Command>
        {
            {"info", Command.Info},
            {"pro", Command.Prograde},
            {"retro", Command.Retrograde},
            {"norm", Command.Normal},
            {"antinorm", Command.AntiNormal}, 
            {"rad+", Command.RadialIn},
            {"rad-", Command.RadialOut},
            {"hohmann", Command.PlanHohmann},
            {"circularize", Command.PlanCircularize},
            {"burn", Command.ExecuteBurn},
            {"abort", Command.Abort}
                
        };

        public void ProcessCommand(string input)
        {
            if (string.IsNullOrEmpty(input))
                return;
            string[] parts = input.ToLower().Split(' ');
            string command = parts[0];
            if (!_commandMap.TryGetValue(command, out Command cmd))
            {
                //SWLogger.LogWarning($"MechJeb: Unknown command '{command}'");
                return;
            }
            ExecuteCommand(cmd, parts);
        }

        private void ExecuteCommand(Command cmd, string[] args)
        {
            //var vesselMgr = MechJebPlugin.VesselManager;
            //var autopilot = MechJebPlugin.AutoPilotController;
            //var planner = MechJebPlugin.ManuverPlanner;

            switch (cmd)
            {
                case Command.Info:
                    //vesselMgr.LogVesselInfo();
                    break;
                case Command.Prograde:
                    //autopilot.PointPrograde();
                    //SWLogger.LogInfo("MechJeb: Pointing prograde");
                    break;
                case Command.Retrograde:
                    //autopilot.PointRetrograde();
                    //SWLogger.LogInfo("MechJeb: Pointing retrograde");
                    break;
                case Command.Normal:
                    //autopilot.PointNormal();
                    //SWLogger.LogInfo("MechJeb: Pointing normal");
                    break;
                case Command.AntiNormal:
                    //autopilot.PointAntiNormal();
                    //SWLogger.LogInfo("MechJeb: Pointing anti-normal");
                    break;
                case Command.RadialOut:
                    //autopilot.PointRadialOut();
                    //SWLogger.LogInfo("MechJeb: Pointing radial out");
                    break;
                case Command.RadialIn:
                    //autopilot.PointRadialIn();
                    //SWLogger.LogInfo("MechJeb: Pointing radial in");
                    break;
                case Command.PlanHohmann:
                    if (args.Length > 1 && double.TryParse(args[1], out double altitude))
                    {
                        //var plan = planner.PlanHohmannTransfer(altitude);
                    }
                    else
                    {
                        //SWLogger.LogWarning("MechJeb: Usage: hohmann <altitude>");
                    }
                    break;
                case Command.PlanCircularize:
                    //var cirPlan = planner.PlanCircularization();
                    break;
                case Command.ExecuteBurn:
                    if (args.Length > 1 && double.TryParse(args[1], out double dv))
                    {
                        //autopilot.ExecuteProgradeBurn(dv);
                    }
                    break;
                case Command.Abort:
                    //autopilot.Disable();
                    //SWLogger.LogInfo("MechJeb: Autopilot disabled");
                    break;
            }
        }


    }
}