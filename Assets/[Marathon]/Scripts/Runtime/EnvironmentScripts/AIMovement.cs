using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.SplineMovementSystem;
using HCB.SplineMovementSystem.Samples;
using HCB.Core;

public class AIMovement : SplineCharacterMovementController
{

    private Stamina _stamina;

    public Stamina Stamina { get { return _stamina == null ? _stamina = GetComponent<Stamina>() : _stamina; } }

    private SplineCharacterAnimationController _splineCharacterAnimationController;


    private bool _canRegenerate;
    private bool _isReplenish;
  

    public SplineCharacterAnimationController SplineCharacterAnimationController
    { get { return _splineCharacterAnimationController == null ? _splineCharacterAnimationController = GetComponentInChildren<SplineCharacterAnimationController>() : _splineCharacterAnimationController; } }

    public SplineClampData ClampData;
    public GameObject Graphic;


    public float XPos;
    public Vector3 DesiredPos;
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
        AIMove();
        AIRightLeft();

        base.Update();
    }

    public void AIMove()
    {

        if (!SplineCharacter.CanMoveForward)
            return;

        Stamina.StaminaDrain();

        //buraya stamina 0 ise olecek kodu gelecek.

        if(Stamina.CurrentStamina <= 0)
        {
            Debug.Log(gameObject.name + "Dead");
        }

        //AI stop
        if (Stamina.CurrentStamina <= Random.Range(0,30))
        {
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
        SplineCharacterAnimationController.TriggerAnimation("Run");

        _isReplenish = false;
    }

    public void AIStop()
    {
        SplineCharacter.CanMoveForward = false;
        SplineCharacterAnimationController.TriggerAnimation("Tired");

        
    }


    void AIRightLeft()
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;

        if (_canRegenerate)
            return;

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