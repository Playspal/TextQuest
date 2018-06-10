using System;
using System.Collections.Generic;
using UnityEngine;
using UnityExpansion.UI;

public static class Ui
{
    public static UiPanelFooter PanelFooter
    {
        get
        {
            return UiLayout.FindPanel("Ui/PanelFooter") as UiPanelFooter;
        }
    }

    public static ScreenCards ScreenCards
    {
        get
        {
            return UiLayout.FindScreen("Ui/ScreenCards") as ScreenCards;
        }
    }
    
    public static UiScreenTown ScreenTown
    {
        get
        {
            return UiLayout.FindScreen("Ui/ScreenTown") as UiScreenTown;
        }
    }
    
    public static UiScreenMap ScreenMap
    {
        get
        {
            return UiLayout.FindScreen("Ui/ScreenMap") as UiScreenMap;
        }
    }
    
    public static UiScreenPopulation ScreenPopulation
    {
        get
        {
            return UiLayout.FindScreen("Ui/ScreenPopulation") as UiScreenPopulation;
        }
    }

    public static UiPopupConfirm ShowPopupConfirm()
    {
        UiPopupConfirm popup = UiLayout.CreatePopup("Ui/PopupConfirm") as UiPopupConfirm;

        popup.Rotation = UnityEngine.Random.Range(-1f, 1f);
        popup.OnShowBegin += () => Quest.Instance.SetPause(true);
        popup.OnHideBegin += () => Quest.Instance.SetPause(false);

        return popup;
    }
    
    public static UiPopupAssignWorker ShowPopupAssignWorker(QuestBuilding building)
    {
        UiPopupAssignWorker popup = UiLayout.CreatePopup("Ui/PopupAssignWorker") as UiPopupAssignWorker;

        popup.SetBuilding(building);
        
        popup.OnShowBegin += () => Quest.Instance.SetPause(true);
        popup.OnHideBegin += () => Quest.Instance.SetPause(false);

        return popup;
    }
    
    public static UiPopupConfirm ShowPopupNotEnoughResources(string message)
    {
        UiPopupConfirm popupConfirm = Ui.ShowPopupConfirm();
        popupConfirm.OnOk += Ui.ShowScreenMap;
        popupConfirm.SetData
        (
            "НЕХВАТАЕТ РЕСУРСОВ",
            message,
            "ДА",
            "НЕТ"
        );

        return popupConfirm;
    }

    public static void ShowScreenCards()
    {
        ScreenCards.Show();
        PanelFooter.Hide();
    }
    
    public static void ShowScreenTown()
    {
        UiLayout.SetActiveScreen(ScreenTown);
        PanelFooter.Show();
    }

    public static void ShowScreenMap()
    {
        UiLayout.SetActiveScreen(ScreenMap);
        PanelFooter.Show();
    }
    
    public static void ShowScreenPopulation()
    {
        UiLayout.SetActiveScreen(ScreenPopulation);
        PanelFooter.Show();
    }
}
