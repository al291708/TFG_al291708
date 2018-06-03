using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gap : MonoBehaviour
{

    //HUD selectedTotem;
    bool enabledGap = false;
    Util.Factions faction;
    public Transform goal;
    public Transform relatedPowerStone;
    public int pathID;

    void Start()
    {
        disable();
    }

    void OnMouseDown()
    {
        doGapAction();
    }

    public void disable()
    {
        enabledGap = false;
        GetComponent<Renderer>().material.color = Color.gray;
    }

    public void enable()
    {
        enabledGap = true;
        if (faction == Util.Factions.Ittla) {
            GetComponent<Renderer>().material = Util.gc.ittlaMaterial;
        } else {
            GetComponent<Renderer>().material = Util.gc.nertaMaterial;
        }
    }

    public bool isEnabled()
    {
        return enabledGap;
    }

    public void setFaction(Util.Factions f)
    {
        faction = f;
    }

    public Util.Factions getFaction()
    {
        return faction;
    }

    public void doGapAction()
    {
        if (enabledGap)
        {

            GameObject prefabToInstantiate = null;
            if (faction == Util.Factions.Ittla)
            {
                if (Util.ittlaPlayerSelectedAction == Util.Action.FaithForce)
                {
                    Util.gc.takeOffIttlaSpirits(Util.gc.ittlaFaithForceButton.GetComponent<ForceButtons>().cost);

                    Util.gc.killNertaTotemsOnPath(pathID);

                }
                else
                {
                    Util.gc.emptySelectedOptionCharge(faction);

                    prefabToInstantiate = Util.gc.GetComponent<GameController>().getTotemBySelectedAction(Util.ittlaPlayerSelectedAction);

                    GameObject instantiatedTotem = Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
                    instantiatedTotem.GetComponent<Renderer>().material = Util.gc.ittlaMaterial;

                    if (instantiatedTotem.transform.childCount >= 1)
                    {
                        GameObject model = instantiatedTotem.transform.GetChild(0).gameObject;
                        model.GetComponent<Renderer>().material = Util.gc.ittlaMaterial;
                    }

                    instantiatedTotem.GetComponent<Totem>().setFaction(Util.Factions.Ittla);
                    instantiatedTotem.GetComponent<Totem>().moveTo(goal);
                    instantiatedTotem.GetComponent<Totem>().setPathID(pathID);
                    instantiatedTotem.GetComponent<Totem>().setIsFrozen(false);
                    if (Util.IS_TRAINING)
                    {
                        instantiatedTotem.GetComponent<MeshRenderer>().enabled = false;
                        foreach (Transform go in instantiatedTotem.transform)
                        {
                            if (go.GetComponent<MeshRenderer>() != null)
                            {
                                go.GetComponent<MeshRenderer>().enabled = false;
                            }
                        }
                    }
                    else
                    {
                        Util.gc.audiosourceINSERTTOTEM.Play();
                    }

                    Util.gc.ittlaOnBoardTotems.Add(instantiatedTotem);

                }
                Util.gc.GetComponent<GameController>().disableIttlaGaps();
                Util.ittlaPlayerSelectedAction = Util.Action.None;
            }
            else if (faction == Util.Factions.Nerta)
            {
                if (Util.nertaPlayerSelectedAction == Util.Action.FaithForce)
                {
                    Util.gc.takeOffNertaSpirits(Util.gc.nertaFaithForceButton.GetComponent<ForceButtons>().cost);

                    Util.gc.killIttlaTotemsOnPath(pathID);

                }
                else
                {
                    Util.gc.emptySelectedOptionCharge(faction);

                    prefabToInstantiate = Util.gc.GetComponent<GameController>().getTotemBySelectedAction(Util.nertaPlayerSelectedAction);

                    GameObject instantiatedTotem = Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
                    instantiatedTotem.GetComponent<Renderer>().material = Util.gc.nertaMaterial;


                    if (instantiatedTotem.transform.childCount >= 1)
                    {
                        GameObject model = instantiatedTotem.transform.GetChild(0).gameObject;
                        model.GetComponent<Renderer>().material = Util.gc.nertaMaterial;
                    }

                    instantiatedTotem.GetComponent<Totem>().setFaction(Util.Factions.Nerta);
                    instantiatedTotem.GetComponent<Totem>().moveTo(goal);
                    instantiatedTotem.GetComponent<Totem>().setPathID(pathID);
                    instantiatedTotem.GetComponent<Totem>().setIsFrozen(false);
                    if (Util.IS_TRAINING)
                    {
                        instantiatedTotem.GetComponent<MeshRenderer>().enabled = false;
                        foreach (Transform go in instantiatedTotem.transform)
                        {
                            if (go.GetComponent<MeshRenderer>() != null)
                            {
                                go.GetComponent<MeshRenderer>().enabled = false;
                            }
                        }
                    }
                    else
                    {
                        Util.gc.audiosourceINSERTTOTEM.Play();
                    }
                    Util.gc.nertaOnBoardTotems.Add(instantiatedTotem);

                }
                Util.gc.GetComponent<GameController>().disableNertaGaps();
                Util.nertaPlayerSelectedAction = Util.Action.None;
            }
        }
    }    
}
