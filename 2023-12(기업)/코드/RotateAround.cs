using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    // 회전 중심이 될 스테이지 오브젝트
    public GameObject stage;

    // 마우스의 X 및 Y 이동에 대한 변수
    private float xRotateMove, yRotateMove;

    // 회전 속도 및 스크롤 속도
    public float rotateSpeed = 200.0f;
    public float scrollSpeed = 500.0f;

    void Update()
    {
        // 마우스 왼쪽 버튼이 눌렸을 때
        if (Input.GetMouseButton(0))
        {
            // 마우스의 X 및 Y 이동을 계산하고 시간 및 회전 속도에 곱함
            xRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;
            yRotateMove = Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;

            // 스테이지의 현재 위치를 가져옴
            Vector3 stagePosition = stage.transform.position;

            // X 및 Y 축 주위로 회전하여 카메라를 움직임
            transform.RotateAround(stagePosition, Vector3.right, -yRotateMove);
            transform.RotateAround(stagePosition, Vector3.up, xRotateMove);

            // 스테이지를 바라보도록 조정
            transform.LookAt(stagePosition);
        }
        else // 마우스 왼쪽 버튼이 눌리지 않았을 때
        {
            // 마우스 스크롤 휠 값을 가져옴
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

            // 카메라의 로컬 회전을 기반으로 전방으로 이동하여 줌 인/아웃 효과를 생성
            Vector3 cameraDirection = this.transform.localRotation * Vector3.forward;
            this.transform.position += cameraDirection * Time.deltaTime * scrollWheel * scrollSpeed;
        }
    }
}