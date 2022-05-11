using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.SplineMovementSystem;
using HCB.SplineMovementSystem.Samples;

public class AIMovement : SplineCharacterMovementController
{

    private Stamina _stamina;

    public Stamina Stamina { get { return _stamina == null ? _stamina = GetComponent<Stamina>() : _stamina; } }

    private SplineCharacterAnimationController _splineCharacterAnimationController;

    

  

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
       
        _currentSpeed = Random.Range(2, 5);

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

        if (!SplineCharacter.CanMoveForward)
            return;

        Stamina.StaminaDrain();

        //AI stop
        if (Stamina.CurrentStamina <= 30)
        {
            
            StartCoroutine(WaitForRegenerate());

                //SplineCharacterAnimationController.TriggerAnimation("Idle");
           
        }
            

        //Kosmaya basladigi zaman animasyon cagir 1 kere.

    }

    IEnumerator WaitForRegenerate()
    {

        while(Stamina.CurrentStamina <= 90)
        {
            AIStop();
            yield return null;
        }

        yield return new WaitForSeconds(Random.Range(2, 10));
        SplineCharacter.CanMoveForward = true;
        
    }

    public void AIStop()
    {
        SplineCharacter.CanMoveForward = false;
        //SplineCharacterAnimationController.TriggerAnimation("Idle");

        Stamina.StaminaRegen();
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