using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.SplineMovementSystem;
using HCB.SplineMovementSystem.Samples;
using HCB.Core;
using Dreamteck.Forever;
using HCB.IncrimantalIdleSystem;
using Sirenix.OdinInspector;

public class AIMovement : SplineCharacterMovementController
{

    private Stamina _stamina;
    private Runner _runner;

    public Stamina Stamina { get { return _stamina == null ? _stamina = GetComponent<Stamina>() : _stamina; } }

    private SplineCharacterAnimationController _splineCharacterAnimationController;
   


    private bool _canRegenerate;
    private bool _isReplenish;

    private IdleStat idleStat;
    
    public Runner Runner { get { return _runner == null ? _runner = GetComponent<Runner>() : _runner; } }

    public SplineCharacterAnimationController SplineCharacterAnimationController
    { get { return _splineCharacterAnimationController == null ? _splineCharacterAnimationController = GetComponentInChildren<SplineCharacterAnimationController>() : _splineCharacterAnimationController; } }

    

    public SplineClampData ClampData;
    public GameObject Graphic;

    [Space]
    [Header("AIRightLeft")]
    [SerializeField] private float _xPos;
    [SerializeField] private float _leanSpeed;
    [SerializeField] [Range(0f, 5f)] private float timeToMove;

    private Vector3 _desiredPos;
    

    private float _timer = 0;

    private bool _canMove;
   
    

    protected override void Awake()
    {

        base.Awake();
        //Speed'i degistirmek icin currentSpeed'i kullan(splineCharacter) scriptable objelere dokunma.

    }

    protected void Start()
    {
        //tanimlamadigimiz icin 0 geliyordu karakteri ortaya koyuyordu oyun basinda, simdi grafigin pozisyonuna esitledik.
        _desiredPos = Graphic.transform.localPosition;
        

    }

    protected override void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(Ready);
        LevelManager.Instance.OnLevelFinish.AddListener(AIFinishStop);
        
        //splineCharacterMovementController'daki base bu, once yazarsak bunu aliyor.
        base.OnEnable();
       

    }

    protected override void OnDisable()
    {
        if (Managers.Instance == null) 
            return;
        LevelManager.Instance.OnLevelStart.RemoveListener(Ready);
        LevelManager.Instance.OnLevelFinish.RemoveListener(AIFinishStop);
        base.OnDisable();
       
    }

    protected override void Update()
    {
        if (_canRegenerate)
            Stamina.StaminaRegen();
        
        AIRightLeft();
        AIMove();

        base.Update();

    }

    public void LateUpdate()
    {
       
        Runner.follow = SplineCharacter.CanMoveForward;
    }

    private void Ready()
    {
        SplineCharacterAnimationController.TriggerAnimation("Crouch");
    }

    public void AIMove()
    {
        if (!SplineCharacter.CanMoveForward)
            return;
        if (!SplineCharacter.IsControlable)
            return;


        CheckMove();

        Stamina.StaminaDrain();

        if (Stamina.CurrentStamina <= 0)
        {
            
            SplineCharacterAnimationController.TriggerAnimation("Dead");
            Runner.followSpeed = 0;
            SplineCharacter.IsControlable = false;
            
            //When Dead, Collider off
            SplineCharacter.GetComponentInChildren<CapsuleCollider>().enabled = false;
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
    }

    IEnumerator WaitForRegenerate()
    {
        float luck = Random.Range(1, 10);

        _isReplenish = true;

        //surekli bu donguye girip bazilarinin durmasini onluyoruz AI stoptan once.(ki olsunler) Luck dongusunu kirip kurtulmalari gerek olumden ve durmalilar.
        while (true)
        {
            if(luck > 2)
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
        if (!SplineCharacter.CanMoveForward)
            return;
        if (!SplineCharacter.IsControlable)
            return;

        if (_canRegenerate)
            return;

        //surekli timer guncelliyoruz.
        _timer += Time.deltaTime;

        Graphic.transform.localPosition = Vector3.Lerp(Graphic.transform.localPosition, _desiredPos, Time.deltaTime * _leanSpeed);

        if (_timer >= timeToMove)
        {
           
            if (Vector3.Distance(Graphic.transform.localPosition, _desiredPos) <= 0.01f)
            {
                _xPos = Random.Range(-ClampData.MovementWidth /2, ClampData.MovementWidth / 2);
                _desiredPos = new Vector3(_xPos, Graphic.transform.localPosition.y, Graphic.transform.localPosition.z);
            }

            _timer = 0;
        }
    }

    public void AIFinishStop()
    {
        SplineCharacter.CanMoveForward = false;
        
        SplineCharacter.IsControlable = false;
    }

    private void CheckMove()
    {
        //if (_canMove)
        //    return;
        if (GameManager.Instance.IsCountdown)
        {
           
        }

       

    }


}