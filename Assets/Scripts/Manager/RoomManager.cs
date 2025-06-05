using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ObstableManager에서 float값 2개 전달 받을려니깐 함수 2개만들어야함 그냥 룸을 구조체로 두자
public class RoomManager : MonoBehaviour
{
    private Camera mainCamera;
    public float roomWidth;  // 룸 가로 크기
    public float roomHeight; // 룸 세로 크기

    [Header("Current Room")]
    public Vector2 currentRoom;

    [Header("Player")]
    public Transform player;

    private void Awake()
    {
        //InitCamera
        mainCamera = Camera.main;
        //InitRoomSetting
        roomHeight = mainCamera.orthographicSize * 2f;
        roomWidth = roomHeight * mainCamera.aspect;
    }

    private void Start()
    {
        if (ObstacleManager.Instance != null)
        {
            ObstacleManager.Instance.SetRoomActive(currentRoom);
        }
    }

    private void SetCameraToRoom(Vector2 roomPosition)
    {
        Vector3 cameraPos = new Vector3(
            roomPosition.x * roomWidth,
            roomPosition.y * roomHeight,
            mainCamera.transform.position.z
        );

        mainCamera.transform.position = cameraPos;
    }

    private void Update()
    {
        CheckRoomPosition();
    }

    //경계 기반 
    private void CheckRoomPosition()
    {
        Vector3 playerPos = player.position;
        Vector3 cameraPos = mainCamera.transform.position;

        // 화면의 실제 경계 계산
        float leftBoundary = cameraPos.x - (roomWidth * 0.5f);
        float rightBoundary = cameraPos.x + (roomWidth * 0.5f);
        float bottomBoundary = cameraPos.y - (roomHeight * 0.5f);
        float topBoundary = cameraPos.y + (roomHeight * 0.5f);

        Vector2 roomChange = Vector2.zero;

        // 경계 체크 및 룸 변경
        if (playerPos.x < leftBoundary)
            roomChange.x = -1;
        else if (playerPos.x > rightBoundary)
            roomChange.x = 1;

        if (playerPos.y < bottomBoundary)
            roomChange.y = -1;
        else if (playerPos.y > topBoundary)
            roomChange.y = 1;

        if (roomChange != Vector2.zero)
        {
            // 대각선 이동 방지: 더 큰 변화량을 우선
            if (Mathf.Abs(roomChange.x) > Mathf.Abs(roomChange.y))
                roomChange.y = 0;
            else if (Mathf.Abs(roomChange.y) > Mathf.Abs(roomChange.x))
                roomChange.x = 0;

            TransitionToRoom(currentRoom + roomChange);
        }
    }
    private void TransitionToRoom(Vector2 newRoom)
    {
        Vector2 previousRoom = currentRoom;
        currentRoom = newRoom;
        SetCameraToRoom(currentRoom);

        // ObstacleManager에게 방이 바뀌었음을 알림
        if (ObstacleManager.Instance != null)
        {
            ObstacleManager.Instance.SetRoomActive(currentRoom);
        }

        Debug.Log($"방이 {previousRoom}에서 {currentRoom}으로 변경되었습니다.");
    }
}

    //기존의 CheckRoom 함수 -> ViewPort기반의 플레이어가 카메라에 있는지 체크하는 함수
    //단점 -> 매프레임 마다 호출되고 viewport기반의 계산이 비쌈
    /*private void CheckRoomPosition()
    {
        // 플레이어의 월드 좌표(Transform.position)를 뷰포트 좌표로 변환합니다.
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(player.position);

        bool moved = false;
        Vector2 newRoom = currentRoom;

        if (viewportPoint.x < 0)
        {
            newRoom.x -= 1;
            moved = true;
        }
        else if (viewportPoint.x > 1)
        {
            newRoom.x += 1;
            moved = true;
        }

        if (viewportPoint.y < 0)
        {
            newRoom.y -= 1;
            moved = true;
        }
        else if (viewportPoint.y > 1)
        {
            newRoom.y += 1;
            moved = true;
        }

        if (moved)
        {
            currentRoom = newRoom;
            SetCameraToRoom(currentRoom);
        }
    }*/
