using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MageSkillTests;

[ExecuteInEditMode]
public class MonoBehaviourTester : MonoBehaviour
{
    //void TestTest()
    //{
    //    UnityEditor.EditorApplication.isPlaying = true;
    //    StartCoroutine(Co_WaitUntilGameRun());
    //}

    IEnumerator Co_WaitUntilGameRun()
    {
        yield return null;
        yield return new WaitUntil(() => Application.isPlaying);
        print(Application.isPlaying);
        yield return new WaitForSeconds(2f);
        UnityEditor.EditorApplication.isPlaying = false;
    }

    void TestMageSkill()
    {
        var tester = new MageSkillTester();
        tester.TestAOE();
    }
}
