using UnityEngine;

[System.Serializable]
public struct TalkData
{
    public string name;
    public string[] contexts;
}

public class Dialogue : MonoBehaviour
{
    [SerializeField] string eventName;
    [SerializeField] TalkData[] talkDatas;
}
