   M S B A M L   `   `   `  ���� A  =TelERP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TelERP.VistasTelERP.Vistas  clr-namespace:TelERPTelERP  %    TelERP.Vistas.SelectUserTrabajo   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:TelERP.Vistas     "pclr-namespace:TelERP       � Title$  SelectUserTrabajo��$	��500��$	��600�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � 	Resources &   System.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value5  /System.Windows.Controls.DataGridLengthConverter)  #System.Windows.Controls.DataGridRow �� Triggers	 P� Property
 P� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader*  $System.Windows.Controls.DataGridCell ]� 
Conditions �� Property �� Value �� 
TargetType ��Name ��Name
 � Tag �� Triggers �� 
TargetName��%6  '        '  �     '  h    '  �    '�� $    '�� ?    �� "  .���  �@ VerticalGridLinesBrush
 ���� ��   �@ HorizontalGridLinesBrush �#0277bd��  �@ ColumnWidth$ * ��  �@ RowHeaderWidth$ 0Q���  � �� �#e1f5fe�� "  .���  � ��
 �������  � ��$ 28�� P� 	 �@J�IsMouseOver
 . . ���  � �� �#0277bd��  � ��
 ������� "  .���  � �� �#01579b��  � ��
 �������  � ��$ Center=���  � ��$ 20(��� "  .���  �@��TextAlignment$ Center=���  � ��$ 16(��� " ��.���  � �� �#039be5��  � ��
 �������  � ��$ 0q���  � ��$ 10q���  � ��$ 16(� ]�  ��  �@J�IsMouseOver . ./���  � �� �#80d8ff��  � �� �#01579b��  � �� �#01579b��  � ��$ 2q��� " ��.���  � ��$ 16(���  � �� �� " ��.R�� .+��� ���#b3e5fc���#01579b$��0,0,0,2q��� $ PART_ContentHost��$��5,0,0,0q�$��Center=��� $ WaterMarkLabel��$��5,0,0,0q�#��z $��Center=�$y�	Collapsed=����	#FF324868$��Arial)� ]�  ��  � ��$  ����  �@J�	IsFocused .  ./��� $ WaterMarkLabel�� � y�$ Visible=�P� 	 � {�
 .  . ���  � ��
 �iii�.��� ���#4fc3f7.+���    TelERP.Strings   SelUser#��Z $
��Left=�$��	10,10,0,0q�$	��Top=�$	��580��$��Center=�$��36(�$
��Bold"��� -      Cancelar#��Z $
��Left=�$��
40,449,0,0q�$	��Top=�$	��170��  -     Name$ 	dgSelUser��$	��324��$��40,109,40,0q�$	��Top=�   
IsReadOnly .    CanUserAddRows .  �� -      	Confirmar#��Z $
��Left=�$��390,449,0,0q�$	��Top=�$	��170����    Buscar#��Z $
��Left=�$��	40,73,0,0q�$	��Top=�$��16(��� -    ��Name$ txtSearchSelUser��   
HintBuscar# Z $
��Left=�$��31��$��
101,73,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��459��