using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithTime : MonoBehaviour
{
	public float timer;

    void Update()
    {
        StartCoroutine(DestroyOverTime());
    }

	IEnumerator DestroyOverTime()
	{
		yield return new WaitForSeconds(timer);
		Destroy(gameObject);
	}
}
