using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemType;
    [SerializeField] private Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
		AfterItemTypeChange();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void AfterItemTypeChange()
	{
		if(itemType == "������")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
		}
		else if (itemType == "��������")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
		}
		else if (itemType == "��")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
		}
		else if (itemType == "������")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[3];
		}
		else if (itemType == "������")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[4];
		}
		else if (itemType == "�й�ġ")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[5];
		}
		else if (itemType == "�������ǹ�ġ")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[6];
		}
		else if (itemType == "�޺�����")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[7];
		}
		else if (itemType == "����������")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[8];
		}
		else if (itemType == "���")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[9];
		}
		else if (itemType == "������")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[10];
		}
		else if (itemType == "�Թڴ����")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[11];
		}
		else if (itemType == "�ʸ������")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[12];
		}
		else if (itemType == "�ִ����")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[13];
		}
		else if (itemType == "��� �����")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[14];
		}
		else if (itemType == "���ִ����")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[15];
		}
		else if (itemType == "���̸Ӵ����")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[16];
		}
		else if (itemType == "�������")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[17];
		}
		else if (itemType == "���������")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[18];
		}
		else if (itemType == "��Ÿ�����")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[19];
		}
		else if (itemType == "3�ܴ����")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[20];
		}
		else if (itemType == "�����ҳഫ���")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[21];
		}
	}
}
