using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private Camera mainCamera;
    private float roomWidth;  // �� ���� ũ��
    private float roomHeight; // �� ���� ũ��

    [Header("Current Room")]
    public Vector2 currentRoom;

    [Header("Player")]
    public Transform player;

    private void Start()
    {
        //InitCamera
        mainCamera = Camera.main;
        //InitRoomSetting
        roomHeight = mainCamera.orthographicSize * 2f;
        roomWidth = roomHeight * mainCamera.aspect;
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

    //��� ��� 
    private void CheckRoomPosition()
    {
        Vector3 playerPos = player.position;
        Vector3 cameraPos = mainCamera.transform.position;

        // ȭ���� ���� ��� ���
        float leftBoundary = cameraPos.x - (roomWidth * 0.5f);
        float rightBoundary = cameraPos.x + (roomWidth * 0.5f);
        float bottomBoundary = cameraPos.y - (roomHeight * 0.5f);
        float topBoundary = cameraPos.y + (roomHeight * 0.5f);

        Vector2 roomChange = Vector2.zero;

        // ��� üũ �� �� ����
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
            // �밢�� �̵� ����: �� ū ��ȭ���� �켱
            if (Mathf.Abs(roomChange.x) > Mathf.Abs(roomChange.y))
                roomChange.y = 0;
            else if (Mathf.Abs(roomChange.y) > Mathf.Abs(roomChange.x))
                roomChange.x = 0;

            TransitionToRoom(currentRoom + roomChange);
        }
    }
    private void TransitionToRoom(Vector2 newRoom)
    {
        currentRoom = newRoom;
        SetCameraToRoom(currentRoom);
    }
}

    //������ CheckRoom �Լ� -> ViewPort����� �÷��̾ ī�޶� �ִ��� üũ�ϴ� �Լ�
    //���� -> �������� ���� ȣ��ǰ� viewport����� ����� ���
    /*private void CheckRoomPosition()
    {
        // �÷��̾��� ���� ��ǥ(Transform.position)�� ����Ʈ ��ǥ�� ��ȯ�մϴ�.
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
