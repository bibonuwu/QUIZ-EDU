   M S B A M L   `   `   `  ����   FireQuiz!    FireQuiz.CreateQuestionPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       N� Title$  Create Question��.����  �� CornerRadius$ 10��   PrimaryBackgroundColor#���   ._���   SecundaryBackgroundColor#���  $ 40��$��50,20,50,50q� �� Padding$ 20q�._�� $��20q�.+��� $
��Auto=�$��Disabled=�.����  �� Orientation$ Vertical=�$	��Top=�.+��� $ 
Horizontal=�$��0,0,0,10q�.+��� $��Тип вопроса:��$
��Bold"�$��Center=�$��0,0,10,0q��� -    ��Name$ QuestionTypeComboBox��$	��180��.@��� $��Обычный тест�� �� 
IsSelected . �� $ ��Сәйкестендіру���� -    ��Name$ StandardTestPanel��.+��� $#��Cұрақты жазыңыз:��$��18(�$��0,0,0,5q��� $ 
Horizontal=�.+��� -    ��Name$ QuestionText��$��18(�$��	0,0,10,10q�$	��100�� �� TextWrapping$
 Wrap=�$��Center=�$��Center=�$	��300���� $ Vertical=�.+��� -   	 ��Name$	 QuestionImageBorder��$	��100��$	��100��$��2q�
�������$��5q�
�������._��� -   
 ��Name$
 QuestionImage��$��UniformToFill=��� -    ��Name$ DeleteQuestionImageButton��$!��❌ Удалить фото��$y�	Collapsed=��� $��Center=�$��10,0,0,0q�$��16(�20Cуреттті қою үшін: WIN + Shift + S�� $��Жауаптар:��$��18(�$��0,10,0,5q��� -   $ AnswerPanel1��$ 
Horizontal=�$��5q�.+��� $��Center=�$��25(�.��a)�� -   $ AnswerTextBox1��$	��250��$��
0,10,10,10q��� -	    ��Name$ AnswerCheckBox1��$��Правильный��$��5q�$��Center=��� -
   $	 AnswerImageBorder1��$	��100��$	��100��$��2q�
�������$��10q�
�������._��� -   $
 AnswerImage1��$��UniformToFill=��� -   $ DeleteAnswerImageButton1��$	��❌��$��30��$��30��$y�	Collapsed=��� -   $ AnswerPanel2��$ 
Horizontal=�$��5q�.+��� $��Center=�$��25(�.��b)�� -   $ AnswerTextBox2��$	��250��$��
0,10,10,10q��� -   $ AnswerCheckBox2��$��Правильный��$��5q�$��Center=��� -   $	 AnswerImageBorder2��$	��100��$	��100��$��2q�
�������$��10q�
�������._��� -   $
 AnswerImage2��$��UniformToFill=��� -   $ DeleteAnswerImageButton2��$	��❌��$��30��$��30��$y�	Collapsed=��� -   $ AnswerPanel3��$ 
Horizontal=�$��5q�.+��� $��Center=�$��25(�.��c)�� -   $ AnswerTextBox3��$	��250��$��
0,10,10,10q��� -   $ AnswerCheckBox3��$��Правильный��$��5q�$��Center=��� -   $	 AnswerImageBorder3��$	��100��$	��100��$��2q�
�������$��10q�
�������._��� -   $
 AnswerImage3��$��UniformToFill=��� -   $ DeleteAnswerImageButton3��$	��❌��$��30��$��30��$y�	Collapsed=��� -   $ AnswerPanel4��$ 
Horizontal=�$��5q�.+��� $��Center=�$��25(�.��d)�� -   $ AnswerTextBox4��$	��250��$��
0,10,10,10q��� -   $ AnswerCheckBox4��$��Правильный��$��5q�$��Center=��� -   $	 AnswerImageBorder4��$	��100��$	��100��$��2q�
�������$��10q�
�������._��� -   $
 AnswerImage4��$��UniformToFill=��� -   $ DeleteAnswerImageButton4��$	��❌��$��30��$��30��$y�	Collapsed=��� -   $ AnswerPanel5��$ 
Horizontal=�$��5q�.+��� $��Center=�$��25(�.��e)�� -    $ AnswerTextBox5��$	��250��$��
0,10,10,10q��� -!   $ AnswerCheckBox5��$��Правильный��$��5q��� -"   $	 AnswerImageBorder5��$	��100��$	��100��$��2q�
�������$��10q�
�������._��� -#   $
 AnswerImage5��$��UniformToFill=��� -$   $ DeleteAnswerImageButton5��$	��❌��$��30��$��30��$y�	Collapsed=��� $ 
Horizontal=�$��Right=�$��0,20,0,0q�.+��� -%   $��Сақтау��$	��150��$��36���� -&   $ MatchingTestPanel��$y�	Collapsed=�.+��� $!��Главный вопрос��$
��Bold"�$��18(�$��0,0,0,5q��� $��18(�$��50��$��0,0,0,20q����� *MainMatchingQuestion+ �� Mode$ TwoWay=��� -'    ��Name$ MatchingPairsControl������ *MatchingPairs+���� .R��� ���#333$ 30��$��0,0,0,20q�$ 20q�._��  � ColumnDefinitions �� $��40 ��� $	��120 ��� $��20 ��� $��* ��� $��20 ��� $	��250 �.+��� $��0��$
��Bold"�$��22(�$	��Top=�$��0,0,5,0q����� *
PairLetter+�� -(   $��1��$	��100��$	��100��
�������$ 5��$��2q����� *BorderBrush+._��� $��UniformToFill=�$��98��$��98������ *	LeftImage+�� $��3��$��16(�$	��100��$��Center=�$��0,0,0,0q����� *
LeftText+$ TwoWay=��� $��5�����#222.+��� ���� *	Options+���� .R��� $ 
Horizontal=�$��0,0,0,5q�.+��� $
��Bold"�$��16(�$��0,0,5,0q����� *DataContext.PairLetter+ �� RelativeSource ��  �� AncestorType$ ItemsControlL��� $	��140��$��32��$��0,0,0,0q����� *.+$ TwoWay=��� -)   $��+��$��40��$��40��$��0,10,0,0q�
 � Tag �� �� $ 
Horizontal=�$��Center=�$��0,20,0,0q�.+��� -*   $��+��$��50��$��50���� $ 
Horizontal=�$��Right=�$��0,20,0,0q�.+��� -+   $��Сақтау��$	��150��$��36��