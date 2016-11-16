---
# required metadata

title: Use the sqlcmd command-line utility on Linux - SQL Server vNext CTP1 | Microsoft Docs
description: This tutorial shows how to run sqlcmd on Linux to run Transact-SQL queries.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 11/15/2016
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 9e6c1ae1-59a4-4589-b839-18d6a52f2676

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---
# Connect and query SQL Server on Linux with sqlcmd

This topic provides connection requirements and guidance for SQL Server vNext CTP1 running on Linux. In most cases, the connection requirements and processes do not differ across platforms. This topic approaches the subject in the context of Linux and then points to other resources.

This topic shows how to use [sqlcmd](https://msdn.microsoft.com/library/ms162773.aspx) to connect to SQL Server vNext on Linux. After successfully connecting, run a simple Transact-SQL (T-SQL) query to verify communication with the database.

> [!TIP]
> **Sqlcmd** is just one tool for connecting to SQL Server to run queries and perform management and development tasks. For other tools such as SQL Server Management Studio and Visual Studio Code, see the [Develop](sql-server-linux-develop-overview.md) and [Manage](sql-server-linux-management-overview.md) areas. 

## Install the SQL Server command-line tools

**Sqlcmd** is part of the SQL Server command-line tools, which are not installed automatically with SQL Server on Linux. If you have not already installed the SQL Server command-line tools on your Linux machine, you must install them. For more information on how to install the tools, follow the instructions for your Linux distribution:

- [Red Hat Enterprise Linux](sql-server-linux-setup-tools.md#RHEL)
- [Ubuntu](sql-server-linux-setup-tools.md#ubuntu)

## Connection requirements
To connect to SQL Server on Linux, you must use SQL Authentication (username and password). To connect remotely, you must ensure that the port SQL Server listens on is open. By default, SQL Server listens on TCP port 1433. Depending on your Linux distribution and configuration, you might have to open this port in the firewall. 

## Connect to SQL Server on Linux

The following steps show how to connect to SQL Server vNext on Linux with sqlcmd.

1. On your Linux box, open a command terminal.

2. Run **sqlcmd** with parameters for your SQL Server name (-S), the user name (-U), and the password (-P). 

   The following command connects to the local SQL Server instance (**localhost**) on Linux.

   ```bash
   sqlcmd -S localhost -U SA -P '<YourPassword>'
   ```

   > [!TIP]
   > You can omit the password on the command-line to be prompted to enter it.

   To connect to a remote instance, specify the machine name or IP address for the **-S** parameter. 

   ```bash
   sqlcmd -S 192.555.5.555 -U SA -P '<YourPassword>'
   ```

   > [!TIP]
   > If you get a connection failure, first attempt to diagnose the problem from the error message. Then review the [connection troubleshooting recommendations](sql-server-linux-connect-and-query.md#troubleshoot).

## Query SQL Server

After you connect to your server, run some queries. If you are new to writing queries, see [Writing Transact-SQL Statements](https://msdn.microsoft.com/library/ms365303.aspx).

For example, this query returns the name of all of the databases.

```sql
SELECT Name from sys.Databases;
GO
```

This query creates a database using the SQL Server default settings.

```sql
CREATE DATABASE testdb;
GO
```

Use the database:

```sql
USE testdb;
GO
```

Create a table in the current database:

```sql
CREATE TABLE inventory (id INT, name NVARCHAR(50), quantity INT);
GO
```

Insert data into the new table:

```sql
INSERT INTO inventory VALUES (1, 'banana', 150);
INSERT INTO inventory VALUES (2, 'orange', 154);
GO
```

Select from the table:

```sql
SELECT * FROM inventory WHERE quantity > 152;
GO
```

To end your sqlcmd session, type `QUIT`.

```sql
QUIT
```
In this walk-through you connected to SQL Server, and created and populated a database. For more information on how to use sqlcmd.exe, see [sqlcmd Utility](https://msdn.microsoft.com/library/ms162773.aspx).

## Next Steps

If you're new to T-SQL, see [Tutorial: Writing Transact-SQL Statements](https://msdn.microsoft.com/library/ms365303.aspx) and the [Transact-SQL Reference (Database Engine)](https://msdn.microsoft.com/library/bb510741.aspx).

For other ways to connect to SQL Server on Linux, see the [Develop](sql-server-linux-develop-overview.md) and [Manage](sql-server-linux-management-overview.md) areas. 


