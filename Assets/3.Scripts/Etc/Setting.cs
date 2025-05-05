using Michsky.MUIP;
using UnityEngine;

public class Setting : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private ButtonManager button;

    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
}
