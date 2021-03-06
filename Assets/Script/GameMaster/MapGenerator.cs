using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Créer proceduralement le niveau 
/// </summary>
public class MapGenerator : MonoBehaviour
{
	private int seed;
	private int number;

	public System.Random random;
	public int difficulty;
	List<Vector2> solPossible = new List<Vector2>();
	List<List<Vector2>> Paths;
	private int[,] map = new int[13, 11];
	public float frequenceDeMur;

	public GameObject zombie;
	public GameObject explorer;
	public GameObject watchman;
	public GameObject hunter;
	Vector2 entree;
	GameObject[,] mapEnnemisList;

	int nombreDeSol;
	List<int> solParPath;

	/// <summary>
	/// Créér une matice de symbole correcpondant au la carte de seed et number donnée
	/// </summary>
	/// <param name="seed">Seed de la nouvelle carte</param>
	/// <param name="number">Numéro de niveau</param>
	/// <returns>Matrice 13 11 de la nouvelle carte</returns>
	public int[,] FetchMap(int seed, int number)
	{
		this.seed = seed;
		this.number = number;
		random = new System.Random(seed * number);
		return CreateMap();
	}

	/// <summary>
	/// Créer la carte du jeu, place l'ensemple des murs, bonus, entrée et sortie de la carte.
	/// Les chemins couloir de la carte sont égamelement détecté pour la génération des ennemis.
	/// </summary>
	/// <returns>Matrice 13 11 de la nouvelle carte</returns>
	private int[,] CreateMap()
	{
		difficulty = number*3;
		difficulty += (number % 5 == 0) ? 40 : (number >1) ? 10:0;
		difficulty = Mathf.Min(400, difficulty);
		Debug.Log("Difficulty " + difficulty);
		List<Vector2> sortiePossible = new List<Vector2>();
		List <Vector2> bonusPossible = new List<Vector2>();
		float value;
		
		// Génération du labyrinth 
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
				else if (value < frequenceDeMur)
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
		// Achet des bonus é placer 
		List<int> purchases = BuyBonus();
		foreach (int bonusType in purchases)
		{ // place chaque bonus acheter 
			index = random.Next(0, bonusPossible.Count);
			bonus = bonusPossible[index];
			bonusPossible.RemoveAt(index);
			map[(int)bonus.x, (int)bonus.y] = bonusType;			
		}
		//Debug.Log("Bonus set ");
		// le reste des case deviennet des mur cassable
		foreach (Vector2 mur in bonusPossible)
		{	//mur cassable
			map[(int)mur.x, (int)mur.y] = 10;
		}

		//Debug.Log("Creation des paths ");
		// calcule les couloirs dans lesquel les ennemies peuvent voyager
		createPaths();

		//Debug.Log("Paths crée ");
		//Set l'entrée
		index = random.Next(0, sortiePossible.Count);
		entree = sortiePossible[index];
		sortiePossible.RemoveAt(index);
		map[(int)entree.x, (int)entree.y] = 21;

		//Debug.Log("Entree set ");
		// set sortie é une distance de 7 minimum
		index = random.Next(0, sortiePossible.Count);
		Vector2 sortie = sortiePossible[index];
		sortiePossible.RemoveAt(index);
		bool sortieFound = (Vector2.Distance(entree, sortie) > 5);
		while (!sortieFound && sortiePossible.Count>0)
        {
			index = random.Next(0, sortiePossible.Count);
			sortie = sortiePossible[index];
			sortiePossible.RemoveAt(index);
			sortieFound = (Vector2.Distance(entree, sortie) > 5);
		}
		map[(int)sortie.x, (int)sortie.y] = 22;

		//Debug.Log("Sortie set ");

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
		//Debug.Log("Cadre set ");

		return map;
	}

	/// <summary>
	/// Calcule les couloirs dans lesquel les ennemies peuvent voyager
	/// </summary>
	void createPaths()
    {
		nombreDeSol = 0;
		solParPath = new List<int>();
		Paths = new List<List<Vector2>>();
		//Debug.Log("sols = "+solPossible.Count);
		while (solPossible.Count>0)
        {  // Tantqu'il reste des paths possible on créer un nouveau path
			List<Vector2> path = new List<Vector2>();
			addToPath(solPossible[0], path);
			nombreDeSol += path.Count;
			solParPath.Add(path.Count);
			Paths.Add(path);
		}
    }

	/// <summary>
	/// Génére les ennemies dans la scénes
	/// </summary>
	/// <returns>Matrice des ennemie </returns>
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

		// Achet des ennemis é placer 
		List<int> purchases = BuyEnnemis();
		int index=0,i,j,sommePasse;
		Vector2 a, b;
		int ennemi_type;

		// Tant que l'on peut placer des ennemies
		while (purchases.Count>0 && Paths.Count>0)
        {
			i = 0;
			j = 0;
			sommePasse = 0;
			// Détermine le point de départ d'un ennemies, par précausion celui-ci ne peut pas ce trouver é moin de 2 case de l'entree
			index = random.Next(0, nombreDeSol);
			do
			{
				sommePasse += solParPath[j];
				j++;
			} while (index>sommePasse && j < solParPath.Count);
			j--;
			nombreDeSol -= solParPath[j];
			solParPath.RemoveAt(j);
			index = j;
			do
			{
				a = Paths[index][random.Next(0, Paths[index].Count)];
				Paths[index].Remove(a);
			} while (Vector2.Distance(entree, a) < 2f && Paths[index].Count > 0);
		
			if (Vector2.Distance(entree, a) >= 2f)
			{ // Si on é bien trouvé une entrée valide

				// Selection d'un ennemies 
				do
				{
					ennemi_type = purchases[i];
					i++;
				} while (ennemi_type < 10 && Paths[index].Count == 0 && i < purchases.Count);
				//Debug.Log("Creating " + ennemi_type);
				if (ennemi_type < 20 && Paths[index].Count == 0 && i == purchases.Count)
                {
				}
				else if (ennemi_type < 20)
                {
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

	/// <summary>
	/// Instancie un Zombie é la possition donnée qui se 
	/// </summary>
	/// <param name="startPosition">position initiale et premier waypoint</param>
	/// <param name="endPosition">deusiéme waypoint</param>
	/// <param name="type">variante de l'ennemie</param>
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

	/// <summary>
	/// Instancie un Watchman é la possition donnée qui se 
	/// </summary>
	/// <param name="startPosition">position initiale et premier waypoint</param>
	/// <param name="endPosition">deusiéme waypoint</param>
	/// <param name="type">variante de l'ennemie</param>
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

	/// <summary>
	/// Instancie un Explorer é la possition donnée qui se 
	/// </summary>
	/// <param name="startPosition">position initiale et premier waypoint</param>
	/// <param name="endPosition">deusiéme waypoint</param>
	/// <param name="type">variante de l'ennemie</param>
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

	/// <summary>
	/// Instancie un Hunter é la possition donnée qui se 
	/// </summary>
	/// <param name="startPosition">position initiale et premier waypoint</param>
	/// <param name="endPosition">deusiéme waypoint</param>
	/// <param name="type">variante de l'ennemie</param>
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

	/// <summary>
	/// Ajoute le sol é la position donnée é la path si possible et ajoute également c'est voisin
	/// </summary>
	/// <param name="position">possition du sol é rajouter</param>
	/// <param name="Path">la path é alimenter</param>
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
				pt = new Vector2(x - 1, y);
				addToPath(pt, Path);
			}
			if (map[x + 1, y] == 0)
			{
				pt = new Vector2(x + 1, y);
				addToPath(pt, Path);
			}
			if (map[x, y - 1] == 0)
			{
				pt = new Vector2(x , y-1);
				addToPath(pt, Path);
			}
			if (map[x, y + 1] == 0)
			{
				pt = new Vector2(x, y+1);
				addToPath(pt, Path);
			}
		}
	}

	/// <summary>
	/// Fournie une liste de Bonus é répartir sur la carte. La liste est formé en fonction de la difficulté
	/// </summary>
	/// <returns>liste de Bonus é répartir sur la carte</returns>
	List<int> BuyBonus()
    {
		List<ShopItem> shop = new List<ShopItem>();
		List<int> purchase = new List<int>();
		int difficultyPool = difficulty;
		int choise;
		int lastChoise=-1;
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
			if(shop[choise].cost< difficultyPool && (lastChoise != -1 || shop[choise].cost > (difficultyPool * .3f) || shop.Count < 2))
            {
				if (lastChoise != choise || shop.Count < 2)
				{
					lastChoise = choise;
					purchase.Add(shop[choise].itemID);
					difficultyPool -= shop[choise].cost;
					bonus_restant--;
					// if the item just bought could not be bought again remove it from the shop
					if (shop[choise].cost > difficultyPool)
					{
						shop.Remove(shop[choise]);
					}
				}
			}
			else // else remove it from the shop
			{
				shop.Remove(shop[choise]);
			}
			

		}

		return purchase;
	}

	/// <summary>
	/// Fournie une liste de Ennemis é répartir sur la carte. La liste est formé en fonction de la difficulté
	/// </summary>
	/// <returns>liste de Ennemis é répartir sur la carte</returns>
	List<int> BuyEnnemis()
	{
		List<ShopItem> shop = new List<ShopItem>();
		List<int> purchase = new List<int>();
		int difficultyPool = difficulty;
		int choise;
		int lastChoise = -1;
		int ennemis_restant = 4;
		// bonus zombie
		shop.Add(new ShopItem(0, 10));
		shop.Add(new ShopItem(1, 25)); // vitesse 1.5
		shop.Add(new ShopItem(2, 40)); // vie +1
		shop.Add(new ShopItem(3, 60)); // vie +1 & vitesse 1.5

		// bonus watchman
		shop.Add(new ShopItem(10, 20));
		shop.Add(new ShopItem(11, 35)); // vitesse 1.5
		shop.Add(new ShopItem(12, 50)); // vie +1
		shop.Add(new ShopItem(13, 70)); // vie +1 & vitesse 1.5
		
		// bonus explorer
		shop.Add(new ShopItem(20, 30));
		shop.Add(new ShopItem(21, 55)); // vitesse 1.5
		shop.Add(new ShopItem(22, 70)); // vie +1
		shop.Add(new ShopItem(23, 90)); // vie +1 & vitesse 1.5
		
		// bonus hunter
		shop.Add(new ShopItem(30, 60));
		shop.Add(new ShopItem(31, 85)); // vitesse 1.5
		shop.Add(new ShopItem(32, 100)); // vie +1
		shop.Add(new ShopItem(33, 120)); // vie +1 & vitesse 1.5

		while (difficultyPool > 0 && shop.Count > 0 && ennemis_restant > 0)
		{
			choise = random.Next(0, shop.Count);
			// purchase item is posible
			if (shop[choise].cost < difficultyPool && (lastChoise != -1 || shop[choise].cost > (difficultyPool * .3f) || shop.Count<2))
			{
				if (lastChoise != choise || shop.Count < 2 )
				{
					lastChoise = choise;
					purchase.Add(shop[choise].itemID);
					difficultyPool -= shop[choise].cost;
					ennemis_restant--;
					// if the item just bought could not be bought again remove it from the shop
					if (shop[choise].cost > difficultyPool)
					{
						shop.Remove(shop[choise]);
					}
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


