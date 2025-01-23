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

        // ���� ������ ���� ���� ���Ѻ��� ������ ���ο� ���� ����
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

        // x, y, z �� �࿡ ���� �ݶ��̴� ũ�� ��,��� ������ �ݶ��̴� �� �������� �����ϰ� ��ġ ���
        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);
        float posZ = basePosition.z + Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);
        return spawnPos;
    }

    // ���� �����Լ�
    private void MonsterSpawn()
    {
        // ������ ù��° ������
        GameObject selectedPrefab = prefabs[0];

        // ������ ��ġ ��������
        Vector3 spawnPos = GetRandomPosition();
        // ���õ� �������� ������ ��ġ�� ����
        GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);

        // ������ ������Ʈ�� ����Ʈ�� ����
        gameObjects.Add(instance);
    }
}
