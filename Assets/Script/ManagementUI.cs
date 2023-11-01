using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagementUI : MonoBehaviour
{
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
		KeyInput();
		ReSizeUI();
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

	private void ReSizeUI()
    {
		if (this.gameObject.name == "ESC_UI")
        {
			float ScreanSizeY = 2.0f * Camera.main.orthographicSize;
			float ScreanSizeX = ScreanSizeY * Camera.main.aspect;

			//this.gameObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector2(ScreanSizeX, ScreanSizeY);
			//this.gameObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ScreanSizeX * 100);
			//this.gameObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ScreanSizeY * 100);
		}
	}

	private void KeyInput()
	{
		if (this.gameObject.name == "ESC_UI")
		{
			if (GameManager.instance.EscapeButtonState == true)
			{
				Cursor.visible = true;
				for (int i = 0; i < this.gameObject.transform.childCount; i = i + 1)
				{
					this.gameObject.transform.GetChild(i).gameObject.SetActive(true);
				}
			}
			else
			{
				Cursor.visible = false;
				for (int i = 0; i < this.gameObject.transform.childCount; i = i + 1)
				{
					this.gameObject.transform.GetChild(i).gameObject.SetActive(false);
				}
			}
		}

		if (this.gameObject.name == "Inventory_UI")
        {
			if (GameManager.instance.EButtonState == true)
			{
				for (int i = 0; i < this.gameObject.transform.childCount; i = i + 1)
				{
					this.gameObject.transform.GetChild(i).gameObject.SetActive(true);

					//this.gameObject.transform.GetChild(i).gameObject.GetComponent<RectTransform>().
				}
			}
			else
			{
				for (int i = 0; i < this.gameObject.transform.childCount; i = i + 1)
				{
					this.gameObject.transform.GetChild(i).gameObject.SetActive(false);
				}
			}
		}
	}
}