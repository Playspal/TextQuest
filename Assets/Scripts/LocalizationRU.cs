using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LocalizationRU
{
    public static Dictionary<string, string> Load()
    {
        return new Dictionary<string, string>()
        {
            {LocalizationKeys.CommonButtonCancel, "Отмена"},
        
            {LocalizationKeys.BuildingCommonNotEnoughResourcesToConstruct, "Недостаточно ресурсов для постройки. Отправиться на поиски ресурсов?"},
            {LocalizationKeys.BuildingSignalFireName, "Сигнальный костер"},
            {LocalizationKeys.BuildingSignalFireDescription, "Сигнальный костер помогает привлекать новых поселенцев. Для поддержания огня требуется один человек."},
            {LocalizationKeys.BuildingSignalFireDescriptionJob, "Поддерживает сигнальный костер"},
            {LocalizationKeys.BuildingSignalFireActionNameConstruct, "Разжечь"},
            {LocalizationKeys.BuildingSignalFireDeconstructedByRain, "Никто не присматривал за сигнальным костром и сильный дождь потушил его."},
            {LocalizationKeys.BuildingSignalFireDeconstructedByTime, "Никто не поддерживал сигнальный костер и он постепенно потух"}
        };
    }
}
