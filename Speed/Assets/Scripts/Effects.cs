using UnityEngine;
using System.Collections;

public class Effects : MonoBehaviour
{
	public Gradient gradient;
	float time;
	public float LifeTime;
	public float ScaleModifier;

	void Awake ()
	{
		transform.rotation = transform.rotation * Quaternion.Euler (0,0,Random.Range(0,360));
	}

	void Update ()
	{
		time += Time.deltaTime;
		transform.GetComponent<MeshRenderer> ().material.SetColor ("_TintColor",gradient.Evaluate(time/LifeTime));
		transform.localScale += new Vector3 (ScaleModifier/2, ScaleModifier/2, ScaleModifier);
		if (time / LifeTime > 1)
		{
			Destroy (gameObject);
		}
	}
}