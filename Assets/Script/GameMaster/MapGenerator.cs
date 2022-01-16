using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	private int seed;
	private int number;

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

	public int[,] FetchMap(int seed, int number)
	{
		this.seed = seed;
		this.number = number;
		return CreateMap();
	}

	private int[,] CreateMap()
	{
		random = new System.Random(seed * number);
		difficulty = number * 5;
		difficulty += (number % 5 == 0 && number!=1) ? 40 : 10;
		difficulty = Mathf.Min(400, difficulty);
		Debug.Log("Difficulty " + difficulty);
		List<Vector2> sortiePossible = new List<Vector2>();
		List <Vector2> bonusPossible = new List<Vector2>();
		float value;
		
		for (int i = 0; i < 13; i++)
		{
			for (int j = 0; j < 11; j++)
			{
				value = random.Next(0, 100)/100.0f;
				//Debug.Log(value);
				if (i == 0 || j == 0 || i == 12 || j == 10 || (i % 2 == 0 && j % 2 == 0))
				{
					map[i, j] = 20;
				}
				else if (value < 0.55)
				{
					// sol
					map[i, j] = 0;
					solPossible.Add(new Vector2(i, j));
					sortiePossible.Add(new Vector2(i, j));
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
		List<int> purchases = BuyBonus();
		foreach (int bonusType in purchases)
		{
			index = random.Next(0, bonusPossible.Count);
			bonus = bonusPossible[index];
			bonusPossible.RemoveAt(index);
			map[(int)bonus.x, (int)bonus.y] = bonusType;			
		}
		foreach(Vector2 mur in bonusPossible)
		{	//mur cassable
			map[(int)mur.x, (int)mur.y] = 10;
		}

		createPaths();
		//Set l'entrée
		index = random.Next(0, sortiePossible.Count);
		entree = sortiePossible[index];
		sortiePossible.RemoveAt(index);
		map[(int)entree.x, (int)entree.y] = 21;
		// set sortie
		index = random.Next(0, sortiePossible.Count);
		Vector2 sortie = sortiePossible[index];
		sortiePossible.RemoveAt(index);
		bool sortieFound = (Vector2.Distance(entree, sortie) > 7);
		while (!sortieFound)
        {
			index = random.Next(0, sortiePossible.Count);
			sortie = sortiePossible[index];
			sortiePossible.RemoveAt(index);
			sortieFound = (Vector2.Distance(entree, sortie) > 7);
		}
		map[(int)sortie.x, (int)sortie.y] = 22;


		// Assure que le joueur aura la place de ce déplacer en entourant la sortie de sol
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

		return map;
	}

    void createPaths()
    {
		Paths = new List<List<Vector2>>();
		int i = 0;
		//Debug.Log("sols = "+solPossible.Count);
		while (solPossible.Count>0)
        {
			List<Vector2> path = new List<Vector2>();
			addToPath(solPossible[i], path);
			Paths.Add(path);
		}
    }

	public GameObject[,] PlaceEnnemie()
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

		List<int> purchases = BuyEnnemis();
		int index,i ;
		GameObject qqc;
		Vector2 a, b;
		int ennemi_type=0;
		//Debug.Log("Creating Enemies "+max_zombie+" " + max_exploreur + " " + max_watchman + " " + max_hunter +"  "+ (Paths.Count > 0));
		
		while (purchases.Count>0 && Paths.Count>0)
        {
			i = 0;
			index = random.Next(0, Paths.Count);
			do
			{
				a = Paths[index][random.Next(0, Paths[index].Count)];
				Paths[index].Remove(a);
			} while (Vector2.Distance(entree, a) < 2f && Paths[index].Count > 0);
		
			//Debug.Log(Paths.Count);
			if (Vector2.Distance(entree, a) >= 2f)
			{
				do
				{
					ennemi_type = purchases[i];
					i++;
				} while (ennemi_type < 10 && Paths[index].Count == 0 && i < purchases.Count);
				//Debug.Log("Creating " + ennemi_type);
				if (ennemi_type < 20 && Paths[index].Count == 0 && i == purchases.Count)
                {
					//Debug.Log("Callceling " + ennemi_type);
				}
				else if (ennemi_type < 20)
                {
					//Debug.Log("1-Creating passive" + Paths[index].Count);
					if (Paths[index].Count == 0)
					{
						b = a;
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
					if (ennemi_type < 10)
                    {
						//Debug.Log("3-Creating passive" + ennemi_type);
						CreateZombie(a, b, ennemi_type);
						//Debug.Log("4-Creating passive" + ennemi_type);
						purchases.Remove(ennemi_type);
						//Debug.Log("5-Creating passive" + ennemi_type);
					}
                    else
                    {
						CreateWatchman(a, b, ennemi_type);
						purchases.Remove(ennemi_type);
					}
                }
                else
				{
					//Debug.Log("Creating active" + ennemi_type);
					if (ennemi_type < 30)
					{
						CreateExplorer(a, ennemi_type);
						purchases.Remove(ennemi_type);
					}
					else
					{
						CreateHunter(a, ennemi_type);
						purchases.Remove(ennemi_type);
					}
				}
			}
			//Debug.Log("1-Finished ");
			Paths.Remove(Paths[index]);
			//Debug.Log("2-Finished");
			if(purchases.Count > 0 && Paths.Count == 0)
				Debug.Log("Out of paths");
		}
		return mapEnnemisList;
	}

	void CreateZombie(Vector2 startPosition, Vector2 endPosition, int type)
    {
		GameObject qqc;
		Debug.Log("Creating Zombie "+type);

		qqc = Instantiate(zombie, startPosition, Quaternion.identity);
		mapEnnemisList[(int)startPosition.x, (int)startPosition.y] = qqc;
		qqc.transform.SetParent(GameObject.Find("Map").transform, false);
		qqc.GetComponent<Ennemis>().waypoint1 = startPosition;
		qqc.GetComponent<Ennemis>().waypoint2 = endPosition;
		qqc.GetComponent<Ennemis>().currentTarget = startPosition;
		if(type == 1)
        {
			qqc.GetComponent<Ennemis>().speed *= 1.5f;
        }
        else if(type == 2)
		{
			qqc.GetComponent<Ennemis>().life += 1;
		}
		else if(type == 3)
		{
			qqc.GetComponent<Ennemis>().speed *= 1.5f;
			qqc.GetComponent<Ennemis>().life += 1;
		}
	}

	void CreateWatchman(Vector2 startPosition, Vector2 endPosition, int type)
	{
		GameObject qqc;
		Debug.Log("Creating Watchman");

		qqc = Instantiate(watchman, startPosition, Quaternion.identity);
		mapEnnemisList[(int)startPosition.x, (int)startPosition.y] = qqc;
		qqc.transform.SetParent(GameObject.Find("Map").transform, false);
		qqc.GetComponent<Ennemis>().waypoint1 = startPosition;
		qqc.GetComponent<Ennemis>().waypoint2 = endPosition;
		qqc.GetComponent<Ennemis>().currentTarget = startPosition;
		if (type == 11)
		{
			qqc.GetComponent<Ennemis>().speed *= 1.5f;
		}
		else if (type == 12)
		{
			qqc.GetComponent<Ennemis>().life += 1;
		}
		else if (type == 13)
		{
			qqc.GetComponent<Ennemis>().speed *= 1.5f;
			qqc.GetComponent<Ennemis>().life += 1;
		}
	}

	void CreateExplorer(Vector2 startPosition, int type)
	{
		GameObject qqc;
		Debug.Log("Creating Explorer");

		qqc = Instantiate(explorer, startPosition, Quaternion.identity);
		mapEnnemisList[(int)startPosition.x, (int)startPosition.y] = qqc;
		qqc.transform.SetParent(GameObject.Find("Map").transform, false);
		qqc.GetComponent<Ennemis>().currentTarget = startPosition;
		if (type == 21)
		{
			qqc.GetComponent<Ennemis>().speed *= 1.5f;
		}
		else if (type == 22)
		{
			qqc.GetComponent<Ennemis>().life += 1;
		}
		else if (type == 23)
		{
			qqc.GetComponent<Ennemis>().speed *= 1.5f;
			qqc.GetComponent<Ennemis>().life += 1;
		}
	}

	void CreateHunter(Vector2 startPosition, int type)
	{
		GameObject qqc;
		Debug.Log("Creating Hunter");

		qqc = Instantiate(hunter, startPosition, Quaternion.identity);
		mapEnnemisList[(int)startPosition.x, (int)startPosition.y] = qqc;
		qqc.transform.SetParent(GameObject.Find("Map").transform, false);
		qqc.GetComponent<Ennemis>().currentTarget = startPosition;
		if (type == 31)
		{
			qqc.GetComponent<Ennemis>().speed *= 1.5f;
		}
		else if (type == 32)
		{
			qqc.GetComponent<Ennemis>().life += 1;
		}
		else if (type == 33)
		{
			qqc.GetComponent<Ennemis>().speed *= 1.5f;
			qqc.GetComponent<Ennemis>().life += 1;
		}
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

	List<int> BuyBonus()
    {
		List<ShopItem> shop = new List<ShopItem>();
		List<int> purchase = new List<int>();
		int difficultyPool = difficulty;
		int choise;
		int bonus_restant = 5;
		// bonus vitesse
		shop.Add(new ShopItem(12, 7));
		shop.Add(new ShopItem(121, 11)); // durée 1.5x
		shop.Add(new ShopItem(122, 16)); // durée 2x
				  
		// bonus new puissance
		shop.Add(new ShopItem(11, 20));
		shop.Add(new ShopItem(111, 30)); // durée 1.5x
		shop.Add(new ShopItem(112, 30)); // puissance + 2
				  
		// bonus new poussee
		shop.Add(new ShopItem(13, 20));
		shop.Add(new ShopItem(131, 25)); // durée 1.5x
		shop.Add(new ShopItem(132, 30)); // durée 2x
				  
		// bonus new godMode
		shop.Add(new ShopItem(14, 50));
		shop.Add(new ShopItem(141, 100)); // durée 1.5x
		shop.Add(new ShopItem(142, 200)); // durée 2x

		while(difficultyPool>0 && shop.Count > 0 && bonus_restant>0)
        {
			choise = random.Next(0, shop.Count);
			// purchase item is posible
			if(shop[choise].cost< difficultyPool)
            {
				purchase.Add(shop[choise].itemID);
				difficultyPool -= shop[choise].cost;
				bonus_restant--;
				// if the item just bought could not be bought again remove it from the shop
				if (shop[choise].cost > difficultyPool)
				{
					shop.Remove(shop[choise]);
				}
			}
			else // else remove it from the shop
			{
				shop.Remove(shop[choise]);
			}
			

		}

		return purchase;
	}

	List<int> BuyEnnemis()
	{
		List<ShopItem> shop = new List<ShopItem>();
		List<int> purchase = new List<int>();
		int difficultyPool = difficulty;
		int choise;
		int ennemis_restant = 4;
		// bonus zombie
		shop.Add(new ShopItem(0, 5));
		shop.Add(new ShopItem(1, 15)); // vitesse 1.5
		shop.Add(new ShopItem(2, 20)); // vie +1
		shop.Add(new ShopItem(3, 40)); // vie +1 & vitesse 1.5

		// bonus watchman
		shop.Add(new ShopItem(10, 20));
		shop.Add(new ShopItem(11, 35)); // vitesse 1.5
		shop.Add(new ShopItem(12, 40)); // vie +1
		shop.Add(new ShopItem(13, 80)); // vie +1 & vitesse 1.5

		// bonus explorer
		shop.Add(new ShopItem(20, 50));
		shop.Add(new ShopItem(21, 75)); // vitesse 1.5
		shop.Add(new ShopItem(22, 80)); // vie +1
		shop.Add(new ShopItem(23, 160)); // vie +1 & vitesse 1.5

		// bonus hunter
		shop.Add(new ShopItem(30, 100));
		shop.Add(new ShopItem(31, 150)); // vitesse 1.5
		shop.Add(new ShopItem(32, 175)); // vie +1
		shop.Add(new ShopItem(33, 200)); // vie +1 & vitesse 1.5

		while (difficultyPool > 0 && shop.Count > 0 && ennemis_restant > 0)
		{
			choise = random.Next(0, shop.Count);
			// purchase item is posible
			if (shop[choise].cost < difficultyPool)
			{
				purchase.Add(shop[choise].itemID);
				difficultyPool -= shop[choise].cost;
				ennemis_restant--;
				// if the item just bought could not be bought again remove it from the shop
				if (shop[choise].cost > difficultyPool)
				{
					shop.Remove(shop[choise]);
				}
			}
			else // else remove it from the shop
			{
				shop.Remove(shop[choise]);
			}
			
		}

		return purchase;
	}

	class ShopItem
    {
		public int itemID;
		public int cost;

		public ShopItem(int itemID,int cost)
        {
			this.cost = cost;
			this.itemID = itemID;
        }
	}
}


