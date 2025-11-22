using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform followerTransform;
    public Transform followingTransform;

    Vector3 basePosition;
    Quaternion baseRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Follower" + followerTransform.position.ToString("F4") + " Rotation: " + followerTransform.rotation.eulerAngles.ToString("F4"));
        Debug.Log("Base" + followingTransform.position.ToString("F4") + " Rotation: " + followingTransform.rotation.eulerAngles.ToString("F4"));
        basePosition = followerTransform.position;
        baseRotation = followerTransform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Following Transform Position: " + followingTransform.position.ToString("F4") + " Rotation: " + followingTransform.rotation.eulerAngles.ToString("F4"));
        followerTransform.position = basePosition + followingTransform.position;
        followerTransform.rotation = baseRotation * followingTransform.rotation;
    }
}
