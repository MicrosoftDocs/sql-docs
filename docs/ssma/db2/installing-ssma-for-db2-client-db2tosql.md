---
title: "Install SSMA for Db2 client (Db2ToSQL)"
description: Learn about installation prerequisites for the SQL Server Migration Assistant (SSMA) for Db2 client and how to install.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-installation
---

# Install SSMA for Db2 client (Db2ToSQL)

The SQL Server Migration Assistant (SSMA) client consists of the program files that perform the following tasks:

- Connect to a Db2 database.
- Connect to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
- Convert Db2 database objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] syntax.
- Load the objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
- Migrate data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

This article provides the installation prerequisites and instructions for installing SSMA.

## Prerequisites

SSMA is designed to work with Db2 on z/OS version 9.0, 10.0, and later versions, Db2 on LUW version 9.8, 10.1, and later versions, Db2 for i version 7.1 and later versions, and [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] and later versions.

Before you install SSMA, make sure that the computer meets the following requirements:

- Windows 7 or later versions, or Windows Server 2008 or later versions.
- Windows Installer 3.1 or later versions.
- The [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] version 4.7.2 or a later version. You can obtain it from the [.NET Framework Developer Center](https://go.microsoft.com/fwlink/?LinkId=48882).
- Microsoft OLE DB Provider for Db2 version 5 or a later version, and connectivity to the Db2 databases that you want to migrate.
- Access to and sufficient permissions on the computer that hosts the target instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database where you're migrating database objects and data. For more information, see [Connect to SQL Server](connecting-to-sql-server-db2tosql.md).
- 4 GB of RAM is recommended.

## SSMA for Db2 Extension Pack

In this version of SSMA, you don't need a separate install on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] of the SSMA extension pack, which supports data migration, and Db2 providers to enable server-to-server connectivity.

The SSMA extension pack adds a schema the database in the specified instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The **sysdb** schema contains the tables and stored procedures that are required to migrate data, and the user-defined functions that emulate Db2 system functions.

## Microsoft OLE DB Provider for Db2

To download the OLE DB provider for Db2 version 6.0, go to [Microsoft SQL Server 2017 Feature Pack](https://www.microsoft.com/download/details.aspx?id=55992).

SSMA is a Web download. To download the latest version, see the [SQL Server Migration Assistant download page](https://aka.ms/ssmafordb2).

To install the SSMA client:

1. Double-click **SSMAforDb2_*n*.msi**, where *n* is the build number.

1. On the **Welcome** page, select **Next**.

   If you don't have the prerequisites installed, a message appears indicating that you must first install required components. Make sure that you installed all prerequisites, and then run the installation program again.

1. Read the End-User License Agreement. If you agree, select **I accept the agreement**, and then select **Next**.

1. On the **Choose Setup Type** page, select **Typical**.

1. On the **Ready to Install** page, you can enable or disable telemetry and automatic update checks every time the tool starts. Select **Install** to start the installation.

> [!IMPORTANT]  
> Please uninstall all prior versions of SSMA for Db2 before installing the new version.

The default installation location is `C:\Program Files\Microsoft SQL Server Migration Assistant for Db2`.

## Related content

- [Install SSMA Components on SQL Server (Db2ToSQL)](installing-ssma-components-on-sql-server-db2tosql.md)
- [Migrate Db2 Databases to SQL Server (Db2ToSQL)](migrating-db2-databases-to-sql-server-db2tosql.md)
