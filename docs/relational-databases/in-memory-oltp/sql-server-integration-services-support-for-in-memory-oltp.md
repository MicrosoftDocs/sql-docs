---
title: "SQL Server Integration Services Support for In-Memory OLTP | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: ea82a9b9-e9ed-4d6f-b3fd-917f6c687ae3
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQL Server Integration Services Support for In-Memory OLTP
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  You can use a memory-optimized table, a view referencing memory-optimized tables, or a natively compiled stored procedure as the source or destination for your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] (SSIS) package. You can use [ADO NET Source](../../integration-services/data-flow/ado-net-source.md), [OLE DB Source](../../integration-services/data-flow/ole-db-source.md), or [ODBC Source](../../integration-services/data-flow/odbc-source.md) in the data flow of an SSIS package and configure the source component to retrieve data from a memory-optimized table or a view, or specify a SQL statement to execute a natively compiled stored procedure. Similarly, you can use [ADO NET Destination](../../integration-services/data-flow/ado-net-destination.md), [OLE DB Destination](../../integration-services/data-flow/ole-db-destination.md), or [ODBC Destination](../../integration-services/data-flow/odbc-destination.md) to load data into a memory-optimized table or a view, or specify a SQL statement to execute a natively compiled stored procedure.  
  
 You can configure the above mentioned source and destination components in an SSIS package to read from/write to memory-optimized tables and views in the same way as with other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables and views. However, you need to be aware of the important points in the following section when using natively compiled stored procedures.  
  
## Invoking a natively compiled stored procedure from an SSIS Package  
 To invoke a natively compiled stored procedure from an SSIS package, we recommend that you use an ODBC Source or ODBC Destination with an SQL statement of the format: **\<procedure name>** without the **EXEC** keyword. If you use the EXEC keyword in the SQL statement, you will see an error message because the ODBC connection manager interprets the SQL command text as a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement rather than a stored procedure and use cursors, which are not supported for execution of natively compiled stored procedures. The connection manager treats the SQL statement without the EXEC keyword as a stored procedure call and will not use a cursor.  
  
 You can also use ADO .NET Source and OLE DB Source to invoke a natively compiled stored procedure, but we recommend that you use ODBC Source. If you configure the ADO .NET Source to execute a natively compiled stored procedure, you will see an error message because the data provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (SqlClient), which the ADO .NET Source uses by default, does not support execution of natively compiled stored procedures. You can configure the ADO .NET Source to use the ODBC Data Provider, OLE DB Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client. However, note that the ODBC Source performs better than ADO .NET Source with ODBC Data Provider.  
  
## See Also  
 [SQL Server Support for In-Memory OLTP](../../relational-databases/in-memory-oltp/sql-server-support-for-in-memory-oltp.md)  
  
  
