---
title: "Uses of Catalog Data | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "catalog data [ODBC]"
  - "functions [ODBC], catalog functions"
  - "catalog functions [ODBC], using catalog data"
ms.assetid: d5915d0c-eec3-4382-850e-bd863763c99a
author: MightyPen
ms.author: genemi
manager: craigg
---
# Uses of Catalog Data
Applications use catalog data in a variety of ways. Here are some common uses:  
  
-   **Constructing SQL statements at run time.** Vertical applications, such as an order entry application, contain hard-coded SQL statements. The tables and columns that are used by the application are fixed ahead of time, as are the statements that access these tables. For example, an order entry application usually contains a single, parameterized **INSERT** statement for adding new orders to the system.  
  
     Generic applications, such as a spreadsheet program that uses ODBC to retrieve data, often construct SQL statements at run time based on input from the user. Such an application could require the user to type the names of the tables and columns to use. However, it would be easier for the user if the application displayed lists of tables and columns from which the user could make selections. To build these lists, the application would call the **SQLTables** and **SQLColumns** catalog functions.  
  
-   **Constructing SQL statements during development.** Application development environments typically allow the programmer to create database queries while developing a program. The queries are then hard-coded in the application being built.  
  
     Such environments could also use **SQLTables** and **SQLColumns** to create lists from which the programmer could make selections. These environments might also use **SQLPrimaryKeys** and **SQLForeignKeys** to automatically determine and show relationships between selected tables, and use **SQLStatistics** to determine and highlight indexed fields so the programmer can create efficient queries.  
  
-   **Constructing cursors.** An application, driver, or middleware that provides a scrollable cursor engine could use **SQLSpecialColumns** to determine which column or columns uniquely identify a row. The program could build a *keyset* containing the values of these columns for each row that has been fetched. When the application scrolls back to the row, it would then use these values to fetch the most recent data for the row. For more information about scrollable cursors and keysets, see [Scrollable Cursors](../../../odbc/reference/develop-app/scrollable-cursors.md).
