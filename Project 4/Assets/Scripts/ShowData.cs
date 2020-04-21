using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowData : MonoBehaviour
{
    SaveSystem.SavedData saveData;
    SaveSystem sSystem;
    LineRenderer lr;

    string playerName;
    float timePlayed;
    float previouslyChosen = 0;

    [SerializeField, Range(1,3)]int chosen = 1;



    // Start is called before the first frame update
    void Start()
    {
        sSystem = this.GetComponent<SaveSystem>();
        lr = this.GetComponent<LineRenderer>();

        saveData = sSystem.LoadData("TestSave");

        playerName = saveData.playerName;
        timePlayed = saveData.timePlayed;

        DisplayData(chosen);
    }

    private void Update()
    {
        if (chosen != previouslyChosen)
        {
            DisplayData(chosen);
        }
    }

    public void DisplayData(int number)
    {
        switch (number)
        {
            case 1:
                ShowSpecific(saveData.populationOverTime);
                break;
            case 2:
                ShowSpecific(saveData.unitsOverTime);
                break;
            case 3:
                ShowSpecific(saveData.citiesOverTime);
                break;
        }

        previouslyChosen = chosen;
    }

    void ShowSpecific(int[] list)
    {
        int timesRan = 0;
        int position = 0;
        timesRan = list.Length;
        lr.positionCount = list.Length + 1;
        foreach (var item in list)
        {
            float time = 0;
            if (timesRan <= 0)
            {
                time = 0;
            }
            else
            {
                time = timePlayed / timesRan;
            }
            print(item + " | " + time);
            timesRan--;
            position++;

            lr.SetPosition(position, new Vector3((time / (list.Length * 500)) * 9, (item / list.Length), 0));
        }
    }
}
