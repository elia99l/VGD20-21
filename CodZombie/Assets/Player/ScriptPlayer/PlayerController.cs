using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController controller;
    public float speed = 1.0f;
    public float mouseSensitivity = 1.0f;
    public float jumpStrength = 1.0f;
    public GameObject eyes;
    public GameController gameController;
    public Texture mirino;
    private static GameObject instance; //Per evitare che si creino altre istanza di un oggetto

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Awake() //Evita che venga eliminato il player quando passiamo ad una nuova scena
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RotateCharacter();
        MoveCharacter();

        RaycastHit hit;
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(eyes.transform.position, eyes.transform.forward, out hit))
            {
                if (hit.transform.tag == "Target")
                {
                    /* Hit a target */
                    gameController.IncreaseScore();
                    Destroy(hit.transform.gameObject);
                }
            }

        }
    }

    private void OnGUI() //Funzione per posizionare il mirino al centro dello shermo
    {
        GUI.DrawTexture(new Rect(Screen.width / 2, Screen.height / 2, 50, 50), mirino);
    }

    private void RotateCharacter()
    {
        Cursor.lockState = CursorLockMode.Locked;

        float x = Input.GetAxis("Mouse X") * mouseSensitivity;
        float y = Input.GetAxis("Mouse Y") * mouseSensitivity;

        Vector3 oldCharacterRotation = transform.localRotation.eulerAngles;
        Vector3 newRotation = new Vector3(0f, x + oldCharacterRotation.y, 0);
        transform.localRotation = Quaternion.Euler(newRotation);

        Vector3 oldEyesRotation = eyes.transform.localRotation.eulerAngles;
        Vector3 newEyesRotation = new Vector3(-y + oldEyesRotation.x, 0f, 0f);
        eyes.transform.localRotation = Quaternion.Euler(newEyesRotation);

    }
    private void MoveCharacter()
    {
        /*
        // SimpleMove Solution (No Jump) //
        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;

        Vector3 movement = (transform.forward * z) + (transform.right * x);

        controller.SimpleMove(movement);
        */

        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;
        float y = 0f;
      //  bool jump = Input.GetButtonDown("Jump");
      /*
        if (controller.isGrounded && jump)
        {
            y = jumpStrength;
        }
      */
        Vector3 movement = (transform.forward * z * Time.deltaTime) +
                            (transform.right * x * Time.deltaTime);

        movement.y = -9.81f * Time.deltaTime + y;

        controller.Move(movement);
    }
}
