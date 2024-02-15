using CT6RIGPR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager: MonoBehaviour
{
    public int _sticky;
    public int _speed;
    public int _freeze;
    public int _slow;

    private bool _stickyEnabled;
    private bool _speedEnabled;
    private bool _freezeEnabled;
    private bool _slowEnabled;

    private bool isSticking;
//    GameObject stuckObject;
    BallController _ballController;

    // Start is called before the first frame update
    void Start()
    {
        _ballController = gameObject.GetComponent<BallController>();
        _sticky = 10;
        _speed = 10;
        _freeze = 10;
        _slow = 10;

    }

    //Ways to do this:
    //Script for each power up. Or this manager arranges the use of all of them. Sorting centrally is simpler.
    void useSticky()
    {
        _stickyEnabled = true;
        _sticky--;
        StartCoroutine(DisableStickyCoroutine(10f));

        //How to implement this? When it's true, if colliding with something while not grounded, set gravity to 0? Or override BallController in lateupdate?
        //How to handle the ball bouncing off the wall? 
        //Could lock the player onto the wall for the duration and have the josytick control. Ideally would read what design wants
        //Convo indicates that design wants it to stick to wall, unable to rotate for duration. Mouse/Y axis on VR controller would be used to handle altitude.
    }

    void useSpeed()
    {
        _speedEnabled = true;
        _speed--;
        _ballController._maxForce = Constants.BALL_DEFAULT_MAX_BUFFED_FORCE;
        StartCoroutine(DisableSpeedCoroutine(10f));
	}

	private void Update()
    {
		if (!_stickyEnabled && _sticky > 0 && Input.GetKeyDown(KeyCode.Alpha1)) //Sticky power up.
		{
			useSticky();
		}
		if (!_speedEnabled && _speed > 0 && Input.GetKeyDown(KeyCode.Alpha2)) //Speed power up.
		{
			useSpeed();
		}

        Debug.Log(_speedEnabled);

		if (isSticking && !Physics.Linecast(transform.position, transform.position + (_ballController._ballGravity * 1.1f)))
		{
//			Debug.DrawLine(transform.position, transform.position - stuckObject.transform.position, Color.red);
            _ballController._ballGravity = Physics.gravity;
            GetComponent<Rigidbody>().useGravity = true;


//            Vector3 lineDirection = transform.position - stuckObject.transform.position;
//            Debug.Log(lineDirection.x + " " + lineDirection.y + " " + lineDirection.z);
            Debug.Log("Raycast not coliding");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_stickyEnabled && collision.gameObject.tag == "Wall")
        {
            Vector3 normal = collision.contacts[0].normal;
            _ballController._ballGravity = -normal;

            GetComponent<Rigidbody>().useGravity = false;
            isSticking = true;
//            stuckObject = collision.gameObject;
        }
    }

/*    private void OnCollisionExit(Collision collision) //Bouncing off the wall is a problem. Replace with an inumerator that checks for distance from the collided wall every half second?
    {
        _ballController._ballGravity = Physics.gravity;
        GetComponent<Rigidbody>().useGravity = true;
    } */

    private IEnumerator DisableStickyCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        _ballController._ballGravity = Physics.gravity;
        GetComponent<Rigidbody>().useGravity = true;
        _stickyEnabled = false;
        isSticking = false;
    }

    private IEnumerator DisableSpeedCoroutine(float time)
    {
		yield return new WaitForSeconds(time);
		_ballController._maxForce = Constants.BALL_DEFAULT_MAX_FORCE;
		_speedEnabled = false;
	}

}
