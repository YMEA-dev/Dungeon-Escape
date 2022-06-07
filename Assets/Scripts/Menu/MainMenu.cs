using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //PhotonNetwork.OfflineMode = true;
        PhotonNetwork.LoadLevel(2);
    }

    public void GoToMultiplayer()
    {
        //PhotonNetwork.OfflineMode = false;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
