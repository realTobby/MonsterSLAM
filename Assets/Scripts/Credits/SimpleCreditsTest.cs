using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCreditsTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CreditsReader cr = new CreditsReader();

        var res = cr.GetCredits();

        

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
