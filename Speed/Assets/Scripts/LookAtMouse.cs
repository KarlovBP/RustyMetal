using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour
{
	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast (ray, out hit))
		{
			Vector3 rot = transform.eulerAngles;
			transform.LookAt(hit.point);
			transform.eulerAngles = new Vector3(rot.x, transform.eulerAngles.y-90, rot.z);
		}
	}
}