using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{

    [SerializeField] private TextMeshPro _letter;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color[] _colorBloc = new Color[2];
    private Color OldColr;
    private bool _scale = true;


    public char[] _chars { get; private set; }
   
    private void Start()
    {
        _chars = _letter.text.ToCharArray();
        OldColr = _spriteRenderer.color;
    }

    public string Letter()
    {
        return _letter.text;
    }

    public void inst(char Char)
    {
        _letter.text = Char.ToString();
    }

    public void WillChangeTheColor(int valye)
    {
        _spriteRenderer.color = valye <=1? _colorBloc[valye]: OldColr;
    }
   
    public void Scale(bool Scel)
    {
        _scale = Scel;
    }

    public void Scale()
    {
        if (_scale)
        {
            gameObject.transform.DOScale(0.99f, 0.1f);
            gameObject.transform.DOScale(0.85f, 0.1f).SetDelay(0.05f);
            _scale = false;
        }
        
    }
     

}
