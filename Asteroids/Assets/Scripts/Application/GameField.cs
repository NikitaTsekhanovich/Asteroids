using UnityEngine;

namespace Application
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _boxCollider2D;
        [SerializeField] private RectTransform _rectBackground;
        [SerializeField] private Canvas _gameCanvas;
        
        private const float BoundRange = -0.3f;

        private float _boundX;
        private float _boundY;

        private void Start()
        {
            InitSizeField();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var objectTransform = collision.transform;

            if (Mathf.Abs(objectTransform.position.y) - _boundY >= BoundRange &&
                Mathf.Abs(objectTransform.position.x) - _boundX >= BoundRange)
            {
                objectTransform.position *= -1;
            }
            else if (Mathf.Abs(objectTransform.position.y) >= _boundY)
            {
                objectTransform.position = 
                    new Vector3(objectTransform.position.x, -objectTransform.position.y, 0f);
            }
            else
            {
                objectTransform.position = 
                    new Vector3(-objectTransform.position.x, objectTransform.position.y, 0f);
            }
        }

        private void InitSizeField()
        {
            _boxCollider2D.offset = Vector2.zero;
            
            _boxCollider2D.size = new Vector2(
                _rectBackground.rect.size.x * _gameCanvas.transform.localScale.x,
                _rectBackground.rect.size.y * _gameCanvas.transform.localScale.y);
            
            _boundX = _boxCollider2D.size.x / 2;
            _boundY = _boxCollider2D.size.y / 2;
        }
    }
}
