using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.SplineMovementSystem;
using HCB.SplineMovementSystem.Samples;
using HCB.Core;
using Dreamteck.Forever;

public class AIMovement : SplineCharacterMovementController
{

    private Stamina _stamina;
    private Runner _runner;

    public Stamina Stamina { get { return _stamina == null ? _stamina = GetComponent<Stamina>() : _stamina; } }

    private SplineCharacterAnimationController _splineCharacterAnimationController;


    private bool _canRegenerate;
    private bool _isReplenish;
    
    public Runner Runner { get { return _runner == null ? _runner = GetComponent<Runner>() : _runner; } }

    public SplineCharacterAnimationController SplineCharacterAnimationController
    { get { return _splineCharacterAnimationController == null ? _splineCharacterAnimationController = GetComponentInChildren<SplineCharacterAnimationController>() : _splineCharacterAnimationController; } }

    public SplineClampData ClampData;
    public GameObject Graphic;


    public float XPos;
    private Vector3 _desiredPos;
    public float Speed = 2.0f;

    private float _timer = 0;

    private bool _canMove = true;
   
    [SerializeField]  private float timeToMove = 4f;

    protected override void Awake()
    {
        base.Awake();
        //Speed'i degistirmek icin currentSpeed'i kullan(splineCharacter) scriptable objelere dokunma.
        
        //SPEED
        _currentSpeed = Random.Range(1.10f, 5f);
        maxSpeed = _currentSpeed;

        //tanimlamadigimiz icin 0 geliyordu karakteri ortaya koyuyordu oyun basinda, simdi grafigin pozisyonuna esitledik.
        _desiredPos = Graphic.transform.localPosition;

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
        if (_canRegenerate)
            Stamina.StaminaRegen();

        AIRightLeft();
        AIMove();

        base.Update();

        Runner.follow = SplineCharacter.CanMoveForward;
    }

    public void AIMove()
    {
        if (!SplineCharacter.CanMoveForward)
            return;
        if (!SplineCharacter.IsControlable)
            return;

        Stamina.StaminaDrain();

        if (Stamina.CurrentStamina <= 0)
        {

            SplineCharacterAnimationController.TriggerAnimation("Dead");
            Runner.followSpeed = 0;
            //IEnumerator eklenip gameobje yok olacak.
        }

        //AI stop
        if (Stamina.CurrentStamina <= Random.Range(0,30))
        {
            //buraya terleme particle gelecek
            if (_isReplenish)
                return;

            StartCoroutine(WaitForRegenerate());

                //SplineCharacterAnimationController.TriggerAnimation("Idle");
        }
        
        //If AI finishes first, fail
        if(SplineCharacter.IsFinished)
        {
            GameManager.Instance.OnStageFail.Invoke();
        }

        //AI Death
       
    }

    IEnumerator WaitForRegenerate()
    {
        float luck = Random.Range(1, 10);

        _isReplenish = true;

        while (true)
        {
            if(luck > 5)
            {
                break;
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(1, 3));
                luck = Random.Range(1, 10);
            }
        }

        AIStop();

        _canRegenerate = true;

        yield return new WaitForSeconds(Random.Range(1, 5));

        _canRegenerate = false;

        SplineCharacter.CanMoveForward = true;
        //Kosmaya basladigi zaman animasyon cagir 1 kere.
        

        _isReplenish = false;

    }

    public void AIStop()
    {
        SplineCharacter.CanMoveForward = false;
    }

    void AIRightLeft()
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;

        if (_canRegenerate)
            return;

        //surekli timer guncelliyoruz.
        _timer += Time.deltaTime;

        Graphic.transform.localPosition = Vector3.Lerp(Graphic.transform.localPosition, _desiredPos, Time.deltaTime * Speed);

        if (_timer >= timeToMove)
        {
           
            if (Vector3.Distance(Graphic.transform.localPosition, _desiredPos) <= 0.01f)
            {
                XPos = Random.Range(-ClampData.MovementWidth /2, ClampData.MovementWidth / 2);
                _desiredPos = new Vector3(XPos, Graphic.transform.localPosition.y, Graphic.transform.localPosition.z);
            }

            _timer = 0;
        }
    }

}