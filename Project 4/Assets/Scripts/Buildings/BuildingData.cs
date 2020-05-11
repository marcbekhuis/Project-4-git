using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingData
{
    public BuildingData(BuildingPrefab building, Vector2Int gridPosition, PlayerData OwnedByPlayer, CityData OwnedByCity, GameObject ScriptGameObject = null)
    {
        this.building = building;
        this.scriptGameObject = ScriptGameObject;
        this.gridPosition = gridPosition;
        ownedByPlayer = OwnedByPlayer;
        ownedByCity = OwnedByCity;
        health = building.maxHealth;
    }

    public BuildingPrefab building;
    public GameObject scriptGameObject;
    public Vector2Int gridPosition;
    public PlayerData ownedByPlayer;
    public CityData ownedByCity;
    public float health;

    public void OpenActionPanel() // Opens or closes the ui action panel
    {
        if (GameData.activeActionPanel) // Destroys the active action panel
        {
            GameObject.Destroy(GameData.activeActionPanel);
        }
        if (GameData.selectedBuilding != this) // Opens the action panel if the previous action panel wasn't this one.
        {
            GameObject spawnedPanel = GameObject.Instantiate(building.actionPanel, GameData.canvas.transform);

            foreach (var spawnUnit in spawnedPanel.GetComponentsInChildren<SpawnUnit>())
            {
                spawnUnit.buildingData = this;
            }

            GameData.activeActionPanel = spawnedPanel;
            GameData.selectedBuilding = this;
            GameData.selectedUnit = null;
        }
        else
        {
            GameData.selectedBuilding = null;
            GameData.selectedUnit = null;
        }
    }

    public void UpdateHealth(float amount)
    {
        health += amount;

        if (health <= 0)
        {
            GameData.buildings[gridPosition.x, gridPosition.y] = null;
            GameData.buildingTilemap.SetTile((Vector3Int)gridPosition, null);
            ownedByPlayer.buildings.Remove(this);

            if (building.maxNumberOfResidence > 0) // removes the building from the residence list if it increased the max population.
            {
                ownedByCity.residenceBuildings.Remove(this);
            }

            if (building.townCenter) // removes all the town data and removes any building thats owned by the town.
            {
                TownCenter townCenter = scriptGameObject.GetComponent<TownCenter>();
                foreach (var takenTile in townCenter.cityData.takenTiles) // removes any building owned by the city.
                {
                    GameData.tiles[(int)takenTile.x, (int)takenTile.y].ownedByCity = null;
                    GameData.tiles[(int)takenTile.x, (int)takenTile.y].ownedByPlayer = null;

                    if (GameData.buildings[(int)takenTile.x, (int)takenTile.y] != null)
                    {
                        if (GameData.buildings[(int)takenTile.x, (int)takenTile.y].scriptGameObject)
                        {
                            GameObject.Destroy(GameData.buildings[(int)takenTile.x, (int)takenTile.y].scriptGameObject);
                        }

                        GameData.thisPlayer.buildings.Remove(GameData.buildings[(int)takenTile.x, (int)takenTile.y]);
                        GameData.buildings[(int)takenTile.x, (int)takenTile.y] = null;
                    }

                    Vector2Int position = new Vector2Int((int)takenTile.x, (int)takenTile.y);

                    GameData.buildingTilemap.SetTile((Vector3Int)position, null);
                    GameData.borderTilemap.SetTile((Vector3Int)position, null);
                }

                GameData.thisPlayer.cities.Remove(townCenter.cityData);

                if (scriptGameObject)
                {
                    GameObject.Destroy(scriptGameObject);
                }

                GameData.fogOfWar.UpdateVisibility();

                foreach (var unit in GameData.thisPlayer.units) // check if the player has a settler to see if he/she can continue playing.
                {
                    if (unit.unit.name == "Settler")
                    {
                        return;
                    }
                }

                if (GameData.thisPlayer.cities.Count == 0) // checks if the player has any cities left so he/she can continue playing.
                {
                    GameData.gameOverScreen.SetActive(true);
                }
            }

        }
    }
}
