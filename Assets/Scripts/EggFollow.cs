using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EggFollow : MonoBehaviour
{
    [SerializeField] private Transform[] eggPoints;
    [SerializeField] private GameObject canvasJump;
    [SerializeField] private GameObject canvasJumpLock;
    [SerializeField] private GameObject canvasShield;
    [SerializeField] private GameObject canvasShieldLock;
    [SerializeField] private GameObject canvasShoot;
    [SerializeField] private GameObject canvasShootLock;

    [SerializeField] private TextMeshProUGUI messageText;  
    [SerializeField] private float messageDuration = 3f;  
    [SerializeField] private float blinkInterval = 0.3f;
    private bool[] eggPointOccupied;

    private void Start()
    {
        eggPointOccupied = new bool[eggPoints.Length];
        messageText.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Egg"))
        {
            Egg egg = collision.gameObject.GetComponent<Egg>(); 
            if (egg != null)
            {
                AttachEgg(egg);
                Player player = GetComponent<Player>();

                if (egg.eggType == EggType.Jump)  
                {

                    player.UnlockJump();
                    canvasJump.SetActive(true);
                    canvasJumpLock.SetActive(false);
                    StartCoroutine(DisplayMessage("Double jump unlocked!"));
                    SoundManager.instance.PlaySound(SoundManager.Sound.Hint);

                }
                else if (egg.eggType == EggType.Shield)
                {
                    player.UnlockShield();
                    canvasShield.SetActive(true);
                    canvasShieldLock.SetActive(false);
                    StartCoroutine(DisplayMessage("Shield unlocked!"));
                    SoundManager.instance.PlaySound(SoundManager.Sound.Hint);

                }
                else if (egg.eggType == EggType.Shoot)
                {
                    player.UnlockShoot();
                    canvasShoot.SetActive(true);
                    canvasShootLock.SetActive(false);
                    StartCoroutine(DisplayMessage("Fireball unlocked!"));
                    SoundManager.instance.PlaySound(SoundManager.Sound.Hint);

                }
            }
        }
    }

    private void AttachEgg(Egg egg)
    {
       
        for (int i = 0; i < eggPoints.Length; i++)
        {
            if (!eggPointOccupied[i])  
            {
                egg.StopMove();
                egg.transform.SetParent(eggPoints[i]);          
                egg.transform.localPosition = Vector3.zero;
                egg.PlayHatchAnimation();
                eggPointOccupied[i] = true;

                Collider2D eggCollider = egg.GetComponent<Collider2D>();
                if (eggCollider != null)
                {
                    eggCollider.enabled = false;
                }
                return;                                         
            }
        }

        Debug.Log("No available egg points.");
    }

    private IEnumerator DisplayMessage(string message)
    {
        float elapsedTime = 0f;
        bool isTextVisible = true;

        messageText.gameObject.SetActive(true);  
        messageText.text = message;  

        while (elapsedTime < messageDuration)
        {
            isTextVisible = !isTextVisible; 
            messageText.gameObject.SetActive(isTextVisible);  

            yield return new WaitForSeconds(blinkInterval);  
            elapsedTime += blinkInterval;
        }

        messageText.gameObject.SetActive(false);  
    }

}
