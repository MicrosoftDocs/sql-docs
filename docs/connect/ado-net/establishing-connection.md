---
title: "Establishing connection"
description: Guideline for connecting to a SQL Server by SqlClient provider.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-chmalh
ms.date: "11/13/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# Establishing connection

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

To connect to Microsoft SQL Server, use the <xref:Microsoft.Data.SqlClient.SqlConnection> object of the Microsoft SqlClient Data Provider for SQL Server. For securely storing and retrieving connection strings, see [Protecting Connection Information](protecting-connection-information.md).

## Closing connections

We recommend that you always close the connection when you are finished using it, so that the connection can be returned to the pool. The `Using` block in Visual Basic or C# automatically disposes of the connection when the code exits the block, even in the case of an unhandled exception. See [using Statement](/dotnet/csharp/language-reference/keywords/using-statement) and [Using Statement](/dotnet/visual-basic/language-reference/statements/using-statement) for more information.

You can also use the `Close` or `Dispose` methods of the connection object. Connections that are not explicitly closed might not be added or returned to the pool. For example, a connection that has gone out of scope but that has not been explicitly closed will only be returned to the connection pool if the maximum pool size has been reached and the connection is still valid.

> [!NOTE]
> Do not call `Close` or `Dispose` on a **Connection**, a **DataReader**, or any other managed object in the `Finalize` method of your class. In a finalizer, only release unmanaged resources that your class owns directly. If your class does not own any unmanaged resources, do not include a `Finalize` method in your class definition. For more information, see [Garbage Collection](/dotnet/standard/garbage-collection/index).

> [!NOTE]
> Login and logout events will not be raised on the server when a connection is fetched from or returned to the connection pool, because the connection is not actually closed when it is returned to the connection pool. For more information, see [SQL Server Connection Pooling (ADO.NET)](sql-server-connection-pooling.md).

## Connecting to SQL Server

For valid string format names and values, see the <xref:Microsoft.Data.SqlClient.SqlConnection.ConnectionString%2A> property of the <xref:Microsoft.Data.SqlClient.SqlConnection> object. You can also use the <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder> class to create syntactically valid connection strings at run time. For more information, see [Connection String Builders](connection-string-builders.md).

The following code example demonstrates how to create and open a connection to a SQL Server database.

[!code-csharp[SqlConnection.Open#1](~/../sqlclient/doc/samples/SqlConnection_Open.cs#1)]

### Integrated security and ASP.NET

SQL Server Integrated Security (also known as trusted connections) helps to provide protection when connecting to SQL Server as it does not expose a user ID and password in the connection string and is the recommended method for authenticating a connection. Integrated security uses the current security identity, or token, of the executing process. For desktop applications, this identity is typically the identity of the currently logged-on user.

The security identity for ASP.NET applications can be set to one of several different options. To better understand the security identity that an ASP.NET application uses when connecting to SQL Server, see [ASP.NET Impersonation](/previous-versions/aspnet/xh507fc5(v=vs.100)), [ASP.NET Authentication](/previous-versions/aspnet/eeyk640h(v=vs.100)), and [How to: Access SQL Server Using Windows Integrated Security](/previous-versions/aspnet/bsz5788z(v=vs.100)).

## See also

- [Connecting to a data source](connecting-to-data-source.md)
- [Connection strings](connection-strings.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
