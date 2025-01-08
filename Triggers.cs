using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Triggers : MonoBehaviour
{

    public GameObject pig;
    public Rigidbody pigRB;
    public Transform TP1;
    public GameObject T2;
    private CharacterController controller;
    private Transform player;
    public TextMeshProUGUI interact;
    public Canvas padUI;
    public GameObject padDoor;
    public GameObject PT;
    public Button[] buttons;
    private int[] correctCode;

    private int n1;
    public TextMeshProUGUI code1;
    private int n2;
    public TextMeshProUGUI code2;
    private int n3;
    public TextMeshProUGUI code3;

    private int[] pressedButtons = new int[3];
    private int pressCount = 0;

    private bool inPT = false;
    private bool inOil1 = false;
    private bool inOil2 = false;
    private bool inOil3 = false;

    public GameObject oil1;
    public GameObject oil2;
    public GameObject oil3;
    public GameObject barrels1;
    public GameObject barrels2;

    public Animator pigAnimator;

    public AudioSource audioSource; //roar
    public AudioSource audioSource2; //stairs noise
    public AudioSource audioSource3; //oil
    public AudioSource audioSource4; //barrels
    public AudioSource audioSource5; //key

    private bool fePicked = false;
    private bool fePlaced = false;
    private bool inCart1 = false;
    private bool inCart2 = false;
    public GameObject FE1;
    public GameObject FE2;
    public GameObject smoke;
    public GameObject lastDoor;

    public GameObject key;
    private bool inKey;
    private bool hasKey;

    public Transform TP2;
    public GameObject Drop;

    private bool inExit = false;

    public GameObject V1;
    public GameObject V2;
    public AudioSource audioSource6; //V1
    public AudioSource audioSource7; //radio
    public AudioSource audioSource8; //V2
    public AudioSource audioSource9; //V3
    public AudioSource audioSource10; //V4
    public TextMeshProUGUI instructions;

    public Light flashlight;

    public bool pigMoving;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GetComponent<Transform>();
        interact.text = "";
        padDoor.SetActive(true);
        padUI.enabled = false;
        n1 = Random.Range(1, 9);
        n2 = Random.Range(1, 9);
        n3 = Random.Range(1, 9);
        correctCode = new int[] {n1, n2, n3};

        code1.text = n1 + " _ _";
        code2.text = "_ " + n2 + " _";
        code3.text = "_ _ " + n3;

        instructions.text = "Find the code to the hallway hidden in the room  \n(# _ _) = digit 1 \n(_ # _) = digit 2 \n(_ _ #) = digit 3";

        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i + 1;
            buttons[i].onClick.AddListener(() => OnButtonPressed(buttonIndex));
        }

        barrels2.SetActive(false);
        FE1.SetActive(true);
        FE2.SetActive(false);
        smoke.SetActive(false);
        key.SetActive(false); 
        lastDoor.SetActive(true);
        hasKey = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(inPT)
            {
                padUI.enabled = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                controller.enabled = false;
            }

            if(inOil1)
            {
                oil1.gameObject.SetActive(false);
                interact.text = "";

                if (oil2.activeSelf || oil3.activeSelf)
                {
                    audioSource3.Play();
                }
                else
                {
                    audioSource4.Play();
                }
            }

            if (inOil2)
            {
                oil2.gameObject.SetActive(false);
                interact.text = "";

                if (oil1.activeSelf || oil3.activeSelf)
                {
                    audioSource3.Play();
                }
                else
                {
                    audioSource4.Play();
                }
            }

            if (inOil3)
            {
                oil3.gameObject.SetActive(false);
                interact.text = "";

                if (oil1.activeSelf || oil2.activeSelf)
                {
                    audioSource3.Play();
                }
                else
                {
                    audioSource4.Play();
                }
            }

            if(inCart1 && !fePlaced)
            {
                fePicked = true;
                FE1.SetActive(false);
            }

            if(inCart2 && fePicked && !fePlaced)
            {
                fePlaced = true;
                fePicked = false;
                FE2.SetActive(true);
                smoke.SetActive(true);
                key.SetActive(true);
                audioSource5.Play();
                audioSource.volume = 0.3f;
                audioSource.Play();
                lastDoor.SetActive(false);
            }

            if(inKey)
            {
                key.SetActive(false);
                inKey = false;
                interact.text = "Holding key";
                hasKey = true;
                audioSource9.Play();
                audioSource7.Play();
                instructions.text = "ESCAPE OUT THE EXIT NOW!";
            }

            if(inExit && hasKey)
            {
                controller.enabled = false;
                Vector3 newPos = player.position;
                newPos = new Vector3(-10f, -10f, -10f);
                player.position = newPos;
                interact.text = "You Escaped!";
                audioSource10.Play();
                audioSource7.Play();
                flashlight.enabled = false;
            }

        }

        if(!oil1.gameObject.activeSelf)
        {
            inOil1 = false;
        }

        if(!oil2.gameObject.activeSelf)
        {
            inOil2 = false;
        }

        if(!oil3.gameObject.activeSelf)
        {
            inOil3 = false;
        }

            if (!oil1.activeSelf && !oil2.activeSelf && !oil3.activeSelf)
        {
            barrels1.SetActive(false);
            barrels2.SetActive(true);
        }

        if (pigMoving)
        {
            pig.transform.Translate(Vector3.forward * .04f);
        }

        AnimatorStateInfo stateInfo = pigAnimator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Jump") && stateInfo.normalizedTime >= 0.8f && !pigMoving)
        {
            pig.GetComponent<Collider>().enabled = false;
        }


    }

    void OnButtonPressed(int buttonId)
    {

        if (pressCount != 3)
        {
            pressedButtons[pressCount] = buttonId;
            pressCount++;
        }
        if(pressCount == 3)
        {
            CheckCode();
            padUI.enabled = false;
        }


    }

    void CheckCode()
    {

        padUI.enabled = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        controller.enabled = true;

        bool isCorrect = true;

        for (int i = 0; i < correctCode.Length; i++)
        {
            if (pressedButtons[i] != correctCode[i])
            {
                isCorrect = false;
                break;
            }
        }
            if(isCorrect)
            { 
                padDoor.SetActive(false);
                PT.SetActive(false);
                interact.text = "";
                inPT = false;
                pigAnimator.Play("Roar", 0, 0.2f);
                pigAnimator.SetTrigger("Jump");

                audioSource.time = .3f;
                audioSource.Play();
            }
            else
            {
                pressedButtons = new int[3];
                pressCount = 0;
            }
    }

    void PigMove()
    {
        pigMoving = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("PT"))
        {
            interact.text = "E to use pad";
            inPT = true;
        }

        if(other.CompareTag("Oil1"))
        {
            interact.text = "E clean oil";
            inOil1 = true;
        }

        if (other.CompareTag("Oil2"))
        {
            interact.text = "E clean oil";
            inOil2 = true;
        }

        if (other.CompareTag("Oil3"))
        {
            interact.text = "E clean oil";
            inOil3 = true;
        }

        if(other.CompareTag("Cart1"))
        {
            inCart1 = true;

            if (!fePicked)
            {
                interact.text = "Pick up fire extinguisher";
            }
            if(fePicked)
            {
                interact.text = "Holding fire extinguisher";
            }
            if (fePlaced)
            {
                interact.text = "";
            }
        }

        if (other.CompareTag("Cart2"))
        {
            inCart2 = true;

            if (!fePlaced && fePicked)
            {
                interact.text = "Place fire extinguisher";
            }
            if (fePlaced)
            {
                interact.text = "";
            }
        }

        if(other.CompareTag("Key"))
        {
            inKey = true;

            if(key.activeSelf)
            {
                interact.text = "Pick up key";
            }
            else
            {
                interact.text = "Holding key";
            }
        }

        if(other.CompareTag("Exit"))
        {
            inExit = true;

            if(hasKey)
            {
                interact.text = "Use Key";
            }
            else
            {
                interact.text = "Requires Key";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("PT"))
        {
            interact.text = "";
            inPT = false;
        }
        if(other.CompareTag("T2"))
        {
            pig.transform.position = TP1.transform.position;
            pigAnimator.Play("Walk", 0, 0.1f);
            PigMove();
            audioSource2.Play();
            T2.SetActive(false);
            pig.GetComponent<Collider>().enabled = true;
            audioSource2.enabled = true;
        }
        if (other.CompareTag("Oil1") || (other.CompareTag("Oil2")) || (other.CompareTag("Oil3")))
        {
            interact.text = "";
        }

        if (other.CompareTag("Oil1"))
        {
            interact.text = "";
            inOil1 = false;
        }
        if (other.CompareTag("Oil2"))
        {
            interact.text = "";
            inOil2 = false;
        }
        if (other.CompareTag("Oil3"))
        {
            interact.text = "";
            inOil3 = false;
        }
        if (other.CompareTag("Cart1"))
        {
            inCart1 = false;

            if (!fePicked || fePlaced)
            {
                interact.text = "";
            }
            else
            {
                interact.text = "Holding fire extinguisher";
            }
        }

        if (other.CompareTag("Cart2"))
        {
            inCart2 = false;

            if (!fePicked || fePlaced)
            {
                interact.text = "";
            }
            else
            {
                interact.text = "Holding fire extinguisher";
            }
        }

        if (other.CompareTag("Key"))
        {
            inKey = false;

            if (key.activeSelf)
            { 
                interact.text = "";
            }
            else
            {
            interact.text = "Holding key";
            }
        }

        if (other.CompareTag("Drop") && hasKey)
        {
            pig.SetActive(true);
            pig.transform.position = TP2.transform.position;
            pig.transform.rotation *= Quaternion.Euler(0, 0f, 0);
            pigAnimator.Play("JumpDown",0 ,0.1f);
            pigAnimator.SetTrigger("Dies");
            Drop.SetActive(false);
            pigRB.useGravity = true;
        }

        if (other.CompareTag("Exit"))
        {
            if(hasKey)
            {
                interact.text = "Holding Key";
            }
            if(fePicked)
            {
                interact.text = "Holding fire extinguisher";
            }
            else
            {
                interact.text = "";
            }
        }

        if(other.CompareTag("V1"))
        {
            audioSource6.Play();
            audioSource7.Play();
            instructions.text = "Clean up the oil spills";
            V1.SetActive(false);
        }

        if (other.CompareTag("V2"))
        {
            audioSource8.Play();
            audioSource7.Play();
            instructions.text = "Pick up the Fire Hydrant up the stairs and put it on the cart below the red light to clean out the vents";
            V2.SetActive(false);
        }

    }

}

