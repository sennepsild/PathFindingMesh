using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundTree : MonoBehaviour {

    public MapGenerator mapgen;
    public void Start()
    {
        mapgen = MapGenerator.mapgen;
        PutOnGround();

        

        Debug.Log(mapgen.meshHeightCurve.Evaluate(0.7f) * mapgen.meshHeightMultiplier*10);
    }

    public void Update()
    {
        
        if (transform.position.y < (mapgen.meshHeightCurve.Evaluate(0.45f) * mapgen.meshHeightMultiplier )*10 || transform.position.y > (mapgen.meshHeightCurve.Evaluate(0.6f) * mapgen.meshHeightMultiplier) * 10)
        {

            
            Destroy(this.gameObject);
        }

        
    }

    public void PutOnGround() {

        
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {

           

            transform.position = hit.point;

            
           
        }
    }
}
