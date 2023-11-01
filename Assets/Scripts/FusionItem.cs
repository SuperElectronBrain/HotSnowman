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

			if (i == 0) { items[i].GetComponent<Item>().itemType = "´«Á¶°¢"; }
			else if (i == 1) { items[i].GetComponent<Item>().itemType = "³ª¹µ°¡Áö"; }
			else if (i == 2) { items[i].GetComponent<Item>().itemType = "´ç±Ù"; }
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
					if((temp_Item.itemType == "´«Á¶°¢" && item.itemType == "³ª¹µ°¡Áö") || (temp_Item.itemType == "³ª¹µ°¡Áö" && item.itemType == "´«Á¶°¢"))
					{
						for (int i = 0; i < items.Count; i = i + 1)
						{
							if (items[i] == null)
							{
								items[i] = Instantiate(itemPrefab);
								items[i].transform.position = transform.position + new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, 0);
								items[i].GetComponent<Item>().itemType = "´«Á¶°¢";
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
								items[i].GetComponent<Item>().itemType = "³ª¹µ°¡Áö";
								items[i].GetComponent<Item>().AfterItemTypeChange();
								break;
							}
						}

						temp_Item.itemType = "ÇÔ¹Ú´«»ç¶÷";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "ÀÌ(°¡) Á¦ÀÛµÇ¾ú½À´Ï´Ù!";
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
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "µµ°¨ ¼öÁýÀÌ ³¡³µ´Ù!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else if((temp_Item.itemType == "´«Á¶°¢" && item.itemType == "Äá") || (temp_Item.itemType == "Äá" && item.itemType == "´«Á¶°¢"))
					{
						for (int i = 0; i < items.Count; i = i + 1)
						{
							if (items[i] == null)
							{
								items[i] = Instantiate(itemPrefab);
								items[i].transform.position = transform.position + new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, 0);
								items[i].GetComponent<Item>().itemType = "´«Á¶°¢";
								items[i].GetComponent<Item>().AfterItemTypeChange();
								break;
							}
						}

						temp_Item.itemType = "¶Ê¸Á´«»ç¶÷";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "ÀÌ(°¡) Á¦ÀÛµÇ¾ú½À´Ï´Ù!";
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
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "µµ°¨ ¼öÁýÀÌ ³¡³µ´Ù!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else if ((temp_Item.itemType == "´«Á¶°¢" && item.itemType == "³ª¹µÀÙ") || (temp_Item.itemType == "³ª¹µÀÙ" && item.itemType == "´«Á¶°¢"))
					{
						for (int i = 0; i < items.Count; i = i + 1)
						{
							if (items[i] == null)
							{
								items[i] = Instantiate(itemPrefab);
								items[i].transform.position = transform.position + new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, 0);
								items[i].GetComponent<Item>().itemType = "´«Á¶°¢";
								items[i].GetComponent<Item>().AfterItemTypeChange();
								break;
							}
						}

						temp_Item.itemType = "±Ö´«»ç¶÷";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "ÀÌ(°¡) Á¦ÀÛµÇ¾ú½À´Ï´Ù!";
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
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "µµ°¨ ¼öÁýÀÌ ³¡³µ´Ù!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else if ((temp_Item.itemType == "´«Á¶°¢" && item.itemType == "¸®º»²ö") || (temp_Item.itemType == "¸®º»²ö" && item.itemType == "´«Á¶°¢"))
					{
						for (int i = 0; i < items.Count; i = i + 1)
						{
							if (items[i] == null)
							{
								items[i] = Instantiate(itemPrefab);
								items[i].transform.position = transform.position + new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, 0);
								items[i].GetComponent<Item>().itemType = "´«Á¶°¢";
								items[i].GetComponent<Item>().AfterItemTypeChange();
								break;
							}
						}

						temp_Item.itemType = "³ì´Â ´«»ç¶÷";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "ÀÌ(°¡) Á¦ÀÛµÇ¾ú½À´Ï´Ù!";
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
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "µµ°¨ ¼öÁýÀÌ ³¡³µ´Ù!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else if ((temp_Item.itemType == "´«Á¶°¢" && item.itemType == "ÅÐ¹¶Ä¡") || (temp_Item.itemType == "ÅÐ¹¶Ä¡" && item.itemType == "´«Á¶°¢"))
					{
						for (int i = 0; i < items.Count; i = i + 1)
						{
							if (items[i] == null)
							{
								items[i] = Instantiate(itemPrefab);
								items[i].transform.position = transform.position + new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, 0);
								items[i].GetComponent<Item>().itemType = "´«Á¶°¢";
								items[i].GetComponent<Item>().AfterItemTypeChange();
								break;
							}
						}

						temp_Item.itemType = "ÀúÁÖ´«»ç¶÷";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "ÀÌ(°¡) Á¦ÀÛµÇ¾ú½À´Ï´Ù!";
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
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "µµ°¨ ¼öÁýÀÌ ³¡³µ´Ù!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else if ((temp_Item.itemType == "´«Á¶°¢" && item.itemType == "¹«Áö°³½Ç¹¶Ä¡") || (temp_Item.itemType == "¹«Áö°³½Ç¹¶Ä¡" && item.itemType == "´«Á¶°¢"))
					{
						for (int i = 0; i < items.Count; i = i + 1)
						{
							if (items[i] == null)
							{
								items[i] = Instantiate(itemPrefab);
								items[i].transform.position = transform.position + new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, 0);
								items[i].GetComponent<Item>().itemType = "´«Á¶°¢";
								items[i].GetComponent<Item>().AfterItemTypeChange();
								break;
							}
						}

						temp_Item.itemType = "°ÔÀÌ¸Ó´«»ç¶÷";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "ÀÌ(°¡) Á¦ÀÛµÇ¾ú½À´Ï´Ù!";
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
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "µµ°¨ ¼öÁýÀÌ ³¡³µ´Ù!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else if ((temp_Item.itemType == "´«Á¶°¢" && item.itemType == "´Þºû´«²É") || (temp_Item.itemType == "´Þºû´«²É" && item.itemType == "´«Á¶°¢"))
					{
						for (int i = 0; i < items.Count; i = i + 1)
						{
							if (items[i] == null)
							{
								items[i] = Instantiate(itemPrefab);
								items[i].transform.position = transform.position + new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, 0);
								items[i].GetComponent<Item>().itemType = "´«Á¶°¢";
								items[i].GetComponent<Item>().AfterItemTypeChange();
								break;
							}
						}

						temp_Item.itemType = "Èü´«»ç¶÷";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "ÀÌ(°¡) Á¦ÀÛµÇ¾ú½À´Ï´Ù!";
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
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "µµ°¨ ¼öÁýÀÌ ³¡³µ´Ù!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else if ((temp_Item.itemType == "´«Á¶°¢" && item.itemType == "º°ºû´«°¡·ç") || (temp_Item.itemType == "º°ºû´«°¡·ç" && item.itemType == "´«Á¶°¢"))
					{
						for (int i = 0; i < items.Count; i = i + 1)
						{
							if (items[i] == null)
							{
								items[i] = Instantiate(itemPrefab);
								items[i].transform.position = transform.position + new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, 0);
								items[i].GetComponent<Item>().itemType = "´«Á¶°¢";
								items[i].GetComponent<Item>().AfterItemTypeChange();
								break;
							}
						}

						temp_Item.itemType = "¿À¸®´«»ç¶÷";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "ÀÌ(°¡) Á¦ÀÛµÇ¾ú½À´Ï´Ù!";
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
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "µµ°¨ ¼öÁýÀÌ ³¡³µ´Ù!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else if ((temp_Item.itemType == "ÇÔ¹Ú´«»ç¶÷" && item.itemType == "´ç±Ù") || (temp_Item.itemType == "´ç±Ù" && item.itemType == "ÇÔ¹Ú´«»ç¶÷"))
					{
						for (int i = 0; i < items.Count; i = i + 1)
						{
							if (items[i] == null)
							{
								items[i] = Instantiate(itemPrefab);
								items[i].transform.position = transform.position + new Vector3(((i % Vertical) - (Horizontal / 2)) * mapScale, ((-i / Horizontal) + (Vertical / 2)) * mapScale, 0);
								items[i].GetComponent<Item>().itemType = "´ç±Ù";
								items[i].GetComponent<Item>().AfterItemTypeChange();
								break;
							}
						}

						temp_Item.itemType = "»êÅ¸´«»ç¶÷";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "ÀÌ(°¡) Á¦ÀÛµÇ¾ú½À´Ï´Ù!";
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
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "µµ°¨ ¼öÁýÀÌ ³¡³µ´Ù!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else if ((temp_Item.itemType == "¶Ê¸Á´«»ç¶÷" && item.itemType == "³ª¹µÀÙ") || (temp_Item.itemType == "³ª¹µÀÙ" && item.itemType == "¶Ê¸Á´«»ç¶÷"))
					{
						temp_Item.itemType = "3´Ü´«»ç¶÷";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "ÀÌ(°¡) Á¦ÀÛµÇ¾ú½À´Ï´Ù!";
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
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "µµ°¨ ¼öÁýÀÌ ³¡³µ´Ù!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else if ((temp_Item.itemType == "¿À¸®´«»ç¶÷" && item.itemType == "¿äÁ¤º°") || (temp_Item.itemType == "¿äÁ¤º°" && item.itemType == "¿À¸®´«»ç¶÷"))
					{
						temp_Item.itemType = "¸¶¹ý¼Ò³à´«»ç¶÷";
						temp_Item.AfterItemTypeChange();
						Destroy(item.gameObject);

						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_Item.itemType + "ÀÌ(°¡) Á¦ÀÛµÇ¾ú½À´Ï´Ù!";
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
							popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "µµ°¨ ¼öÁýÀÌ ³¡³µ´Ù!!";
							StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							fairy.SetActive(true);
							StartCoroutine(EndGame(2.0f));
						}
					}
					else
					{
						popupWindow.SetActive(true);
						popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Á¸ÀçÇÏÁö ¾Ê´Â Á¶ÇÕ¹ýÀÔ´Ï´Ù.";
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