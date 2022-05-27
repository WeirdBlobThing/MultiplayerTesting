using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{


    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;

    [SerializeField]
    private float thrusterForce = 1000f;

    [Header("Spring settings:")] 
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;

    //component caching
    private PlayerMotor motor;
    private ConfigurableJoint joint;
    private Animator animator;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        animator = GetComponent<Animator>();

        SetJointSetting(jointSpring);
    }


    void Update()
    {
        //Calculat movement velocity as a 3D vector
        float _xMov = Input.GetAxis("Horizontal");
        float _zMov = Input.GetAxis("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        //final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical) * speed;

        //animate movement
        animator.SetFloat("ForwardVelocity", _zMov);

        //Apply movement
        motor.Move(_velocity);

        //calculate rotation as a 3d vector (turning around)
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //apply rotation
        motor.Rotate(_rotation);

        //calculate rotation as a 3d vector (turning around)
        float _xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;

        //apply camera rotation
        motor.RotateCamera(_cameraRotation);

        //calculate the thruster force based on player input
        Vector3 _thrusterForce = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSetting(0f);
        }
        else
        {
            SetJointSetting(jointSpring);
        }

        //apply thruster force
        motor.ApplyThruster(_thrusterForce);
    }

    private void SetJointSetting(float _jointSpring)
    {
        joint.yDrive = new JointDrive
        {
            positionSpring = jointSpring,
            maximumForce = jointMaxForce

        };
    }
}

