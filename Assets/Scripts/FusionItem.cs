using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class FusionItem : MonoBehaviour
{
    public int Horizontal;
    public int Vertical;
	public float mapScale;
	[SerializeField] private GameObject itemPrefab;
	[HideInInspector] public List<GameObject> items; 
	[HideInInspector] public Item item;
	[SerializeField] private GameObject popupWindow;
	[SerializeField] private GameObject[] snowmans;
	[SerializeField] private GameObject fairy;

	// Start is called before the first frame update
	void Start()
    {
		items = new List<GameObject>();
        for(int i = 0; i < (Horizontal * Vertical); i = i + 1)
        {
            items.Add(null);
		}

		for (int i = 0; i < 3; i = i + 1)
		{
			items[i] = Instantiate(itemPrefab);
			items[i].transform.position = transform.position + new Vector3
			(
				((i % Vertical) - (Horizontal / 2)) * mapScale,
				((-i / Horizontal) + (Vertical / 2)) * mapScale,
				0
			);

			if (i == 0) { items[i].GetComponent<Item>().itemType = "������"; }
			else if (i == 1) { items[i].GetComponent<Item>().itemType = "��������"; }
			else if (i == 2) { items[i].GetComponent<Item>().itemType = "���"; }
			items[i].GetComponent<Item>().AfterItemTypeChange();
		}
	}

    // Update is called once per frame
    void Update()
    {
		ItemDrag();
	}

    void ItemDrag()
    {
		if (Input.GetMouseButton(0) == true)
		{
			Vector2 targetPosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, Camera.main.transform.position.z);
			RaycastHit2D hit = Physics2D.Raycast(targetPosition, Vector3.forward, 15.0f, 1);

			if (hit == true)
			{
				if (hit.collider.tag == "Item")
				{
					if (item == null)
					{
						item = hit.transform.GetComponent<Item>();
					}
					item.transform.position = targetPosition;

					return;
				}
			}
		}

		if(item != null)
		{
			float x = item.transform.position.x < (Horizontal / 2) * -mapScale ? (Horizontal / 2) * -mapScale :
				(item.transform.position.x > (Horizontal / 2) * mapScale ? (Horizontal / 2) * mapScale : item.transform.position.x);
			float y = item.transform.position.y > (Vertical / 2) * mapScale ? (Vertical / 2) * mapScale :
				(item.transform.position.y < (Vertical / 2) * -mapScale ? (Vertical / 2) * -mapScale : item.transform.position.y);
			item.transform.position = new Vector3(x, y, item.transform.position.z);

			int coordinate_X = (int)((item.transform.position.x / mapScale) + (Horizontal / 2) + 0.5f);
			int coordinate_Y = (int)((item.transform.position.y / mapScale) - (Vertical / 2) - 0.5f);
			if (items[coordinate_X + (Horizontal * -coordinate_Y)] != null)
			{
				Item temp_Item = items[coordinate_X + (Horizontal * -coordinate_Y)].GetComponent<Item>();
				if (temp_Item.itemType != item.itemType)
				{
					if((temp_Item.itemType == "������" && item.itemType == "��������") || (temp_Item.itemType == "��������" && item.itemType == "������"))
					{
						for (int i = 0; i < items.Count; i = i + 1)
						{
							if (items[i] == null)
							{
								items[i] = Instantiate(itemPrefab);
								items[i].transform.position = transform.position + new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, 0);
								items[i].GetComponent<Item>().itemType = "������";
								items[i].GetComponent<Item>().AfterItemTypeChange();
								break;
							}
						}

						for (int i = 0; i < items.Count; i = i + 1)
						{
							if (items[i] == null)
							{
								items[i] = Instantiate(itemPrefab);
								items[i].transform.position = transform.position + new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, 0);
								items[i].GetComponent<Item>().itemType = "��������";
								items[i].GetComponent<Item>().AfterItemTypeChange();
								break;
							}
						}

						temp_Item.itemType = "�Թڴ����";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "��(��) ���۵Ǿ����ϴ�!";
						StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));

						snowmans[0].SetActive(true);
						int count = 0;
						for(int i = 0; i < snowmans.Length; i = i + 1)
						{
							if(snowmans[i].activeSelf == true)
							{
								count = count + 1;
							}
						}

						if(count >= snowmans.Length)
						{
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "���� ������ ������!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else if((temp_Item.itemType == "������" && item.itemType == "��") || (temp_Item.itemType == "��" && item.itemType == "������"))
					{
						for (int i = 0; i < items.Count; i = i + 1)
						{
							if (items[i] == null)
							{
								items[i] = Instantiate(itemPrefab);
								items[i].transform.position = transform.position + new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, 0);
								items[i].GetComponent<Item>().itemType = "������";
								items[i].GetComponent<Item>().AfterItemTypeChange();
								break;
							}
						}

						temp_Item.itemType = "�ʸ������";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "��(��) ���۵Ǿ����ϴ�!";
						StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));

						snowmans[1].SetActive(true);
						int count = 0;
						for (int i = 0; i < snowmans.Length; i = i + 1)
						{
							if (snowmans[i].activeSelf == true)
							{
								count = count + 1;
							}
						}

						if (count >= snowmans.Length)
						{
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "���� ������ ������!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else if ((temp_Item.itemType == "������" && item.itemType == "������") || (temp_Item.itemType == "������" && item.itemType == "������"))
					{
						for (int i = 0; i < items.Count; i = i + 1)
						{
							if (items[i] == null)
							{
								items[i] = Instantiate(itemPrefab);
								items[i].transform.position = transform.position + new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, 0);
								items[i].GetComponent<Item>().itemType = "������";
								items[i].GetComponent<Item>().AfterItemTypeChange();
								break;
							}
						}

						temp_Item.itemType = "�ִ����";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "��(��) ���۵Ǿ����ϴ�!";
						StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));

						snowmans[2].SetActive(true);
						int count = 0;
						for (int i = 0; i < snowmans.Length; i = i + 1)
						{
							if (snowmans[i].activeSelf == true)
							{
								count = count + 1;
							}
						}

						if (count >= snowmans.Length)
						{
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "���� ������ ������!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else if ((temp_Item.itemType == "������" && item.itemType == "������") || (temp_Item.itemType == "������" && item.itemType == "������"))
					{
						for (int i = 0; i < items.Count; i = i + 1)
						{
							if (items[i] == null)
							{
								items[i] = Instantiate(itemPrefab);
								items[i].transform.position = transform.position + new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, 0);
								items[i].GetComponent<Item>().itemType = "������";
								items[i].GetComponent<Item>().AfterItemTypeChange();
								break;
							}
						}

						temp_Item.itemType = "��� �����";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "��(��) ���۵Ǿ����ϴ�!";
						StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));

						snowmans[3].SetActive(true);
						int count = 0;
						for (int i = 0; i < snowmans.Length; i = i + 1)
						{
							if (snowmans[i].activeSelf == true)
							{
								count = count + 1;
							}
						}

						if (count >= snowmans.Length)
						{
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "���� ������ ������!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else if ((temp_Item.itemType == "������" && item.itemType == "�й�ġ") || (temp_Item.itemType == "�й�ġ" && item.itemType == "������"))
					{
						for (int i = 0; i < items.Count; i = i + 1)
						{
							if (items[i] == null)
							{
								items[i] = Instantiate(itemPrefab);
								items[i].transform.position = transform.position + new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, 0);
								items[i].GetComponent<Item>().itemType = "������";
								items[i].GetComponent<Item>().AfterItemTypeChange();
								break;
							}
						}

						temp_Item.itemType = "���ִ����";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "��(��) ���۵Ǿ����ϴ�!";
						StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));

						snowmans[4].SetActive(true);
						int count = 0;
						for (int i = 0; i < snowmans.Length; i = i + 1)
						{
							if (snowmans[i].activeSelf == true)
							{
								count = count + 1;
							}
						}

						if (count >= snowmans.Length)
						{
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "���� ������ ������!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else if ((temp_Item.itemType == "������" && item.itemType == "�������ǹ�ġ") || (temp_Item.itemType == "�������ǹ�ġ" && item.itemType == "������"))
					{
						for (int i = 0; i < items.Count; i = i + 1)
						{
							if (items[i] == null)
							{
								items[i] = Instantiate(itemPrefab);
								items[i].transform.position = transform.position + new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, 0);
								items[i].GetComponent<Item>().itemType = "������";
								items[i].GetComponent<Item>().AfterItemTypeChange();
								break;
							}
						}

						temp_Item.itemType = "���̸Ӵ����";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "��(��) ���۵Ǿ����ϴ�!";
						StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));

						snowmans[5].SetActive(true);
						int count = 0;
						for (int i = 0; i < snowmans.Length; i = i + 1)
						{
							if (snowmans[i].activeSelf == true)
							{
								count = count + 1;
							}
						}

						if (count >= snowmans.Length)
						{
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "���� ������ ������!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else if ((temp_Item.itemType == "������" && item.itemType == "�޺�����") || (temp_Item.itemType == "�޺�����" && item.itemType == "������"))
					{
						for (int i = 0; i < items.Count; i = i + 1)
						{
							if (items[i] == null)
							{
								items[i] = Instantiate(itemPrefab);
								items[i].transform.position = transform.position + new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, 0);
								items[i].GetComponent<Item>().itemType = "������";
								items[i].GetComponent<Item>().AfterItemTypeChange();
								break;
							}
						}

						temp_Item.itemType = "�������";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "��(��) ���۵Ǿ����ϴ�!";
						StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));

						snowmans[6].SetActive(true);
						int count = 0;
						for (int i = 0; i < snowmans.Length; i = i + 1)
						{
							if (snowmans[i].activeSelf == true)
							{
								count = count + 1;
							}
						}

						if (count >= snowmans.Length)
						{
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "���� ������ ������!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else if ((temp_Item.itemType == "������" && item.itemType == "����������") || (temp_Item.itemType == "����������" && item.itemType == "������"))
					{
						for (int i = 0; i < items.Count; i = i + 1)
						{
							if (items[i] == null)
							{
								items[i] = Instantiate(itemPrefab);
								items[i].transform.position = transform.position + new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, 0);
								items[i].GetComponent<Item>().itemType = "������";
								items[i].GetComponent<Item>().AfterItemTypeChange();
								break;
							}
						}

						temp_Item.itemType = "���������";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "��(��) ���۵Ǿ����ϴ�!";
						StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));

						snowmans[7].SetActive(true);
						int count = 0;
						for (int i = 0; i < snowmans.Length; i = i + 1)
						{
							if (snowmans[i].activeSelf == true)
							{
								count = count + 1;
							}
						}

						if (count >= snowmans.Length)
						{
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "���� ������ ������!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else if ((temp_Item.itemType == "�Թڴ����" && item.itemType == "���") || (temp_Item.itemType == "���" && item.itemType == "�Թڴ����"))
					{
						for (int i = 0; i < items.Count; i = i + 1)
						{
							if (items[i] == null)
							{
								items[i] = Instantiate(itemPrefab);
								items[i].transform.position = transform.position + new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, 0);
								items[i].GetComponent<Item>().itemType = "���";
								items[i].GetComponent<Item>().AfterItemTypeChange();
								break;
							}
						}

						temp_Item.itemType = "��Ÿ�����";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "��(��) ���۵Ǿ����ϴ�!";
						StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));

						snowmans[8].SetActive(true);
						int count = 0;
						for (int i = 0; i < snowmans.Length; i = i + 1)
						{
							if (snowmans[i].activeSelf == true)
							{
								count = count + 1;
							}
						}

						if (count >= snowmans.Length)
						{
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "���� ������ ������!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else if ((temp_Item.itemType == "�ʸ������" && item.itemType == "������") || (temp_Item.itemType == "������" && item.itemType == "�ʸ������"))
					{
						temp_Item.itemType = "3�ܴ����";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "��(��) ���۵Ǿ����ϴ�!";
						StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));

						snowmans[9].SetActive(true);
						int count = 0;
						for (int i = 0; i < snowmans.Length; i = i + 1)
						{
							if (snowmans[i].activeSelf == true)
							{
								count = count + 1;
							}
						}

						if (count >= snowmans.Length)
						{
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "���� ������ ������!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else if ((temp_Item.itemType == "���������" && item.itemType == "������") || (temp_Item.itemType == "������" && item.itemType == "���������"))
					{
						temp_Item.itemType = "�����ҳഫ���";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "��(��) ���۵Ǿ����ϴ�!";
						StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));

						snowmans[10].SetActive(true);
						int count = 0;
						for (int i = 0; i < snowmans.Length; i = i + 1)
						{
							if (snowmans[i].activeSelf == true)
							{
								count = count + 1;
							}
						}

						if (count >= snowmans.Length)
						{
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "���� ������ ������!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else
					{
						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "�������� �ʴ� ���չ��Դϴ�.";
						StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));

						for (int i = 0; i < items.Count; i = i + 1)
						{
							if (items[i] == item.gameObject)
							{
								item.transform.position = new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, item.transform.position.z);
								item = null;
								return;
							}
						}
					}
					return;
				}

				for (int i = 0; i < items.Count; i = i + 1)
				{
					if (items[i] == item.gameObject)
					{
						item.transform.position = new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, item.transform.position.z);
						item = null;
						return;
					}
				}
			}
			else
			{
				for (int i = 0; i < items.Count; i = i + 1)
				{
					if (items[i] == item.gameObject) { items[i] = null; }
				}
				items[coordinate_X + (Horizontal * -coordinate_Y)] = item.gameObject;
			}

			item.transform.position = new Vector3((coordinate_X - (Horizontal / 2)) * mapScale, (coordinate_Y + (Vertical / 2)) * mapScale, item.transform.position.z);
			item = null;
		}
	}

	IEnumerator DeActivateGameObject(GameObject p_GameObject, float p_DelayTime)
	{
		yield return new WaitForSeconds(p_DelayTime);
		p_GameObject.SetActive(false);
	}

	IEnumerator EndGame(float p_DelayTime)
	{
		yield return new WaitForSeconds(p_DelayTime);
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}