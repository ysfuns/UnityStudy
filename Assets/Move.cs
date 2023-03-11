using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pitaya;

public class Move : MonoBehaviour
{
    [SerializeField] float Speed = 2f;

    private IPitayaClient _client;
    private bool _connected;
    private bool _requestSent;
    private string _roomid;

    struct ResponseCreateRoom
    {
        public int Command;
        public RoomItem Data;

    }


    public class RoomItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID;
        /// <summary>
        /// 
        /// </summary>
        public string RoomID;
        /// <summary>
        /// 房间三号
        /// </summary>
        public string RoomName;
        /// <summary>
        /// 
        /// </summary>
        public string Creator;
        /// <summary>
        /// 
        /// </summary>
        public int State;
        /// <summary>
        /// 
        /// </summary>
        public int PlayerNumber;

        public List<RoomPlayer> Players;
    }

    public class RoomPlayer
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public string PlayerID;
        /// <summary>
        /// 角色名字
        /// </summary>
        public string PlayerName;
        /// <summary>
        /// 角色状态
        /// </summary>
        public int State;
        /// <summary>
        /// 角色类型
        /// </summary>
        public int Role;
        /// <summary>
        /// 当前角色所在房间
        /// </summary>
        public string RoomID;
    }


    // Start is called before the first frame update
    void Start()
    {
        _client = new PitayaClient();



        _connected = false;
        _requestSent = false;

        _client.NetWorkStateChangedEvent += (ev, error) =>
        {
            if (ev == PitayaNetWorkState.Connected)
            {
                Debug.Log("Successfully connected!");
                _connected = true;
            }
            else if (ev == PitayaNetWorkState.FailToConnect)
            {
                Debug.Log("Failed to connect");
            }
        };


        _client.OnRoute("onRoomMessage", (data) =>
            {
                Debug.Log("Got request data: " + data);
                var createData = JsonUtility.FromJson<ResponseCreateRoom>(data);
                if (createData.Command == 2)
                {
                    //_roomid = createData.Data.RoomID;
                }
            }
        );


        _client.OnRoute("onGameMessage", (data) =>
        {
            Debug.Log("Got game data: " + data);
        }
);


        _client.Connect("127.0.0.1", 3255,
            new Dictionary<string, string>
                  {
                      {"oi", "mano"}
                  });

        Invoke("guestLogin", 2);
        Invoke("roomCreat", 3);
        Invoke("loadResComp", 5);



    }

    void guestLogin()
    {
        string str = "{ \"UUID\": \"xxx-sss-sswww\", \"PlayerName\": \"玩家1号\"}";
        _client.Request("Gate.guestlogin", str,
                (data) =>
                {
                    Debug.Log("Got request data: " + data);
                },
                (err) =>
                {
                    Debug.LogError("Got error: code = " + err.Code + ", msg = " + err.Msg);
                });
    }
    void roomCreat()
    {
        string str = "{ \"RoomName\": \"大家一起玩\"}";

        _client.Request("Room.create", str,
                (data) =>
                {
                    Debug.Log("Got request data: " + data);


                },
                (err) =>
                {
                    Debug.LogError("Got error: code = " + err.Code + ", msg = " + err.Msg);
                });
    }

    void gameBegin()
    {

    }
    void loadResComp()
    {
        string str = "{ \"Command\": 8}";
        _client.Request("Room.commodmessage", str,
        (data) =>
        {
            Debug.Log("Got request data: " + data);
        },
        (err) =>
        {
            Debug.LogError("Got error: code = " + err.Code + ", msg = " + err.Msg);
        });
    }

    // Update is called once per frame
    void Update()
    {
        //float X = Input.GetAxis("Horizontal");
        //float Z = Input.GetAxis("Vertical");
        //transform.Translate(X * Time.deltaTime * Speed, 0, Z * Time.deltaTime * Speed);

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * Speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * Speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * Speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * Speed);
        }
    }
}
