// =============================================================================
// Assignment_PatrolGuard.cs
// -----------------------------------------------------------------------------
// 목적: 웨이포인트 순찰 가드 과제 (심화 난이도) — 다양한 쿼터니언 API 활용
// ★ 과제 설명:
//    경비병이 웨이포인트를 순서대로 순회한다. 각 단계에서 다른 쿼터니언 API를 사용:
//    - Quaternion.Euler() — 초기 방향 설정
//    - Quaternion.FromToRotation() — 두 방향 벡터 사이 회전 계산
//    - Quaternion.RotateTowards() — 일정 각속도로 회전
// =============================================================================

using UnityEngine;
using TMPro;

public class Assignment_PatrolGuard : MonoBehaviour
{
    [Header("=== 웨이포인트 ===")]
    [Tooltip("순찰 경로를 이루는 웨이포인트 배열")]
    [SerializeField] private Transform[] waypoints;

    [Tooltip("이동 속도 (m/s)")]
    [SerializeField][Range(1f, 10f)] private float moveSpeed = 3f;

    [Tooltip("회전 속도 (도/초)")]
    [SerializeField][Range(30f, 360f)] private float turnSpeed = 120f;

    [Header("=== 초기 설정 ===")]
    [Tooltip("시작 시 Y축 회전 각도 (도)")]
    [SerializeField][Range(0f, 360f)] private float startYAngle = 0f;

    [Header("=== UI 연결 ===")]
    [SerializeField] private TMP_Text uiInfoText;

    [Header("=== 디버그 정보 (읽기 전용) ===")]
    [SerializeField] private int currentWaypointIndex;
    [SerializeField] private float distanceToNext;
    [SerializeField] private float angleToTarget;

    private Quaternion targetRotation;
    private float arrivalThreshold = 0.5f;

    private void Start()
    {
        transform.rotation *= Quaternion.Euler(0f, startYAngle, 0f);
    }

    private void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypointIndex];
        Vector3 dirToNext = target.position - transform.position;
        dirToNext.y = 0f;
        dirToNext.Normalize();
        distanceToNext = Vector3.Distance(transform.position, target.position);

        // 1.게임 시작 시 Inspector에서 설정한 각도로 초기 방향을 잡는다 v
        // 2.경비병은 항상 다음 웨이포인트를 향해 일정한 각속도로 몸을 돌린다 v
        // 3.경비병은 자신이 바라보는 방향(forward)으로 전진한다 v
        // 4.웨이포인트에 도착하면 다음 웨이포인트로 전환하고, 마지막 이후에는 처음으로 순환한다 v
        // 사용 API: Quaternion.Euler, Quaternion.FromToRotation, Quaternion.RotateTowards

        var targetRotation = Quaternion.FromToRotation(transform.forward, dirToNext) * transform.rotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        // 이동 (제공됨 — 수정 불필요)
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        // 도착 판정
        if (distanceToNext < arrivalThreshold)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        angleToTarget = Quaternion.Angle(transform.rotation, targetRotation);

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (uiInfoText == null) return;

        string waypointInfo = waypoints != null && waypoints.Length > 0
            ? $"{currentWaypointIndex + 1}/{waypoints.Length}"
            : "없음";

        uiInfoText.text = $"[Assignment_PatrolGuard]\n" +
            $"웨이포인트: {waypointInfo}\n" +
            $"거리: {distanceToNext:F1}m\n" +
            $"각도 차이: {angleToTarget:F1}°\n" +
            $"\n<color=yellow>경비병이 웨이포인트를 순찰합니다</color>";
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!enabled) return;

        if (waypoints == null || waypoints.Length == 0) return;

        // 웨이포인트 연결선
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] == null) continue;

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(waypoints[i].position, 0.3f);

            int next = (i + 1) % waypoints.Length;
            if (waypoints[next] != null)
            {
                Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
                Gizmos.DrawLine(waypoints[i].position, waypoints[next].position);
            }
        }

        if (!Application.isPlaying) return;

        // 현재 forward 방향 (초록)
        VectorGizmoHelper.DrawArrow(transform.position,
            transform.position + transform.forward * 1.5f,
            Color.green, 0.15f, 20f);

        // 목표 방향 (빨강)
        if (waypoints[currentWaypointIndex] != null)
        {
            Vector3 dirToTarget = (waypoints[currentWaypointIndex].position - transform.position).normalized;
            VectorGizmoHelper.DrawArrow(transform.position,
                transform.position + dirToTarget * 1.5f,
                Color.red, 0.15f, 20f);
        }
    }
#endif
}
