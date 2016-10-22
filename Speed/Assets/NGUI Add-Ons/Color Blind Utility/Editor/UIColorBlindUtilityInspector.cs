using UnityEngine;
using UnityEditor;

namespace ColorBlindUtility.NGUI 
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(UIColorBlindUtility), true)]
	public class UIColorBlindUtilityInspector : Editor 
	{
		private UIColorBlindUtility colorBlindUtility;
		
		void Awake() 
		{
			colorBlindUtility = (UIColorBlindUtility)target;
		}
		
		public override void OnInspectorGUI()
		{
			EditorGUILayout.Separator();
			colorBlindUtility.colorBlindMode = (ColorBlindMode)EditorGUILayout.EnumPopup("Color-Blind Mode", colorBlindUtility.colorBlindMode);
			EditorGUILayout.Separator();
		}
	}
}