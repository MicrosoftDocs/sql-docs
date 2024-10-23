---
title: "New GUI features in SSMA for Db2 (Db2ToSQL)"
description: Discover new features of the SQL Server Migration Assistant (SSMA) user interface.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
---
# New GUI features in SSMA for Db2 (Db2ToSQL)

This article describes new features of the SQL Server Migration Assistant (SSMA) user interface.

## Layouts

This feature allows you to choose one of two predefined windows layout or create you own layout. To access the layout submenu, navigate to **View** > **Layouts**. There you can choose one of the existing layouts, add new layout or manage layouts.

### Add Current Layout

To save the current layout, navigate to **View** > **Layouts**, then select **Add Current Layout**.

### Choose predefined layout

To choose one of the predefined layouts, navigate to **View** > **Layouts**, then select **Default Layout** or **Without Explorers**. You can also use shortcuts **Ctrl**+**Alt**+`1` or **Ctrl**+**Alt**+`2` for predefined layouts.

### Choose user-defined layout

To choose user-defined layout, navigate to **View** > **Layouts**, then select one of the user-defined layouts. You can also use shortcuts defined for the layouts.

### Manage layouts

To open the **Manage Layouts** dialog, navigate to **View** > **Layouts** > **Manage Layouts**. In this dialog, you see a list of the existing layouts on the left side of the dialog. There you can select the layout to change its settings. Also you can change layouts order in the list or delete the layout using buttons on the top of the list. On the right side of the dialog, you can change the following layout settings:

- Layout name
- Synchronization of metadata explorers
- Visibility and width of the source and target metadata explorers
- Visibility of the source or target windows and their sizes
- Visibility and height of auxiliary windows

## Bookmarks

This feature allows you to set one or more bookmarks in the source or target code, quick found a bookmark by using shortcuts, manage the bookmarks with a friendly dialog.

### Toggle bookmark

You can set/remove a bookmark in the following ways:

- Use button **Toggle Bookmark** on top of source or target SQL window
- Select the gray area on the left of the SQL window
- Use **Ctrl**+**Shift**+&lt;0..9&gt; to set a numbered bookmark

### Bookmark navigation

You can walk through the bookmarks in the following ways:

- Use buttons **Next Bookmark** and **Previous Bookmark** on the top of the SQL window
- Use **Ctrl**+&lt;0..9&gt; to find a numbered bookmark
- Use buttons **Go To** or **View Source** in the Manage Bookmarks dialog

### Remove bookmark

You can remove a bookmark in the following ways:

- Use button **Clear** on the top of the SQL window to remove all bookmarks in the current document
- Use buttons **Remove** or **Remove All** in Manage Bookmarks dialog

### Manage bookmarks

To open the Manage Bookmarks dialog, navigate to **Edit** > **Manage Bookmarks**. In this dialog, you find a list of existing bookmarks. You can use the buttons on the right side of the dialog to manage the bookmarks.

## Object history

GUI object history provides you with the following advantages when you navigate objects:

- You can use **Go Back** and **Go Forward** buttons to navigate the objects you already visited

- When you go back to the object, you go back to the same tab that you left

- When you go back to the object and the tab is SQL, you go back to the same cursor position that you left

## Advanced search capabilities

Advanced search capabilities provide the powerful and flexible searching features that let you find object declaration, get object information, perform a quick search, perform advanced object searching in categories using patterns, and so on.

### Get quick information

You can get quick information on the object at the cursor position in the following ways:

- Select button **Quick Info** on the top of SQL window
- Select **Quick Info** in the right-click context menu
- Press **Ctrl**+**Shift**+**Space**

### Find declaration

You can go to the declaration of the object at cursor position in the following ways:

- Select button **Go To Declaration** on top of the SQL window
- Select **Go To Declaration** in the right-click context menu
- Press **F12**

### Quick search

You can perform quick text search using the following features:

- Search by using the **Ctrl**+**F** shortcut
- Repeat last search forward by using **F3**
- Repeat last search backward by using **Shift**+**F3**
- Find next occurrence of the word at the cursor position by using **Ctrl**+**F3**
- Find previous occurrence of the word at the cursor position by using **Ctrl**+**Shift**+**F3**
- Perform all these actions with menu items.

### Advanced search

To open Advanced Search dialog, navigate to **Edit** > **Find**, then select **Advanced Search**. In this dialog, you can find any object using a pattern. On the top of the dialog, you can choose search area and object categories.
