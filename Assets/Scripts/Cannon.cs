using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float Power = 500.0f;
    public float Mass = 10.0f;
    public int maxStep = 20;
    public float timeStep = 0.1f;

    public GameObject CannonBall;
    public GameObject Trajectory;

    public List<GameObject> Objects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    List<Vector3> PredictTrajectory(Vector3 force, float mass)
    {
        List<Vector3> trajectory = new List<Vector3>();

        Vector3 position = transform.position;
        Vector3 velocity = force / mass; // impulse calculation

        trajectory.Add(position);

        for (int i = 1; i <= maxStep; i++)
        {
            float timeElapsed = timeStep * i;
            // Calculate projectile motion!
            trajectory.Add(position +
                           velocity * timeElapsed +
                           Physics.gravity * (0.5f * timeElapsed * timeElapsed)); // gravitational acceleration

            if (CheckCollision(trajectory[i - 1], trajectory[i], out Vector3 hitPoint))
            {
                trajectory[i] = hitPoint;
                break;
            }
        }

        return trajectory;
    }


    private bool CheckCollision(Vector3 start, Vector3 end, out Vector3 hitPoint)
    {
        hitPoint = end;
        Vector3 direction = end - start;
        float distance = direction.magnitude;

        if (Physics.Raycast(start, direction.normalized, out RaycastHit hit, distance, 1 << LayerMask.NameToLayer("Default")))
        {
            hitPoint = hit.point;
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {

        foreach (var o in Objects)
        {
            o.SetActive(false);
        }

        List<Vector3> trajectorys2 = PredictTrajectory(transform.forward * Power, Mass);

        if (Objects.Count == trajectorys2.Count)
        {
            for (var index = 0; index < trajectorys2.Count; index++)
            {
                var trajectory = trajectorys2[index];
                Objects[index].SetActive(true);
                Objects[index].transform.position = trajectory;
            }
        }
    }
}
