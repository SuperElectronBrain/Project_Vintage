using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public bool CameraCollisionMode = true;
	private	float CameraSpace = 0.2f;// 카메라 거리 보정값
	private	float CameraMaxDistance = 2.0f;// 카메라 최대 거리
	private	float CameraMinDistance = 0.0f;// 카메라 최소 거리
	private	float CameraDistance = 0.0f;// 카메라 현재 거리
	private	float CameraDistanceSave = 0.0f;// 카메라 현재 거리 백업
	private	float CameraAngleX = 0.0f;// (카메라 <--> 타켓) 수평 각도
	private	float CameraAngleY = 0.0f;// (카메라 <--> 타켓) 수직 각도

	// 제일 먼저 한 번만 호출
	private void Awake()
	{

	}

	//활성화 될 때 마다 호출
	private void OnEnable()
	{

	}

	//두 번째로 한 번만 호출
	private void Start()
	{
		CameraDistance = CameraMaxDistance - CameraSpace;
		CameraDistanceSave = CameraDistance;
	}

	//프레임당 한 번 호출
	private void Update()
	{
		CameraKeyInput();
	}

	//0.02초마다 한 번 호출
	private void FixedUpdate()
	{
		ChangeTargetRotation();
		CameraMovement();
		CameraColliderCollision();
	}

	//프레임당 한 번 호출
	private void LateUpdate()
	{

	}

	//파괴될 때 호출
	private void OnDestroy()
	{
	}

	//비활성화 될 때 마다 호출
	private void OnDisable()
	{

	}

	private void CameraMovement()
	{
		this.gameObject.transform.position = new Vector3 // 카메라 즉시 이동
		//Vector3 TargetPosition = new Vector3 
			(
			GameManager.instance.TargetPosition.x + (Mathf.Sin(CameraAngleX * Mathf.Deg2Rad) * (Mathf.Cos(CameraAngleY * Mathf.Deg2Rad) * CameraDistance)),
			GameManager.instance.TargetPosition.y + (Mathf.Sin(CameraAngleY * Mathf.Deg2Rad) * CameraDistance),
			GameManager.instance.TargetPosition.z - (Mathf.Cos(CameraAngleX * Mathf.Deg2Rad) * (Mathf.Cos(CameraAngleY * Mathf.Deg2Rad) * CameraDistance))
			); //카메라 지연 이동

		//this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, TargetPosition, Time.fixedDeltaTime * 10.0f);

		this.gameObject.transform.rotation = Quaternion.Euler(CameraAngleY, -CameraAngleX, 0.0f);	
	}//카메라 위치 설정

	private void CameraKeyInput()
	{
		if(GameManager.instance.EscapeButtonState == false && GameManager.instance.LeftControlButtonState == false)
        {
			//캐릭터 - 카메라 간격
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

			//캐릭터 - 카메라 상하 각도
			CameraAngleY = CameraAngleY + (-Input.GetAxis("Mouse Y") * 2.0f);

			if (CameraAngleY < -90.0f)
			{
				CameraAngleY = -90.0f;
			}
			else if (CameraAngleY > 90.0f)
			{
				CameraAngleY = 90.0f;
			}

			//캐릭터 - 카메라 좌우 각도
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
	}//카메라 조작

	private void SetCameraColliderPosition()
	{
		//캐릭터 - 카메라간 콜라이더 위치 설정
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
	}//캐릭터 - 카메라간 콜라이더 설정

	private void SetCameraRayPosition()
    {
		RaycastHit hit;

		Vector3 TemporaryPosition = new Vector3
		(
		this.gameObject.transform.position.x - GameManager.instance.TargetPosition.x,
		this.gameObject.transform.position.y - GameManager.instance.TargetPosition.y,
		this.gameObject.transform.position.z - GameManager.instance.TargetPosition.z
		);// 레이를 발사할 방향

		if (Physics.Raycast(GameManager.instance.TargetPosition, TemporaryPosition, out hit, CameraMaxDistance))
		{
			if (hit.transform.gameObject.tag == "Structure" || hit.transform.gameObject.tag == "Block")
			{
				TemporaryPosition = new Vector3
					(
					hit.point.x - GameManager.instance.TargetPosition.x,
					hit.point.y - GameManager.instance.TargetPosition.y,
					hit.point.z - GameManager.instance.TargetPosition.z
					);//레이가 충돌한 방향과 위치

				if (((-TemporaryPosition.z) / Mathf.Cos(CameraAngleX * Mathf.Deg2Rad)) / Mathf.Cos(CameraAngleY * Mathf.Deg2Rad) < CameraDistanceSave + CameraSpace)
				{
					CameraDistance = (((-TemporaryPosition.z) / Mathf.Cos(CameraAngleX * Mathf.Deg2Rad)) / Mathf.Cos(CameraAngleY * Mathf.Deg2Rad)) - CameraSpace;
					//카메라의 위치를 레이가 충돌한 곳으로 변경 (CameraSpace는 카메라 위치 보정용)
					if (CameraDistance < CameraSpace)
                    {
						CameraDistance = CameraSpace;// (카메라 <--> 카메라 타겟)의 거리를 CameraSpace 길이로 교정

						if (((-TemporaryPosition.z) / Mathf.Cos(CameraAngleX * Mathf.Deg2Rad)) / Mathf.Cos(CameraAngleY * Mathf.Deg2Rad) < CameraDistance)
                        {
							CameraDistance = (((-TemporaryPosition.z) / Mathf.Cos(CameraAngleX * Mathf.Deg2Rad)) / Mathf.Cos(CameraAngleY * Mathf.Deg2Rad));
							//카메라의 위치를 레이가 충돌한 곳으로 변경
						}// 만약 교정된 (카메라 <--> 카메라 타겟)의 거리가 (레이가 충돌한 곳 <--> 카메라 타겟)보다 멀다면
					}// 만약 변경된 거리값이 보정용 값 보다 작다면
				}// (레이가 충돌한 곳 <--> 카메라 타겟)의 거리가 (카메라 <--> 카메라 타겟)의 거리 보다 짧다면 (CameraSpace는 카메라 위치 보정용)
			}// 레이에 맞은 물체가 건물과 블럭이라면
		}// 카메라가 비추고 있는 타겟을 시작점으로 카메라가 위치한 방향에 CameraMaxDistance 길이의 레이를 발사
		else
		{
			CameraDistance = CameraDistanceSave; // 카메라를 원위치 시켜줌
		}// 레이에 충돌한 물체가 없다면
	}//캐릭터 - 카메라간 레이캐스트 설정

	private void CameraColliderCollision()
	{
		if(CameraCollisionMode == true)
		{
			this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
			SetCameraColliderPosition();
		}// 카메라 모드가 박스 콜라이더 모드라면
		else
		{
			this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
			SetCameraRayPosition();
		}// 카메라 모드가 레이캐스트 모드라면
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
