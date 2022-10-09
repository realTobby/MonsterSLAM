using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public HammerStart _hammerStart;

    public void OpenCredits()
    {
        _hammerStart.OpenScene(2);
    }


}
