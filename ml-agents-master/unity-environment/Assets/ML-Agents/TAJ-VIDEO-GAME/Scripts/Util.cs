using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Util {

    public const bool IS_TRAINING = false;

    public const bool WRITE_GAMES_IN_FILE = false;
    public const float TIME_SCALE = 1f;

    public static int partidas = 0;

    public const float NPC_MIN_WAITING_BETWEEN_ACTIONS = 1f;
    public const float NPC_MAX_WAITING_BETWEEN_ACTIONS = 1f;
    public const float AGENT_MIN_WAITING_BETWEEN_DECISIONS = 2f;
    public const float AGENT_MAX_WAITING_BETWEEN_DECISIONS = 2f;

    public const float NPC_PROBABILITY_OF_USING_SHRINE_GAP = 25;

    public const int INITIAL_LIFE = 3;
    public const int INITIAL_SPIRITS = 0;
    public const int INITIAL_WIND_SPEED_MULTIPLIER = 1;

    public const int CONSIDERED_MAX_LENGTH_OF_PATHS = 13; // Only for Agent Observations
    
    public const int VASU_INITIAL_LIFE = 6;
    public const int KAAPO_INITIAL_LIFE = 4;
    public const int RAD_INITIAL_LIFE = 2;


    public enum Factions { Nerta, Ittla };
    public enum Action { None, Vasu, Rad, Kaapo, IceForce, WindForce, SunForce, FaithForce };

    public static Action ittlaPlayerSelectedAction = Action.None;
    public static Action nertaPlayerSelectedAction = Action.None;

    public static List<Action> ittlaAvailableActions = getInitialAvailableActions();
    public static List<Action> nertaAvailableActions = getInitialAvailableActions();

    public static GameController gc;
    public static NPC ittlaNPC;
    public static NPC nertaNPC;

    public static int ittlaLife = INITIAL_LIFE; // Life = Powerstones
    public static int nertaLife = INITIAL_LIFE; // Life = Powerstones

    public static int ittlaSpirits = INITIAL_SPIRITS;
    public static int nertaSpirits = INITIAL_SPIRITS;
    
    public static float ittlaWindSpeedMultiplier = 1;
    public static float nertaWindSpeedMultiplier = 1;

    public static float iceEffectDuration = 5;
    public static float windEffectDuration = 5;


    public static bool gameOver = true;
    public static bool academyShouldBeReset = false;

    public static int ittlaGamesWon = 0;
    public static int nertaGamesWon = 0;
    
    public static List<Action> getInitialAvailableActions(){
        List<Action> initialAvailableActions = new List<Action>();
        initialAvailableActions.Add(Action.None);
        return initialAvailableActions;
    }
    
    public static void debugAvailableActions(){
        Debug.Log("---");
        string debug = "";
        foreach (Action ittlaAction in ittlaAvailableActions)
        {
            debug += ittlaAction + ", ";
        }
        Debug.Log("Ittla: " + debug);
        debug = "";
        foreach (Action nertaAction in nertaAvailableActions)
        {
            debug += nertaAction + ", ";
        }
        Debug.Log("Nerta: " + debug);
        Debug.Log("---");
    }

    public static void writeToFile(string filename, string contenido)
    {
        string path = "Assets/"+filename;

        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(contenido);
        writer.Close();
    }

}
