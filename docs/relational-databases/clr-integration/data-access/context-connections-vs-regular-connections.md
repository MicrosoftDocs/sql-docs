---
title: "Context Connections Vs. Regular Connections"
description: In SQL Server, sometimes you must use regular connections for Transact-SQL statements, but context connections offer performance and resource usage advantages.
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
# Context connections vs. regular connections

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

If you're connecting to a remote server, always use regular connections rather than context connections. If you need to connect to the same server on which the stored procedure or function is running, use the context connection in most cases. This method has benefits such as running in the same transaction space, and not having to reauthenticate.

Additionally, using the context connection typically results in better performance and less resource usage. The context connection is an in-process-only connection, so it can contact the server *directly* by bypassing the network protocol and transport layers to send Transact-SQL statements and receive results. The authentication process is bypassed, as well. The following figure shows the primary components of the `SqlClient` managed provider, and how the different components interact with each other when using a regular connection versus the context connection.

:::image type="content" source="media/context-connections-vs-regular-connections/data-access.png" alt-text="Diagram of code paths of a context and a regular connection.":::

The context connection follows a shorter code path and involves fewer components, so you can expect requests and results to get to and from the server faster than in a regular connection. Query execution time on the server is the same for context and regular connections.

There are some cases in which you might need to open a separate regular connection to the same server. For example, there are certain restrictions on using the context connection, described in [Restrictions on context connections and regular connections](context-connections-and-regular-connections-restrictions.md).

## Related content

- [Context connection](context-connection.md)
