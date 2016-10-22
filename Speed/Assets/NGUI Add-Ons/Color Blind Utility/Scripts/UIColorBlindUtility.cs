using UnityEngine;
using System;
using System.Collections.Generic;

namespace ColorBlindUtility.NGUI 
{
	[AddComponentMenu("NGUI/Add-ons/Color-Blind Support/Color-Blind Utility")]
	public class UIColorBlindUtility : MonoBehaviour 
	{
		public ColorBlindMode colorBlindMode = ColorBlindMode.None;

		[System.NonSerialized]
		public int currentIndex = 0;

		public Action<ColorBlindMode> OnColorBlindModeChangeEvent;

		private void OnEnable() 
		{
			foreach (UIColorBlindBase element in GetComponentsInChildren<UIColorBlindBase>()) 
			{
				OnColorBlindModeChangeEvent += element.Apply;
			}
		}

		private void OnDisable() 
		{
			foreach (UIColorBlindBase element in GetComponentsInChildren<UIColorBlindBase>()) 
			{
				OnColorBlindModeChangeEvent -= element.Apply;
			}
		}

		/// <summary>
		/// Changes the current color-blind mode, useful for changing the state programatically or through in-game settings. 
		/// (0 = None, 1 = Protanopia, 2 = Deuteranopia, 3 = Tritanopia).
		/// </summary>
		public void SetColorBlindIndex(int index) 
		{
			int newIndex = Mathf.Clamp(index, 0, 3);
			colorBlindMode = (ColorBlindMode)newIndex;
			currentIndex = newIndex;
		}

		private void Update() 
		{
			if (currentIndex != (int)colorBlindMode) 
			{
				if (OnColorBlindModeChangeEvent != null) 
				{
					OnColorBlindModeChangeEvent(colorBlindMode);
				}
				currentIndex = (int)colorBlindMode;
			}
		}
	}
}