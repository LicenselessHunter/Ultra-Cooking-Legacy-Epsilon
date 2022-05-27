using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelocateCamera : MonoBehaviour
{
    public bool canMoveVertical;
    public bool canMoveHorizontal;
    public Vector3 newPosition;
    public float newCameraSize;
    public float lerp = 0f;
    public float duration = 20f;
    bool returning;

    [Header("Foreign Camera components")]
    CameraFollow CameraScript;
    Camera CameraComponent;

    void Start()
    {
        GameObject Camera = GameObject.Find("Main Camera");
        CameraScript = Camera.GetComponent<CameraFollow>();
        CameraComponent = Camera.GetComponent<Camera>();
    }

    void Update()
    {
        if(returning)
        {
            lerp += Time.deltaTime / duration;
            CameraScript.minCameraPos = Vector3.Lerp(CameraScript.minCameraPos, CameraScript.originalMinCameraPos, lerp);
            CameraScript.maxCameraPos = Vector3.Lerp(CameraScript.maxCameraPos, CameraScript.originalMaxCameraPos, lerp);
            CameraComponent.orthographicSize = Mathf.Lerp(CameraComponent.orthographicSize, CameraScript.originalCameraSize, lerp);
        }
    }

    IEnumerator InterpolateBackToOriginal()
    {
        returning = true;
        yield return new WaitForSeconds(duration);
        returning = false;
        lerp = 0;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.name == "Player")
        {
            lerp += Time.deltaTime / duration;
            if(newCameraSize != 0)
            {
                float a = CameraComponent.orthographicSize;
                CameraComponent.orthographicSize = Mathf.Lerp(a, newCameraSize, lerp);
            }
            if(canMoveVertical)
            {
                float a = CameraScript.minCameraPos.x;
                float b = CameraScript.maxCameraPos.x;
                CameraScript.minCameraPos.x = Mathf.Lerp(a, newPosition.x, lerp);
                CameraScript.maxCameraPos.x = Mathf.Lerp(b, newPosition.x, lerp);
            }
            if(canMoveHorizontal)
            {
                CameraScript.minCameraPos.y = newPosition.y;
                CameraScript.maxCameraPos.y = newPosition.y;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.name == "Player")
        {
            lerp = 0;
            StartCoroutine(InterpolateBackToOriginal());
        }
    }
}