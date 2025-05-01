using System.Collections;
using UnityEngine;
namespace Bomberman.Player
{


    public class PlayerMove : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private Transform _initialPos;
        [SerializeField] private float _speed;
        private Animator _anim;

        [Header("Bomb")]
        [SerializeField] private int _bombWave = 1;
        [SerializeField] private GameObject _prefabBomb;
        [SerializeField] private float _shootTime = 2;
        [SerializeField] private bool _shootIsActive = true; 
        private bool dying = false; 

        private void Awake()
        {
            _anim = GetComponentInChildren<Animator>();
        }
        void Start()
        {
            _initialPos = gameObject.transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _shootIsActive)
            {
                print("Bomba creada");
                Instantiate(_prefabBomb, transform.position, transform.rotation);
                StartCoroutine(DelayedShooting());
                gameObject.GetComponent<AudioSource>().Play();

            }
            #region Movement
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(transform.right * _speed * Time.deltaTime);
                _anim.SetInteger("Action", 3);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
               
                transform.Translate(transform.right * -_speed * Time.deltaTime);
                _anim.SetInteger("Action", 3);
                

            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(transform.forward * _speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(transform.forward * -_speed * Time.deltaTime);
                _anim.SetInteger("Action", 1);


            }
            #endregion
            #region Sprites
            //Sprites
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;
                _anim.SetInteger("Action", 3);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
                _anim.SetInteger("Action", 3);

            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                _anim.SetInteger("Action", 2);
            }
            #endregion

            #region Stop Movement
            //StopMovement
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                if (_anim.GetInteger("Action") == 3)
                {
                    _anim.SetInteger("Action", 0);
                }
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                if (_anim.GetInteger("Action") == 3)
                {
                    _anim.SetInteger("Action", 0);
                }
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                if (_anim.GetInteger("Action") == 2)
                {
                    _anim.SetInteger("Action", 0);
                }
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                if (_anim.GetInteger("Action") == 1)
                {
                    _anim.SetInteger("Action", 0);
                }
            }
            #endregion
        }

     
        IEnumerator DelayedShooting()
        {

            _shootIsActive = false;
            yield return new WaitForSeconds(2f);
            _shootIsActive = true;
        }
    }
}
