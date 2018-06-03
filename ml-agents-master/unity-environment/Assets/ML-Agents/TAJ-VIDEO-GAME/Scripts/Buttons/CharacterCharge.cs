using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCharge : MonoBehaviour {

    public float chargeSpeedInSeconds;
    [SerializeField]
    private float charge;
    public Button button;
    public Util.Action action;
    public Util.Factions faction;

	// Use this for initialization
	void Start () {
        //init();
	}

    public void init()
    {
        charge = 0;
        GetComponent<Image>().fillAmount = charge;
        disableButton();
        StartCoroutine("startChargeCoroutine");
    }
    
    IEnumerator startChargeCoroutine()
    {
        while(Util.gameOver == false)
        {
            yield return new WaitForSeconds(chargeSpeedInSeconds);
            charge += 0.1f;
            GetComponent<Image>().fillAmount = charge;
            //Debug.Log("Carga: " + charge);

            if (charge >= 1f)
            {
                enableButton();
                Util.gc.checkIttlaButtonsStatus();
                Util.gc.checkNertaButtonsStatus();
                break;
            }
        }
    }

    public void emptyCharge(){
        charge = 0f;
        GetComponent<Image>().fillAmount = charge;
        disableButton();
        StartCoroutine("startChargeCoroutine");
        Util.gc.checkIttlaButtonsStatus();
        Util.gc.checkNertaButtonsStatus();
    }

    public void fulfillCharge()
    {
        charge = 1f;
        GetComponent<Image>().fillAmount = charge;
        enableButton();
        Util.gc.checkIttlaButtonsStatus();
        Util.gc.checkNertaButtonsStatus();
    }

    public float getCharge()
    {
        return charge;
    }

    public void enableButton()
    {
        if ((faction == Util.Factions.Ittla && Util.ittlaNPC == null) ||
            (faction == Util.Factions.Nerta && Util.nertaNPC == null))
        {
            // Only allow the use of the buttons if the Faction is not controlled by the machine
            button.enabled = true;
        }
        button.GetComponent<Image>().color = Color.white;
        if (faction == Util.Factions.Ittla)
        {
            if (Util.ittlaAvailableActions.Contains(action) == false)
            {
                Util.ittlaAvailableActions.Add(action);
                //GameController.debugAvailableActions();
            }
        }
        else if (faction == Util.Factions.Nerta)
        {
            if (Util.nertaAvailableActions.Contains(action) == false)
            {
                Util.nertaAvailableActions.Add(action);
                //GameController.debugAvailableActions();
            }
        }
        //Debug.Log("Activo botón" + button.name + " Carga: " + charge);
    }

    public void disableButton()
    {
        button.enabled = false;
        button.GetComponent<Image>().color = Color.gray;
        if (faction == Util.Factions.Ittla)
        {
            Util.ittlaAvailableActions.Remove(action);
            //GameController.debugAvailableActions();
        }
        else if (faction == Util.Factions.Nerta)
        {
            Util.nertaAvailableActions.Remove(action);
            //GameController.debugAvailableActions();
        }
        //Debug.Log("Desactivo boton" + button.name + " Carga: " + charge);
    }

    public bool isCharged()
    {
        if (charge >= 1.0f)
        {
            return true;
        }
        return false;
    }
}
