   M S B A M L   `   `   `  ????J  FStoreManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:StoreManagement.Views.UserControls"StoreManagement.Views.UserControls  5      7    1StoreManagement.Views.UserControls.UCBrandManager   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Blocal0clr-namespace:StoreManagement.Views.UserControls     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      V RMaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5         MaterialDesignBody#???   5	      $??Regular"?5
      $??13(?5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=?5         TextRenderingMode$
 Auto=?5      $??Calibri)?5        MaterialDesignPaper#???  5        UCBrandManagerVM ? DataContext# [ 5      .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded??6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   ?? *LoadCommand+5       ? 	Resources 5       ?? 
TargetType  	BoldStyle	 ?? Property
 ?? Value  TitileStyle  NormalStyle??%  &	       &	 ?     &	 F    ?? 5   
   " ??6   .?5      ?? 	 ? ??$
 30(?5      ?? 5      	 ? ??$
 Calibri)?5      ?? 5       	 ? ??$

 Bold"?5!      5"      ?? 5#   
   " ??6   .?5%      ?? 	 ? ??$
 20(?5&      ?? 5'      	 ? ??$
 Calibri)?5(      ?? 5)      	 ? ??$

 Bold"?5*      5+      ?? 5,   
   " ??6   .?5.      ?? 	 ? ??$
 18(?5/      ?? 50      	 ? ??$
 Calibri)?51      ?? 52      	 ? ??$

 Bold"?53      54      55      .??56      ?  ? RowDefinitions 57   
   ?? 58      $
??auto ?6   6,   ?? 59      $
??auto ?6   6,   ?? 5:      $
??auto ?6   6,   ?? 5;      6   .+?5=   
   ? $??0??6   $??48??5>      ???#ff5555$??5q?5@      .+?5A      ?? $ ??Quản lý Thương hiệu??6   
???????$??Center=?5C      $??Center=?5D      #??[ 5E      5F      #  MaterialDesignThemes.Wpf.Card  5G   
   $??1??6   $??5q?5H      .??5I      ?  ? ColumnDefinitions 5J      ?? 5K      $
??Auto ?6'   66   ?? 5L      6)   .+?5O      ??  ?? Orientation$ 
Horizontal=?6   $??0??5P      $??0 0 0 10q?5Q      .+?5R      ?? $ Vertical=?6!   $??20q?5S   !   .+?5T      ]? ??.  .+?5U      ?? $??Mã thương hiệu ??6(   $??Center=?5V   (   $
??Left=?5W   (   ?? 5X      $	??150??6&   $??5 0 15 5q?5Z   &   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Mã thương hiệu??5[   &    % !MaterialDesignFloatingHintTextBox#??[ 5\   &    ?? 
IsReadOnly . $??Right=?5^   &   ??5Y   &   ?? *	BrandID+5^   &   5_      5a      ?? 5b      $ Vertical=?6!   $??20q?5c   !   .+?5d      ]? ??.  .+?5e      ?? $??Tên thương hiệu ??6(   $??Center=?5f   (   $
??Left=?5g   (   ?? 5h      $	??150??6&   $??5 0 15 5q?5j   &   $ Tên thương hiệu??5k   &   #??[ 5l   &   $??Right=?5m   &   ??5i   &   ?? *	BrandName+5m   &   5n      5o      5q      5s      5u        5w   
   $??2??6   $??5q?5x      
???????.??5z      ? $??0 0 0 10q?6   .+?5{      ?? $??Center=?5|      $??Thêm??5}      $	??100??5~      $??20 10 10 10q?5      ??5{      ?? *
AddCommand+5   0   ?? 5?      $??Center=?5?      $??Sửa??5?      $	??100??5?      $??20 10 10 10q?5?      ??5?      ?? *UpdateCommand+5?   0   ?? 5?      $??Center=?5?      $
??Xóa??5?      $	??100??5?      $??20 10 10 10q?5?      ??5?      ?? *DeleteCommand+5?   0   5?      5?        5?   
   $??3??6   $??Stretch=?5?      $??5q?5?      .??5?      ]? .+?5?      ?? ??6   ?? *
ListBrands+ ?? SelectedItem 5?      ?? *BrandSelectedItem+ 5?        5?      $ SelectionChanged??6)   . 5?         64   ?? *BrandSelectionChanged+5?       ?? View 5?      ?? 5?      .P?5?      ?? $	??Mã??6-    ?? CellTemplate 5?   "   ?? 5?   &   .R?5?   *   ?? $??Center=?5?   4   ??5?   4   ?? *	BrandID+5?   4   5?   '   5?      ?? 5?      $??Tên thương hiệu??6-    5?   "   ?? 5?   &   .R?5?   *   ?? $??Center=?5?   4   $
??Wrap=?5?   4   ??5?   4   ?? *	BrandName+5?   4   5?   '   5?      5?      5?      5?      5?      5?      5?      