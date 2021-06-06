using System.Windows.Forms;
using System.Drawing;

namespace DataGridViewHeaderCheckbox
{
    public class DataGridViewRolloverCell : DataGridViewTextBoxCell
    {
        protected override void Paint(
            Graphics graphics,
            Rectangle clipBounds,
            Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates cellState,
            object value,
            object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            // デフォルトのセル外観を描画
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
                value, formattedValue, errorText, cellStyle,
                advancedBorderStyle, paintParts);

            // マウスポインタの位置取得
            Point cursorPosition = this.DataGridView.PointToClient(Cursor.Position);

            // マウスポインタが現在のセルの上にある場合は、セルに赤い境界線を描画
            if (cellBounds.Contains(cursorPosition))
            {
                Rectangle newRect = new Rectangle(cellBounds.X + 1,
                    cellBounds.Y + 1, cellBounds.Width - 4,
                    cellBounds.Height - 4);
                graphics.DrawRectangle(Pens.Red, newRect);
            }
        }

        // マウスポインタがセルに入ると、セルを強制的に再描画
        protected override void OnMouseEnter(int rowIndex)
        {
            this.DataGridView.InvalidateCell(this);
        }

        // マウスポインタがセルから離れたときに、セルを強制的に再描画
        protected override void OnMouseLeave(int rowIndex)
        {
            this.DataGridView.InvalidateCell(this);
        }
    }

}
