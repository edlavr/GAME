using GramGames.CraftingSystem.DataContainers;
using UnityEngine;

[CreateAssetMenu(fileName = "GridPattern", menuName = "GridUtils/GridPattern", order = 1)]

public class GridPatternSO : ScriptableObject
{
    [Tooltip("Chance of each GridCell spawning an item")]
    [Range(0, 100)] public int itemDensity;

    //[Tooltip("List of recipes")]
    public NodeContainer[] recipeRange;
}