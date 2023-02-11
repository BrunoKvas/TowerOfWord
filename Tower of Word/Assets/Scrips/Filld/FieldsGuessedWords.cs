using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldsGuessedWords : MonoBehaviour
{
    [SerializeField] private GameObject _circle;
    private List<string> _secretsWords;
    void Start()
    {
        _secretsWords = PuzzleGenerator.Instance.SecretsWords;
        CreateFilld();
    }

    void CreateFilld()
    {

        float NewLineIndentation = 0;
        int controlPos = 0;

        for (int x = 0; x < _secretsWords.Count; x++)
        {
            float DistanceBetweenImage = 0;
            float posY = 3.8f;
            
            for (int y = 0; y < _secretsWords[x].Length; y++)
            {

                int posX = controlPos <= 1 ? -11 : 7;
                var image = Instantiate(_circle, new Vector2(DistanceBetweenImage + posX + NewLineIndentation, posY), Quaternion.identity);
                image.transform.parent = gameObject.transform;
                AnimationFoundWord.Instance.CompletionPositonCircle(image, x, y);
                StartLevelFilldMove(image, new Vector2(DistanceBetweenImage - 2.2f + NewLineIndentation, posY));
                DistanceBetweenImage += 0.5f;
            }

            NewLineIndentation += _secretsWords[x].Length - 1.5f;
            controlPos++;
        }

    }


    void StartLevelFilldMove(GameObject image, Vector2 pos)
    {
        image.transform.DOMove(pos, 0.6f).SetDelay(0.4f);

    }
}
