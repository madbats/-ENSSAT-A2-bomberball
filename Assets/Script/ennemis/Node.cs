using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Point de la carte selon le pathfinding
/// </summary>
public class Node
{
	/// <summary>
	/// D�termine si le point peut-�tre travers�
	/// </summary>
	public bool walkable;

	/// <summary>
	/// Position sur la carte
	/// </summary>
	public Vector3 worldPosition;
	public int x;
	public int y;

	/// <summary>
	/// Distance � parcourir pour atteindre ce point en partant du point de d�part
	/// </summary>
	public int gCost;
	/// <summary>
	/// Distance � parcourir pour atteindre le point cibl� en partant de ce point
	/// </summary>
	public int hCost;

	/// <summary>
	/// Le point � travers� avant
	/// </summary>
	public Node parent;

	public Node(MapItem item)
	{
		walkable = (item is Sol);
		GameObject bomb = GameObject.Find("Bomb(Clone)");
		worldPosition = item.transform.position;
		x = (int)item.transform.position.x;
		y = (int)item.transform.position.y;
		if (bomb != null)
		{
			walkable = walkable && (Vector2.Distance(item.transform.position, bomb.transform.position)>.1);
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
