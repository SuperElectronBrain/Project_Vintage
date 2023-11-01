using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;

	//버튼 관련
	public bool SaveButtonState = false;
	public bool LoadButtonState = false;
	public bool EscapeButtonState = false;
	public bool LeftControlButtonState = false;
	public bool EButtonState = false;

	//세이브 관련
	public int ObjectCount = 0;
	public int SaveCount = 0;

	//카메라 관련
	public Vector3 TargetPosition = new Vector3(0.0f, 0.0f, 0.0f);
	public float ChangeTargetRotationX;
	public float ChangeTargetRotationY;

	//프리펩 관련
	public Material[] mater;
	public Mesh[] mesh;
	public GameObject[] obj;
	public GameObject InGamePlayer;
	public GameObject InGameMonster;
	public GameObject InGameBlock;
	public GameObject InGameStruct;
	public GameObject InGameTerrain;
	public GameObject InGameBuilding;
	public GameObject InGameObject;
	public GameObject InGameStuff;

	// 제일 먼저 한 번만 호출
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			if (instance != this)
			{
				Destroy(this.gameObject);
			}
		}
	}

	//활성화 될 때 마다 호출
	private void OnEnable()
	{

	}

	//두 번째로 한 번만 호출
	private void Start()
	{
		GameLoad();
		Cursor.visible = false;
	}

	//프레임당 한 번 호출
	private void Update()
	{
		KeyBoardInput();
	}

	//0.02초마다 한 번 호출
	private void FixedUpdate()
	{
		GameLoadButton();
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
	private void GameLoad()
	{
		Stream readStream = new FileStream("FileStream.dat", FileMode.Open);
		StreamReader streamReader = new StreamReader(readStream);

		for (int i = 0; streamReader.EndOfStream == false; i = i + 1)
		{
			string temporary = streamReader.ReadLine();

			if (temporary == "Block" + i)
			{
				Vector3 position_set = new Vector3(float.Parse(streamReader.ReadLine()), float.Parse(streamReader.ReadLine()), float.Parse(streamReader.ReadLine()));
				Vector3 rotation_set = new Vector3(float.Parse(streamReader.ReadLine()), float.Parse(streamReader.ReadLine()), float.Parse(streamReader.ReadLine()));
				GameObject _instance = Instantiate(InGameBlock, position_set, Quaternion.Euler(rotation_set), InGameTerrain.transform);
				_instance.GetComponent<OneInAll>().Object_Type = int.Parse(streamReader.ReadLine());
				_instance.GetComponent<OneInAll>().Object_ID = i;
				_instance.gameObject.tag = "Block";
			}
			else if (temporary == "Structure" + i)
			{
				Vector3 position_set = new Vector3(float.Parse(streamReader.ReadLine()), float.Parse(streamReader.ReadLine()), float.Parse(streamReader.ReadLine()));
				Vector3 rotation_set = new Vector3(float.Parse(streamReader.ReadLine()), float.Parse(streamReader.ReadLine()), float.Parse(streamReader.ReadLine()));
				GameObject _instance = Instantiate(InGameStruct, position_set, Quaternion.Euler(rotation_set), InGameBuilding.transform);
				_instance.GetComponent<OneInAll>().Object_Type = int.Parse(streamReader.ReadLine());
				_instance.GetComponent<OneInAll>().Object_ID = i;
				_instance.gameObject.tag = "Structure";
			}
			else if (temporary == "Player" + i)
			{
				Vector3 position_set = new Vector3(float.Parse(streamReader.ReadLine()), float.Parse(streamReader.ReadLine()), float.Parse(streamReader.ReadLine()));
				Vector3 rotation_set = new Vector3(float.Parse(streamReader.ReadLine()), float.Parse(streamReader.ReadLine()), float.Parse(streamReader.ReadLine()));
				GameObject _instance = Instantiate(InGamePlayer, position_set, Quaternion.Euler(rotation_set));
				_instance.GetComponent<OneInAll>().Object_Type = int.Parse(streamReader.ReadLine());
				_instance.GetComponent<OneInAll>().Object_ID = i;
				_instance.gameObject.tag = "Player";
			}
			else if (temporary == "Monster" + i)
			{
				Vector3 position_set = new Vector3(float.Parse(streamReader.ReadLine()), float.Parse(streamReader.ReadLine()), float.Parse(streamReader.ReadLine()));
				Vector3 rotation_set = new Vector3(float.Parse(streamReader.ReadLine()), float.Parse(streamReader.ReadLine()), float.Parse(streamReader.ReadLine()));
				GameObject _instance = Instantiate(InGameMonster, position_set, Quaternion.Euler(rotation_set));
				_instance.GetComponent<OneInAll>().Object_Type = int.Parse(streamReader.ReadLine());
				_instance.GetComponent<OneInAll>().Object_ID = i;
				_instance.gameObject.tag = "Monster";
			}
			else if (temporary == "Object" + i)
			{
				Vector3 position_set = new Vector3(float.Parse(streamReader.ReadLine()), float.Parse(streamReader.ReadLine()), float.Parse(streamReader.ReadLine()));
				Vector3 rotation_set = new Vector3(float.Parse(streamReader.ReadLine()), float.Parse(streamReader.ReadLine()), float.Parse(streamReader.ReadLine()));
				GameObject _instance = Instantiate(InGameObject, position_set, Quaternion.Euler(rotation_set), InGameStuff.transform);
				_instance.GetComponent<OneInAll>().Object_Type = int.Parse(streamReader.ReadLine());
				_instance.GetComponent<OneInAll>().Object_ID = i;
				_instance.gameObject.tag = "Object";
			}
		}

		streamReader.Close();

		//

		//for (int i = 0; i <= PlayerPrefs.GetInt("SpaceSize") - 1; i = i + 1)
		//{
		//	if (PlayerPrefs.GetString("Object" + i) == ("Block" + i))
		//	{
		//		GameObject _instance = Instantiate(InGameBlock, new Vector3(PlayerPrefs.GetFloat("Object" + i + "PositionX"), PlayerPrefs.GetFloat("Object" + i + "PositionY"), PlayerPrefs.GetFloat("Object" + i + "PositionZ")), Quaternion.identity, InGameTerrain.transform);
		//		_instance.GetComponent<OneInAll>().Object_ID = i;
		//		_instance.GetComponent<OneInAll>().Object_Type = PlayerPrefs.GetInt("Object" + i + "Tpye");
		//	}
		//
		//	if (PlayerPrefs.GetString("Object" + i) == ("Player" + i))
		//	{
		//		GameObject _instance = Instantiate(InGameBlock, new Vector3(PlayerPrefs.GetFloat("Object" + i + "PositionX"), PlayerPrefs.GetFloat("Object" + i + "PositionY"), PlayerPrefs.GetFloat("Object" + i + "PositionZ")), Quaternion.identity);
		//		_instance.GetComponent<OneInAll>().Object_ID = i;
		//		_instance.GetComponent<OneInAll>().Object_Type = PlayerPrefs.GetInt("Object" + i + "Tpye");
		//	}
		//}
	}

	private void GameLoadButton()
	{
		if (LoadButtonState == true)
		{
			if (ObjectCount <= 0)
			{
				GameLoad();

				LoadButtonState = false;
			}
		}
	}

	private void KeyBoardInput()
	{
		if (Input.GetKeyDown(KeyCode.Escape) == true)
		{
			if (EscapeButtonState == false)
			{
				EscapeButtonState = true;
			}
			else
			{
				EscapeButtonState = false;
			}
		}
		if (Input.GetKeyDown(KeyCode.LeftControl) == true)
		{
			if (LeftControlButtonState == false)
			{
				LeftControlButtonState = true;
			}
			else
			{
				LeftControlButtonState = false;
			}
		}
		if (Input.GetKeyDown(KeyCode.E) == true)
		{
			if (EButtonState == false)
			{
				EButtonState = true;
			}
			else
			{
				EButtonState = false;
			}
		}
	}
}
