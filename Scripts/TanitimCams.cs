using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TanitimCams : MonoBehaviour
{
    public List<Camera> tanitimCams = new List<Camera>();

    void Update() {
        if (PlayerPrefs.GetInt("tanitim") == 1) {
            FirstPersonLook.stopped = true;
            PlayerPrefs.SetInt("tanitim", 0);
            StartCoroutine(CycleCameras());
        }
    }

    IEnumerator CycleCameras() {
        int ind = 0;
        // İlk kamera aktif olsun
        UpdateActiveCamera(ind);

        while (ind < 6) {
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                ind++;
                UpdateActiveCamera(ind);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && ind > 0) {
                ind--;
                UpdateActiveCamera(ind);
            }
            yield return null; // Her frame sonunda döngüyü devam ettir
        }

        FirstPersonLook.stopped = false;
    }

    void UpdateActiveCamera(int activeIndex) {
        for (int i = 0; i < tanitimCams.Count; i++) {
            tanitimCams[i].gameObject.SetActive(i == activeIndex);
        }
    }
}
