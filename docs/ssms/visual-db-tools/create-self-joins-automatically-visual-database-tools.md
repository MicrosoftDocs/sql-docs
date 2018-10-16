---
title: "Create Self-Joins Automatically (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "automatic joins"
  - "self-joins"
  - "joins [SQL Server], self"
ms.assetid: f9ec90e8-3aad-415c-a5c4-8dfa9540e37f
author: "stevestein"
ms.author: "sstein"
manager: craigg

---
# Create Self-Joins Automatically (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
If a table has a reflexive relationship in the database, you can join it to itself automatically.  
  
### To create a self-join automatically  
  
1.  Add to the [Diagram pane](../../ssms/visual-db-tools/diagram-pane-visual-database-tools.md) the table you want to work with.  
  
2.  Add the same table again, so that the Diagram pane shows the same table twice within the Diagram pane.  
  
    The [Query and View Designer](../../ssms/visual-db-tools/query-and-view-designer-tools-visual-database-tools.md) assigns an alias to the second instance by adding a sequential number to the table name. In addition, the Query and View Designer creates a join line between the two rectangles representing the two different ways the table participates in the query.  
  
## See Also  
[Query with Joins &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/query-with-joins-visual-database-tools.md)  
  
