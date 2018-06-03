using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEffect : MonoBehaviour
{
    public float scrollSpeedY = 0.2F;
    public float movementSpeedX;
    public float qtyMovementX;

    private float offsetX = 0;
    private float offsetY = 0;


    public Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        if (Util.gameOver == false)
        {
            offsetX = (Mathf.Sin(Time.time * movementSpeedX)) * qtyMovementX;
            offsetY = Time.time * (-scrollSpeedY);
            rend.material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
        }
    }
}
