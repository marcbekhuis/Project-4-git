using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StartUpSetVolume : MonoBehaviour
{
    [SerializeField] private string[] volumeNames;
    [SerializeField] private AudioMixer AudioMixer;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (var volumeName in volumeNames)
        {
            if (PlayerPrefs.HasKey(volumeName))
            {
                AudioMixer.SetFloat(volumeName, PlayerPrefs.GetFloat(volumeName));
            }
            else
            {
                float value = 0;
                AudioMixer.GetFloat(volumeName, out value);
                PlayerPrefs.SetFloat(volumeName, value);
            }
        }
    }
}
