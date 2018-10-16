---
title: "Query Definitions Differ Dialog Box (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdtsql.chm:69639"
  - "vdtsql.chm:69640"
  - "vdt.dlgbox.querydefinitionsdiffer"
ms.assetid: 90383473-2922-40e5-9682-3850849aa856
author: "stevestein"
ms.author: "sstein"
manager: craigg

---
# Query Definitions Differ Dialog Box (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
This dialog box notifies you that your query cannot be represented graphically in the Diagram and Criteria panes, and that you can edit your query only in the SQL pane.  
  
This dialog box may appear when you enter or edit an SQL statement in the SQL pane; you then move to another pane, verify the query, or attempt to execute the query; and one of the following conditions applies:  
  
-   The SQL statement is incomplete or contains one or more syntax errors.  
  
-   The SQL statement is valid but is not supported in the graphical panes (for example, a Union query).  
  
-   The SQL statement is valid but contains syntax specific to the data connection you are using.  
  
> [!TIP]  
> You can check whether a statement is valid using the **Verify SQL Statement** button on the **Query** toolbar.  
  
The dialog box displays a message with the reason that the SQL statement cannot be represented, and then asks how you want to proceed.  
  
> [!NOTE]  
> The **Query Definitions Differ** dialog box does not appear if you have hidden the Diagram and Criteria panes.  
  
## Options  
**Ignore Button**  
Choose this button to specify that you want to accept the SQL statement, either to edit it further or to execute it. If you accept the statement, the Diagram and Criteria panes appear dimmed to indicate that they do not represent the statement in the SQL pane.  
  
**Undo Button**  
Choose this button to discard your changes to the SQL pane.  
  
> [!NOTE]  
> If the statement is correct but not supported graphically by the Query and View Designer, you can execute it even though it cannot be represented in the Diagram and Criteria panes. For example, if you enter a Union query, the statement can be executed but not represented in the other panes.  
  
## See Also  
[Query and View Designer Tools &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/query-and-view-designer-tools-visual-database-tools.md)  
  
