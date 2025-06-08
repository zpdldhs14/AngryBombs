using System.Collections.Generic;
using UnityEngine;

public interface ITrajectoryStrategy
{
    List<Vector3> CalculateTrajectory(Vector3 start, Vector3 force, float mess, float timeStep, int maxSteps);
    bool CheckCollision(Vector3 start, Vector3 end, out Vector3 hitPoint);
}