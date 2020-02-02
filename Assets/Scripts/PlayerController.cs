using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Manager manager = null;

    [SerializeField] private Animator anim = null;
    [SerializeField] private GameObject Boat = null;
    [SerializeField] private float MoveSpeed = 5f;

    [SerializeField] private float XUpperLimit = 0f;
    [SerializeField] private float XLowerLimit = 0f;
    [SerializeField] private float YUpperLimit = 0f;
    [SerializeField] private float YLowerLimit = 0f;

    [SerializeField] private GameObject OverlayObject = null;

    public List<GameObject> ObjectsCollected = null;

    public bool IsCarryingObject = false;

    private bool IsSwitching = false;
    private bool IsSwimming = false;

    private void Update()
    {
        Keyboard_Movement();

        if(GetComponent<Rigidbody>().angularVelocity.magnitude > 10f || GetComponent<Rigidbody>().velocity.magnitude > 10f)
        {
            ResetRigidBody();
        }
    }

    private void Keyboard_Movement()
    {
        if(Manager.InWater)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    if (transform.position.y >= YUpperLimit)
                    {
                        if (Vector3.Distance(gameObject.transform.position, Boat.transform.position) < 50f)
                        {
                            manager.LoadGetonBoatText();
                        }
                    }
                    else
                    {
                        transform.position += transform.up * MoveSpeed * Time.deltaTime;

                        if(transform.GetChild(0).transform.localEulerAngles.x != 90f)
                        {
                            transform.GetChild(0).transform.localEulerAngles = Vector3.right * 90f;
                        }
                    }
                }

                if (Input.GetKey(KeyCode.S))
                {
                    if (transform.position.y >= YLowerLimit)
                    {
                        if (manager.EnterTheShip.activeInHierarchy)
                        {
                            manager.EnterTheShip.SetActive(false);
                        }

                        transform.position -= transform.up * MoveSpeed * Time.deltaTime;

                        if (transform.GetChild(0).transform.localEulerAngles.x != -90f)
                        {
                            transform.GetChild(0).transform.localEulerAngles = Vector3.right * -90f;
                        }
                    }
                }

                if (Input.GetKey(KeyCode.A))
                {
                    if (transform.position.x > XLowerLimit)
                    {
                        transform.position -= transform.right * MoveSpeed * Time.deltaTime;

                        if (transform.GetChild(0).transform.localEulerAngles.y != 90f)
                        {
                            transform.GetChild(0).transform.localEulerAngles = Vector3.up * 90f;
                        }
                    }
                }

                if (Input.GetKey(KeyCode.D))
                {
                    if (transform.position.x < XUpperLimit)
                    {
                        transform.position += transform.right * MoveSpeed * Time.deltaTime;


                        if (transform.GetChild(0).transform.localEulerAngles.y != -90f)
                        {
                            transform.GetChild(0).transform.localEulerAngles = Vector3.up * -90f;
                        }
                    }
                }

                if(!IsSwimming)
                {
                    IsSwimming = true;
                    anim.SetFloat("Speed", 1f);
                }
            }
            else
            {
                if(IsSwimming)
                {
                    IsSwimming = false;
                    anim.SetFloat("Speed", 0f);
                }

                if(transform.GetChild(0).transform.localEulerAngles.x != 0f)
                {
                    transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, transform.GetChild(0).transform.localEulerAngles.y, 0f);
                }
            }

            if(Input.GetKey(KeyCode.Space))
            {
                if(manager.EnterTheShip.activeInHierarchy && Manager.InWater && !IsSwitching)
                {
                    IsSwitching = true;
                    manager.GetOutOfWater_Func();
                    StartCoroutine(DelayBetweenSpace());
                }
            }
        }
        else
        {
            if(Input.GetKey(KeyCode.Space) && !IsSwitching)
            {
                IsSwitching = true;
                manager.GoInWater_Func();
                StartCoroutine(DelayBetweenSpace());
            }

            if(Input.GetKey(KeyCode.Escape) && manager.Credits_Panel.activeInHierarchy)
            {
                manager.LoadMainMenu();
            }

            if(Input.GetKey(KeyCode.Escape) && manager.shipManager.gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
            {
                manager.LoadMainMenu();
            }

            if(Input.GetKey(KeyCode.Space) && manager.Story_Panel.activeInHierarchy)
            {
                manager.StartStory();
            }
        }
    }
    
    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Interactables")
        {
            if(!IsCarryingObject)
            {
                ObjectPicked(collision.gameObject);

                ObjectsCollected.Add(collision.gameObject);
            }
        }
    }

    private IEnumerator DelayBetweenSpace()
    {
        yield return new WaitForSeconds(1f);

        IsSwitching = false;
    }

    private void ObjectPicked(GameObject g)
    {
        manager.am.PlayCollectingSound();

        IsCarryingObject = true;

        g.SetActive(false);

        manager.LoadGetBackToBoatText();
    }

    public void Initiate_overlay()
    {
        OverlayObject.SetActive(true);
    }

    public void Stop_Overlay()
    {
        OverlayObject.SetActive(false);
    }

    private void ResetRigidBody()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}