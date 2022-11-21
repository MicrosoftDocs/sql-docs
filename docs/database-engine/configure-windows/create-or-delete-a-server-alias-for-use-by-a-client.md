---
title: Create or delete a server alias for use by a client
description: Find out how to create and delete an alias, an alternate name you can use when you connect to an instance of SQL Server. Learn about the benefits of aliases.
author: rwestMSFT
ms.author: randolphwest
ms.date: "07/14/2021"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
f1_keywords:
  - "server alias"
helpviewer_keywords:
  - "aliases [SQL Server], deleting"
  - "aliases [SQL Server], creating"
---

# Create or delete a server alias for use by a client

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This topic describes how to create or delete a server alias in SQL Server by using SQL Server Configuration Manager. An alias is an alternate name that can be used to make a connection. The alias encapsulates the required elements of a connection string, and exposes them with a name chosen by the user. Aliases can be used with any client application. By creating server aliases, your client computer can connect to multiple servers using different network protocols, without having to specify the protocol and connection details for each one. In addition, you can also have different network protocols enabled all the time, even if you only need to use them occasionally. If you have configured the server to listen on a non-default port number or named pipe, and you have disabled the SQL Server Browser service, create an alias that specifies the new port number or named pipe.

## <a name="SSMSProcedure"></a> Using SQL Server Configuration Manager

### To create an alias

1. In [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md), expand **SQL Server Native Client Configuration**, right-click **Aliases**, and then select **New Alias**.

2. In the **Alias Name** box, type the name of the alias. Client applications use this name when they connect.

3. In the **Server** box, type the name or IP address of a server. For a named instance append the instance name.

4. In the **Protocol** box, select the protocol used for this alias. Selecting a protocol, changes the title of the optional properties box to Port No, Pipe Name, or Connection String.  

The connection strings described in SQL Server Configuration Manager Help can be useful for programmers who create their own connection strings. To access this information, in the **New Alias** dialog box, press F1, or select **Help**.

> [!NOTE]
> If a configured alias is connecting to the wrong server or instance, disable and then reenable the associated network protocol. Doing this clears any cached connection information and allows the client to connect correctly.

### To delete an alias

1. In SQL Server Configuration Manager, expand **SQL Server Native Client Configuration**, and then select **Aliases**.  

2. In the details pane, right-click the alias that you want to delete, and then select **Delete**.

## Next steps

- [SQL Server Config Manager](../../relational-databases/sql-server-configuration-manager.md)