   M S B A M L   `   `   `  ����A  =kb_app, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:kb_app.Windowskb_app.Windows  5   	       kb_app.Windows.BaseWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:kb_app.Windows     5   	     � Title$  Window��5   	   $	��450��5	   	   $	��800��5
   	   ��5      �� 5   
    �� ImageSource$$ ..\Resources\BaseWindow_bg.jpg��6    j� Stretch$
 Fill=�6B   6S   .��5      � -   5         �Name$
 grid��5       � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ Controls_GroupBox��5      $��Controls��6.      GroupBox_Style#��[  6@   .��5      �� -   5         ��Name$ Controls_StackPanel��5       �� Orientation$ Vertical=�66   $��0��6M   $��0��6Z   .+�5      �� -   5        	 ��Name$	 
Add_Button��5        Button_Style#��[ 6F   .��6l   Add6q   �� 5       -   5        $	 Edit_Button��5       #��[ 6H   .��6n   Edit6t   �� 5!      -   5        $	 Delete_Button��5!      #��[ 6L   .��6r   Delete6z   �� 5"      -   5        $	 Search_Button��5"      #��[ 6L   .��6r   Search6z   5#      5$      &   System.Windows.Controls.DataGrid  5&   
   -   5        $��Table��5&      
   AutoGenerateColumns$

 True��6       SelectionMode$ Single=�6;      CanUserAddRows$ False��6R      
IsReadOnly$
 True��6i   $��0��6{   $��1��6�     Datagrid_Style#��[ 6�   �� 5(   
   -	   5        $ Input_GroupBox��5(      $��Entry fields��6,   $��5q�6B   $��2��6N   $��0��6^   #��[  6k   .��5)      �� -
   5        $ Input_StackPanel��5)      $ Vertical=�63   .+�5*      �� -   5        $	 Enter_Button��5*      ${�False��6L   $y�	Collapsed=�6^   #��[ 6u   .��6�   Enter6�   5+      5,      5-      5.      