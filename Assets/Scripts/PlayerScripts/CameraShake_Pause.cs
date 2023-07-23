using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake_Pause : MonoBehaviour
{
    public static CameraShake_Pause instance;

    private bool isShake;
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }

    public void HitPause(int duration)
    {

        StartCoroutine("Pause", duration);

    }

    public void CameraShake(float duration,float strength)
    {
        if(!isShake)
        {
            StartCoroutine(Shake(duration,strength));
        }

    }



    IEnumerator Pause(int duration)
    {
        float pauseTime = duration / 60f;
        Time.timeScale = 0.15f;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1;
    }



    IEnumerator Shake(float duration,float strength)
    {
        isShake = true;
        Transform camera = Camera.main.transform;
        Vector3 startPosition = camera.position;
        while(duration>0)
        {
            Camera.main.transform.position = Random.insideUnitSphere * strength + startPosition;
            duration -= Time.deltaTime;
            
            yield return null;
        }

        isShake = false;

        //camera.position = startPosition;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
