using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{
    //public static Launcher Instance;
    
    [SerializeField] private TMP_InputField createRoomNameInputField;
    [SerializeField] private TMP_InputField joinRoomNameInputField;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private TMP_Text roomNameText;

    /*private void Awake()
    {
        Instance = this;
    }*/

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
}
