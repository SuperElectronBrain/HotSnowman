using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{
	[SerializeField] private string buttonName;

	// Start is called before the first frame update
	void Start()
	{
		if (buttonName == null)
		{
			buttonName = "";
		}

		if (buttonName == "GameStartButton")
		{
			GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene"); });
		}
		else if (buttonName == "GoToRoomButton")
		{
			GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
			{
				Vector3 temp_Vector = GetMapPosition("Room");
				Camera.main.transform.position = new Vector3(temp_Vector.x, temp_Vector.y, temp_Vector.z - 10.0f);
			});
		}
		else if (buttonName == "GoToSnowFieldButton")
		{
			GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => 
			{
				Vector3 temp_Vector = GetMapPosition("SnowFeild");
				Camera.main.transform.position = new Vector3(temp_Vector.x, temp_Vector.y, temp_Vector.z - 10.0f);
			});
		}
		else if (buttonName == "IllustratedBookButton")
		{
			GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
			{
				Vector3 temp_Vector = GetMapPosition("IllustratedBook");
				Camera.main.transform.position = new Vector3(temp_Vector.x, temp_Vector.y, temp_Vector.z - 10.0f);
			});
		}
		else if (buttonName == "GameQuitButton")
		{
			GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
			{
				#if UNITY_EDITOR
					UnityEditor.EditorApplication.isPlaying = false;
				#else
					Application.Quit();
				#endif
			});
		}
	}

	Vector3 GetMapPosition(string p_MapName = "")
	{
		GameObject tmep_mapSet = GameObject.Find("MapSet");
		if (tmep_mapSet != null)
		{
			for(int i = 0; i < tmep_mapSet.transform.childCount; i = i + 1)
			{
				Transform tmep_MapSetChild = tmep_mapSet.transform.GetChild(i);
				if (tmep_MapSetChild.name == p_MapName)
				{
					return tmep_MapSetChild.position;
				}
			}
		}

		return Vector3.zero;
	}
}
