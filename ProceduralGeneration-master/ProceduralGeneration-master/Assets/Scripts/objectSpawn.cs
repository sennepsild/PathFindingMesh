using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSpawn : MonoBehaviour {

    private float nextActionTime = 0f;
    public float period = 0.1f;

    public Vector3 playerPos;

    public GameObject playerObject;
    public GameObject sphere;

    void Start()
    {
        //transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        playerPos = playerObject.transform.position;

        if (Time.time > nextActionTime)
        {
            nextActionTime += period;

            Instantiate(sphere, playerPos, Quaternion.identity);

            //Debug.Log("Sphere is instantiated");
        }

        
        
    }
}
