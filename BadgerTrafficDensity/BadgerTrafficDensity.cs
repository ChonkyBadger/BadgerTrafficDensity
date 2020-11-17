using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace BadgerTrafficDensity
{
    public class BadgerTrafficDensity : BaseScript
    {
        float pedFrequency;
        float vehFrequency;

        string jsonConfig = LoadResourceFile("BadgerTrafficDensity", "config/config.json");

        public BadgerTrafficDensity()
        {
            Tick += onTick;

            // Load config values
            JObject o = JObject.Parse(jsonConfig);
            pedFrequency = (float)o.SelectToken("badgerTrafficDensity.pedFrequency");
            vehFrequency = (float)o.SelectToken("badgerTrafficDensity.vehicleFrequency");
        }
        private async Task onTick()
        {
            // Set Ped Density
            SetPedDensityMultiplierThisFrame(pedFrequency);
            SetScenarioPedDensityMultiplierThisFrame(pedFrequency, pedFrequency);

            // Set Traffic Density
            SetVehicleDensityMultiplierThisFrame(vehFrequency);
            SetRandomVehicleDensityMultiplierThisFrame(vehFrequency);
            SetParkedVehicleDensityMultiplierThisFrame(vehFrequency);
        }
    }
}
