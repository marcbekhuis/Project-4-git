using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayMusic : MonoBehaviour
{
    [SerializeField] private bool playsInEveryScene = false;
    [SerializeField] private bool overRides = false;
    [SerializeField] private AudioClip[] songs;
    [SerializeField] [Tooltip("Should the songs be played random or in the order of the array.")] private bool randomSong = false;
    [SerializeField] [Tooltip("Should a new song start playing when the previous song is done.")] private bool loops = false;

    private AudioSource audioSource;
    private int songnumber = 0;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1)
        {
            if (overRides)
            {
                if (objs[0] == this)
                {
                    Destroy(objs[1]);
                }
                else
                {
                    Destroy(objs[0]);
                }
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        if(playsInEveryScene) DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (randomSong)
        {
            audioSource.PlayOneShot(songs[Random.Range(0, songs.Length)]);
        }
        else
        {
            audioSource.PlayOneShot(songs[songnumber]);
            songnumber++;
            if (songnumber == songs.Length) songnumber = 0;
        }

    }

    private void Update()
    {
        if (!audioSource.isPlaying && loops)
        {
            if (randomSong)
            {
                audioSource.PlayOneShot(songs[Random.Range(0, songs.Length)]);
            }
            else
            {
                audioSource.PlayOneShot(songs[songnumber]);
                songnumber++;
                if (songnumber == songs.Length) songnumber = 0;
            }
        }
    }
}
