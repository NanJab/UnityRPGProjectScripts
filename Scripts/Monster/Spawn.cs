using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] prefabs;
    private BoxCollider area;

    public int enemySpawnLimit;
    private float createTime = -1f;
    public float coolTime;

    public List<GameObject> gameObjects = new List<GameObject>();

    void Start()
    {
        area = GetComponent<BoxCollider>();
        area.enabled = false;

        for (int i = 0; i < enemySpawnLimit; i++)
        {
            MonsterSpawn();
        }
    }

    void Update()
    {
        for(int i = 0; i < gameObjects.Count; i++)
        {
            if (gameObjects[i] == null)
            {
                gameObjects.RemoveAt(i);
            }
        }

        // 현재 생성된 몬스터 수가 제한보다 적으면 새로운 몬스터 생성
        if (gameObjects.Count < enemySpawnLimit && Time.time > createTime + coolTime)
        {
            createTime = Time.time;
            MonsterSpawn();
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 basePosition = transform.position;
        Vector3 size = area.size;

        // x, y, z 각 축에 대해 콜라이더 크기 좌,우로 나눠서 콜라이더 전 범위에서 랜덤하게 위치 계산
        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);
        float posZ = basePosition.z + Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);
        return spawnPos;
    }

    // 몬스터 생성함수
    private void MonsterSpawn()
    {
        // 몬스터의 첫번째 프리팹
        GameObject selectedPrefab = prefabs[0];

        // 랜덤한 위치 가져오기
        Vector3 spawnPos = GetRandomPosition();
        // 선택된 프리팹을 지정한 위치에 생성
        GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);

        // 생성된 오브젝트를 리스트에 저장
        gameObjects.Add(instance);
    }
}
