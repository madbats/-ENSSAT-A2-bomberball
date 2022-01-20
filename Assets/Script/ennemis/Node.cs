using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Point de la carte selon le pathfinding
/// </summary>
public class Node
{
	/// <summary>
	/// Détermine si le point peut-être traversé
	/// </summary>
	public bool walkable;
	/// <summary>
	/// Détermine si le point peut-être traversé par la vue de l'ennemie
	/// </summary>
	public bool visable;

	/// <summary>
	/// Position sur la carte
	/// </summary>
	public Vector3 worldPosition;
	public int x;
	public int y;

	/// <summary>
	/// Distance à parcourir pour atteindre ce point en partant du point de départ
	/// </summary>
	public int gCost;
	/// <summary>
	/// Distance à parcourir pour atteindre le point ciblé en partant de ce point
	/// </summary>
	public int hCost;

	/// <summary>
	/// Le point à traversé avant
	/// </summary>
	public Node parent;

	public Node(MapItem item)
	{
		walkable = (item is Sol);
		visable = walkable;
		GameObject bomb = GameObject.Find("Bomb(Clone)");
		worldPosition = item.transform.position;
		x = (int)item.transform.position.x;
		y = (int)item.transform.position.y;
		if (bomb != null)
		{
			walkable = walkable && (Vector2.Distance(item.transform.position, bomb.transform.position)>.2);
			///Debug.Log(x + "," + y + "  " + walkable +" "+visable);
		}
	}

	/// <summary>
	/// Cout total de ce point : gCost + hCost
	/// </summary>
	public int fCost
	{
		get
		{
			return gCost + hCost;
		}
	}
}
