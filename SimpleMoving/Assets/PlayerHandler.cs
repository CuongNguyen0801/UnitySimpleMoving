using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private VariableJoystick joystick;

    private float moveDistance = 0.3f; // move distance each frame

    // Start is called before the first frame update
    void Start()
    {
        moveDistance = moveSpeed * Time.deltaTime;
        // only show joystick for mobile
        #if UNITY_ANDROID || UNITY_IOS
        joystick.gameObject.SetActive(true);
        #else
        joystick.gameObject.SetActive(false);
        #endif
    }

    void FixedUpdate()
    {
        // 1. Get move left/right/top/bottom value
        var horizontalMove = Input.GetAxis("Horizontal");
        var verticalMove = Input.GetAxis("Vertical");

        #if UNITY_ANDROID || UNITY_IOS
        horizontalMove = joystick.Horizontal;
        verticalMove = joystick.Vertical;
        #endif

        float moveX = 0;
        
        if (horizontalMove != 0)
        {
            moveX = horizontalMove < 0 ? -moveDistance : moveDistance;
        }
        float moveY = 0;
        if (verticalMove != 0)
        {
            moveY = verticalMove < 0 ? -moveDistance : moveDistance;
        }
        // 2. Move player transform
        if (moveX != 0 || moveY != 0)
        {
            var pos = playerTransform.position;
            playerTransform.position = new Vector3(pos.x + moveX, pos.y + moveY, pos.z);
        }
    }
}
