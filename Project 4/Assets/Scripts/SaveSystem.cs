using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    string saveFolder;
    [SerializeField] private PlayerData playerData;

    void Awake()
    {
        saveFolder = Application.persistentDataPath + "/Saves/";
        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        SavedData savedData = new SavedData(
            playerData.PlayerName,
            Time.timeSinceLevelLoad,
            playerData.population,
            playerData.populationOverTime.ToArray(),
            playerData.units.Count,
            playerData.unitsOverTime.ToArray(),
            playerData.cities.Count,
            playerData.citiesOverTime.ToArray()
            ) ;

        string json = JsonUtility.ToJson(savedData);
        Debug.LogError(json);
        File.WriteAllText(saveFolder + LobbyData.sessionName + ".txt", Encryptor(json));
    }

    public SavedData LoadData(string saveName)
    {
        if (File.Exists(saveFolder + saveName + ".txt"))
        {
            string json = File.ReadAllText(saveFolder + saveName + ".txt");
            return JsonUtility.FromJson<SavedData>(Dencryptor(json));
        }
        else
        {
            return null;
        }
    }

    public void LoadDataTest(string saveName)
    {
        if (File.Exists(saveFolder + saveName + ".txt"))
        {
            string json = File.ReadAllText(saveFolder + saveName + ".txt");
            Debug.LogError(Dencryptor(json));
        }
    }

    public SavedData[] LoadAllData()
    {
        string[] fileNames = Directory.GetFiles(saveFolder);
        SavedData[] savedDatas = new SavedData[fileNames.Length];

        for (int i = 0; i < fileNames.Length; i++)
        {
            string json = File.ReadAllText(saveFolder + fileNames[i] + ".txt");
            savedDatas[i] = JsonUtility.FromJson<SavedData>(Dencryptor(json));
        }

        return savedDatas;
    }

    public class SavedData
    {
        public SavedData(string PlayerName, float TimePlayed, int EndPopulation, int[] PopulationOverTime, int EndUnitAmount, int[] UnitsOverTime, int EndCityAmount, int[] CitiesOverTime)
        {
            playerName = PlayerName;
            timePlayed = TimePlayed;

            endPopulation = EndPopulation;
            populationOverTime = PopulationOverTime;

            endUnitAmount = EndUnitAmount;
            unitsOverTime = UnitsOverTime;

            endCityAmount = EndCityAmount;
            citiesOverTime = CitiesOverTime;
        }

        public string playerName;
        public float timePlayed;

        public int endPopulation;
        public int[] populationOverTime;

        public int endUnitAmount;
        public int[] unitsOverTime;

        public int endCityAmount;
        public int[] citiesOverTime;
    }

    private string Encryptor(string json)
    {
        string result = "";

        for (int i = 0; i < json.Length; i++)
        {
            result += (char)(json[i] + json.Length - i);
        }

        return result;
    }

    private string Dencryptor(string json)
    {
        string result = "";

        for (int i = 0; i < json.Length; i++)
        {
            result += (char)(json[i] - json.Length + i);
        }

        return result;
    }
}
