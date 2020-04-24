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

    public void OpenActionPanel()
    {
        if (GameData.activeActionPanel)
        {
            GameObject.Destroy(GameData.activeActionPanel);
        }
        if (GameData.selectedBuilding != this)
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

            if (building.maxNumberOfResidence > 0)
            {
                ownedByCity.residenceBuildings.Remove(this);
            }

            if (building.townCenter)
            {
                TownCenter townCenter = scriptGameObject.GetComponent<TownCenter>();
                foreach (var takenTile in townCenter.cityData.takenTiles)
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

                foreach (var unit in GameData.thisPlayer.units)
                {
                    if (unit.unit.name == "Settler")
                    {
                        return;
                    }
                }

                if (GameData.thisPlayer.cities.Count == 0)
                {
                    GameData.gameOverScreen.SetActive(true);
                }
            }

        }
    }
}
