using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButtonDown : MonoBehaviour {
    public AudioSource audioSource;
    public Sprite[] kuang;

    public GameObject[] RawMaterial { get; set; }
    public GameObject EndProduct { get; set; }
    public Vector3 TargetPos { get; set; }

    private bool started;

    private void OnMouseDown() {
        if(started)
            return;
        started = true;

        StartCoroutine(DoCrafting());
    }

    private IEnumerator DoCrafting() {
        audioSource.Play();

        // only one child
        transform.GetChild(0).gameObject.SetActive(false);

        var temp = new GameObject("CraftTemp");
        temp.transform.position = TargetPos;

        foreach(var obj in RawMaterial) {
            //obj.GetComponent<ItemMove>().enabled = false;
            //obj.GetComponentInChildren<ItemCraft>().enabled = false;
            //obj.transform.SetParent(temp.transform, false);

            //Destroy(obj);

            // wtf???
            obj.transform.SetParent(temp.transform);
            obj.SetActive(false);
        }

        // todo: calibrate position

        var originPos = temp.transform.position;
        const float wiggleFactor = 0.04f; // todo: expose param

        // todo: use shader to glow
        var sr = temp.AddComponent<SpriteRenderer>();

        while(audioSource.isPlaying) {
            temp.transform.position = originPos + (Vector3)(Random.insideUnitCircle * wiggleFactor);

            sr.sprite = kuang[(int)(audioSource.time * 8 / 6)];

            yield return null;
        }

        Destroy(temp);
        Instantiate(EndProduct, TargetPos, Quaternion.identity);
    }
}
