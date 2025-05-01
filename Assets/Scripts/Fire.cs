using UnityEngine;

public class Fire : MonoBehaviour
{
    public int indiceDetector = 0; //identifica detector
    [SerializeField] Bomb _bombScript;


    private void Start()
    {
        _bombScript = GetComponentInParent<Bomb>();
    }
    //Detecta Celdas
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Cell") && _bombScript.detectorBlocked[indiceDetector] == false 
            && collision.gameObject.GetComponent<CellsColor>() != null)
        {
            print("celda: " + indiceDetector);


            if (collision.gameObject.GetComponent<CellsColor>().cellType == 0)
            {
                _bombScript.detectorBlocked[indiceDetector] = true;

                Destroy(this.gameObject);

            }
            //celda de tipo 2 son las celdas de ladrillo que se pueden romper
            else if (collision.gameObject.GetComponent<CellsColor>().cellType == 1)
            {
                _bombScript.detectorBlocked[indiceDetector] = true;

                //permuta la celda a neutra
                collision.gameObject.GetComponent<CellsColor>().DestroyRedCell();

                Destroy(this.gameObject);

            }



        }
    }
}
