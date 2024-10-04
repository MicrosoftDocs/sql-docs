---
title: Connect with sqlcmd
description: "Learn how to select which protocol sqlcmd uses to communicate with SQL Server. The choices are: TCP/IP, named pipes, and shared memory."
author: dlevy-msft
ms.author: dlevy
ms.reviewer: maghan, randolphwest, mathoma
ms.date: 09/27/2024
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords:
  - "sqlcmd utility, Database Engine connections"
  - "Named Pipes [SQL Server], sqlcmd utility"
  - "TCP/IP [SQL Server], client protocols"
  - "network protocols [SQL Server], sqlcmd utility"
  - "protocols [SQL Server], sqlcmd utility"
  - "VIA"
  - "client protocols [SQL Server]"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
#  Connect to SQL Server with sqlcmd

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This article describes how to connect to the SQL Server database engine by using the [sqlcmd utility](sqlcmd-utility.md).

## Overview

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports client communication with the TCP/IP network protocol (the default), and the named pipes protocol. The shared memory protocol is also available if the client is connecting to an instance of the Database Engine on the same computer. There are three common methods of selecting the protocol. The protocol used by the **sqlcmd** utility is determined in the following order:

- **sqlcmd** uses the protocol specified as part of the connection string, as described later in this article.

- If no protocol is specified as part the connection string, **sqlcmd** uses the protocol defined as part of the alias that's connected. To configure **sqlcmd** to use a specific network protocol by creating an alias, see [Create or Delete a Server Alias for Use by a Client (SQL Server Configuration Manager)](../../database-engine/configure-windows/create-or-delete-a-server-alias-for-use-by-a-client.md).

- If the protocol isn't specified in some other way, **sqlcmd** uses the network protocol determined by the protocol order in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.

The following examples show various ways of connecting to the default instance of Database Engine on port 1433, and named instances of Database Engine presumed to be listening on port 1691. Some of these examples use the IP address of the loopback adapter (127.0.0.1). Test using the IP address of your computer network interface card.

Connect to the Database Engine by specifying the instance name:

```
sqlcmd -S ComputerA
sqlcmd -S ComputerA\instanceB
```

Connect to the Database Engine by specifying the IP address:

```
sqlcmd -S 127.0.0.1
sqlcmd -S 127.0.0.1\instanceB
```

Connect to the Database Engine by specifying the TCP\IP port number:

```
sqlcmd -S ComputerA,1433
sqlcmd -S ComputerA,1691
sqlcmd -S 127.0.0.1,1433
sqlcmd -S 127.0.0.1,1691
```

## Connect using tcp/ip

- Connect using the following general syntax:

    ```
    sqlcmd -S tcp:<computer name>,<port number>
    ```

- Connect to the default instance:

    ```
    sqlcmd -S tcp:ComputerA,1433
    sqlcmd -S tcp:127.0.0.1,1433
    ```

- Connect to a named instance:

    ```
    sqlcmd -S tcp:ComputerA,1691
    sqlcmd -S tcp:127.0.0.1,1691
    ```

## Connect using named pipes

- Connect using one of the following general syntaxes:

    ```
    sqlcmd -S np:\\<computer name>\<pipe name>
    ```

- Connect to the default instance:

    ```
    sqlcmd -S np:\\ComputerA\pipe\sql\query
    sqlcmd -S np:\\127.0.0.1\pipe\sql\query
    ```

- Connect to a named instance:

    ```
    sqlcmd -S np:\\ComputerA\pipe\MSSQL$<instancename>\sql\query
    sqlcmd -S np:\\127.0.0.1\pipe\MSSQL$<instancename>\sql\query
    ```

## Connect using shared memory (a local procedure call) from a client on the server

- Connect using one of the following general syntaxes:

    ```
    sqlcmd -S lpc:<computer name>
    ```

- Connect to the default instance:

    ```
    sqlcmd -S lpc:ComputerA
    ```

- Connect to a named instance:

    ```
    sqlcmd -S lpc:ComputerA\<instancename>
    ```

## Related content

- [sqlcmd utility](sqlcmd-utility.md)
