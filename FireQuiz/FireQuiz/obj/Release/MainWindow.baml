   M S B A M L   `   `   `  ����   FireQuiz    FireQuiz.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  FireQuiz��$	��500��$	��800�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=�
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=� � WindowState$ 	Maximized=�.����  �� CornerRadius$ 10��   PrimaryBackgroundColor#���   ._�� .+��� $��Right=�$	��Top=�$��0,5,10,0q�$��30�� �� Orientation$ 
Horizontal=�$��RightToLeft=�.+��� -    ��Name$ btnClose��  IconButtonsStyle#��[  	 close#���  
	 � Tag$	 IsCloseButton���� -   $ 
btnRestore��$	��5 0q����� 
 �� 
TargetType"
 �� �� BasedOn# [ .���  �� Property � ��  maximize �� Value# �   �� Triggers ��  �� Binding ��  �� ElementName$
 home�� �� Path$ WindowState� �� Value 	Maximized.a���  � ��  restore# �  �� -   $ btnMinimize��#���    minimize#���   � -     �Name$ 	MainFrame��  � NavigationUIVisibility$ Hidden=�$��0,40,0,0q�