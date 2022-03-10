using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using Random = UnityEngine.Random;

public class PlayerManager : MonoBehaviour
{

    PhotonView PV;

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
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), new Vector3(randX, 4.7f, randZ), Quaternion.identity);
    }
}
