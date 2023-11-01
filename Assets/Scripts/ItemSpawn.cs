using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class RefVector3
{
	public Vector3 vector;

	public RefVector3() { vector = Vector3.zero; }
	public RefVector3(Vector3 p_vector) { vector = p_vector; }
}

public class ItemSpawn : MonoBehaviour
{
	[SerializeField] private GameObject popupWindow;
	[SerializeField] private GameObject[] items;
    [SerializeField] private GameObject[] collectingButtons;
	private List<RefVector3> targetPosition;

	// Start is called before the first frame update
	void Start()
    {
        for(int i = 0; i < collectingButtons.Length; i = i + 1)
        {
			GameObject temp_GameObject = collectingButtons[i];
			if (targetPosition == null) { targetPosition = new List<RefVector3>(); }
			targetPosition.Add(new RefVector3(new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-0.5f, -4.5f), 0)));
			RefVector3 temp_Vector = targetPosition[i];

			temp_GameObject.GetComponent<RectTransform>().anchoredPosition = ConvertWorldPositionToUICanvasPosition(temp_Vector.vector + transform.position);
			StartCoroutine(ActivateGameObject(temp_GameObject, i));

			temp_GameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
			{
				if(items.Length > 0)
				{
					FusionItem temp_ItemStorage = GetItemStorage();

					for (int i = 0; i < temp_ItemStorage.items.Count; i = i + 1)
					{
						if (temp_ItemStorage.items[i] == null)
						{
							temp_ItemStorage.items[i] = Instantiate(items[Random.Range(0, items.Length)]);
							temp_ItemStorage.items[i].transform.position = temp_ItemStorage.transform.position + new Vector3
							(
								((i % temp_ItemStorage.Vertical) - (temp_ItemStorage.Horizontal / 2)) * temp_ItemStorage.mapScale,
								((-i / temp_ItemStorage.Horizontal) + (temp_ItemStorage.Vertical / 2)) * temp_ItemStorage.mapScale,
								0
							);

							int randNum = Random.Range(0, 100);
							if(randNum < 20)
							{
								temp_ItemStorage.items[i].GetComponent<Item>().itemType = "콩";
								popupWindow.SetActive(true);
								popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_ItemStorage.items[i].GetComponent<Item>().itemType + "을(를) 얻었다!";
								StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							}
							else if (randNum < 40)
							{
								temp_ItemStorage.items[i].GetComponent<Item>().itemType = "나뭇잎";
								popupWindow.SetActive(true);
								popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_ItemStorage.items[i].GetComponent<Item>().itemType + "을(를) 얻었다!";
								StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							}
							else if (randNum < 55)
							{
								temp_ItemStorage.items[i].GetComponent<Item>().itemType = "리본끈";
								popupWindow.SetActive(true);
								popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_ItemStorage.items[i].GetComponent<Item>().itemType + "을(를) 얻었다!";
								StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));

							}
							else if (randNum < 70)
							{
								temp_ItemStorage.items[i].GetComponent<Item>().itemType = "털뭉치";
								popupWindow.SetActive(true);
								popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_ItemStorage.items[i].GetComponent<Item>().itemType + "을(를) 얻었다!";
								StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							}
							else if (randNum < 80)
							{
								temp_ItemStorage.items[i].GetComponent<Item>().itemType = "무지개실뭉치";
								popupWindow.SetActive(true);
								popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_ItemStorage.items[i].GetComponent<Item>().itemType + "을(를) 얻었다!";
								StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							}
							else if (randNum < 90)
							{
								temp_ItemStorage.items[i].GetComponent<Item>().itemType = "달빛눈꽃";
								popupWindow.SetActive(true);
								popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_ItemStorage.items[i].GetComponent<Item>().itemType + "을(를) 얻었다!";
								StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							}
							else if (randNum < 95)
							{
								temp_ItemStorage.items[i].GetComponent<Item>().itemType = "별빛눈가루";
								popupWindow.SetActive(true);
								popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_ItemStorage.items[i].GetComponent<Item>().itemType + "을(를) 얻었다!";
								StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							}
							else
							{
								temp_ItemStorage.items[i].GetComponent<Item>().itemType = "요정별";
								popupWindow.SetActive(true);
								popupWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = temp_ItemStorage.items[i].GetComponent<Item>().itemType + "을(를) 얻었다!";
								StartCoroutine(DeActivateGameObject(popupWindow, 2.0f));
							}
							temp_ItemStorage.items[i].GetComponent<Item>().AfterItemTypeChange();

							break;
						}
					}
				}

				temp_Vector.vector = new Vector3(Random.Range(-8.8862f, 8.8862f), Random.Range(0f, -5.0f), 0);
				temp_GameObject.GetComponent<RectTransform>().anchoredPosition = ConvertWorldPositionToUICanvasPosition(temp_Vector.vector + transform.position);

				temp_GameObject.SetActive(false);
				StartCoroutine(ActivateGameObject(temp_GameObject, 3.0f));
            });
		}
	}

	private void FixedUpdate()
	{
		for(int i = 0; i < collectingButtons.Length; i = i + 1)
		{
			collectingButtons[i].GetComponent<RectTransform>().anchoredPosition = ConvertWorldPositionToUICanvasPosition(targetPosition[i].vector + transform.position);
		}
	}

	IEnumerator ActivateGameObject(GameObject p_GameObject, float p_DelayTime)
    {
		yield return new WaitForSeconds(p_DelayTime);
		p_GameObject.SetActive(true);
	}

	IEnumerator DeActivateGameObject(GameObject p_GameObject, float p_DelayTime)
	{
		yield return new WaitForSeconds(p_DelayTime);
		p_GameObject.SetActive(false);
	}

	Vector2 ConvertWorldPositionToUICanvasPosition(Vector3 p_targetPosition)
	{
		RectTransform CanvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
		Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(p_targetPosition);
		Vector2 WorldObject_ScreenPosition = new Vector2
		(
			((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
			((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f))
		);
		return WorldObject_ScreenPosition;
	}

	FusionItem GetItemStorage()
	{
		FusionItem temp_ItemStorage = FindObjectOfType<FusionItem>();
		if(temp_ItemStorage != null)
		{
			return temp_ItemStorage;
		}

		return null;
	}
}
