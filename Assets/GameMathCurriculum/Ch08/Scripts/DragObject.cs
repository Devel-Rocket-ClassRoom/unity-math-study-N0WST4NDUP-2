using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 _originPos;
    private Vector3 _startPos;
    public float ReturnSpeed = 2f;
    private bool _isReturning;

    private Terrain _terrain;
    private float _timer;

    private void Start()
    {
        _terrain = Terrain.activeTerrain;
    }

    private void Update()
    {
        if (_isReturning)
        {
            _timer += Time.deltaTime / ReturnSpeed;
            var newPos = Vector3.Lerp(_startPos, _originPos, _timer);
            newPos.y = _terrain.SampleHeight(newPos);
            transform.position = newPos;

            if (_timer >= 1f)
            {
                _isReturning = false;
                transform.position = _originPos;
                _timer = 0f;
            }
        }
    }

    public void DragStart()
    {
        if (!_isReturning)
        {
            _originPos = transform.position;
        }

        _isReturning = false;
        _timer = 0f;
    }

    public void Return()
    {
        _isReturning = true;
        _startPos = transform.position;
    }
}