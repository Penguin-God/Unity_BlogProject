using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing
{
    protected void Assert(bool condition, string message = "테스트 실패!!") => Debug.Assert(condition, message);
    protected void Log(object message) => Debug.Log(message);
}
