using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TAJAcademy : Academy {

    int resets;
    Text resetsText;
    //int steps;


    public override void InitializeAcademy()
    {
        //steps = 0;
        resets = 0;
        if (GameObject.FindGameObjectWithTag("RESETS") != null)
        {
            resetsText = GameObject.FindGameObjectWithTag("RESETS").GetComponent<Text>();
        }
    }

    public override void AcademyReset()
    {
        base.AcademyReset();
        resets += 1;
        if (resetsText != null)
        {
            resetsText.text = "Resets: " + resets;
        }
        Debug.Log("RESETEO ACADEMY");
        if (Util.gc != null && Util.IS_TRAINING)
        {
            Util.gc.startGame();
        }
    }

    public override void AcademyStep()
    {
        base.AcademyStep();
        /*steps += 1;
        resetsText.text = "Steps: " + steps;*/
        if (Util.academyShouldBeReset)
        {
            resets = Util.partidas;
            AcademyReset();         
        }
    }
}
