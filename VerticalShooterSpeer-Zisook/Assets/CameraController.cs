using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private float drag;
    private Vector3 camMove = Vector3.zero;
    private Vector3 camVelocity = Vector3.zero;

    // Update is called once per frame
    void FixedUpdate()
    {
        camMove = (player.transform.position - transform.position).normalized;

        camVelocity += camMove;
        camVelocity.Scale(new Vector3(drag, drag, 0));
        transform.position += camVelocity;
    }
}
