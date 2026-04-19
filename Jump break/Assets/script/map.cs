using UnityEngine;

public class map : MonoBehaviour
{
    [Header("Settings")]
    public Camera cam;
    public float parallaxEffectX;
    public float parallaxEffectY;

    private float length, startPosX, startPosY;

    void Start()
    {
        if (cam == null) cam = Camera.main;

        startPosX = transform.position.x;
        startPosY = transform.position.y;

        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void LateUpdate()
    {
        float distanceX = (cam.transform.position.x * parallaxEffectX);
        float tempX = (cam.transform.position.x * (1 - parallaxEffectX));

        float distanceY = (cam.transform.position.y * parallaxEffectY);

        transform.position = new Vector3(startPosX + distanceX, startPosY + distanceY, transform.position.z);

        if (tempX > startPosX + length) startPosX += length;
        else if (tempX < startPosX - length) startPosX -= length;
    }
}
