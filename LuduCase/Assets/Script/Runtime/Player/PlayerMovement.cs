using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Orijinal Deðiþkenler
    public Vector2 _moveDirection;
    private Rigidbody rb;

    [SerializeField] private InputActionReference move;
    [SerializeField] private float walkSpeed = 5f; // FPS için hýzý biraz artýrdým

    // Kamera ve Rotasyon
    private Transform mainCameraTransform;
    // turnSpeed deðiþkenini kaldýrdým çünkü FPS'te dönüþü Mouse/Cinemachine halleder.

    // --- Yer Kontrolü Deðiþkenleri ---
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCameraTransform = Camera.main.transform;

        // FPS oyunlarýnda Rigidbody rotasyonunu dondurmak, fizik hatalarýný önler.
        // Karakterin devrilmesini engelleriz.
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Input Okuma
        _moveDirection = move.action.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayerToCamera();
    }

    // FPS için Karakteri Kameranýn Baktýðý Yöne Döndürme
    void RotatePlayerToCamera()
    {
        // Kameranýn sadece Y (yatay) rotasyonunu alýyoruz.
        // Yukarý/Aþaðý bakmayý (X ekseni) almýyoruz, yoksa karakter öne/arkaya devrilir.
        Quaternion targetRotation = Quaternion.Euler(0, mainCameraTransform.eulerAngles.y, 0);

        // Karakterin gövdesini kameranýn baktýðý yöne kilitliyoruz.
        transform.rotation = targetRotation;
    }

    void MovePlayer()
    {
        // Eðer input yoksa hýzý sýfýrla (Kaymayý önlemek için)
        if (_moveDirection == Vector2.zero)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            return;
        }

        // FPS Hareket Mantýðý:
        // Artýk karakter kamerayla ayný yöne baktýðý için;
        // transform.forward = Ýleri
        // transform.right = Saða (Strafe) demektir.

        Vector3 moveDir = transform.forward * _moveDirection.y + transform.right * _moveDirection.x;

        // Vektörü normalize et (Çapraz giderken hýzlanmayý önle)
        moveDir.Normalize();

        // Hýzý uygula (Y eksenindeki hýzý koru ki yerçekimi çalýþsýn)
        rb.velocity = new Vector3(moveDir.x * walkSpeed, rb.velocity.y, moveDir.z * walkSpeed);
    }
}