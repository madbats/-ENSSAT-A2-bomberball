using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
	private Node a,b;
		
	public Vector2 currentTarget;
	//private int[,] map;
	private Node[,] grid;

	public void SeekPath()
	{
		CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
		FindPath(transform.position, currentTarget);
	}

	void FindPath(Vector3 startPos, Vector3 targetPos)
	{
		a = grid[(int)startPos.x, (int)startPos.y];
		b = grid[(int)targetPos.x, (int)targetPos.y];
		Debug.Log("a : " + a.x + " - " + a.y + " => " + a.gCost);
		List<Node> openSet = new List<Node>();
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(a);
		//a.gCost = 0;
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

			if (node == b)
			{
				RetracePath(a, b);
				
				return;
			}
			Debug.Log("node : " + node.x + " - " + node.y + " => " + node.gCost);
			foreach (Node neighbour in GetNeighbours(node))
			{
				Debug.Log("neighbour : "+ neighbour.x+" - "+ neighbour.y+" => "+ neighbour.gCost);
				if (!neighbour.walkable || closedSet.Contains(neighbour))
				{
					continue;
				}

				int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
				if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
				{
					Debug.Log("note updated");
					neighbour.gCost = newCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, b);
					neighbour.parent = node;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
		Debug.Log("Path not found");
	}

	void RetracePath(Node a, Node endNode)
	{
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != a)
		{
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse();
		Debug.Log("Path found");
		GetComponent<Ennemis>().path = path;
	}

	int GetDistance(Node nodeA, Node nodeB)
	{
		int dstX = Mathf.Abs((int)nodeA.x - (int)nodeB.x);
		int dstY = Mathf.Abs((int)nodeA.y - (int)nodeB.y);

		return 10 * dstX + 10 * dstY;
	}

	public List<Node> GetNeighbours(Node node)
	{
		List<Node> neighbours = new List<Node>();
		int newX, newY;
		for (int i = -1; i <= 1; i++)
		{
			if (i == 0)
				continue;
			newX = node.x + i;
			newY = node.y + i;
			if (newY >= 0 && newY < 11)
				neighbours.Add(grid[node.x,newY]);
			if (newX >= 0 && newX < 13)
				neighbours.Add(grid[newX, node.y]);
		}
		return neighbours;
	}

	void CreateGrid(MapItem[,] mapItemsList)
	{
		grid = new Node[13, 11];
		for (int i = 0; i < 13; i++)
		{
			for (int j = 0; j < 11; j++)
			{
				grid[i, j] = new Node(mapItemsList[i, j]);
			}
		}
	}

	public void SwitchTarget(Vector2 newTarget)
    {
		currentTarget = newTarget;
	}
}
