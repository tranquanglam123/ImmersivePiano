using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Material _material;

    private void OnEnable()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _material = _meshRenderer.material;
        StartCoroutine(Fade(_material));
    }

    IEnumerator Fade(Material material)
    {
        bool isFading = true;
        while (true)
        {
            if (isFading)
            {
                for (float ft = 1f; ft >= 0; ft -= 0.1f)
                {
                    Color c = material.color;
                    c.a = ft;
                    material.color = c;
                    yield return new WaitForSeconds(.1f);
                }

                isFading = false;
            }
            else
            {
                for (float ft = 0f; ft <= 1; ft += 0.1f)
                {
                    Color c = material.color;
                    c.a = ft;
                    material.color = c;
                    yield return new WaitForSeconds(.1f);
                }

                isFading = true;
            }
        }
    }
}