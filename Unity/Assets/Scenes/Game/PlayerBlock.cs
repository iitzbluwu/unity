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
            StartBlocking();
        }
        else if (Input.GetKeyUp(blockKey))
        {
            StopBlocking();
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
                blockEffect = Instantiate(blockEffectPrefab, transform.position, Quaternion.identity);
                blockEffect.transform.parent = transform;
            }

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
                player.TakeDamage(10);
            }
        }
        else
        {
            player.TakeDamage(10);
        }
    }
}
