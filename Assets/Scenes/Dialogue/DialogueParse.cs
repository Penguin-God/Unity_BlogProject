using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DEBUGTEXT
{
    public string eventName;
    public TalkData[] tds;
}

public class DialogueParse : MonoBehaviour
{

    private void Awake()
    {
        SetTalkDictionary();
    }
    public static Dictionary<string, TalkData[]> TalkDictionary = new Dictionary<string, TalkData[]>();
    [SerializeField] private TextAsset csvFile = null;

    [SerializeField] List<DEBUGTEXT> DEBUGTEXTS = new List<DEBUGTEXT>();

    [ContextMenu("대사 세팅")]
    public void SetTalkDictionary()
    {
        string csvText = csvFile.text.Substring(0, csvFile.text.Length - 1); // 아래 한 줄 빼기
        string[] datas = csvText.Split(new char[] { '\n' }); // 줄바꿈(한 줄)을 기준으로 csv 파일을 쪼개서 string배열에 줄 순서대로 담음

        // for문에 i++를 넣지 않고 while문처럼 사용
        for (int i = 1; i < datas.Length; i++) // 엑셀 파일 1번째 줄은 편의를 위한 분류이므로 i = 1부터 시작
        {
            // A, B, C열을 쪼개서 배열에 담음 (CSV파일은 ,로 데이터를 구분하기 때문에 ,를 기준으로 짜름)
            string[] row = datas[i].Split(new char[] { ',' });

            // 유효한 이벤트 이름이 나올때까지 반복
            if (row[0].Trim() == "" || row[0].Trim() == "end") continue;


            List<TalkData> talkDataList = new List<TalkData>();
            string eventName = row[0];

            while(row[0].Trim() != "end") // talkDataList 하나를 만드는 반복문
            {
                // 캐릭터가 한번에 치는 대사를 담을 리스트 캐릭터가 치는 대사의 길이를 모르므로 리스트로 선언
                List<string> contextList = new List<string>();

                TalkData talkData;
                talkData.name = row[1]; // 캐릭터 이름이 있는 B열

                do // talkData 하나를 만드는 반복문
                {
                    contextList.Add(row[2].ToString());
                    if(++i < datas.Length) row = datas[i].Split(new char[] { ',' });
                    else break;
                } while (row[1] == "" && row[0] != "end");

                talkData.contexts = contextList.ToArray();
                talkDataList.Add(talkData);
            }

            TalkDictionary.Add(eventName.Trim(), talkDataList.ToArray());

            // 디버깅 데이터 세팅
            DEBUGTEXT dEBUGTEXT = new DEBUGTEXT();
            dEBUGTEXT.eventName = eventName;
            dEBUGTEXT.tds = talkDataList.ToArray();
            DEBUGTEXTS.Add(dEBUGTEXT);
        }
    }
}
