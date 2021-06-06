using System;
using System.Windows.Forms;

namespace DataGridViewHeaderCheckbox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // 特殊なHeaderを保持しているカスタムDataGridView
            DataGridViewRolloverCellColumn customDgvcolumn1 = new DataGridViewRolloverCellColumn(); // ロールオーバーに関するアクションが登録されたセル
            DataGridViewCheckBoxColumn customDgvcolumn2 = new DataGridViewCheckBoxColumn(); // 標準のチェックボックスセル
            DataGridViewDisableButtonColumn customDgvcolumn3 = new DataGridViewDisableButtonColumn();   // 無効になるボタンを保持するセル
            customDgvcolumn1.Name = "RolloverCells";
            customDgvcolumn2.Name = "CheckBoxes";
            customDgvcolumn3.Name = "Buttons";

            customDgv.Columns.Add(customDgvcolumn1);
            customDgv.Columns.Add(customDgvcolumn2);
            customDgv.Columns.Add(customDgvcolumn3);

            customDgv.RowCount = 8;
            customDgv.AutoSize = true;
            customDgv.AllowUserToAddRows = false;
            customDgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int i = 0; i < customDgv.RowCount; i++)
            {
                customDgv.Rows[i].Cells["RolloverCells"].Value = "カーソルを合わせると枠が付くセル " + i.ToString(); ;
                customDgv.Rows[i].Cells["Buttons"].Value = "Button " + i.ToString();
            }

            // 一部のHeaderセルのみチェックボックスを付与
            DataGridViewCheckBoxColumnHeaderCell checkBoxColumnHeaderCell = new DataGridViewCheckBoxColumnHeaderCell();
            checkBoxColumnHeaderCell.OnCheckBoxClicked +=
                delegate (bool checked_status)
                {
                    for (int j = 0; j < customDgv.Rows.Count; j++)
                    {
                        customDgv.Rows[j].Cells["CheckBoxes"].Value = checked_status;
                    }
                };
            customDgv.Columns[1].HeaderCell = checkBoxColumnHeaderCell;


            customDgv.CellValueChanged += new DataGridViewCellEventHandler(customDgv_CellValueChanged);
            customDgv.CurrentCellDirtyStateChanged += new EventHandler(customDgv_CurrentCellDirtyStateChanged);
            customDgv.CellClick += new DataGridViewCellEventHandler(customDgv_CellClick);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add("データ１", true);
            dataGridView1.Rows.Add("データ２", true);
            dataGridView1.Rows.Add("データ３", false);
            dataGridView1.Rows.Add("データ４", true);
            int current_column = 1;
            DataGridViewCheckBoxColumnHeaderCell checkBoxColumnHeaderCell = new DataGridViewCheckBoxColumnHeaderCell();

            checkBoxColumnHeaderCell.OnCheckBoxClicked +=
                delegate (bool checked_status)
                {
                    for (int j = 0; j < dataGridView1.Rows.Count; j++)
                    {
                        dataGridView1.Rows[j].Cells[current_column].Value = checked_status;
                    }
                };
            dataGridView1.Columns[current_column].HeaderCell = checkBoxColumnHeaderCell;
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            dataGridView1.CurrentCellDirtyStateChanged += new EventHandler(dataGridView1_CurrentCellDirtyStateChanged);
            dataGridView1.Columns[current_column].Name = "列２";
        }

        // セルの状態が変更すると、CommitEditメソッドを呼び出すことにより、CellValueChangedイベントを手動で発生させる
        private void customDgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (customDgv.IsCurrentCellDirty)
            {
                customDgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        // チェックボックスがクリックすると同じ行のボタンの有効無効を切り替え、DataGridViewに対して再描画を促す
        private void customDgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (customDgv.Columns[e.ColumnIndex].Name == "CheckBoxes")
            {
                DataGridViewDisableButtonCell buttonCell = (DataGridViewDisableButtonCell)customDgv.Rows[e.RowIndex].Cells["Buttons"];
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)customDgv.Rows[e.RowIndex].Cells["CheckBoxes"];
                buttonCell.Enabled = !(Boolean)checkCell.Value;
                customDgv.Invalidate();
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "列２")
            {
                dataGridView1.Invalidate();
            }
        }

        
        // 有効なボタンをクリックすると、有効であることを通知する
        private void customDgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (customDgv.Columns[e.ColumnIndex].Name == "Buttons")
            {
                if (e.RowIndex  == -1)
                {
                    return;
                }
                DataGridViewDisableButtonCell buttonCell = (DataGridViewDisableButtonCell)customDgv.Rows[e.RowIndex].Cells["Buttons"];
                if (buttonCell.Enabled)
                {
                    MessageBox.Show(customDgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() +" is enabled");
                }
            }
        }
        int dataGridView1PreSelectedRowIndex = 0;
        // dataGridView1のセルがクリックしたら、祖に合わせてdataGridView2の表示内容をリセットし書き換える
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            //メッセージボックスを表示する
            DialogResult result = MessageBox.Show("右のdataGridViewの中身を更新しますか？",
                "質問",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.No)
            {
                //「いいえ」が選択された時
                dataGridView1.CurrentRow.Selected = false;
                dataGridView1.Rows[dataGridView1PreSelectedRowIndex].Selected = true;
                return;
            }

            dataGridView2.Rows.Clear();
            if (e.RowIndex == 0)
            {
                dataGridView2.Rows.Add(e.RowIndex + ":データ1");
                dataGridView2.Rows.Add(e.RowIndex + ":データ2");
                dataGridView2.Rows.Add(e.RowIndex + ":データ3");
            }
            else
            {
                dataGridView2.Rows.Add(e.RowIndex + ":dataA");
                dataGridView2.Rows.Add(e.RowIndex + ":dataV");
            }
            dataGridView1PreSelectedRowIndex = e.RowIndex;
        }
    }
}
