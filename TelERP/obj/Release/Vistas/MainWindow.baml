   M S B A M L   `   `   `  ���� A  =TelERP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TelERP.VistasTelERP.Vistas  clr-namespace:TelERPTelERP      TelERP.Vistas.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:TelERP.Vistas     "pclr-namespace:TelERP       � Title$  TelERP�� � WindowState$ 	Maximized=� � 
ResizeMode$ CanResizeWithGrip=� � 	Resources    tb_mouse_over_right �� ImageSource  tb_mouse_over_left �� 
TargetType �� Property �� Value �� 
TargetType	 ��Name
 ��Name
 � Tag �� Triggers ]� 
Conditions �� Property �� Value �� 
TargetName P� Property P� Value  tb_style ��Name J� SnapsToDevicePixels&   System.Windows.Controls.DataGrid5  /System.Windows.Controls.DataGridLengthConverter)  #System.Windows.Controls.DataGridRow �� Triggers=  7System.Windows.Controls.Primitives.DataGridColumnHeader*  $System.Windows.Controls.DataGridCell  estilotitulos  gigante  
estiloinfo��%�  &	        &	 7     '�� o     &	 *    7  7  7   7   '  p    '  B    '  �    '  I    &	 �    &	 �    &	 $    '�� G    '�� �    '�� �    �� $1 +/TelERP;component/Recursos/chevron-left.png���� $2 ,/TelERP;component/Recursos/chevron-right.png���� " ��.���  � ��$ 14(���  � �� �� " ��.R�� .+��� ���#b3e5fc���#01579b$��0,0,0,2q��� $	 PART_ContentHost��$��5,0,0,0q�$��Center=��� $
 WaterMarkLabel��$��5,0,0,0q�#��z $��Center=�$y�	Collapsed=����	#FF324868$��Arial)� ]�  ��  � ��$  ����  �@J�	IsFocused .  ./��� $ WaterMarkLabel�� � y�$ Visible=�P�  � {� .  . ���  � ��
 �iii��� " d�.���  � �� �� " d�.R��� $ border��#��z��#��z�� .  ]�  ��  �@d�	IsChecked$ FalseY���  �@J�IsMouseOver . ./��� $ border��8    � ���� $ border��8   � ��P�  �@d�	IsChecked$
 TrueY�. ��� $ border��8   � ���� $ border��8   � ���� "  .���  �@ VerticalGridLinesBrush
 ���� ��   �@ HorizontalGridLinesBrush �#0277bd��  �@ ColumnWidth$ * ��  �@ RowHeaderWidth$ 0Q���  � �� �#e1f5fe�� "  .���  � ��
 �������  � ��$ 28�� P�  �@J�IsMouseOver . . ���  � �� �#0277bd��  � ��
 ������� "  .���  � �� �#01579b��  � ��
 �������  � ��$ Center=���  � ��$ 20(��� "  .���  �@��TextAlignment$ Center=���  � ��$ 16(��� " ��.���  � ��$ 20(���  � ��$
 Bold"��� " ��.���  � ��$ 50(���  � ��$
 Bold"���  � ��$ Center=��� " ��.���  � ��$ 15(��� " ��.���  � ��$ 14(���  � �� �#e1f5fe�� " ��.���  � �� �#039be5��  � ��
 �������  � ��$ 0q���  � ��$ 10q���  � ��$ 16(� ]�  ��  �@J�IsMouseOver . ./���  � �� �#80d8ff��  � �� �#01579b��  � �� �#01579b��  � ��$ 2q��� " ��.���  � �� �#b3e5fc��  � �� �#01579b.��� -   .+�� -    �Name$ gGeneral�����#4fc3f7$��70,0,0,0q�$��2��$y�	Collapsed=� � RowDefinitions �� $	��11* ��� $	��13* � � ColumnDefinitions �� $
��289* ��� $
��354* �.+��� -   $
 lblFechaMain��$��	0,10,10,0q�$	��Top=�$��Right=�$��1��#��[   -     Name$ dgDeudasGeneral��$��10,120,29,79q�   CanUserAddRows .     
IsReadOnly . $��Stretch=�$��Stretch=�$��2��  -   $ dgAssignGeneral��$��57,63,113,16q� .   . $��1��$��Stretch=�$��Stretch=�$��1����    TelERP.Strings   Deudas#��Z $��	10,78,0,0q�$	��Top=�$
��Left=�#��[ ��    TrabajosPendientes#��Z $��	57,21,0,0q� J� RenderTransformOrigin$ 0.632,0.269&�$
��Left=�$	��Top=�$��1��$��1��#��[ ��    General#��Z $
��10,0q�$	��Top=�#��[ $��2���� -     ��Name$  btnPagarDeudaGeneral��!   
PagarDeuda#��Z! $
��Left=�$��	10,0,0,10q�$��1��$��Bottom=�$	��148���� -   $  btnPagarTrabajosGeneral��"   PagarTrabajo#��Z" $��Right=�$��	0,0,29,10q�$��1��$��Bottom=�$	��148��� -   $ 	gUsuarios�����#4fc3f7$��70,0,0,0q�$��2��$y�	Collapsed=� �� $
��401* ��� $	��290 �.+��� -	   $
 lblSearchUsers��#   Buscar#��Z# $
��Left=�$��	10,82,0,0q�$	��Top=�#��[ �� -
   $ ��Name$$ txtSearchUsers��%   
HintBuscar# Z% $��30��$��
68,82,36,0q�& �� TextWrapping$
& Wrap=�$	��Top=�  -   $ dgUsers��$��10,117,36,97q� .   . �� -   ' ��Name$' imgUsers��$��Right=�$	��100��$��	0,12,91,0q�$	��Top=�$	��100��$��1���� -   $
 lblNombreUsers��(   Nombre#��Z( $
��Left=�$��
10,126,0,0q�$	��Top=�$��1��#��[ �� -   $
 lblApellidoUsers��)   	Apellidos#��Z) $
��Left=�$��
10,156,0,0q�$	��Top=�$��1��#��[ �� -   $
 lblEmailUsers��*   Email#��Z* $
��Left=�$��
10,188,0,0q�$	��Top=�$��1��#��[ �� -   $
 lblPhoneUsers��+   Telefono#��Z+ $
��Left=�$��
10,218,0,0q�$	��Top=�$��1��#��[ �� -   $  
btnNewUser��,   AñadirUsuario#��Z, $
��Left=�$��	10,0,0,24q�$��Bottom=�$	��175��$��47���� -   $  btnModifyUser��-   
ModUsuario#��Z- $��Right=�$��	0,0,36,24q�$��Bottom=�$	��148��$��47���� -   $  btnInfoUsers��.   MostrarInfo#��Z. $��Right=�$��
0,0,48,169q�$��Bottom=�$	��199��$��47��$��1���� -   $  btnAssignUser��/   AñadirTraUsuario#��Z/ $��Right=�$��	0,0,48,97q�$��Bottom=�$	��199��$��47��$��1���� -   $  btnDeleteUser��0   
DelUsuario#��Z0 $��Right=�$��	0,0,48,24q�$��Bottom=�$	��199��$��47��$��1���� ���#01579b$��1q�$��1��$
��Left=�$��1���� 1   Usuarios#��Z1 $
��10,0q�$	��Top=�#��[ $��2���� -   $  btnMsgUsers��2   	EnviarMsg#��Z2 $��Right=�$��
0,0,48,241q�$��Bottom=�$	��199��$��47��$��1��� -   $ 	gServices�����#4fc3f7$��70,0,0,0q�$y�	Collapsed=� �� $
��401* ��� $	��290 �.+��� -   $
 lblSearchServices��#��Z# $
��Left=�$��	10,82,0,0q�$	��Top=�#��[ �� -   $$ txtSearchServices��# Z% $��30��$��
68,82,36,0q�$
& Wrap=�$	��Top=�  -   $ 
dgServices��$��10,117,36,97q� .   . �� -   $
 lblNombreServices��3   Servicio#��Z3 $
��Left=�$��	10,96,0,0q�$	��Top=�#��[ $��1���� -   $
 lblPriceServices��4   Precio#��Z4 $
��Left=�$��
10,127,0,0q�$	��Top=�#��[ $��1���� -   $
 lblDaysServices��5   Dias#��Z5 $
��Left=�$��
10,158,0,0q�$	��Top=�#��[ $��1���� -   $
 lblTurnServices��6   Turno#��Z6 $
��Left=�$��
10,189,0,0q�$	��Top=�#��[ $��1���� -   $  btnNewServices��7   AñadirServicio#��Z7 $
��Left=�$��	10,0,0,22q�$	��176��$��47��$��Bottom=��� -    $  btnModifyServices��8   ModServicio#��Z8 $��	0,0,32,22q�$��Right=�$	��153��$��47��$��Bottom=��� -!   $  btnDeleteServices��9   DelServicio#��Z9 $
��Left=�$��	98,0,0,22q�$��Bottom=�$	��146��$��47��$��1���� -"   $
 lblDescriptionServices��:   Descripcion#��Z: $
��Left=�$��
10,220,0,0q�$	��Top=�#��[ $��1���� -#   ; ��Name$; txbDescriptionServices��$
��Left=�$��
10,255,0,0q�$
��Wrap=�$	��Top=�$
��Auto��$	��234��{�.  $��1���� ���#01579b$��1q�$��1��$
��Left=�$��1���� <   	Servicios#��Z< $
��10,0q�$	��Top=�#��[ $��2��� -$   $ 	gTrabajos�����#4fc3f7$��70,0,0,0q�$y�	Collapsed=� �� $
��401* ��� $	��290 �.+��� -%   $
 lblSearchAssignments��#��Z# $
��Left=�$��	10,82,0,0q�$	��Top=�#��[ �� -&   $$ txtSearchAssignments��# Z% $��30��$��
68,82,36,0q�$
& Wrap=�$	��Top=�  -'   $ dgAssignments��$��10,117,36,97q� .   . �� -(   $
 lblNombreAssignments��#��Z( $
��Left=�$��	11,88,0,0q�$	��Top=�#��[ $��1���� -)   $
 lblUserAssignments��=   Usuario#��Z= $
��Left=�$��
11,119,0,0q�$	��Top=�#��[ $��1���� -*   $
 lblDeadlineAssignments��>   Fecha#��Z> $
��Left=�$��
11,150,0,0q�$	��Top=�#��[ $��1���� -+   $
 lblPriceAssignments��#��Z4 $
��Left=�$��
11,181,0,0q�$	��Top=�#��[ $��1���� -,   $  btnNewAssignments��?   AñadirNuevoTrabajo#��Z? $
��Left=�$��	10,0,0,10q�$��Bottom=�$	��181��$��47���� --   $  btnModifyAssignments��@   
ModTrabajo#��Z@ $��Right=�$��	0,0,31,10q�$��Bottom=�$	��149��$��47���� -.   $  btnDeleteAssignments��A   
DelTrabajo#��ZA $��Right=�$��	0,0,89,10q�$��Bottom=�$	��161��$��47��$��1���� -/   $
 lblDescriptionAssignments��#��Z: $
��Left=�$��
11,212,0,0q�$	��Top=�#��[ $��1���� -0   $; txbDescrptionAssignments��$
��Left=�$��
11,243,0,0q�$
��Wrap=�$	��Top=�$
��Auto��$	��190��$��1���� ���#01579b$��1q�$��1��$
��Left=�$��1���� B   Trabajos#��ZB $
��10,0q�$	��Top=�#��[ $��2���� -1   $  btnPagarAssignments��C   MarcarPagado#��ZC $��Right=�$��	0,0,89,80q�$��Bottom=�$	��161��$��47��$��1��� -2   $ 	gInvoices�����#4fc3f7$��70,0,0,0q�$y�	Collapsed=� �� $
��401* ��� $	��290 �.+�  -3   $ 
dgInvoices��$��10,117,36,75q� .   . �� -4   D   VerDetalles#��ZD $
��Left=�$��	10,0,0,10q�$��Bottom=�$	��163���� -5   $
 lblSearchInvoices��#��Z# $
��Left=�$��	10,82,0,0q�$	��Top=�#��[ �� -6   $$ txtSearchInvoices��# Z% $��30��$��
68,82,36,0q�$
& Wrap=�$	��Top=��� -7   $��btnEnviarFactura��E   
EnviarFact#��ZE $��Right=�$��
0,0,50,111q�$��Bottom=�$	��190��$��1���� -8   $��btnEnviarNota��F   	EnviarNot#��ZF $��
50,0,50,37q�$��41��$��Bottom=�$��1���� ���#01579b$��1q�$��1��$
��Left=�$��1���� G   Facturas#��ZG $
��10,0q�$	��Top=�#��[ $��2���� -9   #��ZC $��Right=�$��	0,0,10,10q�$��Bottom=�$	��163���� -:   H ��Name$H cbFacturasNoPagadas��I   FacturasNoPagadas#��ZI $��1��$��10,117,10,0q�$	��Top=�$��16(�$
��Auto��� -;   $ gAjustes�����#4fc3f7$��70,0,0,0q�$y�	Collapsed=�.+��� $
��Left=�$	��246��$��	10,82,0,0q�
���   �$	��Top=�$	��287���� J   Turnos#��ZJ $
��Left=�$��	26,92,0,0q�$	��Top=�#��[   -<   $ dgTurns��$
��Left=�$	��146��$��
26,124,0,0q�$	��Top=�$	��114�� .   . K   ColumnWidth$K * �� L   Inicio#��ZL $
��Left=�$��146,122,0,0q�$	��Top=�#��[ �� 
M   Fin#��ZM $
��Left=�$��146,160,0,0q�$	��Top=�#��[ �� -=   $$ txtInicioTurn��N   
ModeloHora# ZN $
��Left=�$��30��$��196,122,0,0q�$
& Wrap=�$	��Top=�$��59��O �� 	MaxLength$O 5���� ->   $$ 
txtFinTurn��# ZN $
��Left=�$��30��$��196,160,0,0q�$
& Wrap=�$	��Top=�$��59��$O 5���� -?   $  btnBorrarTurn��P   Borrar#��ZP $
��Left=�$��196,276,0,0q�$	��Top=�$��84���� -@   $  btnAñadirTurn��Q   Añadir#��ZQ $
��Left=�$��146,210,0,0q�$	��Top=�$	��134���� R   
NuevoTurno#��ZR $
��Left=�$��
146,92,0,0q�$	��Top=�#��[ �� $
��Left=�$	��206��$��371,104,0,0q�
���   �$	��Top=�$	��307���� -A   $$ txtTelefonoTelegram��$
��Left=�$��30��$��486,150,0,0q�$
& Wrap=�$	��Top=�$	��126���� -B   $$ txtCodigoTelegram��$
��Left=�$��30��$��486,184,0,0q�$
& Wrap=�$	��Top=�$	��126���� #��Z+ $
��Left=�$��407,150,0,0q�$	��Top=�#��[ �� S   Codigo#��ZS $
��Left=�$	��Top=�$��407,184,0,0q�#��[ �� -C   T   	EnviarCod#��ZT $
��Left=�$��391,248,0,0q�$	��Top=�$	��133���� -D   $  btnconectar��$��Conectar��$
��Left=�$��553,248,0,0q�$	��Top=�$	��105��{�.  �� -E   $  btnModificarTurn��U   	Modificar#��ZU $
��Left=�$��
26,276,0,0q�$	��Top=�$	��114���� $
��Left=�$	��234��$��
10,334,0,0q�
���   �$	��Top=�$	��301���� V   InfoEmpresa#��ZV $
��Left=�$��
19,344,0,0q�$	��Top=�#��[ �� -F   $$ txtnombreempresa��$
��Left=�$��30��$��106,380,0,0q�$
& Wrap=�$	��Top=�$	��191���� #��Z( $
��Left=�$	��Top=�$��
29,380,0,0q�#��[ �� -G   $$ txtcif��$
��Left=�$��30��$��106,414,0,0q�$
& Wrap=�$	��Top=�$	��191���� 
W   Cif#��ZW $
��Left=�$	��Top=�$��
63,414,0,0q�#��[ �� -H   $$ txtdireccion��$
��Left=�$��30��$��106,444,0,0q�$
& Wrap=�$	��Top=�$	��191���� X   	Direccion#��ZX $
��Left=�$	��Top=�$��
22,444,0,0q�#��[ �� -I   $$ txttelefono��$
��Left=�$��30��$��106,480,0,0q�$
& Wrap=�$	��Top=�$	��191���� #��Z+ $
��Left=�$	��Top=�$��
29,480,0,0q�#��[ �� -J   $$ txtemail��$
��Left=�$��30��$��106,514,0,0q�$
& Wrap=�$	��Top=�$	��191���� #��Z* $
��Left=�$	��Top=�$��
48,514,0,0q�#��[ �� Y   ConectarTelegram#��ZY $
��Left=�$��383,112,0,0q�$	��Top=�#��[ �� Z   Ajustes#��ZZ $
��10,0q�$	��Top=�#��[ $��2���� $
��Left=�$	��106��$��371,338,0,0q�
���   �$	��Top=�$	��153���� [   Idioma#��Z[ $
��Left=�$��383,354,0,0q�$	��Top=�#��[ �� -K   $��cbIdioma��$
��Left=�$��383,396,0,0q�$	��Top=�$	��125��$��16(�.@��� -L   $��cbiEspa��	\   es#��Z\ �� -M   $��cbiIng��	]   en#��Z] �� -N   $��cbiFran��	^   fr#��Z^ �� -O   $��Insertar datos de prueba��$��Right=�$��	0,0,10,10q�$��Bottom=�$	��224��$��49��� -P   $ panel��$
��Left=�$��70�����#00b0ff.+��� -Q   _ ��Name$_ 
stackpanel��.+�� $��90��.+��� -R   $��1��$��0��$��80,-20,50,0q�$
��MENU��
�������$��32(�
������ $��Right=�$��Center=�$
��Bold"�` � Triggers` :� a :� RoutedEvent$a 	MouseDown��.[��� .���� .��W� b �� 
TargetName$b panel��c �� TargetProperty$c Width�d g� 	BeginTime$d 0:0:0X�e W� From$	e 250X�	f W� To$f 70X�g g� Duration$g 0:0:0.2D�d� -S   $��tgbtn��$��0��$��Center=�$
��Left=�$��50��$��50��$��
10,-20,0,0q�$��0q�#��[ ���� $) #/TelERP;component/Recursos/menu.png��h j� Stretch$
h None=�` :� $a ToggleButton.Unchecked��.[��� .���� -T   i ��Name$i hidestackpanel��.��W� $b panel��$c Width�$d 0:0:0X�$	e 250X�$f 70X�$g 0:0:0.2D�:� $a ToggleButton.Checked��.[��� .���� -U   $i showstackpanel��.��W� $b panel��$c Width�$d 0:0:0X�$e 70X�$	f 250X�$g 0:0:0.2D�j � ToolTipj _� -V   k _�Name$k tt_desplegar��$��	Desplegar���� 
���   �$��10���� -W   l ��Name$l listview��
������ 
������ $��Disabled=�.@��� -X   m ��Name$m lvihome��$
��Left=�$��0,0,0,0q�.���� n �� Orientation$n 
Horizontal=�$	��250��.+��� $)��#/TelERP;component/Recursos/home.png��$
��Fill=�$��12,0,0,0q�$��35��$��35���� #��Z $��25,0,0,0q�
�������$��24(�
������ $��Center=�j _� -Y   $k tt_home��#��Z �� -Z   $m lviusers��$
��Left=�$��0,0,0,0q�.���� $n 
Horizontal=�$	��250��.+��� $5��//TelERP;component/Recursos/account-multiple.png��$
��Fill=�$��12,0,0,0q�$��35��$��35���� #��Z1 $��25,0,0,0q�
�������$��24(�
������ $��Center=�j _� -[   $k tt_users��#��Z1 �� -\   $m lviservices��$
��Left=�$��0,0,0,0q�.���� $n 
Horizontal=�$	��250��.+��� $-��'/TelERP;component/Recursos/services.png��$
��Fill=�$��12,0,0,0q�$��35��$��35���� #��Z< $��25,0,0,0q�
�������$��24(�
������ $��Center=�j _� -]   $k 
tt_service��#��Z< �� -^   $m 	lviassign��$
��Left=�$��0,0,0,0q�.���� $n 
Horizontal=�$	��250��.+��� $,��&/TelERP;component/Recursos/trabajo.png��$
��Fill=�$��12,0,0,0q�$��35��$��35���� #��ZB $��25,0,0,0q�
�������$��24(�
������ $��Center=�j _� -_   $k 	tt_assign��#��ZB �� -`   $m lviinvoices��$
��Left=�$��0,0,0,0q�.���� $n 
Horizontal=�$	��250��.+��� $,��&/TelERP;component/Recursos/receipt.png��$
��Fill=�$��12,0,0,0q�$��35��$��35���� #��ZG $��25,0,0,0q�
�������$��24(�
������ $��Center=�j _� -a   $k 
tt_invoice��#��ZG �� 
���   �$��10���� -b   $m 
lviajustes��$
��Left=�$��0,0,0,0q�.���� $n 
Horizontal=�$	��250��.+��� -c   $' imgsett��$(��"/TelERP;component/Recursos/cog.png��$
��Fill=�$��12,0,0,0q�$��35��$��35��$ 0.5,0.5&�z��� o �� Angle$o 0Q��� #��ZZ $��25,0,0,0q�
�������$��24(�
������ $��Center=�j _� -d   $k tt_settings��#��ZZ ` :� $a ListViewItem.MouseEnter��.[��� .���� -e   $i 	rotarsett��.��W� $b imgsett��$c RenderTransform.Angle�$e 0X�$	f 180X�$g 0:0:0.5D�:� $a ListViewItem.MouseLeave��.[��� .���� -f   $i desrotarsett��.��W� $b imgsett��$c RenderTransform.Angle�$e 60X�$f 0X�$g 0:0:0D����� " ��.���  � �� �� " ��.R��� $ bd��$��50��#��z��#��z��#��z��p �� Padding#p z�� . ._��� #��z��# z #��z�� ]�  ��  �@J�IsMouseOver . ./��� $ bd�� � �� �#01579b�� $ bd�� � �� �#01579b