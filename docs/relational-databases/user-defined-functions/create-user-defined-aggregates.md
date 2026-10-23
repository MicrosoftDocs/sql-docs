---
title: "Create User-Defined Aggregates"
description: Learn how to create a user-defined aggregate object inside SQL Server that is programmed in a CLR assembly.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 12/27/2024
ms.service: sql
ms.topic: how-to
helpviewer_keywords:
  - "aggregate functions [SQL Server], user-defined"
  - "user-defined functions [CLR integration]"
---
# Create user-defined aggregates

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

You can create a database object inside [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that is programmed in a CLR assembly. Database objects that can use the rich programming model provided by the CLR include triggers, stored procedures, functions, aggregate functions, and types.

Like the built-in aggregate functions provided in [!INCLUDE [tsql](../../includes/tsql-md.md)], user-defined aggregate functions perform a calculation on a set of values and return a single value.

Creating a user-defined aggregate function in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] involves the following steps:

- Define the user-defined aggregate function as a class in a [!INCLUDE [msCoName](../../includes/msconame-md.md)] .NET Framework-supported language. For more information about how to program user-defined aggregates in the CLR, see [CLR user-defined aggregates](../clr-integration-database-objects-user-defined-functions/clr-user-defined-aggregates.md). Compile this class to build a CLR assembly using the appropriate language compiler.

- Register the assembly in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] using the `CREATE ASSEMBLY` statement. For more information about assemblies in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Assemblies (Database Engine)](../clr-integration/assemblies-database-engine.md).

- Create the user-defined aggregate that references the registered assembly using the `CREATE AGGREGATE` statement.

Executing CLR code is off by default in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You can create, alter, and drop database objects that reference managed code modules, but these references don't execute in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], unless the [clr enabled](../../database-engine/configure-windows/clr-enabled-server-configuration-option.md) server configuration option is enabled by using [sp_configure](../system-stored-procedures/sp-configure-transact-sql.md).

Deploying a SQL Server Project in [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [vsprvs](../../includes/vsprvs-md.md)] registers an assembly in the database that was specified for the project. Deploying the project also creates CLR functions in the database for all methods annotated with the `SqlFunction` attribute. For more information, see [Deploy CLR database objects](../clr-integration/deploying-clr-database-objects.md).

## Create, modify, or drop an assembly

- [CREATE ASSEMBLY (Transact-SQL)](../../t-sql/statements/create-assembly-transact-sql.md)
- [ALTER ASSEMBLY (Transact-SQL)](../../t-sql/statements/alter-assembly-transact-sql.md)
- [DROP ASSEMBLY (Transact-SQL)](../../t-sql/statements/drop-assembly-transact-sql.md)

## Create a user-defined aggregate

- [CREATE AGGREGATE (Transact-SQL)](../../t-sql/statements/create-aggregate-transact-sql.md)

## Related content

- [Common language runtime (CLR) integration programming concepts](../clr-integration/common-language-runtime-clr-integration-programming-concepts.md)
