   M S B A M L   `   `   `  ����  EventRegistrationApp,    &EventRegistrationApp.Views.LoginWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$$  Logowanie - System Rejestracji��5   	   $	��650��60   $	��500��6=    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5   	   .��5      � ���#F5F5F5 � RowDefinitions 5	   
   �� 5
      $��80 �6   �� 5      $��* �6   �� 5      $��80 �6   .+�5   
   �� $��0��6   ���#2196F3 �� CornerRadius$ 	0,0,10,10��63   ._�5      �� $&�� System Rejestracji na Wydarzenia��6   $��20(�5      $
��Bold"�6&   
�������$��Center=�5      $��Center=�5      5      �� 5   
   $��1��6   $��40,30q�6"   $��Center=�61   .+�5      �� $��Login:��6   $
��Bold"�6%   $��14(�67   $��0,0,0,5q�6E   �� 5      -   5         ��Name$ LoginTextBox��5      $��40��5      $��14(�5      $
��10,8q�5       $��0,0,0,15q�5!      ���#DDD$��2q�5#      $��1��5$      �� 5'      $��Hasło:��6   $
��Bold"�6%   $��14(�67   $��0,0,0,5q�6E   G� 5(      -   5         G�Name$ PasswordBox��5(      $��40��5)      $��14(�5*      $
��10,8q�5+      $��0,0,0,15q�5,      ���#DDD$��2q�5.      $��2��5/      �� 52      -   5        $ PasswordTextBox��52      $��40��53      $��14(�54      $
��10,8q�55      $��0,0,0,15q�56      ���#DDD$��2q�58      $y�	Collapsed=�59      $��3��5:      �� 5<      -   5         ��Name$ ShowPasswordCheckBox��5<      $��Pokaż hasło��5=      $��12(�5>      $��0,0,0,20q�5?      5A      �� 5D      -   5         ��Name$ LoginButton��5D      $��ZALOGUJ SIĘ��5E      $��45��5F      $��16(�5G      $
��Bold"�5H      ���#4CAF50
�������$��0q�5K      $��	0,10,0,20q�5L      	 � Cursor$
	 Hand��5M      5N      �� 5Q      -   5        
 ��Name$
 ErrorMessage��5Q      
���  ��$��Center=�5S      $
��Wrap=�5T      $��12(�5U      $
��0,10q�5V      5W      5Z      5[      