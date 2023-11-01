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
		//스크립트가 적용된 오브젝트가 시작 버튼일때
		if (this.gameObject.name == "StartButton")
		{
			SceneManager.LoadScene("World");
		}

		//스크립트가 적용된 오브젝트가 종료 버튼일때
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
}
