using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LogicaEnemigo : MonoBehaviour
{
    private GameObject target;
    private NavMeshAgent agente;
    private Vida vida;
    private Animator animator;
    private Collider collider;
    private Vida vidaJugador;
    private LogicaJugador logicaJugador;
    public bool Vida0 = false;
    public bool estaAtacando = false;
    public float speed = 1.0f;
    public float angularSpeed = 120;
    public float danio = 25;
    public bool mirando;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Raven");
        vida = gameObject.GetComponent<Vida>();
        vidaJugador = target.GetComponent<Vida>();
        if (vidaJugador == null)
        {
            throw new System.Exception("El objeto Jugador no tiene componente Vida");
        }

        logicaJugador= target.GetComponent<LogicaJugador>();

        if (logicaJugador == null)
        {
            throw new System.Exception("El objeto Jugador no tiene componente LogicaJugador");
        }


        agente = GetComponent<NavMeshAgent>();
        vida = GetComponent<Vida>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
        RevisarVida();
        Perseguir();
        RevisarAtaque();
        EstaDeFrenteAlJugador();
    }

    void EstaDeFrenteAlJugador()
    {
        Vector3 adelante = transform.forward;
        Vector3 targetJugador = (GameObject.Find("Player").transform.position - transform.position).normalized;

    }

    void RevisarVida()
    {
        if (Vida0) return;
        if (vida.valor <= 0)
        {
            Vida0 = true;
            agente.isStopped = true;
            collider.enabled = false;
            animator.CrossFadeInFixedTime("Vida0", 0.1f);
            Destroy(gameObject, 3f);
        }
    }

    void Perseguir()
    {
        if (Vida0) return;
        if (logicaJugador.Vida0) return;
        agente.destination = target.transform.position;
    }

    void RevisarAtaque()
    {
        
        if (Vida0) return;
        if (estaAtacando) return;
        if (logicaJugador.Vida0) return;
        
        float distanciaDelBlanco = Vector3.Distance(target.transform.position, transform.position);
        print(distanciaDelBlanco);
        if (distanciaDelBlanco <= 2.0)
        {
            Atacar();
        }
    }

    void Atacar()
    {
        vidaJugador.RecibirDanio(danio);
        agente.speed = 0;
        agente.angularSpeed = 0;
        estaAtacando = true;
        animator.SetTrigger("DebeAtacar");
        Invoke("ReiniciarAtaque", 1.5f);
    }

    void ReiniciarAtaque()
    {
        estaAtacando = false;
        agente.speed = speed;
        agente.angularSpeed = angularSpeed;
    }
}
