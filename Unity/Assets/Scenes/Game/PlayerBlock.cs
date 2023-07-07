using System.Collections;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    public KeyCode blockKey = KeyCode.S;
    public float blockDuration = 1f;
    public GameObject blockEffectPrefab;

    private bool isBlocking = false;
    private Player player;
    private GameObject blockEffect;
    private Vector2 blockDirection;
    public Animator block_Ani;

    public bool IsBlocking
    {
        get { return isBlocking; }
    }

    private void Start()
    {
        player = GetComponent<Player>();
        Enemy.OnLegionaerDeath += OnLegionaerDeath;
    }

    private void OnDestroy()
    {
        Enemy.OnLegionaerDeath -= OnLegionaerDeath;
    }

    private void OnLegionaerDeath()
    {
        // Reset block state when Legionaer enemy dies
        isBlocking = false;
        if (blockEffect != null)
        {
            Destroy(blockEffect);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(blockKey))
        {
            FindObjectOfType<AudioManager>().Play("BlockOn");
            block_Ani.Play("Secutor_Block");
            StartBlocking();
        }
        else if (Input.GetKeyUp(blockKey))
        {
            FindObjectOfType<AudioManager>().Play("BlockOff");
            StopBlocking();
            block_Ani.SetTrigger("block");
        }
    }

    private void StartBlocking()
    {
        if (!isBlocking && player.CurrentHealth > 0)
        {
            isBlocking = true;

            // Get the direction the player is facing
            Vector2 facingDirection = transform.up;

            // Get the sign of the player's scale in the x-axis
            float scaleSign = Mathf.Sign(transform.localScale.x);

            // Calculate the block direction based on the facing direction and the scale sign
            blockDirection = facingDirection.normalized * -scaleSign;
            StartCoroutine(BlockCoroutine());

            // Instantiate block effect
            if (blockEffectPrefab != null)
            {
                FindObjectOfType<AudioManager>().Play("BlockHit");
                blockEffect = Instantiate(blockEffectPrefab, transform.position, Quaternion.identity);
                blockEffect.transform.parent = transform;
            }

            // Disable movement and combat during blocking
            GetComponent<Movement>().enabled = false;
            GetComponent<PlayerCombat>().enabled = false;

            // Log the block direction
            Debug.Log("Block Direction: " + blockDirection);
        }
    }

    private void StopBlocking()
    {
        if (isBlocking)
        {
            isBlocking = false;

            // Destroy block effect
            if (blockEffect != null)
            {
                Destroy(blockEffect);
            }

            // Enable movement and combat after blocking
            GetComponent<Movement>().enabled = true;
            GetComponent<PlayerCombat>().enabled = true;
        }
    }

    private IEnumerator BlockCoroutine()
    {
        float startTime = Time.time;
        float endTime = startTime + blockDuration;

        while (Time.time < endTime)
        {
            yield return null;
        }

        StopBlocking();
    }

    public void OnEnemyAttack(Vector2 attackDirection)
    {
        FindObjectOfType<AudioManager>().Play("BlockHit");
        if (isBlocking)
        {
            // Calculate the dot product between the player's block direction and the attack direction
            float dotProduct = Vector2.Dot(blockDirection, attackDirection.normalized);

            // Check if the attack is coming from behind based on the player's position
            bool isAttackFromBehind = transform.position.x < attackDirection.x;

            if (dotProduct > 0.5f && !isAttackFromBehind)
            {
                // Player successfully blocks the attack
                Debug.Log("Attack Blocked!");
            }
            else
            {
                // Player fails to block the attack
                //player.TakeDamage(1);
            }
        }
        else
        {
            //player.TakeDamage(1);
        }
    }
}
