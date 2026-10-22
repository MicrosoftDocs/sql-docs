---
title: Installing SQL Server Migration Assistant for Access (AccessToSQL)
description: Learn about installation prerequisites for SQL Server Migration Assistant (SSMA) for Access and how to install, license, upgrade, and uninstall.
author: cwebb-msft
ms.author: chweb
ms.reviewer: niball
ms.date: 04/20/2026
ms.service: sql
ms.subservice: ssma
ms.topic: install-set-up-deploy
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-installation
helpviewer_keywords:
  - "installing SSMA"
  - "instructions, installation"
  - "instructions, upgrade"
  - "licensing SSMA"
  - "prerequisites for installing SSMA"
  - "procedure, installation"
  - "procedure, licensing"
  - "procedure, upgrading"
  - "removing SSMA"
  - "Setup"
  - "uninstalling SSMA"
  - "upgrading SSMA"
---
# Install SQL Server Migration Assistant for Access (AccessToSQL)

The SQL Server Migration Assistant (SSMA) client migrates Access databases. [!INCLUDE [ssma-target-description](../includes/ssma-target-description.md)] The client:

- Connects to the Access source and the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] target.
- Converts database objects for the target.
- Loads the converted objects into the target.
- Migrates the data.

This article provides information about installation prerequisites, a link to the latest version of SSMA, and instructions for installing, licensing, uninstalling, and upgrading SSMA.

## Prerequisites

Before you install SSMA, make sure that your system meets the following requirements:

- Windows 11 or later versions, or Windows Server 2022 or later versions.
- The .NET Framework version 4.7.2 or a later version. The .NET Framework is available at [Microsoft .NET Guide](/dotnet/framework/).
- Access to and sufficient permissions on the computer that hosts the target instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] to which you're migrating database objects and data.
- Microsoft Data Access Object (DAO) provider version 12.0 or 14.0. You can install DAO provider from Microsoft Office 2010/2007 product or download it from Microsoft web site.
- 4 GB of RAM (recommended).

## Install SSMA

To download the latest version of SSMA, see the [SQL Server Migration Assistant download page](https://aka.ms/ssmaforaccess).

> [!IMPORTANT]  
> Uninstall all prior versions of SSMA for Access before installing the new version.

To install SSMA:

1. Double-click `SSMAforAccess_<n>.msi`, where `<n>` is the build number.

1. On the Welcome page, select **Next**.

   If you don't have the prerequisites installed, a message appears that indicates that you must first install required components. Make sure that you install all prerequisites, and then run the installation program again.

1. Read the End-User License Agreement; if you agree, select **I accept the agreement**, and then select **Next**.

1. On the **Choose Setup Type** page, select **Typical**.

1. On the **Ready to Install** page, you can enable or disable data collection and automatic update checks every time the tool starts. Select **Install** to start the installation.

The default installation location is `C:\Program Files\Microsoft SQL Server Migration Assistant for Access`.

## Uninstall SSMA for Access

Uninstall SSMA by using **Add or Remove Programs** in Control Panel. Uninstalling the program doesn't delete SSMA project files or log files.

To uninstall SSMA:

1. Select **Start**, select **Control Panel**, and then select **Add or Remove Programs**.
1. Select **Microsoft SQL Server Migration Assistant for Access**, and then select **Remove**.

## Upgrade to a later version

If you want to upgrade to a later version of SSMA for Access, you must first uninstall SSMA for Access, and then install the newer version. Follow the instructions in the Uninstalling SSMA for Access section to complete this process.

If you open a project created in an earlier version of SSMA for Access, SSMA might ask if you want to convert the project to the newer version. Select **Yes** to work with the project in the newer version of SSMA.

## Related content

- [Prepare Access databases for migration](preparing-access-databases-for-migration-accesstosql.md)
- [Migrate Access databases to SQL Server and Azure SQL](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)
- [Link Access applications to SQL Server and Azure SQL](linking-access-applications-to-sql-server-azure-sql-db-accesstosql.md)
