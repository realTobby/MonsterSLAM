
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ViewCredits : MonoBehaviour
{
	public GameObject TextMeshProObject;
	[Range(0,100)]
	public float ScrollTextSpeed = 20;
	[Range(0,1000)]
	public int ScrollTextPadding = 100;
	private float screenSizeWidth;
	private float screenSizeHeigth;
	private List<CreditItem> _creditItems = new List<CreditItem>();

	private CreditsReader cReader = new CreditsReader();

	// Start is called before the first frame update
	async void Awake()
	{
		screenSizeWidth = GetComponent<Canvas>().renderingDisplaySize.x;
		screenSizeHeigth = GetComponent<Canvas>().renderingDisplaySize.y;

		_creditItems.Add(new CreditItem(TextMeshProObject, transform, screenSizeWidth, "MonsterSLAM",""));
		_creditItems.Add(new CreditItem(TextMeshProObject, transform, screenSizeWidth,"For The Monster Mash Game Jam",""));
		_creditItems.Add(new CreditItem(TextMeshProObject, transform, screenSizeWidth,"GitHub: https://github.com/realTobby/MonsterSLAM","https://github.com/realTobby/MonsterSLAM"));
		_creditItems.Add(new CreditItem(TextMeshProObject, transform, screenSizeWidth,"Itch.io: https://itch.io/jam/the-monster-mash-jam","https://itch.io/jam/the-monster-mash-jam"));

		
		var credits_list = cReader.GetCredits();
		foreach(var item in credits_list)
		{
			_creditItems.Add(new CreditItem(TextMeshProObject, transform, screenSizeWidth, item.Description + ": " + item.Author, item.Source));
		}


		CalcAllCreditItemPosition();
	}
	// Update is called once per frame
	void Update()
	{
		foreach (var item in _creditItems)
		{
			item.SetHorizontalPosition(item.GetHorizontalPosition() + Time.deltaTime * 500.0f * (ScrollTextSpeed / 100.0f));
		}

		if (_creditItems.Count > 0)
		{
			if (_creditItems[_creditItems.Count - 1].GetHorizontalPosition() > screenSizeHeigth)
				CalcAllCreditItemPosition();
		}
	}

	private void CalcAllCreditItemPosition()
	{
		float startPosition = -150.0f;
		foreach(var item in _creditItems)
		{
			item.SetHorizontalPosition(startPosition);
			startPosition -= ScrollTextPadding;
		}
	}

	public void ClickedBackButton()
	{
		SceneManager.LoadScene(0);
	}
}
