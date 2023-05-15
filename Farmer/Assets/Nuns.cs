using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuns : Collidable
{
    private float lastSpeak;
    private float cooldown = 4.0f;

    protected override void Start()
    {
        lastSpeak = -cooldown;
        // base.Start();
        boxCollider = transform.Find("MediumNun").GetComponent<BoxCollider2D>();
        // GameManager.instance.ShowText("heeyyyyy", 20, Color.black, transform.Find("SpeechBubble").position + new Vector3(0.0f, 0.0f, 0), Vector3.one, 0.01f);
        transform.Find("SpeechBubble").gameObject.SetActive(false);
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (Time.time - lastSpeak > cooldown)
        {
            lastSpeak = Time.time;
            Speak();
        }
    }

    private void Speak()
    {
        // StartCoroutine(ToggleSpeechBubble());
        transform.Find("SpeechBubble").gameObject.SetActive(true);
        GameManager.instance.ShowText("heeyyyyy this is a long\nsentence ?", 15, Color.black, transform.Find("SpeechBubble").position, Vector3.one, 5f);
    }

    private IEnumerator ToggleSpeechBubble()
    {
        transform.Find("SpeechBubble").gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        transform.Find("SpeechBubble").gameObject.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

        if (Time.time - lastSpeak <= cooldown)
        {
            transform.Find("SpeechBubble").gameObject.SetActive(false);
        }
    }
}
