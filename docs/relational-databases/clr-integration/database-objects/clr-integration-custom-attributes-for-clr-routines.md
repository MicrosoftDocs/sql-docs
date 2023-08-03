---
title: "CLR integration: custom attributes for CLR routines"
description: Custom attributes can be applied to CLR routines, user-defined types, and user-defined aggregates that are registered in Microsoft SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/19/2023
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "routines [CLR integration]"
  - "SqlFacet attribute"
  - "SqlTrigger attribute"
  - "SqlProcedure attribute"
  - "custom attributes [CLR integration]"
  - "SqlUserDefinedAggregate attribute"
  - "attributes [CLR integration]"
  - "SqlMethod attribute"
  - "SqlFunction attribute"
  - "common language runtime [SQL Server], attributes"
  - "SqlUserDefinedTypeAttribute attribute"
---
# CLR integration: custom attributes for CLR routines

[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]

The attributes listed can be applied to common language runtime (CLR) routines, user-defined types, and user-defined aggregates that are registered in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. If the attribute isn't applied, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] assumes the default value. The attributes listed are defined in the **Microsoft.SqlServer.Server** namespace.

## The SqlUserDefinedAggregate attribute

The **SqlUserDefinedAggregate** attribute indicates that the method should be registered as a user-defined aggregate. Every user-defined aggregate must be annotated with this attribute.

For more information, see [SqlUserDefinedAggregateAttribute](/dotnet/api/microsoft.sqlserver.server.sqluserdefinedaggregateattribute).

## The SqlFunction attribute

The **SqlFunction** attribute indicates the method should be registered as a function, with the appropriate function attributes set.

For more information, see [SqlFunctionAttribute](/dotnet/api/microsoft.sqlserver.server.sqlfunctionattribute).

## The SqlFacet attribute

The **SqlFacet** attribute is used to return information about the return type of a user-defined type (UDT) expression.

For more information, see [SqlFacetAttribute](/dotnet/api/microsoft.sqlserver.server.sqlfacetattribute).

## The SqlProcedure attribute

The **SqlProcedure** attribute indicates the method should be registered as a stored procedure. This attribute is used only by Visual Studio to register the specified method as a stored procedure automatically; it isn't used by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].

<!--For more information, see [SqlProcedureAttribute](/dotnet/api/microsoft.sqlserver.server.sqlprocedureattribute).-->

## The SqlTrigger attribute

The **SqlTrigger** attribute indicates the method should be registered as a trigger.

<!--For more information, see [SqlTriggerContext](/dotnet/api/microsoft.sqlserver.server.sqltriggercontext) and [SqlTriggerAttribute](/dotnet/api/microsoft.sqlserver.server.sqltriggerattribute).-->

For more information, see [SqlTriggerContext](/dotnet/api/microsoft.sqlserver.server.sqltriggercontext).

## The SqlUserDefinedTypeAttribute

You can apply the SqlUserDefinedTypeAttribute to a class definition in the assembly. It causes [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to create a user-defined type that is bound to the class definition that has this custom attribute.

For more information, see [SqlUserDefinedTypeAttribute](/dotnet/api/microsoft.sqlserver.server.sqluserdefinedtypeattribute).

## The SqlMethod attribute

The **SqlMethod** attribute is used to indicate the determinism and data access properties of a method or a property on a UDT.

For more information, see [SqlMethodAttribute](/dotnet/api/microsoft.sqlserver.server.sqlmethodattribute).

## See also

- [CLR User-Defined Aggregates](../../../relational-databases/clr-integration-database-objects-user-defined-functions/clr-user-defined-aggregates.md)
- [CLR User-Defined Functions](../../../relational-databases/clr-integration-database-objects-user-defined-functions/clr-user-defined-functions.md)
- [CLR User-Defined Types](../../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md)
- [CLR Stored Procedures](/dotnet/framework/data/adonet/sql/clr-stored-procedures)
- [CLR Triggers](/dotnet/framework/data/adonet/sql/clr-triggers)
