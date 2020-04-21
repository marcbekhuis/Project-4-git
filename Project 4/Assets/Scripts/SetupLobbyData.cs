using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupLobbyData : MonoBehaviour
{
    [SerializeField] private InputField sessionName;
    [SerializeField] private InputField playerName;

    public void SetData()
    {
        LobbyData.playerName = playerName.text;
        LobbyData.sessionName = sessionName.text;
    }
}
