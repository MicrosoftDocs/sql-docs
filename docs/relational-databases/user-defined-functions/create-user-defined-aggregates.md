---
title: "Create User-defined Aggregates"
description: "Create User-defined Aggregates"
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/28/2022"
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "aggregate functions [SQL Server], user-defined"
  - "user-defined functions [CLR integration]"
---
# Create user-defined aggregates

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

You can create a database object inside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is programmed in a CLR assembly. Database objects that can use the rich programming model provided by the CLR include triggers, stored procedures, functions, aggregate functions, and types.

Like the built-in aggregate functions provided in [!INCLUDE[tsql](../../includes/tsql-md.md)], user-defined aggregate functions perform a calculation on a set of values and return a single value.

Creating a user-defined aggregate function in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] involves the following steps:

- Define the user-defined aggregate function as a class in a [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Framework-supported language. For more information about how to program user-defined aggregates in the CLR, see [CLR User-Defined Aggregates](../../relational-databases/clr-integration-database-objects-user-defined-functions/clr-user-defined-aggregates.md). Compile this class to build a CLR assembly using the appropriate language compiler.

- Register the assembly in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using the CREATE ASSEMBLY statement. For more information about assemblies in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Assemblies &#40;Database Engine&#41;](../../relational-databases/clr-integration/assemblies-database-engine.md).

- Create the user-defined aggregate that references the registered assembly using the CREATE AGGREGATE statement.

Executing CLR code is off by default in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can create, alter, and drop database objects that reference managed code modules, but these references won't execute in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] unless the [clr enabled](../../database-engine/configure-windows/clr-enabled-server-configuration-option.md) option is enabled by using [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md).

Deploying a SQL Server Project in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] registers an assembly in the database that was specified for the project. Deploying the project also creates CLR functions in the database for all methods annotated with the **SqlFunction** attribute. For more information, see [Deploying CLR Database Objects](../../relational-databases/clr-integration/deploying-clr-database-objects.md).

## Create, modify, or drop an assembly

- [CREATE ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/create-assembly-transact-sql.md)
- [ALTER ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-assembly-transact-sql.md)
- [DROP ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-assembly-transact-sql.md)

## Create a user-defined aggregate

- [CREATE AGGREGATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-aggregate-transact-sql.md)

## See also

- [Common Language Runtime &#40;CLR&#41; Integration Programming Concepts](../../relational-databases/clr-integration/common-language-runtime-clr-integration-programming-concepts.md)
