   M S B A M L   `   `   `  ����  EventRegistrationApp/    )EventRegistrationApp.Views.UserMainWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$,  &Panel Uzytkownika - System Rejestracji��5   	   $	��700��68   $	��900��6E    � WindowStartupLocation$ CenterScreen=�5   	   .��5      � ���#F5F5F5 � RowDefinitions 5   
   �� 5	      $��80 �6   �� 5
      $��* �6   �� 5      $��60 �6   .+�5   
   �� $��0��6   ���#4CAF50 �� CornerRadius$ 	0,0,10,10��63   ._�5      �  � ColumnDefinitions 5      �� 5      $��* �6'   �� 5      $
��Auto �6'   .+�5      �� $��0��6   $��Center=�6-   $
��20,0q�6H   .+�5      �� -   5         ��Name$ WelcomeText��5       $��18(�65   $
��Bold"�6C   
�������$��Witaj, Uzytkownik!��5   3   �� 5      $��Panel Uzytkownika��6    $��14(�69   
������� J� Opacity$	 0.8Q�5   3   5      �� 5      -   5         ��Name$ LogoutButton��5   )   $��1��6   $��Wyloguj��6?   ���#F44336
�������$��0q�5      $��20,10q�6-   $
��20,0q�6=   $
��Bold"�5       6+   5!      5"      �� 5%   
   $��1��6   $��20q�6"   
�������$��0q�5&       �� TabStripPlacement$	 Top=�6)   .@�5)      �� $��📝 Zapis na Wydarzenie��6   $��14(�66   $
��Bold"�6D   .��5*      �� $
��Auto=�6   $��20q�6B   .��5+      �� $	��600��6!   $��Center=�60   .+�5-      �� $#��Zarejestruj sie na wydarzenie��6$   $��24(�5.   $   $
��Bold"�62   $��	0,20,0,30q�5/   $   ���#333$��Center=�50   $   �� 53      $��Wybierz wydarzenie:��6$   $
��Bold"�54   $   $��14(�66   $��0,10,0,8q�55   $   �� 57      -   5        	 ��Name$	 EventComboBox��57   #   $��40��58   #   $��14(�6/   $��0,0,0,20q�59   #   
����������#DDD$��2q�5<   #   
 K� ZIndex$

 1000��5=   #   5>   #   �� 5A      $��Szczegoly wydarzenia:��6$   $
��Bold"�5B   $   $��14(�66   $��0,10,0,8q�5C   $   �� 5E      ���#DDD$��1q�64   $ 5��5F   !   $��80��62   $	��120��6A   $��0,0,0,15q�5G   !   ._�5H      �� $
��Auto=�6+   .��5I   "   �� -   5        $ AgendaTextBlock��5I   ,   
������  �� Padding$ 15q�5K   ,   $
��Wrap=�5L   ,   $��12(�5M   ,   $/��)Wybierz wydarzenie aby zobaczyc szczegoly��5N   ,   5O      5P      �� 5R      $��Data wydarzenia:��6$   $
��Bold"�5S   $   $��14(�66   $��0,5,0,5q�5T   $   �� 5U      -   5        $ EventDateTextBlock��5U   $   $��14(�5V   $   ���#666$��-��5W   $   $��0,0,0,20q�6-   �� 5Y      $��Typ uczestnictwa:��6$   $
��Bold"�5Z   $   $��14(�66   $��0,10,0,8q�5[   $   �� 5]      -   5        $	 ParticipationTypeComboBox��5]   #   $��40��5^   #   $��14(�6/   $��0,0,0,20q�5_   #   
����������#DDD$��2q�5b   #   $	
 999��5c   #    �� SelectedIndex$ 0��5d   #   .@�5e      �� $��Sluchacz��6+   �� 5f      $��Autor��6+   �� 5g      $��Sponsor��6+   �� 5h      $��Organizator��6+   5i      �� 5l      $��Preferencje zywieniowe:��6$   $
��Bold"�5m   $   $��14(�66   $��0,10,0,8q�5n   $   �� 5p      -   5        $	 FoodPreferenceComboBox��5p   #   $��40��5q   #   $��14(�6/   $��0,0,0,30q�5r   #   
����������#DDD$��2q�5u   #   $	
 998��5v   #   $ 0��5w   #   .@�5x      �� $��Bez preferencji��6+   �� 5y      $��Wegetarianskie��6+   �� 5z      $��Bez glutenu��6+   5{      �� 5}      -   5        $ RegistrationMessage��5}   $   
���  ��$��14(�5~   5   $
��Wrap=�5   $   $��Center=�5�   $   $��0,0,0,20q�5�   $   $�� ��5�   $   �� 5�      -	   5        $ RegisterForEventButton��5�   !   $(��"🎯 ZAREJESTRUJ SIE NA WYDARZENIE��5�   !   $��55��5�   !   $��18(�6-   $
��Bold"�6;   ���#2196F3
�������$��0q�5�   !   $��	0,10,0,30q�5�   !   $��Stretch=�5�   !   5�   !   �� 5�      $a��[Po wypelnieniu wszystkich pol kliknij przycisk powyzej aby zarejestrowac sie na wydarzenie.��6$   $��12(�5�   $   $��Italic$�62   ���#666$
��Wrap=�5�   6   $��Center=�5�   $   $��0,0,0,20q�5�   $   5�      5�      5�      �� 5�      -
   5         ��Name$ MyRegistrationsTab��5�      $��📋 Moje Zgłoszenia��62   $��14(�6N   $
��Bold"�6\   .��5�      � $��20q�6    5�      �� 5�      $
��Auto �6(   �� 5�      $��* �6(   .+�5�      �� $��0��6    $$��Moje zgłoszenia na wydarzenia��6-   $��20(�5�       $
��Bold"�6.   $��	0,20,0,20q�5�       ���#333$��Center=�5�       �� 5�      $��1��6   ���#DDD$��1q�6=   $ 5��6Q   ._�5�      �� .+�5�      &   System.Windows.Controls.DataGrid  -   5          Name$ MyRegistrationsDataGrid��5�   '      AutoGenerateColumns$ False��5�   '      
IsReadOnly$
 True��6C      AlternatingRowBackground �#F9F9F9   GridLinesVisibility$ 
Horizontal=�5�   '      HeadersVisibility$ Column=�5�   '   $	��300��5�   '      Columns 5�   "   0  *System.Windows.Controls.DataGridTextColumn  5�   &   ,  &System.Windows.Controls.DataGridColumn   Header$ 
Wydarzenie��69      Width5  /System.Windows.Controls.DataGridLengthConverter$	 200 6k   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6M   �� *	EventName+6k     5�   &   $ Typ uczestnictwa��69   $	 150 6y    6S   �� *ParticipationType+6y     5�   &   $ Preferencje zywieniowe��69   $	 150 6|    6Y   �� *FoodPreference+6|     5�   &   $ Status��69   $	 100 6d    6I   �� *Status+6d     5�   &   $ Data zgłoszenia��69   $	 150 6w    6R   �� *RegistrationDate+6w   5�      �� 5�      $d��^Brak zgłoszen na wydarzenia. Przejdz do zakladki 'Zapis na Wydarzenie' aby sie zarejestrowac.��6(   $��Italic$�5�   (   ���#999$��Center=�5�   (   $��20q�5�   (   $��14(�64   5�      5�      5�      5�      5�      5�      5�      