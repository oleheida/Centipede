using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    [SerializeField] float backgroundScrollSpeed = 0.2f;
    Material bgMaterial;
    Vector2 offset;

    void Start()
    {
        bgMaterial = GetComponent<Renderer>().material; //getting the material of my background
        offset = new Vector2(0f, backgroundScrollSpeed); //setting value for offset
    }

    void Update()
    {
        bgMaterial.mainTextureOffset += offset * Time.deltaTime; //setting up movement for bg
    }
}
