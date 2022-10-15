using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1000)]
public class RoomSpawner : MonoBehaviour
{
    public GameObject PREFAB_ROOM_BASE;

    public DungeonRoom CurrentRoom;
    public GameObject LastRoom;
    public GameObject RoomToDestroy;

    public GameObject PREFAB_FLOORTILE;

    public bool LastRoomWasTreasure = false;

    private void Awake()
    {
        GameManager.Instance.WaveManager.OnWaveEnd += NewRoom;
        // to start the game!
        StartingRoom();
    }

    private void StartingRoom()
    {
        // create path where player will stand, and the starting room the player has to go into
        Debug.Log("Starting room!");
        CurrentRoom = Instantiate(PREFAB_ROOM_BASE, new Vector3Int(0, 0, 0), Quaternion.identity).GetComponent<DungeonRoom>();
        CurrentRoom.Init(Random.Range(15, 25), Random.Range(15, 25), true, new Vector3Int(0,0,0), false);
    }

    public void DestroyLastRoom()
    {
        if(RoomToDestroy != null)
        {
            RoomToDestroy.GetComponent<DungeonRoom>().Suicide();
        }
    }


    public void NewRoom()
    {
        if(GameManager.Instance.WaveManager.WaveCount > 0)
        {
            Vector3Int startPos = CurrentRoom.GetComponent<DungeonRoom>().NextRoomInterfacePosition;
            Vector3 startPosClean = new Vector3(startPos.x / 2, 0, startPos.z / 2);

            bool treasureLuck = Random.Range(0, 100) > 65 ? true : false;

            if(treasureLuck)
            {
                if(LastRoomWasTreasure)
                {
                    treasureLuck = false;
                }
                else
                {
                    LastRoomWasTreasure = false;
                }
            }

            RoomToDestroy = LastRoom;
            LastRoom = CurrentRoom.gameObject;

            CurrentRoom = Instantiate(PREFAB_ROOM_BASE, startPos, Quaternion.identity).GetComponent<DungeonRoom>();
            CurrentRoom.Init(Random.Range(15, 25), Random.Range(15, 25), false, startPosClean, treasureLuck);
            LastRoomWasTreasure = treasureLuck;
        }
        
    }


    



}
