---
title: "Installing SSMA for SAP ASE client (SybaseToSQL) | Microsoft Docs"
description: Learn about installation prerequisites for SQL Server Migration Assistant (SSMA) for SAP Adaptive Server Enterprise (ASE) and how to install.
ms.custom:
  - intro-installation
ms.date: "07/14/2020"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: e770c2f2-52b9-4471-a207-0d35df41399c
author: cpichuka 
ms.author: cpichuka 
---

# Installing SSMA for SAP ASE client (SybaseToSQL)

The SSMA client consists of the program files that are used to connect to a SAP Adaptive Server Enterprise (ASE) database server and an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], convert ASE database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] syntax, load the objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], and then migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssAzure](../../includes/ssazure_md.md)].

This topic provides the installation prerequisites and instructions for installing SSMA.

## Prerequisites

SSMA is designed to work with SAP ASE 11.9.2 or later versions and all editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

Before you install SSMA, make sure that the computer meets the following requirements:

- Windows 7 or later versions, or Windows Server 2008 or later versions.
- [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Installer 3.1 or a later version.
- The [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Framework version 4.7.2 or a later version. You can obtain it from the [.NET Framework Developer Center](https://go.microsoft.com/fwlink/?LinkId=48882).
- The Sybase OLE DB/ADO.Net/ODBC provider and connectivity to the SAP ASE database server that contains the databases you want to migrate. You can install providers from the SAP ASE product media. For information about connectivity, see [Connecting to Sybase ASE &#40;SybaseToSQL&#41;](../../ssma/sybase/connecting-to-sybase-ase-sybasetosql.md).
- Access to and sufficient permissions on the computer that hosts the target instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] where you will be migrating database objects and data. For more information, see [Connecting to SQL Server &#40;SybaseToSQL&#41;](../../ssma/sybase/connecting-to-sql-server-sybasetosql.md)/[Connecting to Azure SQL Database &#40;SybaseToSQL&#41;](../../ssma/sybase/connecting-to-azure-sql-db-sybasetosql.md).
- 4 GB RAM recommended.

## Installing the SSMA for Sybase client

SSMA is a Web download. To download the latest version, see the [SQL Server Migration Assistant download page](https://aka.ms/ssmaforsybase).

To install the SSMA client:

1. Double-click **SSMAforSybase_*n*.msi**, where *n* is the build number.
2. On the Welcome page, click **Next**.

   If you do not have the prerequisites installed, a message will appear that indicates that you must first install required components. Make sure that you have installed all prerequisites, and then run the installation program again.

3. Read the End-User License Agreement. If you agree, select **I accept the agreement**, and then click **Next**.
4. On the Choose Setup Type page, click **Typical**.
5. On the **Ready to Install** page you can enable or disable telemetry and automatic update checks every time the tool starts. Click **Install** to start the installation.

> [!IMPORTANT]
> Please uninstall all prior versions of SSMA for Sybase before installing the new version.

The default installation location is `C:\Program Files\Microsoft SQL Server Migration Assistant for Sybase`.

In addition to the SSMA program files, you must also install the SSMA for Sybase Extension Pack on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Installing SSMA Components on SQL Server &#40;SybaseToSQL&#41;](../../ssma/sybase/installing-ssma-components-on-sql-server-sybasetosql.md).

## See also

- [Installing SSMA Components on SQL Server](../../ssma/sybase/installing-ssma-components-on-sql-server-sybasetosql.md)  
- [Migrating Sybase ASE Databases to SQL Server - Azure SQL Database](../../ssma/sybase/migrating-sybase-ase-databases-to-sql-server-azure-sql-db-sybasetosql.md)
