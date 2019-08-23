---
title: "Use the Query and View Designer with International Data (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "Visual Database Tools [SQL Server], international data"
  - "queries [SQL Server], international data"
  - "languages [SQL Server], Query and View Designer"
  - "international considerations [SQL Server], Query and View Designer"
  - "Criteria pane"
  - "View Designer, international data"
  - "Query Designer [SQL Server], international data"
  - "localized information in Query and View Designer [SQL Server]"
  - "global considerations [SQL Server], Query and View Designer"
  - "SQL pane [Visual Database Tools]"
  - "multiple language support [SQL Server], Query and View Designer"
ms.assetid: 4b51c56f-f902-4e72-b919-e36127369b63
author: stevestein
ms.author: sstein
manager: craigg
---
# Use the Query and View Designer with International Data (Visual Database Tools)
  You can use the [Query and View Designer](visual-database-tools.md) with data in any language and in any version of the Windows operating system. The following guidelines outline the differences you will notice and provide information about managing data in international applications.  
  
## Localized Information in the Criteria and SQL Panes  
 If you are using the Criteria pane to create your query, you can enter information in the format that corresponds to the Windows Regional Settings for you computer. For example, if you are searching for data, you can enter the data in the Criteria columns using whatever format you are accustomed to using, with these exceptions:  
  
-   Long data formats are not supported.  
  
-   Currency symbols should not be entered in the Criteria pane.  
  
-   Currency symbols will not display in the Results pane.  
  
    > [!NOTE]  
    >  In the Results pane, you can actually enter the currency symbol that corresponds to the Windows Regional Settings for your computer, but the symbol will be removed and will not display in the Results pane.  
  
-   Unary minus always appears on the left side (for example, -1) regardless of the Regional Settings options.  
  
 In contrast, data and keywords in the SQL pane must always be in ANSI (U.S.) format. For example, as the Query and View Designer builds a query, it inserts the ANSI form of all SQL keywords such as SELECT and FROM. If you add elements to the statement in the SQL pane, be sure to use the ANSI standard form for the elements.  
  
 When you enter data using local-specific format in the Criteria pane, the Query and View Designer automatically translates it to ANSI format in the SQL pane. For example, if your Regional Settings are set to Standard German, you can enter data in the Criteria pane in a format such as "31.12.96." However, the date will appear in the SQL pane in ANSI datetime format as `{ ts '1996-12-31 00:00:00' }.` If you enter data directly in the SQL pane, you must enter it in ANSI format.  
  
## Sort Order  
 The sort order of data in your query is determined by the database. Options that you set in the Windows Regional Settings dialog box do not affect sort order for queries. Within any particular query, however, you can request that rows be returned in a particular order.  
  
## Using Double-Byte Characters  
 You can enter DBCS characters for literals and for database object names such as table and view names or aliases. You can also use DBCS characters for parameter names and parameter marker characters. However, you cannot use DBCS characters in SQL language elements such as function names or SQL keywords.  
  
## See Also  
 [Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](design-queries-and-views-how-to-topics-visual-database-tools.md)  
  
  
