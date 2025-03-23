using UnityEngine;
using UnityEngine.UI;

public class SkillClick : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(DisableButton);
    }

    private void DisableButton()
    {
        button.interactable = false;
    }
}
