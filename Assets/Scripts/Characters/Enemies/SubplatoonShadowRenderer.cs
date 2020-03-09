using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class SubplatoonShadowRenderer : MonoBehaviour
{
    /*
    public Vector3 Offset = new Vector3(-0.1f, -0.1f);
    GameObject shadow;

    public Material Material;

    private void Start()
    {
        shadow = new GameObject("Shadow");
        shadow.transform.parent = transform;

        shadow.transform.localPosition = Offset;
        shadow.transform.localRotation = Quaternion.identity;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        SpriteRenderer sr = shadow.AddComponent<SpriteRenderer>();
        sr.sprite = renderer.sprite;
        sr.material = Material;

        sr.sortingLayerName = renderer.sortingLayerName;
        sr.sortingOrder = renderer.sortingOrder - 1;
    }

    private void Update()
    {
        shadow.transform.localPosition = Offset;
    }



    // public Vector2 offset = new Vector2(-0.1f, -0.1f);

    // private SpriteRenderer sprRndCaster;
    // private SpriteRenderer sprRndShadow;

    // private Transform transCaster;
    // private Transform transShadow;


    // public Material shadowMaterial;
    // public Color shadowColor;



    // void Start()
    // {
    //     transCaster = transform;
    //     transShadow = new GameObject().transform; //new gameobject with transform assigned to transShadow
    //     transShadow.parent = transCaster;
    //     transShadow.gameObject.name = "Shadow";
    //     transShadow.localRotation = Quaternion.identity;

    //     sprRndCaster = GetComponent<SpriteRenderer>();
    //     sprRndShadow = transShadow.gameObject.AddComponent<SpriteRenderer>();

    //     sprRndShadow.material = shadowMaterial;
    //     sprRndShadow.color = shadowColor;
    //     sprRndShadow.sortingLayerName = sprRndCaster.sortingLayerName;
    //     sprRndShadow.sortingOrder = sprRndCaster.sortingOrder - 1;
    // }

    // private void Update()
    // {
    //     shadowColor = new Color (1, 1, 1, .1f);
    // }

    // // Update is called once per frame
    // void LateUpdate() //happens right after update
    // {
    //     transShadow.position = new Vector2(transCaster.position.x + offset.x,
    //         transCaster.position.y + offset.y);
    //     sprRndShadow.sprite = sprRndCaster.sprite;
    // }
    */
}


