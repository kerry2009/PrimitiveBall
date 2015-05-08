using UnityEngine;
using System.Collections;

public class MaterialGradient : MonoBehaviour {
	public Gradient gradient;
	public float timeMultiplier;

	private Color curColor;
	private float time;
	private Material material;

	// Use this for initialization
	void Start () {
		material = GetComponent<Material>();
		material.SetColor ("_TintColor", Color.white);
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime * timeMultiplier;
		curColor = gradient.Evaluate(time);
		material.SetColor ("_TintColor", curColor);
	}
}
