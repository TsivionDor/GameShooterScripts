using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour
{
    public static CharacterSelectionManager Instance; // Singleton instance
    private CharacterButton currentActiveButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleButton(CharacterButton newButton)
    {
        if (currentActiveButton != null)
        {
            if (currentActiveButton == newButton)
            {
                // Close the current button if it's clicked again
                currentActiveButton.ToggleExtendedInfo(false);
                currentActiveButton = null; // reset
                return;
            }
            else
            {
                // Close the previously active button
                currentActiveButton.ToggleExtendedInfo(false);
            }
        }

        currentActiveButton = newButton;
        currentActiveButton.ToggleExtendedInfo(true);
    }
}