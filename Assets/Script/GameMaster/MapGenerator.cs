using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	private int seed;
	private int number;

	public System.Random random;
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
	Vector2 entree;

	public int[,] FetchMap(int seed, int number)
	{
		this.seed = seed;
		this.number = number;
		return CreateMap();
	}

	private int[,] CreateMap()
	{
		random = new System.Random(seed * number);
		difficulty = Mathf.Max((number / 5f) +0.5f,1);
		max_vitesse = Mathf.Max(0f, difficulty);
		max_puissance = Mathf.Max(0f, difficulty-0.5f);
		max_poussee = Mathf.Max(0f, difficulty -1f);
		max_godMode = Mathf.Max(0f, difficulty -2f);
		List<Vector2> bonusPossible = new List<Vector2>();
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
				else if (value < 0.55)
				{
					// sol
					map[i, j] = 0;
					solPossible.Add(new Vector2(i, j));
				}
				else 
				{
					bonusPossible.Add(new Vector2(i, j));
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
		entree = solPossible[index];
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

	public GameObject[,] PlaceEnnemie()
	{
		GameObject[,] mapEnnemisList = {
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
		float max_zombie = Mathf.Max(0f,difficulty);
		float max_exploreur = Mathf.Max(0f, difficulty-0.5f);
		float max_watchman = Mathf.Max(0f, difficulty - 1.5f);
		float max_hunter = Mathf.Max(0f, difficulty - 2.5f);

		int index ;
		//int ennemie;
		GameObject qqc;
		Vector2 a, b;
		//Debug.Log("Creating Enemies "+max_zombie+" " + max_exploreur + " " + max_watchman + " " + max_hunter +"  "+ (Paths.Count > 0));
		while ((int)(max_zombie + max_exploreur + max_watchman + max_hunter) > 0 && Paths.Count>0)
        {
			index = random.Next(0, Paths.Count);
			if (Paths[index].Count > 1)
			{
				a = Vector2.zero;
				b = Vector2.zero;
				// selectionne les waypoints des ennemies
				do
				{
					a = Paths[index][random.Next(0, Paths[index].Count)];
					Paths[index].Remove(a);
				} while (Vector2.Distance(entree, a) < 5f && Paths[index].Count> 2);

				if (Vector2.Distance(entree, a) >= 5f && Paths[index].Count > 0) {
					do
					{
						b = Paths[index][random.Next(0, Paths[index].Count)];
						Paths[index].Remove(b);
					} while ((Vector2.Distance(entree, b) < 4f || Vector2.Distance(a, b)<4f) && Paths[index].Count > 2);
				}

				if (b != Vector2.zero)
				{
					if (max_zombie >= 1)
					{
						Debug.Log("Creating Zombie");

						qqc = Instantiate(zombie, a, Quaternion.identity);
						mapEnnemisList[(int)a.x, (int)a.y] = qqc;
						qqc.transform.SetParent(GameObject.Find("Map").transform, false);
						qqc.GetComponent<Ennemis>().waypoint1 = a;
						qqc.GetComponent<Zombie>().waypoint2 = b;
						qqc.GetComponent<Zombie>().currentTarget = a;

						max_zombie--;
					}
					else if (max_exploreur >= 1)
					{
						Debug.Log("Creating Explorer");
						
						qqc = Instantiate(explorer, a, Quaternion.identity);
						mapEnnemisList[(int)a.x, (int)a.y] = qqc;
						qqc.transform.SetParent(GameObject.Find("Map").transform, false);
						qqc.GetComponent<Ennemis>().waypoint1 = a;
						qqc.GetComponent<Ennemis>().waypoint2 = b;
						qqc.GetComponent<Ennemis>().currentTarget = a;
						max_exploreur--;
					}
					else if (max_watchman >= 1)
					{
						Debug.Log("Creating Watchman");
						
						qqc = Instantiate(watchman, a, Quaternion.identity);
						mapEnnemisList[(int)a.x, (int)a.y] = qqc;
						qqc.transform.SetParent(GameObject.Find("Map").transform, false);
						qqc.GetComponent<Ennemis>().waypoint1 = a;
						qqc.GetComponent<Ennemis>().waypoint2 = b;
						qqc.GetComponent<Ennemis>().currentTarget = a;
						max_watchman--;
					}
					else if (max_hunter >= 1)
					{
						Debug.Log("Creating Hunter");
						
						qqc = Instantiate(hunter, a, Quaternion.identity);
						mapEnnemisList[(int)a.x, (int)a.y] = qqc;
						qqc.transform.SetParent(GameObject.Find("Map").transform, false);
						qqc.GetComponent<Ennemis>().waypoint1 = a;
						qqc.GetComponent<Ennemis>().waypoint2 = b;
						qqc.GetComponent<Ennemis>().currentTarget = a;
						max_hunter--;
					} 
				}
			}
			Paths.Remove(Paths[index]);
		}
		return mapEnnemisList;
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
