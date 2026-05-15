using SpaceWarp2.API.Mods;
namespace MechJeb
{
    public class MechJebPlugin : GeneralMod
    {
        //public static VesselManager VesselManager {get; private set;}
        //public static OrbitCalulator OrbitCalulator {get; private set;}
        //public static AutoPilotController AutoPilotController {get; private set;}
        //public static ManuverPlanner ManuverPlanner {get; private set;}
        public static CommandProcessor CommandProcessor {get; private set; }
        //public static ThrustController ThrustController {get; private set;}
        //public static EventManager EventManager {get; private set;}
        public override void OnPreInitialized() {
        }

        public override void OnInitialized()
        {
            CommandProcessor = new CommandProcessor();
            //ThrustController = new ThrustController();
            //EventManager = new EventManager();
        }

        public override void OnPostInitialized() {

        }
    }
}