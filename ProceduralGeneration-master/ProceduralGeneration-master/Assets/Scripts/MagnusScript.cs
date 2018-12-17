using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnusScript : MonoBehaviour {

    public Texture2D map;
    public static float[,] ganmap;
    Color[] getempixels;

    

    public GameObject quadd;

    public MapGenerator mapgen;

	// Use this for initialization
	void Awake () {

        

        Texture2D tex = new Texture2D(map.width, map.height);

        
        tex.SetPixels(map.GetPixels());

        TextureScale.Bilinear(tex, 241, 241);

        

        getempixels = tex.GetPixels();
        
        //Debug.Log(getempixels);


        ganmap = new float[241, 241];

        for (int y = 0; y < 241; y++)
           
        {
            for (int x = 0; x < 241; x++)
            {
                //Debug.Log(getempixels[x + y * x].r);
                ganmap[y,x] = getempixels[y * 241 + x].r;

                
            }
        }

        quadd.GetComponent<Renderer>().material.mainTexture = tex;
        Debug.Log("DONE BIACH");
        mapgen.GenerateMap();

    }
	
	// Update is called once per frame
	void Update () {

	}
}
