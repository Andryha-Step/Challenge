using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CnControls
{
    [Flags]
    public enum ControlMovementDirection
    {
        Horizontal = 0x1,
        Vertical = 0x2,
        Both = Horizontal | Vertical
    }

    public class SimpleJoystick : MonoBehaviour

#if !UNITY_EDITOR
        , IDragHandler, IPointerUpHandler, IPointerDownHandler
#endif
    {

        public Camera CurrentEventCamera { get; set; }

        public float MovementRange = 50f;

        public string HorizontalAxisName = "Horizontal";

        public string VerticalAxisName = "Vertical";

        [Space(15f)]
        [Tooltip("Should the joystick be hidden on release?")]
        public bool HideOnRelease;

        [Tooltip("Should the Base image move along with the finger without any constraints?")]
        public bool MoveBase = true;

        [Tooltip("Should the joystick snap to finger? If it's FALSE, the MoveBase checkbox logic will be ommited")]
        public bool SnapsToFinger = true;

        [Tooltip("Constraints on the joystick movement axis")]
        public ControlMovementDirection JoystickMoveAxis = ControlMovementDirection.Both;

        [Tooltip("Image of the joystick base")]
        public Image JoystickBase;

        [Tooltip("Image of the stick itself")]
        public Image Stick;

        [Tooltip("Touch Zone transform")]
        public RectTransform TouchZone;

        private Vector2 _initialStickPosition;
        private Vector2 _intermediateStickPosition;
        private Vector2 _initialBasePosition;
        private RectTransform _baseTransform;
        private RectTransform _stickTransform;

        private float _oneOverMovementRange;

        protected VirtualAxis HorizintalAxis;
        protected VirtualAxis VerticalAxis;

        private void Awake()
        {
            _stickTransform = Stick.GetComponent<RectTransform>();
            _baseTransform = JoystickBase.GetComponent<RectTransform>();

            _initialStickPosition = _stickTransform.anchoredPosition;
            _intermediateStickPosition = _initialStickPosition;
            _initialBasePosition = _baseTransform.anchoredPosition;

            _stickTransform.anchoredPosition = _initialStickPosition;
            _baseTransform.anchoredPosition = _initialBasePosition;

            _oneOverMovementRange = 1f / MovementRange;

            if (HideOnRelease)
            {
                Hide(true);
            }
#if UNITY_EDITOR
            gameObject.AddComponent<JoystickInputHelper>();
#endif
        }

        private void OnEnable()
        {

            HorizintalAxis = HorizintalAxis ?? new VirtualAxis(HorizontalAxisName);
            VerticalAxis = VerticalAxis ?? new VirtualAxis(VerticalAxisName);

            CnInputManager.RegisterVirtualAxis(HorizintalAxis);
            CnInputManager.RegisterVirtualAxis(VerticalAxis);
        }

        private void OnDisable()
        {
            CnInputManager.UnregisterVirtualAxis(HorizintalAxis);
            CnInputManager.UnregisterVirtualAxis(VerticalAxis);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            
            CurrentEventCamera = eventData.pressEventCamera ?? CurrentEventCamera;

            Vector3 worldJoystickPosition;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(_stickTransform, eventData.position,
                CurrentEventCamera, out worldJoystickPosition);

            _stickTransform.position = worldJoystickPosition;
            var stickAnchoredPosition = _stickTransform.anchoredPosition;

            if ((JoystickMoveAxis & ControlMovementDirection.Horizontal) == 0)
            {
                stickAnchoredPosition.x = _intermediateStickPosition.x;
            }
            if ((JoystickMoveAxis & ControlMovementDirection.Vertical) == 0)
            {
                stickAnchoredPosition.y = _intermediateStickPosition.y;
            }

            if (stickAnchoredPosition.x < -40)
            {
                Snake.left_w = true;
            }
            if (stickAnchoredPosition.y < -30)
            {
                Snake.down_w = true;
            }
            if (stickAnchoredPosition.x > 40)
            {
                Snake.right_w = true;
            }
            if (stickAnchoredPosition.y > 30)
            {
                Snake.up_w = true;
            }


            _stickTransform.anchoredPosition = stickAnchoredPosition;

            Vector2 difference = new Vector2(stickAnchoredPosition.x, stickAnchoredPosition.y) - _intermediateStickPosition;

            var diffMagnitude = difference.magnitude;
            var normalizedDifference = difference / diffMagnitude;

            if (diffMagnitude > MovementRange)
            {
                if (MoveBase && SnapsToFinger)
                {
                    var baseMovementDifference = difference.magnitude - MovementRange;
                    var addition = normalizedDifference * baseMovementDifference;
                    _baseTransform.anchoredPosition += addition;
                    _intermediateStickPosition += addition;
                }
                else
                {
                    _stickTransform.anchoredPosition = _intermediateStickPosition + normalizedDifference * MovementRange;
                }
            }

            var horizontalValue = Mathf.Clamp(difference.x * _oneOverMovementRange, -1f, 1f);
            var verticalValue = Mathf.Clamp(difference.y * _oneOverMovementRange, -1f, 1f);

            HorizintalAxis.Value = horizontalValue;
            VerticalAxis.Value = verticalValue;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _baseTransform.anchoredPosition = _initialBasePosition;
            _stickTransform.anchoredPosition = _initialStickPosition;
            _intermediateStickPosition = _initialStickPosition;

            HorizintalAxis.Value = VerticalAxis.Value = 0f;

            if (HideOnRelease)
            {
                Hide(true);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (SnapsToFinger)
            {
                CurrentEventCamera = eventData.pressEventCamera ?? CurrentEventCamera;

                Vector3 localStickPosition;
                Vector3 localBasePosition;
                RectTransformUtility.ScreenPointToWorldPointInRectangle(_stickTransform, eventData.position,
                    CurrentEventCamera, out localStickPosition);
                RectTransformUtility.ScreenPointToWorldPointInRectangle(_baseTransform, eventData.position,
                    CurrentEventCamera, out localBasePosition);

                _baseTransform.position = localBasePosition;
                _stickTransform.position = localStickPosition;
                _intermediateStickPosition = _stickTransform.anchoredPosition;
            }
            else
            {
                OnDrag(eventData);
            }
            if (HideOnRelease)
            {
                Hide(false);
            }
        }


        private void Hide(bool isHidden)
        {
            JoystickBase.gameObject.SetActive(!isHidden);
            Stick.gameObject.SetActive(!isHidden);
        }
    }
}
