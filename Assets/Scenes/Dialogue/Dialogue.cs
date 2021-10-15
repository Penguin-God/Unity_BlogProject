using UnityEngine;

[System.Serializable]
public struct TalkData
{
    public string name;
    public string[] contexts;
}

public class Dialogue : MonoBehaviour
{
    [SerializeField] string eventName = null;
    [SerializeField] TalkData[] talkDatas = null;

    void Start()
    {
        if (DialogueParse.TalkDictionary.ContainsKey(eventName)) talkDatas = DialogueParse.TalkDictionary[eventName];
        else Debug.LogWarning("찾을 수 없는 이벤트 이름 : " + eventName);
    }
}
