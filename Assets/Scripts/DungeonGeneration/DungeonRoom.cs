using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    // this script is on the PREFAB_ROOM

    public bool RoomWasEntered = false;

    public int Width;
    public int Height;

    public GameObject GAMEOBJECT_PATHENTRY;
    public GameObject GAMEOBJECT_PATHEXIT;

    public GameObject PREFAB_PATH;

    public GameObject PREFAB_ROOMTILE;
    public GameObject PREFAB_PATHDOOR;

    public GameObject RoomEnterTrigger;

    // if IsStartingRoom then the EntryPath is closed!
    public bool IsStartingRoom = false;
    public Vector3Int PathExitPosition;

    public Vector3 triggerCenter;
    public bool RoomCleared = false;

    public Vector3 StartingPoint;
    public Vector3Int NextRoomInterfacePosition;
    public Vector3 ExitPathStart;

    public Vector3 PlayerTeleportSpot;

    public bool IsTreasure = false;
    public GameObject PREFAB_REWARD;

    public void Suicide()
    {
        GameManager.Instance.WaveManager.OnWaveStart -= CloseEntry;
        GameManager.Instance.WaveManager.OnWaveEnd -= OpenExit;
        Destroy(this.gameObject);
    }

    public void Init(int width, int height, bool startingRoom, Vector3 startingPoint, bool treasure)
    {
        IsTreasure = treasure;
        Width = width;
        Height = height;
        IsStartingRoom = startingRoom;
        StartingPoint = startingPoint;

        PlayerTeleportSpot = new Vector3((startingPoint.x + 2)*2, 2, (startingPoint.z + 2)*2);

        GetComponent<BoxCollider>().size = new Vector3(Width*2, 10, Height*2);
        GetComponent<BoxCollider>().center = new Vector3((Width * 2)/2, 4, (Height * 2)/2);

        CreateRoomAndExitPath();

        GameManager.Instance.WaveManager.OnWaveStart += CloseEntry;
        GameManager.Instance.WaveManager.OnWaveEnd += OpenExit;
    }

    public void TeleportBack()
    {
        if(RoomCleared == false)
        {
            Debug.Log("teleporting back!");
            GameManager.Instance.PlayerGameObject.GetComponent<Rigidbody>().position = PlayerTeleportSpot;
        }
    }

    private void CreateRoomAndExitPath()
    {
        GameObject exitPath = new GameObject();
        exitPath.transform.parent = this.transform;
        exitPath.name = "RoomExitPath";

        Vector3 pathStartingPoint = new Vector3(Random.Range(3, Width - 3), 0, Height-1);

        int segmentLength = 10;
        int segmentWidth = 3;

        NextRoomInterfacePosition = new Vector3Int(((int)(StartingPoint.x + pathStartingPoint.x + segmentLength)-1)*2, 0,((int)(StartingPoint.z + Height + segmentLength)-5)*2);
        ExitPathStart = new Vector3(((int)StartingPoint.x + Mathf.RoundToInt(pathStartingPoint.x))*2, 0, ((int)StartingPoint.z + Height-1)*2);

        var path = Instantiate(PREFAB_PATH, ExitPathStart, Quaternion.identity);
        path.transform.parent = this.transform;


        GAMEOBJECT_PATHENTRY = Instantiate(PREFAB_PATHDOOR, new Vector3(StartingPoint.x * 2, 0, (StartingPoint.z + 1) * 2), Quaternion.Euler(0, -90f, 0));
        GAMEOBJECT_PATHENTRY.transform.parent = this.transform;

        GAMEOBJECT_PATHENTRY.GetComponent<Animator>().Play("doorOpen");

        GAMEOBJECT_PATHEXIT = Instantiate(PREFAB_PATHDOOR, ExitPathStart, Quaternion.identity);
        GAMEOBJECT_PATHEXIT.transform.parent = this.transform;

        // create simple room
        for (int z = Mathf.RoundToInt(StartingPoint.z); z < StartingPoint.z + Height; z++)
        {
            for (int x = Mathf.RoundToInt(StartingPoint.x); x < StartingPoint.x + Width; x++)
            {
                // instantiate simple room ground at correct position
                var roomTile = Instantiate(PREFAB_ROOMTILE, new Vector3(x * 2, 0, z * 2), Quaternion.identity);
                roomTile.transform.parent = this.transform;

                // create walls // dont place walls at path entry/exit
                if (x == StartingPoint.x || x == StartingPoint.x + Width - 1 || z == StartingPoint.z || z == StartingPoint.z + Height - 1)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        var wallTile = Instantiate(PREFAB_ROOMTILE, new Vector3(x * 2, y * 2, z * 2), Quaternion.identity);
                        wallTile.transform.parent = this.transform;
                    }
                }
            }
        }
    }

    private void OpenExit()
    {
        RoomCleared = true;
        GAMEOBJECT_PATHEXIT.GetComponent<Animator>().Play("doorOpen");
    }

    private void CloseEntry()
    {
        RoomWasEntered = true;
        GAMEOBJECT_PATHENTRY.GetComponent<Animator>().Play("doorClose");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(StartingPoint*2, 2);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 1);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(ExitPathStart, 3);
    }

    public void EnterRoom()
    {
        if(RoomCleared == false && RoomWasEntered == false)
        {
            CloseEntry();
            TeleportBack();
            if (GameManager.Instance.WaveManager.IsWaveCompleted == true )
            {
                if(IsTreasure == false)
                {
                    GameManager.Instance.WaveManager.StartNextWave();
                }
                else
                {
                    // spawn rune reward

                    var centerOfRoom = new Vector3((StartingPoint.x + (Width / 2))*2, 2, (StartingPoint.z + (Height / 2))*2);

                    Instantiate(PREFAB_REWARD, centerOfRoom, Quaternion.identity);

                }
            }
        }

        if(IsStartingRoom == false && RoomCleared == false)
        {
            GameManager.Instance.DungeonRoomSpawner.DestroyLastRoom();
        }
        
    }

}
