using UnityEngine;

public class DragAndDropManager : MonoBehaviour
{
    public Terrain terrain;
    private Camera _cam;
    private GameObject _selectedObject;
    private Vector3 _offset;
    private Vector3 _previousPosition;
    private float _dragHeight = 30f;

    private void Awake()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.CompareTag("Draggable"))
            {
                _selectedObject = hit.collider.gameObject;
                _offset = _selectedObject.transform.position - hit.point;
                _previousPosition = _selectedObject.transform.position;
            }
        }

        if (Input.GetMouseButton(0) && _selectedObject != null)
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Area")))
            {
                Debug.Log($"Dragging object... Hit: {hit.collider.gameObject.name}");
                var newPos = hit.point + _offset;
                if (hit.collider.gameObject.CompareTag("Ground"))
                {
                    float terrainHeight = terrain.SampleHeight(hit.point);
                    newPos.y = terrainHeight;
                }
                newPos.y += _dragHeight;
                _selectedObject.transform.position = newPos;
            }
        }

        if (Input.GetMouseButtonUp(0) && _selectedObject != null)
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Area")) && hit.collider.gameObject.CompareTag("Target"))
            {
                _selectedObject.transform.position = hit.transform.position + Vector3.up * _dragHeight;
            }
            else
            {
                _selectedObject.transform.position = _previousPosition;
            }

            _previousPosition = Vector3.zero;
            _selectedObject = null;
        }
    }
}
