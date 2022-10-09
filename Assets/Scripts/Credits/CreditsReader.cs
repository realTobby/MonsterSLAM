using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsReader
{
    public const string CreditsFile = "credits";

    public List<CreditHolder> GetCredits()
    {
        List<CreditHolder> credits = new List<CreditHolder>();
        TextAsset rawCreditsFile = Resources.Load<TextAsset>(CreditsFile);
        var listToReturn = new List<string>();
        var arrayString = rawCreditsFile.text.Split('\n');
        foreach (var line in arrayString)
        {
            string lineParsed = line.Replace("\r", string.Empty);
            string[] parts = lineParsed.Split(',');

            CreditHolder newCreditHolder = new CreditHolder();
            newCreditHolder.Description = parts[0];
            newCreditHolder.Author = parts[1];
            newCreditHolder.Source = parts[2];

            credits.Add(newCreditHolder);
        }
        return credits;
    }
    


}
