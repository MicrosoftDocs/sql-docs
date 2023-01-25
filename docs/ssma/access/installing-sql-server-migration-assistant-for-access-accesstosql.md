---
title: "Installing SQL Server Migration Assistant for Access (AccessToSQL) | Microsoft Docs"
description: Learn about installation prerequisites for SQL Server Migration Assistant (SSMA) for Access and how to install, license, upgrade, and uninstall.
ms.service: sql
ms.custom:
  - intro-installation
ms.date: "07/14/2020"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
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
ms.assetid: dd50eebd-75df-4e0d-8c4d-88b511aae4c7
author: cpichuka 
ms.author: cpichuka 
---

# Installing SQL Server Migration Assistant for Access (AccessToSQL)

[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant (SSMA) for Access is installed by using a Windows Installer-based wizard. This topic provides information about installation prerequisites, a link to the latest version of SSMA, and instructions for installing, licensing, uninstalling, and upgrading SSMA.

## Prerequisites

Before you install SSMA, make sure that your system meets the following requirements:

- Windows 7 or a later version, or Windows Server 2008 or a later version.
- [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Installer 3.1 or a later version.
- The [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Framework version 4.7.2 or a later version. The .NET Framework is available at [Microsoft .NET Guide](/dotnet/framework/).
- Access to and sufficient permissions on the computer that hosts the target instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] to which you will be migrating database objects and data.
- Microsoft Data Access Object (DAO) provider version 12.0 or 14.0. You can install DAO provider from Microsoft Office 2010/2007 product or download it from Microsoft web site.
- 4 GB RAM (recommended).

## Installing SSMA

SSMA is a Web download. To download the latest version, see the [SQL Server Migration Assistant download page](https://aka.ms/ssmaforaccess).

> [!IMPORTANT]
> Please uninstall all prior versions of SSMA for Access before installing the new version.

To install SSMA:
  
1. Double-click **SSMAforAccess_*n*.msi**, where *n* is the build number.
2. On the Welcome page, click **Next**.

   If you do not have the prerequisites installed, a message appears that indicates that you must first install required components. Make sure that you have installed all prerequisites, and then run the installation program again.

3. Read the End-User License Agreement; if you agree, select **I accept the agreement**, and then click **Next**.
4. On the **Choose Setup Type** page, click **Typical**.
5. On the **Ready to Install** page you can enable or disable telemetry and automatic update checks every time the tool starts. Click **Install** to start the installation.
  
The default installation location is `C:\Program Files\Microsoft SQL Server Migration Assistant for Access`.

## Uninstalling SSMA for Access

Uninstall SSMA by using **Add or Remove Programs** in Control Panel. Be aware that uninstalling the program does not delete SSMA project files or log files.

To uninstall SSMA:

1. Click **Start**, click **Control Panel**, and then click **Add or Remove Programs**.
2. Select **Microsoft SQL Server Migration Assistant for Access**, and then click **Remove**.

## Upgrading to a later version

If you want to upgrade to a later version of SSMA for Access, you must first uninstall SSMA for Access and then install the newer version. Follow the instructions in the Uninstalling SSMA for Access section to complete this process.

If you open a project created in an earlier version of SSMA for Access, SSMA may ask if you want to convert the project to the newer version. Click **Yes** to work with the project in the newer version of SSMA.

## See also

- [Preparing Access Databases for Migration](preparing-access-databases-for-migration-accesstosql.md)
- [Migrating Access Databases to SQL Server](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)
- [Linking Access Applications to SQL Server](linking-access-applications-to-sql-server-azure-sql-db-accesstosql.md)
