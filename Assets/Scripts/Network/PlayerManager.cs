using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerManager : MonoBehaviour
{

    PhotonView PV;
    public GameObject instanciatedGameObject;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    { 
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        int randX = Random.Range(70, 263);
        int randZ = Random.Range(-222, -33);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), 
                                                        new Vector3(randX, 4.7f, randZ), Quaternion.identity);
        //Faudrait penser à changer les coordonnées Y pour l'instanciation.
        Debug.Log(SceneManager.GetActiveScene().name);
        //Launcher.Instance.PlayersObject.Add(PV.Owner.ActorNumber, instanciatedGameObject);
        //PV.Owner.TagObject = instanciatedGameObject;
    }

    /*public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        info.Sender.TagObject = gameObject;
    }*/
}
