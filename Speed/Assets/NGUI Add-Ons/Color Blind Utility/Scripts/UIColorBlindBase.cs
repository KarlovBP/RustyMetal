using UnityEngine;

namespace ColorBlindUtility.NGUI 
{
	public abstract class UIColorBlindBase : MonoBehaviour 
	{
		protected ColorBlindMode colorBlindMode = ColorBlindMode.None;

		public virtual void Apply(ColorBlindMode mode) 
		{
			colorBlindMode = mode;
		}
	}
}