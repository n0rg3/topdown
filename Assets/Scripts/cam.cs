using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    Transform player;
    private Vector3 cameraOffset;
    [Range(0,1)]
    public float smoothness;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.Player.transform;
        cameraOffset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = player.transform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothness);
    }
}
