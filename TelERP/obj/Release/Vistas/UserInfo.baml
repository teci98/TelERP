   M S B A M L   `   `   `  ���� A  =TelERP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TelERP.VistasTelERP.Vistas  clr-namespace:TelERPTelERP      TelERP.Vistas.UserInfo   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:TelERP.Vistas     "pclr-namespace:TelERP       � Title$  UserInfo��$��556.525��$��807.922�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=� � 	Resources &   System.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value5  /System.Windows.Controls.DataGridLengthConverter)  #System.Windows.Controls.DataGridRow �� Triggers P� Property	 P� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader*  $System.Windows.Controls.DataGridCell
 ]� 
Conditions �� Property �� Value��%�  '        '  �     '  h    '  �    '�� $    '�� G    �� "  .���  �@ VerticalGridLinesBrush
 ���� ��   �@ HorizontalGridLinesBrush �#0277bd��  �@ ColumnWidth$ * ��  �@ RowHeaderWidth$ 0Q���  � �� �#e1f5fe�� "  .���  � ��
 �������  � ��$ 28�� P�  �@J�IsMouseOver	 . . ���  � �� �#0277bd��  � ��
 ������� "  .���  � �� �#01579b��  � ��
 �������  � ��$ Center=���  � ��$ 20(��� "  .���  �@��TextAlignment$ Center=���  � ��$ 16(��� " ��.���  � ��$ 15(��� " ��.���  � �� �#039be5��  � ��
 �������  � ��$ 0q���  � ��$ 10q���  � ��$ 16(� ]� 
 ��  �@J�IsMouseOver . ./���  � �� �#80d8ff��  � �� �#01579b��  � �� �#01579b��  � ��$ 2q�.��� ���#4fc3f7.+��� -    ��Name$ imgUserInfo��$
��Left=�$	��118��$��	27,22,0,0q�$	��Top=�$	��136���� -    ��Name$ lblNombreUserInfo��   TelERP.Strings   Nombre#��Z $
��Left=�$��
202,22,0,0q�$	��Top=��� -   $ lblApellidoUserInfo��   	Apellidos#��Z $
��Left=�$��
202,60,0,0q�$	��Top=��� -   $ lblEmailUserInfo��   Email#��Z $
��Left=�$��
202,96,0,0q�$	��Top=��� -   $ lblPhoneUserInfo��   Telefono#��Z $
��Left=�$��
521,22,0,0q�$	��Top=��� -   $ lblTelegramUserInfo��   Telegram#��Z $
��Left=�$��
451,96,0,0q�$	��Top=�$y�	Collapsed=��� -   $ lblDebtUserInfo��   Deuda#��Z $
��Left=�$��
521,60,0,0q�$	��Top=�  -     Name$ dgServicesUserInfo��$��27,187,288,100q�   CanUserAddRows .     
IsReadOnly . �� -	    ��Name$ btnCancelUserInfo��   Salir#��Z $
��Left=�$��	27,0,0,23q�$��Bottom=�$��80��$��50���� -
   $ btnAddServiceUserInfo��   ModServicios#��Z $��Right=�$��
0,0,29,263q�$��Bottom=�$	��186��$��50���� -   $ btnAddAssignUserInfo��   AñadirNuevoTrabajo#��Z $��Right=�$��
0,0,29,180q�$��Bottom=�$	��186��$��50���� -   $ btnModAssignUserInfo��   
ModTrabajo#��Z $��Right=�$��
0,0,29,100q�$��Bottom=�$	��186��$��50���� -   $ btnDetallesUserInfo��   VerDetalles#��Z $��Right=�$��	0,0,29,23q�$��Bottom=�$	��186��$��50���� -   $ lblDetalles��   	Servicios#��Z $
��Left=�$��
27,148,0,0q�$	��Top=�$��18(�