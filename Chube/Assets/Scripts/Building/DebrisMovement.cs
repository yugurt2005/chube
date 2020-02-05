using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Random path is found by generating a random number between 0 and the perimeter of the background rectangle.
// Then, moving from the bottom left counterclockwise, it turns that number into a position on the perimeter of the rectangle.
// You get these random spots twice: one for the origin, one for the target.
// I feel bigbrained lol

public class DebrisMovement : MonoBehaviour
{
    public RectTransform bgRect;
    public GameObject bg;
    public SpriteRenderer self;

    public float speed = 0.1f;
    public float rotationSpeed = 1;
    public int rotationDirection = 1;

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

        transform.Rotate(0, 0, rotationSpeed * rotationDirection);
    }

    void setNewPath()
    {
        transform.position = getPositionOnPerimeter(Random.Range(0, perimeter));
        target = getPositionOnPerimeter(Random.Range(0, perimeter));
    }

    Vector3 getPositionOnPerimeter(float length)
    {
        Vector3 pos = new Vector3(bgRect.offsetMin.x, bgRect.offsetMin.y, 0);

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
