public class BaseController
{
    private Player _player;

    public BaseController(Player player)
    {
        _player = player;
    }

    public void MoveVertical(float axis)
    {
        _player.MoveVertical(axis);
    }

    public void MoveHorizontal(float axis)
    {
        _player.MoveHorizontal(axis);
    }
}