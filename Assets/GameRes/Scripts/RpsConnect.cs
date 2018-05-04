using Photon;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RpsConnect : PunBehaviour
{

    public InputField InputField;

    const string NickNamePlayerPrefsKey = "NickName";

    void Start()
    {
        InputField.text = PlayerPrefs.HasKey(NickNamePlayerPrefsKey) ? PlayerPrefs.GetString(NickNamePlayerPrefsKey) : "";
    }

    public void ApplyUserIdAndConnect()
    {
        string nickName = "DemoNick";
        if (this.InputField != null && !string.IsNullOrEmpty(this.InputField.text))
        {
            nickName = this.InputField.text;
            PlayerPrefs.SetString(NickNamePlayerPrefsKey, nickName);
        }

        if (PhotonNetwork.AuthValues == null)
        {
            PhotonNetwork.AuthValues = new AuthenticationValues();
        }

        PhotonNetwork.AuthValues.UserId = nickName;

        Debug.Log("Nickname: " + nickName, this);

        PhotonNetwork.playerName = nickName;
        PhotonNetwork.ConnectUsingSettings("0.5");

        // this way we can force timeouts by pausing the client (in editor)
        PhotonHandler.StopFallbackSendAckThread();
    }


    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedLobby()
    {
        OnConnectedToMaster(); // this way, it does not matter if we join a lobby or not
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("OnPhotonRandomJoinFailed");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2, PlayerTtl = 1 }, null);
        this.GetComponent<RpsCore>().Initial();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.room.Name);
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        Debug.Log("OnPhotonJoinRoomFailed");
    }
}
