---
title: Manage SQL Server on Linux | Microsoft Docs
description: This topic provides links to common management tasks and tools for SQL Server running on Linux.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 03/17/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 6bd8eb0b-593d-467e-87ea-ab1c4dbcd1ea
ms.custom: H1Hack27Feb2017
---
# Choose the right tool to manage SQL Server on Linux

[!INCLUDE[tsql-appliesto-sslinux-only](../../docs/includes/tsql-appliesto-sslinux-only.md)]

There are several ways to manage SQL Server 2017 RC2 on Linux. The following section provide a quick overview of different management tools and techniques with pointers to more resources.

## mssql-conf 
The **mssql-conf** tool configures SQL Server on Linux. For more information, see [Configure SQL Server on Linux with mssql-conf](sql-server-linux-configure-mssql-conf.md).

## Transact-SQL

Almost everything you can do in a client tool can also be accomplished with Transact-SQL statements. SQL Server provides [Dynamic Management Views (DMVs)](https://msdn.microsoft.com/library/ms188754.aspx) that query the status and configuration of SQL Server. There are also [Transact-SQL commands](https://msdn.microsoft.com/library/bb510741.aspx) for database management tasks. You can run these commands in any client tool that supports connecting to SQL Server and running Transact-SQL queries. Examples include [sqlcmd](sql-server-linux-setup-tools.md), [Visual Studio Code](sql-server-linux-develop-use-vscode.md), and [SQL Server Management Studio](sql-server-linux-manage-ssms.md).

## SQL Server Management Studio on Windows

SQL Server Management Studio (SSMS) is a Windows application that provides a graphical user interface for managing SQL Server. Although it currently runs only on Windows, you can use it to remotely connect to your Linux SQL Server instances. For more information on using SSMS to manage SQL Server, see [Use SSMS to Manage SQL Server on Linux](sql-server-linux-manage-ssms.md).

## PowerShell

PowerShell provides a rich command-line environment to manage SQL Server on Linux. For more information, see [Use PowerShell to Manage SQL Server on Linux](sql-server-linux-manage-powershell.md).

## Next steps

For more information about SQL Server on Linux, see [SQL Server on Linux](sql-server-linux-overview.md).