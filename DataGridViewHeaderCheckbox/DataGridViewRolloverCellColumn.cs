using System.Windows.Forms;

namespace DataGridViewHeaderCheckbox
{
    public class DataGridViewRolloverCellColumn : DataGridViewColumn
    {
        public DataGridViewRolloverCellColumn()
        {
            this.CellTemplate = new DataGridViewRolloverCell();
        }

    }
}
