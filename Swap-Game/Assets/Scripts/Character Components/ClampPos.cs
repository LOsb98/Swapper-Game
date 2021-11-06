using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampPos : MonoBehaviour
{
    private Vector2 screenBounds;
    private Vector2 charSize;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        charSize = GetComponent<BoxCollider2D>().size / 2;
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + charSize.x, screenBounds.x - charSize.x);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y + charSize.y, screenBounds.y - charSize.y);
        transform.position = viewPos;
    }
}
