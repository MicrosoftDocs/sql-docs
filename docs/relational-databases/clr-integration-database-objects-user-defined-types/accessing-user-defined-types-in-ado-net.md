---
title: "Accessing User-Defined Types in ADO.NET"
description: UDTs, written in .NET Framework CLR languages, allow a SQL Server database to store objects and custom data structures. In ADO.NET, a provider exposes UDTs.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "ADO.NET [CLR integration]"
  - "UDTs [CLR integration], ADO.NET"
  - "user-defined types [CLR integration], ADO.NET"
ms.assetid: 4b0d876c-8066-490e-8e18-327c0e942b19
---
# Accessing User-Defined Types in ADO.NET
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  User-defined types (UDTs) are written using any of the languages supported by the [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Framework common language runtime (CLR) that produce verifiable code. This includes [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual C# and [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Basic. UDTs allow objects and custom data structures to be stored in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. The data is exposed as public members of a .NET Framework class or structure, and behaviors are defined by methods of the class or structure. A UDT can be used as the column definition of a table, as a variable in a [!INCLUDE[tsql](../../includes/tsql-md.md)] batch, or as an argument of a [!INCLUDE[tsql](../../includes/tsql-md.md)] function or stored procedure.  
  
 In ADO.NET, the **System.Data.SqlClient** provider exposes UDTs in the following ways:  
  
-   Through the **System.Data.SqlClient.SqlDataReader** as an object.  
  
-   Through the **SqlDataReader** as raw bytes.  
  
-   As a parameter of a **System.Data.SqlClient.SqlParameter** object.  
  
## In This Section  
 [Retrieving UDT Data](../../relational-databases/clr-integration-database-objects-user-defined-types/accessing-user-defined-types-retrieving-udt-data.md)  
 Describes how to retrieve UDT data and how to specify parameters.  
  
 [Updating UDT Columns with DataAdapters](../../relational-databases/clr-integration-database-objects-user-defined-types/accessing-user-defined-types-updating-udt-columns-with-dataadapters.md)  
 Describes how to work with UDTs in **DataSets** and how to update UDT data using **DataAdapters**.  
  
## See Also  
 [CLR User-Defined Types](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md)  
  
  
