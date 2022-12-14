---
description: "SQL Syntax Errors Encountered Dialog Box (Visual Database Tools)"
title: SQL Syntax Errors Encountered Dialog Box
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdt.dlgbox.sqlsyntaxerrorsencountered"
  - "vdtsql.chm:69641"
ms.assetid: bc9e5784-227e-4c5d-8084-24274fa6c14a
author: markingmyname
ms.author: maghan
ms.reviewer: 

---
# SQL Syntax Errors Encountered Dialog Box (Visual Database Tools)
[!INCLUDE[SQL Server](../../includes/applies-to-version/sqlserver.md)]
This dialog box notifies you that the designer cannot parse the SQL statement in the SQL pane.  
  
This dialog box appears when you enter or edit an SQL statement in the SQL pane; then either switch to another pane, verify the query, or attempt to execute the query; and one of the following conditions applies:  
  
-   The SQL statement is incomplete or contains one or more syntax errors.  
  
-   The SQL statement is valid but is not supported in the graphical panes (for example, a Union query).  
  
-   The SQL statement is valid but contains syntax specific to the data connection you are using.  
  
> [!TIP]  
> You can check whether a statement is valid using the **Verify SQL Syntax** button on the **Query** toolbar.  
  
The dialog box displays a message with the reason that the SQL statement cannot be parsed. Click **OK** to proceed.  
  
## See Also  
[Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-queries-and-views-how-to-topics-visual-database-tools.md)  
  
