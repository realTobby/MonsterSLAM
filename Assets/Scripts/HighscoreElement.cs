using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreElement : MonoBehaviour
{
    public TMPro.TextMeshProUGUI NameText;
    public TMPro.TextMeshProUGUI ScoreText;

    public void Init(string name, int score)
    {
        NameText.text = "Name: " + name;
        ScoreText.text = "Score: " + score;
    }
}
