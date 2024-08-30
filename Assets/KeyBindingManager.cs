using UnityEngine;
using UnityEngine.UI;

public class KeyBindingManager : MonoBehaviour
{
    public Move_Players movePlayers;

    // Références des boutons dans l'interface utilisateur
    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;
    public Button climbUpButton;
    public Button climbDownButton;
    public Button interactButton;

    private Button buttonToRebind;

    private void Start()
    {
        // Assurez-vous que le script Move_Players est assigné
        if (movePlayers == null)
        {
            movePlayers = FindObjectOfType<Move_Players>();
        }

        // Initialiser les textes des boutons
        UpdateButtonLabels();

        // Ajouter les événements aux boutons
        leftButton.onClick.AddListener(() => StartRebind(leftButton));
        rightButton.onClick.AddListener(() => StartRebind(rightButton));
        jumpButton.onClick.AddListener(() => StartRebind(jumpButton));
        climbUpButton.onClick.AddListener(() => StartRebind(climbUpButton));
        climbDownButton.onClick.AddListener(() => StartRebind(climbDownButton));
        interactButton.onClick.AddListener(() => StartRebind(interactButton));
    }

    private void Update()
    {
        if (buttonToRebind != null)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        AssignKey(buttonToRebind, keyCode);
                        buttonToRebind = null;
                        break;
                    }
                }
            }
        }
    }

    private void StartRebind(Button button)
    {
        buttonToRebind = button;
    }

    private void AssignKey(Button button, KeyCode newKey)
    {
        if (button == leftButton)
        {
            movePlayers.leftKey = newKey;
        }
        else if (button == rightButton)
        {
            movePlayers.rightKey = newKey;
        }
        else if (button == jumpButton)
        {
            movePlayers.jumpKey = newKey;
        }
        else if (button == climbUpButton)
        {
            movePlayers.climbUpKey = newKey;
        }
        else if (button == climbDownButton)
        {
            movePlayers.climbDownKey = newKey;
        }
        else if (button == interactButton)
        {
            movePlayers.interact = newKey;
        }

        button.GetComponentInChildren<Text>().text = newKey.ToString();
    }

    private void UpdateButtonLabels()
    {
        // Initialise les textes des boutons avec les touches actuelles
        leftButton.GetComponentInChildren<Text>().text = movePlayers.leftKey.ToString();
        rightButton.GetComponentInChildren<Text>().text = movePlayers.rightKey.ToString();
        jumpButton.GetComponentInChildren<Text>().text = movePlayers.jumpKey.ToString();
        climbUpButton.GetComponentInChildren<Text>().text = movePlayers.climbUpKey.ToString();
        climbDownButton.GetComponentInChildren<Text>().text = movePlayers.climbDownKey.ToString();
        interactButton.GetComponentInChildren<Text>().text = movePlayers.interact.ToString();
    }
}
