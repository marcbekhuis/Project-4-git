using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ChangeAudioVolume : MonoBehaviour
{
    [SerializeField] private string volumeName;
    [SerializeField] private AudioMixer AudioMixer;

    private Slider slider;

    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();

        if (PlayerPrefs.HasKey(volumeName))
        {
            slider.value = PlayerPrefs.GetFloat(volumeName);
            AudioMixer.SetFloat(volumeName, PlayerPrefs.GetFloat(volumeName));
        }
        else
        {
            float value = 0;
            AudioMixer.GetFloat(volumeName, out value);
            PlayerPrefs.SetFloat(volumeName, value);
            slider.value = value;
        }

    }

    public void SetVolume()
    {
        PlayerPrefs.SetFloat(volumeName, slider.value);
        AudioMixer.SetFloat(volumeName, slider.value);
    }
}
