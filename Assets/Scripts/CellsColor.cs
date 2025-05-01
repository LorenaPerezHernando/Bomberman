using UnityEngine;

public class CellsColor : MonoBehaviour
{
    public int cellType = 0;
    private Renderer _renderer;
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        if (_renderer.material.color == Color.green && _renderer != null )
            GetComponent<BoxCollider>().enabled = false;

        if (_renderer.material.color == Color.red && _renderer != null)
            cellType = 1;
        else cellType = 0; 
    }

    public void DestroyRedCell()
    {
        if(_renderer.material.color == Color.red && _renderer != null)
        {
            _renderer.material.color = Color.green;
            GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
