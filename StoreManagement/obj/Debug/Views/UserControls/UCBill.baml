   M S B A M L   `   `   `  ????J  FStoreManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:StoreManagement.Views.UserControls"StoreManagement.Views.UserControls  5      /    )StoreManagement.Views.UserControls.UCBill   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Blocal0clr-namespace:StoreManagement.Views.UserControls     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      V RMaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5         MaterialDesignBody#???   5	      $??Regular"?5
      $??13(?5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=?5         TextRenderingMode$
 Auto=?5      $??Calibri)?5        MaterialDesignPaper#???  5        UCBillVM ? DataContext# [ 5       ? 	Resources 5       ?? 
TargetType  	BoldStyle ?? Property ?? Value  TitileStyle  NormalStyle??%  &	       &	 ?     &	 F    ?? 5   
   " ??6   .?5      ??  ? ??$ 30(?5      ?? 5       ? ??$ Calibri)?5      ?? 5       ? ??$
 Bold"?5      5      ?? 5   
   " ??6   .?5       ??  ? ??$ 20(?5!      ?? 5"       ? ??$ Calibri)?5#      ?? 5$       ? ??$
 Bold"?5%      5&      ?? 5'   
   " ??6   .?5)      ??  ? ??$ 18(?5*      ?? 5+       ? ??$ Calibri)?5,      ?? 5-       ? ??$
 Bold"?5.      5/      50      .  (System.Windows.Interactivity.Interaction   Triggers 51      /  )System.Windows.Interactivity.EventTrigger  52   
      	EventName$ Loaded??6   .  (System.Windows.Interactivity.TriggerBase	   Actions.	 53      6  0System.Windows.Interactivity.InvokeCommandAction  
   Command
 6$   ?? *LoadCommand+54      .??57      ?  ? RowDefinitions 58   
   ?? 59      $
??auto ?6   6,   ?? 5:      $
??auto ?6   6,   ?? 5;      6   .+?5=   
   ? $??0??6   $??48??5>      ???#ff5555.+?5@      ?? $??Danh sách hóa đơn??6   
???????$??Center=?5B      $??Center=?5C      #??[ 5D      5F      #  MaterialDesignThemes.Wpf.Card  5H   
   $??1??6   $??5q?5I      
???????.??5K      ? $??0 0 0 10q?6   .+?5L      ?? $??
Từ ngày??6   $??Center=?5M      $	??100??5N      $??	10 10 0 0q?5O      $??20(?5P      $
??Lato)?5Q      $
??Bold"?5R      6,   (  "System.Windows.Controls.DatePicker  5S      $??Center=?5T      $	??100??5U      $??10q?5V      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Ngày bắt đầu??5W       ( $MaterialDesignFloatingHintDatePicker#??[ 5X         SelectedDate 5S      ?? *	StartDate+ ?? StringFormat$ 
dd/MM/yyyy??5X      ?? 5Y      $?? đến ngày ??6   $??Center=?5Z      $	??120??5[      $??	10 10 0 0q?5\      $??20(?5]      $
??Lato)?5^      $
??Bold"?5_      6,     5`      $??Center=?5a      $	??100??5b      $??10q?5c      $ Ngày kết thúc??5d      #??[ 5e       5`      ?? *	EndDate+$ 
dd/MM/yyyy??5e      ?? 5f      -   5        $??CashierLabel??5f      $??Mã nhân viên??5g      $??Center=?5h      $	??150??5i      $??	10 10 0 0q?5j      $??20(?5k      $
??Lato)?5l      $
??Bold"?5m      y?5n      ?? *IDVisibility+6>   ?? 5o      
???   ?$ 
Tất cả??5s      $??Center=?5t      ??5o      ?? *	ListUsers+y?5q      ?? *ListUsersVisibility+ ?? SelectedItem 5r      ?? *UserSelectedItem+??5u      ?? 5v      .R?5w      ?? ??6(   ?? *DisplayName+5x       5z        5{      $ SelectionChanged??6)   .	 5|        
 64   ?? *UserSelectionChanged+5}      5      ?? 5?      $??Center=?5?      $??Tìm kiếm??5?      $	??100??5?      $??20 10 10 10q?5?      ??5?      ?? *FilterCommand+5?   0   5?      5?        5?   
   $??2??6   $??5q?5?      $??Stretch=?5?      .??5?      ]? .+?5?      ?? ??6   ?? *	ListBills+ 5?      ?? *SelectedItem+??5?      ?? 5?      " ??6     ?? Triggers 5?      P? 5?   "    P? Property ?@J?IsKeyboardFocusWithin P? Value . . ?5?   &   ??  ?@??
IsSelected . 5?   -   5?   #   5?       ?? View 5?      ?? 5?      .P?5?      ?? $??ID??6-    ?? CellTemplate 5?   "   ?? 5?   &   .R?5?   *   ?? $??Center=?5?   4   ??5?   4   ?? *BillID+5?   4   5?   '   5?      ?? 5?      $??Ngày??6-    5?   "   ?? 5?   &   .R?5?   *   ?? $??Center=?5?   4   $
??Wrap=?5?   4   ??5?   4   ?? *
BillDate+5?   4   5?   '   5?      ?? 5?      $??Số lượng??6-    5?   "   ?? 5?   &   .R?5?   *   ?? $??Center=?5?   4   ??5?   4   ?? *BillDetails.Count+5?   4   5?   '   5?      ?? 5?      $??Tổng??6-    5?   "   ?? 5?   &   .R?5?   *   ?? $??Center=?5?   4   $
??Wrap=?5?   4   ??5?   4   ?? *
TotalPrice+$	 0,0??5?   4   5?   '   5?      ?? 5?      $??Khuyến mãi??6-    5?   "   ?? 5?   &   .R?5?   *   ?? $??Center=?5?   4   ??5?   4   ?? *CodePromotion.CodePercent+5?   4   5?   '   5?      ?? 5?      $??Tổng tiền??6-    5?   "   ?? 5?   &   .R?5?   *   ?? $??Center=?5?   4   $
??Wrap=?5?   4   ??5?   4   ?? *LastTotalPrice+$	 0,0??5?   4   5?   '   5?      ?? 5?      $??Tên nhân viên??6-    5?   "   ?? 5?   &   .R?5?   *   ?? $??Center=?5?   4   ??5?   4   ?? *User.DisplayName+5?   4   5?   '   5?      ?? 5?       5?   "   ?? 5?   &   .R?5?   *   ?? ???	#FF4F6BFF$??2q?5?   1   $??40 10 10 10q?5?   1   ???	#FF4F6BFF ? ToolTip$ Xem chi tiết??5?   1   $??40??5?   1   $
??Auto??5?   1   ??5?   1   ??  ?? Path$' !DataContext.ViewBillDetailCommand? ?? RelativeSource ?? *FindAncestor+ ?? AncestorType" ????5?   1   ?? .??5   .   ??  ?? Orientation$ 
Horizontal=?69   .+?5  2   '	  !MaterialDesignThemes.Wpf.PackIcon	  ?
  ?ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=null]] 
  Kind$
 Cart=?6J   
???????$??25??5  J   $??25??5  J   ?? 5  2   $??Xem chi tiết??6<   $??5 0 0 0q?5  <   
???????$??Center=?5  <   $??15(?5	  <   5  /   5  +   5  '   5     5     5     5     5     5     5     