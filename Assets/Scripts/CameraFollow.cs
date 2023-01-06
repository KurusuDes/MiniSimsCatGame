using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // the transform of the player
    public float smoothTime = 0.3f; // the smooth time for the camera's movement
    public Vector3 offset; // the camera's offset from the player

    private Vector3 velocity = Vector3.zero; // the velocity of the camera's movement

    void Update()
    {
        // calculate the position the camera should be in
        Vector3 targetPosition = target.position + offset;

        // smoothly move the camera towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}