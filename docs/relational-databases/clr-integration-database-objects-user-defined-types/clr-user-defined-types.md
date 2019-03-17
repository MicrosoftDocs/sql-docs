---
title: "CLR User-Defined Types | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "validation [CLR integration]"
  - "types [CLR integration]"
  - "UserDefined serialization format [CLR integration]"
  - "null values [CLR integration]"
  - "serialization"
  - "Native serialization format [CLR integration]"
  - "databases [CLR integration]"
  - "building database objects [CLR integration], user-defined types"
  - "user-defined types [CLR integration]"
  - "common language runtime [SQL Server], user-defined types"
  - "UDTs [CLR integration]"
  - "database objects [CLR integration], user-defined types"
  - "turning on CLR functionality"
  - "customizing UDT expression return types [CLR integration]"
  - "UDTs [CLR integration], about UDTs"
  - "comparing UDT values"
  - "annotations [CLR integration]"
  - "user-defined types [CLR integration], about UDTs"
  - "variables [CLR integration]"
  - "invoking UDT methods"
  - "indexes [CLR integration]"
ms.assetid: 27c4889b-c543-47a8-a630-ad06804f92df
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# CLR User-Defined Types
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] gives you the ability to create database objects that are programmed against an assembly created in the .NET Framework common language runtime (CLR). Database objects that can take advantage of the rich programming model provided by the CLR include triggers, stored procedures, functions, aggregate functions, and types.  
  
> [!NOTE]  
>  The ability to execute CLR code is set to OFF by default in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The CLR can be enabled by using the **sp_configure** system stored procedure.  
  
 Beginning with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], you can use user-defined types (UDTs) to extend the scalar type system of the server, enabling storage of CLR objects in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. UDTs can contain multiple elements and can have behaviors, differentiating them from the traditional alias data types which consist of a single [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system data type.  
  
 Because UDTs are accessed by the system as a whole, their use for complex data types may negatively impact performance. Complex data is generally best modeled using traditional rows and tables. UDTs in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are well suited to the following:  
  
-   Date, time, currency, and extended numeric types  
  
-   Geospatial applications  
  
-   Encoded or encrypted data  
  
 The process of developing UDTs in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] consists of the following steps:  
  
1.  **Code and build the assembly that defines the UDT.** UDTs are defined using any of the languages supported by the.NET Framework common language runtime (CLR) that produce verifiable code. This includes Visual C# and Visual Basic .NET. The data is exposed as fields and properties of a .NET Framework class or structure, and behaviors are defined by methods of the class or structure.  
  
2.  **Register the assembly.** UDTs can be deployed through the Visual Studio user interface in a database project, or by using the [!INCLUDE[tsql](../../includes/tsql-md.md)] CREATE ASSEMBLY statement, which copies the assembly containing the class or structure into a database.  
  
3.  **Create the UDT in SQL Server.** Once an assembly is loaded into a host database, you use the [!INCLUDE[tsql](../../includes/tsql-md.md)] CREATE TYPE statement to create a UDT and expose the members of the class or structure as members of the UDT. UDTs exist only in the context of a single database, and, once registered, have no dependencies on the external files from which they were created.  
  
    > [!NOTE]  
    >  Before [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], UDTs created from .NET Framework assemblies were not supported. However, you can still use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] alias data types by using **sp_addtype**. The CREATE TYPE syntax can be used for creating both native [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user-defined data types and UDTs.  
  
4.  **Create tables, variables, or parameters using the UDT** Beginning with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], a user-defined type can be used as the column definition of a table, as a variable in a [!INCLUDE[tsql](../../includes/tsql-md.md)] batch, or as an argument of a [!INCLUDE[tsql](../../includes/tsql-md.md)] function or stored procedure.  
  
## In This Section  
 [Creating a User-Defined Type](../../relational-databases/clr-integration-database-objects-user-defined-types/creating-user-defined-types.md)  
 Describes how to create UDTs.  
  
 [Registering User-Defined Types in SQL Server](../../relational-databases/clr-integration-database-objects-user-defined-types/registering-user-defined-types-in-sql-server.md)  
 Describes how to register and manage UDTs in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 [Working with User-Defined Types in SQL Server](../../relational-databases/clr-integration-database-objects-user-defined-types/working-with-user-defined-types-in-sql-server.md)  
 Describes how to create queries using UDTs.  
  
 [Accessing User-Defined Types in ADO.NET](../../relational-databases/clr-integration-database-objects-user-defined-types/accessing-user-defined-types-in-ado-net.md)  
 Describes how to work with UDTs using the .NET Framework Data Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in ADO.NET.  
  
  
