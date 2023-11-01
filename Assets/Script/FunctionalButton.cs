using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class FunctionalButton : MonoBehaviour
{
	public void OnClickButton()
	{
		//��ũ��Ʈ�� ����� ������Ʈ�� ���� ��ư�϶�
		if (this.gameObject.name == "StartButton")
		{
			SceneManager.LoadScene("World");
		}

		//��ũ��Ʈ�� ����� ������Ʈ�� ���� ��ư�϶�
		if (this.gameObject.name == "ExitButton")
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}

		if (this.gameObject.name == "SaveButton")
		{
			//PlayerPrefs.DeleteAll();
			//PlayerPrefs.SetInt("SpaceSize", GameManager.instance.ObjectCount);

			Stream writeStream = new FileStream("FileStream.dat", FileMode.Truncate);
			StreamWriter streamWriter = new StreamWriter(writeStream);
			streamWriter.Close();

			GameManager.instance.SaveButtonState = true;
		}

		if (this.gameObject.name == "LoadButton")
		{
			GameManager.instance.LoadButtonState = true;
		}
	}
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
}
