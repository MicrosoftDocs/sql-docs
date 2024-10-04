---
title: "Installing SQL Server Migration Assistant for Access (AccessToSQL)"
description: Learn about installation prerequisites for SQL Server Migration Assistant (SSMA) for Access and how to install, license, upgrade, and uninstall.
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

[!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant (SSMA) for Access is installed by using a Windows Installer-based wizard. This article provides information about installation prerequisites, a link to the latest version of SSMA, and instructions for installing, licensing, uninstalling, and upgrading SSMA.

## Prerequisites

Before you install SSMA, make sure that your system meets the following requirements:

- Windows 7 or a later version, or Windows Server 2008 or a later version.
- [!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows Installer 3.1 or a later version.
- The [!INCLUDE [msCoName](../../includes/msconame-md.md)] .NET Framework version 4.7.2 or a later version. The .NET Framework is available at [Microsoft .NET Guide](/dotnet/framework/).
- Access to and sufficient permissions on the computer that hosts the target instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] to which you are migrating database objects and data.
- Microsoft Data Access Object (DAO) provider version 12.0 or 14.0. You can install DAO provider from Microsoft Office 2010/2007 product or download it from Microsoft web site.
- 4 GB of RAM (recommended).

## Install SSMA

To download the latest version of SSMA, see the [SQL Server Migration Assistant download page](https://aka.ms/ssmaforaccess).

> [!IMPORTANT]  
> Please uninstall all prior versions of SSMA for Access before installing the new version.

To install SSMA:

1. Double-click **SSMAforAccess_*n*.msi**, where *n* is the build number.

1. On the Welcome page, select **Next**.

   If you don't have the prerequisites installed, a message appears that indicates that you must first install required components. Make sure that you have installed all prerequisites, and then run the installation program again.

1. Read the End-User License Agreement; if you agree, select **I accept the agreement**, and then select **Next**.

1. On the **Choose Setup Type** page, select **Typical**.

1. On the **Ready to Install** page, you can enable or disable telemetry and automatic update checks every time the tool starts. Select **Install** to start the installation.

The default installation location is `C:\Program Files\Microsoft SQL Server Migration Assistant for Access`.

## Uninstall SSMA for Access

Uninstall SSMA by using **Add or Remove Programs** in Control Panel. Uninstalling the program doesn't delete SSMA project files or log files.

To uninstall SSMA:

1. Select **Start**, select **Control Panel**, and then select **Add or Remove Programs**.
1. Select **Microsoft SQL Server Migration Assistant for Access**, and then select **Remove**.

## Upgrade to a later version

If you want to upgrade to a later version of SSMA for Access, you must first uninstall SSMA for Access, and then install the newer version. Follow the instructions in the Uninstalling SSMA for Access section to complete this process.

If you open a project created in an earlier version of SSMA for Access, SSMA may ask if you want to convert the project to the newer version. Select **Yes** to work with the project in the newer version of SSMA.

## See also

- [Preparing Access Databases for Migration](preparing-access-databases-for-migration-accesstosql.md)
- [Migrating Access Databases to SQL Server](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)
- [Linking Access Applications to SQL Server](linking-access-applications-to-sql-server-azure-sql-db-accesstosql.md)
