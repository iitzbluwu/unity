using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{   
    [SerializeField]
     GameObject player;

    [SerializeField]
    float speedOfset;

    [SerializeField]
    Vector2 posOffset;

    [SerializeField]
    float leftlimit;

    [SerializeField]
    float rightlimit;


    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 startPos = transform.position;
        
        Vector3 endPos = player.transform.position;
        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -10;

        transform.position = Vector3.Lerp(startPos, endPos, speedOfset*Time.deltaTime);

        /*transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x, leftlimit, rightlimit),
                transform.position.z
            );*/
    }
}