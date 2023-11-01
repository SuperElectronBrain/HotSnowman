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
		if(itemType == "´«Á¶°¢")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
		}
		else if (itemType == "³ª¹µ°¡Áö")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
		}
		else if (itemType == "Äá")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
		}
		else if (itemType == "³ª¹µÀÙ")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[3];
		}
		else if (itemType == "¸®º»²ö")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[4];
		}
		else if (itemType == "ÅÐ¹¶Ä¡")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[5];
		}
		else if (itemType == "¹«Áö°³½Ç¹¶Ä¡")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[6];
		}
		else if (itemType == "´Þºû´«²É")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[7];
		}
		else if (itemType == "º°ºû´«°¡·ç")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[8];
		}
		else if (itemType == "´ç±Ù")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[9];
		}
		else if (itemType == "¿äÁ¤º°")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[10];
		}
		else if (itemType == "ÇÔ¹Ú´«»ç¶÷")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[11];
		}
		else if (itemType == "¶Ê¸Á´«»ç¶÷")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[12];
		}
		else if (itemType == "±Ö´«»ç¶÷")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[13];
		}
		else if (itemType == "³ì´Â ´«»ç¶÷")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[14];
		}
		else if (itemType == "ÀúÁÖ´«»ç¶÷")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[15];
		}
		else if (itemType == "°ÔÀÌ¸Ó´«»ç¶÷")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[16];
		}
		else if (itemType == "Èü´«»ç¶÷")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[17];
		}
		else if (itemType == "¿À¸®´«»ç¶÷")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[18];
		}
		else if (itemType == "»êÅ¸´«»ç¶÷")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[19];
		}
		else if (itemType == "3´Ü´«»ç¶÷")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[20];
		}
		else if (itemType == "¸¶¹ý¼Ò³à´«»ç¶÷")
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[21];
		}
	}
}
