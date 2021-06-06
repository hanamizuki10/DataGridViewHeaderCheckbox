# DataGridViewHeaderCheckbox
DataGridViewのHeaderをカスタマイズするのに色々と試したアプリ
※諸々の都合上.NET 4.5.2にて実装した版

## 以下のコードサンプルの詰め合わせ
全て `Form1.cs` 上にてお試し実装

### ロールオーバー（カーソルを合わせる）と、赤枠が付くセル
関連する追加class：`DataGridViewRolloverCell`,  `DataGridViewRolloverCellColumn`
参考：https://docs.microsoft.com/ja-jp/dotnet/desktop/winforms/controls/customize-cells-and-columns-in-the-datagrid-by-extending-behavior?view=netframeworkdesktop-4.8
![Animation1](https://user-images.githubusercontent.com/41602570/120915771-9e8dd000-c6e0-11eb-9f63-48d6ac51d640.gif)


### チェックボックスがついたHeaderセル、クリックする事で連動して表示
関連する追加class：`DataGridViewCheckBoxColumnHeaderCell`

### セルのチェックボックスクリックと連携して無効になるボタンのセル
関連する追加class：`DataGridViewDisableButtonCell`,  `DataGridViewDisableButtonColumn`

### 二つのDataGridView連動
