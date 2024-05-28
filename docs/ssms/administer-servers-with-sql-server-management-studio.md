---
title: Administer servers with SQL Server Management Studio
description: Administer servers with SQL Server Management Studio.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/23/2024
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords:
  - "SQL Server Management Studio [SQL Server], servers"
  - "servers [SQL Server], administering"
---

# Administer servers with SQL Server Management Studio

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE [ssmanstudiofull-md](../includes/ssmanstudiofull-md.md)] (SSMS) is a rich, integrated administrative client designed to meet the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] and Azure SQL Database administrator's server management requirements. In SSMS, administrative tasks are accomplished using Object Explorer, which allows you to connect to any server in the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] family and graphically browse its contents. A server can be an instance of the [!INCLUDE [ssDE](../includes/ssde-md.md)], [!INCLUDE [ssASnoversion](../includes/ssasnoversion-md.md)] (SSAS), [!INCLUDE [ssRSnoversion](../includes/ssrsnoversion-md.md)] (SSRS), [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] (SSIS), [!INCLUDE [ssazuremi-md](../includes/ssazuremi-md.md)], or [!INCLUDE [ssazure-sqldb](../includes/ssazure-sqldb.md)].

The tool components of SSMS include Registered Servers, Object Explorer, Solution Explorer, Template Explorer, the Object Explorer Details page, and the document window. To display a tool, on the **View** menu, select the tool name. To display the Query Editor tool, select the **New Query** button on the toolbar.

To get started, [download SQL Server Management Studio](download-sql-server-management-studio-ssms.md).

## Encrypt your connection

In [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] and earlier versions, network traffic between SSMS and [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] is unencrypted by default. Don't work with sensitive data (including passwords) in SSMS unless you establish an encrypted connection. For more information, see [How to: Enable Encrypted Connections to the Database Engine (SQL Server Configuration Manager)](../database-engine/configure-windows/configure-sql-server-encryption.md).

For more information about encryption in [!INCLUDE [ssmanstudiofull-md](../includes/ssmanstudiofull-md.md)] 20 and later versions, see [Release notes for SQL Server Management Studio (SSMS)](release-notes-ssms.md).

## SSMS features

Use SSMS to:

- Register servers
- Connect to an instance of the [!INCLUDE [ssDE](../includes/ssde-md.md)], SSAS, [!INCLUDE [ssRS](../includes/ssrs.md)], [!INCLUDE [ssIS](../includes/ssis-md.md)], [!INCLUDE [ssazuremi-md](../includes/ssazuremi-md.md)], or [!INCLUDE [ssazure-sqldb](../includes/ssazure-sqldb.md)]
- Configure server properties
- Manage database and SSAS objects such as cubes, dimensions, and assemblies
- Create objects, such as databases, tables, cubes, database users, and logins
- Manage files and filegroups
- Attach or detach databases
- Launch scripting tools
- Manage security
- View system logs
- Monitor current activity
- Configure replication
- Manage full-text indexes

To start and stop [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] or [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Agent, use [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager.

## Related content

- [Download SQL Server Management Studio (SSMS)](download-sql-server-management-studio-ssms.md)
- [Use SQL Server Management Studio](sql-server-management-studio-ssms.md)
- [How to: View server properties (SQL Server Management Studio)](../database-engine/configure-windows/view-or-change-server-properties-sql-server.md)
