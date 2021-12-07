using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyReaction : MonoBehaviour
{
	//Variables
	float scale = 0f;
	float speed = 0f;

	public float triggeredScale = 1.0f;
	public float triggeredSpeed = 5.0f;

	private bool recalculateNormals = false;

	//Vertices
	private Vector3[] baseVertices;
	private Vector3[] vertices;
	

	void Update()
	{
		//call calculate noise 
		CalcNoise();
	}

	//calculate the noise
	void CalcNoise()
	{
		Mesh mesh = GetComponent<MeshFilter>().mesh;

		if (baseVertices == null)
			baseVertices = mesh.vertices;

		vertices = new Vector3[baseVertices.Length];

		float timex = Time.time * speed + 2.5564f;
		float timey = Time.time * speed + 1.21688f;
		float timez = Time.time * speed + 0.1365143f;

		for (int i = 0; i < vertices.Length; i++)
		{
			Vector3 vertex = baseVertices[i];
			vertex.x += Mathf.PerlinNoise(timex + vertex.x, timex + vertex.y) * scale;
			vertex.y += Mathf.PerlinNoise(timey + vertex.x, timey + vertex.y) * scale;
			vertex.z += Mathf.PerlinNoise(timez + vertex.x, timez + vertex.y) * scale;
			vertices[i] = vertex;
		}

		recalculateNormals = true;

		mesh.vertices = vertices;

		if (recalculateNormals)
		{
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();
		}
	}

	private void OnTriggerStay(Collider other) //if you are in the trigger
	{

		if (other.gameObject.CompareTag("Player")) //and are the player
		{

			if (scale < triggeredScale)
            {
				scale = scale + 0.1f * Time.deltaTime;
			}
			if(speed < triggeredSpeed)
            {
				speed = speed + 1f * Time.deltaTime;
			}
		}
	}

	private void OnTriggerExit(Collider other) //if you are in the trigger
	{

		if (other.gameObject.CompareTag("Player")) //and are the player
		{

			Calm();
		}
	}

	private void Calm()
    {
		while (scale > 0 && speed > 0)
		{
			scale = scale - 0.1f * Time.deltaTime;
			speed = speed - 1f * Time.deltaTime;
		}
		
	}
}


