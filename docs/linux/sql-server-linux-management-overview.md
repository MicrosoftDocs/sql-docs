---
title: Manage SQL Server on Linux | Microsoft Docs
description: This article provides links to common management tasks and tools for SQL Server running on Linux.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 03/17/2017
ms.topic: conceptual
ms.prod: sql
ms.technology: linux
ms.assetid: 6bd8eb0b-593d-467e-87ea-ab1c4dbcd1ea
ms.custom: "sql-linux"
---
# Choose the right tool to manage SQL Server on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

There are several ways to manage SQL Server on Linux. The following section provides a quick overview of different management tools and techniques with pointers to more resources.

## mssql-conf 

The **mssql-conf** tool configures SQL Server on Linux. For more information, see [Configure SQL Server on Linux with mssql-conf](sql-server-linux-configure-mssql-conf.md).

## Transact-SQL

Almost everything you can do in a client tool can also be accomplished with Transact-SQL statements. SQL Server provides [Dynamic Management Views (DMVs)](../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md) that query the status and configuration of SQL Server. There are also [Transact-SQL commands](../t-sql/language-reference.md) for database management tasks. You can run these commands in any client tool that supports connecting to SQL Server and running Transact-SQL queries, for example [sqlcmd](sql-server-linux-setup-tools.md) or [Visual Studio Code](sql-server-linux-develop-use-vscode.md).

## Azure Data Studio

The new Azure Data Studio is a cross-platform tool for managing SQL Server. For more information, see [Azure Data Studio](../azure-data-studio/what-is.md).

## SQL Server Management Studio on Windows

SQL Server Management Studio (SSMS) is a Windows application that provides a graphical user interface for managing SQL Server. Although it currently runs only on Windows, you can use it to remotely connect to your Linux SQL Server instances. For more information on using SSMS to manage SQL Server, see [Use SSMS to Manage SQL Server on Linux](sql-server-linux-manage-ssms.md).

## mssql-cli (preview)

Microsoft has released a new cross-platform scripting tool for SQL Server, [mssql-cli](https://blogs.technet.microsoft.com/dataplatforminsider/2017/12/12/try-mssql-cli-a-new-interactive-command-line-tool-for-sql-server/). This tool is currently in preview.

## PowerShell

PowerShell provides a rich command-line environment to manage SQL Server on Linux. For more information, see [Use PowerShell to Manage SQL Server on Linux](sql-server-linux-manage-powershell.md).

## Next steps

For more information about SQL Server on Linux, see [SQL Server on Linux](sql-server-linux-overview.md).
