   M S B A M L   `   `   `  ���� A  =TelERP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TelERP.VistasTelERP.Vistas  clr-namespace:TelERPTelERP       TelERP.Vistas.ServicesUser   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:TelERP.Vistas     "pclr-namespace:TelERP       � Title$  ServicesUser��$��454.341��$��744.407�� � WindowStartupLocation$ CenterScreen=� � 	Resources &   System.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value5  /System.Windows.Controls.DataGridLengthConverter)  #System.Windows.Controls.DataGridRow �� Triggers P� Property P� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader*  $System.Windows.Controls.DataGridCell	 ]� 
Conditions
 �� Property �� Value �� 
TargetType ��Name ��Name
 � Tag �� Triggers �� 
TargetName��%h  '        '  �     '  h    '  �    '�� )    '�� L    '�� g    �� "  .���  �@ VerticalGridLinesBrush
 ���� ��   �@ HorizontalGridLinesBrush �#0277bd��  �@ ColumnWidth$ * ��  �@ RowHeaderWidth$ 0Q���  � �� �#e1f5fe�� "  .���  � ��
 �������  � ��$ 28�� P�  �@J�IsMouseOver . . ���  � �� �#0277bd��  � ��
 ������� "  .���  � �� �#01579b��  � ��
 �������  � ��$ Center=���  � ��$ 20(��� "  .���  �@��TextAlignment$ Center=���  � ��$ 16(�-   �� " ��.���  � ��$ 15(��� " ��.���  � �� �#039be5��  � ��
 �������  � ��$ 0q���  � ��$ 10q���  � ��$ 16(� ]� 	 �� 
 �@J�IsMouseOver . ./���  � �� �#80d8ff��  � �� �#01579b��  � �� �#01579b��  � ��$ 2q��� " ��.���  � ��$ 14(���  � �� �� " ��.R�� .+��� ���#b3e5fc���#01579b$��0,0,0,2q��� $ PART_ContentHost��$��5,0,0,0q�$��Center=��� $ WaterMarkLabel��$��5,0,0,0q�#��z $��Center=�$y�	Collapsed=����	#FF324868$��Arial)� ]� 	 �� 
 � ��$  ���� 
 �@J�	IsFocused .  ./��� $ WaterMarkLabel�� � y�$ Visible=�P�  � {� .  . ���  � ��
 �iii�.��� ���#4fc3f7 � ColumnDefinitions �� �� $	��245 �.+��� -   $ lblSearchServicesUser��   TelERP.Strings   Buscar#��Z $
��Left=�$��	10,10,0,0q�$	��Top=�$��30��$��53���� -    ��Name$ txtSearchServicesUser��   
HintBuscar# Z $
��Left=�$��30��$��	68,10,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��341��  -     Name$ dgServicesUser��$��10,57,10,68q�   CanUserAddRows .     
IsReadOnly . �� -    ��Name$ btnCancelarServicesUser��   Cancelar#��Z $
��Left=�$��	10,0,0,10q�$��Bottom=�$	��129��$��41���� -   $ btnOkServicesUser��   	Confirmar#��Z $��Right=�$��	0,0,10,10q�$��Bottom=�$	��136��$��1��$��41���� -   $ lblNombreServices��   Servicio#��Z $
��Left=�$��0,58,0,0q�$	��Top=�$��1��$��30���� -   $ lblPriceServices��   Precio#��Z $
��Left=�$��0,89,0,0q�$	��Top=�$��1��$��30���� -	   $ lblDaysServices��   Dias#��Z $
��Left=�$��	0,120,0,0q�$	��Top=�$��1��$��30���� -
   $ lblTurnServices��    Turno#��Z  $
��Left=�$��	0,151,0,0q�$	��Top=�$��1��$��30���� -   $ lblDescriptionServices��!   Descripcion#��Z! $
��Left=�$��	0,182,0,0q�$	��Top=�$��1��$��30���� -   " ��Name$" txbDescriptionServices��$
��Left=�$��	0,213,0,0q�$
��Wrap=�$
��Auto��$	��217��{�.  $	��Top=�$��1��