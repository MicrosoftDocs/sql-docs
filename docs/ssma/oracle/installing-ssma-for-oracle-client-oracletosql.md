---
title: "Installing SSMA for Oracle client (OracleToSQL)"
description: Learn about installation prerequisites for the SQL Server Migration Assistant (SSMA) for Oracle client and how to install.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 07/10/2023
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-installation
---
# Install SSMA for Oracle client (OracleToSQL)

The SSMA client consists of the program files that perform the following tasks:

- Connect to an Oracle database.
- Connect to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
- Convert Oracle database objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] syntax.
- Load the objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
- Migrate data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

This article provides the installation prerequisites and instructions for installing SSMA.

## Prerequisites

SSMA is designed to work with Oracle 9 or later versions and all editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

Before you install SSMA, make sure that the computer meets the following requirements:

- Windows 7 or later versions, or Windows Server 2008 or later versions.
- [!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows Installer 3.1 or a later version.
- The [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] version 4.7.2 or a later version. You can obtain it from the [.NET Framework Developer Center](https://go.microsoft.com/fwlink/?LinkId=48882).
- Connectivity to the Oracle databases that you want to migrate.
- Access to and sufficient permissions on the computer that hosts the target instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] where you are migrating database objects and data. For more information, see [Connecting to SQL Server (OracleToSQL)](connecting-to-sql-server-oracletosql.md).
- 4 GB of RAM recommended.

## Install the SSMA for Oracle client

SSMA is a Web download. To download the latest version, see the [SQL Server Migration Assistant download page](https://aka.ms/ssmafororacle).

To install the SSMA client:

1. Double-click **SSMAforOracle_*n*.msi**, where *n* is the build number.
1. On the Welcome page, select **Next**.

   If you don't have the prerequisites installed, a message appears that indicates that you must first install required components. Make sure that you have installed all prerequisites, and then run the installation program again.

1. Read the End-User License Agreement. If you agree, select **I accept the agreement**, and then select **Next**.
1. On the **Choose Setup Type** page, select **Typical**.
1. On the **Ready to Install** page, you can enable or disable telemetry and automatic update checks every time the tool starts. Select **Install** to start the installation.

> [!IMPORTANT]  
> Please uninstall all prior versions of SSMA for Oracle before installing the new version.

The default installation location is `C:\Program Files\Microsoft SQL Server Migration Assistant for Oracle`.

In addition to the SSMA program files, you must also install the SSMA for Oracle Extension Pack on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Installing SSMA Components on SQL Server](installing-ssma-components-on-sql-server-oracletosql.md).

## See also

- [Installing SSMA Components on SQL Server](installing-ssma-components-on-sql-server-oracletosql.md)
- [Migrating Oracle Databases to SQL Server](migrating-oracle-databases-to-sql-server-oracletosql.md)
