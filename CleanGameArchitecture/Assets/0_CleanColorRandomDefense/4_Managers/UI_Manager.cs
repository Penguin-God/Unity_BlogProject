using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager
{
    Stack<UI_Base> _popupStack = new Stack<UI_Base>();
    UI_Base _sceneUI = null;

    Transform _root;
    public void Init()
    {
        if (_root == null) 
            _root = new GameObject("@UI_Root").transform;
    }

    T InstantiateUI<T>(string uiType, string name) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name)) name = typeof(T).Name;
        return ResourcesManager.Instantiate($"UI/{uiType}/{name}").GetOrAddComponent<T>();
    }

    int _order = 10; // 기본 UI랑 팝업 UI 오더 다르게 하기 위해 초기값 10으로 세팅
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        T popup = InstantiateUI<T>("Popup", name);
        _order++;
        popup.gameObject.GetOrAddComponent<Canvas>().sortingOrder = _order;
        _popupStack.Push(popup);
        return popup;
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Base
    {
        T sceneUI = InstantiateUI<T>("Scene", name);
        _sceneUI = sceneUI;
        sceneUI.transform.SetParent(_root);
        return sceneUI;
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        T subItem = InstantiateUI<T>("SubItem", name);
        if (parent != null) 
            subItem.transform.SetParent(parent);
        subItem.transform.localScale = Vector3.one;
        subItem.transform.localPosition = subItem.transform.position;
        return subItem;
    }

    public void ClosePopupUI() => _popupStack.Pop().gameObject.SetActive(false);

    public void Clear()
    {
        _sceneUI = null;
        _popupStack.Clear();
        if (_root != null) _root = null;
    }
}
