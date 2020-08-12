using System;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonManager : MonoBehaviour
{
	#region Fields

	[SerializeField]
	private TextMeshProUGUI label;

	private Button button;
	private UnityAction buttonAction;

	#endregion

	#region Public Methods

	public void Initialize(string label, Action<(string, string)> clickEventHandler, int progress)
	{
		float fill = 0.0f;
		if (progress == 1)
			fill = 0.333f;
		else if (progress == 2)
			fill = 0.666f;
		else if (progress == 3)
			fill = 1.0f;
		this.label.text = label;
		this.GetComponent<Image>().fillAmount = fill;
		buttonAction += () => clickEventHandler((label, label));
		button.onClick.AddListener(buttonAction);
	}

	#endregion

	#region Private Methods

	private void Awake()
	{
		button = GetComponent<Button>();
	}

	private void OnDestroy()
	{
		button.onClick.RemoveListener(buttonAction);
	}

	#endregion
}
