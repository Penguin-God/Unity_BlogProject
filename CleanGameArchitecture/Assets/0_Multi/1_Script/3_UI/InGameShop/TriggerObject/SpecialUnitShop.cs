using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialUnitShop : ShopObject
{
    protected override void ShowShop()
    {
        Multi_Managers.UI.ShowPopGroupUI<UI_Popup>(PopupGroupType.UnitWindow, path);
        Multi_Managers.Sound.PlayEffect(EffectSoundType.PopSound_2);
    }
}
