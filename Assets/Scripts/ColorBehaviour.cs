using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class ColorBehaviour : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _lineRenderer;
    private Color _greenColor = Color.green;
    private Color _redColor = Color.red;
    private Color _pinkColor = Color.magenta;
    private Material _material;
    private Color _currentColor;
    // Start is called before the first frame update
    private void Start()
    {
       _material = _lineRenderer.GetComponent<Material>();
    }

    public void SetGreenColor()
    {
        _lineRenderer.startColor = _lineRenderer.endColor =_greenColor;
        
    }
    public void SetRedColor()
    {
        _lineRenderer.startColor = _lineRenderer.endColor = _redColor;
    }
    public void SetPinkColor()
    {
        _lineRenderer.startColor = _lineRenderer.endColor = _pinkColor;
    }

    public Color GetCurrentColorLine()
    {
        return _lineRenderer.startColor = _lineRenderer.endColor;
    }
}
