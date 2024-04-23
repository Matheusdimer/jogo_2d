using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{

    [SerializeField]
    private GameObject bandeira;

    private Renderer _bandeiraRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _bandeiraRenderer = bandeira.GetComponent<Renderer>();
        _bandeiraRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 Check()
    {
        _bandeiraRenderer.enabled = true;
        return new Vector3(transform.position.x, transform.position.y + 1f, transform.position.y);
    }
}
