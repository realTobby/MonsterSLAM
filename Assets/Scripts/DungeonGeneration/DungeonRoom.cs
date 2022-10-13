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

    public void Init(int width, int height, bool startingRoom, Vector3 startingPoint)
    {
        Width = width;
        Height = height;
        IsStartingRoom = startingRoom;
        StartingPoint = startingPoint;

        PlayerTeleportSpot = new Vector3((startingPoint.x + 2)*2, 1, (startingPoint.z + 2)*2);

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
            GameManager.Instance.PlayerGameObject.transform.position = PlayerTeleportSpot;
        }
    }

    private void CreateRoomAndExitPath()
    {

        // take random x POS at z-level Height
        // this will be the path startingPoint

        GameObject exitPath = new GameObject();
        exitPath.transform.parent = this.transform;
        exitPath.name = "RoomExitPath";

        Vector3 pathStartingPoint = new Vector3(Random.Range(3, Width - 3), 0, Height-1);

        int segmentLength = 10;
        int segmentWidth = 3;

       

        //// First PathSegment
        //for(int z = (int)StartingPoint.z + Height-1; z < StartingPoint.z + Height-1 + segmentLength; z++)
        //{
        //    for(int x = (int)StartingPoint.x + Mathf.RoundToInt(pathStartingPoint.x); x < StartingPoint.x + pathStartingPoint.x + segmentWidth; x++)
        //    {
        //        var roomTile = Instantiate(PREFAB_ROOMTILE, new Vector3(x * 2, 0, z * 2), Quaternion.identity);
        //        roomTile.transform.parent = exitPath.transform;
        //    }
        //}

        //// Second PathSegment
        //for(int z = ((int)StartingPoint.z + Height-1 + segmentLength) - segmentWidth; z < StartingPoint.z + Height-1 + segmentLength; z++)
        //{
        //    for(int x = (int)StartingPoint.x + Mathf.RoundToInt(pathStartingPoint.x); x < StartingPoint.x + pathStartingPoint.x + segmentLength; x++)
        //    {
        //        var roomTile = Instantiate(PREFAB_ROOMTILE, new Vector3(x * 2, 0, z * 2), Quaternion.identity);
        //        roomTile.transform.parent = exitPath.transform;
        //    }
        //}

        NextRoomInterfacePosition = new Vector3Int(((int)(StartingPoint.x + pathStartingPoint.x + segmentLength)-1)*2, 0,((int)(StartingPoint.z + Height + segmentLength)-5)*2);
        ExitPathStart = new Vector3(((int)StartingPoint.x + Mathf.RoundToInt(pathStartingPoint.x))*2, 0, ((int)StartingPoint.z + Height-1)*2);

        Instantiate(PREFAB_PATH, ExitPathStart, Quaternion.identity);

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


    //public void Init(int w, int h, bool startingRoom, Vector3Int centerPoint, int zEntry)
    //{
    //    roomCenter = centerPoint;
    //    triggerCenter = new Vector3(centerPoint.x + 5, centerPoint.y, centerPoint.z + 5);
    //    RoomEnterTrigger.transform.localPosition = triggerCenter;

    //    PathEntryZ = zEntry;
    //    IsStartingRoom = startingRoom;
    //    Width = w;
    //    Height = h;
    //    GenerateAndPlaceRoom();

    //    GameManager.Instance.WaveManager.OnWaveStart += CloseEntry;
    //    GameManager.Instance.WaveManager.OnWaveEnd += OpenExit;
    //}

    //private void GenerateAndPlaceRoom()
    //{
    //    // pathEntry ALWAYS HAS X 0
    //    // pathExit ALWAYS HAS Z = 0

    //    int pathExitX = Random.Range(1, Width - 2);


    //    for(int z = roomCenter.z; z < roomCenter.z + Height; z++)
    //    {
    //        for(int x = roomCenter.x; x < roomCenter.x + Width; x++)
    //        {
    //            var roomTile = Instantiate(PREFAB_ROOMTILE, new Vector3(x*2, 0, z*2), Quaternion.identity);
    //            roomTile.transform.parent = this.transform;

    //            if (x == roomCenter.x || z == roomCenter.z || x == roomCenter.x +Width - 1 || z == roomCenter.z+Height - 1)
    //            {

    //                // roomCenter.x*2, -6.25f, (roomCenter.z+2) * 2

    //                if((z == roomCenter.z+2 || z == roomCenter.z+3) && x == roomCenter.x)
    //                {

    //                }
    //                else if ((x == roomCenter.x + pathExitX || x == roomCenter.x + pathExitX + 1) && z == roomCenter.z + Height - 1)
    //                {
    //                    // let gap here
    //                    // spawn pathExit (path prefab) instead, but later

    //                }
    //                else
    //                {
    //                    // is a normal wall, place the wall please
    //                    for (int y = 1; y < 4; y++)
    //                    {
    //                        var wallTile = Instantiate(PREFAB_ROOMTILE, new Vector3(x*2, y*2, z*2), Quaternion.identity);
    //                        wallTile.transform.parent = this.transform;
    //                    }
    //                }

    //            }

    //        }
    //    }

    //    if (IsStartingRoom)
    //    {
    //        GAMEOBJECT_PATHENTRY = Instantiate(PREFAB_PATHDOOR, new Vector3(roomCenter.x * 2, -6f, (roomCenter.z + PathEntryZ) * 2), Quaternion.Euler(0, -90f, 0));
    //    }
    //    else
    //    {
    //        GAMEOBJECT_PATHENTRY = Instantiate(PREFAB_PATHDOOR, new Vector3(roomCenter.x * 2, -6.25f, (roomCenter.z + 2) * 2), Quaternion.Euler(0, -90f, 0));
    //    }
    //    GAMEOBJECT_PATHENTRY.transform.parent = this.transform;
    //    PathExitPosition = new Vector3Int(roomCenter.x + pathExitX, 0, roomCenter.z + Height);
    //    GAMEOBJECT_PATHEXIT = Instantiate(PREFAB_PATHDOOR, new Vector3((roomCenter.x + pathExitX) * 2, 1, (roomCenter.z + Height - 1) * 2), Quaternion.identity);
    //    GAMEOBJECT_PATHEXIT.transform.parent = this.transform;

    //}

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
            if (GameManager.Instance.WaveManager.IsWaveCompleted == true)
            {
                GameManager.Instance.WaveManager.StartNextWave();
            }
        }
        
    }

}
