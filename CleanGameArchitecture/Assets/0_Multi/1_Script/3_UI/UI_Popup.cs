using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    protected override void Init()
    {
        Multi_Managers.UI.SetCanvas(gameObject, true);
    }
}
