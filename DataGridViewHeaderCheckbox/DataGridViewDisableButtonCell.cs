using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace DataGridViewHeaderCheckbox
{
    public class DataGridViewDisableButtonCell : DataGridViewButtonCell
    {
        public bool Enabled { get; set; } = true;

        // Cloneコール時、Enabledプロパティがコピーされるよう対応
        public override object Clone()
        {
            DataGridViewDisableButtonCell cell = (DataGridViewDisableButtonCell)base.Clone();
            cell.Enabled = this.Enabled;
            return cell;
        }

        protected override void Paint(
            Graphics graphics,
            Rectangle clipBounds, 
            Rectangle cellBounds, 
            int rowIndex,
            DataGridViewElementStates elementState, 
            object value,
            object formattedValue, 
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            if (this.Enabled)
            {
                // Enabledプロパティが有効であるため、デフォルトの Buttonセル外観を描画
                base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                    elementState, value, formattedValue, errorText,
                    cellStyle, advancedBorderStyle, paintParts);

                return;
            }
            // Enabledプロパティが無効であるため、無効なButtonセル外観を描画

            // Backgroundが指定されている場合、セル背景を描画
            // Draw the cell background, if specified.
            if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
            {
                SolidBrush cellBackground = new SolidBrush(cellStyle.BackColor);
                graphics.FillRectangle(cellBackground, cellBounds);
                cellBackground.Dispose();
            }

            // Borderが指定されている場合、セル境界線を描画
            if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
            {
                PaintBorder(graphics, clipBounds, cellBounds, cellStyle,advancedBorderStyle);
            }

            // ボタンを描画する領域を計算
            Rectangle buttonArea = cellBounds;
            Rectangle buttonAdjustment = this.BorderWidths(advancedBorderStyle);
            buttonArea.X += buttonAdjustment.X;
            buttonArea.Y += buttonAdjustment.Y;
            buttonArea.Height -= buttonAdjustment.Height;
            buttonArea.Width -= buttonAdjustment.Width;

            // 無効なボタン描画
            ButtonRenderer.DrawButton(graphics, buttonArea, PushButtonState.Disabled);

            // 無効なボタンにテキストを描画
            if (this.FormattedValue is String)
            {
                TextRenderer.DrawText(graphics, (string)this.FormattedValue, this.DataGridView.Font, buttonArea, SystemColors.GrayText);
            }

        }

    }
}
