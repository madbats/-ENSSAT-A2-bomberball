using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public bool walkable;
	public Vector3 worldPosition;
	public int x;
	public int y;

	public int gCost;
	public int hCost;
	public Node parent;

	public Node(MapItem item)
	{
		walkable = (item is Sol);
		worldPosition = item.transform.position;
		x = (int)item.transform.position.x;
		y = (int)item.transform.position.y;
	}

	public int fCost
	{
		get
		{
			return gCost + hCost;
		}
	}
}
