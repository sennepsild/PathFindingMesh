using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	public float radius = 1;
	public Vector2 regionSize = Vector2.one;
	public int rejectionSamples = 30;
	public float displayRadius =1;

    public GameObject meshru;
    public MapGenerator mapgen;


    public List<GameObject> trees;


	List<Vector2> points;

	void OnValidate() {
		points = PoissonDiscSampling.GeneratePoints(radius, regionSize, rejectionSamples);
	}


    private void Start()
    {
        mapgen.GenerateMap();

        Destroy(meshru.GetComponent<MeshCollider>());
        meshru.AddComponent<MeshCollider>();
        meshru.GetComponent<MeshCollider>().sharedMesh = meshru.GetComponent<MeshFilter>().sharedMesh;


        if (points != null)
        {
            foreach (Vector2 point in points)
            {


               
                GameObject temp = Instantiate(trees[Mathf.RoundToInt(Random.Range(0, trees.Count))]);
                temp.AddComponent<groundTree>();

                temp.transform.localScale = new Vector3(2, 2, 2);
                temp.transform.position = new Vector3(point.x- (241*10)/2, 50,point.y- (241 * 10)/2);

             }
        }
    }

    /*
    void OnDrawGizmos() {
		Gizmos.DrawWireCube(regionSize/2,regionSize);
		if (points != null) {
			foreach (Vector2 point in points) {

				Gizmos.DrawSphere(point, displayRadius);
			}
		}
	}
    */
}
