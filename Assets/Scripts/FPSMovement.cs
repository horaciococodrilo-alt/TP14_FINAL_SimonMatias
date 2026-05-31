using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float gravedad = -9.81f;
    public float fuerzaSalto = 1.2f;   // salto chico

    [Header("Camara / Mouse")]
    public float sensibilidadMouse = 2f;
    public Transform camara;

    private CharacterController controller;
    private float velocidadVertical = 0f;
    private float rotacionX = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse;
        transform.Rotate(Vector3.up * mouseX);
        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -80f, 80f);
        if (camara != null)
            camara.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 movimiento = transform.right * h + transform.forward * v;

        if (controller.isGrounded)
        {
            if (velocidadVertical < 0)
                velocidadVertical = -2f;

            if (Input.GetButtonDown("Jump"))
                velocidadVertical = Mathf.Sqrt(fuerzaSalto * -2f * gravedad);
        }

        velocidadVertical += gravedad * Time.deltaTime;

        Vector3 velocidadFinal = movimiento * velocidad;
        velocidadFinal.y = velocidadVertical;
        controller.Move(velocidadFinal * Time.deltaTime);
    }
}