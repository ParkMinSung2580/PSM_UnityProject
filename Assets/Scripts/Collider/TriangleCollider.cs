using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCollider : MonoBehaviour
{
    /*[SerializeField] public Vector2[] trianglePoints;
    void OnEnable()
    {
        // Polygon Collider 2D 추가
        PolygonCollider2D polygonCollider = gameObject.AddComponent<PolygonCollider2D>(); 

         // 삼각형 점 배열 생성
        trianglePoints = new Vector2[3];
        trianglePoints[0] = new Vector2(0f, 0.5f);    // 위쪽
        trianglePoints[1] = new Vector2(-0.5f, -0.5f);  // 왼쪽 아래
        trianglePoints[2] = new Vector2(0.5f, -0.5f);   // 오른쪽 아래

        // 점들을 콜라이더에 적용
        polygonCollider.points = trianglePoints;
    }*/

    void Start()
    {
        // 1. SpriteRenderer 컴포넌트 가져오기
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // 2. SpriteRenderer와 Sprite가 있는지 확인
        if (spriteRenderer != null && spriteRenderer.sprite != null)
        {
            // 3. PolygonCollider2D 컴포넌트 가져오기 또는 추가하기
            PolygonCollider2D polyCollider = GetComponent<PolygonCollider2D>();
            if (polyCollider == null)
            {
                polyCollider = gameObject.AddComponent<PolygonCollider2D>();
            }

            // 4. 스프라이트의 Physics Shape 가져오기
            // GetPhysicsShape는 List<Vector2>를 인자로 받습니다.
            // 스프라이트가 여러 개의 물리 경로를 가질 수 있으므로, 경로 인덱스를 지정합니다.
            // 대부분의 경우 Single Sprite Mode에서는 0번 경로를 사용합니다.
            List<Vector2> physicsShapePoints = new List<Vector2>();

            // 스프라이트의 physicsShapeCount 속성을 확인하여 모든 경로를 가져올 수도 있습니다.
            // 예를 들어, Sprite Mode가 Multiple일 경우 여러 경로가 있을 수 있습니다.
            int pathCount = spriteRenderer.sprite.GetPhysicsShapeCount();

            // 기존의 모든 경로를 지웁니다 (선택 사항, 필요에 따라)
            polyCollider.pathCount = pathCount;

            for (int i = 0; i < pathCount; i++)
            {
                physicsShapePoints.Clear(); // 경로를 가져오기 전에 리스트를 비웁니다.
                spriteRenderer.sprite.GetPhysicsShape(i, physicsShapePoints);

                // 가져온 포인트를 PolygonCollider2D에 적용합니다.
                polyCollider.SetPath(i, physicsShapePoints.ToArray());
            }

            Debug.Log("Polygon Collider 2D가 스프라이트의 Physics Shape에 따라 자동으로 생성되었습니다.");
        }
        else
        {
            Debug.LogError("SpriteRenderer 또는 Sprite가 존재하지 않습니다. Polygon Collider 2D를 자동으로 생성할 수 없습니다.");
        }
    }
}
