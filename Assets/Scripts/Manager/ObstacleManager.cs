using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance;

    private Dictionary<Vector2, List<GameObject>> obstacles = new();

    private Camera mainCamera;
    public float roomWidth;
    public float roomHeight;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        roomHeight = mainCamera.orthographicSize * 2f;
        roomWidth = roomHeight * mainCamera.aspect;

        // ���� ���� �� ��� ��ֹ��� �ڵ����� ���
        AutoRegisterAllObstacles();

        // �ʱ⿡�� ��� ��ֹ��� ��Ȱ��ȭ
        SetAllObstaclesInactive();
    }

    private void AutoRegisterAllObstacles()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>(true); // ��Ȱ���� ����

        foreach (Obstacle obs in obstacles)
        {
            Vector2 roomPos = WorldToRoom(obs.transform.position);
            RegisterObstacle(obs.gameObject, roomPos);
        }
        Debug.Log($"�� {obstacles.Length}���� ��ֹ��� ��ϵǾ����ϴ�.");
    }
    //���� �߽� �������� �� ��ǥ ���
    private Vector2 WorldToRoom(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x / roomWidth);
        int y = Mathf.RoundToInt(worldPos.y / roomHeight);
        return new Vector2(x, y);
    }

    public void RegisterObstacle(GameObject obj, Vector2 roomPos)
    {
        if (!obstacles.ContainsKey(roomPos))
            obstacles[roomPos] = new List<GameObject>();

        obstacles[roomPos].Add(obj);
        Debug.Log($"��ֹ� '{obj.name}'�� �� {roomPos}�� ��ϵǾ����ϴ�.");
    }

    public void SetRoomActive(Vector2 currentRoom)
    {
        foreach (var pair in obstacles)
        {
            bool isActive = pair.Key == currentRoom;
            foreach (var obj in pair.Value)
            {
                obj.SetActive(isActive);
            }
        }
    }

    //��� ��ֹ� ��Ȱ��ȭ
    private void SetAllObstaclesInactive()
    {
        foreach (var pair in obstacles)
        {
            foreach (var obj in pair.Value)
            {
                if (obj != null)
                    obj.SetActive(false);
            }
        }
    }
}
