   M S B A M L   `   `   `  ����B  >TestApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:TestAppTestApp  5   	       TestApp.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:TestApp     5   	   -   5          �Name$
  home��5       � Title$ 
MainWindow��5	      $	��619��6   $	��870��6"    � WindowStartupLocation$ CenterScreen=�5
       � WindowStyle$
 None=�6'    J� Opacity$ 0Q�5   
   
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5       � WindowState$ Normal=�6   .��5      ��  �� CornerRadius$ 10��6      PrimaryBackgroundColor#���   6   ._�5   
   � .+�5   
   �� $��Right=�6   $	��Top=�61   $��0,5,10,0q�6I   $��30��6[   	 �� Orientation$	 
Horizontal=�6g   $��RightToLeft=�6�   .+�5      �� -   5        
 ��Name$
 btnClose��5        IconButtonsStyle#��[ 6'    	 close#���  6Q   
 � Tag$ IsCloseButton��6�   �� 5      -   5        $
 
btnRestore��5      $	��5 0q�6)   ��5      �� 5       �� 
TargetType" ��6    �� BasedOn# [ 69   .�5      ��  �� Property � ��  maximize �� Value# �  64    �� Triggers 5      �� 5       �� Binding 6*   ��  �� ElementName$
 home�� �� Path$ WindowState� �� Value 	Maximized6a   .a�5   "   ��  � ��  restore# �  6<   5      5       5"      �� 5#      -   5        $
 btnMinimize��5#      #���  6*     minimize#���  6U   6z   5$      � 5&   
   $��40,40,40,40q�6    � ColumnDefinitions 5'      �� 5(      $��* �6#   �� 5)      $��* �6#   .+�5/      ��   SecundaryBackgroundColor#���  6   $ 40,40,40,40��6N   $��0,0,5,0q�6i   ._�50      �� $��20q�6   $��Center=�6)   $��Center=�6F   .+�51      �� $��/logocenter.png��6   $	��200��65   �� 53      $��0,20,0,0q�6      MontserratMedium#��[ 63   $��16(�6b   .��6q   (&Пошта немесе телефон6�   �� 54      -   5         ��Name$ txtLoginEmail��54   "   #��[ 6:   $��16(�6j   �� 55      #��[ 6!   $��16(�6P   .��6_   Құпия сөз6j   G� 57      -   5         G�Name$ txtLoginPassword��57   e   #��[ 6'   $��16(�6V   6e   �� 58      -   5          BlueRoundedButton#��[ 58      $��Кіру��6H   $��0,10,0,0q�6k   $��35��6}   �� 59      #��[ 6!   $��14(�6Q   $��	0,10,0,10q�6_   .��6r   Ұмыттыңыз ба?6�   �� 5;      #��[ 6%   $��16(�6T    �� Padding$ 5,0,5,0q�6b   �� 5<      $*�$Аккаунтыңыз жоқ па? ��6    �� 5=      -   5        .�5=   +   Тіркелу64   5>      5?      5A      �� 5B   	   $��/assets/login.png��6   $��1��6*   $
��Fill=�6:   $��5,0,0,0q�6I   5D      5E      5F      5G      