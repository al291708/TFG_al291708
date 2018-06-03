using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceButtons : MonoBehaviour {

    public int cost;
    public int effectDurationSeconds;
    private bool inUse;
    public Util.Action action;
    public Util.Factions faction;
    private float remainingTimeOfUse;
    public bool isActive; // saves if action enabled

	// Use this for initialization
    void Start()
    {
        init();
	}

    public void init()
    {
        inUse = false;
        disableButton();
    }

    void FixedUpdate()
    {
        if (inUse)
        {
            remainingTimeOfUse -= Time.fixedDeltaTime;
            if (remainingTimeOfUse <= 0)
            {
                if (action == Util.Action.IceForce)
                {
                    if (faction == Util.Factions.Ittla)
                    {
                        Util.gc.unfreezeNertaTotems();
                    }
                    else if (faction == Util.Factions.Nerta)
                    {
                        Util.gc.unfreezeIttlaTotems();
                    }
                }
                else if (action == Util.Action.WindForce)
                {
                    if (faction == Util.Factions.Ittla)
                    {
                        Util.gc.decelerateIttlaTotems();
                    }
                    else if (faction == Util.Factions.Nerta)
                    {
                        Util.gc.decelerateNertaTotems();
                    }
                }

                stopUsing();
                
            }
        }
    }


    public void enableButton()
    {
        if ((faction == Util.Factions.Ittla && Util.ittlaNPC == null) ||
            (faction == Util.Factions.Nerta && Util.nertaNPC == null))
        {
            // Only allow the use of the buttons if the Faction is not controlled by the machine
            GetComponent<Button>().enabled = true; // enable click functionality for human player
        }
        isActive = true; // saves that button is available because action is
        GetComponent<Image>().color = Color.white; 
        if (faction == Util.Factions.Ittla)
        {
            if(Util.ittlaAvailableActions.Contains(action) == false){
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
        //Debug.Log("Activo botón" + GetComponent<Button>().name);
    }

    public void disableButton()
    {
        GetComponent<Button>().enabled = false; // disable click functionality for human player
        isActive = false; // saves that button is not available because action isn't
        GetComponent<Image>().color = Color.gray;
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
        //Debug.Log("Desactivo boton" + GetComponent<Button>().name);
    }

    public bool isInUse()
    {
        return inUse;
    }

    public void stopUsing(){
        inUse = false;
    }

    public void use(){
        remainingTimeOfUse = effectDurationSeconds;
        if (action == Util.Action.IceForce)
        {
            if (faction == Util.Factions.Ittla)
            {
                if (!Util.IS_TRAINING)
                {
                    Util.gc.audiosourceICE.Play();
                    Util.gc.iceParticlesIttla.GetComponent<ParticleSystem>().Play(true);
                }
                Util.gc.freezeNertaTotems();
            }
            else if (faction == Util.Factions.Nerta)
            {
                if (!Util.IS_TRAINING)
                {
                    Util.gc.audiosourceICE.Play();
                    Util.gc.iceParticlesNerta.GetComponent<ParticleSystem>().Play(true);
                }
                Util.gc.freezeIttlaTotems();
            }

        }
        else if (action == Util.Action.WindForce)
        {
            if (faction == Util.Factions.Ittla)
            {
                if (!Util.IS_TRAINING)
                {
                    Util.gc.audiosourceWIND.Play();
                    Util.gc.windParticlesIttla.GetComponent<ParticleSystem>().Play(true);
                }
                Util.gc.accelerateIttlaTotems();
            }
            else if (faction == Util.Factions.Nerta)
            {
                if (!Util.IS_TRAINING)
                {
                    Util.gc.audiosourceWIND.Play();
                    Util.gc.windParticlesNerta.GetComponent<ParticleSystem>().Play(true);
                }
                Util.gc.accelerateNertaTotems();
            }
        }
        inUse = true;
    }
}
