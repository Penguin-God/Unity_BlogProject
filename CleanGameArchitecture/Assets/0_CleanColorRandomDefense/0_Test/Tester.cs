using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tests;

public class Tester : MonoBehaviour
{
    [ContextMenu("RuleTest")]
    void RuleTest()
    {
        new TestGameRules().TestCountRule();
    }
}
