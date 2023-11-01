using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class OneInAll : MonoBehaviour
{
	public Vector3 StartingPosition;
	public int Object_ID = 0;
	public int Object_Type = 0;
	// 0 : �÷��̾�
	// 1 ~ : ��
	// 100 ~ : �ǹ�
	// 500 ~ : ����
	// 1000 ~ : ������Ʈ

	//ĳ���� ����
	public bool JumpKeyState = false;
	public bool RunKeyState = false;
	public float Force = 0;

	//���� ����
	public int TouchToPlayer = 0;

	//������ ����
	public int TouchToSetRendererState = 0;
	public int TouchToSetRendererReverse = 0;

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
		GameManager.instance.ObjectCount = GameManager.instance.ObjectCount + 1;

		//

		StartingPosition = this.gameObject.transform.position;

		SetPlayerState();
		SetMonsterState();
		SetBlockState();
		SetStructureState();
		SetObjectState();
	}

	//�����Ӵ� �� �� ȣ��
	private void Update()
	{
		KeyInput();
	}

	//0.02�ʸ��� �� �� ȣ��
	private void FixedUpdate()
	{
		GameSave();
		PlayerMovement();
		MonsterMovement();
		ResetObjectPosition();
		DeActivateRenderer();
		PlayerCameraPosition();
	}

	//�����Ӵ� �� �� ȣ��
	private void LateUpdate()
	{

	}

	//�ı��� �� ȣ��
	private void OnDestroy()
	{
		GameManager.instance.ObjectCount = GameManager.instance.ObjectCount - 1;
	}

	//��Ȱ��ȭ �� �� ���� ȣ��
	private void OnDisable()
	{

	}

	private void DeActivateRenderer()
	{
		if (this.gameObject.tag != "Player")
		{
			if(TouchToSetRendererState > 0 && TouchToSetRendererReverse <= 0)
			{
				if(this.gameObject.GetComponent<Renderer>() != null)
				{
					this.gameObject.GetComponent<Renderer>().enabled = true;
				}
				else
				{
					if (this.gameObject.transform.childCount != 0)
					{
						for (int i = 0; i < this.gameObject.transform.childCount; i = i + 1)
						{
							if (this.gameObject.transform.GetChild(i).gameObject.GetComponent<Renderer>() != null)
							{
								this.gameObject.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = true;
							}
						}
					}
				}
			}
			else if(TouchToSetRendererState <= 0 || TouchToSetRendererReverse > 0)
			{
				if (this.gameObject.GetComponent<Renderer>() != null)
				{
					this.gameObject.GetComponent<Renderer>().enabled = false;
				}
				else
				{
					if (this.gameObject.transform.childCount != 0)
					{
						for (int i = 0; i < this.gameObject.transform.childCount; i = i + 1)
						{
							if (this.gameObject.transform.GetChild(i).gameObject.GetComponent<Renderer>() != null)
							{
								this.gameObject.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = false;
							}
						}
					}
				}
			}
		}
	}

	private void ResetObjectPosition()
	{
		if (this.gameObject.transform.position.y <= -1)
		{
			this.gameObject.transform.position = StartingPosition;
		}
	}

	private void SetObjectState()
	{
		if (this.gameObject.tag == "Object")
		{
			if (Object_Type == 1000) // ����
			{
				Instantiate(GameManager.instance.obj[0], this.gameObject.transform); //�׷��ͱ�
				Instantiate(GameManager.instance.obj[1], this.gameObject.transform); //�볪��
				Instantiate(GameManager.instance.obj[2], this.gameObject.transform); //������
				Destroy(this.gameObject.GetComponent<MeshFilter>());
				Destroy(this.gameObject.GetComponent<Renderer>());
				if(this.gameObject.GetComponent<Rigidbody>() == null)
                {
					this.gameObject.AddComponent<Rigidbody>();
                }
				this.gameObject.GetComponent<Rigidbody>().useGravity = false;
				this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			}
			else if (Object_Type == 1001) // ������
			{
			}
		}
	}

	private void SetStructureState()
	{
		if (this.gameObject.tag == "Structure")
		{
			if (Object_Type == 100)
			{
				this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
			}
		}
	}

	private void SetBlockState()
	{
		if (this.gameObject.tag == "Block")
		{
			if (Object_Type == 1) // Ǯ ��
			{
				this.gameObject.GetComponent<Renderer>().material = GameManager.instance.mater[1];
				this.gameObject.GetComponent<MeshFilter>().mesh = GameManager.instance.mesh[0];
				this.gameObject.AddComponent<BoxCollider>();
			}
			else if(Object_Type == 2) //���� �� ��
			{
				this.gameObject.GetComponent<Renderer>().material = GameManager.instance.mater[2];
				this.gameObject.GetComponent<MeshFilter>().mesh = GameManager.instance.mesh[0];
				this.gameObject.AddComponent<BoxCollider>();
			}
			else if (Object_Type == 3) // ��� ��
			{
				this.gameObject.GetComponent<Renderer>().material = GameManager.instance.mater[3];
				this.gameObject.GetComponent<MeshFilter>().mesh = GameManager.instance.mesh[1];
				this.gameObject.AddComponent<MeshCollider>();
			}
			else if (Object_Type == 4) // ���� ��� ��
			{
				this.gameObject.GetComponent<Renderer>().material = GameManager.instance.mater[4];
				this.gameObject.GetComponent<MeshFilter>().mesh = GameManager.instance.mesh[2];
				this.gameObject.AddComponent<MeshCollider>();
			}
			else if (Object_Type == 5) // ���� Ǯ ��
			{
				this.gameObject.GetComponent<Renderer>().material = GameManager.instance.mater[5];
				this.gameObject.GetComponent<MeshFilter>().mesh = GameManager.instance.mesh[3];
				this.gameObject.AddComponent<BoxCollider>();
			}
			else if (Object_Type == 6) // ��� ���� �� ��
			{
				this.gameObject.GetComponent<Renderer>().material = GameManager.instance.mater[6];
				this.gameObject.GetComponent<MeshFilter>().mesh = GameManager.instance.mesh[3];
				this.gameObject.AddComponent<BoxCollider>();
			}
		}
	}

	private void SetPlayerState()
	{
		if (this.gameObject.tag == "Player")
		{
			Force = 0.25f;
		}
	}

	private void SetMonsterState()
	{
		if (this.gameObject.tag == "Monster")
		{
			if (Object_Type == 500)
			{
				this.gameObject.GetComponent<Renderer>().material.color = Color.red;
			}
		}
	}

	private void GameSave()
	{
		//���� ��ư�� ���� ���¶��
		if (GameManager.instance.SaveButtonState == true)
		{
			FileInfo file = new FileInfo("FileStream.dat");
			Stream writeStream;

			if (file.Exists == true)
			{
				writeStream = new FileStream("FileStream.dat", FileMode.Append);
			}
			else
			{
				writeStream = new FileStream("FileStream.dat", FileMode.OpenOrCreate);
			}

			StreamWriter streamWriter = new StreamWriter(writeStream);

			//���� ������Ʈ�� ���¸� ����
			if (this.gameObject.tag == "Block")
			{
				streamWriter.WriteLine("Block" + GameManager.instance.SaveCount);
				streamWriter.WriteLine(this.gameObject.transform.position.x);
				streamWriter.WriteLine(this.gameObject.transform.position.y);
				streamWriter.WriteLine(this.gameObject.transform.position.z);
				streamWriter.WriteLine(this.gameObject.transform.rotation.eulerAngles.x);
				streamWriter.WriteLine(this.gameObject.transform.rotation.eulerAngles.y);
				streamWriter.WriteLine(this.gameObject.transform.rotation.eulerAngles.z);
				streamWriter.WriteLine(Object_Type);
			}

			if (this.gameObject.tag == "Player")
			{
				streamWriter.WriteLine("Player" + GameManager.instance.SaveCount);
				streamWriter.WriteLine(this.gameObject.transform.position.x);
				streamWriter.WriteLine(this.gameObject.transform.position.y);
				streamWriter.WriteLine(this.gameObject.transform.position.z);
				streamWriter.WriteLine(this.gameObject.transform.rotation.eulerAngles.x);
				streamWriter.WriteLine(this.gameObject.transform.rotation.eulerAngles.y);
				streamWriter.WriteLine(this.gameObject.transform.rotation.eulerAngles.z);
				streamWriter.WriteLine(Object_Type);
			}

			if (this.gameObject.tag == "Structure")
			{
				streamWriter.WriteLine("Structure" + GameManager.instance.SaveCount);
				streamWriter.WriteLine(this.gameObject.transform.position.x);
				streamWriter.WriteLine(this.gameObject.transform.position.y);
				streamWriter.WriteLine(this.gameObject.transform.position.z);
				streamWriter.WriteLine(this.gameObject.transform.rotation.eulerAngles.x);
				streamWriter.WriteLine(this.gameObject.transform.rotation.eulerAngles.y);
				streamWriter.WriteLine(this.gameObject.transform.rotation.eulerAngles.z);
				streamWriter.WriteLine(Object_Type);
			}

			if (this.gameObject.tag == "Monster")
			{
				streamWriter.WriteLine("Monster" + GameManager.instance.SaveCount);
				streamWriter.WriteLine(this.gameObject.transform.position.x);
				streamWriter.WriteLine(this.gameObject.transform.position.y);
				streamWriter.WriteLine(this.gameObject.transform.position.z);
				streamWriter.WriteLine(this.gameObject.transform.rotation.eulerAngles.x);
				streamWriter.WriteLine(this.gameObject.transform.rotation.eulerAngles.y);
				streamWriter.WriteLine(this.gameObject.transform.rotation.eulerAngles.z);
				streamWriter.WriteLine(Object_Type);
			}

			if (this.gameObject.tag == "Object")
			{
				streamWriter.WriteLine("Object" + GameManager.instance.SaveCount);
				streamWriter.WriteLine(this.gameObject.transform.position.x);
				streamWriter.WriteLine(this.gameObject.transform.position.y);
				streamWriter.WriteLine(this.gameObject.transform.position.z);
				streamWriter.WriteLine(this.gameObject.transform.rotation.eulerAngles.x);
				streamWriter.WriteLine(this.gameObject.transform.rotation.eulerAngles.y);
				streamWriter.WriteLine(this.gameObject.transform.rotation.eulerAngles.z);
				streamWriter.WriteLine(Object_Type);
			}

			streamWriter.Close();
			GameManager.instance.SaveCount = GameManager.instance.SaveCount + 1;
			

			//���� SaveCount�� ��� Block ������Ʈ���� ���ٸ�
			if (GameManager.instance.ObjectCount <= GameManager.instance.SaveCount)
			{
				//���� ��ư ����
				GameManager.instance.SaveButtonState = false;
				//SaveCount �ʱ�ȭ
				GameManager.instance.SaveCount = 0;
			}
		}

		if (GameManager.instance.LoadButtonState == true)
		{
			Destroy(this.gameObject);
		}
		
		//PlayerPrefs.SetString("Object" + GameManager.instance.SaveCount, "Player" + GameManager.instance.SaveCount);

		//PlayerPrefs.SetFloat("Object" + GameManager.instance.SaveCount + "PositionX", this.gameObject.transform.position.x);
		//PlayerPrefs.SetFloat("Object" + GameManager.instance.SaveCount + "PositionY", this.gameObject.transform.position.y);
		//PlayerPrefs.SetFloat("Object" + GameManager.instance.SaveCount + "PositionZ", this.gameObject.transform.position.z);
		//PlayerPrefs.SetInt("Object" + GameManager.instance.SaveCount + "Tpye", Object_Type);
		//PlayerPrefs.Save();
	}

	private void KeyInput()
	{
		if(this.gameObject.tag == "Player")
		{
			//����
			if (this.gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollision>().CollisionTrigger > 0)
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					JumpKeyState = true;
				}
			}

			//�޸���
			if (Input.GetKey(KeyCode.LeftShift))
			{
				RunKeyState = true;
			}
		}
	}

	private void PlayerMovement()
	{
		if (this.gameObject.tag == "Player")
		{
			float HorizontalMove = Input.GetAxis("Horizontal");
			float VerticalMove = Input.GetAxis("Vertical");
			float DiagonalMove;
			float MoveSpeed = Force;
			float JumpPower;

			if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
				this.gameObject.transform.rotation = Quaternion.Euler(0.0f, -GameManager.instance.ChangeTargetRotationX, 0.0f);
            }

			if (HorizontalMove != 0.0f || VerticalMove != 0.0f)
			{
				//���Ʒ��� �����̴� �ӵ��� �¿�� �����̴� �ӵ����� ũ�ٸ�
				if (Mathf.Abs(HorizontalMove) < Mathf.Abs(VerticalMove))
				{
					DiagonalMove = Input.GetAxis("Vertical") / Mathf.Cos(Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
					VerticalMove = VerticalMove / DiagonalMove;
				}
				//�¿�� �����̴� �ӵ��� ���Ʒ��� �����̴� �ӵ����� ũ�ٸ�
				else if (Mathf.Abs(HorizontalMove) > Mathf.Abs(VerticalMove))
				{
					DiagonalMove = Input.GetAxis("Horizontal") / Mathf.Cos(Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")));
					HorizontalMove = HorizontalMove / DiagonalMove;
				}
				else if(Mathf.Abs(HorizontalMove) == Mathf.Abs(VerticalMove))
				{
					VerticalMove = VerticalMove / Mathf.Sqrt(2.0f);
					HorizontalMove = HorizontalMove / Mathf.Sqrt(2.0f);
				}

				if(Input.GetAxisRaw("Horizontal") == 0)
				{
					HorizontalMove = 0;
				}
				if (Input.GetAxisRaw("Vertical") == 0)
				{
					VerticalMove = 0;
				}

				if(RunKeyState == true)
				{
					MoveSpeed = Force * 2.0f;
					RunKeyState = false;
				}

				//this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(HorizontalMove, 0.0f, VerticalMove) * Time.fixedDeltaTime * MoveSpeed);
				//this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(HorizontalMove, 0.0f, VerticalMove) * Time.fixedDeltaTime * MoveSpeed;
				this.gameObject.transform.Translate(new Vector3(HorizontalMove, 0.0f, VerticalMove) * Time.fixedDeltaTime * MoveSpeed);
			}
			
			if(JumpKeyState == true)
			{
				JumpPower = Force * 750.0f;
				this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * JumpPower);
				JumpKeyState = false;
			}
		}
	}

	private void PlayerCameraPosition()
	{
		if (this.gameObject.tag == "Player")
		{
			GameManager.instance.TargetPosition = this.gameObject.transform.position;
		}
	}

	private void MonsterMovement()
	{
		if (this.gameObject.tag == "Monster")
		{
			if (this.gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollision>().CollisionTrigger > 0)
			{
				float HorizontalMove = this.gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollision>().CollisionObject.transform.position.x - this.gameObject.transform.position.x;
				float VerticalMove = this.gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollision>().CollisionObject.transform.position.z - this.gameObject.transform.position.z;
				float DiagonalMove;

				if (Mathf.Abs(HorizontalMove) < Mathf.Abs(VerticalMove))
				{
					if (HorizontalMove > 0)
					{
						HorizontalMove = HorizontalMove / VerticalMove;
						HorizontalMove = Mathf.Abs(HorizontalMove);
					}
					else if (HorizontalMove < 0)
					{
						HorizontalMove = HorizontalMove / VerticalMove;
						HorizontalMove = Mathf.Abs(HorizontalMove) * -1.0f;
					}

					if (VerticalMove > 0)
					{
						VerticalMove = 1.0f;

						this.gameObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0.0f, -45.0f, 0.0f);
					}
					else if (VerticalMove < 0)
					{
						VerticalMove = -1.0f;

						this.gameObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0.0f, 135.0f, 0.0f);
					}

					DiagonalMove = VerticalMove / Mathf.Cos(Mathf.Atan2(HorizontalMove, VerticalMove));
					VerticalMove = VerticalMove / DiagonalMove;
				}
				else if (Mathf.Abs(HorizontalMove) > Mathf.Abs(VerticalMove))
				{
					if (VerticalMove > 0)
					{
						VerticalMove = VerticalMove / HorizontalMove;
						VerticalMove = Mathf.Abs(VerticalMove);
					}
					else if (VerticalMove < 0)
					{
						VerticalMove = VerticalMove / HorizontalMove;
						VerticalMove = Mathf.Abs(VerticalMove) * -1.0f;
					}

					if (HorizontalMove > 0)
					{
						HorizontalMove = 1.0f;

						this.gameObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0.0f, 45.0f, 0.0f);
					}
					else if (HorizontalMove < 0)
					{
						HorizontalMove = -1.0f;

						this.gameObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0.0f, -135.0f, 0.0f);
					}

					DiagonalMove = HorizontalMove / Mathf.Cos(Mathf.Atan2(VerticalMove, HorizontalMove));
					HorizontalMove = HorizontalMove / DiagonalMove;
				}
				else if(Mathf.Abs(HorizontalMove) == Mathf.Abs(VerticalMove))
				{
					if(HorizontalMove > 0)
					{
						HorizontalMove = 1.0f;
					}
					else if(HorizontalMove < 0)
					{
						HorizontalMove = -1.0f;
					}

					if (VerticalMove > 0)
					{
						VerticalMove = 1.0f;
					}
					else if (VerticalMove < 0)
					{
						VerticalMove = -1.0f;
					}

					VerticalMove = VerticalMove / Mathf.Sqrt(2.0f);
					HorizontalMove = HorizontalMove / Mathf.Sqrt(2.0f);
				}

				if(TouchToPlayer > 0)
				{
					HorizontalMove = 0;
					VerticalMove = 0;
				}

				this.gameObject.transform.Translate(new Vector3(HorizontalMove, 0.0f, VerticalMove) * Time.fixedDeltaTime * 0.25f);
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (this.gameObject.tag == "Monster")
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				TouchToPlayer = TouchToPlayer + 1;
			}
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (this.gameObject.tag == "Monster")
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				if (TouchToPlayer > 0)
				{
					TouchToPlayer = TouchToPlayer - 1;
				}
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (this.gameObject.tag != "Player")
		{
			if (other.gameObject.CompareTag("SetRenderState"))
			{
				TouchToSetRendererState = TouchToSetRendererState + 1;
			}
			if (other.gameObject.CompareTag("SetRenderReverse"))
			{
				if(this.gameObject.tag != "Object" && this.gameObject.tag != "Monster")
				{
					TouchToSetRendererReverse = TouchToSetRendererReverse + 1;
				}
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (this.gameObject.tag != "Player")
		{
			if (other.gameObject.CompareTag("SetRenderState"))
			{
				if (TouchToSetRendererState > 0)
				{
					TouchToSetRendererState = TouchToSetRendererState - 1;
				}
			}
			if (other.gameObject.CompareTag("SetRenderReverse"))
			{
				if (TouchToSetRendererReverse > 0)
				{
					TouchToSetRendererReverse = TouchToSetRendererReverse - 1;
				}
			}
		}
	}
}
