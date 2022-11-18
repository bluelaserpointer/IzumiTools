using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IzumiTools
{
    [DisallowMultipleComponent]
    public class HandCardsArrange : MonoBehaviour
    {
        //inspector
        public bool containChildrenOnAwake = true;
        public bool arrangeOnAwake = true;
        [Header("卡牌叠放顺序(CardOrder)")]
        public bool rightIsUpper = true;
        public bool newerIsUpper = true;
        [Header("卡牌坐标(CardPosition)")]
        [Min(0)]
        public float arrangeRadius = 50f;
        public float arrangeDirection = 90f;
        public float arrangeAngleSpan = 10f;
        [Header("卡牌角度(CardAngle)")]
        public float cardAngleSpan = 5f;
        [Header("卡牌大小(CardScale)")]
        public bool fixCardLocalScale = false;
        public Vector3 cardLocalScale = Vector3.one;
        public float AngleSpanRad => Mathf.Deg2Rad * arrangeAngleSpan;

        //data
        public readonly List<GameObject> cards = new List<GameObject>();
        public void Start()
        {
            if (containChildrenOnAwake)
            {
                foreach (Transform childTf in transform)
                    cards.Add(childTf.gameObject);
            }
            if (arrangeOnAwake)
            {
                Arrange();
            }
        }
        public int CardCount => cards.Count;
        public virtual void Arrange()
        {
            Vector3 myPos = transform.position;
            Vector3 myAngle = transform.eulerAngles;
            float radius = arrangeRadius * transform.lossyScale.magnitude;
            int cardCount = CardCount;
            float enumerateDirection = rightIsUpper ? -1 : 1;
            float angle = -enumerateDirection * (cardCount - 1) * AngleSpanRad / 2 + Mathf.Deg2Rad * arrangeDirection;
            float rotate = -enumerateDirection * (cardCount - 1) * cardAngleSpan / 2;
            foreach (GameObject card in cards)
            {
                card.transform.SetAsFirstSibling();
                card.transform.position = myPos + transform.right * radius * Mathf.Cos(angle) + transform.up * radius * Mathf.Sin(angle);
                card.transform.eulerAngles = myAngle + new Vector3(0, 0, rotate);
                angle += enumerateDirection * AngleSpanRad;
                rotate += enumerateDirection * cardAngleSpan;
            }
        }
        public void Add(GameObject card)
        {
            if (cards.Contains(card))
                return;
            cards.Add(card);
            card.transform.SetParent(transform);
            if (!newerIsUpper)
                card.transform.SetAsFirstSibling();
            if (fixCardLocalScale)
                card.transform.localScale = cardLocalScale;
            Arrange();
        }
        public void Insert(int index, GameObject card)
        {
            if (cards.Contains(card) || index < 0 || index > cards.Count)
                return;
            cards.Insert(index, card);
            card.transform.SetParent(transform);
            if (fixCardLocalScale)
                card.transform.localScale = cardLocalScale;
            Arrange();
        }
        public void Remove(GameObject card)
        {
            if (!cards.Contains(card))
                return;
            cards.Remove(card);
            Arrange();
        }
    }
}
