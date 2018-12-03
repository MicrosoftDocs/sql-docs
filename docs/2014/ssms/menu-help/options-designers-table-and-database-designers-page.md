---
title: "Options (Designers-Table and Database Designers Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "VS.ToolsOptionsPages.Designers.Table_Designer"
ms.assetid: b43f4b97-17b9-4004-a824-f77b9e145741
author: stevestein
ms.author: sstein
manager: craigg
---
# Options (Designers-Table and Database Designers Page)
  Use this page to determine the default behavior of the designer. To access the settings, on the **Tools** menu, click **Options**, expand the **Designers** folder, and click **Table Designer**.  
  
## UIElement List  
 **Override connection string time-out value for table designer updates**  
 Permits a new time-out value to be set for the actions of the table designer. This can be useful when the table designer affects a large table and requires extra time to complete the table modification.  
  
 **Transaction time-out after**  
 Sets a time-out value for the table designer.  
  
 **Auto generate change scripts**  
 Automatically creates a script to implement the changes defined in the table designer.  
  
 **Warn on null primary keys**  
 Provides a warning dialog box when a field that is selected for a primary key can contain null values.  
  
 **Warn about difference detection**  
 If you check this box, when you save the changes, a message box will list any conflicts between your changes and changes that were made by another user.  
  
 **Warn about tables affected**  
 Provides a warning dialog box that lists the tables to be affected by the action and prompts for confirmation.  
  
 **Prevent saving changes that require table re-creation**  
 Prevents a user from making changes that require re-creating the table. The following actions might require a table to be re-created:  
  
-   Adding a new column to the middle of the table  
  
-   Dropping a column  
  
-   Changing column nullability  
  
-   Changing the order of the columns  
  
-   Changing the data type of a column  
  
 **Default table view**  
 Select the way you want to see tables in the designers:  
  
-   **Standard**  
  
     Shows the table header, all column names, data types, and the Allow Nulls setting.  
  
-   **Column Names**  
  
     Shows the column names.  
  
-   **Key**  
  
     Shows the table header and the primary key columns.  
  
-   **Name Only**  
  
     Shows only the table header with it's name.  
  
-   **Custom**  
  
     Allows you to choose which columns to view.  
  
 **Launch add table dialog on new diagram**  
 Automatically opens the **Add Table** dialog box when the designers open.  
  
  
