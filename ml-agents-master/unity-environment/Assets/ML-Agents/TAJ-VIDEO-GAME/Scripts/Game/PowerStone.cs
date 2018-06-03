using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStone : MonoBehaviour {

    public bool conquered;
    private int indexInGameControllerList;
	// Use this for initialization
	void Start () {
        conquered = false;
	}

    public void setIndex(int index)
    {
        indexInGameControllerList = index;
    }

    public int getIndexInGameControllerList()
    {
        return indexInGameControllerList;
    }
}
