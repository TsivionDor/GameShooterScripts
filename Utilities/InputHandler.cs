
using Commands;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Dictionary<KeyCode, BaseKeyDownCommand> _keycommandMapping = new Dictionary<KeyCode, BaseKeyDownCommand>();
    private Dictionary<string, PassAxisCommand> _keyAxisCommandMapping = new Dictionary<string, PassAxisCommand>();
    private Player _player;

    private void Update()
    {
        foreach (var keyCommand in _keycommandMapping)
        {
            if (Input.GetKey(keyCommand.Key))
            {
                keyCommand.Value.Execute();
            }
        }
        foreach (var axisCommand in _keyAxisCommandMapping)
        {
            float axisValue = Input.GetAxis(axisCommand.Key);
            if (Mathf.Abs(axisValue) > 0.01f)
            {
                axisCommand.Value.Execute(axisValue);
            }
        }
    }
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        InitializeCommands();
    }

    private void InitializeCommands()
    {
        _keyAxisCommandMapping["Vertical"] = new MoveForwardCommand();
        _keyAxisCommandMapping["Horizontal"] = new MoveHorizontalCommand();
       

    }



    void ChangeControllerToCommand(BaseController controller)
    {
        foreach (var keyCommand in _keycommandMapping)
        {
            keyCommand.Value.Initialize(controller);
        }

        foreach (var keyAxisCommand in _keyAxisCommandMapping)
        {
            keyAxisCommand.Value.Initialize(controller);
        }
    }




}
