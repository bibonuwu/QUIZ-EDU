   M S B A M L   `   `   `  ����B  >TestApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:TestAppTestApp  5   	       TestApp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:TestApp     5   	   -   5          � Title$  Выбор тестов��5   	    � WindowState$ 	Maximized=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � WindowStyle$
 None=�5   	   
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5   	    J� Opacity$ 1Q�5   	   .��5      ��  �� CornerRadius$ 10��6      PrimaryBackgroundColor#���   6   ._�5   
   � .+�5      �� $��Right=�6   $	��Top=�65   $��0,5,10,0q�6M   $��30��6_    �� Orientation$ 
Horizontal=�6k   $��RightToLeft=�6�   .+�5      �� -   5        	 ��Name$	 btnClose��5        IconButtonsStyle#��[ 6+    	 close#���  6U   

 � Tag$
 IsCloseButton��6�   �� 5      -   5        $	 
btnRestore��5      $	��5,0q�6-   ��5      �� 5       �� 
TargetType" ��6     �� BasedOn# [ 64   .�5      ��  �� Property � ��  maximize �� Value# �  68    �� Triggers 5      �� 5   "    �� Binding 6.   ��  �� ElementName$
 home�� �� Path$ WindowState� �� Value 	Maximized6e   .a�5   &   ��  � ��  restore# �  6@   5    #   5"      5$      �� 5%      -   5        $	 btnMinimize��5%      #��[ 6.     minimize#���  6X   6}   5&      �� 5)      $ 40��6     SecundaryBackgroundColor#���  6'   $��50q�6_   ._�5*      � .+�5,      �� $
��Left=�6!   $	��Top=�6<   $��	30,30,0,0q�6T   .+�5-      �� -   5         ��Name$ CheckTimeText��5-   $     MontserratRegular#��[ 6;   
�������$��20(�6}   �� 5.      $ 
Horizontal=�6%   .+�5/      A� -   5         A�Name$ InternetEllipse��5/   &   $��15��6?   $��15��6J   
�������$��0,0,5,0q�6b   �� 50      $��Интернет��6$   #��[ 67   51      �� 52      $ 
Horizontal=�6%   .+�53      A� -   5        $ FirebaseEllipse��53   &   $��15��6?   $��15��6J   
�������$��0,0,5,0q�6b   �� 54      $��Деректер қоры��6$   #��[ 6<   55      56      �� 59      $��Center=�6!   $��Center=�6>   .+�5:      �� $��Center=�6%   $��0,0,0,20q�6B   .+�5;      �� $ 
Horizontal=�6)   $��Center=�6B   $��0,0,0,20q�6_   .+�5<   "   �� $��Қош келдің��6,     MontserratMedium#��[ 6>   $��20(�6m   $��0,0,5,0q�6{   �� 5=   "   -   5        $ lblFullName��5=   ,   #��[ 6A   $��20(�6p   5>      �� 5@      $ 
Horizontal=�6)   $��Center=�6B   .+�5A   "   �� -	   5        $��MainSubjectsList��5A   /   #��[ 6G   $��16(�6v   �� 5B   "   $
��10,2q�6,   $��5��6:    � LayoutTransform 5C   &   �� 5D   *    �� Angle$ 90Q�6:   5F   #   �� 5G   "   -
   5        $��AdditionalSubjectsList��5G   /   #��[ 6M   $��16(�6|   5H      5I      �� 5L      $ 
Horizontal=�6%   $��Center=�6>   $��0,20,0,0q�6[   .+�5M      �� -   5        $��Тест Бастау��5M   %    	 BlueRoundedButton#��[	 6S   $	��200��6~   $��35��6�   �� 5N      -   5        $'��!Менің статистикам��5N   %   #��[	 6Y   $	��200��6�   $��35��6�   $��10,0,0,0q�6�   �� 5O      -   5        $��Шығу��5O   %   #��[	 6G   $	��200��6r   $��35��6~   $��10,0,0,0q�6�   5P      5Q      5R      5S      5U      5V      5W      