using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisMovement : MonoBehaviour
{
    public RectTransform bgRect;
    public GameObject bg;
    public SpriteRenderer self;

    public float speed = 0.1f;

    private Vector3 target;
    private float perimeter;
    private float width;
    private float height;

    void Start()
    {
        width = bgRect.rect.width;
        height = bgRect.rect.height;
        perimeter = 2 * (width + height);

        setNewPath();
    }

    void Update()
    {
        if (transform.position != target) transform.position = Vector3.MoveTowards(transform.position, target, speed);
        else
        {
            self.enabled = true;
            setNewPath();
        }
    }

    void setNewPath()
    {
        transform.position = getPositionOnPerimeter(Random.Range(0, perimeter));
        target = getPositionOnPerimeter(Random.Range(0, perimeter));
    }

    Vector3 getPositionOnPerimeter(float length)
    {
        Vector3 pos =new Vector3(bgRect.offsetMin.x, bgRect.offsetMin.y, 0);

        if (length <= width)
        {
            pos.x += length;
        }
        else if (length <= width + height)
        {
            pos.x += width;
            pos.y += length - width;
        }
        else if (length <= width * 2 + height)
        {
            pos.x += length - width - height;
            pos.y += height;
        }
        else
        {
            pos.y += length - width * 2 - height;
        }

        return pos;
    }
}
