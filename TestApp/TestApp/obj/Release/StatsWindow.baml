   M S B A M L   `   `   `  ����   TestApp    TestApp.StatsWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$+  %Результаты экзамена�� � WindowState$ 	Maximized=� J� Opacity$ 1Q� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=�
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�.����  �� CornerRadius$ 10��   PrimaryBackgroundColor#���   ._�� .+��� $��Right=�$	��Top=�$��0,5,10,0q�$��30�� �� Orientation$ 
Horizontal=�$��RightToLeft=�.+��� -   	 ��Name$	 btnClose��  IconButtonsStyle#��[  	 close#���  

 � Tag$
 IsCloseButton���� -   $	 
btnRestore��$	��5 0q�����  �� 
TargetType" �� �� BasedOn# [ .���  �� Property � ��  maximize �� Value# �   �� Triggers ��  �� Binding ��  �� ElementName$
 home�� �� Path$ WindowState� �� Value 	Maximized.a���  � ��  restore# �  �� -   $	 btnMinimize��#���    minimize#���  ��   SecundaryBackgroundColor#���  $ 40,40,40,40��$��50,50,50,50q�._��� $��40,40,40,40q�.+�� $��0,0,0,20q� � ColumnDefinitions �� $��3* ��� $��* �.+��� $ 
Horizontal=�.+��� $)��#Емтихан нәтижелері��$��24(�  MontserratBold#��[ �� $ 
Horizontal=�$��Center=�$��20,0,0,0q�.+��� $%��Выберите сессию: ��$��Center=�$��14(�$��0,0,10,0q�  MontserratMedium#��[ �� -    ��Name$ SessionSelector��$	��200��#��[ �� -   $��Мәзірге оралу�� 	 BlueRoundedButton#��[	 $��36��$	��150��$��1��$��Right=�A�  A� Columns$ 4��$��0,0,0,20q�$��90��.+��� ���#E6E2FF$ 10��$��5q� �� Padding$ 15q�._�� .+��� $��Жалпы ұпай��$
��Bold"����#8C40EC$
��Left=�$��20(�$��Center=�#��[ �� -    ��Name$ TotalScoreText��$��24(�$
��Bold"����#8C40EC$��Right=�$��Center=�#��[ �� ���#D2F4FF$ 10��$��5q�$ 15q�._�� .+��� $��Жетістік(%)��$
��Bold"����#34A5CF$
��Left=�$��20(�$��Center=�#��[ �� -   $ AchievementText��$��24(�$
��Bold"����#34A5CF$��Right=�$��Center=�#��[ �� ���#D5FEC7$ 10��$��5q�$ 15q�._�� .+��� $��
Дұрыс��$
��Bold"����#5EB03D$
��Left=�$��20(�$��Center=�#��[ �� -	   $ CorrectAnswersText��$��24(�$
��Bold"����#5EB03D$��Right=�$��Center=�#��[ �� ���#FAD6CE$ 10��$��5q�$ 15q�._�� .+��� $��Қате��$
��Bold"����#C54451$
��Left=�$��20(�$��Center=�#��[ �� -
   $ IncorrectAnswersText��$��24(�$
��Bold"����#C54451$��Right=�$��Center=�#��[ �  �� $��* ��� $��* ��� $��* �.+��� $��Жалпы мәлімет��#��[ $��0,0,20,0q�$��18(����	#FFC0C9EA.���� $��10q�.+��� -   $ UserNameText��#��[ $��16(��� -   $ ExamNameText��#��[ $��16(��� -   $ StartTimeText��#��[ $��16(��� -   $ EndTimeText��#��[ $��16(��� -   $ DurationText��#��[ $��16(��� $:��4Пәндер бойынша көрсеткіштер��#��[ $��18(�$��1�����	#FFC0C9EA.���� -    ��Name$ SubjectsProgressPanel��$��10q��� $*��$Пән бойынша бағалау��$��2��#��[ $��18(�$��20,0,0,0q����	#FFC0C9EA.���� -   $ SubjectsGradePanel��$��10q�� $��0,10,0,0q� �� $��3* ��� $��2* �.+��� $0��*Тақырып бойынша анализ��#��[ $��18(����	#FFC0C9EA$��0,0,20,0q�.���� -   $ TopicAnalysisPanel��$��10q��� $ ��Топ 10 қатысушы��#��[ $��18(�$��1�����	#FFC0C9EA.���� -   $ TopParticipantsPanel��$��10q�