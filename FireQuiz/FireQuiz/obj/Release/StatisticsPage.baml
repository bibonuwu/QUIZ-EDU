   M S B A M L   `   `   `  ����   FireQuiz    FireQuiz.StatisticsPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       N� Title$+  %Статистика учеников��.����  �� CornerRadius$ 10��   PrimaryBackgroundColor#���   ._���   SecundaryBackgroundColor#���  $ 40,40,40,40��$��50,20,50,50q�._�� $��20q�.+��� .+��� $+��%Статистика учеников��  MontserratBold#��[ $��24(�$��Center=�$��0,0,0,20q���  �� Orientation$ 
Horizontal=�$��Center=�$��0,0,0,10q�.+���   MontserratMedium#��[ $��От:��$��Center=�$��5q�(  "System.Windows.Controls.DatePicker  -     Name$ FromDatePicker��#��[ $	��150��$��5q��� #��[ $��До:��$��Center=�$��5q�  -   $ ToDatePicker��#��[ $	��150��$��5q��� -   $��Показать��  BlueRoundedButton#��[ $��36��$	��100��$��10,0,0,0q��� -   $��← Назад��#��[ $	��100��$��36��$��15,0,0,0q�&   System.Windows.Controls.DataGrid  -     Name$ StatsDataGrid��#��[    AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly . $��0,10,0,0q�   HeadersVisibility$ Column=�	   GridLinesVisibility$	 
Horizontal=�
   	RowHeight$
 30Q�$��14(�
�������$��1q����#CCC   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Ученик��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Ученик+  $ 
Класс��$ *  �� *
Класс+  $ Дата��$ *  �� *
Дата+  $ Тест��$ *  �� *
Тест+  $ 
Баллы��$ *  �� *
Баллы+