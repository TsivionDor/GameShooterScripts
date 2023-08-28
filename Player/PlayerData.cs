using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        currentCharacter = GameManager.Instance.SelectedCharacter;
        NotifyDataChanged();
    }
    public Player playerComponent;
    public EnemyController enemyController;

    public PlayerCharacter currentCharacter;
    [System.Serializable]
    public class PlayerDataChangedEvent : UnityEvent { }

    public PlayerDataChangedEvent OnDataChanged = new PlayerDataChangedEvent();




    public void NotifyDataChanged()
    {
        OnDataChanged?.Invoke();
    }



}