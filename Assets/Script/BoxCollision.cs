using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollision : MonoBehaviour
{
	public int CollisionTrigger = 0;
	public GameObject CollisionObject;

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

	}

	//�����Ӵ� �� �� ȣ��
	private void Update()
	{
	}

	//0.02�ʸ��� �� �� ȣ��
	private void FixedUpdate()
	{
	
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

	private void OnTriggerEnter(Collider other)
    {
		if(this.gameObject.name == "TouchToGround")
		{
			if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("Structure"))
			{
				CollisionTrigger = CollisionTrigger + 1;
			}
		}

		if(this.gameObject.name == "PreySearch")
		{
			if (other.gameObject.CompareTag("Player"))
			{
				CollisionTrigger = CollisionTrigger + 1;

				CollisionObject = other.gameObject;
			}
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (this.gameObject.name == "TouchToGround")
		{
			if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("Structure"))
			{
				if (CollisionTrigger > 0)
				{
					CollisionTrigger = CollisionTrigger - 1;
				}
			}
		}

		if (this.gameObject.name == "PreySearch")
		{
			if (other.gameObject.CompareTag("Player"))
			{
				if (CollisionTrigger > 0)
				{
					CollisionTrigger = CollisionTrigger - 1;
				}

				if(CollisionTrigger <= 0)
				{
					CollisionObject = null;
				}
			}
		}
	}
}
