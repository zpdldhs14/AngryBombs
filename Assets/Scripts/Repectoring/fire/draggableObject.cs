using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = System.Object;

public class draggableObject : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler,
    IDragHandler
{
    private Vector3 startPosition;
    private Vector3 pullPosition;
    private Vector3 force;
    private Camera MainCamera;
    private List<GameObject> Objects = new List<GameObject>();
    private ITrajectoryStrategy strategy;
    
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float maxPullDistance;
    [SerializeField] private GameObject Trajectory;
    [SerializeField] private GameObject CannonBall;
    [SerializeField] private Projectile Projectile;
    public Animator animator;
    
    public float Power = 10.0f;
    public float Mass = 10.0f;
    public int maxStep = 20;
    public float timeStep = 0.1f;
    
    

    private void Awake()
    {
        MainCamera = Camera.main;
        startPosition = transform.position; // 시작 위치를 저장
        strategy = new PhysicsTrajectory(LayerMask.GetMask("Default"));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pullPosition = startPosition = MainCamera.ScreenToWorldPoint(
            new Vector3(eventData.position.x,
                eventData.position.y,
                MainCamera.WorldToScreenPoint(transform.position).z));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
        animator.Play("Fire");
        Projectile.CreateProjectile(transform.position, transform.rotation, force);
    }

    private void Reset()
    {
        transform.position = startPosition;
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, startPosition);
        ClearTrajectory();
    }

    private void ClearTrajectory()
    {
        var objects = new List<GameObject>(Objects);
        foreach (var obj in objects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        animator.Play("Ready");
        Vector3 currentMousePosition = MainCamera.ScreenToWorldPoint(
             new Vector3(eventData.position.x,
                         eventData.position.y,
                         MainCamera.WorldToScreenPoint(transform.position).z));

        force = (startPosition - currentMousePosition) * Power; // force는 클릭한 방향으로 계산
        List<Vector3> dragTrajectory = strategy.CalculateTrajectory(startPosition, force, Mass, timeStep, maxStep);
        
        List<Vector3> forwardTrajectory = strategy.CalculateTrajectory(currentMousePosition, force, Mass, timeStep, maxStep);
        
        ClearTrajectory();

        foreach (var point in dragTrajectory)
        {
            var go = Instantiate(Trajectory, point, Quaternion.identity);
            Objects.Add(go);
        }

        if (Objects.Count == forwardTrajectory.Count)
        {
            for (int i = 0; i < forwardTrajectory.Count; i++)
            {
                Objects[i].SetActive(true);
                Objects[i].transform.position = forwardTrajectory[i];
            }
        }
    }
}