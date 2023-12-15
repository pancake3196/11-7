using UnityEngine;

public class FollowMap : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public XyzScript xyzScript; // XyzScript 클래스의 인스턴스

    public Vector3 originalPosition; // 원래 위치
    public float returnDistance = 500f; // 돌아갈 거리

    void Start()
    {
        // XyzScript 클래스의 인스턴스를 참조
        xyzScript = FindObjectOfType<XyzScript>();
        originalPosition = transform.position; // 초기 위치 설정
    }

    void Update()
    {
        if (player != null && xyzScript != null)
        {
            // 플레이어의 방향을 기준으로 맵 방향 설정
            Vector3 playerDirection = player.forward;
            playerDirection.y = 0f; // Y축 방향은 고정
            playerDirection.Normalize();
            transform.forward = playerDirection;

            // XyzScript에서 가져온 gpsSpeed 값을 이용하여 맵 이동 속도 설정
            float mapSpeed = xyzScript.GetCurrentGpsSpeed();

            // fps 적용하여 이동
            float moveDistance = mapSpeed * Time.deltaTime;
            transform.Translate(Vector3.right * moveDistance);

            // 설정한 거리를 벗어났을 때 원래 위치로 돌아가기
            CheckDistanceAndReturn();
        }
    }

    void CheckDistanceAndReturn()
    {
        float distance = Vector3.Distance(transform.position, originalPosition);

        // 설정한 거리를 벗어났을 때 원래 위치로 돌아가기
        if (distance > returnDistance)
        {
            // 설정한 위치로 돌아가기
            transform.position = originalPosition;
        }
    }
}