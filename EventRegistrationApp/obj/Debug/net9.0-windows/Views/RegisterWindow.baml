   M S B A M L   `   `   `  ����  EventRegistrationApp/    )EventRegistrationApp.Views.RegisterWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Rejestracja��5   	   $	��600��6   $	��500��6*    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5   	   .��5      � ���#F5F5F5 � RowDefinitions 5	   
   �� 5
      $��80 �6   �� 5      $��* �6   �� 5      $��80 �6   .+�5   
   �� $��0��6   ���#FF9800 �� CornerRadius$ 	0,0,10,10��63   ._�5      �� $%��Rejestracja Nowego Użytkownika��6   $��18(�5      $
��Bold"�6&   
�������$��Center=�5      $��Center=�5      5      �� 5   
   $��1��6   $��30,20q�6$   .��5      �� .+�5      �� $��Imię:��6   $
��Bold"�6)   $��0,10,0,5q�6;   �� 5      -   5         ��Name$ FirstNameTextBox��5      $��35��64   $��14(�6@   $
��10,5q�6N   �� 5      $��	Nazwisko:��6   $
��Bold"�6-   $��0,10,0,5q�6?   �� 5       -   5        $ LastNameTextBox��5       $��35��63   $��14(�6?   $
��10,5q�6M   �� 5"      $��Login:��6   $
��Bold"�6*   $��0,10,0,5q�6<   �� 5#      -   5        $ LoginTextBox��5#      $��35��60   $��14(�6<   $
��10,5q�6J   �� 5%      $��E-mail:��6   $
��Bold"�6+   $��0,10,0,5q�6=   �� 5&      -   5        $ EmailTextBox��5&      $��35��60   $��14(�6<   $
��10,5q�6J   �� 5(      $��Hasło:��6   $
��Bold"�6*   $��0,10,0,5q�6<   G� 5)      -   5         G�Name$ PasswordBox��5)      $��35��63   $��14(�6?   $
��10,5q�6M   �� 5+      $��Powtórz hasło:��6   $
��Bold"�62   $��0,10,0,5q�6D   G� 5,      -   5        $ ConfirmPasswordBox��5,      $��35��6:   $��14(�6F   $
��10,5q�6T   �� 5.      -   5         ��Name$ ErrorMessage��5.      
���  ��$
��Wrap=�5/      $
��0,10q�60   �� 51      -   5         ��Name$ RegisterButton��51      $��Zarejestruj��61   $��40��52      $��16(�6%   $
��Bold"�63   ���#4CAF50
�������$
��0,20q�54      6'   55      56      59      5:      