using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WDT
{
    public class WRow : WContainter
    {
        private RectTransform m_rectTransform;
        private LayoutElement m_layoutElement;
        private Button m_button;

        public void changeData(IDictionary<string, object> data)
        {
            
        }

        public IDictionary<string, object> Getdata()
        {
            return null;
        }

        protected override string GetElemType(int i)
        {
            return bindDataTable.GetColumnType(i);
        }

        protected override void InitContainter()
        {
            base.InitContainter();
            m_rectTransform = GetComponent<RectTransform>();
            m_layoutElement = GetComponent<LayoutElement>();
            m_button = GetComponent<Button>();
            Assert.IsNotNull(m_rectTransform);
            Assert.IsNotNull(m_button);
            Assert.IsNotNull(m_layoutElement);

            bindDataTable.listRow.Add(this);
        }

        private void ScrollCellContent(object info)
        {
            WDataTable.RowElementInfo rei = (WDataTable.RowElementInfo) info;
            bindDataTable = rei.bindDataTable;
            IList<object> infos = bindDataTable.GetInfosByRowIndex(rei.rowIndex);

            if (!init)
                InitContainter();
            
            if (columnSize != infos.Count)
            {
                columnSize = infos.Count;
                BuildChild();
            }

            m_rectTransform.sizeDelta = new Vector2(bindDataTable.tableWidth, bindDataTable.itemHeight);
            m_layoutElement.preferredHeight = bindDataTable.itemHeight;

            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].SetSize(bindDataTable.GetWidthByColumnIndex(i), bindDataTable.itemHeight);
                elements[i].SetInfo(infos[i], rei.rowIndex, i, bindDataTable);
            }

            m_button.onClick.RemoveAllListeners();
            m_button.onClick.AddListener(() =>
            {
                bindDataTable.OnClickRow(rei.rowIndex);
                if (!bindDataTable.isUseSelect)
                    EventSystem.current.SetSelectedGameObject(null);
            });
        }
    }
}