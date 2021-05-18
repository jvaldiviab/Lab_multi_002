using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    // Seleccionamos todo el GameObjct
    public GameObject ball;
    // Seleccionamos solo el Transform cam
    public Transform cam;
    // La distancia de la pelota
    public float ballDistance = 2f;

    // fuerza para lanzar la bola
    public float ballForceMin = 150f;
    public float ballForceMax = 500f;
    public float ballForce = 250f;

    public float totalTimer = 3f;
    float currentTime = 0.0f;

    // Se esta cogiendo la ball?
    public bool holdingBall = true;

    Rigidbody ballRB;

    bool isPickableBall = false;
    public CanvasController canvasScript;
    public LayerMask pickableLayer;
    RaycastHit hit;



    // Start is called before the first frame update
    void Start()
    {
        ballRB = ball.GetComponent<Rigidbody>();
        ballRB.useGravity = false;

        canvasScript.OcultarCursor(true);
    }

    // Update is called once per frame
    void Update()
    {

        if (holdingBall == true)
        {

            if (Input.GetMouseButtonDown(0))
            {
                currentTime = 0.0f;
                canvasScript.SetValueBar(0);
                canvasScript.ActivarSlider(true);
            }

            if (Input.GetMouseButton(0))
            {
                currentTime += Time.deltaTime;
                ballForce = Mathf.Lerp(ballForceMin, ballForceMax, currentTime / totalTimer);
                canvasScript.SetValueBar(currentTime / totalTimer);
            }

            if (Input.GetMouseButtonUp(0))
            {
                holdingBall = false;
                ballRB.useGravity = true;
                ballRB.AddForce(cam.forward * ballForce);

                // Una vez lanzado, 
                canvasScript.OcultarCursor(false);

                canvasScript.ActivarSlider(false);
            }
        }
        else
        {
            if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, pickableLayer))
            {
                if (isPickableBall == false)
                {
                    isPickableBall = true;
                    canvasScript.ChangePickableBallColor(true);
                }
                if (isPickableBall && Input.GetKeyDown(KeyCode.E))
                {
                    holdingBall = true;
                    ballRB.useGravity = false;
                    ballRB.velocity = Vector3.zero;
                    ballRB.angularVelocity = Vector3.zero;

                    ball.transform.localRotation = Quaternion.identity;

                    GameController.instance.canScore = false;

                    canvasScript.ChangePickableBallColor(true);
                    canvasScript.OcultarCursor(true);
                }
            }
            else if (isPickableBall == true)
            {
                isPickableBall = false;
                canvasScript.ChangePickableBallColor(false);
            }
        }
               
    }

    private void LateUpdate()
    {
        if(holdingBall == true)
        {
            // la ball delante a determinada distancia
            ball.transform.position = cam.position + cam.forward * ballDistance;
        }
    }

}
