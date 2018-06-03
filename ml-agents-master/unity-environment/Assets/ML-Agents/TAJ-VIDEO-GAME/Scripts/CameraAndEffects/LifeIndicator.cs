using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeIndicator : MonoBehaviour {

    private Camera cameraToLookAt;
    public Text lifeText;
    public Totem totem;

    void Start()
    {


        cameraToLookAt = Camera.main;
        if (totem.getFaction() == Util.Factions.Ittla)
        {
            GetComponent<Image>().color = Util.gc.ittlaMaterial.color;
            lifeText.color = Color.white;
        }
        else if (totem.getFaction() == Util.Factions.Nerta)
        {
            GetComponent<Image>().color = Util.gc.nertaMaterial.color;
            lifeText.color = Color.white;
        }

    }

    void Update()
    {
        lifeText.text = totem.life.ToString();


        Vector3 v = new Vector3(transform.position.x, -cameraToLookAt.transform.position.y, -cameraToLookAt.transform.position.z);
        transform.LookAt(v);
        /*Vector3 v = cameraToLookAt.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(cameraToLookAt.transform.position - v);*/
        //transform.Rotate(0, 0, 0);
    }
}
