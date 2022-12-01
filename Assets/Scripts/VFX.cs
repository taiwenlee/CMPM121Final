using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    [SerializeField] GameObject Vfx;

    Vector2 mouse;

    public GameObject Vfx1 { get => Vfx; set => Vfx = value; }

    // Start is called before the first frame update
    void Start()
    {
        Vfx1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Mouse();
    }

    void Mouse()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mouse = Camera.main.ScreenToWorldPoint(input.mousePosition);
            Vfx1.SetActive(true);
            Vfx1.transform.position = new Vector3(mouse.x, mouse.y, 0f);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vfx1.SetActive(false);
        }
    }
}
