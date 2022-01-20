using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pathfinding A* 
/// </summary>
public class PathFinding : MonoBehaviour
{
	private Node a, b;
	public Vector2 currentTarget;

	/// <summary>
	/// Matrice des point de la map
	/// </summary>
	private Node[,] grid;
	
	/// <summary>
	/// Cherche le plus cours chemin entre la position actuelle et la cible
	/// </summary>
	public void SeekPath()
	{
		FindPath(transform.position, currentTarget);
	}

	/// <summary>
	/// Détermine le point le plus loins, dans le champ de vision de l'ennemis
	/// </summary>
	/// <param name="vision">champ de vision de l'ennemis</param>
	/// <returns>Le point le plus loin</returns>
	public Vector2 FindFurthestPoint(int vision)
    {
		Vector3 startPos = transform.position;
		//CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
		a = grid[(int)startPos.x, (int)startPos.y];
		Node furthest = a;
		List<Node> furthestGroup = new List<Node>();
		furthestGroup.Add(a);
		//Debug.Log("a : " + a.x + " - " + a.y + " => " + a.gCost);
		List<Node> openSet = new List<Node>();
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(a);
		while (openSet.Count > 0)
		{
			// node le plus loin
			Node node = openSet[0];
			for (int i = 1; i<openSet.Count; i++)
			{
				if (openSet[i].gCost > node.gCost)
					node = openSet[i];
			}

			openSet.Remove(node);
			closedSet.Add(node);

			// update distance des voisins
			//Debug.Log("node : " + node.x + " - " + node.y + " => " + node.gCost);
			foreach (Node neighbour in GetNeighbours(node))
			{
				//Debug.Log("neighbour : " + neighbour.x + " - " + neighbour.y + " => " + neighbour.gCost);
				if (!neighbour.visable || closedSet.Contains(neighbour))
				{
					continue;
				}

				int newCostToNeighbour = node.gCost + 10;//GetDistance(node, neighbour);
				//Debug.Log("note updated");
				neighbour.gCost = newCostToNeighbour;

				if (!openSet.Contains(neighbour))
					openSet.Add(neighbour);
				
			}
			if(node.gCost <= vision * 10) {
				if (node.gCost > furthest.gCost)
				{
					furthest = node;
					furthestGroup.Clear();
					furthestGroup.Add(node);
				}
				else if (node.gCost == furthest.gCost) {
					furthestGroup.Add(node);
				}
			}
		}
		furthest = furthestGroup[GameObject.Find("GameMaster").GetComponent<MapGenerator>().random.Next(0, furthestGroup.Count)];
		
		//Debug.Log("Furthest Point has gCost "+ furthest.x+" "+ furthest.y+ " => "+ furthest.gCost);
		return new Vector2(furthest.x, furthest.y);
	}

	/// <summary>
	/// Détermine si l'ennemie peut voir le joueur de puis sa position
	/// </summary>
	/// <param name="vision">champ de vision de l'ennemis</param>
	/// <param name="playerPosition">Position de l'ennemi</param>
	/// <returns></returns>
	public bool FindPlayer(int vision,Vector2 playerPosition)
	{
		Vector3 startPos = transform.position;
		//CreateGrid(GameObject.Find("Map").GetComponent<Map>().mapItemsList);
		a = grid[(int)startPos.x, (int)startPos.y];
		
		//Debug.Log("a : " + a.x + " - " + a.y + " => " + a.gCost);
		List<Node> openSet = new List<Node>();
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(a);
		while (openSet.Count > 0)
		{
			// node le plus loin
			Node node = openSet[0];
			for (int i = 1; i < openSet.Count; i++)
			{
				if (openSet[i].gCost > node.gCost)
					node = openSet[i];
			}

			openSet.Remove(node);
			closedSet.Add(node);

			// update distance des voisins
			//Debug.Log("node : " + node.x + " - " + node.y + " => " + node.gCost);
			foreach (Node neighbour in GetNeighbours(node))
			{
				//Debug.Log("neighbour : " + neighbour.x + " - " + neighbour.y + " => " + neighbour.gCost);
				if (!neighbour.visable || closedSet.Contains(neighbour))
				{
					continue;
				}

				int newCostToNeighbour = node.gCost + 10;//GetDistance(node, neighbour);
														 //Debug.Log("note updated");
				neighbour.gCost = newCostToNeighbour;

				if (!openSet.Contains(neighbour))
					openSet.Add(neighbour);

			}
			if (node.gCost <= vision * 10)
			{
				if(Vector2.Distance(playerPosition,new Vector2(node.x, node.y))<1f){
					return true;
                }
			}
		}
		return false;
	}

	/// <summary>
	/// Trouve le plus cours chemin entre les 2 points
	/// </summary>
	/// <param name="startPos">Point de départ</param>
	/// <param name="targetPos">Point cible</param>
	void FindPath(Vector3 startPos, Vector3 targetPos)
	{
		a = grid[(int)startPos.x, (int)startPos.y];
		b = grid[(int)targetPos.x, (int)targetPos.y];
		//Debug.Log("a : " + a.x + " - " + a.y + " => " + a.gCost);
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
			//Debug.Log("node : " + node.x + " - " + node.y + " => " + node.gCost);
			foreach (Node neighbour in GetNeighbours(node))
			{
				//Debug.Log("neighbour : "+ neighbour.x+" - "+ neighbour.y+" => "+ neighbour.gCost);
				if (!neighbour.walkable || closedSet.Contains(neighbour))
				{
					continue;
				}

				int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
				if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
				{
					//Debug.Log("note updated");
					neighbour.gCost = newCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, b);
					neighbour.parent = node;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
		Debug.Log("Path not found");
		GetComponent<Ennemis>().path = new List<Node>();
	}

	/// <summary>
	/// Trace le chemin entre les deux point et le place dans le path de l'ennemi
	/// </summary>
	/// <param name="a">Point de départ</param>
	/// <param name="endNode">Point cible</param>
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
		//Debug.Log("Path found");
		GetComponent<Ennemis>().path = path;
	}

	/// <summary>
	/// Calcule la distance entre deux point
	/// </summary>
	/// <param name="nodeA">Point 1</param>
	/// <param name="nodeB">Point 2</param>
	/// <returns>Distance</returns>
	int GetDistance(Node nodeA, Node nodeB)
	{
		int dstX = Mathf.Abs((int)nodeA.x - (int)nodeB.x);
		int dstY = Mathf.Abs((int)nodeA.y - (int)nodeB.y);

		return 10 * dstX + 10 * dstY;
	}

	/// <summary>
	/// Retourne les voisins (sans diagonale) du point donné
	/// </summary>
	/// <param name="node">Le point</param>
	/// <returns>List des voisins</returns>
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

	/// <summary>
	/// Convertie la Matrice d'objet de la map en matrice de point pour le pathfinding
	/// </summary>
	/// <param name="mapItemsList"></param>
	public void CreateGrid(MapItem[,] mapItemsList)
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

	/// <summary>
	/// Change la cible actuelle
	/// </summary>
	/// <param name="newTarget">Nouvelle cible</param>
	public void SwitchTarget(Vector2 newTarget)
    {
		currentTarget = newTarget;
	}

}
