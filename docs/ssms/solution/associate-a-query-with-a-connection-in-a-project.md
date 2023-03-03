---
title: "Associate a Query with a Connection in a Project"
description: "Associate a Query with a Connection in a Project"
author: "markingmyname"
ms.author: "maghan"
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords:
  - "connections [SQL Server Management Studio], query associations"
  - "projects [SQL Server Management Studio], connections"
  - "projects [SQL Server Management Studio], query connections"
  - "query associations [SQL Server Management Studio]"
---
# Associate a Query with a Connection in a Project
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
If a query was created without a connection, or if a query is moved from one project to another it will not be associated with a connection in the current project.  
  
### To associate a query with a connection in a project  
  
1.  If the query is open in query editor, right-click a blank area of query editor, point to **Connection**, and then click **Connect**. If the query is not open, double-click the query in Solution Explorer to connect the query.  
  
2.  In the **Connect to Database Engine** dialog box, provide the connection information. If the connection information matches an existing connection, the query will be associated with that connection.  
  
## See Also  
[Solution Explorer](../../ssms/solution/solution-explorer.md)  
[Change the Connection Associated with a Query](../../ssms/solution/change-the-connection-associated-with-a-query.md)  
[View or Change the Properties of a Connection in a Project](../../ssms/solution/view-or-change-the-properties-of-a-connection-in-a-project.md)  
  
