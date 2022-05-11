using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.SplineMovementSystem;
using HCB.SplineMovementSystem.Samples;

public class AIMovement : SplineCharacterMovementController
{

    private Stamina _stamina;
    private SplineCharacterAnimationController _splineCharacterAnimationController;

    public SplineCharacterAnimationController SplineCharacterAnimationController
    { get { return _splineCharacterAnimationController == null ? _splineCharacterAnimationController = GetComponentInChildren<SplineCharacterAnimationController>() : _splineCharacterAnimationController; } }

    public SplineClampData ClampData;
    public GameObject Graphic;


    public float XPos;
    public Vector3 DesiredPos;
    public float Speed = 2.0f;

    private float _timer = 0;

    
   
    [SerializeField]  private float timeToMove = 4f;

    protected override void Awake()
    {
        base.Awake();


    }

    protected override void OnEnable()
    {
        //splineCharacterMovementController'daki base bu, once yazarsak bunu aliyor.
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void Update()
    {
        AIMove();
        AIRightLeft();

        base.Update();

        
        
        

    }

    public void AIMove()
    {
        //Speed'i degistirmek icin currentSpeed'i kullan(splineCharacter) scriptable objelere dokunma.
        _currentSpeed = Random.Range(2, 5);

        //Kosmaya basladigi zaman animasyon cagir 1 kere.

    }

    public void AIStop()
    {
        _currentSpeed = 0;
        //SplineCharacterAnimationController.TriggerAnimation("Idle");
    }


    void AIRightLeft()
    {

        //surekli timer guncelliyoruz.
        _timer += Time.deltaTime;

        
        Graphic.transform.localPosition = Vector3.Lerp(Graphic.transform.localPosition, DesiredPos, Time.deltaTime * Speed);

        if (_timer >= timeToMove)
        {
           
            if (Vector3.Distance(Graphic.transform.localPosition, DesiredPos) <= 0.01f)
            {
                XPos = Random.Range(-ClampData.MovementWidth /2, ClampData.MovementWidth / 2);
                DesiredPos = new Vector3(XPos, Graphic.transform.localPosition.y, Graphic.transform.localPosition.z);
                
            }


            _timer = 0;
        }
    }
}