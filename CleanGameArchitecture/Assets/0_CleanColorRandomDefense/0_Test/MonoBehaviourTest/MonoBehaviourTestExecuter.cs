using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MageSkillTests;
using System;

public class MonoBehaviourTestExecuter : MonoBehaviour
{
    void TestMageSkill()
    {
        var tester = GetOrAddComponent<MageSkillTester>();
        tester.TestAOE();
    }

    T GetOrAddComponent<T>() where T : Component
    {
        T result = GetComponent<T>();
        if (result == null)
            result = gameObject.AddComponent<T>();
        return result;
    }
}

public class MonoBehaviourTester : MonoBehaviour
{
    protected void AfterAssert(float delayTime, Func<bool> conditoin)
        => StartCoroutine(Co_AfterAssert(delayTime, conditoin));

    IEnumerator Co_AfterAssert(float delayTime, Func<bool> conditoin)
    {
        yield return new WaitForSeconds(delayTime);
        Debug.Assert(conditoin());
    }
}
