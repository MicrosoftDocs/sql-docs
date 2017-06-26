---
# required metadata

title: Connect and query SQL Server on Linux | Microsoft Docs
description: This tutorial shows how to run sqlcmd on Linux to run Transact-SQL queries.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 03/17/2017
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
ms.custom: H1Hack27Feb2017

---
# Connect to SQL Server on Linux and run Transact-SQL queries

This topic provides connection requirements and guidance for SQL Server 2017 RC1 running on Linux. In most cases, the connection requirements and processes do not differ across platforms. This topic approaches the subject in the context of Linux and then points to other resources. 

This topic is a walk-through. In this walk-through, you will use [sqlcmd](https://msdn.microsoft.com/library/ms162773.aspx) to connect to SQL Server 2017 on Linux. After successfully connecting, you will use Transact-SQL (T-SQL) queries to create and populate a small database.

> [!TIP]
> **Sqlcmd** is just one tool for connecting to SQL Server to run queries and perform management and development tasks. For other tools such as SQL Server Management Studio and Visual Studio Code, see the [Develop](sql-server-linux-develop-overview.md) and [Manage](sql-server-linux-management-overview.md) areas. 

## Install the SQL Server command-line tools

**Sqlcmd** is part of the SQL Server command-line tools, which are not installed automatically with SQL Server on Linux. If you have not already installed the SQL Server command-line tools on your Linux machine, you must install them. For more information on how to install the tools, follow the instructions for your Linux distribution:

- [Red Hat Enterprise Linux](sql-server-linux-setup-tools.md#RHEL)
- [Ubuntu](sql-server-linux-setup-tools.md#ubuntu)
- [SLES](sql-server-linux-setup-tools.md#SLES)
- [MacOS](sql-server-linux-setup-tools.md#macos)

## Connection requirements
To connect to SQL Server on Linux, you must use SQL Authentication (username and password). To connect remotely, you must ensure that the port SQL Server listens on is open. By default, SQL Server listens on TCP port 1433. Depending on your Linux distribution and configuration, you might have to open this port in the firewall. 

## Connect to SQL Server on Linux

In the following steps, connect to SQL Server 2017 on Linux with sqlcmd.

> [!TIP] 
> On macOS, use [sql-cli](https://www.npmjs.com/package/sql-cli) because sqlcmd and bcp are not available.

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
   > If you get a connection failure, first attempt to diagnose the problem from the error message. Then review the [connection troubleshooting recommendations](sql-server-linux-troubleshooting-guide.md#connection).

## Query SQL Server

After you connect to SQL Server you can run queries to return information or create database objects. If you are new to writing queries, see [Writing Transact-SQL Statements](https://msdn.microsoft.com/library/ms365303.aspx). In the following steps, you will use sqlcmd to:

1. Query SQL Server for a list of the databases.

1. Use Transact SQL to create a database.

1. Create and populate a table in the new database. 

1. Query the table.

To to complete each of these tasks, copy the Transact-SQL from the examples below into the sqlcmd session that you created in the previous step. 

For example, this query returns the name of all of the databases.

```sql
SELECT Name from sys.Databases;
GO
```

Create a database using the SQL Server default settings.

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

In this walk-through you connected to SQL Server with sqlcmd, and created and populated a database. For more information on how to use sqlcmd.exe, see [sqlcmd Utility](https://msdn.microsoft.com/library/ms162773.aspx).

## Connect and query from Windows

It is important to note that SQL Server tools on Windows connect to SQL Server instances on Linux in the same way they would connect to any remote SQL Server instance. So, you can follow the same steps in this topic running sqlcmd.exe from a remote Windows machine. Just verify that you use the target Linux machine name or IP address rather than localhost. For other connection requirements, see [connection troubleshooting recommendations](sql-server-linux-troubleshooting-guide.md#connection).

For other tools that run on Windows but connect to SQL Server on Linux, see:
- [SQL Server Management Studio (SSMS)](sql-server-linux-develop-use-ssms.md)
- [Windows PowerShell](sql-server-linux-manage-powershell.md)
- [SQL Server Data Tools (SSDT)](sql-server-linux-develop-use-ssdt.md)

## Next Steps

If you're new to T-SQL, see [Tutorial: Writing Transact-SQL Statements](https://msdn.microsoft.com/library/ms365303.aspx) and the [Transact-SQL Reference (Database Engine)](https://msdn.microsoft.com/library/bb510741.aspx).

For other ways to connect to SQL Server on Linux, see the [Develop](sql-server-linux-develop-overview.md) and [Manage](sql-server-linux-management-overview.md) areas. 


