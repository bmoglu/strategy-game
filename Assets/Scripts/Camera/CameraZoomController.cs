using UnityEngine;

public class CameraZoomController : MonoBehaviour
{
    private Camera _camera;
    private float _targetZoom;
    
    private const float ZoomFactor = 3f;
    private const float ZoomLerpSpeed = 10f;
    private const float MinZoom = 5f;
    private const float MaxZoom = 10f;
    

    private void Start()
    {
        //Camera reference
        _camera = Camera.main;
        //Camera orthographicSize
        _targetZoom = _camera.orthographicSize;
    }

   
    private void Update()
    {
        //Get data
        var scrollData = Input.GetAxis("Mouse ScrollWheel");

        //Calculate zoom
        _targetZoom -= scrollData * ZoomFactor;

        //Some limitation
        _targetZoom = Mathf.Clamp(_targetZoom, MinZoom, MaxZoom);
        
        //Do zoom
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _targetZoom, Time.deltaTime * ZoomLerpSpeed);
    }
}
