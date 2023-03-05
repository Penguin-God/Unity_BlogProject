using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MoneyTest
{
    readonly Dictionary<CurrentType, float> _exchangeRateToDollerByCurrentType = new Dictionary<CurrentType, float>();

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _exchangeRateToDollerByCurrentType.Add(CurrentType.KRW, 1200);
    }

    [Test]
    public void TestCreateMoney()
    {
        var money = new Money();
        Assert.AreEqual(0, money.Amount);
    }

    [Test]
    public void TestAdd()
    {
        var money = new Money();
        money.Add(5);
        Assert.AreEqual(5, money.Amount);
    }
}
