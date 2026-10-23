---
title: "SqlTriggerContext Object"
description: In SQL Server CLR integration, the SqlTriggerContext class provides context information for a trigger including type of action and columns modified in operation.
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "SqlTriggerContext object"
  - "triggers [CLR integration]"
  - "context [CLR integration]"
---
# SqlTriggerContext object

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The [!INCLUDE [dnprdnshort-md](../../includes/dnprdnshort-md.md)] common language runtime (CLR) `SqlTriggerContext` class provides context information about the trigger. This contextual information includes the type of action that caused the trigger to fire, which columns were modified in an `UPDATE` operation, and, with a data definition language (DDL) trigger, an XML `EventData` structure that describes the triggering operation.

For more information and examples of how to use the `SqlTriggerContext` class, see [CLR triggers](/dotnet/framework/data/adonet/sql/clr-triggers) and [Microsoft.SqlServer.Server.SqlTriggerContext](/dotnet/api/microsoft.sqlserver.server.sqltriggercontext).

## Related content

- [CLR triggers](/dotnet/framework/data/adonet/sql/clr-triggers)
- [EVENTDATA (Transact-SQL)](../../t-sql/functions/eventdata-transact-sql.md)
