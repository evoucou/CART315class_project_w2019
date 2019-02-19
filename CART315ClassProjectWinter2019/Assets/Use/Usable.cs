﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Usable : MonoBehaviour
{

    /**
     * How far away can an object be and
     * still get used?
     */
    public float maxDistance = 2.0f;

  
    /**
     * These can be set in the Unity editor and will be
     * optionally triggered by the use action
     */
    private ParticleSystem specialEffect;
    private AudioSource sound;
    public AudioClip audioClip;


    /**
     * This optional event can be set in the editor
     * to call any function from any script after this
     * item has been used
     */
    public UnityEvent doAfterBeingUsed;

    private void Start()
    {
        specialEffect = GetComponent<ParticleSystem>();
        sound = GetComponent<AudioSource>();
    }

    /**
     * Use this object on whatever is in front of the
     * character
     */
    public void Use(bool shouldDestroyAfterUse = false)
    {
        //Get the object in front of this player
        //Call Use(useTarget, shouldDestroyAfterUse)
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.parent.parent.position, transform.forward, out hit, maxDistance))
        {
            UseTarget TargetUse = hit.collider.gameObject.GetComponent<UseTarget>();
            if (TargetUse != null)
            {
                TargetUse.Use();
                if (specialEffect != null)
                {
                    specialEffect.Play();

                }
                if (sound != null)
                {
                    sound.PlayOneShot(audioClip);
                }
                if (shouldDestroyAfterUse)
                {
                    Destroy(gameObject);
                }
                if (doAfterBeingUsed != null)
                {
                    doAfterBeingUsed.Invoke();
                }
            }
        }
    }

    /**
     * Use this object on a specific target
     */
    public void Use(GameObject target, bool shouldDestroyAfterUse = false)
    {
        //Trigger the particles and sound
        //If shouldDestroyAfterUse is true, then destroy this object
        //Finally, do whatever after used
        UseTarget TargetUse = target.GetComponent<UseTarget>();
        if (TargetUse != null)
        {
            TargetUse.Use();
            if (specialEffect != null)
            {
                specialEffect.Play();

            }
            if (sound != null)
            {
                sound.PlayOneShot(audioClip);
            }
            if (shouldDestroyAfterUse)
            {
                Destroy(gameObject);
            }
        }
        if (doAfterBeingUsed != null)
        {
            doAfterBeingUsed.Invoke();
        }
    }

   

}
