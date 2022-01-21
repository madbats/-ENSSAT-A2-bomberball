using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapReader : MonoBehaviour
{
	private string createdMap;

	private int seed;
	private int number;
	public System.Random random;
	int difficulty;
	List<Vector2> solPossible = new List<Vector2>();
	List<List<Vector2>> Paths;
	List<Vector2> ennemis = new List<Vector2>();
	private int[,] map = new int[13, 11];

	public GameObject zombie;
	public GameObject explorer;
	public GameObject watchman;
	public GameObject hunter;
	Vector2 entree;
	public GameObject[,] mapEnnemisList;

	public int[,] FetchMap(int seed)
	{
		this.seed = seed;
		this.createdMap = PlayerPrefs.GetString("map");
		return CreateMap();
	}

	private int[,] CreateMap()
	{
		random = new System.Random(seed);

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

		for (int i = 0; i < 13; i++)
		{
			for (int j = 0; j < 11; j++)
			{
				carac = int.Parse("" + createdMap[2 * (13 * j + i)] + createdMap[2 * (13 * j + i) + 1]);
				if (carac == 30 || carac == 31 || carac == 32 || carac == 33)
				{
					map[i, j] = 0;
					solPossible.Add(new Vector2(i, j));
					ennemis.Add(new Vector2(i, j));
				}
				else
                {
					map[i, j] = carac;
					if(carac == 21)
                    {
						entree = new Vector2(i, j);
                    }
					else if(carac == 0)
                    {
						solPossible.Add(new Vector2(i, j));
					}
                }
			}
		}

		createPaths();

		int index = 0;
		Debug.Log(Paths.Count);

		for (int i = 0; i < 13; i++)
		{
			for (int j = 0; j < 11; j++)
			{
				carac = int.Parse("" + createdMap[2 * (13 * j + i)] + createdMap[2 * (13 * j + i) + 1]);
				if (carac == 30)
				{
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
					index++;
					Debug.Log(index);
				}
				else if (carac == 31)
				{
					CreateExplorer(new Vector2(i, j));
					index++;
					Debug.Log(index);
				}
				else if (carac == 32)
				{
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
					index++;
					Debug.Log(index);
				}
				else if (carac == 33)
				{
					CreateHunter(new Vector2(i, j));
					index++;
					Debug.Log(index);
				}
			}
		}

		return map;
	}

	void createPaths()
	{
		Paths = new List<List<Vector2>>();
		//Debug.Log("sols = "+solPossible.Count);
		for(int i = 0; i < ennemis.Count; i++)
		{
			List<Vector2> path = new List<Vector2>();
			addToPath(ennemis[i], path);
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
		qqc.GetComponent<EnnemisCreator>().waypoint1 = startPosition;
		qqc.GetComponent<EnnemisCreator>().waypoint2 = endPosition;
		qqc.GetComponent<EnnemisCreator>().currentTarget = startPosition;
	}

	void CreateWatchman(Vector2 startPosition, Vector2 endPosition)
	{
		GameObject qqc;
		Debug.Log("Creating Watchman");

		qqc = Instantiate(watchman, startPosition, Quaternion.identity);
		mapEnnemisList[(int)startPosition.x, (int)startPosition.y] = qqc;
		qqc.transform.SetParent(GameObject.Find("Map").transform, false);
		qqc.GetComponent<EnnemisCreator>().waypoint1 = startPosition;
		qqc.GetComponent<EnnemisCreator>().waypoint2 = endPosition;
		qqc.GetComponent<EnnemisCreator>().currentTarget = startPosition;
	}

	void CreateExplorer(Vector2 startPosition)
	{
		GameObject qqc;
		Debug.Log("Creating Explorer");

		qqc = Instantiate(explorer, startPosition, Quaternion.identity);
		mapEnnemisList[(int)startPosition.x, (int)startPosition.y] = qqc;
		qqc.transform.SetParent(GameObject.Find("Map").transform, false);
		qqc.GetComponent<EnnemisCreator>().currentTarget = startPosition;
	}

	void CreateHunter(Vector2 startPosition)
	{
		GameObject qqc;
		Debug.Log("Creating Hunter");

		qqc = Instantiate(hunter, startPosition, Quaternion.identity);
		mapEnnemisList[(int)startPosition.x, (int)startPosition.y] = qqc;
		qqc.transform.SetParent(GameObject.Find("Map").transform, false);
		qqc.GetComponent<EnnemisCreator>().currentTarget = startPosition;
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


