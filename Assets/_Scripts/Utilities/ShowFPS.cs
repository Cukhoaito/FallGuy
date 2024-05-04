namespace FallGuy
{
    using UnityEngine;

    public class MbShowFPS : MonoBehaviour
    {
        private float _fps;
        private float _ms;

        private GUIStyle _fontStyle;
        private Rect _rect;
        private int _count = 0;
        private float _deltaTime = 0f;

        private Font _font;

        private void Start()
        {
            _font = Resources.Load<Font>("Fonts/LilitaOne-Regular");
            _fontStyle = new GUIStyle
            {
                fontSize = 16,
                fontStyle = FontStyle.Normal,
                alignment = TextAnchor.UpperRight,
                font = _font,
                normal =
                {
                    textColor = Color.white
                }
            };
        }

        private void Update()
        {
            _rect.Set(Screen.width - 250, 10, 200, 40);
            _count++;
            _deltaTime += Time.deltaTime;
            if (!(_deltaTime >= 1f)) return;
            _fps = _count / _deltaTime;
            _ms = _deltaTime * 1000 / _count;
            _count = 0;
            _deltaTime = 0f;
        }

        private void OnGUI()
        {
            var info = $"{_fps:0.0}fps，{_ms:0.0}ms";
            GUI.Label(_rect, info, _fontStyle);
        }
    }
}