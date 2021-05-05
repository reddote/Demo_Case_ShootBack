using UnityEngine;

public class PlayerController : CharacterControllerScript{
    [SerializeField] private float playerHorizontalSpeed;
    [SerializeField] private GameObject deadParticle;
    private GameObject _weapon;
    private FloatingJoystick _joystick;
    private Transform _finishLineTransform;
    private Transform _camera;
    private Vector3 _initialPosition;

    protected override void UpdateOverride(){
        if (!LevelController.instance.isFinished){
            SpeedAfterHitObstacles();
            FinisLineChecker();
        }

        if (!LevelController.instance.isPlayerDead){
            PlayerMovement();
        }
    }

    //this method check did player manage to finish game or he fall of the platform
    protected virtual void FinisLineChecker(){
        if (Mathf.Abs(_finishLineTransform.position.z - transform.position.z) <= 0.1f){
            LevelController.instance.isFinished = true;
        }

        if ((_initialPosition.y - transform.position.y) >= 1f){//fall of platform
            LevelController.instance.isFinished = true;
            LevelController.instance.isPlayerDead = true;
        }
    }
   
    protected override void StartOverride(){
        base.StartOverride();
        _weapon = GetComponentInChildren<WeaponSystems>().gameObject;
        _camera = GetComponentInChildren<Camera>().transform;
        _joystick = LevelController.instance.joystick;
        _finishLineTransform = LevelController.instance.finishLine;
        _initialPosition = transform.position;
    }

    protected override float SetPositionX(){
        float _xSpeed = (_joystick.Horizontal * (playerHorizontalSpeed * Time.deltaTime));
        return _xSpeed;
    }

    //this will disable to gun
    //visualize dead
    public void PlayerChildObjectModifierForEndGame(){
        _camera.SetParent(null);
        _weapon.SetActive(false);
    }
    
    public void PlayerDead(){
        deadParticle.SetActive(true);
    }

}