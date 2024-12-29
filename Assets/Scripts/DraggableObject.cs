using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler,
    IDragHandler
{
    private Vector3 startPosition;
    private Vector3 pullPosition;
    private Vector3 force;

    private Camera MainCamera;

    public Animator animator;
    
    public float Mass = 10.0f;
    public int maxStep = 20;
    public float timeStep = 0.1f;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float maxPullDistance;

    private List<GameObject> Objects = new List<GameObject>();
    [SerializeField] private GameObject Trajectory;
    [SerializeField] private GameObject CannonBall;
    public float Power = 10.0f;

    private void Awake()
    {
        MainCamera = Camera.main;
        startPosition = transform.position; // 시작 위치를 저장
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

    List<Vector3> PredictTrajectory(Vector3 force, float mass)
    {
        List<Vector3> trajectory = new List<Vector3>();

        Vector3 position = startPosition; // 초기의 시작 위치 설정
        Vector3 velocity = force / mass; //impulse를 계산합니다.

        trajectory.Add(position);

        for (int i = 1; i <= maxStep; i++)
        {
            float timeElapsed = timeStep * i;
            // 중력과 가속도 적용!
            trajectory.Add(position +
                           velocity * timeElapsed +
                           Physics.gravity * (0.5f * timeElapsed * timeElapsed)); // 중력 가속도 적용

            if (CheckCollision(trajectory[i - 1], trajectory[i], out Vector3 hitPoint))
            {
                trajectory[i] = hitPoint;
                break;
            }
        }

        return trajectory;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        pullPosition = startPosition = MainCamera.ScreenToWorldPoint(
            new Vector3(eventData.position.x,
                eventData.position.y,
                MainCamera.WorldToScreenPoint(transform.position).z));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.position = startPosition;
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, startPosition);

        foreach (var o in Objects)
        {
            Destroy(o);
        }

        Objects.Clear();
        animator.Play("Fire");
        GameObject go = Instantiate(CannonBall, transform.position, transform.rotation);
        go.GetComponent<Rigidbody>().mass = Mass;
        go.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse); // OnDrag���� ����� force ���
        Bomb bomb = go.GetComponent<Bomb>();
        if (bomb != null)
        {
            bomb.explosionDamage = 50; // 예시 데미지 값
            bomb.explosionRadius = 5f; // 예시 반경 값
            bomb.damageableLayers = LayerMask.GetMask("Monster", "Structure"); // 데미지를 받을 레이어 설정
        }
        Destroy(go, 3.0f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        animator.Play("Ready");
        Vector3 currentMousePosition = MainCamera.ScreenToWorldPoint(
             new Vector3(eventData.position.x,
                         eventData.position.y,
                         MainCamera.WorldToScreenPoint(transform.position).z));

        force = (startPosition - currentMousePosition) * Power; // force는 클릭한 방향으로 계산
        List<Vector3> trajectorys = PredictTrajectory(force, Mass);

        foreach (var o in Objects)
        {
            Destroy(o);
        }

        Objects.Clear();

        foreach (var trajectory in trajectorys)
        {
            var go = Instantiate(Trajectory, trajectory, Quaternion.identity);
            Objects.Add(go);
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