using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int MonsterCount = 0;

    public GameObject PREFAB_SPAWN_VFX;
    public GameObject PREFAB_MONSTER;
    public GameObject PREFAB_MONSTER_PORTAL;

    public GameObject UI_NEXT_WAVE_BUTTON;
    public TMPro.TextMeshProUGUI UI_WAVECOUNT;

    public int WaveCount = 1;

    public bool AutoStart = false;

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
        HideWaveControls();
        OnWaveStart?.Invoke();
        PrepareAndSpawnWave();       
    }

    IEnumerator SpawnMonsterWithDelay(Dictionary<int, Vector3> positions)
    {
        foreach (var m in positions)
        {
            var monster = Instantiate(PREFAB_MONSTER, m.Value, Quaternion.identity);
            monster.GetComponent<Monster>().InitMonster(Mathf.FloorToInt(WaveCount / 2) > 0 ? Mathf.FloorToInt(WaveCount / 2) : 1);
            monster.transform.tag = "SpawnedMonster";
            yield return new WaitForSeconds(.5f);
        }

        var portals = GameObject.FindGameObjectsWithTag("Portal");
        foreach(var item in portals.ToList())
        {
            Destroy(item);
        }
        IsWaveCompleted = false;
        WaveCount++;
    }

    private void PrepareAndSpawnWave()
    {
        Debug.Log("PrepareAndSpawnWave invoked");
        // 2-3 random portals
        // every portal spawns a number of enemies with a delay

        List<Vector3> SpawnPositions = new List<Vector3>();

        int portalCount = Random.Range(1, 4);

        for(int p = 0; p < portalCount; p++)
        {
            SpawnPositions.Add(RandomPositionInsideArena());
            Instantiate(PREFAB_MONSTER_PORTAL, SpawnPositions[p], Quaternion.identity);
        }

        Dictionary<int, Vector3> MonsterSpawnPositions = new Dictionary<int, Vector3>();

        for(int m = 0; m < WaveCount+1; m++)
        {
            Vector3 randomPortal = SpawnPositions[Random.Range(0, SpawnPositions.Count)];
            MonsterSpawnPositions.Add(m, randomPortal);
        }
        StartCoroutine(nameof(SpawnMonsterWithDelay), MonsterSpawnPositions);
    }

    private Vector3 RandomPositionInsideArena()
    {
        return new Vector3(Random.Range(-14f, 12), 1, Random.Range(-16, 10));
    }

    private void CheckForInput()
    {
        if(IsWaveCompleted == true && MonsterCount == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartNextWave();
            }
        }
    }

    private void Update()
    {
        UI_WAVECOUNT.text = "Wave: " + WaveCount;

        MonsterCount = GameObject.FindGameObjectsWithTag("SpawnedMonster").Length;

        CheckForInput();

        if(IsWaveCompleted == false && MonsterCount == 0)
        {
            IsWaveCompleted = true;

            if(AutoStart && GameManager.Instance.IsGamePaused == false)
            {
                StartNextWave();
            }
            else
            {
                ShowWaveControls();
                OnWaveEnd?.Invoke();
            }
        }
    }

}
