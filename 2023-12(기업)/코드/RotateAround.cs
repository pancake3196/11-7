using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    // ȸ�� �߽��� �� �������� ������Ʈ
    public GameObject stage;

    // ���콺�� X �� Y �̵��� ���� ����
    private float xRotateMove, yRotateMove;

    // ȸ�� �ӵ� �� ��ũ�� �ӵ�
    public float rotateSpeed = 200.0f;
    public float scrollSpeed = 500.0f;

    void Update()
    {
        // ���콺 ���� ��ư�� ������ ��
        if (Input.GetMouseButton(0))
        {
            // ���콺�� X �� Y �̵��� ����ϰ� �ð� �� ȸ�� �ӵ��� ����
            xRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;
            yRotateMove = Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;

            // ���������� ���� ��ġ�� ������
            Vector3 stagePosition = stage.transform.position;

            // X �� Y �� ������ ȸ���Ͽ� ī�޶� ������
            transform.RotateAround(stagePosition, Vector3.right, -yRotateMove);
            transform.RotateAround(stagePosition, Vector3.up, xRotateMove);

            // ���������� �ٶ󺸵��� ����
            transform.LookAt(stagePosition);
        }
        else // ���콺 ���� ��ư�� ������ �ʾ��� ��
        {
            // ���콺 ��ũ�� �� ���� ������
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

            // ī�޶��� ���� ȸ���� ������� �������� �̵��Ͽ� �� ��/�ƿ� ȿ���� ����
            Vector3 cameraDirection = this.transform.localRotation * Vector3.forward;
            this.transform.position += cameraDirection * Time.deltaTime * scrollWheel * scrollSpeed;
        }
    }
}