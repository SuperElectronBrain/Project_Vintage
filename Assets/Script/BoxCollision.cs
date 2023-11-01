using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollision : MonoBehaviour
{
	public int CollisionTrigger = 0;
	public GameObject CollisionObject;

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

	}

	//프레임당 한 번 호출
	private void Update()
	{
	}

	//0.02초마다 한 번 호출
	private void FixedUpdate()
	{
	
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
