﻿#if WDT_USE_TMPRO
// define WDT_USE_TMPRO if use text mesh pro for datatable elemenet
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace WDT
{
    public class WTMPText : WElement
    {
        private TextMeshProUGUI m_text;
        private RectTransform m_rectTransform;

        protected override void InitElement()
        {
            base.InitElement();
            m_text = GetComponentInChildren<TextMeshProUGUI>();
            m_rectTransform = GetComponent<RectTransform>();
            Assert.IsNotNull(m_text);
            Assert.IsNotNull(m_rectTransform);
        }

        public override void SetInfo(object info, int rowIndex, int columnIndex, WDataTable dataTable)
        {
            base.SetInfo(info, rowIndex, columnIndex, dataTable);
            m_text.text = info.ToString();
        }

        public override void SetSize(int width, int height)
        {
            base.SetSize(width, height);
            m_rectTransform.sizeDelta = new Vector2(width, height);
        }
    }
}
#endif