   M S B A M L   `   `   `  ����  FireQuiz    FireQuiz.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  FireQuiz��5   	   $	��500��6   $	��800��6'    � WindowStartupLocation$ CenterScreen=�5       � WindowStyle$
 None=�66   
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5       � WindowState$ 	Maximized=�5      .��5      ��  �� CornerRadius$ 10��6      PrimaryBackgroundColor#���   6   ._�5   
   � .+�5      �� $��Right=�6   $	��Top=�65   $��0,5,10,0q�6M   $��30��6_    �� Orientation$ 
Horizontal=�6k   $��RightToLeft=�6�   .+�5      �� -   5         ��Name$ btnClose��5        IconButtonsStyle#��[ 6+    	 close#���  6U   
	 � Tag$	 IsCloseButton��6�   �� 5      -   5        $ 
btnRestore��5      $	��5 0q�6-   ��5      �� 5      
 �� 
TargetType"
 ��6     �� BasedOn# [ 6=   .�5      ��  �� Property � ��  maximize �� Value# �  68    �� Triggers 5      �� 5   "    �� Binding 6.   ��  �� ElementName$
 home�� �� Path$ WindowState� �� Value 	Maximized6e   .a�5   &   ��  � ��  restore# �  6@   5   #   5      5      �� 5      -   5        $ btnMinimize��5      #���  6.     minimize#���  6Y   6~   5       � 5$   
   -   5          �Name$ 	MainFrame��5$        � NavigationUIVisibility$ Hidden=�6#   $��0,40,0,0q�6C   5&      5'      5(      