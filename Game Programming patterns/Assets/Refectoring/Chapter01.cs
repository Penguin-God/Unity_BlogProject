using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System.Linq;
using System.Text;

public struct Invoice
{
    public string customer;
    public Performance[] performances;
}

public struct Performance
{
    public string playId;
    public int audience;
}

public struct Play
{
    public string id;
    public string name;
    public string type;
}

public class Chapter01 : MonoBehaviour
{
    [SerializeField, TextArea] string result;

    [ContextMenu("Do")]
    void Do()
    {
        Invoice invoice = new Invoice
        {
            customer = "BigCo",
            performances = new Performance[]
            {
                new Performance { playId = "hamlet", audience = 55 },
                new Performance { playId = "as-like", audience = 35 },
                new Performance { playId = "othello", audience = 40 }
            }
        };

        Play[] plays = new Play[]
        {
            new Play { id = "hamlet", name = "Hamlet", type = "tragedy" },
            new Play { id = "as-like", name = "As You Like It", type = "comedy" },
            new Play { id = "othello", name = "Othello", type = "tragedy" }
        };

        string text = Statement(invoice, plays);
        Debug.Assert(result == text);
        print(text);
    }

    public string Statement(Invoice invoice, Play[] plays)
    {
        var invoiceData = CalculrateInvoeData(invoice.performances, plays);

        var result = new StringBuilder($"청구내역 (고객명: {invoice.customer})\n");
        result.Append(invoiceData.claimText.ToString());
        result.AppendLine($"총액 {(invoiceData.amount / 100).ToString("C", BuildFormat())}");
        result.AppendLine($"적립 포인트 {invoiceData.credit}점");

        return result.ToString();
    }

    NumberFormatInfo BuildFormat()
    {
        var format = new CultureInfo("en-US", false).NumberFormat;
        format.CurrencySymbol = "$";
        format.CurrencyNegativePattern = 1;
        format.NumberNegativePattern = 1;
        return format;
    }


    (float amount, int credit, StringBuilder claimText) CalculrateInvoeData(Performance[] performances, Play[] plays)
    {
        (float amount, int credit, StringBuilder claimText) result = (0, 0, new StringBuilder(""));
        foreach (var perf in performances)
        {
            var play = plays.FirstOrDefault(x => x.id == perf.playId);

            // 유즈케이스
            result.credit += CalculateCredit(perf, play);
            result.amount += CalculateAmount(perf, play);

            result.claimText.AppendLine($"{play.name}: {CalculateAmount(perf, play) / 100} {perf.audience}석"); // 뷰
        }
        return result;
    }

    int CalculateCredit(Performance perf, Play play)
    {
        int result = 0;
        result += Mathf.Max(perf.audience - 30, 0);
        result = CalculrateMileage(result, perf, play);
        return result;

        int CalculrateMileage(int volumeCredits, Performance perf, Play play)
        {
            if (play.type == "comedy")
                volumeCredits += Mathf.FloorToInt(perf.audience / 5);

            return volumeCredits;
        }
    }

    float CalculateAmount(Performance perf, Play play)
    {
        float result;
        switch (play.type)
        {
            case "tragedy":
                result = 40_000;
                if (perf.audience > 30)
                    result += 1_000 * (perf.audience - 30);
                break;
            case "comedy":
                result = 30_000;
                if (perf.audience > 20)
                    result += 10_000 + 500 * (perf.audience - 20);
                result += 300 * perf.audience;
                break;

            default:
                throw new System.Exception($"알 수 없는 장르: {play.type}");
        }

        return result;
    }

    string Origin( (string customer, (string playId, int audience)[] performances) invoice, (string id, string name, string type)[] plays)
    {
        float totalAmount = 0;
        int volumeCredits = 0;
        var result = new StringBuilder($"청구내역 (고객명: {invoice.customer})\n");
        var format = new CultureInfo("en-US", false).NumberFormat;
        format.CurrencySymbol = "$";
        format.CurrencyNegativePattern = 1;
        format.NumberNegativePattern = 1;

        foreach (var perf in invoice.performances)
        {
            var play = plays.FirstOrDefault(x => x.id == perf.playId);
            float thisAmount = 0;

            switch (play.type)
            {
                case "tragedy":
                    thisAmount = 40_000;

                    if (perf.audience > 30)
                    {
                        thisAmount += 1_000 * (perf.audience - 30);
                    }
                    break;
                case "comedy":
                    thisAmount = 30_000;

                    if (perf.audience > 20)
                    {
                        thisAmount += 10_000 + 500 * (perf.audience - 20);
                    }
                    thisAmount += 300 * perf.audience;
                    break;

                default:
                    throw new System.Exception($"알 수 없는 장르: {play.type}");
            }

            // 포인트를 적립한다.
            volumeCredits += Mathf.Max(perf.audience - 30, 0);

            // 희극 관객 5명마다 추가 포인트를 제공한다.
            if (play.type == "comedy")
            {
                volumeCredits += Mathf.FloorToInt(perf.audience / 5);
            }

            // 청구 내역을 출력한다.
            result.AppendLine($"{play.name}: {(thisAmount / 100)} {perf.audience}석");
            totalAmount += thisAmount;
        }
        result.AppendLine($"총액 {(totalAmount / 100)}");
        result.AppendLine($"적립 포인트 {volumeCredits}점");

        return result.ToString();
    }
}


class save
{
    void Do()
    {
        (string customer, (string playId, int audience)[] performances) invoice =
            (
              customer: "BigCo",
              performances: new (string playId, int audience)[]
              {
                ( "hamlet", audience : 55 ),
                ( "as-like", audience : 35 ),
                ( "othello", audience : 40 )
              }
                );
        (string id, string name, string type)[] plays = new (string id, string name, string type)[]
        {
              (id:"hamlet", name: "Hamlet", type: "tragedy" ),
              (id:"as-like", name: "As YOu Like it", type: "comedy" ),
              (id:"othello", name: "Othello", type: "tragedy" )
        };
        string text = Statement(invoice, plays);

        //Debug.Assert(result == text);
        //print(text);
    }

    //    {
    //  "customer": "BigCo",
    //  "performances": [
    //    { "playID": "hamlet", "audience": 55 },
    //    { "playID": "as-like", "audience": 35 },
    //    { "playID": "othello", "audience": 40 }
    //        ]
    //    }


    //    {
    //  "hamlet": { "name": "Hamlet", "type": "tragedy" },
    //  "as-like": { "name": "As YOu Like it", "type": "comedy" },
    //  "othello": { "name": "Othello", "type": "tragedy" }
    //}

    public string Statement((string customer, (string playId, int audience)[] performances) invoice, (string id, string name, string type)[] plays)
    {
        var format = BuildFormat();
        return BuildText(invoice.customer, CalculrateInvoeData(invoice.performances, plays));
    }

    (float amount, int credit, StringBuilder claimText)
        CalculrateInvoeData((string playId, int audience)[] performances, (string id, string name, string type)[] plays)
    {
        (float amount, int credit, StringBuilder claimText) result = (0, 0, new StringBuilder(""));
        foreach (var perf in performances)
        {
            var play = plays.FirstOrDefault(x => x.id == perf.playId);

            result.credit += CalculatePoint(perf, play);
            result.amount += CalculateAmount(perf, play);
            result.claimText.AppendLine($"{play.name}: {CalculateAmount(perf, play) / 100} {perf.audience}석");
        }
        return result;
    }

    NumberFormatInfo BuildFormat()
    {
        var format = new CultureInfo("en-US", false).NumberFormat;
        format.CurrencySymbol = "$";
        format.CurrencyNegativePattern = 1;
        format.NumberNegativePattern = 1;
        return format;
    }

    string BuildText(string customer, (float amount, int credit, StringBuilder claimText) invoiceData)
    {
        var result = new StringBuilder($"청구내역 (고객명: {customer})\n");
        result.Append(invoiceData.claimText.ToString());
        result.AppendLine($"총액 {invoiceData.amount / 100}");
        result.AppendLine($"적립 포인트 {invoiceData.credit}점");
        return result.ToString();
    }

    int CalculatePoint((string playId, int audience) perf, (string id, string name, string type) play)
    {
        int result = 0;
        result += Mathf.Max(perf.audience - 30, 0);
        result = CalculrateMileage(result, perf, play);
        return result;

        int CalculrateMileage(int volumeCredits, (string playId, int audience) perf, (string id, string name, string type) play)
        {
            if (play.type == "comedy")
                volumeCredits += Mathf.FloorToInt(perf.audience / 5);

            return volumeCredits;
        }
    }

    float CalculateAmount((string playId, int audience) perf, (string id, string name, string type) play)
    {
        float result;
        switch (play.type)
        {
            case "tragedy":
                result = 40_000;
                if (perf.audience > 30)
                    result += 1_000 * (perf.audience - 30);
                break;
            case "comedy":
                result = 30_000;
                if (perf.audience > 20)
                    result += 10_000 + 500 * (perf.audience - 20);
                result += 300 * perf.audience;
                break;

            default:
                throw new System.Exception($"알 수 없는 장르: {play.type}");
        }

        return result;
    }

}