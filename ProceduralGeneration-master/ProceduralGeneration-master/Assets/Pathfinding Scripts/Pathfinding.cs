using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Pathfinding : MonoBehaviour
{
    public GameObject imageOfGrid;
    public Transform seeker, target;
    Grid grid;
    public BezierSpline spline;

    public bool autoUpdate;

    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    public void Pathfind()
    {
        grid = GetComponent<Grid>();
        string AllTheData = "";
        for (int i = 0; i < 20; i++)
        {


            bool SeekerInPos = false;
            while (!SeekerInPos)
            {
                
                seeker.position = new Vector3(Random.Range(-(grid.gridWorldSize.x / 2), grid.gridWorldSize.x / 2), 0, Random.Range(-(grid.gridWorldSize.y / 2), grid.gridWorldSize.y / 2));
                SeekerInPos = grid.NodeFromWorldPoint(seeker.position).walkable;
            }
            bool targetInPos = false;
            while (!targetInPos)
            {
                target.position = new Vector3(Random.Range(-(grid.gridWorldSize.x / 2), grid.gridWorldSize.x / 2), 0, Random.Range(-(grid.gridWorldSize.y / 2), grid.gridWorldSize.y / 2));
                targetInPos = grid.NodeFromWorldPoint(target.position).walkable;
            }


            //var splineScript = GetComponent<BezierSpline>();
            grid = GetComponent<Grid>();
            FindPath(seeker.position, target.position);

            AllTheData+=("Number: "+i+ " Point Distance: " + Vector3.Distance(target.position, seeker.position) + ", Path Length: " + spline.GetLength()+" \n");
            MakeImg(i);
        }

        File.WriteAllText(Application.dataPath + "/../CollectedData.txt", AllTheData);
    }

    public void MakeImg(int number)
    {
        imageOfGrid.GetComponent<Renderer>().sharedMaterial.mainTexture = grid.ImgOfPath(number);
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        spline.Reset();
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        int i = 0;
        while (currentNode != startNode)
        {
            spline.points[i] = currentNode.worldPosition;
            path.Add(currentNode);
            currentNode = currentNode.parent;
            i++;
            if (spline.points.Length == i)
            {
                spline.AddCurve();
            }
        }
        path.Reverse();
        grid.path = path;

    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}