using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagementUI : MonoBehaviour
{
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
		KeyInput();
		ReSizeUI();
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