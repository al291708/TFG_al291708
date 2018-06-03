using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPath : MonoBehaviour {

    int childIndex;

    public Transform paths;
    Transform currentMarker;
    Transform endMarker;
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;
    public Vector3 finalRotation;

    void Start()
    {        
        currentMarker = paths.transform.GetChild(0).transform;
        endMarker = paths.transform.GetChild(1).transform;
        transform.position = currentMarker.transform.position;
        transform.LookAt(currentMarker.GetComponent<CameraPosition>().lookAtMe.position);
        childIndex = 1;

        startTime = Time.time;
        journeyLength = Vector3.Distance(currentMarker.position, endMarker.position);
    }

    void Update()
    {
        if (transform.eulerAngles.x > finalRotation.x - 0.1 &&
            transform.eulerAngles.x < finalRotation.x + 0.1 &&
            transform.eulerAngles.y > finalRotation.y - 0.1 &&
            transform.eulerAngles.y < finalRotation.y + 0.1 &&
            transform.eulerAngles.z > finalRotation.z - 0.1 &&
            transform.eulerAngles.z < finalRotation.z + 0.1)
        {
            Debug.Log("Animacion terminada");
            Util.gc.startGame();
            Destroy(this);
        }

        if (Util.IS_TRAINING)
        {
            transform.position = paths.transform.GetChild(paths.transform.childCount - 1).transform.position;
            transform.eulerAngles = finalRotation;
        }
        else
        {

            if (currentMarker.GetComponent<CameraPosition>() != null)
            {
                float distCovered = (Time.time - startTime) * speed;
                float fracJourney = distCovered / journeyLength;

                // Rotate
                Vector3 direction = endMarker.GetComponent<CameraPosition>().lookAtMe.transform.position - transform.position;
                Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, fracJourney / 10);

                if (Vector3.Distance(transform.position, endMarker.position) > 0.01f)
                {
                    // Move
                    transform.position = Vector3.Lerp(currentMarker.position, endMarker.position, fracJourney);
                }
                else
                {
                    if (childIndex + 1 < paths.transform.childCount)
                    {
                        currentMarker = paths.transform.GetChild(childIndex).transform;
                        endMarker = paths.transform.GetChild(childIndex + 1).transform;
                        childIndex += 1;
                        startTime = Time.time;
                        journeyLength = Vector3.Distance(currentMarker.position, endMarker.position);
                    }
                }

                
            }
        }
    }
}
