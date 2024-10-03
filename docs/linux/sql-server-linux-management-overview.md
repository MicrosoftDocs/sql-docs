---
title: Manage SQL Server on Linux
description: This article provides links to common management tasks and tools for SQL Server running on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/23/2023
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
monikerRange: ">=sql-server-linux-2017||>=sql-server-2017"
---
# Choose the right tool to manage SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

There are several ways to manage [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux. The following section provides a quick overview of different management tools and techniques with pointers to more resources.

## mssql-conf

The **mssql-conf** tool configures [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux. For more information, see [Configure SQL Server on Linux with mssql-conf](sql-server-linux-configure-mssql-conf.md).

## Transact-SQL

Almost everything you can do in a client tool can also be accomplished with Transact-SQL statements. [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] provides [Dynamic Management Views (DMVs)](../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md) that query the status and configuration of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. There are also [Transact-SQL commands](../t-sql/language-reference.md) for database management tasks. You can run these commands in any client tool that supports connecting to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] and running Transact-SQL queries, for example [sqlcmd](sql-server-linux-setup-tools.md) or [Visual Studio Code](../tools/visual-studio-code/sql-server-develop-use-vscode.md).

## Azure Data Studio

The new Azure Data Studio is a cross-platform tool for managing [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. For more information, see [Azure Data Studio](/azure-data-studio/what-is-azure-data-studio).

## Named Pipes

The Named Pipes protocol isn't supported for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux.

## SQL Server Management Studio on Windows

SQL Server Management Studio (SSMS) is a Windows application that provides a graphical user interface for managing [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. Although it currently runs only on Windows, you can use it to remotely connect to your Linux [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instances. For more information on using SSMS to manage [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see [Use SSMS to Manage SQL Server on Linux](sql-server-linux-manage-ssms.md).

## mssql-cli (preview)

Microsoft has released a new cross-platform scripting tool for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], [mssql-cli](https://blogs.technet.microsoft.com/dataplatforminsider/2017/12/12/try-mssql-cli-a-new-interactive-command-line-tool-for-sql-server/). This tool is currently in preview.

## PowerShell

PowerShell provides a rich command-line environment to manage [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux. For more information, see [Use PowerShell to Manage SQL Server on Linux](sql-server-linux-manage-powershell.md).

## Related content

- [SQL Server on Linux](sql-server-linux-overview.md)
- [Start, stop, and restart SQL Server services on Linux](sql-server-linux-start-stop-restart-sql-server-services.md)
