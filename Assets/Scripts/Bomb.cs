using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Bomberman.Player;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private GameObject[] detectors;
    public bool[] detectorBlocked = new bool[] { false, false, false, false };

    private int alcance = 3;
    [SerializeField] private GameObject _prefabFire;
    private bool _detonate = false;//usado para detonar en caso de haber fuego
    float _timeToDetonate = 0.2f;
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        gameObject.GetComponent<BoxCollider>().enabled = false;

        gameObject.transform.localScale = Vector3.one;

        StartCoroutine(detona());

    }

    IEnumerator detona()
    {

        ////parpadea tamaño bomba
        //for (int k = 0; k < 10 && _detonate == false; k++)
        //{
        //    gameObject.transform.localScale = Vector3.one * 0.9f;
        //    yield return new WaitForSeconds(_timeToDetonate / 2);
        //    gameObject.transform.localScale = Vector3.one;
        //    yield return new WaitForSeconds(_timeToDetonate / 2);
        //}


        #region Explodes
        //pone un fuego en el lugar de la bomba
        GameObject fuego = Instantiate(_prefabFire, transform.position, transform.rotation);


        //cada cuadro de alcance es analizado

        gameObject.GetComponent<AudioSource>().Play();

        //esta por el alcance
        alcance = _player.GetComponent<PlayerMove>()._bombWave;
        for (int j = 0; j < alcance; j++)
        {
            //recorre los detectores
            for (int i = 0; i < detectors.Length; i++)
            {
                detectors[i].SetActive(!detectorBlocked[i]);


                if (!detectorBlocked[i])
                {
                    fuego = Instantiate(_prefabFire, detectors[i].transform.position, transform.rotation);


                    //IMPORTANT == Este yield es el UNICO responsable de q le de TIEMPO A DETECTARLOS TODOS LOS CUADRADOS
                    yield return new WaitForSeconds(.01f);
                }

                yield return null;
            }//detector no bloqueado end if
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    #endregion
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Cell"))
        {
            print("Colision con celda");

        }
        if (collision.gameObject.CompareTag("Fire"))
        {
            print("Colision con celda");
            _detonate = true;
            _timeToDetonate = 0;
            //StartCoroutine(detona());
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;

        }
    }
}
