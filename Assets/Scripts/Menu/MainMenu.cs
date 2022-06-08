using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviourPunCallbacks
{
    public void PlayGame()
    {
        if (PhotonNetwork.OfflineMode)
            PhotonNetwork.OfflineMode = false;
        PhotonNetwork.OfflineMode = true;
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.CreateRoom(null);
        PhotonNetwork.LoadLevel(2);
        SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
    }

    private void SceneManagerOnsceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.buildIndex == 2 && GameObject.FindGameObjectsWithTag("Player").Length < 1 && PhotonNetwork.OfflineMode)
        {
            Debug.Log("Coucou");
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
        }
    }


    public void GoToMultiplayer()
    {
        PhotonNetwork.OfflineMode = false;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
