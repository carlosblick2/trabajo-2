using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    public float valor = 100;
    public Slider vidaVisual;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        vidaVisual.GetComponent<Slider>().value = valor;

        if (valor <= 0)
        {
            Debug.Log("GAME OVER");
        }
    }
    

    public void RecibirDanio(float danio)
    {
        valor -= danio;
        if (valor < 0)
        {
            valor = 0;
        }
    }
}

