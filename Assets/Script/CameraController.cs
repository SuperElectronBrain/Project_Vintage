using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public bool CameraCollisionMode = true;
	private	float CameraSpace = 0.2f;// ī�޶� �Ÿ� ������
	private	float CameraMaxDistance = 2.0f;// ī�޶� �ִ� �Ÿ�
	private	float CameraMinDistance = 0.0f;// ī�޶� �ּ� �Ÿ�
	private	float CameraDistance = 0.0f;// ī�޶� ���� �Ÿ�
	private	float CameraDistanceSave = 0.0f;// ī�޶� ���� �Ÿ� ���
	private	float CameraAngleX = 0.0f;// (ī�޶� <--> Ÿ��) ���� ����
	private	float CameraAngleY = 0.0f;// (ī�޶� <--> Ÿ��) ���� ����

	// ���� ���� �� ���� ȣ��
	private void Awake()
	{

	}

	//Ȱ��ȭ �� �� ���� ȣ��
	private void OnEnable()
	{

	}

	//�� ��°�� �� ���� ȣ��
	private void Start()
	{
		CameraDistance = CameraMaxDistance - CameraSpace;
		CameraDistanceSave = CameraDistance;
	}

	//�����Ӵ� �� �� ȣ��
	private void Update()
	{
		CameraKeyInput();
	}

	//0.02�ʸ��� �� �� ȣ��
	private void FixedUpdate()
	{
		ChangeTargetRotation();
		CameraMovement();
		CameraColliderCollision();
	}

	//�����Ӵ� �� �� ȣ��
	private void LateUpdate()
	{

	}

	//�ı��� �� ȣ��
	private void OnDestroy()
	{
	}

	//��Ȱ��ȭ �� �� ���� ȣ��
	private void OnDisable()
	{

	}

	private void CameraMovement()
	{
		this.gameObject.transform.position = new Vector3 // ī�޶� ��� �̵�
		//Vector3 TargetPosition = new Vector3 
			(
			GameManager.instance.TargetPosition.x + (Mathf.Sin(CameraAngleX * Mathf.Deg2Rad) * (Mathf.Cos(CameraAngleY * Mathf.Deg2Rad) * CameraDistance)),
			GameManager.instance.TargetPosition.y + (Mathf.Sin(CameraAngleY * Mathf.Deg2Rad) * CameraDistance),
			GameManager.instance.TargetPosition.z - (Mathf.Cos(CameraAngleX * Mathf.Deg2Rad) * (Mathf.Cos(CameraAngleY * Mathf.Deg2Rad) * CameraDistance))
			); //ī�޶� ���� �̵�

		//this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, TargetPosition, Time.fixedDeltaTime * 10.0f);

		this.gameObject.transform.rotation = Quaternion.Euler(CameraAngleY, -CameraAngleX, 0.0f);	
	}//ī�޶� ��ġ ����

	private void CameraKeyInput()
	{
		if(GameManager.instance.EscapeButtonState == false && GameManager.instance.LeftControlButtonState == false)
        {
			//ĳ���� - ī�޶� ����
			CameraDistance = CameraDistance + (-Input.GetAxis("Mouse ScrollWheel") * 2.0f);

			if (CameraDistance < CameraMinDistance)
			{
				CameraDistance = CameraMinDistance;
			}
			else if (CameraDistance > CameraMaxDistance - CameraSpace)
			{
				CameraDistance = CameraMaxDistance - CameraSpace;
			}

			if (Input.GetAxis("Mouse ScrollWheel") != 0)
			{
				CameraDistanceSave = CameraDistance;
			}

			//ĳ���� - ī�޶� ���� ����
			CameraAngleY = CameraAngleY + (-Input.GetAxis("Mouse Y") * 2.0f);

			if (CameraAngleY < -90.0f)
			{
				CameraAngleY = -90.0f;
			}
			else if (CameraAngleY > 90.0f)
			{
				CameraAngleY = 90.0f;
			}

			//ĳ���� - ī�޶� �¿� ����
			CameraAngleX = CameraAngleX + (-Input.GetAxis("Mouse X") * 2.0f);
			if (CameraAngleX > 180.0f)
			{
				CameraAngleX = -180.0f;
			}
			else if (CameraAngleX < -180.0f)
			{
				CameraAngleX = 180.0f;
			}
		}
		else if(GameManager.instance.LeftControlButtonState == true)
        {
			if(Input.GetMouseButtonDown(1) == true)
            {
				GameManager.instance.LeftControlButtonState = false;
			}
		}

		if(Input.GetKeyDown(KeyCode.F1) == true)
        {
			CameraDistanceSave = CameraMaxDistance - CameraSpace;
			CameraAngleX = 0.0f;
			CameraAngleY = 0.0f;
		}
	}//ī�޶� ����

	private void SetCameraColliderPosition()
	{
		//ĳ���� - ī�޶� �ݶ��̴� ��ġ ����
		Vector3 ColliderPosition = new Vector3
				(
				GameManager.instance.TargetPosition.x + (Mathf.Sin(CameraAngleX * Mathf.Deg2Rad) * ((Mathf.Cos(CameraAngleY * Mathf.Deg2Rad) * CameraDistance)) / 2),
				GameManager.instance.TargetPosition.y + (Mathf.Sin(CameraAngleY * Mathf.Deg2Rad) * (CameraDistance / 2)),
				GameManager.instance.TargetPosition.z - (Mathf.Cos(CameraAngleX * Mathf.Deg2Rad) * ((Mathf.Cos(CameraAngleY * Mathf.Deg2Rad) * CameraDistance)) / 2)
				);

		//float collider_scale_y = 1.0f;
		//this.gameObject.transform.GetChild(0).gameObject.transform.localPosition = new Vector3
		//	(
		//	this.gameObject.transform.GetChild(0).gameObject.transform.localPosition.x,
		//	collider_scale_y / 2,
		//	this.gameObject.transform.GetChild(0).gameObject.transform.localPosition.z
		//	);

		this.gameObject.transform.GetChild(0).gameObject.transform.localScale = new Vector3(0.1f, 0.1f, CameraDistance + 0.1f);

		this.gameObject.transform.GetChild(0).gameObject.transform.position = ColliderPosition;

		this.gameObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(CameraAngleY, -CameraAngleX, 0.0f);
	}//ĳ���� - ī�޶� �ݶ��̴� ����

	private void SetCameraRayPosition()
    {
		RaycastHit hit;

		Vector3 TemporaryPosition = new Vector3
		(
		this.gameObject.transform.position.x - GameManager.instance.TargetPosition.x,
		this.gameObject.transform.position.y - GameManager.instance.TargetPosition.y,
		this.gameObject.transform.position.z - GameManager.instance.TargetPosition.z
		);// ���̸� �߻��� ����

		if (Physics.Raycast(GameManager.instance.TargetPosition, TemporaryPosition, out hit, CameraMaxDistance))
		{
			if (hit.transform.gameObject.tag == "Structure" || hit.transform.gameObject.tag == "Block")
			{
				TemporaryPosition = new Vector3
					(
					hit.point.x - GameManager.instance.TargetPosition.x,
					hit.point.y - GameManager.instance.TargetPosition.y,
					hit.point.z - GameManager.instance.TargetPosition.z
					);//���̰� �浹�� ����� ��ġ

				if (((-TemporaryPosition.z) / Mathf.Cos(CameraAngleX * Mathf.Deg2Rad)) / Mathf.Cos(CameraAngleY * Mathf.Deg2Rad) < CameraDistanceSave + CameraSpace)
				{
					CameraDistance = (((-TemporaryPosition.z) / Mathf.Cos(CameraAngleX * Mathf.Deg2Rad)) / Mathf.Cos(CameraAngleY * Mathf.Deg2Rad)) - CameraSpace;
					//ī�޶��� ��ġ�� ���̰� �浹�� ������ ���� (CameraSpace�� ī�޶� ��ġ ������)
					if (CameraDistance < CameraSpace)
                    {
						CameraDistance = CameraSpace;// (ī�޶� <--> ī�޶� Ÿ��)�� �Ÿ��� CameraSpace ���̷� ����

						if (((-TemporaryPosition.z) / Mathf.Cos(CameraAngleX * Mathf.Deg2Rad)) / Mathf.Cos(CameraAngleY * Mathf.Deg2Rad) < CameraDistance)
                        {
							CameraDistance = (((-TemporaryPosition.z) / Mathf.Cos(CameraAngleX * Mathf.Deg2Rad)) / Mathf.Cos(CameraAngleY * Mathf.Deg2Rad));
							//ī�޶��� ��ġ�� ���̰� �浹�� ������ ����
						}// ���� ������ (ī�޶� <--> ī�޶� Ÿ��)�� �Ÿ��� (���̰� �浹�� �� <--> ī�޶� Ÿ��)���� �ִٸ�
					}// ���� ����� �Ÿ����� ������ �� ���� �۴ٸ�
				}// (���̰� �浹�� �� <--> ī�޶� Ÿ��)�� �Ÿ��� (ī�޶� <--> ī�޶� Ÿ��)�� �Ÿ� ���� ª�ٸ� (CameraSpace�� ī�޶� ��ġ ������)
			}// ���̿� ���� ��ü�� �ǹ��� ���̶��
		}// ī�޶� ���߰� �ִ� Ÿ���� ���������� ī�޶� ��ġ�� ���⿡ CameraMaxDistance ������ ���̸� �߻�
		else
		{
			CameraDistance = CameraDistanceSave; // ī�޶� ����ġ ������
		}// ���̿� �浹�� ��ü�� ���ٸ�
	}//ĳ���� - ī�޶� ����ĳ��Ʈ ����

	private void CameraColliderCollision()
	{
		if(CameraCollisionMode == true)
		{
			this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
			SetCameraColliderPosition();
		}// ī�޶� ��尡 �ڽ� �ݶ��̴� �����
		else
		{
			this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
			SetCameraRayPosition();
		}// ī�޶� ��尡 ����ĳ��Ʈ �����
	}

	private void ChangeTargetRotation()
	{
		GameManager.instance.ChangeTargetRotationX = CameraAngleX;
		GameManager.instance.ChangeTargetRotationY = CameraAngleY;
	}

	private void OnTriggerEnter(Collider other)
	{

	}
	private void OnTriggerExit(Collider other)
	{

	}
}
