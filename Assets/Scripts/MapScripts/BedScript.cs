using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BedScript : MonoBehaviour
{
    public GameObject restMessageText;
    public InputAction interactAction;

    private bool playerInRange;

    private void OnEnable()
    {
        interactAction.Enable();
        interactAction.performed += OnInteract;
    }

    private void OnDisable()
    {
        interactAction.performed -= OnInteract;
        interactAction.Disable();
    }

    private void Start()
    {
        if (restMessageText != null)
        {
            restMessageText.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        playerInRange = true;

        if (restMessageText != null)
        {
            restMessageText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = false;
        if (restMessageText != null)
        {
            restMessageText.SetActive(false);
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (playerInRange)
        {
            Rest();
            restMessageText.SetActive(false);
        }
    }

    private void Rest()
    {
        GameStateManager.Instance.ResetEnemies();
        
        PlayerHealth playerHealth = FindAnyObjectByType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.HealFull();
        }
        Debug.Log("Rest");
    }
}
