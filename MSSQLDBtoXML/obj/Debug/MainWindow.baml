   M S B A M L   `   `   `  ����G  CMSSQLDBtoXML, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MSSQLDBtoXMLMSSQLDBtoXML  5   	   H DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5   	       MSSQLDBtoXML.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:MSSQLDBtoXML     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	     � Title$  Transfer to XML��5	   	   $	��230��6!   $	��544��6.    � WindowStartupLocation$ CenterScreen=�6:    � 
ResizeMode$ NoResize=�6_    � DataContext 5      &    MSSQLDBtoXML.MainWindowViewModel  5   
   -   5        md��5   $   .��5      ��  �� Orientation$ Vertical=�6   $��10q�6(   .+�5   
   �� $ 
Horizontal=�6   $��3q�6.   .+�5      �� $	��100��6   .��6    Host6&   �� 5      $	��400��6   ��6"   �� *Host+5      �� 5   
   $ 
Horizontal=�6   $��3q�6.   .+�5      �� $	��100��6   .��6    
Database6*   �� 5      $	��400��6   ��6"   �� *
Database+5      �� 5   
   $ 
Horizontal=�6   $��3q�6.   .+�5      �� $	��100��6   .��6    	User Name6+   �� 5      $	��150��6   ��6"   �� *
UserName+�� 5      $	��100��6   .��6    
Password6*   G� 5      -   5         G�Name$ txtPassword��5   &   $	��150��6   6&   5      �� 5   
   $ 
Horizontal=�6   $��3q�6.   .+�5      �� $	��100��6   .��6    	File Name6+   �� 5      $	��350��6   $��0 0 2 0q�6"   ��63   �� *
FileName+�� 5       $��48��6   ��6    �� *BrowseCommand+.��6C   ...6H   5!      �� 5"   
   $��30��6   $ 
Horizontal=�6"   $��10q�6;   $��Right=�6G   .+�5#      �� $��75��6   $��0 0 5 0q�6    ��61   �� *TestCommand+��6Q   ��  �� ElementName$ txtPassword��.��6�   Test6�   �� 5$      $��75��6   $��0 0 5 0q�6    ��61   �� *ExecuteCommand+��6T   �� $ txtPassword��.��6�   
Transfer6�   �� 5%      $��75��6   $��10 0 5 0q�6!   ��63   �� *CloseCommand+��6T   ��  �� RelativeSource �� *FindAncestor+ �� AncestorType" �.��6�   Close6�   5&      5'      5(      