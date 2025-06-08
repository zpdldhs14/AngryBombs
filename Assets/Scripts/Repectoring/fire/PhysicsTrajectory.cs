using System.Collections.Generic;
using UnityEngine;

public class PhysicsTrajectory : ITrajectoryStrategy
{
    private readonly LayerMask _layerMask;

    public PhysicsTrajectory(LayerMask layerMask)
    {
        this._layerMask = layerMask;
    }
    
    public List<Vector3> CalculateTrajectory(Vector3 startPosition, Vector3 force, float mass, float timeStep, int maxSteps)
    {
        List<Vector3> trajectory = new List<Vector3>();

        Vector3 position = startPosition;
        Vector3 velocity = force / mass;

        trajectory.Add(position);

        for (int i = 1; i <= maxSteps; i++)
        {
            float timeElapsed = timeStep * i;
            Vector3 nextPosition = position +
                                   velocity * timeElapsed +
                                   Physics.gravity * (0.5f * timeElapsed * timeElapsed);

            // 다음 위치를 먼저 추가
            trajectory.Add(nextPosition);

            // 충돌 체크는 이전 위치와 현재 추가된 위치 사이에서 수행
            if (CheckCollision(trajectory[i-1], nextPosition, out Vector3 hitPoint))
            {
                // 충돌이 발생한 경우 마지막 위치를 충돌 지점으로 수정
                trajectory[trajectory.Count - 1] = hitPoint;
                break;
            }
        }

        return trajectory;
    }

    public bool CheckCollision(Vector3 start, Vector3 end, out Vector3 hitPoint)
    {
        hitPoint = end;
        Vector3 direction = end - start;
        float distance = direction.magnitude;

        if (Physics.Raycast(start, direction.normalized, out RaycastHit hit, distance, _layerMask))
        {
            hitPoint = hit.point;
            return true;
        }
        return false;
    }
}