using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] Camera cam;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.GetComponent<Dialogue>().GetObjectDialogue() == null) return;
                TalkData[] talkDatas = hit.transform.GetComponent<Dialogue>().GetObjectDialogue();
                DebugDialogue(talkDatas);
            }
        }
    }

    void DebugDialogue(TalkData[] talkDatas)
    {
        for (int i = 0; i < talkDatas.Length; i++)
        {
            Debug.Log(talkDatas[i].name);
            foreach (string context in talkDatas[i].contexts) Debug.Log(context);
        }
    }
}
