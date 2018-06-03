using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Totem : MonoBehaviour
{
    Transform totemGoal;
    public NavMeshAgent agent;
    public int life;
    public float speed;
    private bool frozen;
    Util.Factions faction;
    private int pathID;
    
    void OnTriggerEnter(Collider other)
    {        
        // Check the collision only once (only the Ittla totems manage the collisions,
        // just to not count them two times and fall into mistakes)
        if (faction == Util.Factions.Ittla)
        {
            Totem enemyTotem = other.GetComponent<Totem>();
            if (enemyTotem != null)
            {
                if (enemyTotem.getFaction() != faction)
                {
                    int enemyTotemLifeOnCollision = enemyTotem.life;
                    int lifeOnCollision = life;


                    enemyTotem.life -= lifeOnCollision;
                    life -= enemyTotemLifeOnCollision;

                    /*Debug.Log(faction + " life: " + life + " - " + this.gameObject );
                    Debug.Log(enemyTotem.getFaction() + " life: " + enemyTotem.life + " - " + enemyTotem.gameObject);
                    Debug.Log("---");*/


                    if (enemyTotem.life <= 0)
                    {
                        enemyTotem.life = 0;
                        Util.gc.nertaOnBoardTotems.Remove(enemyTotem.gameObject);
                        Destroy(enemyTotem.gameObject);
                    }
                    if (life <= 0)
                    {
                        life = 0;
                        Util.gc.ittlaOnBoardTotems.Remove(this.gameObject);
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
    

    void Update()
    {
        //transform.position += new Vector3(0, Mathf.Sin(Time.deltaTime * 40), 0);
        if (Util.gameOver == false)
        {
            if (goalReached())
            {
                if (totemGoal.CompareTag("Heaven"))
                {
                    if (faction == Util.Factions.Ittla)
                    {
                        Util.gc.sumUpOneIttlaSpirit();
                        Util.gc.ittlaOnBoardTotems.Remove(this.gameObject);
                    }
                    else
                    {
                        Util.gc.sumUpOneNertaSpirit();
                        Util.gc.nertaOnBoardTotems.Remove(this.gameObject);
                    }

                    if (!Util.IS_TRAINING)
                    {
                        Util.gc.audiosourceSPIRIT.Play();
                    }
                }
                else if (totemGoal.CompareTag("Gap"))
                {
                    if (faction == Util.Factions.Ittla)
                    {
                        Util.gc.conquerNertaPowerStone(totemGoal.GetComponent<Gap>().relatedPowerStone.GetComponent<PowerStone>().getIndexInGameControllerList());
                        Util.gc.ittlaOnBoardTotems.Remove(this.gameObject);
                    }
                    if (faction == Util.Factions.Nerta)
                    {
                        Util.gc.conquerIttlaPowerStone(totemGoal.GetComponent<Gap>().relatedPowerStone.GetComponent<PowerStone>().getIndexInGameControllerList());
                        Util.gc.nertaOnBoardTotems.Remove(this.gameObject);
                    }
                }
                Destroy(this.gameObject);
            }
        }
    }

    public int getPathID()
    {
        return pathID;
    }

    public void setPathID(int id)
    {
        pathID = id;
    }

    public void updateSpeed()
    {
        if (faction == Util.Factions.Ittla)
        {
            agent.speed = speed * Util.ittlaWindSpeedMultiplier;
            agent.acceleration = speed * Util.ittlaWindSpeedMultiplier;
        }
        else if (faction == Util.Factions.Nerta)
        {
            agent.speed = speed * Util.nertaWindSpeedMultiplier;
            agent.acceleration = speed * Util.nertaWindSpeedMultiplier;
        }
    }

    public void pauseMovement()
    {
        agent.Stop();
    }
    public void resumeMovement()
    {
        agent.Resume();
    }

    public void moveTo(Transform goal)
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        updateSpeed();
        agent.destination = goal.position;
        totemGoal = goal;
    }

    public Vector3 getTotemGoalPosition(){
        return totemGoal.transform.position;
    }


    public void freeze()
    {
        pauseMovement();
        frozen = true;
    }

    public void setIsFrozen(bool isFrozen)
    {
        frozen = isFrozen;
    }


    public void unfreeze()
    {
        resumeMovement();
        frozen = false;
    }

    public bool isFrozen()
    {
        return frozen;
    }

    public bool goalReached()
    {
        if (agent != null)
        {
            float dist = agent.remainingDistance;
            if (dist != Mathf.Infinity &&
                agent.pathStatus == NavMeshPathStatus.PathComplete
                && agent.remainingDistance < agent.stoppingDistance)
            {
                return true;
            }            
        }
        return false;
    }

    public void setFaction(Util.Factions f)
    {
        faction = f;
    }

    public Util.Factions getFaction()
    {
        return faction;
    }
}