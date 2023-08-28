using UnityEngine;
using TMPro;

public class PlayerDataObserver : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] TextMeshProUGUI playerDataText;

    private void Start()
    {
        playerData.OnDataChanged.AddListener(UpdatePlayerDataDisplay);
        UpdatePlayerDataDisplay();
    }

    private void OnDestroy()
    {
        playerData.OnDataChanged.RemoveListener(UpdatePlayerDataDisplay);
    }

    void UpdatePlayerDataDisplay()
    {
        if (GameManager.Instance.SelectedCharacter != null)
        {
            playerDataText.text = $"Character: {GameManager.Instance.SelectedCharacter.Name}\n";
        }
    }
}