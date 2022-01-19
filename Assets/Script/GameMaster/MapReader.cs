using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapReader : MonoBehaviour
{
	private string createdMap;

	public System.Random random;
	int difficulty;
	List<Vector2> solPossible = new List<Vector2>();
	List<List<Vector2>> Paths;
	private int[,] map = new int[13, 11];

	public GameObject zombie;
	public GameObject explorer;
	public GameObject watchman;
	public GameObject hunter;
	Vector2 entree;
	GameObject[,] mapEnnemisList;

	public int[,] FetchMap()
	{
		this.createdMap = PlayerPrefs.GetString("map");
		return CreateMap();
	}

	private int[,] CreateMap()
	{
		GameObject[,] testEnnemisList = {
			{ null, null, null, null, null, null, null, null, null, null, null},
			{ null, null, null, null, null, null, null, null, null, null, null},
			{ null, null, null, null, null, null, null, null, null, null, null},
			{ null, null, null, null, null, null, null, null, null, null, null},
			{ null, null, null, null, null, null, null, null, null, null, null},
			{ null, null, null, null, null, null, null, null, null, null, null},
			{ null, null, null, null, null, null, null, null, null, null, null},
			{ null, null, null, null, null, null, null, null, null, null, null},
			{ null, null, null, null, null, null, null, null, null, null, null},
			{ null, null, null, null, null, null, null, null, null, null, null},
			{ null, null, null, null, null, null, null, null, null, null, null},
			{ null, null, null, null, null, null, null, null, null, null, null},
			{ null, null, null, null, null, null, null, null, null, null, null}};
		mapEnnemisList = testEnnemisList;

		int carac;

		Vector2 b;
		int index;

		for (int i = 0; i < 13; i++)
		{
			for (int j = 0; j < 11; j++)
			{
				carac = createdMap[2 * (13 * i + j)] + createdMap[2 * (13 * i + j) + 1];
				if (carac == 30)
				{
					map[i, j] = 0;
					CreateZombie(new Vector2(i, j), new Vector2());
				}
				else if(carac == 31)
                {
					map[i, j] = 0;
					CreateExplorer(new Vector2(i, j));
                }
				else if(carac == 32)
                {
					map[i, j] = 0;
					CreateWatchman(new Vector2(i, j), new Vector2());
				}
				else if(carac == 33)
                {
					map[i, j] = 0;
					CreateHunter(new Vector2(i, j));
				}
				else
                {
					map[i, j] = carac;
					if(carac == 21)
                    {
						entree = new Vector2(i, j);
                    }
                }
			}
		}

		createPaths();

		for (int i = 0; i < 13; i++)
		{
			for (int j = 0; j < 11; j++)
			{
				carac = createdMap[2 * (13 * i + j)] + createdMap[2 * (13 * i + j) + 1];
				if (carac == 30)
				{
					index = random.Next(0, Paths.Count);

					if (Paths[index].Count == 0)
					{
						b = new Vector2(i, j);
					}
					else
					{
						do
						{
							b = Paths[index][random.Next(0, Paths[index].Count)];
							Paths[index].Remove(b);
						} while ((Vector2.Distance(entree, b) < 2f) && Paths[index].Count > 0);
						//Debug.Log("2-Creating passive" + ennemi_type);
						if (Paths[index].Count == 0)
						{ }
					}

					CreateZombie(new Vector2(i, j), b);
					Paths.Remove(Paths[index]);
				}
				else if (carac == 31)
				{
					CreateExplorer(new Vector2(i, j));
				}
				else if (carac == 32)
				{
					index = random.Next(0, Paths.Count);

					if (Paths[index].Count == 0)
					{
						b = new Vector2(i, j);
					}
					else
					{
						do
						{
							b = Paths[index][random.Next(0, Paths[index].Count)];
							Paths[index].Remove(b);
						} while ((Vector2.Distance(entree, b) < 2f) && Paths[index].Count > 0);
						//Debug.Log("2-Creating passive" + ennemi_type);
						if (Paths[index].Count == 0)
						{ }
					}

					CreateWatchman(new Vector2(i, j), b);
					Paths.Remove(Paths[index]);
				}
				else if (carac == 33)
				{
					CreateHunter(new Vector2(i, j));
				}
			}
		}

		return map;
	}

	void createPaths()
	{
		Paths = new List<List<Vector2>>();
		int i = 0;
		//Debug.Log("sols = "+solPossible.Count);
		while (solPossible.Count > 0)
		{
			List<Vector2> path = new List<Vector2>();
			addToPath(solPossible[i], path);
			Paths.Add(path);
		}
	}

	void CreateZombie(Vector2 startPosition, Vector2 endPosition)
	{
		GameObject qqc;
		Debug.Log("Creating Zombie ");

		qqc = Instantiate(zombie, startPosition, Quaternion.identity);
		mapEnnemisList[(int)startPosition.x, (int)startPosition.y] = qqc;
		qqc.transform.SetParent(GameObject.Find("Map").transform, false);
		qqc.GetComponent<Ennemis>().waypoint1 = startPosition;
		qqc.GetComponent<Ennemis>().waypoint2 = endPosition;
		qqc.GetComponent<Ennemis>().currentTarget = startPosition;
	}

	void CreateWatchman(Vector2 startPosition, Vector2 endPosition)
	{
		GameObject qqc;
		Debug.Log("Creating Watchman");

		qqc = Instantiate(watchman, startPosition, Quaternion.identity);
		mapEnnemisList[(int)startPosition.x, (int)startPosition.y] = qqc;
		qqc.transform.SetParent(GameObject.Find("Map").transform, false);
		qqc.GetComponent<Ennemis>().waypoint1 = startPosition;
		qqc.GetComponent<Ennemis>().waypoint2 = endPosition;
		qqc.GetComponent<Ennemis>().currentTarget = startPosition;
	}

	void CreateExplorer(Vector2 startPosition)
	{
		GameObject qqc;
		Debug.Log("Creating Explorer");

		qqc = Instantiate(explorer, startPosition, Quaternion.identity);
		mapEnnemisList[(int)startPosition.x, (int)startPosition.y] = qqc;
		qqc.transform.SetParent(GameObject.Find("Map").transform, false);
		qqc.GetComponent<Ennemis>().currentTarget = startPosition;
	}

	void CreateHunter(Vector2 startPosition)
	{
		GameObject qqc;
		Debug.Log("Creating Hunter");

		qqc = Instantiate(hunter, startPosition, Quaternion.identity);
		mapEnnemisList[(int)startPosition.x, (int)startPosition.y] = qqc;
		qqc.transform.SetParent(GameObject.Find("Map").transform, false);
		qqc.GetComponent<Ennemis>().currentTarget = startPosition;
	}

	void addToPath(Vector2 position, List<Vector2> Path)
	{
		if (solPossible.Contains(position))
		{
			Path.Add(position);
			solPossible.Remove(position);
			int x = (int)position.x;
			int y = (int)position.y;
			Vector2 pt;
			if (map[x - 1, y] == 0)
			{
				try
				{
					pt = new Vector2(x - 1, y);
					addToPath(pt, Path);
				}
				catch (IndexOutOfRangeException e)
				{
					Debug.LogWarning((x - 1) + " -- " + y);
				}

			}
			if (map[x + 1, y] == 0)
			{
				try
				{
					pt = new Vector2(x + 1, y);
					addToPath(pt, Path);
				}
				catch (IndexOutOfRangeException e)
				{
					Debug.LogWarning((x + 1) + " -- " + y);
				}
			}
			if (map[x, y - 1] == 0)
			{
				try
				{
					pt = new Vector2(x, y - 1);
					addToPath(pt, Path);
				}
				catch (IndexOutOfRangeException e)
				{
					Debug.LogWarning(x + " -- " + (y - 1));
				}
			}
			if (map[x, y + 1] == 0)
			{
				try
				{
					pt = new Vector2(x, y + 1);
					addToPath(pt, Path);
				}
				catch (IndexOutOfRangeException e)
				{
					Debug.LogWarning(x + " -- " + (y + 1));
				}
			}
		}
	}

	private Vector2 getVector(int x, int y)
	{
		bool found = false;
		int i;
		for (i = 0; i < solPossible.Count && !found; i++)
		{
			found = (solPossible[i].x == x && solPossible[i].y == y);
		}
		if (found)
			return solPossible[i - 1];
		else
			throw new IndexOutOfRangeException();
	}
}


