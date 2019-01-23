---
title: "Manage Bookmarks | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "bookmarks [SQL Server Management Studio]"
ms.assetid: 67cc3fd6-3238-4c58-a3ec-2d3b0438143a
author: MightyPen
ms.author: genemi
manager: craigg
---
# Manage Bookmarks
  While you are working in a code editor, the **Bookmarks** window allows you to create links to specific lines of code within your document. You can display this window from the **View** menu.  
  
 To create and navigate through bookmarks, click the buttons located on the **TextEditor** toolbar and at the top of the **Bookmarks** window. You can add and remove bookmarks, activate or disable bookmarks, and organize bookmarks into folders. Certain commands are also available from the **Bookmarks** window shortcut menu. To add or remove a bookmark, place the insertion point on the desired line in the Editor, and then click **Toggle a bookmark**. To activate a bookmark, select its check box in the **Bookmarks** window; to disable (but not remove) a bookmark, clear its check box.  
  
## Text Editor Toolbar  
 The following buttons are enabled on the **Text Editor** toolbar when a text document is opened in the Editor. To display the **Text Editor** toolbar when you are in the Query Editor, on the **View** menu, point to **Toolbars**, and then click **Text Editor**.  
  
 **Toggle a bookmark on the current line**  
 Adds or removes a bookmark on the selected line of the document in the active Editor. Does not alter the line of code bookmarked.  
  
 **Move the caret to previous bookmark**  
 Selects the previous bookmark that is enabled in the **Bookmarks** window. When the first bookmark is reached, jumps ahead to the last one. As needed, opens the file where the selected bookmark occurs in the Editor. Scrolls that document to the bookmarked line, and places the insertion point there.  
  
 **Move the caret to the next bookmark**  
 Selects the next bookmark that is enabled in the **Bookmarks** window. When the last bookmark is reached, jumps back to the first one. As needed, opens the file where the selected bookmark occurs in the Editor. Scrolls that document to the bookmarked line, and places the insertion point there.  
  
 **Clear all bookmarks in the current document**  
 Displays a confirmation message, and then removes all bookmarks from the active document. Does not remove the lines of code that were bookmarked.  
  
> [!CAUTION]  
>  You cannot undo this procedure. Afterward, you must use **Toggle a bookmark on the current line** to create new bookmarks. To disable bookmarks but not remove them, clear their check boxes in the **Bookmarks** window.  
  
## Bookmarks Window  
 To organize bookmarks, create bookmark folders in the **Bookmarks** window. Drag and drop bookmarks into the folders. The following buttons are available at the top of the **Bookmarks** window.  
  
 **Toggle a bookmark on the current line.**  
 Adds or removes a bookmark on the selected line of the document in the active Editor. Does not alter the line of code bookmarked.  
  
 **New folder**  
 Adds a folder to the **Bookmarks** window.  
  
> [!TIP]  
>  In a lengthy code file, it can be helpful to organize bookmarks into task-related folders. Selecting a folder enables the **Go to previous bookmark in folder** and **Go to next bookmark in folder** buttons.  
  
 **Move the caret to previous bookmark**  
 Selects the previous bookmark that is enabled in the **Bookmarks** window. When the first bookmark is reached, jumps ahead to the last one. As needed, opens the file where the selected bookmark occurs in the Editor. Scrolls that document to the bookmarked line, and places the insertion point there.  
  
 **Move the caret to the next bookmark**  
 Selects the next bookmark that is enabled in the **Bookmarks** window. When the last bookmark is reached, jumps back to the first one. As needed, opens the file where the selected bookmark occurs in the Editor. Scrolls that document to the bookmarked line, and places the insertion point there.  
  
 **Move the caret to previous bookmark in the current folder**  
 Selects the previous bookmark that is enabled within the same folder in the **Bookmarks** window. When the first bookmark is reached, jumps ahead to the last one in that folder. As needed, opens the file where the selected bookmark occurs in the Editor. Scrolls that document to the bookmarked line, and places the insertion point there.  
  
 **Move the caret to next bookmark in the current folder**  
 Selects the next bookmark that is enabled within the same folder in the **Bookmarks** window. When the first bookmark is reached, jumps back to the first one in that folder. As needed, opens the file where the selected bookmark occurs in the Editor. Scrolls that document to the bookmarked line, and places the insertion point there.  
  
 **Disable/Enable All Bookmarks**  
 Clears or enables the check boxes for all bookmarks in the **Bookmarks** window. Does not remove bookmarks, or alter the lines of code that they mark.  
  
 **Delete**  
 Removes the currently selected bookmark from the **Bookmarks** window and from the document where the bookmark occurred. Does not remove the line of code that was bookmarked.  
  
 Bookmark check boxes  
 Each bookmark has its own check box. To activate an existing bookmark, select its check box in the **Bookmarks** window. To hide (but not remove) an existing bookmark, clear its check box in the **Bookmarks** window.  
  
## Bookmarks Window Shortcut Menu  
 When you right-click on an entry in the **Bookmarks** window, the following commands are available from the shortcut menu.  
  
 **Delete**  
 Removes the currently selected bookmark from the **Bookmarks** window and from the document where the bookmark occurred. Does not remove the line of code that was bookmarked.  
  
 **Rename**  
 Allows you to assign a new display name to a bookmark or folder.  
  
 **Disable/Enable Bookmark**  
 Clears or enables the check box for the selected bookmark in the **Bookmarks** window. Does not remove the bookmark, or alter the line of code that it marks.  
  
 **Disable/Enable All Bookmarks**  
 Clears or enables the check boxes for all bookmarks in the **Bookmarks** window. Does not remove bookmarks, or alter the lines of code that they mark.  
  
## See Also  
 [SQL Server Management Studio Keyboard Shortcuts](../../ssms/sql-server-management-studio-keyboard-shortcuts.md)  
