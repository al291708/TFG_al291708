using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TAJAgent : Agent {

    int myLife;
    int enemyLife;
    int mySpirits;
    int resets;
    public Text textinfo;
    private int experiences;
    Text observationsInfoText;

    public override void InitializeAgent()
    {
        base.InitializeAgent();
        if (GameObject.FindGameObjectWithTag("OBSERVACIONES") != null)
        {
            observationsInfoText = GameObject.FindGameObjectWithTag("OBSERVACIONES").GetComponent<Text>();
        }
        resets = 0;

        experiences = 0;
        if (GetComponent<NPC>().faction == Util.Factions.Ittla)
        {
            myLife = Util.ittlaLife;
            enemyLife = Util.nertaLife;
        }
        else if (GetComponent<NPC>().faction == Util.Factions.Nerta)
        {
            myLife = Util.nertaLife;
            enemyLife = Util.ittlaLife;
        }
    }

    public override void CollectObservations()
    {
        if (Util.gc != null)
        {
            base.CollectObservations();
            if (GetComponent<NPC>().faction == Util.Factions.Ittla)
            {
                AddVectorObs(Util.ittlaLife); // My life
                AddVectorObs(Util.nertaLife); // Enemy's life

                AddVectorObs((int)Convert.ToInt16(Util.gc.IttlaPowerStones[0].GetComponent<PowerStone>().conquered)); // My Power Stone 1 is conquered
                AddVectorObs((int)Convert.ToInt16(Util.gc.IttlaPowerStones[1].GetComponent<PowerStone>().conquered)); // My Power Stone 2 is conquered
                AddVectorObs((int)Convert.ToInt16(Util.gc.IttlaPowerStones[2].GetComponent<PowerStone>().conquered)); // My Power Stone 3 is conquered
                AddVectorObs((int)Convert.ToInt16(Util.gc.NertaPowerStones[0].GetComponent<PowerStone>().conquered)); // Enemy Power Stone 1 is conquered
                AddVectorObs((int)Convert.ToInt16(Util.gc.NertaPowerStones[1].GetComponent<PowerStone>().conquered)); // Enemy Power Stone 2 is conquered
                AddVectorObs((int)Convert.ToInt16(Util.gc.NertaPowerStones[2].GetComponent<PowerStone>().conquered)); // Enemy Power Stone 3 is conquered
                AddVectorObs(Util.ittlaSpirits); // My spirits
                AddVectorObs(Util.nertaSpirits); // Enemy's spirits
                AddVectorObs(Util.gc.getTotalLifeOfIttlaTotemsInPath(1)); // My totems total life in path 0
                AddVectorObs(Util.gc.getTotalLifeOfIttlaTotemsInPath(2)); // My totems total life in path 1
                AddVectorObs(Util.gc.getTotalLifeOfIttlaTotemsInPath(3)); // My totems total life in path 2
                AddVectorObs(Util.gc.getTotalLifeOfIttlaTotemsInPath(0)); // My totems total life in shrine path
                AddVectorObs(Util.gc.getTotalLifeOfNertaTotemsInPath(1)); // Enemy's totems total life in path 0
                AddVectorObs(Util.gc.getTotalLifeOfNertaTotemsInPath(2)); // Enemy's totems total life in path 1
                AddVectorObs(Util.gc.getTotalLifeOfNertaTotemsInPath(3)); // Enemy's totems total life in path 2
                AddVectorObs(Util.gc.getTotalLifeOfNertaTotemsInPath(0)); // Enemy's totems total life in shrine path
                
                AddVectorObs(Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(1)); // Progressed percentage of closest totem to end of path 0
                AddVectorObs(Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(2)); // Progressed percentage of closest totem to end of path 1
                AddVectorObs(Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(3)); // Progressed percentage of closest totem to end of path 2
                AddVectorObs(Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(0)); // Progressed percentage of closest totem to end of shrine path
                AddVectorObs(Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(1)); // Progressed percentage of closest enemy totem to end of path 0
                AddVectorObs(Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(2)); // Progressed percentage of closest enemy totem to end of path 1
                AddVectorObs(Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(3)); // Progressed percentage of closest enemy totem to end of path 2
                AddVectorObs(Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(0)); // Progressed percentage of closest enemy totem to end of shrine path
                
                AddVectorObs((int)Convert.ToInt16(Util.gc.ittlaVasuCharge.GetComponent<CharacterCharge>().isCharged())); // My Vasu is Charged
                AddVectorObs((int)Convert.ToInt16(Util.gc.ittlaKaapoCharge.GetComponent<CharacterCharge>().isCharged())); // My Kaapo is Charged
                AddVectorObs((int)Convert.ToInt16(Util.gc.ittlaRadCharge.GetComponent<CharacterCharge>().isCharged())); // My Rad is Charged
                AddVectorObs((int)Convert.ToInt16(Util.gc.nertaVasuCharge.GetComponent<CharacterCharge>().isCharged())); // Enemy's Vasu is Charged
                AddVectorObs((int)Convert.ToInt16(Util.gc.nertaKaapoCharge.GetComponent<CharacterCharge>().isCharged())); // Enemy's Kaapo is Charged
                AddVectorObs((int)Convert.ToInt16(Util.gc.nertaRadCharge.GetComponent<CharacterCharge>().isCharged())); // Enemy's Rad is Charged                
                AddVectorObs((int)Convert.ToInt16(Util.gc.ittlaIceForceButton.GetComponent<ForceButtons>().isActive)); // My Ice Force can be used
                AddVectorObs((int)Convert.ToInt16(Util.gc.ittlaWindForceButton.GetComponent<ForceButtons>().isActive)); // My Wind Force can be used
                AddVectorObs((int)Convert.ToInt16(Util.gc.ittlaSunForceButton.GetComponent<ForceButtons>().isActive)); // My Sun Force can be used
                AddVectorObs((int)Convert.ToInt16(Util.gc.ittlaFaithForceButton.GetComponent<ForceButtons>().isActive)); // My Faith Force can be used
                AddVectorObs((int)Convert.ToInt16(Util.gc.nertaIceForceButton.GetComponent<ForceButtons>().isActive)); // Enemy's Ice Force can be used
                AddVectorObs((int)Convert.ToInt16(Util.gc.nertaWindForceButton.GetComponent<ForceButtons>().isActive)); // Enemy's Wind Force can be used
                AddVectorObs((int)Convert.ToInt16(Util.gc.nertaSunForceButton.GetComponent<ForceButtons>().isActive)); // Enemy's Sun Force can be used
                AddVectorObs((int)Convert.ToInt16(Util.gc.nertaFaithForceButton.GetComponent<ForceButtons>().isActive)); // Enemy's Faith Force can be used
                
                AddVectorObs((int)Convert.ToInt16(Util.gc.ittlaIceForceButton.GetComponent<ForceButtons>().isInUse())); // My Ice Force is in use
                AddVectorObs((int)Convert.ToInt16(Util.gc.ittlaWindForceButton.GetComponent<ForceButtons>().isInUse())); // My Wind Force is in use
                AddVectorObs((int)Convert.ToInt16(Util.gc.nertaIceForceButton.GetComponent<ForceButtons>().isInUse())); // Enemy's Ice Force is in use
                AddVectorObs((int)Convert.ToInt16(Util.gc.nertaWindForceButton.GetComponent<ForceButtons>().isInUse())); // Enemy's Wind Force is in use
                
                SetTextObs("Testing " + gameObject.GetInstanceID());
            }
            else if (GetComponent<NPC>().faction == Util.Factions.Nerta)
            {
                AddVectorObs(Util.nertaLife); // My life
                AddVectorObs(Util.ittlaLife); // Enemy's life

                AddVectorObs((int)Convert.ToInt16(Util.gc.NertaPowerStones[0].GetComponent<PowerStone>().conquered)); // My Power Stone 1 is conquered
                AddVectorObs((int)Convert.ToInt16(Util.gc.NertaPowerStones[1].GetComponent<PowerStone>().conquered)); // My Power Stone 2 is conquered
                AddVectorObs((int)Convert.ToInt16(Util.gc.NertaPowerStones[2].GetComponent<PowerStone>().conquered)); // My Power Stone 3 is conquered
                AddVectorObs((int)Convert.ToInt16(Util.gc.IttlaPowerStones[0].GetComponent<PowerStone>().conquered)); // Enemy Power Stone 1 is conquered
                AddVectorObs((int)Convert.ToInt16(Util.gc.IttlaPowerStones[1].GetComponent<PowerStone>().conquered)); // Enemy Power Stone 2 is conquered
                AddVectorObs((int)Convert.ToInt16(Util.gc.IttlaPowerStones[2].GetComponent<PowerStone>().conquered)); // Enemy Power Stone 3 is conquered
                AddVectorObs(Util.nertaSpirits); // My spirits
                AddVectorObs(Util.ittlaSpirits); // Enemy's spirits
                AddVectorObs(Util.gc.getTotalLifeOfNertaTotemsInPath(1)); // My totems total life in path 0
                AddVectorObs(Util.gc.getTotalLifeOfNertaTotemsInPath(2)); // My totems total life in path 1
                AddVectorObs(Util.gc.getTotalLifeOfNertaTotemsInPath(3)); // My totems total life in path 2
                AddVectorObs(Util.gc.getTotalLifeOfNertaTotemsInPath(0)); // My totems total life in shrine path
                AddVectorObs(Util.gc.getTotalLifeOfIttlaTotemsInPath(1)); // Enemy's totems total life in path 0
                AddVectorObs(Util.gc.getTotalLifeOfIttlaTotemsInPath(2)); // Enemy's totems total life in path 1
                AddVectorObs(Util.gc.getTotalLifeOfIttlaTotemsInPath(3)); // Enemy's totems total life in path 2
                AddVectorObs(Util.gc.getTotalLifeOfIttlaTotemsInPath(0)); // Enemy's totems total life in shrine path

                AddVectorObs(Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(1)); // Progressed percentage of closest totem to end of path 0
                AddVectorObs(Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(2)); // Progressed percentage of closest totem to end of path 1
                AddVectorObs(Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(3)); // Progressed percentage of closest totem to end of path 2
                AddVectorObs(Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(0)); // Progressed percentage of closest totem to end of shrine path
                AddVectorObs(Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(1)); // Progressed percentage of closest enemy totem to end of path 0
                AddVectorObs(Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(2)); // Progressed percentage of closest enemy totem to end of path 1
                AddVectorObs(Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(3)); // Progressed percentage of closest enemy totem to end of path 2
                AddVectorObs(Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(0)); // Progressed percentage of closest enemy totem to end of shrine path
                AddVectorObs((int)Convert.ToInt16(Util.gc.nertaVasuCharge.GetComponent<CharacterCharge>().isCharged())); // My Vasu is Charged
                AddVectorObs((int)Convert.ToInt16(Util.gc.nertaKaapoCharge.GetComponent<CharacterCharge>().isCharged())); // My Kaapo is Charged
                AddVectorObs((int)Convert.ToInt16(Util.gc.nertaRadCharge.GetComponent<CharacterCharge>().isCharged())); // My Rad is Charged
                AddVectorObs((int)Convert.ToInt16(Util.gc.ittlaVasuCharge.GetComponent<CharacterCharge>().isCharged())); // Enemy's Vasu Charge
                AddVectorObs((int)Convert.ToInt16(Util.gc.ittlaKaapoCharge.GetComponent<CharacterCharge>().isCharged())); // Enemy's Kaapo is Charged
                AddVectorObs((int)Convert.ToInt16(Util.gc.ittlaRadCharge.GetComponent<CharacterCharge>().isCharged())); // Enemy's Rad is Charged
                AddVectorObs((int)Convert.ToInt16(Util.gc.nertaIceForceButton.GetComponent<ForceButtons>().isActive)); // My Ice Force can be used
                AddVectorObs((int)Convert.ToInt16(Util.gc.nertaWindForceButton.GetComponent<ForceButtons>().isActive)); // My Wind Force can be used
                AddVectorObs((int)Convert.ToInt16(Util.gc.nertaSunForceButton.GetComponent<ForceButtons>().isActive)); // My Sun Force can be used
                AddVectorObs((int)Convert.ToInt16(Util.gc.nertaFaithForceButton.GetComponent<ForceButtons>().isActive)); // My Faith Force can be used
                AddVectorObs((int)Convert.ToInt16(Util.gc.ittlaIceForceButton.GetComponent<ForceButtons>().isActive)); // Enemy's Ice Force can be used
                AddVectorObs((int)Convert.ToInt16(Util.gc.ittlaWindForceButton.GetComponent<ForceButtons>().isActive)); // Enemy's Wind Force can be used
                AddVectorObs((int)Convert.ToInt16(Util.gc.ittlaSunForceButton.GetComponent<ForceButtons>().isActive)); // Enemy's Sun Force can be used
                AddVectorObs((int)Convert.ToInt16(Util.gc.ittlaFaithForceButton.GetComponent<ForceButtons>().isActive)); // Enemy's Faith Force can be used

                AddVectorObs((int)Convert.ToInt16(Util.gc.nertaIceForceButton.GetComponent<ForceButtons>().isInUse())); // My Ice Force is in use
                AddVectorObs((int)Convert.ToInt16(Util.gc.nertaWindForceButton.GetComponent<ForceButtons>().isInUse())); // My Wind Force is in use
                AddVectorObs((int)Convert.ToInt16(Util.gc.ittlaIceForceButton.GetComponent<ForceButtons>().isInUse())); // Enemy's Ice Force is in use
                AddVectorObs((int)Convert.ToInt16(Util.gc.ittlaWindForceButton.GetComponent<ForceButtons>().isInUse())); // Enemy's Wind Force is in use
                SetTextObs("Testing " + gameObject.GetInstanceID());

                string observaciones = "Mi vida - " + Util.nertaLife + "\n";
                observaciones += "Vida enemigo - " + Util.ittlaLife + "\n";
                observaciones += "Mi powerstone 1 conquistada - " + (int)Convert.ToInt16(Util.gc.NertaPowerStones[0].GetComponent<PowerStone>().conquered) + "\n";
                observaciones += "Mi powerstone 2 conquistada - " + (int)Convert.ToInt16(Util.gc.NertaPowerStones[1].GetComponent<PowerStone>().conquered) + "\n";
                observaciones += "Mi powerstone 3 conquistada - " + (int)Convert.ToInt16(Util.gc.NertaPowerStones[2].GetComponent<PowerStone>().conquered) + "\n";
                observaciones += "Powerstone 1 enemigo conquistada - " + (int)Convert.ToInt16(Util.gc.IttlaPowerStones[0].GetComponent<PowerStone>().conquered) + "\n";
                observaciones += "Powerstone 2 enemigo conquistada - " + (int)Convert.ToInt16(Util.gc.IttlaPowerStones[0].GetComponent<PowerStone>().conquered) + "\n";
                observaciones += "Powerstone 3 enemigo conquistada - " + (int)Convert.ToInt16(Util.gc.IttlaPowerStones[0].GetComponent<PowerStone>().conquered) + "\n";
                observaciones += "Mis spirits - " + Util.nertaSpirits + "\n";
                observaciones += "Spirits enemigo - " + Util.ittlaSpirits + "\n";
                observaciones += "Mi vida en path 1 - " + Util.gc.getTotalLifeOfNertaTotemsInPath(1) + "\n";
                observaciones += "Mi vida en path 2 - " + Util.gc.getTotalLifeOfNertaTotemsInPath(2) + "\n";
                observaciones += "Mi vida en path 3 - " + Util.gc.getTotalLifeOfNertaTotemsInPath(3) + "\n";
                observaciones += "Mi vida en path shrine - " + Util.gc.getTotalLifeOfNertaTotemsInPath(0) + "\n";
                observaciones += "Vida enemigo en path 1 - " + Util.gc.getTotalLifeOfIttlaTotemsInPath(1) + "\n";
                observaciones += "Vida enemigo en path 2 - " + Util.gc.getTotalLifeOfIttlaTotemsInPath(2) + "\n";
                observaciones += "Vida enemigo en path 3 - " + Util.gc.getTotalLifeOfIttlaTotemsInPath(3) + "\n";
                observaciones += "Vida enemigo en path shrine - " + Util.gc.getTotalLifeOfIttlaTotemsInPath(0) + "\n";
                observaciones += "Mis totems cerca de fin path 1 - " + Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(1) + "\n";
                observaciones += "Mis totems cerca de fin path 2 - " + Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(2) + "\n";
                observaciones += "Mis totems cerca de fin path 3 - " + Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(3) + "\n";
                observaciones += "Mis totems cerca de fin path shrine - " + Util.gc.getLifeOfAllNertaTotemsNearToEndOfPath(0) + "\n";
                observaciones += "Totems enemigos cerca de fin path 1 - " + Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(1) + "\n";
                observaciones += "Totems enemigos cerca de fin path 2 - " + Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(2) + "\n";
                observaciones += "Totems enemigos cerca de fin path 3 - " + Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(3) + "\n";
                observaciones += "Totems enemigos cerca de fin path shrine - " + Util.gc.getLifeOfAllIttlaTotemsNearToEndOfPath(0) + "\n";
                observaciones += "Mi Vasu cargado - " + (int)Convert.ToInt16(Util.gc.nertaVasuCharge.GetComponent<CharacterCharge>().isCharged()) + "\n";
                observaciones += "Mi Kaapo cargado - " + (int)Convert.ToInt16(Util.gc.nertaKaapoCharge.GetComponent<CharacterCharge>().isCharged()) + "\n";
                observaciones += "Mi Rad cargado - " + (int)Convert.ToInt16(Util.gc.nertaRadCharge.GetComponent<CharacterCharge>().isCharged()) + "\n";
                observaciones += "Vasu enemigo cargado - " + (int)Convert.ToInt16(Util.gc.ittlaVasuCharge.GetComponent<CharacterCharge>().isCharged()) + "\n";
                observaciones += "Kaapo enemigo cargado - " + (int)Convert.ToInt16(Util.gc.ittlaKaapoCharge.GetComponent<CharacterCharge>().isCharged()) + "\n";
                observaciones += "Rad enemigo cargado - " + (int)Convert.ToInt16(Util.gc.ittlaRadCharge.GetComponent<CharacterCharge>().isCharged()) + "\n";
                observaciones += "Mi Ice se puede usar - " + (int)Convert.ToInt16(Util.gc.nertaIceForceButton.GetComponent<ForceButtons>().isActive) + "\n";
                observaciones += "Mi Wind se puede usar - " + (int)Convert.ToInt16(Util.gc.nertaWindForceButton.GetComponent<ForceButtons>().isActive) + "\n";
                observaciones += "Mi Sun se puede usar - " + (int)Convert.ToInt16(Util.gc.nertaSunForceButton.GetComponent<ForceButtons>().isActive) + "\n";
                observaciones += "Mi Faith se puede usar - " + (int)Convert.ToInt16(Util.gc.nertaFaithForceButton.GetComponent<ForceButtons>().isActive) + "\n";
                observaciones += "Ice enemigo lo puede usar - " + (int)Convert.ToInt16(Util.gc.ittlaIceForceButton.GetComponent<ForceButtons>().isActive) + "\n";
                observaciones += "Wind enemigo lo puede usar - " + (int)Convert.ToInt16(Util.gc.ittlaWindForceButton.GetComponent<ForceButtons>().isActive) + "\n";
                observaciones += "Sun enemigo lo puede usar - " + (int)Convert.ToInt16(Util.gc.ittlaSunForceButton.GetComponent<ForceButtons>().isActive) + "\n";
                observaciones += "Faith enemigo lo puede usar - " + (int)Convert.ToInt16(Util.gc.ittlaFaithForceButton.GetComponent<ForceButtons>().isActive) + "\n";
                observaciones += "Mi Ice se está usando - " + (int)Convert.ToInt16(Util.gc.nertaIceForceButton.GetComponent<ForceButtons>().isInUse()) + "\n";
                observaciones += "Mi Wind se está usando - " + (int)Convert.ToInt16(Util.gc.nertaWindForceButton.GetComponent<ForceButtons>().isInUse()) + "\n";
                observaciones += "Ice enemigo lo está usando - " + (int)Convert.ToInt16(Util.gc.ittlaIceForceButton.GetComponent<ForceButtons>().isInUse()) + "\n";
                observaciones += "Wind enemigo lo está usando - " + (int)Convert.ToInt16(Util.gc.ittlaWindForceButton.GetComponent<ForceButtons>().isInUse()) + "\n";
                if (observationsInfoText != null)
                {
                    observationsInfoText.text = observaciones;
                }
            }

        }
        
    }
    
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        experiences += 1;
        int discreteAction = (int)Mathf.Clamp(vectorAction[0], 0, 17);
        textinfo.text = "discrete action: " + discreteAction;
        //textinfo.text = "decisiones: " + experiences;
        
        if (GetComponent<NPC>().faction == Util.Factions.Ittla)
        {
            float reward = Util.ittlaNPC.doIttlaActionDiscrete(discreteAction);
            SetReward(reward);

            if (myLife > Util.ittlaLife) // If I lose life (power stone)
            {
                myLife = Util.ittlaLife;
                //Debug.Log("Pierdo power stone");
                SetReward(-0.05f);
            }

            if (enemyLife > Util.nertaLife) // If enemy loses life (power stone)
            {
                enemyLife = Util.nertaLife;
                //Debug.Log("Conquisto power stone");
                SetReward(+0.3f);
            }

            if (Util.ittlaLife <= 0)
            {
                // If I die
                //Debug.Log("Pierdo");
                SetReward(-0.2f);
                Done();
            }
            else if (Util.nertaLife <= 0)
            {
                // If I win
                //Debug.Log("Gano");
                SetReward(1f);
                Done();
            }
        }
        else if (GetComponent<NPC>().faction == Util.Factions.Nerta)
        {
            float reward = Util.nertaNPC.doNertaActionDiscrete(discreteAction);
            SetReward(reward);

            if (myLife > Util.nertaLife) // If I lose life (power stone)
            {
                myLife = Util.nertaLife;
                //Debug.Log("Pierdo power stone");
                SetReward(-0.05f);
            }

            if (enemyLife > Util.ittlaLife) // If enemy loses life (power stone)
            {
                enemyLife = Util.ittlaLife;
                //Debug.Log("Conquisto power stone");
                SetReward(+0.3f);
            }

            if (Util.nertaLife <= 0)
            {
                // If I die
                //Debug.Log("Pierdo");
                SetReward(-0.2f);
                Done();
            }
            else if (Util.ittlaLife <= 0)
            {
                // If I win
                //Debug.Log("Gano");
                SetReward(1f);
                Done();
            }
        }

    }


    public override void AgentReset()
    {
        Debug.Log("RESETEO AGENTE");
        if (resets != 0) // If it not the first time reset is called (just after initialize agent at instantiating it)
        {
            if (GetComponent<NPC>().faction == Util.Factions.Ittla)
            {
                myLife = Util.ittlaLife;
                enemyLife = Util.nertaLife;
            }
            else
            {
                myLife = Util.nertaLife;
                enemyLife = Util.ittlaLife;
            }
            Util.gc.stop();
            //Util.gc.start();
        }
        resets++;
    }


}
