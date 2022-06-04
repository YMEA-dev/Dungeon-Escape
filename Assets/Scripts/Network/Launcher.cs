using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks 
{
    public static Launcher Instance;
    
    [SerializeField] private TMP_InputField createRoomNameInputField;
    [SerializeField] private TMP_InputField joinRoomNameInputField;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private TMP_Text roomNameText;
    [SerializeField] private GameObject startGameButton;

    public Dictionary<int, GameObject> PlayersObject;

    private void Awake()
    {
        Instance = this;
        PlayersObject = new Dictionary<int, GameObject>();
    }

    void Start()
    {
        Debug.Log("Connection to server...");
        PhotonNetwork.ConnectUsingSettings();
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server.");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby.");
        MenuManager.Instance.OpenMenu("lobby");
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(createRoomNameInputField.text))
            return;
        PhotonNetwork.CreateRoom(createRoomNameInputField.text);
        MenuManager.Instance.OpenMenu("loading");
    }
    
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomNameInputField.text);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed : " + message;
        MenuManager.Instance.OpenMenu("error");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        Debug.Log("PhotonNetwork.CurrentRoom.Name");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed : " + message;
        MenuManager.Instance.OpenMenu("error");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("lobby");
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void QuitGame()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel(0);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Start();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PlayersObject.Remove(otherPlayer.ActorNumber);
    }
}
