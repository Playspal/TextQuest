using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityExpansion.UI;

public class UiVerticalListItem : UiObject
{
    public virtual float GetPreferredHeight()
    {
        return Height;
    }
}
