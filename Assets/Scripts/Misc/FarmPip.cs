using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FarmPip : MonoBehaviour
{
    [SerializeField] private float lifetime;
    private float startLifetime = 3;
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private TextMeshPro textObj;
    [SerializeField] private Color goldColor;
    [SerializeField] private Color healthColor;
    private Color curColor;
    [SerializeField] private Sprite goldSprite;
    [SerializeField] private Sprite healthSprite;
    private Vector3 targetPos;
    private float speed = 0.5f;
    public void initialize(FarmType type, string text)
    {
        startLifetime = lifetime;
        switch (type)
        {
            case FarmType.Health:
                image.sprite = healthSprite;
                curColor = healthColor;
                break;
            case FarmType.Points:
                image.sprite = goldSprite;
                curColor = goldColor;
                text = "$" + text;
                break;
            default:
                image.sprite = goldSprite;
                curColor = goldColor;
                text = "$" + text;
                break;
        }
        textObj.color = curColor;
        textObj.text = text;
        targetPos = new Vector3(transform.position.x + Random.Range(-10f, 10f), transform.position.y, transform.position.z + Random.Range(-10f, 10f));
    }

    private void LateUpdate()
    {
        lifetime -= Time.deltaTime;
        textObj.alpha = lifetime/startLifetime;
        image.color = new Color(curColor.r, curColor.g, curColor.b, lifetime/startLifetime);
        if(lifetime <= 0)
        {
            Destroy(gameObject);
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

}
