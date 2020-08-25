using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_CustomCharacterController : MonoBehaviour
{
    // PUBLIC
    public GameObject character;

    [Range(0, 2)]
    public int _speedState = 0;

    [Range(0.1f, 10f)]
    public float _speedWalk = 2.0f;
    public string _walkStateName = "Walking";

    [Range(0.1f, 10f)]
    public float _speedRun = 5.0f;
    public string _runStateName = "Slow Run";

    [Range(0.1f, 10f)]
    public float _speedFastRun = 10.0f;
    public string _fastRunStateName = "Fast Run";

    public L_UseButton _walk;
    public L_UseButton _fastRun;

    public L_UseButton _foward;
    public L_UseButton _right;
    public L_UseButton _back;
    public L_UseButton _left;

    public Animator _anim;
    // PUBLIC END






    // PRIVATE
    Vector3 _lastMovement = Vector3.zero;
    Vector3 _movement = Vector3.zero;
    float _speed;
    // PRIVATE END






    private void Start()
    {
        if (character == null)
        {
            character = gameObject;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnEnable()
    {
        _walk.Subscribe(Walk);
        _fastRun.Subscribe(FastRun);

        _foward.Subscribe(Foward);
        _right.Subscribe(Right);
        _back.Subscribe(Back);
        _left.Subscribe(Left);
    }

    private void OnDisable()
    {
        _walk.Unsubscribe();
        _fastRun.Unsubscribe();

        _foward.Unsubscribe();
        _right.Unsubscribe();
        _back.Unsubscribe();
        _left.Unsubscribe();
    }






    void DefineSpeed()
    {
        _anim.SetInteger("MovementState", _speedState);

        AnimatorStateInfo stateInfo = _anim.GetCurrentAnimatorStateInfo(0);
        AnimatorTransitionInfo transitionInfo = _anim.GetAnimatorTransitionInfo(0);
        if (stateInfo.IsName("Idle2") || stateInfo.IsName("Idle1"))
        {
            if (transitionInfo.normalizedTime > 0.15f)
                _speed = transitionInfo.normalizedTime * _speedWalk;
            else
                _speed = 0;
        }
        else if(stateInfo.IsName(_walkStateName))
        {
            if (transitionInfo.IsName(_walkStateName +" -> " + _runStateName))
            {
                _speed = (1 - transitionInfo.normalizedTime) * _speedWalk + transitionInfo.normalizedTime * _speedRun;
            }
            else if (transitionInfo.IsName(_walkStateName + " -> Idle1"))
            {
                if (transitionInfo.normalizedTime < 0.95f)
                    _speed = (1 - transitionInfo.normalizedTime) * _speedWalk;
                else
                    _speed = 0;
            }
            else
            {
                _speed = _speedWalk;
            }
        }
        else if (stateInfo.IsName(_runStateName))
        {
            if (transitionInfo.IsName(_runStateName + " -> " + _fastRunStateName))
            {
                _speed = (1 - transitionInfo.normalizedTime) * _speedRun + transitionInfo.normalizedTime * _speedFastRun;
            }
            else if (transitionInfo.IsName(_runStateName + " -> " + _walkStateName))
            {
                _speed = (1 - transitionInfo.normalizedTime) * _speedRun + transitionInfo.normalizedTime * _speedWalk;
            }
            else if (transitionInfo.IsName(_runStateName + " -> Idle1"))
            {
                _speed = (1 - transitionInfo.normalizedTime) * _speedRun;
            }
            else
            {
                _speed = _speedRun;
            }
        }
        else if (stateInfo.IsName(_fastRunStateName))
        {

            if (transitionInfo.IsName(_fastRunStateName + " -> " + _runStateName))
            {
                _speed = (1 - transitionInfo.normalizedTime) * _speedFastRun + transitionInfo.normalizedTime * _speedRun;
            }
            else
            {
                _speed = _speedFastRun;
            }
        }
    }






    void Walk()
    {
        _speedState = 0;
    }

    void FastRun()
    {
        _speedState = 2;
    }






    void Foward()
    {
        _movement += character.transform.forward;
    }

    void Right()
    {
        _movement += character.transform.right;
    }

    void Back()
    {
        _movement -= character.transform.forward;
    }

    void Left()
    {
        _movement -= character.transform.right;
    }






    void Move()
    {
        DefineSpeed();

        if (_movement.magnitude > 0.5f)
        {
            _anim.SetBool("Move", true);
        }
        else
        {
            _anim.SetBool("Move", false);
        }

        Vector3 move = ((_movement + _lastMovement).normalized * _speed * Time.fixedDeltaTime);

        character.transform.Translate(move);
        _lastMovement = (_lastMovement + _movement) * 0.5f;
        _movement = Vector3.zero;
        _speedState = 1;
    }
}
