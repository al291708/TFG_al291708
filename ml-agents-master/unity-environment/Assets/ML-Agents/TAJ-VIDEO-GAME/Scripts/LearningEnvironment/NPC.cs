using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public Util.Factions faction;
    public bool isRandomActionsNPC;
    public float minTimeBetweenDecisions;
    public float maxTimeBetweenDecisions;
    
	void Start () {

        if (faction == Util.Factions.Ittla)
        {
            Util.ittlaNPC = this;
        } else if (faction == Util.Factions.Nerta)
        {
            Util.nertaNPC = this;
        }
        play();
	}

    public void play()
    {
        if (isRandomActionsNPC)
        {
            playRandom();
        }
        else
        {
            playRequestingAgentDecisions();
        }
    }

    private void playRandom()
    {
        if (faction == Util.Factions.Ittla)
        {
            StartCoroutine("ittlaDoActionsRandomly");
        }
        else if (faction == Util.Factions.Nerta)
        {
            StartCoroutine("nertaDoActionsRandomly");
        }
    }

    private void playRequestingAgentDecisions()
    {
        StartCoroutine("requestAgentDecision");
    }

    IEnumerator requestAgentDecision()
    {
        while (Util.academyShouldBeReset == false)
        {
            float randomWaitingTime = UnityEngine.Random.Range(minTimeBetweenDecisions, maxTimeBetweenDecisions);
            if (GetComponent<Agent>() != null)
            {
                GetComponent<Agent>().RequestDecision();
            }

            yield return new WaitForSeconds(randomWaitingTime);
        }
    }

    IEnumerator ittlaDoActionsRandomly()
    {
        while (Util.academyShouldBeReset == false)
        {

            // Do things
            if (Util.ittlaPlayerSelectedAction == Util.Action.None)
            {
                doIttlaActionWithRandomGapChoice(Util.ittlaAvailableActions[Random.Range(0, Util.ittlaAvailableActions.Count - 1)]);
            }

            yield return new WaitForSeconds(Random.Range(minTimeBetweenDecisions, maxTimeBetweenDecisions));
        }        
    }

    IEnumerator nertaDoActionsRandomly()
    {
        while (Util.academyShouldBeReset == false)
        {
            // Do things
            if (Util.nertaPlayerSelectedAction == Util.Action.None)
            {
                doNertaActionWithRandomGapChoice(Util.nertaAvailableActions[Random.Range(0, Util.nertaAvailableActions.Count - 1)]);
            }

            yield return new WaitForSeconds(Random.Range(minTimeBetweenDecisions, maxTimeBetweenDecisions));
        }
    }
    
    private void doIttlaActionWithRandomGapChoice(Util.Action action)
    {
        if (action == Util.Action.Vasu)
        {
            Util.gc.ittlaSelectVasu();
            StartCoroutine("useRandomIttlaGap");
        }
        else if (action == Util.Action.Kaapo)
        {
            Util.gc.ittlaSelectKaapo();
            StartCoroutine("useRandomIttlaGap");
        }
        else if (action == Util.Action.Rad)
        {
            Util.gc.ittlaSelectRad();
            StartCoroutine("useRandomIttlaGap");
        }
        else if (action == Util.Action.IceForce)
        {
            Util.gc.ittlaSelectIceForce();
        }
        else if (action == Util.Action.WindForce)
        {
            Util.gc.ittlaSelectWindForce();
        }
        else if (action == Util.Action.SunForce)
        {
            Util.gc.ittlaSelectSunForce();

        }
        else if (action == Util.Action.FaithForce)
        {
            Util.gc.ittlaSelectFaithForce();
            StartCoroutine("useRandomIttlaGap");            
        }
    }


    public float doIttlaActionDiscrete(int discreteAction)
    {
        float reward = 0;

        // DISCRETE ACTION - ACTION TO DO
        // 0 - Vasu Path 1
        // 1 - Vasu Path 2
        // 2 - Vasu Path 3
        // 3 - Vasu Path Shrine
        // 4 - Rad Path 1
        // 5 - Rad Path 2
        // 6 - Rad Path 3
        // 7 - Rad Path Shrine
        // 8 - Kaapo Path 1
        // 9 - Kaapo Path 2
        // 10 - Kaapo Path 3
        // 11 - Kaapo Path Shrine
        // 12 - Ice
        // 13 - Wind
        // 14 - Sun
        // 15 - Faith Path 1
        // 16 - Faith Path 2
        // 17 - Faith Path 3
        // 18 - Faith Path Shrine

        
        switch (discreteAction)
        {
            case 0:
                // 0 - Vasu Path 1
                if (Util.ittlaAvailableActions.Contains(Util.Action.Vasu))
                {
                    // Si este path tiene power stone del enemigo aún viva, recompensar más
                    if (Util.gc.pathHasNertaPowerStoneAlive(0))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward += 0.1f;
                    }
                    // Si este path tiene power stone del enemigo conquistada y no hay enemigos en el path, penalizar
                    if (!Util.gc.pathHasNertaPowerStoneAlive(0) && Util.gc.isPathEmptyOfNertaTotems(1))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward -= 0.002f;
                    }
                    // Si hay algún enemigo y todos ellos suman igual o menos que la vida de Rad, y mi power stone de este camino no está conquistada (hay que defenderla), recompenso porque al poner Vasu los mato
                    if (Util.gc.pathHasIttlaPowerStoneAlive(0) && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(1) > 0 && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(1) <= Util.VASU_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }
                    // Si hay algún enemigo y todos ellos suman más que la vida de Vasu, recompenso porque lo mejor que puedo hacer es poner Vasu(el totem que más vida tiene)
                    if ( (Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(1) > 0) && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(1) > Util.VASU_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }
                    

                    Util.gc.ittlaSelectVasu();
                    useIttlaGap(0);
                }
                break;
            case 1:
                // 1 - Vasu Path 2
                if (Util.ittlaAvailableActions.Contains(Util.Action.Vasu))
                {
                    // Si este path tiene power stone del enemigo aún viva, recompensar más
                    if (Util.gc.pathHasNertaPowerStoneAlive(1))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 1.");
                        reward += 0.1f;
                    }
                    // Si este path tiene power stone del enemigo conquistada y no hay enemigos en el path, penalizar
                    if (!Util.gc.pathHasNertaPowerStoneAlive(1) && Util.gc.isPathEmptyOfNertaTotems(2))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward -= 0.002f;
                    }
                    // Si hay algún enemigo y todos ellos suman igual o menos que la vida de Rad, y mi power stone de este camino no está conquistada (hay que defenderla), recompenso porque al poner Vasu los mato
                    if (Util.gc.pathHasIttlaPowerStoneAlive(1) && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(2) > 0 && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(2) <= Util.VASU_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }
                    // Si hay algún enemigo y todos ellos suman más que la vida de Vasu, recompenso porque lo mejor que puedo hacer es poner Vasu(el totem que más vida tiene)
                    if ((Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(2) > 0) && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(2) > Util.VASU_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }

                    Util.gc.ittlaSelectVasu();
                    useIttlaGap(1);
                }
                break;
            case 2:
                // 2 - Vasu Path 3
                if (Util.ittlaAvailableActions.Contains(Util.Action.Vasu))
                {

                    // Si este path tiene power stone del enemigo aún viva, recompensar más
                    if (Util.gc.pathHasNertaPowerStoneAlive(2))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 2.");
                        reward += 0.1f;
                    }
                    // Si este path tiene power stone del enemigo conquistada y no hay enemigos en el path, penalizar
                    if (!Util.gc.pathHasNertaPowerStoneAlive(2) && Util.gc.isPathEmptyOfNertaTotems(3))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward -= 0.002f;
                    }
                    // Si hay algún enemigo y todos ellos suman igual o menos que la vida de Rad, y mi power stone de este camino no está conquistada (hay que defenderla), recompenso porque al poner Vasu los mato
                    if (Util.gc.pathHasIttlaPowerStoneAlive(2) && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(3) > 0 && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(3) <= Util.VASU_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }
                    // Si hay algún enemigo y todos ellos suman más que la vida de Vasu, recompenso porque lo mejor que puedo hacer es poner Vasu(el totem que más vida tiene)
                    if ((Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(3) > 0) && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(3) > Util.VASU_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }

                    Util.gc.ittlaSelectVasu();
                    useIttlaGap(2);
                }
                break;
            case 3:
                // 3 - Vasu Path Shrine
                if (Util.ittlaAvailableActions.Contains(Util.Action.Vasu))
                {
                    Util.gc.ittlaSelectVasu();
                    useIttlaGap(3);
                }
                break;
            case 4:
                // 4 - Rad Path 1
                if (Util.ittlaAvailableActions.Contains(Util.Action.Rad))
                {
                    // Si este path tiene power stone del enemigo aún viva, recompensar más
                    if (Util.gc.pathHasNertaPowerStoneAlive(0))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward += 0.1f;
                    }
                    // Si este path tiene power stone del enemigo conquistada y no hay enemigos en el path, penalizar
                    if (!Util.gc.pathHasNertaPowerStoneAlive(0) && Util.gc.isPathEmptyOfNertaTotems(1))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward -= 0.002f;
                    }
                    // Si hay algún enemigo y todos ellos suman igual o menos que la vida de Rad, y mi power stone de este camino no está conquistada (hay que defenderla), recompenso porque al poner Rad los mato
                    if (Util.gc.pathHasIttlaPowerStoneAlive(0) && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(1) > 0 && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(1) <= Util.RAD_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }
                    
                    Util.gc.ittlaSelectRad();
                    useIttlaGap(0);
                }
                break;
            case 5:
                // 5 - Rad Path 2
                if (Util.ittlaAvailableActions.Contains(Util.Action.Rad))
                {
                    // Si este path tiene power stone del enemigo aún viva, recompensar más
                    if (Util.gc.pathHasNertaPowerStoneAlive(1))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 1.");
                        reward += 0.1f;
                    }
                    // Si este path tiene power stone del enemigo conquistada y no hay enemigos en el path, penalizar
                    if (!Util.gc.pathHasNertaPowerStoneAlive(1) && Util.gc.isPathEmptyOfNertaTotems(2))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward -= 0.002f;
                    }
                    // Si hay algún enemigo y todos ellos suman igual o menos que la vida de Rad, y mi power stone de este camino no está conquistada (hay que defenderla), recompenso porque al poner Rad los mato
                    if (Util.gc.pathHasIttlaPowerStoneAlive(1) && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(2) > 0 && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(2) <= Util.RAD_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }

                    Util.gc.ittlaSelectRad();
                    useIttlaGap(1);
                }
                break;
            case 6:
                // 6 - Rad Path 3
                if (Util.ittlaAvailableActions.Contains(Util.Action.Rad))
                {
                    // Si este path tiene power stone del enemigo aún viva, recompensar más
                    if (Util.gc.pathHasNertaPowerStoneAlive(2))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 2.");
                        reward += 0.1f;
                    }
                    // Si este path tiene power stone del enemigo conquistada y no hay enemigos en el path, penalizar
                    if (!Util.gc.pathHasNertaPowerStoneAlive(2) && Util.gc.isPathEmptyOfNertaTotems(3))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward -= 0.002f;
                    }
                    // Si hay algún enemigo y todos ellos suman igual o menos que la vida de Rad, y mi power stone de este camino no está conquistada (hay que defenderla), recompenso porque al poner Rad los mato
                    if (Util.gc.pathHasIttlaPowerStoneAlive(2) && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(3) > 0 && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(3) <= Util.RAD_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }

                    Util.gc.ittlaSelectRad();
                    useIttlaGap(2);
                }
                break;
            case 7:
                // 7 - Rad Path Shrine
                if (Util.ittlaAvailableActions.Contains(Util.Action.Rad))
                {
                    Util.gc.ittlaSelectRad();
                    useIttlaGap(3);
                }
                break;
            case 8:
                // 8 - Kaapo Path 1
                if (Util.ittlaAvailableActions.Contains(Util.Action.Kaapo))
                {
                    // Si este path tiene power stone del enemigo aún viva, recompensar más
                    if (Util.gc.pathHasNertaPowerStoneAlive(0))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward += 0.1f;
                    }
                    // Si este path tiene power stone del enemigo conquistada y no hay enemigos en el path, penalizar
                    if (!Util.gc.pathHasNertaPowerStoneAlive(0) && Util.gc.isPathEmptyOfNertaTotems(1))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward -= 0.002f;
                    }
                    // Si hay algún enemigo y todos ellos suman igual o menos que la vida de Rad, y mi power stone de este camino no está conquistada (hay que defenderla), recompenso porque al poner Kaapo los mato
                    if (Util.gc.pathHasIttlaPowerStoneAlive(0) && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(1) > 0 && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(1) <= Util.KAAPO_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }

                    Util.gc.ittlaSelectKaapo();
                    useIttlaGap(0);
                }
                break;
            case 9:
                // 9 - Kaapo Path 2
                if (Util.ittlaAvailableActions.Contains(Util.Action.Kaapo))
                {

                    // Si este path tiene power stone del enemigo aún viva, recompensar más
                    if (Util.gc.pathHasNertaPowerStoneAlive(1))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 1.");
                        reward += 0.1f;
                    }
                    // Si este path tiene power stone del enemigo conquistada y no hay enemigos en el path, penalizar
                    if (!Util.gc.pathHasNertaPowerStoneAlive(1) && Util.gc.isPathEmptyOfNertaTotems(2))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward -= 0.002f;
                    }
                    // Si hay algún enemigo y todos ellos suman igual o menos que la vida de Rad, y mi power stone de este camino no está conquistada (hay que defenderla), recompenso porque al poner Kaapo los mato
                    if (Util.gc.pathHasIttlaPowerStoneAlive(1) && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(2) > 0 && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(2) <= Util.KAAPO_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }
                    Util.gc.ittlaSelectKaapo();
                    useIttlaGap(1);
                }
                break;
            case 10:
                // 10 - Kaapo Path 3
                if (Util.ittlaAvailableActions.Contains(Util.Action.Kaapo))
                {
                    // Si este path tiene power stone del enemigo aún viva, recompensar más
                    if (Util.gc.pathHasNertaPowerStoneAlive(2))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 2.");
                        reward += 0.1f;
                    }
                    // Si este path tiene power stone del enemigo conquistada y no hay enemigos en el path, penalizar
                    if (!Util.gc.pathHasNertaPowerStoneAlive(2) && Util.gc.isPathEmptyOfNertaTotems(3))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward -= 0.002f;
                    }
                    // Si hay algún enemigo y todos ellos suman igual o menos que la vida de Rad, y mi power stone de este camino no está conquistada (hay que defenderla), recompenso porque al poner Kaapo los mato
                    if (Util.gc.pathHasIttlaPowerStoneAlive(2) && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(3) > 0 && Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(3) <= Util.KAAPO_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }
                    
                    Util.gc.ittlaSelectKaapo();
                    useIttlaGap(2);
                }
                break;
            case 11:
                // 11 - Kaapo Path Shrine
                if (Util.ittlaAvailableActions.Contains(Util.Action.Kaapo))
                {
                    Util.gc.ittlaSelectKaapo();
                    useIttlaGap(3);
                }
                break;
            case 12:
                // 12 - Ice
                if (Util.ittlaAvailableActions.Contains(Util.Action.IceForce))
                {
                    Util.gc.ittlaSelectIceForce();
                    if (Util.gc.nertaOnBoardTotems.Count > 0) // If ice is used and there are enemy totems it will affect to
                    {
                        //Debug.Log("Uso hielo y hay enemigos");
                        reward = 0.1f;
                    }
                }
                break;
            case 13:
                // 13 - Wind
                if (Util.ittlaAvailableActions.Contains(Util.Action.WindForce))
                {
                    Util.gc.ittlaSelectWindForce();
                    if (Util.gc.ittlaOnBoardTotems.Count > 0) // If wind is used and there are totems it will affect to
                    {
                        //Debug.Log("Uso viento y tengo totems");
                        reward = 0.1f;
                    }
                }
                break;
            case 14:
                // 14 - Sun
                if (Util.ittlaAvailableActions.Contains(Util.Action.SunForce))
                {
                    if ((Util.gc.ittlaVasuCharge.GetComponent<CharacterCharge>().getCharge() < 1f ||
                     Util.gc.ittlaKaapoCharge.GetComponent<CharacterCharge>().getCharge() < 1f ||
                     Util.gc.ittlaRadCharge.GetComponent<CharacterCharge>().getCharge() < 1f)) // If at least one totem has less that 100% charge
                    {
                        //Debug.Log("Uso sol y no tenia carga en algun totem");
                        reward = 0.05f;
                    }
                    Util.gc.ittlaSelectSunForce();
                }
                break;
            case 15:
                // 15 - Faith Path 1
                if (Util.ittlaAvailableActions.Contains(Util.Action.FaithForce))
                {
                    Util.gc.ittlaSelectFaithForce();
                    if (!Util.gc.isPathEmptyOfNertaTotems(1))
                    {
                        //Debug.Log("Uso fe y habia enemigos en path 0");
                        reward = 0.2f;
                    }
                    useIttlaGap(0);
                }
                break;
            case 16:
                // 16 - Faith Path 2
                if (Util.ittlaAvailableActions.Contains(Util.Action.FaithForce))
                {
                    Util.gc.ittlaSelectFaithForce();
                    if (!Util.gc.isPathEmptyOfNertaTotems(2))
                    {
                        //Debug.Log("Uso fe y habia enemigos en path 1");
                        reward = 0.2f;
                    }
                    useIttlaGap(1);
                }
                break;
            case 17:
                if (Util.ittlaAvailableActions.Contains(Util.Action.FaithForce))
                {
                    // 17 - Faith Path 3
                    Util.gc.ittlaSelectFaithForce();
                    if (!Util.gc.isPathEmptyOfNertaTotems(3))
                    {
                        //Debug.Log("Uso fe y habia enemigos en path 2");
                        reward = 0.2f;
                    }
                    useIttlaGap(2);
                }
                break;
            case 18:
                // 18 - Faith Path Shrine
                if (Util.ittlaAvailableActions.Contains(Util.Action.FaithForce))
                {
                    Util.gc.ittlaSelectFaithForce();
                    if (!Util.gc.isPathEmptyOfNertaTotems(0))
                    {
                        //Debug.Log("Uso fe y habia enemigos en path shrine");
                        reward = 0.2f;
                    }
                    useIttlaGap(3);
                }
                break;
            default:
                break;
        }

        return reward;
    }
      

    public float doNertaActionDiscrete(int discreteAction)
    {
        float reward = 0;

        // DISCRETE ACTION - ACTION TO DO
        // 0 - Vasu Path 1
        // 1 - Vasu Path 2
        // 2 - Vasu Path 3
        // 3 - Vasu Path Shrine
        // 4 - Rad Path 1
        // 5 - Rad Path 2
        // 6 - Rad Path 3
        // 7 - Rad Path Shrine
        // 8 - Kaapo Path 1
        // 9 - Kaapo Path 2
        // 10 - Kaapo Path 3
        // 11 - Kaapo Path Shrine
        // 12 - Ice
        // 13 - Wind
        // 14 - Sun
        // 15 - Faith Path 1
        // 16 - Faith Path 2
        // 17 - Faith Path 3
        // 18 - Faith Path Shrine

        switch (discreteAction)
        {
            case 0:
                // 0 - Vasu Path 1
                if (Util.nertaAvailableActions.Contains(Util.Action.Vasu))
                {
                    // Si este path tiene power stone del enemigo aún viva, recompensar más
                    if (Util.gc.pathHasIttlaPowerStoneAlive(0))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward += 0.1f;
                    }
                    // Si este path tiene power stone del enemigo conquistada y no hay enemigos en el path, penalizar
                    if (!Util.gc.pathHasIttlaPowerStoneAlive(0) && Util.gc.isPathEmptyOfIttlaTotems(1))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward -= 0.002f;
                    }
                    // Si hay algún enemigo y todos ellos suman igual o menos que la vida de Rad, y mi power stone de este camino no está conquistada (hay que defenderla), recompenso porque al poner Vasu los mato
                    if (Util.gc.pathHasNertaPowerStoneAlive(0) && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(1) > 0 && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(1) <= Util.VASU_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }
                    // Si hay algún enemigo y todos ellos suman más que la vida de Vasu, recompenso porque lo mejor que puedo hacer es poner Vasu(el totem que más vida tiene)
                    if ((Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(1) > 0) && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(1) > Util.VASU_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }

                    Util.gc.nertaSelectVasu();
                    useNertaGap(0);
                }
                break;
            case 1:
                // 1 - Vasu Path 2
                if (Util.nertaAvailableActions.Contains(Util.Action.Vasu))
                {
                    // Si este path tiene power stone del enemigo aún viva, recompensar más
                    if (Util.gc.pathHasIttlaPowerStoneAlive(1))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 1.");
                        reward += 0.1f;
                    }
                    // Si este path tiene power stone del enemigo conquistada y no hay enemigos en el path, penalizar
                    if (!Util.gc.pathHasIttlaPowerStoneAlive(1) && Util.gc.isPathEmptyOfIttlaTotems(2))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward -= 0.002f;
                    }
                    // Si hay algún enemigo y todos ellos suman igual o menos que la vida de Rad, y mi power stone de este camino no está conquistada (hay que defenderla), recompenso porque al poner Vasu los mato
                    if (Util.gc.pathHasNertaPowerStoneAlive(1) && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(2) > 0 && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(2) <= Util.VASU_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }
                    // Si hay algún enemigo y todos ellos suman más que la vida de Vasu, recompenso porque lo mejor que puedo hacer es poner Vasu(el totem que más vida tiene)
                    if ((Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(2) > 0) && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(2) > Util.VASU_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }
                    Util.gc.nertaSelectVasu();
                    useNertaGap(1);
                }
                break;
            case 2:
                // 2 - Vasu Path 3
                if (Util.nertaAvailableActions.Contains(Util.Action.Vasu))
                {
                    // Si este path tiene power stone del enemigo aún viva, recompensar más
                    if (Util.gc.pathHasIttlaPowerStoneAlive(2))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 2.");
                        reward += 0.1f;
                    }
                    // Si este path tiene power stone del enemigo conquistada y no hay enemigos en el path, penalizar
                    if (!Util.gc.pathHasIttlaPowerStoneAlive(2) && Util.gc.isPathEmptyOfIttlaTotems(3))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward -= 0.002f;
                    }
                    // Si hay algún enemigo y todos ellos suman igual o menos que la vida de Rad, y mi power stone de este camino no está conquistada (hay que defenderla), recompenso porque al poner Vasu los mato
                    if (Util.gc.pathHasNertaPowerStoneAlive(2) && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(3) > 0 && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(3) <= Util.VASU_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }
                    // Si hay algún enemigo y todos ellos suman más que la vida de Vasu, recompenso porque lo mejor que puedo hacer es poner Vasu(el totem que más vida tiene)
                    if ((Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(3) > 0) && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(3) > Util.VASU_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }

                    Util.gc.nertaSelectVasu();
                    useNertaGap(2);
                }
                break;
            case 3:
                // 3 - Vasu Path Shrine
                if (Util.nertaAvailableActions.Contains(Util.Action.Vasu))
                {
                    Util.gc.nertaSelectVasu();
                    useNertaGap(3);
                }
                break;
            case 4:
                // 4 - Rad Path 1
                if (Util.nertaAvailableActions.Contains(Util.Action.Rad))
                {
                    // Si este path tiene power stone del enemigo aún viva, recompensar más
                    if (Util.gc.pathHasIttlaPowerStoneAlive(0))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward += 0.1f;
                    }
                    // Si este path tiene power stone del enemigo conquistada y no hay enemigos en el path, penalizar
                    if (!Util.gc.pathHasIttlaPowerStoneAlive(0) && Util.gc.isPathEmptyOfIttlaTotems(1))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward -= 0.002f;
                    }
                    // Si hay algún enemigo y todos ellos suman igual o menos que la vida de Rad, y mi power stone de este camino no está conquistada (hay que defenderla), recompenso porque al poner Rad los mato
                    if (Util.gc.pathHasNertaPowerStoneAlive(0) && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(1) > 0 && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(1) <= Util.RAD_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }
                    Util.gc.nertaSelectRad();
                    useNertaGap(0);
                }
                break;
            case 5:
                // 5 - Rad Path 2
                if (Util.nertaAvailableActions.Contains(Util.Action.Rad))
                {
                    // Si este path tiene power stone del enemigo aún viva, recompensar más
                    if (Util.gc.pathHasIttlaPowerStoneAlive(1))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 1.");
                        reward += 0.1f;
                    }
                    // Si este path tiene power stone del enemigo conquistada y no hay enemigos en el path, penalizar
                    if (!Util.gc.pathHasIttlaPowerStoneAlive(1) && Util.gc.isPathEmptyOfIttlaTotems(2))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward -= 0.002f;
                    }
                    // Si hay algún enemigo y todos ellos suman igual o menos que la vida de Rad, y mi power stone de este camino no está conquistada (hay que defenderla), recompenso porque al poner Rad los mato
                    if (Util.gc.pathHasNertaPowerStoneAlive(1) && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(2) > 0 && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(2) <= Util.RAD_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }

                    Util.gc.nertaSelectRad();
                    useNertaGap(1);
                }
                break;
            case 6:
                // 6 - Rad Path 3
                if (Util.nertaAvailableActions.Contains(Util.Action.Rad))
                {
                    // Si este path tiene power stone del enemigo aún viva, recompensar más
                    if (Util.gc.pathHasIttlaPowerStoneAlive(2))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 2.");
                        reward += 0.1f;
                    }
                    // Si este path tiene power stone del enemigo conquistada y no hay enemigos en el path, penalizar
                    if (!Util.gc.pathHasIttlaPowerStoneAlive(2) && Util.gc.isPathEmptyOfIttlaTotems(3))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward -= 0.002f;
                    }
                    // Si hay algún enemigo y todos ellos suman igual o menos que la vida de Rad, y mi power stone de este camino no está conquistada (hay que defenderla), recompenso porque al poner Rad los mato
                    if (Util.gc.pathHasNertaPowerStoneAlive(2) && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(3) > 0 && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(3) <= Util.RAD_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }

                    Util.gc.nertaSelectRad();
                    useNertaGap(2);
                }
                break;
            case 7:
                // 7 - Rad Path Shrine
                if (Util.nertaAvailableActions.Contains(Util.Action.Rad))
                {
                    Util.gc.nertaSelectRad();
                    useNertaGap(3);
                }
                break;
            case 8:
                // 8 - Kaapo Path 1
                if (Util.nertaAvailableActions.Contains(Util.Action.Kaapo))
                {
                    // Si este path tiene power stone del enemigo aún viva, recompensar más
                    if (Util.gc.pathHasIttlaPowerStoneAlive(0))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward += 0.1f;
                    }
                    // Si este path tiene power stone del enemigo conquistada y no hay enemigos en el path, penalizar
                    if (!Util.gc.pathHasIttlaPowerStoneAlive(0) && Util.gc.isPathEmptyOfIttlaTotems(1))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward -= 0.002f;
                    }
                    // Si hay algún enemigo y todos ellos suman igual o menos que la vida de Rad, y mi power stone de este camino no está conquistada (hay que defenderla), recompenso porque al poner Kaapo los mato
                    if (Util.gc.pathHasNertaPowerStoneAlive(0) && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(1) > 0 && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(1) <= Util.KAAPO_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }

                    Util.gc.nertaSelectKaapo();
                    useNertaGap(0);
                }
                break;
            case 9:
                // 9 - Kaapo Path 2
                if (Util.nertaAvailableActions.Contains(Util.Action.Kaapo))
                {
                    // Si este path tiene power stone del enemigo aún viva, recompensar más
                    if (Util.gc.pathHasIttlaPowerStoneAlive(1))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 1.");
                        reward += 0.1f;
                    }
                    // Si este path tiene power stone del enemigo conquistada y no hay enemigos en el path, penalizar
                    if (!Util.gc.pathHasIttlaPowerStoneAlive(1) && Util.gc.isPathEmptyOfIttlaTotems(2))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward -= 0.002f;
                    }
                    // Si hay algún enemigo y todos ellos suman igual o menos que la vida de Rad, y mi power stone de este camino no está conquistada (hay que defenderla), recompenso porque al poner Kaapo los mato
                    if (Util.gc.pathHasNertaPowerStoneAlive(1) && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(2) > 0 && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(2) <= Util.KAAPO_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }

                    Util.gc.nertaSelectKaapo();
                    useNertaGap(1); 
                }
                break;
            case 10:
                // 10 - Kaapo Path 3
                if (Util.nertaAvailableActions.Contains(Util.Action.Kaapo))
                {
                    // Si este path tiene power stone del enemigo aún viva, recompensar más
                    if (Util.gc.pathHasIttlaPowerStoneAlive(2))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 2.");
                        reward += 0.1f;
                    }
                    // Si este path tiene power stone del enemigo conquistada y no hay enemigos en el path, penalizar
                    if (!Util.gc.pathHasIttlaPowerStoneAlive(2) && Util.gc.isPathEmptyOfIttlaTotems(3))
                    {
                        //Debug.Log("Enemigo powerstone viva en path 0.");
                        reward -= 0.002f;
                    }
                    // Si hay algún enemigo y todos ellos suman igual o menos que la vida de Rad, y mi power stone de este camino no está conquistada (hay que defenderla), recompenso porque al poner Kaapo los mato
                    if (Util.gc.pathHasNertaPowerStoneAlive(2) && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(3) > 0 && Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(3) <= Util.KAAPO_INITIAL_LIFE)
                    {
                        reward += 0.05f;
                    }

                    Util.gc.nertaSelectKaapo();
                    useNertaGap(2);
                }
                break;
            case 11:
                // 11 - Kaapo Path Shrine
                if (Util.nertaAvailableActions.Contains(Util.Action.Kaapo))
                {
                    Util.gc.nertaSelectKaapo();
                    useNertaGap(3);
                }
                break;
            case 12:
                // 12 - Ice
                if (Util.nertaAvailableActions.Contains(Util.Action.IceForce))
                {
                    Util.gc.nertaSelectIceForce();
                    if (Util.gc.ittlaOnBoardTotems.Count > 0) // If ice is used and there are enemy totems it will affect to
                    {
                        //Debug.Log("Uso hielo y hay enemigos");
                        reward = 0.1f;
                    }
                }
                break;
            case 13:
                // 13 - Wind
                if (Util.nertaAvailableActions.Contains(Util.Action.WindForce))
                {
                    Util.gc.nertaSelectWindForce();
                    if (Util.gc.nertaOnBoardTotems.Count > 0) // If wind is used and there are totems it will affect to
                    {
                        //Debug.Log("Uso viento y tengo totems");
                        reward = 0.1f;
                    }
                }
                break;
            case 14:
                // 14 - Sun
                if (Util.nertaAvailableActions.Contains(Util.Action.SunForce))
                {
                    //Debug.Log(Util.gc.nertaVasuCharge.GetComponent<CharacterCharge>().getCharge());
                    //Debug.Log(Util.gc.nertaKaapoCharge.GetComponent<CharacterCharge>().getCharge());
                    //Debug.Log(Util.gc.nertaRadCharge.GetComponent<CharacterCharge>().getCharge());
                    if ((Util.gc.nertaVasuCharge.GetComponent<CharacterCharge>().getCharge() < 1f ||
                     Util.gc.nertaKaapoCharge.GetComponent<CharacterCharge>().getCharge() < 1f ||
                     Util.gc.nertaRadCharge.GetComponent<CharacterCharge>().getCharge() < 1f)) // If at least one totem has less that 100% charge
                    {
                        //Debug.Log("Uso sol y no tenia carga en algun totem");
                        reward = 0.05f;
                    }
                    Util.gc.nertaSelectSunForce();
                }
                break;
            case 15:
                // 15 - Faith Path 1
                if (Util.nertaAvailableActions.Contains(Util.Action.FaithForce))
                {
                    Util.gc.nertaSelectFaithForce();
                    if (!Util.gc.isPathEmptyOfIttlaTotems(1))
                    {
                        //Debug.Log("Uso fe y habia enemigos en path 0");
                        reward = 0.2f;
                    }
                    useNertaGap(0);
                }
                break;
            case 16:
                // 16 - Faith Path 2
                if (Util.nertaAvailableActions.Contains(Util.Action.FaithForce))
                {
                    Util.gc.nertaSelectFaithForce();
                    if (!Util.gc.isPathEmptyOfIttlaTotems(2))
                    {
                        //Debug.Log("Uso fe y habia enemigos en path 1");
                        reward = 0.2f;
                    }
                    useNertaGap(1);
                }
                break;
            case 17:
                // 17 - Faith Path 3
                if (Util.nertaAvailableActions.Contains(Util.Action.FaithForce))
                {
                    Util.gc.nertaSelectFaithForce();
                    if (!Util.gc.isPathEmptyOfIttlaTotems(3))
                    {
                        //Debug.Log("Uso fe y habia enemigos en path 2");
                        reward = 0.2f;
                    }
                    useNertaGap(2);
                }
                break;
            case 18:
                // 17 - Faith Path Shrine
                if (Util.nertaAvailableActions.Contains(Util.Action.FaithForce))
                {
                    Util.gc.nertaSelectFaithForce();
                    if (!Util.gc.isPathEmptyOfIttlaTotems(0))
                    {
                        //Debug.Log("Uso fe y habia enemigos en path shrine");
                        reward = 0.2f;
                    }
                    useNertaGap(3);
                }
                break;
            default:
                break;
        }

        return reward;
    }

    private void doNertaActionWithRandomGapChoice(Util.Action action)
    {
        if (action == Util.Action.Vasu)
        {
            Util.gc.nertaSelectVasu();
            StartCoroutine("useRandomNertaGap");
        }
        else if (action == Util.Action.Kaapo)
        {
            Util.gc.nertaSelectKaapo();
            StartCoroutine("useRandomNertaGap");
        }
        else if (action == Util.Action.Rad)
        {
            Util.gc.nertaSelectRad();
            StartCoroutine("useRandomNertaGap");
        }
        else if (action == Util.Action.IceForce)
        {
            Util.gc.nertaSelectIceForce();
        }
        else if (action == Util.Action.WindForce)
        {
            Util.gc.nertaSelectWindForce();
        }
        else if (action == Util.Action.SunForce)
        {
            Util.gc.nertaSelectSunForce();

        }
        else if (action == Util.Action.FaithForce)
        {
            Util.gc.nertaSelectFaithForce();
            StartCoroutine("useRandomNertaGap");

        }
    }

    IEnumerator useRandomIttlaGap()
    {
        yield return new WaitForSeconds(Random.Range(Util.NPC_MIN_WAITING_BETWEEN_ACTIONS, Util.NPC_MAX_WAITING_BETWEEN_ACTIONS));
        int randomNumber = Random.Range(0, 100);
        if (randomNumber <= Util.NPC_PROBABILITY_OF_USING_SHRINE_GAP){
            // Select Shrine Gap (It should always be the last gap in HUD.IttlaGaps)
            Util.gc.IttlaGaps[Util.gc.IttlaGaps.Length - 1].GetComponent<Gap>().doGapAction();
        } else {
            // Select Random Gap (including the shrine)
            Util.gc.IttlaGaps[Random.Range(0, Util.gc.IttlaGaps.Length - 1)].GetComponent<Gap>().doGapAction();
         }
    }

    IEnumerator useRandomNertaGap()
    {
        yield return new WaitForSeconds(Random.Range(Util.NPC_MIN_WAITING_BETWEEN_ACTIONS, Util.NPC_MAX_WAITING_BETWEEN_ACTIONS));
        int randomNumber = Random.Range(0, 100);
        if (randomNumber <= Util.NPC_PROBABILITY_OF_USING_SHRINE_GAP) {
            // Select Shrine Gap (It should always be the last gap in HUD.NertaGaps)
            Util.gc.NertaGaps[Util.gc.NertaGaps.Length - 1].GetComponent<Gap>().doGapAction();
        } else {
            // Select Random Gap (including the shrine) 
            Util.gc.NertaGaps[Random.Range(0, Util.gc.NertaGaps.Length - 1)].GetComponent<Gap>().doGapAction();
        }
    }

    private void useIttlaGap(int index)
    {
        Util.gc.IttlaGaps[index].GetComponent<Gap>().doGapAction();
    }
    private void useNertaGap(int index)
    {
        Util.gc.NertaGaps[index].GetComponent<Gap>().doGapAction();
    }
	
}
