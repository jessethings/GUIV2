   M S B A M L   `   `   `  ����>  :GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GUIGUI  5   	       GUI.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:GUI     5   	     � Title$  Fortuna Uploader��5   	   $	��598��6"   $
��1000��6/   $y�Visible=�6<    � 
ResizeMode$ NoResize=�6Q    � WindowStartupLocation$ CenterScreen=�6g   
���   � � WindowStyle$
 None=�6�    � AllowsTransparency . 
���   � � Icon$ Images/favicon.ico��6�   .��5	      �� -   5        $
��Left=�5	      $	��600��6,   $	��Top=�69   $
��1000��6Q   ��5
   
   �� 5       �� ImageSource$! Images/fgl_banner_home4.jpg��6    j� Stretch$ UniformToFill=�6C   .+�5   
   �� -   5         ��Name$ panTitleBar��5      $��40��6*   	 � Cursor$	 SizeAll��66   .+�5      �� -   5        
 ��Name$
 butClose��5      $��X��6'   $��Center=�63   $��Right=�6W   $��24��6s   $%��/GUI;component/Fonts/#Open Sans)�6~   $��15(�6�   $	��Top=�6�   
���   �$
	 Hand��6  $��0q�6  ��6�   Z� ��6�   Z� 63  �� 5      -   5        $
 butMinimise��5      $��__��6*   $��Center=�67   $��24��6[   $%��/GUI;component/Fonts/#Open Sans)�6f   $��15(�6�   
���   �$
	 Hand��6�   $��Right=�6�   $��Bottom=�6  $��
0,-24,24,0q�6&  $
��Bold"�6:  $��0q�6L  ��6�   Z� ��6�   Z� 6`  5      �� 5   
   -   5        $ panVisualBar��5      $��10��6+   $	��350��67    J� Opacity$	 0.5Q�6C   
�������$��225,60,225,0q�6d   �� 5   
   -   5        $ panLogin��5      $
��Left=�6'   $	��Top=�6B   $	��350��6Z   $	��400��6f   
�������$��	325,0,0,0q�6�    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Opacity$	 0.5Q�6#      
BlurRadius$ 50Q�61   .+�5      �� -   5         ��Name$
 Logo��5      $��90��6"   $��Images/pastedImage.png��6.   $
��Fill=�6N   $��	25,10,0,0q�6]   $	��300��6p   $
��Left=�6|   $	��Top=�6�   �� 5      -   5         ��Name$ label��5      $��Email��6#   ���	#FF151515$%��/GUI;component/Fonts/#Open Sans)�6J   $��20(�6w   $��	-8,30,0,0q�6�   $	��300��6�   $��Center=�6�   $	  ��6�   �� 5      -	   5         ��Name$ txtUsername��5       �� TextWrapping$
 Wrap=�6+   $��0q�6?   $	 IBeam��6J   4  .System.Windows.Automation.AutomationProperties   IsRequiredForForm . $%��/GUI;component/Fonts/#Open Sans)�6�   $��20(�6�   $��Center=�6�   $	��300��6�    � ToolTip$' !Enter the account's email address��6�   ���	#FF6C6C6C �� SelectionBrush �	#FF015289$��0,0,0,3q�6J   �� 	MaxLength$ 50��6d   �� MaxLines$ 1��6s  ���	#FF1515156�  �� 5      -
   5        $ 
label_Copy��5      $��Password��6(   ���	#FF151515$%��/GUI;component/Fonts/#Open Sans)�6R   $��20(�6   $��
21,15,29,0q�6�   $	��300��6�   $��Center=�6�   G� 5      -   5         G�Name$ txtPassword��5      ���	#FF6C6C6C G� SelectionBrush �	#FF015289$��0,0,0,3q�6b   $	��300��6|   $��Center=�6�   $��Center=�6�   $%��/GUI;component/Fonts/#Open Sans)�6�   $��20(�6�    G� 	MaxLength$ 50��6�   ���	#FF151515$	 IBeam��6!  $  Enter the account password��60  �� 5      -   5         ��Name$ chkRememberMe��5      $��Remember Me��6.   $	��300��6D   $��20��6P   $��0,15,0,0q�6\   $%��/GUI;component/Fonts/#Open Sans)�6n   $
	 Hand��6�   ���	#FF151515$8 2Would you like to stay logged into the application��6�   �� 5      -   5        $
 butLogin��5      $��LOGIN��6'   ���	#FF00A3E5$��0q�6N   $
	 Hand��6b   $* $I would like to make a login attempt��6p   
�������$��20(�6�   $%��/GUI;component/Fonts/#Open Sans)�6�   $	��145��6�   $��37��6�   $��	25,25,0,0q�6   J� OpacityMask �	#FF004D85$
��Left=�6`  �� 5      -   5        $
 butOpenRegisterMenu��5      $��REGISTER��62   ���	#FF00A3E5$��0q�6\   $
	 Hand��6p   $, &I would like to register a new account��6~   
�������$��20(�6�   $%��/GUI;component/Fonts/#Open Sans)�6�   $	��145��6�   $��37��6	  $��
0,-37,25,0q�6   �	#FF004D85$��Right=�6y  5      �� 5   
   -   5        $ panRegister��5      $
��Left=�6*   $	��Top=�6E   $	��350��6]   $	��400��6i   
�������$��325,-400,0,0q�6�    5         5!      $	 0.5Q�6#   $ 50Q�61   .+�5#      �� -   5        $ label1��5#      $��Email��6$   ���	#FF151515$%��/GUI;component/Fonts/#Open Sans)�6K   $��20(�6x   $��	-8,10,0,0q�6�   $	��300��6�   $��Center=�6�   $	  ��6�   �� 5$      -   5        $ txtRegEmail��5$      $
 Wrap=�6+   $��0q�6?   $	 IBeam��6J    . $%��/GUI;component/Fonts/#Open Sans)�6�   $��20(�6�   $��Center=�6�   $	��300��6�   $ Enter your email address��6�   ���	#FF6C6C6C �	#FF015289$��0,0,0,3q�6A  $ 50��6[  $ 1��6j  ���	#FF1515156w  �� 5%      -   5        $ label1_Copy��5%      $��User Nickname��6)   ���	#FF151515$%��/GUI;component/Fonts/#Open Sans)�6X   $��20(�6�   $��
21,10,29,0q�6�   $	��300��6�   $��Center=�6�   $	  ��6�   �� 5&      -   5        $ 
txtRegName��5&      $
 Wrap=�6*   $
��25,0q�6>   $	 IBeam��6L    . $%��/GUI;component/Fonts/#Open Sans)�6�   $��20(�6�   $��Center=�6�   $	��300��6�   $. (The desired nickname or your farm's name��6�   ���	#FF6C6C6C �	#FF015289$��0,0,0,3q�6S  $ 50��6m  $ 1��6|  ���	#FF1515156�  �� 5'      -   5        $ label_Copy1��5'      $��Password��6)   ���	#FF151515$%��/GUI;component/Fonts/#Open Sans)�6S   $��20(�6�   $��
21,15,29,0q�6�   $	��300��6�   $��Center=�6�   G� 5(      -   5        $ 
txtRegPass��5(      ���	#FF6C6C6C �	#FF015289$��0,0,0,3q�6a   $	��300��6{   $��Center=�6�   $��Center=�6�   $%��/GUI;component/Fonts/#Open Sans)�6�   $��20(�6�   $ 50��6�   ���	#FF151515$	 IBeam��6   $ Your desired password��6/  �� 5)      -   5        $ label_Copy2��5)      $��Confirm Password��6)   ���	#FF151515$%��/GUI;component/Fonts/#Open Sans)�6[   $��20(�6�   $��
21,15,29,0q�6�   $	��300��6�   $��Center=�6�   G� 5*      -   5        $ txtRegPassConfirm��5*      ���	#FF6C6C6C �	#FF015289$��0,0,0,3q�6h   $	��300��6�   $��Center=�6�   $��Center=�6�   $%��/GUI;component/Fonts/#Open Sans)�6�   $��20(�6�   $ 50��6  ���	#FF151515$	 IBeam��6'  $
��25,0q�66  $" Confirm the desired password��6D  �� 5+      -   5        $
 butRegister��5+      $��REGISTER��6*   ���	#FF00A3E5$��0q�6T   $
	 Hand��6h   $( "Send my registration to the server��6v   
�������$��20(�6�   $%��/GUI;component/Fonts/#Open Sans)�6�   $	��145��6�   $��37��6�   $��	25,25,0,0q�6	   �	#FF004D85$
��Left=�64  6O  �� 5,      -   5        $
 butSwitchBackToLogin��5,      $��CANCEL��63   ���	#FF00A3E5$��0q�6[   $
	 Hand��6o   $< 6Nevermind, I want to go back to the login page instead��6}   
�������$��20(�6�   $%��/GUI;component/Fonts/#Open Sans)�6�   $	��145��6  $��37��6  $��25,-37,25,0q�6$   �	#FF004D85$��Right=�6Q  6m  5-      �� 5.   
   -   5        $ 	Copyright��5.      $��Bottom=�6#   $��0,65,0,0q�6>   
�������$��Center=�6c   $	 0.5Q�6�   $%��/GUI;component/Fonts/#Open Sans)�6�   $��10(�6�   5/      50      