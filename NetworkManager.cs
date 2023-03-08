using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public InputField NickNameInput;
    public InputField RoomInput;            
    private void Awake()
    {
        PhotonNetwork.SendRate = 60; //Amount of data transmission(전송) and recived to and from the photon network
        PhotonNetwork.SerializationRate = 30; //
    }
    // Start is called before the first frame update
    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        print("Server connection completed");
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        JoinLobby();
        SceneManager.LoadScene("Lobby");
    }
    public void JoinLobby() => PhotonNetwork.JoinLobby();
    public override void OnJoinedLobby() => print("로비접속완료");

    public void CreateRoom() => PhotonNetwork.CreateRoom(RoomInput.text, new RoomOptions { MaxPlayers=10});
    //CreateRoom inclues "joinRoom"
    public override void OnCreatedRoom()
    {
        print("RoomMake Create");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("CreateRoom Failed");
    }
    public void JoinRoom() =>PhotonNetwork.JoinRoom(RoomInput.text);
    public override void OnJoinedRoom()
    {
        print("completed Joined Room");
        SceneManager.LoadScene("Room");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("Failed Joined Room");
    }
    public void StartGame()
    {
     SceneManager.LoadScene("Main");
     print(PhotonNetwork.LocalPlayer.ActorNumber+"내가 확인할 것");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
