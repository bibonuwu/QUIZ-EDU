   M S B A M L   `   `   `  ����  FireQuiz    FireQuiz.StatisticsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$+  %Статистика учеников��5      .��5      ��  �� CornerRadius$ 10��6      PrimaryBackgroundColor#���   6   ._�5   
   ��   SecundaryBackgroundColor#���  6   $ 40,40,40,40��6J   $��50,20,50,50q�6e   ._�5
      � $��20q�6   .+�5   
   �� .+�5      �� $+��%Статистика учеников��6     MontserratBold#��[ 5      $��24(�5      $��Center=�5      $��0,0,0,20q�5      �� 5       �� Orientation$ 
Horizontal=�6   $��Center=�62   $��0,0,0,10q�6O   .+�5      ��   MontserratMedium#��[ 6!   $��От:��6P   $��Center=�6[   $��5q�6v   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ FromDatePicker��5   Q   #��[ 6"   $	��150��6i   $��5q�6u   �� 5      #��[ 6!   $��До:��6P   $��Center=�6[   $��5q�6v     5      -   5        $ ToDatePicker��5   Q   #��[ 6"   $	��150��6g   $��5q�6s   �� 5      -   5        $��Показать��5        BlueRoundedButton#��[ 5      $��36��6D   $	��100��5      $��10,0,0,0q�5       �� 5#      -   5        $��← Назад��5#   !   #��[ 5$   	   $	��100��5%   	   $��36��5&   	   $��15,0,0,0q�5'   "   5(      &   System.Windows.Controls.DataGrid  5+      -   5          Name$ StatsDataGrid��5+   K   #��[ 6      AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly . $��0,10,0,0q�5/         HeadersVisibility$ Column=�50      	   GridLinesVisibility$	 
Horizontal=�51      
   	RowHeight$
 30Q�52      $��14(�53      
�������$��1q�55      ���#CCC   Columns 58      0  *System.Windows.Controls.DataGridTextColumn  59      ,  &System.Windows.Controls.DataGridColumn   Header$ Ученик��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6T   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *Ученик+6T     5:      $ 
Класс��6)   $ * 6R    68   �� *
Класс+6R     5;      $ Дата��6)   $ * 6P    67   �� *
Дата+6P     5<      $ Тест��6)   $ * 6P    67   �� *
Тест+6P     5=      $ 
Баллы��6)   $ * 6R    68   �� *
Баллы+6R   5@      5A      5B      5C      5D      5E      