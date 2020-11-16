using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace BadgerTrafficDensity
{
    public class BadgerTrafficDensity : BaseScript
    {
        float pedFrequency;
        float vehFrequency;

        string jsonConfig = LoadResourceFile("BadgerScripts", "config/config.json");

        Newtonsoft.Json.JsonObjectAttribute json = new Newtonsoft.Json.JsonObjectAttribute();

        public BadgerTrafficDensity()
        {
            Tick += onTick;

            JObject o = JObject.Parse(jsonConfig);
            pedFrequency = (float)o.SelectToken("pedTrafficFrequency.pedFrequency");
            vehFrequency = (float)o.SelectToken("pedTrafficFrequency.vehicleFrequency");
            Debug.WriteLine("[BadgerTrafficDensity Config:]" + "\n" + "pedFrequency: " + pedFrequency + "\n" + "vehicleFrequency: " + vehFrequency);
        }
        private async Task onTick()
        {
            // Ped Density
            SetPedDensityMultiplierThisFrame(pedFrequency);
            SetScenarioPedDensityMultiplierThisFrame(pedFrequency, pedFrequency);

            // Traffic Density
            SetVehicleDensityMultiplierThisFrame(vehFrequency);
            SetRandomVehicleDensityMultiplierThisFrame(vehFrequency);
            SetParkedVehicleDensityMultiplierThisFrame(vehFrequency);
        }
    }
}
