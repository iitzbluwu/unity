using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviroment : MonoBehaviour
{
    public float speed = 1f; // Geschwindigkeit der Bewegung
    public Vector3 targetPosition; // Zielposition des Bildes

    public Animator vorhang;
    public GameObject vorhangObject;

    private void Start()
    {
        vorhang.Play("Vorhang");
        // Setze die Startposition des Bildes oberhalb des Kamera-Blickfelds
        transform.position = new Vector3(targetPosition.x, targetPosition.y + 8f, targetPosition.z);

        // Starte die Bewegung des Bildes
        StartCoroutine(MoveImage());
        Invoke("VorhangAus", 2f);
    }

    private IEnumerator MoveImage()
    {
        while (transform.position.y > targetPosition.y)
        {
            // Aktuelle Position des Bildes
            Vector3 currentPosition = transform.position;

            // Neue Position berechnen (in diesem Fall um 1 Einheit nach unten)
            Vector3 newPosition = currentPosition + Vector3.down * speed * Time.deltaTime;

            // Die Position des Bildes aktualisieren
            transform.position = newPosition;

            yield return null;
        }
    }
    void VorhangAus()
    {
        vorhangObject.SetActive(false);
    }
}
