---
title: "New GUI Features in SSMA for Access to SQL"
description: "New GUI Features in SSMA for Access to SQL"
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: concept-article
ms.collection:
  - sql-migration-content
---
# New GUI features in SSMA (AccessToSQL)

This section describes new features of the SSMA for Access user interface.

## Layouts

This feature enables you to choose one of two predefined window layouts or create your own layout. To access the layout submenu, on the **View** menu point to **Layouts**. There you can choose one of the existing layouts, add the current layout, or manage layouts.

### Add current layout

To save the current layout, on the **View** menu point to **Layouts**, and then select **Add Current Layout**.

### Choose predefined layout

To choose one of the predefined layouts, on the **View** menu point to **Layouts**, and then select **Default Layout** or **Without Explorers**. You can also use shortcuts **Ctrl**+**Alt**+**1** or **Ctrl**+**Alt**+**2** for predefined layouts respectively.

### Choose user-defined layout

To choose user-defined layout, on the **View** menu point to **Layouts**, then select one of the user-defined layouts. You can also use shortcuts defined for the layouts.

### Manage layouts

To open **Manage Layouts** dialog, on the **View** menu point to **Layouts** and select **Manage Layouts**. In the **Manage Layouts** dialog you find a list of the existing layouts on the left side of the dialog. There you can select the layout to change its settings. Also you can change layouts order in the list or delete the layout using buttons on the top of the list. On the right side of the dialog you can change the following layout settings:

- Layout name
- Synchronization of metadata explorers
- Visibility and width of the source and target metadata explorers
- Visibility of the source or target windows and their sizes
- Visibility and height of auxiliary windows

## Bookmarks

This feature enables you to set one or more bookmarks in the source or target code. You can quickly find a bookmark by using shortcuts and manage the bookmarks through an easy-to-use dialog.

### Toggle bookmark

Set or remove a bookmark in the following ways:

- Use the **Toggle Bookmark** button at the top of the source or target SQL window.
- Select the gray area to the left of the SQL window.
- Use **Ctrl**+**Shift**+**0** through **9** to set a numbered bookmark.

### Bookmark navigation

Move through the bookmarks in the following ways:

- Use the **Next Bookmark** and **Previous Bookmark** buttons at the top of the SQL window.
- Use **Ctrl**+**0** through **9** to find a numbered bookmark.
- Use the **Go To** or **View Source** buttons in the **Manage Bookmarks** dialog.

### Remove bookmark

Remove a bookmark in the following ways:

- Use the **Clear** button at the top of the SQL window to remove all bookmarks in the current document.
- Use the **Remove** or **Remove All** buttons in the **Manage Bookmarks** dialog.

### Manage bookmarks

To open the **Manage Bookmarks** dialog, select **Manage Bookmarks** on the **Edit** menu. In the dialog, you see a list of existing bookmarks. Use the buttons on the right side of the dialog to manage the bookmarks.

## Object History

GUI Object History provides the following advantages when you navigate objects:

- Use **Go Back** and **Go Forward** buttons to navigate the objects you already visited.
- When you go back to the object, you return to the same tab that you left.
- When you go back to the object and the tab is **SQL**, you return to the same cursor position that you left.

## Advanced search capabilities

Advanced search capabilities provide powerful and flexible searching features. You can find object declarations, get object information, perform quick searches, and perform advanced object searching in categories using patterns.

### Get quick information

You can get quick information on the object at the cursor position in the following ways:

- Select the **Quick Info** button on the top of the SQL window.
- Select **Quick Info** in the right-click pop-up menu.
- Press **Ctrl**+**Shift**+**Space**.

### Find declaration

Go to the declaration of the object at the cursor position in one of the following ways:

- Select **Go To Declaration** at the top of the SQL window.
- Select **Go To Declaration** in the right-click pop-up menu.
- Press **F12**.

### Quick search

Perform a quick text search using the following features:

- Use **Ctrl**+**F** to start searching.
- Use **F3** to repeat the last search forward.
- Use **Shift**+**F3** to repeat the last search backward.
- Use **Ctrl**+**F3** to find the next occurrence of the word at the cursor position.
- Use **Ctrl**+**Shift**+**F3** to find the previous occurrence of the word at the cursor position.
- Use menu items to perform all these actions.

### Advanced search

To open the **Advanced Search** dialog, point to **Find** on the **Edit** menu, and then select **Advanced Search**. In the dialog, you can find any object by using a pattern. At the top of the dialog, you can choose the search area and object categories.
