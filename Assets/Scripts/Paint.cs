using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paint : MonoBehaviour
{
    [Range(2, 512)]
    [SerializeField] private int _textureSize = 128;
    [SerializeField] private TextureWrapMode _textureWrapMode;
    [SerializeField] private FilterMode _filterMode;
    [SerializeField] private Texture2D _texture;
    [SerializeField] private Material _material;

    [SerializeField] private Camera _camera;
    [SerializeField] private Collider _collider;
    [SerializeField] private Color _color;
    [SerializeField] private int _brushSize = 8;

    bool isChange = false;


    [SerializeField] private int _resolution = 128;

    public Color CurrentColor = Color.black;

    private void OnValidate()
    {
        if (_texture == null)
        {
            _texture = new Texture2D(_textureSize, _textureSize);
        }

        if(_texture.width != _textureSize)
        {
            _texture.Reinitialize(_textureSize, _textureSize);
        }

        _texture.wrapMode = _textureWrapMode;
        _texture.filterMode = _filterMode;
        _material.mainTexture = _texture;
        _texture.Apply();
    }

    private void Update()
    {
        _brushSize += (int)Input.mouseScrollDelta.y;

        if(Input.GetMouseButton(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            FloodFildForSprite();

            if (_collider.Raycast(ray, out hit, 100f))
            {
                int rayX = (int) (hit.textureCoord.x * _textureSize);
                int rayY = (int) (hit.textureCoord.y * _textureSize);

                // DrawQuad(rayX, rayY);
                 DrawCircle(rayX, rayY);
                 flood();         
                _texture.Apply();
            }
        }
    }

   public void DrawQuad(int rayX, int rayY)
    {
        for (int y = 0; y < _brushSize; y++)
        {
            for (int x = 0; x < _brushSize; x++)
            {
                _texture.SetPixel(rayX + x - _brushSize / 2, rayY + y - _brushSize / 2, _color);
            }
        }
    }

   public void DrawCircle(int rayX, int rayY)
    {
        if(isChange == false)
        {
            for (int y = 0; y < _brushSize; y++)
            {
                for (int x = 0; x < _brushSize; x++)
                {
                    float x2 = Mathf.Pow(x - _brushSize / 2, 2);
                    float y2 = Mathf.Pow(y - _brushSize / 2, 2);
                    float r2 = Mathf.Pow(_brushSize / 2 - 0.5f, 2);

                    if (x2 + y2 < r2)
                    {
                        _texture.SetPixel(rayX + x - _brushSize / 2, rayY + y - _brushSize / 2, CurrentColor);
                    }
                }
            }
        }    
    }

    public void SetPencilColor(Image thisColor)
    {
        CurrentColor = thisColor.color;
    }

    public Color GetCurrentColor()
    {
        return CurrentColor;
    }

    void flood()
    {
        if(isChange == true)
        {
            for (int y = 0; y < _resolution; y++)
            {
                for (int x = 0; x < _resolution; x++)
                {
                    _texture.SetPixel(x, y, CurrentColor);

                }
            }
        }     
    }

    public void ChangeTrue()
    {
        isChange = true;
    }

    public void ChangeFalse()
    {
        isChange = false;
    }

    public void FloodFildForSprite()
    {
        if (isChange == true)
        {
            var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, -Vector2.up);

            if (Input.GetButton("Fire1"))
            {
                if (hit.collider != null)
                {
                    SpriteRenderer sp = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                    sp.color = CurrentColor;
                }
            }
        }
    }
}