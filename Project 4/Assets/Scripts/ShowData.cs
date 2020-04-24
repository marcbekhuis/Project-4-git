using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowData : MonoBehaviour
{
    SaveSystem.SavedData saveData;
    SaveSystem.SavedData[] saveDatas;
    SaveSystem sSystem;
    LineRenderer lr;

    [SerializeField] private Text playerText;
    [SerializeField] private Dropdown sessionDropdown;
    [SerializeField] private Dropdown typeDropdown;

    string playerName;
    float timePlayed;
    float previouslyChosen = 0;
    Camera camera;

    //[SerializeField, Range(1,3)]int chosen = 1;



    // Start is called before the first frame update
    void Start()
    {
        sSystem = this.GetComponent<SaveSystem>();
        lr = this.GetComponent<LineRenderer>();
        camera = Camera.main;

        saveDatas = sSystem.LoadAllData();

        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        foreach (var item in saveDatas)
        {
            options.Add(new Dropdown.OptionData(item.sessionName));
        }

        sessionDropdown.options = options;

        SetData();
    }

    public void SetData()
    {
        saveData = saveDatas[sessionDropdown.value];

        playerText.text = "Player name: " + saveData.playerName;

        playerName = saveData.playerName;
        timePlayed = saveData.timePlayed;

        DisplayData();
    }

    public void DisplayData()
    {
        switch (typeDropdown.value)
        {
            case 0:
                ShowSpecific(saveData.populationOverTime);
                break;
            case 1:
                ShowSpecific(saveData.unitsOverTime);
                break;
            case 2:
                ShowSpecific(saveData.citiesOverTime);
                break;
        }
    }

    void ShowSpecific(int[] list)
    {
        lr.SetVertexCount(list.Length);

        int highestValue = list.Length / 2;

        foreach (var item in list)
        {
            if (item > highestValue)
            {
                highestValue = item;
            }
        }

        camera.orthographicSize = highestValue * 0.7f;

        for (int i = 0; i < list.Length; i++)
        {
            lr.SetPosition(i, new Vector3(-camera.orthographicSize * 2 + 0.1f * camera.orthographicSize + i, -camera.orthographicSize + 0.1f * camera.orthographicSize + list[i],0));
        }

        //int timesRan = 0;
        //int position = 0;
        //timesRan = list.Length;
        //lr.positionCount = list.Length + 1;
        //foreach (var item in list)
        //{
        //    float time = 0;
        //    if (timesRan <= 0)
        //    {
        //        time = 0;
        //    }
        //    else
        //    {
        //        time = timePlayed / timesRan;
        //    }
        //    print(item + " | " + time);
        //    timesRan--;
        //    position++;

        //    lr.SetPosition(position, new Vector3((time / (list.Length * 500)) * 9, (item / list.Length), 0));
        //}
    }
}
