   M S B A M L   `   `   `  ����  TestApp    TestApp.Periodic   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Periodic��5   	   $	��600��6   $	��900��6'    � WindowStartupLocation$ CenterScreen=�63    � WindowStyle$
 None=�6X   
������  � AllowsTransparency .  J� Opacity$ 1Q�5       � 
ResizeMode$ NoResize=�5       � WindowState$ Normal=�6   .��5      ��  �� CornerRadius$ 10��6      PrimaryBackgroundColor#���   6   ._�5   
   � .+�5      (� -   5        $��viewbox��5       (� Stretch$ Uniform=�6)   $��40,40,40,40q�6;   .��5      �� -   5        $��	zoomImage��5      $��/Periodic.jpg��5      	 J� RenderTransformOrigin$	 0.5,0.5&�5      z�5      �� 5      -   5        
imageScale��
 �� ScaleX$
 1Q�5   A    �� ScaleY$ 1Q�6L   5      5      �� 5      $��Right=�6   $	��Top=�65   $��0,5,10,0q�6M   $��30��6_    �� Orientation$ 
Horizontal=�6k   $��RightToLeft=�6�   .+�5      �� -   5         ��Name$ btnClose��5        IconButtonsStyle#��[ 6+    	 close#���  6U   
 � Tag$ IsCloseButton��6�   �� 5       -   5        $ 
btnRestore��5       $	��5 0q�6-   ��5!      �� 5"       �� 
TargetType" ��6     �� BasedOn# [ 6=   .�5#      ��  �� Property � ��  maximize �� Value# �  68    �� Triggers 5$      �� 5%   "    �� Binding 6.   ��  �� ElementName$
 home�� �� Path$ WindowState� �� Value 	Maximized6e   .a�5&   &   ��  � ��  restore# �  6@   5'   #   5)      5+      �� 5,      -   5        $ btnMinimize��5,      #���  6.     minimize#���  6Y   6~   5-      �� 5/        MontserratMedium#��[ 6   $
��Left=�6C   $	��Top=�6^   $��	10,10,0,0q�6v   .��6�   MKХимиялық элементтердің периодтық жүйесі51      �� 53      $ 
Horizontal=�6   $��Right=�54      $��Bottom=�55      $��10q�56      .+�57      �� -   5        $	��150��57      $��30��6%     BlueRoundedButton#��[ 6K   $��0,0,5,0q�6v   .��6�   !Суретті ауыстыру59      �� 5:      -	   5        $��30��5:      #��[ 69   $��22(�6d   .��6r   +6u   �� 5;      -
   5        $��30��5;      $��5,0,0,0q�6$   #��[ 6K   $��22(�6v   $��30��6�   .��6�   −6�   5<      5=      5>      5@      