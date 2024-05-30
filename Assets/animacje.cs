using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class animacje : MonoBehaviour
{
    public GameObject foodObject;//obiekt do dodania kolo skryptu 
    private NavMeshAgent agent;//wykrywanie agenta na scenie
    private float hunger;
    public Image HungerImage;//slider do dodania kolo skryptu 
    private float maxHunger = 100f;//maksymalna ilosc jedzenia
    private Animator animator;//wykrywanie animatora
    private bool isHungerDecreasing = true;//zatrzymanie spadania jedzenia
    private bool pickingUp = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        hunger = maxHunger;//poczatkowa wartosc jedzenia
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isHungerDecreasing)
        {
            hunger -= 10f * Time.deltaTime;//spadanie z czasem jedzenia
            hunger = Mathf.Clamp(hunger, 0f, maxHunger);//sprawdzenie czy zatrzymalo sie na 0
            HungerImage.fillAmount = hunger;//zaktualizowanie jedzenia
        }

        if (!pickingUp)
        {
            if (hunger <= 0 && foodObject != null)//jesli bedzie 0 jedzenia
            {
                agent.SetDestination(foodObject.transform.position);//agent podejdzie do obiektu jedzenia
                animator.SetBool("isWalking", false);//animacja chodzenia wlaczona
            }
            else
            {
                animator.SetBool("isWalking", true);//w innych przypadkach chodzenie wylaczone
            }
        }
    }

    private void OnTriggerEnter(Collider other)//dotykanie przez gracza obiektow za pomoca collidera
    {
        if (other.gameObject == foodObject && hunger <= 0)//jesli gracz dotknie obiektu jedzenia
        {
            StartCoroutine(EatFood());//ma zadzialac metoda ponizej zapisana
        }
    }

    private IEnumerator EatFood()//potrzebne do funkcji odczekania
    {
        pickingUp = true;
        animator.SetBool("isWalking", true);//animacja chodzenia wylaczona
        animator.SetBool("pickingUp", false);//animacja modlenia wlaczona

        yield return new WaitForSeconds(5);//gracz odczekuje 5 sekund

        animator.SetBool("pickingUp", true);//animacja modlenia wylaczona
        hunger += 50f;//dodaje sie 50 do jedzenia
        hunger = Mathf.Clamp(hunger, 0f, maxHunger);//sprawdza czy jedzenie jest na 0 chyba?
        HungerImage.fillAmount = hunger;//aktualizacja paska jedzenia
        isHungerDecreasing = hunger < 50f;//pasek po dodaniu 50 nie spada dalej

        pickingUp = false;
    }
}
