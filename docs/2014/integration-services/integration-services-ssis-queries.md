---
title: "Integration Services (SSIS) Queries | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Query Builder [Integration Services]"
  - "queries [Integration Services]"
  - "statements [Integration Services]"
  - "queries [Integration Services], about queries in packages"
ms.assetid: 8822bd29-4575-46c8-92a0-1a39bc2604c1
author: janinezhang
ms.author: janinez
manager: craigg
---
# Integration Services (SSIS) Queries
  The Execute SQL task, the OLE DB source, the OLE DB destination, and the Lookup transformation can use SQL queries. In the Execute SQL task, the SQL statements can create, update, and delete database objects and data; run stored procedures; and perform SELECT statements. In the OLE DB source and the Lookup transformation, the SQL statements are typically SELECT statements or EXEC statements. The latter most frequently run stored procedures that return result sets.  
  
 A query can be parsed to establish whether it is valid. When parsing a query that uses a connection to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], the query is parsed, executed, and the execution outcome (success or failure) is assigned to the parsing outcome. If the query uses a connection to a data other than [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], the statement is parsed only.  
  
 The SQL statement can be defined either by entering it directly in the designer, or by specifying a file connection or a variable that contains the statement.  
  
## Direct Input SQL  
 Query Builder is available in the user interface for the Execute SQL task, the OLE DB source, the OLE DB destination, and the Lookup transformation. Query Builder offers the following advantages:  
  
-   Work visually or with SQL commands.  
  
     Query Builder includes graphical panes that compose your query visually and a text pane that displays the SQL text of your query. You can work in either the graphical or text panes. Query Builder synchronizes the views so that the query text and graphical representation always match.  
  
-   Join related tables.  
  
     If you add more than one table to your query, Query Builder automatically determines how the tables are related and constructs the appropriate join command.  
  
-   Query or update databases.  
  
     You can use Query Builder to return data using Transact-SQL SELECT statements, or to create queries that update, add, or delete records in a database.  
  
-   View and edit results immediately.  
  
     You can execute your query and work with a recordset in a grid that lets you scroll through and edit records in the database.  
  
 Although Query Builder is visually limited to creating SELECT queries, you can type the SQL for other types of statements such as DELETE and UPDATE statements in the text pane. The graphical pane is automatically updated to reflect the SQL statement that you typed.  
  
 You can also provide direct input by typing the query in the task or data flow component dialog box or the Properties window.  
  
 For more information, see [Query Builder](../../2014/integration-services/query-builder.md).  
  
## SQL in Files  
 The SQL statement for the Execute SQL task can also reside in a separate file. For example, you can write queries using tools such as the Query Editor in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], save the query to a file, and then read the query from the file when running a package. The file can contain only the SQL statements to run and comments. To use a SQL statement stored in a file, you must provide a file connection that specifies the file name and location. For more information, see [File Connection Manager](connection-manager/file-connection-manager.md).  
  
## SQL in Variables  
 If the source of the SQL statement in the Execute SQL task is a variable, you provide the name of the variable that contains the query. The Value property of the variable contains the query text. You set the ValueType property of the variable to a string data type and then type or copy the SQL statement into the Value property. For more information, see [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md) and [Use Variables in Packages](../../2014/integration-services/use-variables-in-packages.md).  
  
  
