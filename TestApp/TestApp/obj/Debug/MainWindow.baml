   M S B A M L   `   `   `  ����B  >TestApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:TestAppTestApp  5   	       TestApp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:TestApp     5   	   -   5          � WindowState$  	Maximized=�5   
    J� Opacity$ 0Q�5	       � WindowStartupLocation$ CenterScreen=�5       � WindowStyle$
 None=�5   
   
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5       � Title$ Выбор тестов��5   	   .��5      ��  �� CornerRadius$ 10��6      PrimaryBackgroundColor#���   6   ._�5   
   � .+�5      �� $��Right=�6   $	��Top=�65   $��0,5,10,0q�6M   $��30��6_    �� Orientation$ 
Horizontal=�6k   $��RightToLeft=�6�   .+�5      �� -   5        	 ��Name$	 btnClose��5        IconButtonsStyle#��[ 6+    	 close#���  6U   

 � Tag$
 IsCloseButton��6�   �� 5      -   5        $	 
btnRestore��5      $	��5 0q�6-   ��5      �� 5       �� 
TargetType" ��6     �� BasedOn# [ 6=   .�5      ��  �� Property � ��  maximize �� Value# �  68    �� Triggers 5      �� 5   "    �� Binding 6.   ��  �� ElementName$
 home�� �� Path$ WindowState� �� Value 	Maximized6e   .a�5   &   ��  � ��  restore# �  6@   5   #   5      5!      �� 5"      -   5        $	 btnMinimize��5"      #���  6.     minimize#���  6Y   6~   5#      �� 5$      $ 40,40,40,40��6     SecundaryBackgroundColor#���  61   $��50,50,50,50q�6i   ._�5&      � .+�5'      �� $
��Left=�6!   $	��Top=�6<   $��	30,30,0,0q�6T   .+�5(      �� $ 
Horizontal=�6%   .+�5)      A� -   5         A�Name$ InternetEllipse��5)   &   $��15��6?   $��15��6J   
�������$��0,0,5,0q�6b   �� 5*        MontserratRegular#��[ 6$   $��Интернет��6T   5+      �� 5-      $ 
Horizontal=�6%   .+�5.      A� -   5        $ FirebaseEllipse��5.   &   $��15��6?   $��15��6J   
�������$��0,0,5,0q�6b   �� 5/      #��[ 6$   $��Деректер қоры��6U   50      �� 51      -   5         ��Name$ CheckTimeText��51   U   #��[ 6%   
�������6l   53      �� 57      $��Center=�6!   $��Center=�6>   .+�5;      �� $��Center=�6%   $��0,0,0,20q�6B   $��Center=�6T   .+�5<      �� $ 
Horizontal=�6)   $��Center=�6B   $��Center=�6_   $��0,0,0,20q�6z   .+�5=   "   ��   MontserratMedium#��[ 6,   $��20(�6[   $��0,0,5,0q�6i   Қош келдің6�   �� 5>   "   -   5        $ lblFullName��5>   [   #��[ 6,   $��20(�6p   $��0,0,0,5q�6~   5@      �� 5C      $ 
Horizontal=�6)   $��Center=�6B   $��Center=�6_   .+�5E   "   �� -	   5        $��MainSubjectsList��5E   ^   #��[ 6/   $��16(�6v   �� 5F   "   $��16(�6-   #��[ 6<   $��	10,2,10,2q�6l   $��5��6    � LayoutTransform 5G   &   �� 5H   *    �� Angle$ 90Q�6:   5J   #   �� 5K   "   -
   5        $��AdditionalSubjectsList��5K   m   $��16(�6/   #��[ 6>   6m   5M      5P      �� 5Q      $ 
Horizontal=�6%   $��Center=�6>   $��Center=�6[   $��0,20,0,0q�6v   .+�5R      �� -   5        $+��%Начать тестирование��5R   %    	 BlueRoundedButton#��[	 6[   $	��200��6�   $��35��6�   �� 5S      -   5        $!��Моя статистика��5S   %   #��[	 6V   $	��200��6�   $��35��6�   $��10,0,0,0q�6�   �� 5T      -   5        $��Шығу��5T   %   #��[	 6G   $	��200��6r   $��35��6~   $��10,0,0,0q�6�   5V      5W      5X      5\      5]      5_      5`      