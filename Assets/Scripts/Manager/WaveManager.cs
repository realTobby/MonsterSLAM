using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public GameObject PREFAB_SPAWN_VFX;
    public GameObject PREFAB_MONSTER;
    public GameObject PREFAB_MONSTER_PORTAL;

    public GameObject UI_NEXT_WAVE_BUTTON;
    public TMPro.TextMeshProUGUI UI_WAVECOUNT;

    public int WaveCount = 1;
    public int CurrentMonsterCount = 0;

    public bool IsWaveCompleted = false;


    public System.Action OnWaveStart;
    public System.Action OnWaveEnd;

    private void ShowWaveControls()
    {
        UI_NEXT_WAVE_BUTTON.SetActive(true);
    }

    private void HideWaveControls()
    {
        UI_NEXT_WAVE_BUTTON.SetActive(false);
    }

    public void StartNextWave()
    {
        KilledAllMonsters = false;
        IsWaveCompleted = false;
        HideWaveControls();
        OnWaveStart?.Invoke();
        PrepareAndSpawnWave();       
    }

    private void PrepareAndSpawnWave()
    {
        for(int i = 0; i < WaveCount+1; i++)
        {
            var pos = RandomPositionInsideArena();
            Instantiate(PREFAB_SPAWN_VFX, pos, Quaternion.identity);
            var monster = Instantiate(PREFAB_MONSTER, pos, Quaternion.identity);
            monster.GetComponent<Monster>().InitMonster(Mathf.FloorToInt(WaveCount / 2) > 0 ? Mathf.FloorToInt(WaveCount / 2) : 1);
            monster.transform.tag = "SpawnedMonster";
        }
        WaveCount++;
    }

    public Vector3 RandomPositionInsideArena(DungeonRoom room)
    {
        Vector3 arenaCenter = room.StartingPoint;
        int roomW = room.Width;
        int roomH = room.Height;

        Vector3 pos = new Vector3(Random.Range((arenaCenter.x + 2) * 2, (arenaCenter.x + roomW - 2) * 2), 1, Random.Range((arenaCenter.z + 2) * 2, (arenaCenter.z + roomH - 2) * 2));

        return pos;/*new Vector3(Random.Range(-14f, 12), 1, Random.Range(-16, 10));*/
    }

    public Vector3 RandomPositionInsideArena()
    {
        Vector3 arenaCenter = GameManager.Instance.DungeonRoomSpawner.CurrentRoom.GetComponent<DungeonRoom>().StartingPoint;
        int roomW = GameManager.Instance.DungeonRoomSpawner.CurrentRoom.GetComponent<DungeonRoom>().Width;
        int roomH = GameManager.Instance.DungeonRoomSpawner.CurrentRoom.GetComponent<DungeonRoom>().Height;

        Vector3 pos = new Vector3(Random.Range((arenaCenter.x+2)*2, (arenaCenter.x+roomW-2)*2),1, Random.Range((arenaCenter.z+2)*2, (arenaCenter.z + roomH - 2)*2));

        return pos;/*new Vector3(Random.Range(-14f, 12), 1, Random.Range(-16, 10));*/
    }

    public bool KilledAllMonsters = false;

    private void Update()
    {
        UI_WAVECOUNT.text = "Room: " + WaveCount;

        CurrentMonsterCount = GameObject.FindGameObjectsWithTag("SpawnedMonster").Length;

        if(IsWaveCompleted == false && CurrentMonsterCount == 0)
        {
            if(WaveCount > 0)
            {

                GameManager.Instance.DungeonRoomSpawner.CurrentRoom.RoomCleared = true;
                OnWaveEnd?.Invoke();
            }
            IsWaveCompleted = true;
            KilledAllMonsters = true;
        }


        //if(WaveCount > 25)
        //{
        //    Debug.Log("If you want you can continue to play but there wont be anything else to see... you probably saw already everything!");
        //    Debug.Log("Wow, you cleared 50 rooms! Honestly this must be boring at that point...Its so repetetive...If you found this message, then congratulations, you got the true ending!");
        //    SceneManager.LoadScene(3);
        //}

    }

}
