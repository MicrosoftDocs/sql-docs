---
title: Restrictions on Context Connections and Regular Connections
description: This article describes the restrictions associated with code running in the Microsoft SQL Server process through context and regular connections.
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "context connections [CLR integration]"
  - "regular connections [CLR integration]"
---
# Restrictions on context connections and regular connections

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

This article discusses the restrictions associated with code executing in the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] process through context and regular connections.

## Restrictions on context connections

When developing your application, take into account the following restrictions that apply to context connections:

- You can have only one context connection open at a given time for a given connection. If you have multiple statements running concurrently in separate connections, each one of them can get their own context connection. The restriction doesn't affect concurrent requests from different connections; it only affects a given request on a given connection.

- Multiple Active Result Sets (MARS) isn't supported in a context connection.

- The `SqlBulkCopy` class doesn't operate in a context connection.

- Update batching in a context connection isn't supported

- `SqlNotificationRequest` can't be used with commands that execute against a context connection.

- Canceling commands that are running against the context connection isn't supported. The `SqlCommand.Cancel` method silently ignores the request.

- No other connection string keywords can be used when you use `context connection=true`.

- The `SqlConnection.DataSource` property returns null if the connection string for the `SqlConnection` is `context connection=true`, instead of the name of the instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)].

- Setting the `SqlCommand.CommandTimeout` property has no effect when the command is executed against a context connection.

## Restrictions on regular connections

When developing your application, take into account the following restrictions that apply to regular connections:

- Asynchronous command execution against internal servers isn't supported. Including `async=true` in the connection string of a command, and then executing the command, results in `System.NotSupportedException` being thrown. This message appears:

  ```output
  Asynchronous processing is not supported when running inside the SQL Server process.
  ```

- `SqlDependency` object isn't supported.

## Related content

- [Context connection](context-connection.md)
