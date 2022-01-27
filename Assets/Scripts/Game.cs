using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
	public MergableItem DraggableObjectPrefab;
	public GridHandler MainGrid;
	public GridPatternSO GridPattern;
	
	private List<string> ActiveRecipes = new List<string>();
	
	private System.Random _random;
	[SerializeField] private int _difficulty = 1;

	private static Game _instance;

	public static Game Instance => _instance;
	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(gameObject);
		} else {
			_instance = this;
		}
		
		_random = new System.Random();

		
		Screen.fullScreen =
			false; // https://issuetracker.unity3d.com/issues/game-is-not-built-in-windowed-mode-when-changing-the-build-settings-from-exclusive-fullscreen

		// load all item definitions
		ItemUtils.InitializeMap();
	}

	private void Start()
	{
		ReloadLevel(_difficulty);
	}

	public void ReloadLevel(int difficulty = 1)
	{
		// clear the board
		var fullCells = MainGrid.GetFullCells.ToArray();
		for (int i = fullCells.Length - 1; i >= 0; i--)
			MainGrid.ClearCell(fullCells[i]);

		// choose new recipes
		ActiveRecipes.Clear();
		difficulty = Mathf.Max(difficulty, 1);
		
		if (difficulty > ItemUtils.RecipeMap.Count)
		{
			// we are choosing (difficulty) recipes over recipes specified in Grid Pattern scriptable object
			// therefore difficulty can't be larger than the number of recipes
			throw new Exception("number of recipes must be >= difficulty!");
		}
		
		// array of random indices for recipes. 
		int[] randIndices = new int[difficulty];
		for (int i = 0; i < randIndices.Length; i++)
		{
			randIndices[i] = -1;
		}
		
		for (int i = 0; i < difficulty; i++)
		{
			// choosing a random recipe. Ensures no repetition.
			int rand;
			do
			{
				rand = _random.Next(ItemUtils.RecipeMap.Count);
			} while (randIndices.Contains(rand));
			randIndices[i] = rand;

			var recipe = ItemUtils.RecipeMap.ElementAt(randIndices[i]).Key;

			ActiveRecipes.Add(recipe);
		}


		// populate the board
		var emptyCells = MainGrid.GetEmptyCells.ToArray();
		foreach (var cell in emptyCells)
		{
			if (_random.Next(0, 101) <= GridPattern.itemDensity) // item density
			{
				var chosenRecipe = ActiveRecipes[Random.Range(0, difficulty)]; // Random recipe from list of active recipes
				var ingredients = ItemUtils.RecipeMap[chosenRecipe].ToArray();
				var ingredient = ingredients[Random.Range(0, ingredients.Length)]; // Random ingredient
                var item = ItemUtils.ItemsMap[ingredient.NodeGUID];
                cell.SpawnItem(item);
			}
			
		}
	}
}
