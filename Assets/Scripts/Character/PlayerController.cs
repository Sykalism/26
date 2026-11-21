using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour, ISaveable
{

    [Header("Sword Material")]
    [SerializeField] Dissolve dissolve;

    [Header("Effect")]
    [SerializeField] GameObject dashParticle;
    [SerializeField] Transform spawnPoint;
    

    public CharacterBehaviour character {get; private set;}

    //Inputs
    private float inputDirection => Input.GetAxis("Horizontal"); 
    private bool moveInput => inputDirection != 0f;
    private bool runInput => Input.GetKey(KeyCode.LeftShift);
    private bool jumpInput => Input.GetKeyDown(KeyCode.Z);
    private bool dashInput => Input.GetKeyDown(KeyCode.C);
    private bool attackInput => Input.GetKeyDown(KeyCode.X);
    private bool parryInput => Input.GetKey(KeyCode.LeftControl);
    private bool flipInput;


    void Awake()
    {
        character = GetComponent<CharacterBehaviour>();
    }
    void Update()
    {
        if (!character.controlFlipViaAnimation)
        {
            flipInput = character.directional.isFlipped;
        }
        character.actionInput.InitiateInput
        (
            inputDirection, 
            moveInput,
            runInput,
            jumpInput,
            dashInput,
            attackInput,
            flipInput,
            parryInput
            
        );

        if (character.isGrounded)
        {
            character.controlFlipViaAnimation = true;
            character.directional.autoSet = false;
        }
        else
        {
            character.controlFlipViaAnimation = false;
            character.directional.autoSet = true;
        }

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
        if (character.dash.StartAction)
        {
            Instantiate(dashParticle, spawnPoint.position, spawnPoint.rotation );
            
        }
    }


    public void DieEvent()
    {
        GameManager.Instance.GameOver();
    }

    public bool GetFlipInput()
    {
        return flipInput;
    }
    public void SetFlipInput(bool value)
    {
        flipInput = value;
    }

    //get save and load data
    public void Save(SaveData data)
    {
        data.player.position = new float[2];
        data.player.position[0] = transform.position.x;
        data.player.position[1] = transform.position.y;

        data.player.currentHealth = character.stats.health.current;
    }
    public void Load(SaveData data)
    {
        transform.position = new Vector3
        (
            data.player.position[0],
            data.player.position[1],
            0f
        );

        character.stats.health.SetCurrent(data.player.currentHealth);
    }




}
