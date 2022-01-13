using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	public float scale;
	public int octaves;
	public float persistance;
	public float lacunarity;
	private int seed;
	private int number;

	public GameObject point;
	System.Random random;
	public float max_vitesse;
	public float max_puissance;
	public float max_poussee;
	public float max_godMode;
	float difficulty;
	List<Vector2> solPossible = new List<Vector2>();
	List<List<Vector2>> Paths;
	private int[,] map = new int[13, 11];

	public GameObject zombie;
	public GameObject explorer;
	public GameObject watchman;
	public GameObject hunter;
	public GameObject waypoint;

	public int[,] FetchMap(int seed, int number)
	{
		this.seed = seed;
		this.number = number;
		return CreateMap();
	}

	private int[,] CreateMap()
	{
		random = new System.Random(seed * number);
		difficulty = number / 3f;
		max_vitesse = difficulty + 1;
		max_puissance = difficulty;
		max_poussee = difficulty-1;
		max_godMode = difficulty-1;
		List<Vector2> bonusPossible = new List<Vector2>();
		Vector2 offset = new Vector2(13 * number, 11 * difficulty);
		//float[,] noiseMap = GenerateNoiseMap(13, 11, seed, scale, octaves, persistance, lacunarity, offset);
		float value;
		
		for (int i = 0; i < 13; i++)
		{
			for (int j = 0; j < 11; j++)
			{
				value = random.Next(0, 100)/100.0f;
				Debug.Log(value);
				if (i == 0 || j == 0 || i == 12 || j == 10 || (i % 2 == 0 && j % 2 == 0))
				{
					map[i, j] = 20;
				}
				else if (value < 0.5)
				{
					// sol
					map[i, j] = 0;
					solPossible.Add(new Vector2(i, j));
				}
				else {
					if (value < 0.6)
					{   // puissance 11
						bonusPossible.Add(new Vector2(i, j));
					}
					else if (value < 0.7 )
					{   // vitesse 12
						bonusPossible.Add(new Vector2(i, j));
					}
					else if (value < 0.8 )
					{
						// poussee 13
						bonusPossible.Add(new Vector2(i, j));
					}
					else if (value < 0.9)
					{   // godmode 14
						bonusPossible.Add(new Vector2(i, j));
					}
					else
					{
						//mur cassable
						map[i, j] = 10;
					}
				}
			}
		}
		// set bonus
		int index;
		Vector2 bonus;
		while (bonusPossible.Count>0)
		{
			index = random.Next(0, bonusPossible.Count);
			bonus = bonusPossible[index];
			bonusPossible.RemoveAt(index);
			if(max_vitesse >= 1)
            {
				map[(int)bonus.x, (int)bonus.y] = 12;
				max_vitesse--;
			}
            else if (max_puissance >= 1)
			{
				map[(int)bonus.x, (int)bonus.y] = 11;
				max_puissance--;
			}
			else if(max_poussee >= 1)
			{
				map[(int)bonus.x, (int)bonus.y] = 13;
				max_poussee--;
			}
			else if (max_godMode >= 1)
			{
				map[(int)bonus.x, (int)bonus.y] = 14;
				max_godMode--;
            }
            else
            {
				//mur cassable
				map[(int)bonus.x, (int)bonus.y] = 10;
			}
		}


		//Set l'entrée
		index = random.Next(0, solPossible.Count);
		Vector2 entree = solPossible[index];
		solPossible.RemoveAt(index);
		map[(int)entree.x, (int)entree.y] = 21;

		// set sortie
		index = random.Next(0, solPossible.Count);
		Vector2 sortie = solPossible[index];
		solPossible.RemoveAt(index);
		bool sortieFound = (Vector2.Distance(entree, sortie) > 7);
		while (!sortieFound)
        {
			index = random.Next(0, solPossible.Count);
			sortie = solPossible[index];
			solPossible.RemoveAt(index);
			sortieFound = (Vector2.Distance(entree, sortie) > 7);
		}
		map[(int)sortie.x, (int)sortie.y] = 22;
		createPaths();

		if (map[(int)entree.x - 1, (int)entree.y] != 20)
        {
			map[(int)entree.x - 1, (int)entree.y] = 0;
		}
		if (map[(int)entree.x + 1, (int)entree.y] != 20)
		{
			map[(int)entree.x + 1, (int)entree.y] = 0;
		}
		if (map[(int)entree.x, (int)entree.y - 1] != 20)
		{
			map[(int)entree.x, (int)entree.y - 1] = 0;
		}
		if (map[(int)entree.x, (int)entree.y + 1] != 20)
		{
			map[(int)entree.x, (int)entree.y + 1] = 0;
		}
		//diagonnal
		if (map[(int)entree.x + 1, (int)entree.y + 1] != 20)
		{
			map[(int)entree.x + 1, (int)entree.y + 1] = 0;
		}
		if (map[(int)entree.x + 1, (int)entree.y - 1] != 20)
		{
			map[(int)entree.x + 1, (int)entree.y - 1] = 0;
		}
		if (map[(int)entree.x - 1, (int)entree.y - 1] != 20)
		{
			map[(int)entree.x - 1, (int)entree.y - 1] = 0;
		}
		if (map[(int)entree.x - 1, (int)entree.y + 1] != 20)
		{
			map[(int)entree.x - 1, (int)entree.y + 1] = 0;
		}
		PlaceEnnemie();

		return map;
	}

    void createPaths()
    {
		Paths = new List<List<Vector2>>();
		int i = 0;
		while (solPossible.Count>0)
        {
			List<Vector2> path = new List<Vector2>();
			addToPath(solPossible[i], path);
			Paths.Add(path);
		}
    }

	public void PlaceEnnemie()
	{
		int[,] mapEnnemisList = new int[13, 11];
		float max_zombie = difficulty + 1;
		float max_exploreur = difficulty;
		float max_watchman = difficulty - 1;
		float max_hunter = difficulty - 1;

		int index ;
		int ennemie;
		GameObject qqc;
		Vector2 a, b;
		while((int)(max_zombie + max_exploreur + max_watchman + max_hunter) > 0 && Paths.Count>0)
        {
			index = random.Next(0, Paths.Count);
			if (Paths[index].Count > 1) ;
			ennemie = random.Next(0, (int)(max_zombie + max_exploreur + max_watchman + max_hunter));
            if (max_zombie >= 1)
            {
				a = Paths[index][random.Next(0, Paths[index].Count)];
				Paths[index].Remove(a);
				b = Paths[index][random.Next(0, Paths[index].Count)];
				Paths[index].Remove(b);

				qqc = Instantiate(zombie,a, Quaternion.identity);
				qqc.transform.SetParent(transform, false);
				qqc.GetComponent<Zombie>().waypoints = new Transform[2];
				GameObject w = Instantiate(waypoint, a, Quaternion.identity);
				qqc.GetComponent<Zombie>().waypoints[0] = w.transform;
				
				w = Instantiate(waypoint, b, Quaternion.identity);
				qqc.GetComponent<Zombie>().waypoints[1] = w.transform;
				
				qqc.GetComponent<Zombie>().InitPath();
			}
			else if (max_exploreur >= 1)
			{
				a = Paths[index][random.Next(0, Paths[index].Count)];
				Paths[index].Remove(a);
				b = Paths[index][random.Next(0, Paths[index].Count)];
				Paths[index].Remove(b);

				qqc = Instantiate(explorer, a, Quaternion.identity);
				qqc.transform.SetParent(transform, false);
				qqc.GetComponent<Explorer>().waypoints = new Transform[2];
				GameObject w = Instantiate(waypoint, a, Quaternion.identity);
				qqc.GetComponent<Explorer>().waypoints[0] = w.transform;

				w = Instantiate(waypoint, b, Quaternion.identity);
				qqc.GetComponent<Explorer>().waypoints[1] = w.transform;

				qqc.GetComponent<Explorer>().InitPath();
			}
			else if(max_watchman >= 1)
			{
				a = Paths[index][random.Next(0, Paths[index].Count)];
				Paths[index].Remove(a);
				b = Paths[index][random.Next(0, Paths[index].Count)];
				Paths[index].Remove(b);

				qqc = Instantiate(watchman, a, Quaternion.identity);
				qqc.transform.SetParent(transform, false);
				qqc.GetComponent<Watchman>().waypoints = new Transform[2];
				GameObject w = Instantiate(waypoint, a, Quaternion.identity);
				qqc.GetComponent<Watchman>().waypoints[0] = w.transform;

				w = Instantiate(waypoint, b, Quaternion.identity);
				qqc.GetComponent<Watchman>().waypoints[1] = w.transform;

				qqc.GetComponent<Watchman>().InitPath();
			}
			else if(max_hunter >= 1)
			{
				a = Paths[index][random.Next(0, Paths[index].Count)];
				Paths[index].Remove(a);
				b = Paths[index][random.Next(0, Paths[index].Count)];
				Paths[index].Remove(b);

				qqc = Instantiate(hunter, a, Quaternion.identity);
				qqc.transform.SetParent(transform, false);
				qqc.GetComponent<Hunter>().waypoints = new Transform[2];
				GameObject w = Instantiate(waypoint, a, Quaternion.identity);
				qqc.GetComponent<Hunter>().waypoints[0] = w.transform;

				w = Instantiate(waypoint, b, Quaternion.identity);
				qqc.GetComponent<Hunter>().waypoints[1] = w.transform;

				qqc.GetComponent<Hunter>().InitPath();
			}
			Paths.Remove(Paths[index]);
		}
		
	}


	void addToPath(Vector2 position, List<Vector2> Path) 
	{
        if (solPossible.Contains(position))
        {
			Path.Add(position);
			solPossible.Remove(position);
			Instantiate(point, position, Quaternion.identity,this.transform);
			int x = (int)position.x;
			int y = (int)position.y;
			Vector2 pt;
			if (map[x - 1, y] == 0)
			{
                try {
					pt = new Vector2(x - 1, y);
					addToPath(pt, Path);
				}catch(IndexOutOfRangeException e){
					Debug.LogWarning((x-1) + " -- " + y);
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
					pt = new Vector2(x , y-1);
					addToPath(pt, Path);
				}
				catch (IndexOutOfRangeException e)
				{
					Debug.LogWarning(x + " -- " + (y-1));
				}
			}
			if (map[x, y + 1] == 0)
			{
				try
				{
					pt = new Vector2(x, y+1);
					addToPath(pt, Path);
				}
				catch (IndexOutOfRangeException e)
				{
					Debug.LogWarning(x + " -- " + (y+1));
				}
			}
		}
	}

    private Vector2 getVector(int x, int y)
    {
		bool found=false;
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
