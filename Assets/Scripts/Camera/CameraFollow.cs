using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	private Vector2 velocity;

	public float smoothTimeY;
	public float smoothTimeX;

	public GameObject player;

	public bool bounds;

	//This is the original bounds and size for the camera, should be the size of the current level
	[Header("Original")]
	public float originalCameraSize;
	public Vector3 originalMinCameraPos;
	public Vector3 originalMaxCameraPos;

	//These are the Vectors the camera is going to use every frame, they are meant to be replaced when a player enters a boss room (for example), and reset to the original values above when they exit.
	[Header("Replaceable")]
	public Vector3 minCameraPos;
	public Vector3 maxCameraPos;

    void Start()
    {
		minCameraPos = originalMinCameraPos;
		maxCameraPos = originalMaxCameraPos;
		originalCameraSize = GetComponent<Camera>().orthographicSize;
		GetPlayer();
    }
 
	public void GetPlayer()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

    void FixedUpdate()
    {
	    float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + 1, ref velocity.y, smoothTimeY);

		transform.position = new Vector3(posX,posY, transform.position.z);

		if(bounds)
		{
			transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x), 
			Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
			Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
		}
    }
}
