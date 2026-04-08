using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(2f, 5f)] public float MaxDuration = 1f;

    private Vector3 _p0;
    private Vector3 _p1;
    private Vector3 _p2;
    private Vector3 _p3;
    private float _duration;

    private void Update()
    {
        if (_duration <= 0f)
        {
            Destroy(gameObject);
            return;
        }

        _duration -= Time.deltaTime;
        float t = 1f - _duration / MaxDuration;
        transform.position = CubicBezier(t);
    }

    public void Init(Vector3 endPos)
    {
        _p0 = transform.position;
        _p1 = _p0
                + Vector3.up * Random.Range(-5f, 5f)
                + Vector3.forward * Random.Range(-5f, 5f)
                + Vector3.right * Random.Range(_p0.x, (endPos - _p0).x / 2f);
        _p2 = endPos
                + Vector3.up * Random.Range(-5f, 5f)
                + Vector3.forward * Random.Range(-5f, 5f)
                + Vector3.left * Random.Range((endPos - _p0).x / 2f, endPos.x);
        _p3 = endPos;
        _duration = Random.Range(1f, MaxDuration);
    }

    private Vector3 CubicBezier(float t)
    {
        // de Casteljau 알고리즘 — 3단계 Lerp
        Vector3 a = Vector3.Lerp(_p0, _p1, t);
        Vector3 b = Vector3.Lerp(_p1, _p2, t);
        Vector3 c = Vector3.Lerp(_p2, _p3, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(d, e, t);
    }
}
