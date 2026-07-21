---
title: SQL Server Configuration Manager Help
description: Get acquainted with SQL Server Configuration Manager. Learn how to use it to manage SQL Server services and configure network connectivity.
author: rwestMSFT
ms.author: randolphwest
ms.date: 03/16/2026
ms.service: sql
ms.subservice: tools-other
ms.topic: concept-article
ms.collection:
  - data-tools
helpviewer_keywords:
  - "SQL Server Configuration Manager, help"
monikerRange: ">=sql-server-2017"
---

# SQL Server Configuration Manager help

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

Use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager to configure [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services and configure network connectivity.

To create or manage database objects, configure security, and write [!INCLUDE [tsql](../../includes/tsql-md.md)] queries, use [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information, see [What is SQL Server Management Studio (SSMS)?](/ssms/sql-server-management-studio-ssms)

> [!TIP]  
> If you need to configure [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Linux, use the **mssql-conf** tool. For more information, see [Configure SQL Server on Linux with the mssql-conf tool](../../linux/sql-server-linux-configure-mssql-conf.md).

This section contains the F1 Help articles for the dialogs in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.

> [!NOTE]  
> [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager can't configure versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] earlier than [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)].

## Services

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager manages services that are related to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Although many of these tasks can be accomplished using the Windows Services dialog, is important to note that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager performs additional operations on the services it manages, such as applying the correct permissions when the service account is changed. Using the normal Windows Services dialog to configure any of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services might cause the service to malfunction.

Use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager for the following tasks for services:

- Start, stop, and pause services
- Configure services to start automatically or manually, disable the services, or change other service settings
- Change the passwords for the accounts used by the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services
- Start [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] using trace flags (command line parameters)
- View the properties of services

## SQL Server network configuration

Use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager for the following tasks related to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services on this computer:

- Enable or disable a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] network protocol
- Configure a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] network protocol

> [!NOTE]  
> For a short tutorial about how to configure protocols and connect to the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)], see [Tutorial: Get started with the Database Engine](../../relational-databases/tutorial-getting-started-with-the-database-engine.md).

## SQL Server Native Client configuration

> [!IMPORTANT]  
> [!INCLUDE [snac-removed-oledb-and-odbc](../../includes/snac-removed-oledb-and-odbc.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] clients connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by using the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client network library. Use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager for the following tasks related to client applications on this computer:

- For [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] client applications on this computer, specify the protocol order, when connecting to instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- Configure client connection protocols.

- For [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] client applications, create aliases for instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], so that clients can connect using a custom connection string.

For more information about each of these tasks, see F1 help for each task.

## Open SQL Server Configuration Manager

[!INCLUDE [open-sql-server-configuration-manager](../../includes/paragraph-content/open-sql-server-configuration-manager.md)]

## Related content

- [SQL Server Services](sql-server-services.md)
- [SQL Server Network Configuration](sql-server-network-configuration.md)
- [SQL Native Client 11.0 Configuration](sql-native-client-11-0-configuration.md)
- [Choosing a Network Protocol](/previous-versions/sql/sql-server-2016/ms187892(v=sql.130))
