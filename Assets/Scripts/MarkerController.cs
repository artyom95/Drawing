using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MarkerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefabMarker;
    [SerializeField] 
    private ColorBehaviour _colorBehaviour;

     
    private Collider _collider;
    
    
    
    private GameObject _prefab ;
    private bool _isPrefabMarkerInstantiate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Instantiate()
    {
        if (!_isPrefabMarkerInstantiate)
        {
            _prefab = Instantiate(_prefabMarker);
            _isPrefabMarkerInstantiate = true;
        }
        SetColor();

    }

    private void SetColor()
    {
        var renderer = _prefab.GetComponent<Renderer>(); 
        renderer.material.color = _colorBehaviour.GetCurrentColorLine();
    }
    
    public void SetPositionPrefab()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPosition = default;
        if (_isPrefabMarkerInstantiate)
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                worldPosition = hitInfo.point;
            }

            _prefab.transform.position = new Vector3(worldPosition.x + 0.2f, worldPosition.y - 0.6f, worldPosition.z);
          // HideMarker();
        }
    }
    
    // реализовать сокрытие маркера при наведении на кнопку
    private void HideMarker()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (_collider.Raycast(ray, out  RaycastHit hitInfo, 1000 ))     // collider  is null !!!
        {
            var collider = hitInfo.collider.GetComponentsInChildren<Collider>();

           foreach (var VARIABLE in collider)
           {
               
               if (VARIABLE.tag == "Button")
               {
                   _prefab.SetActive(false);
               } 
           }   
        }
        _prefab.SetActive(true);
        
    }

    public GameObject GetPrefab()
    {
        return _prefab;
    }
}
