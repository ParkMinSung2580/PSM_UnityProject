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

        // 게임 시작 시 모든 장애물을 자동으로 등록
        AutoRegisterAllObstacles();

        // 초기에는 모든 장애물을 비활성화
        SetAllObstaclesInactive();
    }

    private void AutoRegisterAllObstacles()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>(true); // 비활성도 포함

        foreach (Obstacle obs in obstacles)
        {
            Vector2 roomPos = WorldToRoom(obs.transform.position);
            RegisterObstacle(obs.gameObject, roomPos);
        }
        Debug.Log($"총 {obstacles.Length}개의 장애물이 등록되었습니다.");
    }
    //룸의 중심 기준으로 방 좌표 계산
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
        Debug.Log($"장애물 '{obj.name}'이 방 {roomPos}에 등록되었습니다.");
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

    //모든 장애물 비활성화
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
