---
title: "SQL Server Common Language Runtime Integration"
ms.date: "08/15/2019"
ms.assetid: c7a324c4-160d-44c2-b593-641af06eca61
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: MightyPen
ms.author: genemi
---
# SQL Server Common Language Runtime Integration

![Download-DownArrow-Circled](../../ssdt/media/download.png)[Download ADO.NET](../sql-connection-libraries.md#anchor-20-drivers-relational-access)

SQL Server 2005 introduced the integration of the common language runtime (CLR) component of the .NET Framework for Microsoft Windows. This means that you can write stored procedures, triggers, user-defined types, user-defined functions, user-defined aggregates, and streaming table-valued functions, using any .NET Framework language, including Microsoft Visual Basic .NET and Microsoft Visual C#. The <xref:Microsoft.SqlServer.Server> namespace contains a set of new application programming interfaces (APIs) so that managed code can interact with the Microsoft SQL Server environment.  
  
 This section describes features and behaviors that are specific to SQL Server common language runtime (CLR) integration and the SQL Server in-process specific extensions to ADO.NET.  
  
 This section is meant to provide only enough information to get started programming with SQL Server CLR integration, and is not meant to be comprehensive. For more detailed information, see the version of SQL Server Books Online for the version of SQL Server you are using.  
  
 **SQL Server Books Online**  
  
1. [Common Language Runtime (CLR) Integration Programming Concepts](https://go.microsoft.com/fwlink/?LinkId=115240)  
  
## In This Section  
 [Introduction to SQL Server CLR Integration](../../connect/ado-net/introduction-to-sql-server-clr-integration.md)  
 Provides an introduction to SQL Server CLR integration. Provides links to additional topics.  
  
 [CLR User-Defined Functions](../../connect/ado-net/clr-user-defined-functions.md)  
 Describes how to implement and use the various types of CLR functions: table-valued, scalar, and user-defined aggregate functions.  
  
 [CLR User-Defined Types](../../connect/ado-net/clr-user-defined-types.md)  
 Describes how to implement and use CLR user-defined types. Provides links to additional topics.  
  
 [CLR Stored Procedures](../../connect/ado-net/clr-stored-procedures.md)  
 Describes how to implement and use CLR stored procedures. Provides links to additional topics.  
  
 [CLR Triggers](../../connect/ado-net/clr-triggers.md)  
 Describes how to implement and use CLR triggers. Provides links to additional topics.  
  
 [The Context Connection](../../connect/ado-net/the-context-connection.md)  
 Describes the context connection.  
  
 [SQL Server In-Process-Specific Behavior of ADO.NET](../../connect/ado-net/sql-server-in-process-specific-behavior-of-adonet.md)  
 Describes the SQL Server in-process specific extensions to ADO.NET, and the context connection. Provides links to additional topics.  
  
## See also

- [SQL Server and ADO.NET](../../connect/ado-net/index.md)
- [ADO.NET Managed Providers and DataSet Developer Center](https://go.microsoft.com/fwlink/?LinkId=217917)
