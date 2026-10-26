using UnityEngine;

public class PlayerController : MonoBehaviour, ISaveable
{

    [Header("Sword Material")]
    [SerializeField] Dissolve dissolve;
    

    private CharacterBehaviour character;

    //Inputs
    private float inputDirection => Input.GetAxisRaw("Horizontal"); 
    private bool moveInput => inputDirection != 0f;
    private bool runInput => Input.GetKey(KeyCode.LeftShift);
    private bool jumpInput => Input.GetKeyDown(KeyCode.Z);
    private bool dashInput => Input.GetKeyDown(KeyCode.C);
    private bool attackInput => Input.GetKeyDown(KeyCode.X);


    void Awake()
    {
        character = GetComponent<CharacterBehaviour>();
    }
    void Update()
    {
        if (character.jump.IsExecuting) Debug.Log("negga");
        character.actionInput.InitiateInput
        (
            inputDirection, 
            moveInput,
            runInput,
            jumpInput,
            dashInput,
            attackInput
        );

        if (character.rb.linearVelocity.y > 0f)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                EnvironmentManager.Instance.IncreaseDepth();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                EnvironmentManager.Instance.DecreaseDepth();
            }
        }

    }

    public void DieEvent()
    {
        GameManager.Instance.GameOver();
    }


    //get save and load data
    public void Save(SaveData data)
    {
        data.player.position = new float[2];
        data.player.position[0] = transform.position.x;
        data.player.position[1] = transform.position.y;

        data.player.currentHealth = character.healthPoint.CurrentHealth;
    }
    public void Load(SaveData data)
    {
        transform.position = new Vector3
        (
            data.player.position[0],
            data.player.position[1],
            0f
        );

        character.healthPoint.CurrentHealth = data.player.currentHealth;
    }




}
