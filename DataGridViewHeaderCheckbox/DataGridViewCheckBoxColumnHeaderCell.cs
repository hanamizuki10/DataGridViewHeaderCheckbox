using System;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;    // System.Windows.Forms.VisualStyles.CheckBoxState
using System.Drawing;

namespace DataGridViewHeaderCheckbox
{

    public class DataGridViewCheckBoxColumnHeaderCell : DataGridViewColumnHeaderCell
    {
        private Point _checkBoxLocation;
        private Size _checkBoxSize;
        private Point _cellLocation = new Point();


        public bool Checked { get; set; } = false;
        public delegate void CheckBoxClickedHandler(bool state);
        public event CheckBoxClickedHandler OnCheckBoxClicked;


        protected override void Paint(
            Graphics graphics,
            Rectangle clipBounds,
            Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates dataGridViewElementState,
            object value,
            object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            // デフォルトのセル外観を描画
            base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                dataGridViewElementState,
                "    " + this.OwningColumn.HeaderText,
                "    " + this.OwningColumn.HeaderText,
                errorText,
                cellStyle,
                advancedBorderStyle,
                paintParts);

            // チェックボックスがクリックされた時用に、チェックボックスの位置とサイズを計算して保持して置く
            Point p = new Point();
            Size s = CheckBoxRenderer.GetGlyphSize(graphics, CheckBoxState.UncheckedNormal);
            p.X = cellBounds.Location.X + 5;
            p.Y = cellBounds.Location.Y + (cellBounds.Height / 2) - (s.Height / 2);
            _cellLocation = cellBounds.Location;
            _checkBoxLocation = p;
            _checkBoxSize = s;
            if (this.Checked)
            {
                CheckBoxRenderer.DrawCheckBox(graphics, _checkBoxLocation, CheckBoxState.CheckedNormal);
            }
            else
            {
                CheckBoxRenderer.DrawCheckBox(graphics, _checkBoxLocation, CheckBoxState.UncheckedNormal);
            }
        }

        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            // マウスクリック時の座標がチェックボックスの上に重なっていたらチェックボックスの選択状態を反転させる
            Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);
            if (p.X >= _checkBoxLocation.X && p.X <= _checkBoxLocation.X + _checkBoxSize.Width
            && p.Y >= _checkBoxLocation.Y && p.Y <= _checkBoxLocation.Y + _checkBoxSize.Height)
            {
                this.Checked = !this.Checked;
                if (OnCheckBoxClicked != null)
                {
                    OnCheckBoxClicked(this.Checked);
                    this.DataGridView.InvalidateCell(this);
                }

            }
            base.OnMouseClick(e);
        }
    }
    }
