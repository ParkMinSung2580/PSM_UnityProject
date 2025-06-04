using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCollider : MonoBehaviour
{
    /*[SerializeField] public Vector2[] trianglePoints;
    void OnEnable()
    {
        // Polygon Collider 2D �߰�
        PolygonCollider2D polygonCollider = gameObject.AddComponent<PolygonCollider2D>(); 

         // �ﰢ�� �� �迭 ����
        trianglePoints = new Vector2[3];
        trianglePoints[0] = new Vector2(0f, 0.5f);    // ����
        trianglePoints[1] = new Vector2(-0.5f, -0.5f);  // ���� �Ʒ�
        trianglePoints[2] = new Vector2(0.5f, -0.5f);   // ������ �Ʒ�

        // ������ �ݶ��̴��� ����
        polygonCollider.points = trianglePoints;
    }*/

    void Start()
    {
        // 1. SpriteRenderer ������Ʈ ��������
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // 2. SpriteRenderer�� Sprite�� �ִ��� Ȯ��
        if (spriteRenderer != null && spriteRenderer.sprite != null)
        {
            // 3. PolygonCollider2D ������Ʈ �������� �Ǵ� �߰��ϱ�
            PolygonCollider2D polyCollider = GetComponent<PolygonCollider2D>();
            if (polyCollider == null)
            {
                polyCollider = gameObject.AddComponent<PolygonCollider2D>();
            }

            // 4. ��������Ʈ�� Physics Shape ��������
            // GetPhysicsShape�� List<Vector2>�� ���ڷ� �޽��ϴ�.
            // ��������Ʈ�� ���� ���� ���� ��θ� ���� �� �����Ƿ�, ��� �ε����� �����մϴ�.
            // ��κ��� ��� Single Sprite Mode������ 0�� ��θ� ����մϴ�.
            List<Vector2> physicsShapePoints = new List<Vector2>();

            // ��������Ʈ�� physicsShapeCount �Ӽ��� Ȯ���Ͽ� ��� ��θ� ������ ���� �ֽ��ϴ�.
            // ���� ���, Sprite Mode�� Multiple�� ��� ���� ��ΰ� ���� �� �ֽ��ϴ�.
            int pathCount = spriteRenderer.sprite.GetPhysicsShapeCount();

            // ������ ��� ��θ� ����ϴ� (���� ����, �ʿ信 ����)
            polyCollider.pathCount = pathCount;

            for (int i = 0; i < pathCount; i++)
            {
                physicsShapePoints.Clear(); // ��θ� �������� ���� ����Ʈ�� ���ϴ�.
                spriteRenderer.sprite.GetPhysicsShape(i, physicsShapePoints);

                // ������ ����Ʈ�� PolygonCollider2D�� �����մϴ�.
                polyCollider.SetPath(i, physicsShapePoints.ToArray());
            }

            Debug.Log("Polygon Collider 2D�� ��������Ʈ�� Physics Shape�� ���� �ڵ����� �����Ǿ����ϴ�.");
        }
        else
        {
            Debug.LogError("SpriteRenderer �Ǵ� Sprite�� �������� �ʽ��ϴ�. Polygon Collider 2D�� �ڵ����� ������ �� �����ϴ�.");
        }
    }
}
