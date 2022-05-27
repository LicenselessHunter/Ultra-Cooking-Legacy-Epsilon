using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private PlayerMovement player;

    void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
    }
    private void Update()
    {
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.transform.parent.rotation.z * 1.0f);
    }

}
