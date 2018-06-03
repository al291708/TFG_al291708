using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject[] IttlaGaps;
    public GameObject[] NertaGaps;

    public GameObject vasu_GO;
    public GameObject kaapo_GO;
    public GameObject rad_GO;

    public GameObject[] IttlaPowerStones;
    public GameObject[] NertaPowerStones;

    public GameObject ittlaButtons;
    public GameObject nertaButtons;

    public GameObject ittlaVasuCharge;
    public GameObject ittlaKaapoCharge;
    public GameObject ittlaRadCharge;
    public GameObject ittlaIceForceButton;
    public GameObject ittlaWindForceButton;
    public GameObject ittlaSunForceButton;
    public GameObject ittlaFaithForceButton;

    public GameObject nertaVasuCharge;
    public GameObject nertaKaapoCharge;
    public GameObject nertaRadCharge;
    public GameObject nertaIceForceButton;
    public GameObject nertaWindForceButton;
    public GameObject nertaSunForceButton;
    public GameObject nertaFaithForceButton;
    public GameObject ittlaNPC;
    public GameObject nertaNPC;

    
    public Text ittlaSpiritsText;
    public Text nertaSpiritsText;

    public Text gamesWonText;

    public Material ittlaMaterial;
    public Material nertaMaterial;

    public GameObject gameOverScreen;
    public GameObject gameOverScreenIttlaWinner;
    public GameObject gameOverScreenNertaWinner;

    public AudioSource audiosourceICE;
    public AudioSource audiosourceWIND;
    public AudioSource audiosourceSUN;
    public AudioSource audiosourceFAITH;
    public AudioSource audiosourceINSERTTOTEM;
    public AudioSource audiosourcePOWERSTONE;
    public AudioSource audiosourceSPIRIT;
    public AudioSource audiosourceWATER;

    public GameObject iceParticlesIttla;
    public GameObject iceParticlesNerta;
    public GameObject windParticlesIttla;
    public GameObject windParticlesNerta;
    public GameObject sunParticlesIttla;
    public GameObject sunParticlesNerta;
    public GameObject faithParticlesPath1;
    public GameObject faithParticlesPath2;
    public GameObject faithParticlesPath3;
    public GameObject faithParticlesPathIttlaShrine;
    public GameObject faithParticlesPathNertaShrine;

    public GameObject ittlaNPCRandom;
    public GameObject ittlaNPCMedium;
    public GameObject ittlaNPCHard;
    public GameObject nertaNPCRandom;
    public GameObject nertaNPCMedium;
    public GameObject nertaNPCHard;

    public GameObject trainingInfo;


    [SerializeField]
    public List<GameObject> ittlaOnBoardTotems;
    [SerializeField]
    public List<GameObject> nertaOnBoardTotems;

    void Start(){
        if (PlayerSelection.playerFactionChosen == PlayerSelection.PlayerSelections.Ittla)
        {
            nertaNPC.SetActive(true);
        }
        else if (PlayerSelection.playerFactionChosen == PlayerSelection.PlayerSelections.Nerta)
        {
            ittlaNPC.SetActive(false);
        }


        Time.timeScale = Util.TIME_SCALE;

        Util.gc = this;

        assignPlayersToGaps();
        ittlaSpiritsText.text = Util.ittlaSpirits.ToString();
        nertaSpiritsText.text = Util.nertaSpirits.ToString();
        checkIttlaButtonsStatus();
        checkNertaButtonsStatus();
        assignPowerStoneIndexes();


        if (Util.WRITE_GAMES_IN_FILE)
        {
            Util.writeToFile("partidas.txt", "---");
        }

        if (Util.IS_TRAINING == true)
        {
            disableRenderers();
            ittlaNPCRandom.SetActive(true);
            nertaNPCHard.SetActive(true);
            ittlaButtons.SetActive(true);
            nertaButtons.SetActive(true);
            Util.gameOver = false;
            trainingInfo.SetActive(true);

            startGame();
        }
        else
        {
            audiosourceWATER.Play();
            if (PlayerSelection.playerFactionChosen == PlayerSelection.PlayerSelections.Ittla)
            {
                switch (PlayerSelection.playerDifficultyChosen)
                {
                    case PlayerSelection.Difficulty.Easy:
                        nertaNPCRandom.SetActive(true);
                        break;
                    case PlayerSelection.Difficulty.Medium:
                        nertaNPCMedium.SetActive(true);
                        break;
                    case PlayerSelection.Difficulty.Hard:
                        nertaNPCHard.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
            else if (PlayerSelection.playerFactionChosen == PlayerSelection.PlayerSelections.Nerta)
            {
                switch (PlayerSelection.playerDifficultyChosen)
                {
                    case PlayerSelection.Difficulty.Easy:
                        ittlaNPCRandom.SetActive(true);
                        break;
                    case PlayerSelection.Difficulty.Medium:
                        ittlaNPCMedium.SetActive(true);
                        break;
                    case PlayerSelection.Difficulty.Hard:
                        ittlaNPCHard.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
        }

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


    private void disableRenderers()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            MeshRenderer mr = go.GetComponent<MeshRenderer>();
            if (mr != null)
            {
                mr.enabled = false;
            } 
        }
    }

    public void assignPlayersToGaps()
    {
        foreach (GameObject IttlaGap in IttlaGaps)
        {
            IttlaGap.GetComponent<Gap>().setFaction(Util.Factions.Ittla);
        }
        foreach (GameObject NertaGap in NertaGaps)
        {
            NertaGap.GetComponent<Gap>().setFaction(Util.Factions.Nerta);
        }
    }

    public void assignPowerStoneIndexes()
    {
        for (int key = 0; key < IttlaPowerStones.Length; key++) {
            IttlaPowerStones[key].GetComponent<PowerStone>().setIndex(key);
        }
        for (int key = 0; key < NertaPowerStones.Length; key++) {
            NertaPowerStones[key].GetComponent<PowerStone>().setIndex(key);
        }
    }

    public void enableIttlaGaps()
    {
        foreach (GameObject IttlaGap in IttlaGaps)
        {
            IttlaGap.GetComponent<Gap>().enable();
        }
    }
    public void enableNertaGaps()
    {
        foreach (GameObject NertaGap in NertaGaps)
        {
            NertaGap.GetComponent<Gap>().enable();
        }
    }

    public void disableIttlaGaps()
    {
        foreach (GameObject IttlaGap in IttlaGaps)
        {
            IttlaGap.GetComponent<Gap>().disable();
        }
    }
    public void disableNertaGaps()
    {
        foreach (GameObject NertaGap in NertaGaps)
        {
            NertaGap.GetComponent<Gap>().disable();
        }
    }

    public void ittlaSelectVasu()
    {
        if (Util.ittlaPlayerSelectedAction == Util.Action.Vasu)
        {
            Util.ittlaPlayerSelectedAction = Util.Action.None;
            disableIttlaGaps();
        }
        else
        {
            Util.ittlaPlayerSelectedAction = Util.Action.Vasu;
            enableIttlaGaps();
        }
    }

    public void ittlaSelectKaapo()
    {
        if (Util.ittlaPlayerSelectedAction == Util.Action.Kaapo)
        {
            Util.ittlaPlayerSelectedAction = Util.Action.None;
            disableIttlaGaps();
        }
        else
        {
            Util.ittlaPlayerSelectedAction = Util.Action.Kaapo;
            enableIttlaGaps();
        }
    }

    public void ittlaSelectRad()
    {
        if (Util.ittlaPlayerSelectedAction == Util.Action.Rad)
        {
            Util.ittlaPlayerSelectedAction = Util.Action.None;
            disableIttlaGaps();
        }
        else
        {
            Util.ittlaPlayerSelectedAction = Util.Action.Rad;
            enableIttlaGaps();
        }
    }

    public void ittlaSelectIceForce()
    {
        Util.ittlaPlayerSelectedAction = Util.Action.IceForce;
        takeOffIttlaSpirits(ittlaIceForceButton.GetComponent<ForceButtons>().cost);
        ittlaIceForceButton.GetComponent<ForceButtons>().use();
        Util.ittlaPlayerSelectedAction = Util.Action.None;
    }

    public void freezeIttlaTotems()
    {
        foreach (GameObject ittlaTotem in ittlaOnBoardTotems)
        {
            ittlaTotem.GetComponent<Totem>().freeze();
        }
    }

    public void unfreezeIttlaTotems()
    {
        foreach (GameObject ittlaTotem in ittlaOnBoardTotems)
        {
            ittlaTotem.GetComponent<Totem>().unfreeze();
        }
    }
    
    public void ittlaSelectWindForce()
    {
        Util.ittlaPlayerSelectedAction = Util.Action.WindForce;
        takeOffIttlaSpirits(ittlaWindForceButton.GetComponent<ForceButtons>().cost);
        ittlaWindForceButton.GetComponent<ForceButtons>().use();
        Util.ittlaPlayerSelectedAction = Util.Action.None;
    }

    public void accelerateIttlaTotems()
    {
        Util.ittlaWindSpeedMultiplier = 2;
        foreach (GameObject ittlaTotem in ittlaOnBoardTotems)
        {
            ittlaTotem.GetComponent<Totem>().updateSpeed();
        }
        checkIttlaButtonsStatus();
    }

    public void decelerateIttlaTotems()
    {
        Util.ittlaWindSpeedMultiplier = 1;
        foreach (GameObject ittlaTotem in ittlaOnBoardTotems)
        {
            ittlaTotem.GetComponent<Totem>().updateSpeed();
        }
        checkIttlaButtonsStatus();
    }

    public void ittlaSelectSunForce()
    {
        Util.ittlaPlayerSelectedAction = Util.Action.SunForce;
        takeOffIttlaSpirits(ittlaSunForceButton.GetComponent<ForceButtons>().cost);

        if (!Util.IS_TRAINING)
        {
            audiosourceSUN.Play();
            Util.gc.sunParticlesIttla.GetComponent<ParticleSystem>().Play(true);
        }

        // Full charge my totems
        ittlaVasuCharge.GetComponent<CharacterCharge>().fulfillCharge();
        ittlaKaapoCharge.GetComponent<CharacterCharge>().fulfillCharge();
        ittlaRadCharge.GetComponent<CharacterCharge>().fulfillCharge();

        Util.ittlaPlayerSelectedAction = Util.Action.None;
    }
    public void ittlaSelectFaithForce()
    {
        if (Util.ittlaPlayerSelectedAction == Util.Action.FaithForce)
        {
            Util.ittlaPlayerSelectedAction = Util.Action.None;
            disableIttlaGaps();
        }
        else
        {
            Util.ittlaPlayerSelectedAction = Util.Action.FaithForce;
            enableIttlaGaps();
        }
    }

    public void nertaSelectVasu()
    {
        if (Util.nertaPlayerSelectedAction == Util.Action.Vasu)
        {
            Util.nertaPlayerSelectedAction = Util.Action.None;
            disableNertaGaps();
        }
        else
        {
            Util.nertaPlayerSelectedAction = Util.Action.Vasu;
            enableNertaGaps();
        }
    }

    public void nertaSelectKaapo()
    {
        if (Util.nertaPlayerSelectedAction == Util.Action.Kaapo)
        {
            Util.nertaPlayerSelectedAction = Util.Action.None;
            disableNertaGaps();
        }
        else
        {
            Util.nertaPlayerSelectedAction = Util.Action.Kaapo;
            enableNertaGaps();
        }
    }

    public void nertaSelectRad()
    {
        if (Util.nertaPlayerSelectedAction == Util.Action.Rad)
        {
            Util.nertaPlayerSelectedAction = Util.Action.None;
            disableNertaGaps();
        }
        else
        {
            Util.nertaPlayerSelectedAction = Util.Action.Rad;
            enableNertaGaps();
        }
    }

    public void nertaSelectIceForce()
    {
        Util.nertaPlayerSelectedAction = Util.Action.IceForce;
        takeOffNertaSpirits(nertaIceForceButton.GetComponent<ForceButtons>().cost);
        nertaIceForceButton.GetComponent<ForceButtons>().use();
        Util.nertaPlayerSelectedAction = Util.Action.None;
    }

    public void freezeNertaTotems()
    {
        foreach (GameObject nertaTotem in nertaOnBoardTotems)
        {
            nertaTotem.GetComponent<Totem>().freeze();
        }
    }

    public void unfreezeNertaTotems()
    {
        foreach (GameObject nertaTotem in nertaOnBoardTotems)
        {
            nertaTotem.GetComponent<Totem>().unfreeze();
        }
    }

    public void nertaSelectWindForce()
    {
        Util.nertaPlayerSelectedAction = Util.Action.WindForce;
        takeOffNertaSpirits(nertaWindForceButton.GetComponent<ForceButtons>().cost);
        nertaWindForceButton.GetComponent<ForceButtons>().use();
        Util.nertaPlayerSelectedAction = Util.Action.None;
    }

    public void accelerateNertaTotems()
    {
        Util.nertaWindSpeedMultiplier = 2;
        foreach (GameObject nertaTotem in nertaOnBoardTotems)
        {
            nertaTotem.GetComponent<Totem>().updateSpeed();
        }
        checkNertaButtonsStatus();
    }

    public void decelerateNertaTotems()
    {
        Util.nertaWindSpeedMultiplier = 1;
        foreach (GameObject nertaTotem in nertaOnBoardTotems)
        {
            nertaTotem.GetComponent<Totem>().updateSpeed();
        }
        checkNertaButtonsStatus();
    }

    public void nertaSelectSunForce()
    {
        Util.nertaPlayerSelectedAction = Util.Action.SunForce;
            
        takeOffNertaSpirits(nertaSunForceButton.GetComponent<ForceButtons>().cost);

        if (!Util.IS_TRAINING)
        {
            audiosourceSUN.Play();
            Util.gc.sunParticlesNerta.GetComponent<ParticleSystem>().Play(true);
        }
        // Full charge my totems
        nertaVasuCharge.GetComponent<CharacterCharge>().fulfillCharge();
        nertaKaapoCharge.GetComponent<CharacterCharge>().fulfillCharge();
        nertaRadCharge.GetComponent<CharacterCharge>().fulfillCharge();


        Util.nertaPlayerSelectedAction = Util.Action.None;
    }
    public void nertaSelectFaithForce()
    {
        if (Util.nertaPlayerSelectedAction == Util.Action.FaithForce)
        {
            Util.nertaPlayerSelectedAction = Util.Action.None;
            disableNertaGaps();
        }
        else
        {
            Util.nertaPlayerSelectedAction = Util.Action.FaithForce;
            enableNertaGaps();
        }
    }

    public GameObject getTotemBySelectedAction(Util.Action selectedAction)
    {
        switch (selectedAction)
        {
            case Util.Action.Vasu:
                return vasu_GO;
            case Util.Action.Kaapo:
                return kaapo_GO;
            case Util.Action.Rad:
                return rad_GO;
            default:
                return null;
        }
    }
    
    public void emptySelectedOptionCharge(Util.Factions faction)
    {
        if (faction == Util.Factions.Ittla)
        {
            switch (Util.ittlaPlayerSelectedAction)
            {
                case Util.Action.Vasu:
                    ittlaVasuCharge.GetComponent<CharacterCharge>().emptyCharge();
                    break;
                case Util.Action.Kaapo:
                    ittlaKaapoCharge.GetComponent<CharacterCharge>().emptyCharge();
                    break;
                case Util.Action.Rad:
                    ittlaRadCharge.GetComponent<CharacterCharge>().emptyCharge();
                    break;
                default:
                    break;
            }
        }
        else if (faction == Util.Factions.Nerta)
        {
            switch (Util.nertaPlayerSelectedAction)
            {
                case Util.Action.Vasu:
                    nertaVasuCharge.GetComponent<CharacterCharge>().emptyCharge();
                    break;
                case Util.Action.Kaapo:
                    nertaKaapoCharge.GetComponent<CharacterCharge>().emptyCharge();
                    break;
                case Util.Action.Rad:
                    nertaRadCharge.GetComponent<CharacterCharge>().emptyCharge();
                    break;
                default:
                    break;
            }
        }
    }

    public void sumUpOneIttlaSpirit()
    {
        Util.ittlaSpirits += 1;
        ittlaSpiritsText.text = Util.ittlaSpirits.ToString();
        checkIttlaButtonsStatus();
    }

    public void sumUpOneNertaSpirit()
    {
        Util.nertaSpirits += 1;
        nertaSpiritsText.text = Util.nertaSpirits.ToString();
        checkNertaButtonsStatus();
    }

    public void takeOffIttlaSpirits(int numberOfSpirits)
    {
        Util.ittlaSpirits -= numberOfSpirits;
        ittlaSpiritsText.text = Util.ittlaSpirits.ToString();
        checkIttlaButtonsStatus();
    }
    
    public void takeOffNertaSpirits(int numberOfSpirits)
    {
        Util.nertaSpirits -= numberOfSpirits;
        nertaSpiritsText.text = Util.nertaSpirits.ToString();
        checkNertaButtonsStatus();
    }

    public void checkIttlaButtonsStatus()
    {

        if (ittlaIceForceButton.GetComponent<ForceButtons>().cost <= Util.ittlaSpirits &&
            ittlaIceForceButton.GetComponent<ForceButtons>().isInUse() == false)
        {
            ittlaIceForceButton.GetComponent<ForceButtons>().enableButton();
        }
        else
        {
            ittlaIceForceButton.GetComponent<ForceButtons>().disableButton();
        }

        if (ittlaWindForceButton.GetComponent<ForceButtons>().cost <= Util.ittlaSpirits &&
            ittlaWindForceButton.GetComponent<ForceButtons>().isInUse() == false)
        {
            ittlaWindForceButton.GetComponent<ForceButtons>().enableButton();
        }
        else
        {
            ittlaWindForceButton.GetComponent<ForceButtons>().disableButton();
        }

        if (ittlaSunForceButton.GetComponent<ForceButtons>().cost <= Util.ittlaSpirits &&
            (ittlaVasuCharge.GetComponent<CharacterCharge>().getCharge() < 1f ||
            ittlaKaapoCharge.GetComponent<CharacterCharge>().getCharge() < 1f ||
            ittlaRadCharge.GetComponent<CharacterCharge>().getCharge() < 1f) )
        {
            ittlaSunForceButton.GetComponent<ForceButtons>().enableButton();
        }
        else
        {
            ittlaSunForceButton.GetComponent<ForceButtons>().disableButton();
        }

        if (ittlaFaithForceButton.GetComponent<ForceButtons>().cost <= Util.ittlaSpirits)
        {
            ittlaFaithForceButton.GetComponent<ForceButtons>().enableButton();
        }
        else
        {
            ittlaFaithForceButton.GetComponent<ForceButtons>().disableButton();
        }
    }

    public void checkNertaButtonsStatus()
    {

        if (nertaIceForceButton.GetComponent<ForceButtons>().cost <= Util.nertaSpirits &&
            nertaIceForceButton.GetComponent<ForceButtons>().isInUse() == false)
        {
            nertaIceForceButton.GetComponent<ForceButtons>().enableButton();
        }
        else
        {
            nertaIceForceButton.GetComponent<ForceButtons>().disableButton();
        }

        if (nertaWindForceButton.GetComponent<ForceButtons>().cost <= Util.nertaSpirits &&
            nertaWindForceButton.GetComponent<ForceButtons>().isInUse() == false)
        {
            nertaWindForceButton.GetComponent<ForceButtons>().enableButton();
        }
        else
        {
            nertaWindForceButton.GetComponent<ForceButtons>().disableButton();
        }

        if (nertaSunForceButton.GetComponent<ForceButtons>().cost <= Util.nertaSpirits &&
            (nertaVasuCharge.GetComponent<CharacterCharge>().getCharge() < 1f ||
            nertaKaapoCharge.GetComponent<CharacterCharge>().getCharge() < 1f ||
            nertaRadCharge.GetComponent<CharacterCharge>().getCharge() < 1f) )
        {
            nertaSunForceButton.GetComponent<ForceButtons>().enableButton();
        }
        else
        {
            nertaSunForceButton.GetComponent<ForceButtons>().disableButton();
        }

        if (nertaFaithForceButton.GetComponent<ForceButtons>().cost <= Util.nertaSpirits)
        {
            nertaFaithForceButton.GetComponent<ForceButtons>().enableButton();
        }
        else
        {
            nertaFaithForceButton.GetComponent<ForceButtons>().disableButton();
        }
    }

    public void conquerIttlaPowerStone(int powerStoneIndex)
    {
        if (IttlaPowerStones[powerStoneIndex].GetComponent<PowerStone>().conquered == false)
        {
            // This power stone has not been conquered and this is the first time a totem arrives to the end of its path.
            IttlaPowerStones[powerStoneIndex].GetComponent<PowerStone>().conquered = true;

            if (!Util.IS_TRAINING)
            {
                IttlaPowerStones[powerStoneIndex].GetComponent<Renderer>().materials[1].color = Color.black;
                audiosourcePOWERSTONE.Play();
            }

            Util.ittlaLife -= 1;
            if (Util.ittlaLife <= 0)
            {
                Util.ittlaLife = 0;
                gameOver();
            }
        }
        /*else
        {
            // Is not the first totem arriving to the end of this power stone's path
            // Sum up 1 spirit
            sumUpOneNertaSpirit();
        }*/

        Debug.Log("Ittla Life: " + Util.ittlaLife);
    }


    public void conquerNertaPowerStone(int powerStoneIndex)
    {


        if (NertaPowerStones[powerStoneIndex].GetComponent<PowerStone>().conquered == false)
        {
            // This power stone has not been conquered and this is the first time a totem arrives to the end of its path.
            NertaPowerStones[powerStoneIndex].GetComponent<PowerStone>().conquered = true;

            if (!Util.IS_TRAINING)
            {
                NertaPowerStones[powerStoneIndex].GetComponent<Renderer>().materials[1].color = Color.black;
                audiosourcePOWERSTONE.Play();
            }

            Util.nertaLife -= 1;
            if (Util.nertaLife <= 0)
            {
                Util.nertaLife = 0;
                gameOver();
            }
        }
        /*else
        {
            // Is not the first totem arriving to the end of this power stone's path
            // Sum up 1 spirit
            sumUpOneIttlaSpirit();
        }*/


        Debug.Log("Nerta Life: " + Util.nertaLife);
    }

    public void gameOver()
    {
        Util.gameOver = true;

        // Stop all characters coroutines
        StopCoroutine("ittlaExecuteIceForce");
        StopCoroutine("nertaExecuteIceForce");
        StopCoroutine("accelerateIttlaTotems");
        StopCoroutine("accelerateNertaTotems");

        // Destroy old game ittla characters
        foreach (GameObject ittlaTotem in ittlaOnBoardTotems)
            Destroy(ittlaTotem);

        // Destroy old game nerta characters
        foreach (GameObject nertaTotem in nertaOnBoardTotems)
            Destroy(nertaTotem);

        // Empty list of current characters on board
        ittlaOnBoardTotems = new List<GameObject>();
        nertaOnBoardTotems = new List<GameObject>();

        disableIttlaGaps();
        disableNertaGaps();
        // Disable buttons
        if (!Util.IS_TRAINING)
        {
            ittlaButtons.SetActive(false);
            nertaButtons.SetActive(false);
        }


        showWinner();

        Util.partidas++;
        if (Util.WRITE_GAMES_IN_FILE)
        {
            Util.writeToFile("partidas.txt", Util.partidas + " -->  Ittla " + Util.ittlaGamesWon + " - " + Util.nertaGamesWon + " Nerta");
        }
    }


    public void stop()
    {
        // Academy has to be reset
        Util.academyShouldBeReset = true;

        // Stop all characters coroutines
        StopCoroutine("ittlaExecuteIceForce");
        StopCoroutine("nertaExecuteIceForce");
        StopCoroutine("accelerateIttlaTotems");
        StopCoroutine("accelerateNertaTotems");

        // Destroy old game ittla characters
        foreach (GameObject ittlaTotem in ittlaOnBoardTotems)
            Destroy(ittlaTotem);

        // Destroy old game nerta characters
        foreach (GameObject nertaTotem in nertaOnBoardTotems)
            Destroy(nertaTotem);

        // Empty list of current characters on board
        ittlaOnBoardTotems = new List<GameObject>();
        nertaOnBoardTotems = new List<GameObject>();


        // Reset spirits to initial values
        Util.ittlaSpirits = Util.INITIAL_SPIRITS;
        Util.nertaSpirits = Util.INITIAL_SPIRITS;

        // Reset available actions to initial values
        Util.ittlaAvailableActions = Util.getInitialAvailableActions();
        Util.nertaAvailableActions = Util.getInitialAvailableActions();

        // Update spirits texts
        ittlaSpiritsText.text = Util.ittlaSpirits.ToString();
        nertaSpiritsText.text = Util.nertaSpirits.ToString();

        // Reset power stones conquered status and color
        foreach (GameObject ittlaPowerStone in IttlaPowerStones)
        {
            ittlaPowerStone.GetComponent<PowerStone>().conquered = false;
            ittlaPowerStone.GetComponent<Renderer>().materials[1].color = ittlaMaterial.color;
        }
        foreach (GameObject nertaPowerStone in NertaPowerStones)
        {
            nertaPowerStone.GetComponent<PowerStone>().conquered = false;
            nertaPowerStone.GetComponent<Renderer>().materials[1].color = nertaMaterial.color;
        }

        // Disable Gaps
        disableIttlaGaps();
        disableNertaGaps();
    }
    
    public void startGame()
    {

        // Academy has to be reset
        Util.academyShouldBeReset = false;

        // Reset lifes to initial values
        Util.ittlaLife = Util.INITIAL_LIFE;
        Util.nertaLife = Util.INITIAL_LIFE;

        // Make the game roll
        Util.gameOver = false;

        Util.ittlaPlayerSelectedAction = Util.Action.None;
        Util.nertaPlayerSelectedAction = Util.Action.None;

        Util.ittlaWindSpeedMultiplier = Util.INITIAL_WIND_SPEED_MULTIPLIER;
        Util.nertaWindSpeedMultiplier = Util.INITIAL_WIND_SPEED_MULTIPLIER;


        // Enable buttons
        ittlaButtons.SetActive(true);
        nertaButtons.SetActive(true);

        // Reset Ittla buttons
        ittlaVasuCharge.GetComponent<CharacterCharge>().init();
        ittlaKaapoCharge.GetComponent<CharacterCharge>().init();
        ittlaRadCharge.GetComponent<CharacterCharge>().init();
        ittlaIceForceButton.GetComponent<ForceButtons>().init();
        ittlaWindForceButton.GetComponent<ForceButtons>().init();
        ittlaSunForceButton.GetComponent<ForceButtons>().init();
        ittlaFaithForceButton.GetComponent<ForceButtons>().init();

        // Reset Nerta buttons
        nertaVasuCharge.GetComponent<CharacterCharge>().init();
        nertaKaapoCharge.GetComponent<CharacterCharge>().init();
        nertaRadCharge.GetComponent<CharacterCharge>().init();
        nertaIceForceButton.GetComponent<ForceButtons>().init();
        nertaWindForceButton.GetComponent<ForceButtons>().init();
        nertaSunForceButton.GetComponent<ForceButtons>().init();
        nertaFaithForceButton.GetComponent<ForceButtons>().init();

        // Check status of buttons
        checkIttlaButtonsStatus();
        checkNertaButtonsStatus();

        // Make NPC's play
        if (Util.ittlaNPC != null)
        {
            Util.ittlaNPC.GetComponent<NPC>().play();
        }
        if (Util.nertaNPC != null)
        {
            Util.nertaNPC.GetComponent<NPC>().play();
        }
    }


    public void showWinner()
    {
        if (Util.ittlaLife <= 0)
        {
            Debug.Log("Winner: " + Util.Factions.Nerta);
            Util.nertaGamesWon += 1;
            showGameOverScreen(Util.Factions.Nerta);
        }
        else if (Util.nertaLife <= 0)
        {
            Debug.Log("Winner: " + Util.Factions.Ittla);
            Util.ittlaGamesWon += 1;
            showGameOverScreen(Util.Factions.Ittla);
        }
        gamesWonText.text = "Ittla " + Util.ittlaGamesWon + " - " + Util.nertaGamesWon + " Nerta";
    }
    
    public void killIttlaTotemsOnPath(int pathID)
    {

        if (!Util.IS_TRAINING)
        {
            Util.gc.audiosourceFAITH.Play();
            switch (pathID)
            {
                case 0:
                    Util.gc.faithParticlesPathIttlaShrine.GetComponent<ParticleSystem>().Play(true);
                    break;
                case 1:
                    Util.gc.faithParticlesPath1.GetComponent<ParticleSystem>().Play(true);
                    break;
                case 2:
                    Util.gc.faithParticlesPath2.GetComponent<ParticleSystem>().Play(true);
                    break;
                case 3:
                    Util.gc.faithParticlesPath3.GetComponent<ParticleSystem>().Play(true);
                    break;

                default:
                    break;
            }
        }

        List<GameObject> killTotems = new List<GameObject>();
        foreach (GameObject ittlaTotem in ittlaOnBoardTotems)
        {
            if (pathID == ittlaTotem.GetComponent<Totem>().getPathID())
            {
                killTotems.Add(ittlaTotem);
            }
        }
        // Kill them
        foreach (GameObject ittlaTotem in killTotems)
        {
            ittlaTotem.GetComponent<Totem>().life = 0;
            ittlaOnBoardTotems.Remove(ittlaTotem);
            Destroy(ittlaTotem);
        }
    }

    public void killNertaTotemsOnPath(int pathID)
    {

        if (!Util.IS_TRAINING)
        {
            Util.gc.audiosourceFAITH.Play();
            switch (pathID)
            {
                case 0:
                    Util.gc.faithParticlesPathNertaShrine.GetComponent<ParticleSystem>().Play(true);
                    break;
                case 1:
                    Util.gc.faithParticlesPath1.GetComponent<ParticleSystem>().Play(true);
                    break;
                case 2:
                    Util.gc.faithParticlesPath2.GetComponent<ParticleSystem>().Play(true);
                    break;
                case 3:
                    Util.gc.faithParticlesPath3.GetComponent<ParticleSystem>().Play(true);
                    break;
                default:
                    break;
            }
        }

        List<GameObject> killTotems = new List<GameObject>();

        foreach (GameObject nertaTotem in nertaOnBoardTotems)
        {
            if (pathID == nertaTotem.GetComponent<Totem>().getPathID())
            {
                killTotems.Add(nertaTotem);
            }
        }
        // Kill them
        foreach (GameObject nertaTotem in killTotems)
        {
            nertaTotem.GetComponent<Totem>().life = 0;
            nertaOnBoardTotems.Remove(nertaTotem);
            Destroy(nertaTotem);
        }
    }

    public int getTotalLifeOfIttlaTotemsInPath(int pathID)
    {
        int totalLife = 0;
        foreach (GameObject ittlaTotem in ittlaOnBoardTotems)
        {
            if (pathID == ittlaTotem.GetComponent<Totem>().getPathID())
            {
                totalLife += ittlaTotem.GetComponent<Totem>().life;
            }
        }
        return totalLife;
    }
    public int getTotalLifeOfNertaTotemsInPath(int pathID)
    {
        int totalLife = 0;
        foreach (GameObject nertaTotem in nertaOnBoardTotems)
        {
            if (pathID == nertaTotem.GetComponent<Totem>().getPathID())
            {
                totalLife += nertaTotem.GetComponent<Totem>().life;
            }
        }
        return totalLife;
    }
    public float getDistanceOfClosestIttlaTotemToEndOfPath(int pathID)
    {
        float closestDistance = Util.CONSIDERED_MAX_LENGTH_OF_PATHS;
        foreach (GameObject ittlaTotem in ittlaOnBoardTotems)
        {
            if (pathID == ittlaTotem.GetComponent<Totem>().getPathID())
            {
                float distancia = Vector3.Distance(ittlaTotem.transform.position, ittlaTotem.GetComponent<Totem>().getTotemGoalPosition());
                if (distancia < closestDistance)
                {
                    closestDistance = distancia;
                }
            }
        }
        return closestDistance;
    }
    public float getDistanceOfClosestNertaTotemToEndOfPath(int pathID)
    {
        float closestDistance = Util.CONSIDERED_MAX_LENGTH_OF_PATHS;
        foreach (GameObject nertaTotem in nertaOnBoardTotems)
        {
            if (pathID == nertaTotem.GetComponent<Totem>().getPathID())
            {
                float distancia = Vector3.Distance(nertaTotem.transform.position, nertaTotem.GetComponent<Totem>().getTotemGoalPosition());
                if (distancia < closestDistance)
                {
                    closestDistance = distancia;
                }
            }
        }
        return closestDistance;
    }

    public float getLifeOfAllIttlaTotemsNearToEndOfPath(int pathID)
    {
        float life = 0;
        foreach (GameObject ittlaTotem in ittlaOnBoardTotems)
        {
            if (pathID == ittlaTotem.GetComponent<Totem>().getPathID())
            {
                float distancia = Vector3.Distance(ittlaTotem.transform.position, ittlaTotem.GetComponent<Totem>().getTotemGoalPosition());
                if (distancia < Util.CONSIDERED_MAX_LENGTH_OF_PATHS/2)
                {
                    life += ittlaTotem.GetComponent<Totem>().life;
                }
            }
        }
        return life;
    }
    public float getLifeOfAllNertaTotemsNearToEndOfPath(int pathID)
    {
        float life = 0;
        foreach (GameObject nertaTotem in nertaOnBoardTotems)
        {
            if (pathID == nertaTotem.GetComponent<Totem>().getPathID())
            {
                float distancia = Vector3.Distance(nertaTotem.transform.position, nertaTotem.GetComponent<Totem>().getTotemGoalPosition());
                if (distancia < Util.CONSIDERED_MAX_LENGTH_OF_PATHS / 2)
                {
                    life += nertaTotem.GetComponent<Totem>().life;
                }
            }
        }
        return life;
    }

    // path 0 --> shrine
    // path 1 --> closest to the camera
    // path 2 --> the one in the middle
    // path 3 --> the farthest
    public bool isPathEmptyOfIttlaTotems(int path)
    {
        int totemsInPath = 0;
        foreach (GameObject ittlaTotem in ittlaOnBoardTotems)
        {
            if (path == ittlaTotem.GetComponent<Totem>().getPathID())
            {
                totemsInPath += 1;
            }
        }

        if (totemsInPath > 0)
        {
            return false;
        }

        return true;
    }

    // path 0 --> shrine
    // path 1 --> closest to the camera
    // path 2 --> the one in the middle
    // path 3 --> the farthest
    public bool isPathEmptyOfNertaTotems(int path)
    {
        int totemsInPath = 0;
        foreach (GameObject nertaTotem in nertaOnBoardTotems)
        {
            if (path == nertaTotem.GetComponent<Totem>().getPathID())
            {
                totemsInPath += 1;
            }
        }

        if (totemsInPath > 0)
        {
            return false;
        }

        return true;
    }

    public int getNumberOfGaps()
    {
        return IttlaGaps.Length;
    }

    
    private void showGameOverScreen(Util.Factions winner)
    {
        if (!Util.IS_TRAINING)
        {
            gameOverScreen.SetActive(true);
            if (winner == Util.Factions.Ittla)
            {
                gameOverScreenIttlaWinner.SetActive(true);
            }
            else
            {
                gameOverScreenNertaWinner.SetActive(true);
            }
        }
    }

    public void goToMenu()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public bool pathHasIttlaPowerStoneAlive(int path)
    {
        if (IttlaPowerStones[path].GetComponent<PowerStone>().conquered == false)
        {
            return true;
        }
        return false;
    }
    public bool pathHasNertaPowerStoneAlive(int path)
    {
        if (NertaPowerStones[path].GetComponent<PowerStone>().conquered == false)
        {
            return true;
        }
        return false;
    }

    public bool pathHasMuchMoreIttlaThanNertaTotems(int path)
    {
        if (getTotalLifeOfIttlaTotemsInPath(path) > getTotalLifeOfNertaTotemsInPath(path) * 1.5f)
        {
            return true;
        }
        return false;
    }
    public bool pathHasMuchMoreNertaThanIttlaTotems(int path)
    {
        if (getTotalLifeOfNertaTotemsInPath(path) > getTotalLifeOfIttlaTotemsInPath(path) * 1.5f)
        {
            return true;
        }
        return false;
    }
}
