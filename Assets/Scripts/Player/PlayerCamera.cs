using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = 0f;
    public Transform target;
    private RectTransform canvasRect; // Tambahkan referensi ke RectTransform dari canvas

    private float minX, maxX, minY, maxY;
    private float camHalfHeight, camHalfWidth;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject canvasObject = GameObject.Find("Canvas");
        if (canvasObject != null)
        {
            canvasRect = canvasObject.GetComponent<RectTransform>();
            if (canvasRect != null)
            {
                Vector3[] canvasCorners = new Vector3[4];
                canvasRect.GetWorldCorners(canvasCorners);
                minX = canvasCorners[0].x;
                maxX = canvasCorners[2].x;
                minY = canvasCorners[0].y;
                maxY = canvasCorners[2].y;

                camHalfHeight = Camera.main.orthographicSize;
                camHalfWidth = camHalfHeight * Camera.main.aspect;
            }
        }
    }

    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);

        // Batasi posisi kamera berdasarkan batas-batas canvas dan ukuran kamera
        newPos.x = Mathf.Clamp(newPos.x, minX + camHalfWidth, maxX - camHalfWidth);
        newPos.y = Mathf.Clamp(newPos.y, minY + camHalfHeight, maxY - camHalfHeight);

        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}