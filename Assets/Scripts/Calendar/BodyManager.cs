using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class BodyManager : MonoBehaviour
{
	#region Fields

	[SerializeField]
	private GameObject buttonPrefab;

	[SerializeField]
	private GameObject placeHolderPrefab;

	private List<GameObject> cells;

	#endregion

	#region Public Methods

	public void Initialize(int year, int month, Action<(string, string)> clickEventHandler)
	{
		var dateTime = new DateTime(year, month, 1);
		var daysInMonth = DateTime.DaysInMonth(year, month);

		var dayOfWeek = (int)dateTime.DayOfWeek;
		var size = (dayOfWeek + daysInMonth) / 7;

		if ((dayOfWeek + daysInMonth) % 7 > 0)
			size++;

		var arr = new int[size * 7];

		for (var i = 0; i < daysInMonth; i++)
			if (dayOfWeek == 0)
				arr[dayOfWeek + i] = i + 1;
			else
				arr[dayOfWeek + i - 1] = i + 1; // el - 1 en el index causa que el primer día de la semana sea lunes en vez de domingo 
		if (cells == null)
			cells = new List<GameObject>();

		foreach(var c in cells)
			Destroy(c);

		cells.Clear();

		foreach(var a in arr)
		{
			var instance = Instantiate(a == 0 ? placeHolderPrefab : buttonPrefab, transform);
			var buttonManager = instance.GetComponent<ButtonManager>();
			int fill = PlayerPrefs.GetInt(DateTime.Today.ToString());
			if (buttonManager != null)
				buttonManager.Initialize(a.ToString(), clickEventHandler, fill);
			cells.Add(instance);
		}
	}

	#endregion
}
