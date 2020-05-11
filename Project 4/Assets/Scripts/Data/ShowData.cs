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

        // Set camera position and size.

        int highestY = 0;

        foreach (var item in list)
        {
            if (item > highestY)
            {
                highestY = item;
            }
        }

        if (highestY > list.Length / 2)
        {
            camera.orthographicSize = highestY * 0.7f;
        }
        else
        {
            camera.orthographicSize = list.Length / 2 * 0.7f;
        }

        camera.transform.position = new Vector3(list.Length / 2f, highestY / 2f,-10);

        // sets the position for each point in the graf.

        for (int i = 0; i < list.Length; i++)
        {
            lr.SetPosition(i, new Vector3(i, list[i],0));
        }
    }
}
