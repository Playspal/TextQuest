using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LocalizationKey = System.String;

public static class LocalizationKeys
{
    // Common buttons
    public static LocalizationKey CommonButtonCancel = "common/button/cancel";

    // Buildings
    public static LocalizationKey BuildingCommonNotEnoughResourcesToConstruct = "building/common/not-enough-resources-to-construct";
    public static LocalizationKey BuildingSignalFireName = "building/signal-fire/name";
    public static LocalizationKey BuildingSignalFireDescription = "building/signal-fire/description";
    public static LocalizationKey BuildingSignalFireDescriptionJob = "building/signal-fire/description-job";
    public static LocalizationKey BuildingSignalFireActionNameConstruct = "building/signal-fire/action-name-construct";
    public static LocalizationKey BuildingSignalFireDeconstructedByRain = "building/signal-fire/deconstructed-by-rain";
    public static LocalizationKey BuildingSignalFireDeconstructedByTime = "building/signal-fire/deconstructed-by-time";
    
    
    public static string Get(this LocalizationKey key)
    {
        return Localization.Get(key);
    }
}
