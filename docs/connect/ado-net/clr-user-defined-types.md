---
title: "CLR User-Defined Types"
ms.date: "08/15/2019"
ms.assetid: 9f70e0b0-3a0d-4eb1-b914-07a5d0c167c2
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: MightyPen
ms.author: genemi
---
# CLR User-Defined Types

![Download-DownArrow-Circled](../../ssdt/media/download.png)[Download ADO.NET](../sql-connection-libraries.md#anchor-20-drivers-relational-access)

Microsoft SQL Server provides support for user-defined types (UDTs) implemented with the Microsoft .NET Framework common language runtime (CLR). The CLR is integrated into SQL Server, and this mechanism enables you to extend the type system of the database. UDTs provide user extensibility of the SQL Server data type system, and also the ability to define complex structured types.  
  
 UDTs can provide two key benefits from an application architecture perspective:  
  
- Strong encapsulation (both in the client and the server) between the internal state and the external behaviors.  
  
- Deep integration with other related server features. Once you define your own UDT, you can use it in all contexts where you can use a system type in SQL Server, including column definitions, and as variables, parameters, function results, cursors, triggers, and replication.  
  
 For more detailed information, see the [SQL Server documentation](/sql) for the version of SQL Server you're using.
  
 **SQL Server documentation**
  
1. [CLR User-Defined Types](/sql/relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types)  
  
## See also

- [ADO.NET Overview](https://docs.microsoft.com/dotnet/framework/data/adonet/ado-net-overview)
