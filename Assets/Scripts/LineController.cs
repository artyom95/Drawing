
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class LineController : MonoBehaviour
{
    
    
    [SerializeField] 
    private LineRenderer _lineRenderer;
    [SerializeField] 
    private float _startWidth = 0.1f;
    [SerializeField] 
    private float _endWidth = 0.1f;
    [SerializeField]
    private ColorBehaviour _colorBehaviour;

    [SerializeField] 
    private MarkerController _markerController;

    [SerializeField]
    private Collider _colliderSquare;
    
    
    
    
    private bool _isButtonClick;
  
    private Vector2 _startPosition;

  
    private List<GameObject> _linesList = new ();
    private Vector2 _currentPosition;
    private bool _isLineDraw;

    // Start is called before the first frame update
    private void Awake ()
    {
        _lineRenderer.startWidth = _startWidth;
        _lineRenderer.endWidth = _endWidth;
       
    }
    
    public void ClickButton()
    {
        _isButtonClick = true;
        _isLineDraw = false;
        SetPositionCountLineRenderer();
        _markerController.Instantiate();
        Debug.Log("ClickButton");
      
    }
 
 private void Update()
 {
    _markerController.SetPositionPrefab();
     if (Input.GetMouseButton(0)&& _isButtonClick) 
     {
         _currentPosition = GetMousePosition();

         if (_currentPosition == _startPosition)
         {
             return;
         }


         if (IsMouseOnDrawPosition())
         {
             DrawLine();
             _isLineDraw = true; 
             _markerController.GetPrefab().SetActive(true);

         }
         else
         {
             _markerController.GetPrefab().SetActive(false);
         }
     }
     if (Input.GetMouseButtonUp(0)&& _isLineDraw)
     {
         LeaveLineOnScreen();
         _isLineDraw = false;
     }
 }

 
 private void LeaveLineOnScreen()
 {
     var mesh = new Mesh();
     _lineRenderer.BakeMesh(mesh);

     var meshObject = new GameObject("Line");
     var meshFilter = meshObject.AddComponent<MeshFilter>();
     meshObject.AddComponent<MeshRenderer>();
     meshFilter.mesh = mesh;
     var currentColor = _colorBehaviour.GetCurrentColorLine();
     meshFilter.GetComponent<MeshRenderer>().material.color = currentColor;
     _linesList.Add(meshObject);
    SetPositionCountLineRenderer();
 }

 public void ClearLine()
 {
    
     SetPositionCountLineRenderer();
     _isLineDraw = false;
     _isButtonClick = false;
     foreach (var gameObject in _linesList)
     {
         Destroy(gameObject);
     }
     
 }

 private void DrawLine()
 {
     
     _startPosition = GetMousePosition();
     _lineRenderer.positionCount++; 
     _lineRenderer.SetPosition(_lineRenderer.positionCount-1,new Vector3(_startPosition.x, _startPosition.y,0f));
     Debug.Log("first Line");
     
 }

 private Vector2 GetMousePosition()
 {
     return Camera.main.ScreenToWorldPoint(Input.mousePosition); 
 }

 private void SetPositionCountLineRenderer()
 {
     _lineRenderer.positionCount = 0;
 }

 private bool IsMouseOnDrawPosition()
 {
      var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         if (_colliderSquare.Raycast(ray, out RaycastHit hitInfo,1000))
         {

             var nameCollider = hitInfo.collider.name;
             if (nameCollider == _colliderSquare.name)
             {
                 return true;
             }
            
         }
     return false;
 }
}
