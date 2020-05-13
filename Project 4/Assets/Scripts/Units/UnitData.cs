using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitData
{
    public UnitData(UnitPrefab unit, UnitMovement unitMovement, GameObject gameObject, Vector2Int gridPosition, PlayerData OwnedByPlayer, UnitCombat unitCombat)
    {
        this.unit = unit;
        this.unitMovement = unitMovement;
        this.gameObject = gameObject;
        this.gridPosition = gridPosition;
        ownedByPlayer = OwnedByPlayer;
        health = unit.maxHealth;
        this.unitCombat = unitCombat;
    }

    public UnitPrefab unit;
    public UnitMovement unitMovement;
    public UnitCombat unitCombat;
    public GameObject gameObject;
    public Vector2Int gridPosition;
    public PlayerData ownedByPlayer;
    public float health;

    public void OpenActionPanel() // Opens or closes the ui action panel
    {
        if (GameData.activeActionPanel) // Destroys the active action panel
        {
            GameObject.Destroy(GameData.activeActionPanel);
        }
        if (GameData.selectedUnit != this) // Opens the action panel if the previous action panel wasn't this one.
        {
            GameObject spawnedPanel = GameObject.Instantiate(unit.actionPanel, GameData.canvas.transform);
            GameData.activeActionPanel = spawnedPanel;
            GameData.selectedBuilding = null;
            GameData.selectedUnit = this;

            spawnedPanel.transform.Find("Move Unit").GetComponent<SelectUnitMoveToPos>().unitsData = this;
            foreach (var placeBuilding in spawnedPanel.GetComponentsInChildren<PlaceBuilding>())
            {
                placeBuilding.unit = this;
            }
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
            if (ownedByPlayer)
            {
                ownedByPlayer.units.Remove(this);
            }
            GameObject.Destroy(gameObject);
            GameData.units[gridPosition.x, gridPosition.y] = null;

            if (GameData.sfxUnitCombat)
            {
                if (GameData.unitDamage)
                {
                    if (!GameData.sfxUnitCombat.isPlaying) GameData.sfxUnitCombat.PlayOneShot(GameData.unitDamage);
                }
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
