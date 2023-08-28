using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;



namespace Commands
{

    public abstract class BaseCommand
    {
        protected BaseController _controller;
        public void Initialize(BaseController controller)
        {
            _controller = controller;
        }
    }

    public abstract class BaseKeyDownCommand : BaseCommand
    {
        public abstract void Execute();
    }

    public abstract class PassAxisCommand : BaseCommand
    {
        public abstract void Execute(float axis);
    }

    public class MoveVerticalCommand : PassAxisCommand
    {
        public override void Execute(float axis)
        {
            _controller.MoveVertical(axis);
        }
    }

    public class MoveHorizontalCommand : PassAxisCommand
    {
        public override void Execute(float axis)
        {
            _controller.MoveHorizontal(axis);
        }
    }

   
    public class MoveForwardCommand : PassAxisCommand
    {
        public override void Execute(float axis)
        {
            _controller.MoveVertical(axis);
        }
    }


}