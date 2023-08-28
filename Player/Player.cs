using UnityEngine;
using Commands;
using System;

public class Player : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField] private float moveSpeed;

    private PassAxisCommand moveVerticalCommand;
    private PassAxisCommand moveHorizontalCommand;

    private SkillBase currentWeapon;
    private PlayerData playerData;

    public float currentMoveSpeed { get; private set; }

    public void Initialize(PlayerCharacter character, PlayerData data)
    {
        currentWeapon = character.StartingWeapon;
        moveSpeed = character.Might; // Assuming MightBonus as movement speed modifier
        playerData = data;
        currentMoveSpeed = moveSpeed; // Set initial speed

        // Initialize movement commands
        moveVerticalCommand = new MoveVerticalCommand();
        moveHorizontalCommand = new MoveHorizontalCommand();

        moveVerticalCommand.Initialize(new BaseController(this));
        moveHorizontalCommand.Initialize(new BaseController(this));
    }

    private void Update()
    {
        // Execute movement commands based on user input
        moveVerticalCommand.Execute(Input.GetAxis("Vertical"));
        moveHorizontalCommand.Execute(Input.GetAxis("Horizontal"));

        currentWeapon?.Activate(playerData); // If the weapon needs any data from the player
    }

    public void MoveVertical(float axis)
    {
        transform.Translate(Vector3.up * axis * currentMoveSpeed * Time.deltaTime);
    }

    public void MoveHorizontal(float axis)
    {
        transform.Translate(Vector3.right * axis * currentMoveSpeed * Time.deltaTime);
    }

    public float GetMagnetRangeBoost()
    {
        // Return magnet range boost if any, or 0f if none
        return 0f;
    }


}

// Controller class to pass the context to the command
