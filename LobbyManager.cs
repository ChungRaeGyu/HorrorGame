using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LobbyManager : MonoBehaviour
{
    public GameObject NickName;
    GameObject Character;
    public GameObject[] CharacterList;
    // Start is called before the first frame update
    void Start()
    {
        NickName.GetComponent<Text>().text = PhotonNetwork.LocalPlayer.NickName;
        Character = Instantiate(CharacterList[0], transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
    }
    //push button
    public void CharacterCollect()
    {//EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text-- Button's 
        switch(EventSystem.current.currentSelectedGameObject.name){
            case "1": Destroy(GameObject.Find(Character.name));Character=Instantiate(CharacterList[0],transform.position,transform.rotation);
            break;
            case "2": Destroy(GameObject.Find(Character.name)); Character=Instantiate(CharacterList[1], transform.position, transform.rotation); break;
            case "3": Destroy(GameObject.Find(Character.name)); Character = Instantiate(CharacterList[2], transform.position, transform.rotation); break;
            case "4": Destroy(GameObject.Find(Character.name)); Character = Instantiate(CharacterList[3], transform.position, transform.rotation); break;
            case "5": Destroy(GameObject.Find(Character.name)); Character = Instantiate(CharacterList[4], transform.position, transform.rotation); break;
            case "6": Destroy(GameObject.Find(Character.name)); Character = Instantiate(CharacterList[5], transform.position, transform.rotation); break;
            case "7": Destroy(GameObject.Find(Character.name)); Character = Instantiate(CharacterList[6], transform.position, transform.rotation); break;
            case "8": Destroy(GameObject.Find(Character.name)); Character = Instantiate(CharacterList[7], transform.position, transform.rotation); break;
            default : Debug.Log(EventSystem.current.currentSelectedGameObject.name); break;
        }
    }
}
