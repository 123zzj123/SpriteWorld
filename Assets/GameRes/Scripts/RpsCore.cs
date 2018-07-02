using System;
using System.Collections;
using Photon;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class RpsCore : PunBehaviour, IPunTurnManagerCallbacks
{

    [SerializeField]
    private RectTransform ConnectUiView;

    [SerializeField]
    private RectTransform GameUiView;

    [SerializeField]
    private CanvasGroup ButtonCanvasGroup;

    [SerializeField]
    private RectTransform TimerFillImage;

    [SerializeField]
    private Text TimeText;

    [SerializeField]
    private Image RemotePet;

    [SerializeField]
    private Image LocalPet;

    [SerializeField]
    private Slider RemoteSlider;

    [SerializeField]
    private Slider LocalSlider;

    [SerializeField]
    private Text RemotePlayerText;

    [SerializeField]
    private Text LocalPlayerText;

    [SerializeField]
    private Image WinOrLossImage;

    [SerializeField]
    private Sprite[] sprite = new Sprite[5];

    [SerializeField]
    private Sprite SpriteWin;

    [SerializeField]
    private Sprite SpriteLose;

    [SerializeField]
    private RectTransform DisconnectedPanel;

    [SerializeField]
    private Text RemoteStatus;

    [SerializeField]
    private AudioClip win;

    [SerializeField]
    private AudioClip loss;


    private PunTurnManager turnManager;

    // keep track of when we show the results to handle game logic.
    private bool IsShowingResults;

    private bool InitialRemote = false;

    private int remotePetIndex = 0;
    private int localPetIndex = 0;

    private float remoteHp = 1.0f;
    private float localHp = 1.0f;

    public void Start()
    {
        this.turnManager = this.gameObject.AddComponent<PunTurnManager>();
        this.turnManager.TurnManagerListener = this;
        this.turnManager.TurnDuration = 5f;

        ButtonCanvasGroup.interactable = false;
        RefreshUIViews();
    }

    public void Update()
    {
        Vuforia.CameraDevice.Instance.Stop();
        Vuforia.CameraDevice.Instance.Deinit();
        // Check if we are out of context, which means we likely got back to the demo hub.
        if (this.DisconnectedPanel == null)
        {
            Destroy(this.gameObject);
        }

        if (!PhotonNetwork.inRoom)
        {
            return;
        }

        // disable the "reconnect panel" if PUN is connected or connecting
        if (PhotonNetwork.connected && this.DisconnectedPanel.gameObject.GetActive())
        {
            this.DisconnectedPanel.gameObject.SetActive(false);
        }
        if (!PhotonNetwork.connected && !PhotonNetwork.connecting && !this.DisconnectedPanel.gameObject.GetActive())
        {
            this.DisconnectedPanel.gameObject.SetActive(true);
        }


        if (PhotonNetwork.room.PlayerCount > 1)
        {
            LocalSlider.value = localHp;
            RemoteSlider.value = remoteHp;

            if (this.turnManager.IsOver)
            {
                return;
            }

            if (this.turnManager.Turn > 0 && this.TimeText != null && !IsShowingResults)
            {

                this.TimeText.text = this.turnManager.RemainingSecondsInTurn.ToString("F1") + " SECONDS";

                TimerFillImage.anchorMax = new Vector2(1f - this.turnManager.RemainingSecondsInTurn / this.turnManager.TurnDuration, 1f);
            }


        }

        // remote player's selection is only shown, when the turn is complete (finished by both)
        if (this.turnManager.IsCompletedByAll)
        {
            IsShowingResults = true;
        }
        else
        {
            //remote disconnect
            if (this.turnManager.Turn > 0 && PhotonNetwork.room.PlayerCount < 2)
            {
                this.RemotePet.color = new Color(1, 1, 1, 0.5f);
            }

        }

    }

    #region TurnManager Callbacks

    /// <summary>Called when a turn begins (Master Client set a new Turn number).</summary>
    public void OnTurnBegins(int turn)
    {
        Debug.Log("OnTurnBegins() turn: " + turn);

        this.WinOrLossImage.gameObject.SetActive(false);

        IsShowingResults = false;
        if(turn % 2 != 0)
        {
            if (PhotonNetwork.isMasterClient)
                ButtonCanvasGroup.interactable = true;
            else
                MakeTurn(-1);
        }
        else
        {
            if (PhotonNetwork.isMasterClient)
                MakeTurn(-1);
            else
                ButtonCanvasGroup.interactable = true;
        }
    }


    public void OnTurnCompleted(int obj)
    {
        Debug.Log("OnTurnCompleted: " + obj);
        this.CalculateWinAndLoss();
        if (!WinOrLossImage.gameObject.GetActive())
            this.OnEndTurn();
        else
            StartCoroutine("LeaveTheRoom");
    }


    // when a player moved (but did not finish the turn)
    public void OnPlayerMove(PhotonPlayer photonPlayer, int turn, object move)
    {
        //Debug.Log("OnPlayerMove: " + photonPlayer + " turn: " + turn + " action: " + move);
        //throw new NotImplementedException();
    }


    // when a player made the last/final move in a turn
    public void OnPlayerFinished(PhotonPlayer photonPlayer, int turn, object move)
    {
        Debug.Log("OnTurnFinished: " + photonPlayer + " turn: " + turn + " action: " + move);

        int skill = (int)(byte)move;
        if(skill == 255)
        {
            return;
        }
        if (skill != -1)
        {
            if (!photonPlayer.IsLocal)
            {
                localHp -= 0.1f;
            }
            else
            {
                remoteHp -= 0.1f;
            }
        }
    }



    public void OnTurnTimeEnds(int obj)
    {
        if (!IsShowingResults)
        {
            Debug.Log("OnTurnTimeEnds: Calling OnTurnCompleted");
            OnTurnCompleted(-1);
        }
    }

    #endregion

    #region Core Gameplay Methods


    /// <summary>Call to start the turn (only the Master Client will send this).</summary>
    public void StartTurn()
    {
        if (PhotonNetwork.isMasterClient)
        {
            this.turnManager.BeginTurn();
        }
    }

    public void MakeTurn(int selection)
    {
        this.turnManager.SendMove((byte)selection, true);
    }

    public void OnEndTurn()
    {
        this.StartCoroutine("ShowResultsBeginNextTurnCoroutine");
    }

    public IEnumerator ShowResultsBeginNextTurnCoroutine()
    {
        ButtonCanvasGroup.interactable = false;

        yield return new WaitForSeconds(1.0f);

        this.StartTurn();
    }

    private void CalculateWinAndLoss()
    {
        if(localHp <= 0)
        {
            WinOrLossImage.sprite = SpriteLose;
            WinOrLossImage.gameObject.SetActive(true);
            GameObject.Find("GamePanel").GetComponent<AudioSource>().clip = loss;
            GameObject.Find("GamePanel").GetComponent<AudioSource>().Play();
        }
        else if(remoteHp <= 0)
        {
            WinOrLossImage.sprite = SpriteWin;
            WinOrLossImage.gameObject.SetActive(true);
            GameObject.Find("GamePanel").GetComponent<AudioSource>().clip = win;
            GameObject.Find("GamePanel").GetComponent<AudioSource>().Play();
        }
    }

    #endregion


    #region Handling Of Buttons

    public void SelectSkill(int skill)
    {
        StartCoroutine(AudioManager.PlayMenuAudio("GameUIView"));
        this.MakeTurn(skill);
    }

    public void OnClickReConnectAndRejoin()
    {
        PhotonNetwork.ReconnectAndRejoin();
        PhotonHandler.StopFallbackSendAckThread();  // this is used in the demo to timeout in background!
    }

    #endregion

    public void ExitRoom()
    {
        StartCoroutine(AudioManager.LoadingNextScene("Canvas" ,"PetMenu"));
    }

    public IEnumerator LeaveTheRoom()
    {
        yield return new WaitForSeconds(1.0f);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
    }

    public void Initial()
    {
        remoteHp = 1.0f;
        localHp = 1.0f;
        WinOrLossImage.gameObject.SetActive(false);
        RemotePet.color = new Color(1, 1, 1, 1);
        RemotePet.sprite = null;
        remotePetIndex = 0;
        RemotePlayerText.text = "Name";
    }

    void RefreshUIViews()
    {
        TimerFillImage.anchorMax = new Vector2(0f, 1f);

        ConnectUiView.gameObject.SetActive(!PhotonNetwork.inRoom);
        GameUiView.gameObject.SetActive(PhotonNetwork.inRoom);
    }


    public override void OnLeftRoom()
    {
        //Debug.Log("OnLeftRoom()");

        RefreshUIViews();
    }

    public override void OnJoinedRoom()
    {
        this.localPetIndex = SSDirector.CurrentPet;
        this.LocalPet.sprite = sprite[localPetIndex - 1];
        this.LocalPlayerText.text = PhotonNetwork.playerName;

        RefreshUIViews();

        if (PhotonNetwork.room.PlayerCount == 2)
        {
            RemoteStatus.text = "";
            if (!PhotonNetwork.isMasterClient)
            {
                this.photonView.RPC("IniRemote", PhotonTargets.Others, localPetIndex);
            }

            if (this.turnManager.Turn == 0)
            {
                // when the room has two players, start the first turn (later on, joining players won't trigger a turn)
                this.StartTurn();
            }
        }
        else
        {
            Debug.Log("Waiting for another player");
        }
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debug.Log("Other player arrived");
        RemoteStatus.text = "";
        this.photonView.RPC("IniRemote", PhotonTargets.Others, localPetIndex);

        if (PhotonNetwork.room.PlayerCount == 2)
        {
            PhotonNetwork.room.EmptyRoomTtl = -1;
            PhotonNetwork.room.IsOpen = false;
            PhotonNetwork.room.PlayerTtl = 1;
            this.StartTurn();
        }
    }


    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        Debug.Log("Other player disconnected! " + otherPlayer.ToStringFull());
        RemoteStatus.text = "Other player disconnected!";
        remoteHp = 0;
        this.CalculateWinAndLoss();
        StartCoroutine("LeaveTheRoom");
    }


    public override void OnConnectionFail(DisconnectCause cause)
    {
        this.DisconnectedPanel.gameObject.SetActive(true);
    }

    [PunRPC]
    void IniRemote(int index)
    {
        this.remotePetIndex = index;
        this.RemotePet.sprite = sprite[remotePetIndex - 1];
        if(!PhotonNetwork.isMasterClient)
        this.RemotePlayerText.text = PhotonNetwork.masterClient.NickName;
        else
        {
            this.RemotePlayerText.text = PhotonNetwork.masterClient.GetNext().NickName;
        }
    }
}
