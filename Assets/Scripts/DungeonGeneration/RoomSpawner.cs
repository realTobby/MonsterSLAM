using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1000)]
public class RoomSpawner : MonoBehaviour
{
    public GameObject PREFAB_ROOM_BASE;

    public DungeonRoom CurrentRoom;

    public GameObject PREFAB_FLOORTILE;

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
        CurrentRoom.Init(Random.Range(15, 25), Random.Range(15, 25), true, new Vector3Int(0,0,0));
    }

    public void NewRoom()
    {
        if(GameManager.Instance.WaveManager.WaveCount > 0)
        {
            Debug.Log("New room Generated!");
            // GENERATE PATH AND AT THE END WILL BE THE NEXT CENTER POINT, GENERATE ROOM FROM THAT

            // somehow calculate that lol

            // generate path from last exit "node"
            // get back last position to create a new room

            Vector3Int startPos = CurrentRoom.GetComponent<DungeonRoom>().NextRoomInterfacePosition;
            Vector3 startPosClean = new Vector3(startPos.x / 2, 0, startPos.z / 2);

            CurrentRoom = Instantiate(PREFAB_ROOM_BASE, startPos, Quaternion.identity).GetComponent<DungeonRoom>();
            CurrentRoom.Init(Random.Range(15, 25), Random.Range(15, 25), false, startPosClean);
        }
        
    }


    



}
