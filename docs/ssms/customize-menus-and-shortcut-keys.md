---
title: "Customize Menus and Shortcut Keys | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Management Studio [SQL Server], shortcuts"
  - "keyboard shortcuts [SQL Server Management Studio]"
  - "menu shortcuts [SQL Server Management Studio]"
  - "shortcuts [SQL Server Management Studio], customizing"
  - "hot keys [SQL Server Management Studio]"
  - "keyboard shortcuts [SQL Server Management Studio], customizing"
  - "customizing shortcut keys [SQL Server]"
  - "customizing menus [SQL Server]"
  - "accelerator keys"
ms.assetid: fb4edf3c-71b6-4645-b1d1-ddfdd69f0d7b
author: "markingmyname"
ms.author: "maghan"
manager: craigg
---
# Customize Menus and Shortcut Keys
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
A keyboard accelerator allows you to select a menu command or button by pressing ALT+*\<single letter>*. For example, to open the **Edit** menu, press ALT+E. You can rearrange and modify toolbar buttons, menus, and menu commands by using the **Customize** dialog box. Instructions are provided for changing the settings using the mouse and using only the keyboard.  
  
Keyboard accelerators for stored procedures using the Ctrl key can be created from the **Keyboard** page of the **Tools**/**Options** dialog box.  
  
> [!NOTE]  
> Click **Collapse All** at the top of this page to show only the headings.  
  
## Opening the Keyboard Accelerator Dialog Box Using the Mouse  
  
#### To access the dialog box for assigning or changing a keyboard accelerator (using the mouse)  
  
1.  On the **Tools** menu, click **Customize**.  
  
2.  Make sure the toolbar you want to change is visible.  
  
    1.  In the **Customize** dialog box, click the **Toolbars** tab.  
  
    2.  Select the check box for the toolbar you want to display.  
  
3.  In the **Customize** dialog box, click the **Commands** tab.  
  
## Changing a Toolbar Buttons Accelerator Key Using the Mouse  
  
#### To assign or change a toolbar button's keyboard accelerator (using the mouse)  
  
1.  Click the button on the toolbar.  
  
2.  In the **Customize** dialog box, on the **Commands** tab, click **Modify Selection**.  
  
3.  In the **Name** box on the shortcut menu, type a name for the toolbar button with an ampersand (&) before the letter that you want as the keyboard accelerator.  
  
4.  Press **ENTER**.  
  
5.  In the **Customize** dialog box, click **Close**.  
  
## Changing a Menu Commands Accelerator Key Using the Mouse  
  
#### To assign or change a menu command's keyboard accelerator (using the mouse)  
  
1.  Click the menu name on the menu bar or toolbar.  
  
2.  Click the menu command.  
  
3.  In the **Customize** dialog box, click **Modify Selection**.  
  
4.  In the **Name** box on the shortcut menu, type a name for the menu command with an ampersand (&) before the letter that you want as the keyboard accelerator.  
  
5.  Press **ENTER**.  
  
6.  In the **Customize** dialog box, click **Close**.  
  
## Opening the Keyboard Accelerator Dialog Box Using the Keyboard  
  
#### To access the dialog box for assigning or changing a keyboard accelerator (using the keyboard)  
  
1.  Press ALT+T, then type C, to open the **Customize** dialog box.  
  
2.  Make sure the toolbar you want to change is visible.  
  
    1.  In the **Customize** dialog box, press ALT+B to show the **Toolbars** tab.  
  
    2.  Use the arrow keys to select the toolbar you want to display, then **SPACE** to select the check box.  
  
3.  In the **Customize** dialog box, press ALT+C to display the **Commands** tab.  
  
## Changing a Toolbar Buttons Accelerator Key Using the Keyboard  
  
#### To assign or change a toolbar button's keyboard accelerator (using the keyboard)  
  
1.  Press ALT+R to display the **Rearrange Commands** dialog box.  
  
2.  In the **Rearrange Commands** dialog box, use the arrow keys to select **Toolbar**.  
  
3.  Tab to the **Toolbar** list, and use the arrow keys to select the toolbar that contains the button you want to change, and then press **ENTER**.  
  
4.  Tab to the **Controls** list, and use the arrow keys to select the button you want to change.  
  
5.  Press **ALT+M**, to select **Modify Selection**.  
  
6.  Tab to the **Name** box on the shortcut menu, type a name for the toolbar button with an ampersand (&) before the letter that you want as the keyboard accelerator.  
  
7.  Press **ENTER**.  
  
8.  Tab to the **Close** button, and then press **ENTER**.  
  
## Changing a Menu Commands Accelerator Key Using the Keyboard  
  
#### To assign or change a menu command's keyboard accelerator (using the keyboard)  
  
1.  Press ALT+R to display the **Rearrange Commands** dialog box.  
  
2.  Tab to **Menu Bar** and then use the arrow keys to click the menu you want in the **Menu Bar** list, and then press **ENTER**.  
  
3.  Tab to the **Controls** list, and use the arrow keys to select the button you want to change.  
  
4.  Press ALT+M, to select **Modify Selection**.  
  
5.  Tab to the **Name** box on the shortcut menu, and type a name for the toolbar button with an ampersand (&) before the letter that you want as the keyboard accelerator.  
  
6.  Press **ENTER**.  
  
7.  In the **Customize** dialog box, click **Close**.  
  
## Creating a Keyboard Accelerator for a Stored Procedure  
  
#### To create a keyboard accelerator for a stored procedure  
  
1.  On the **Tools** menu, click **Options**.  
  
2.  On the **Keyboard** page, select an unused keyboard combination in the **Shortcut** list.  
  
3.  In the **Stored Procedure** box, type the stored procedure name, and then click **OK**.  
  
## Adding a New Item to the Menu  
  
#### To add a new item to the menu  
  
1.  On the **Tools** menu, click **Options**.  
  
2.  In the **Customize** dialog box, on the **Commands** tab, click **New Menu**.  
  
3.  On the **Commands** box, drag **New Menu** to the menu bar and drop it where you want the new menu to appear.  
  
4.  On the menu, right-click **New Menu**, and in the **Name** box, type a name for the new menu.  
  
5.  In the **Customize** dialog box, select category such as **File**, and select a command such as **Open File**. Drag the command to the new menu. As you point to the new menu, the menu will expand. Drop the command onto the expanded menu.  
  
6.  In the **Customize** dialog box, click **Close**.  
  
> [!NOTE]  
> Some commands are available only when [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] is displaying relevant content. If no commands on the menu are available, the menu item will not be available.  
  
## See Also  
[Features in SQL Server Management Studio](../ssms/features-in-sql-server-management-studio.md)  
  
