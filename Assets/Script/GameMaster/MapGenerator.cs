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

	public float max_vitesse;
	public float max_puissance;
	public float max_poussee;
	public float max_godMode;

	private int[,] map = new int[13, 11];

	public int[,] FetchMap(int seed, int number)
	{
		this.seed = seed;
		this.number = number;
		return CreateMap();
	}

	private int[,] CreateMap()
	{
		System.Random random = new System.Random(seed * number);
		float difficulty = number / 3f;
		max_vitesse = difficulty + 1;
		max_puissance = difficulty;
		max_poussee = difficulty-1;
		max_godMode = difficulty-1;
		List<Vector2> entreSortiePossible = new List<Vector2>();
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
					entreSortiePossible.Add(new Vector2(i, j));
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
		int index,choix;
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
		index = random.Next(0, entreSortiePossible.Count);
		Vector2 entree = entreSortiePossible[index];
		entreSortiePossible.RemoveAt(index);
		map[(int)entree.x, (int)entree.y] = 21;

		// set sortie
		index = random.Next(0, entreSortiePossible.Count);
		Vector2 sortie = entreSortiePossible[index];
		entreSortiePossible.RemoveAt(index);
		bool sortieFound = (Vector2.Distance(entree, sortie) > 7);
		while (!sortieFound)
        {
			index = random.Next(0, entreSortiePossible.Count);
			sortie = entreSortiePossible[index];
			entreSortiePossible.RemoveAt(index);
			sortieFound = (Vector2.Distance(entree, sortie) > 7);
		}
		map[(int)sortie.x, (int)sortie.y] = 22;



		if(map[(int)entree.x - 1, (int)entree.y] != 20)
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

	private static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
	{
		float[,] noiseMap = new float[mapWidth, mapHeight];

		System.Random prng = new System.Random(seed);
		Vector2[] octaveOffsets = new Vector2[octaves];
		for (int i = 0; i < octaves; i++)
		{
			float offsetX = prng.Next(-100000, 100000) + offset.x;
			float offsetY = prng.Next(-100000, 100000) + offset.y;
			octaveOffsets[i] = new Vector2(offsetX, offsetY);
		}

		if (scale <= 0)
		{
			scale = 0.0001f;
		}

		float maxNoiseHeight = float.MinValue;
		float minNoiseHeight = float.MaxValue;

		float halfWidth = mapWidth / 2f;
		float halfHeight = mapHeight / 2f;


		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{

				float amplitude = 1;
				float frequency = 1;
				float noiseHeight = 0;

				for (int i = 0; i < octaves; i++)
				{
					float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
					float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

					float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
					noiseHeight += perlinValue * amplitude;

					amplitude *= persistance;
					frequency *= lacunarity;
				}

				if (noiseHeight > maxNoiseHeight)
				{
					maxNoiseHeight = noiseHeight;
				}
				else if (noiseHeight < minNoiseHeight)
				{
					minNoiseHeight = noiseHeight;
				}
				noiseMap[x, y] = noiseHeight;
			}
		}

		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{
				noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
			}
		}

		return noiseMap;
	}
}
