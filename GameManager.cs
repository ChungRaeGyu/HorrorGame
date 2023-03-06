using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print(PhotonNetwork.LocalPlayer.ActorNumber);
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
