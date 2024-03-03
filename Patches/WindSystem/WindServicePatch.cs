using HarmonyLib;
using Timberborn.SingletonSystem;
using Timberborn.WeatherSystem;
using Timberborn.WindSystem;

namespace Seasons.Patches.WindSystem;


[HarmonyPatch(typeof(WindService))]
public class WindServicePatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(WindService.ChangeWind))]
    public static void Postfix(WindService __instance)
    {
        __instance._eventBus.Post(new SeasonWindChangedEvent(__instance.WindDirection, __instance.WindStrength));
    }
}