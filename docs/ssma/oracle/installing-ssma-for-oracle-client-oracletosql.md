---
title: "Installing SSMA for Oracle client (OracleToSQL) | Microsoft Docs"
description: Learn about installation prerequisites for the SQL Server Migration Assistant (SSMA) for Oracle client and how to install.
ms.service: sql
ms.custom:
  - intro-installation
ms.date: "07/14/2020"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: d5d4903d-e296-4bbf-8780-63674c4d62d5
author: cpichuka 
ms.author: cpichuka 
---

# Installing SSMA for Oracle client (OracleToSQL)

The SSMA client consists of the program files that perform the following tasks:  
  
- Connect to an Oracle database.
- Connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
- Convert Oracle database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] syntax.
- Load the objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
- Migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

This topic provides the installation prerequisites and instructions for installing SSMA.

## Prerequisites

SSMA is designed to work with Oracle 9 or later versions and all editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

Before you install SSMA, make sure that the computer meets the following requirements:

- Windows 7 or later versions, or Windows Server 2008 or later versions.
- [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Installer 3.1 or a later version.
- The [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] version 4.7.2 or a later version. You can obtain it from the [.NET Framework Developer Center](https://go.microsoft.com/fwlink/?LinkId=48882).
- Connectivity to the Oracle databases that you want to migrate.
- Access to and sufficient permissions on the computer that hosts the target instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] where you will be migrating database objects and data. For more information, see [Connecting to SQL Server &#40;OracleToSQL&#41;](../../ssma/oracle/connecting-to-sql-server-oracletosql.md).
- 4 GB RAM recommended.  
  
## Installing the SSMA for Oracle client

SSMA is a Web download. To download the latest version, see the [SQL Server Migration Assistant download page](https://aka.ms/ssmafororacle).

To install the SSMA client:

1. Double-click **SSMAforOracle_*n*.msi**, where *n* is the build number.
2. On the Welcome page, click **Next**.

   If you do not have the prerequisites installed, a message will appear that indicates that you must first install required components. Make sure that you have installed all prerequisites, and then run the installation program again.  

3. Read the End-User License Agreement. If you agree, select **I accept the agreement**, and then click **Next**.
4. On the **Choose Setup Type** page, click **Typical**.
5. On the **Ready to Install** page you can enable or disable telemetry and automatic update checks every time the tool starts. Click **Install** to start the installation.

> [!IMPORTANT]
> Please uninstall all prior versions of SSMA for Oracle before installing the new version.

The default installation location is `C:\Program Files\Microsoft SQL Server Migration Assistant for Oracle`.

In addition to the SSMA program files, you must also install the SSMA for Oracle Extension Pack on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Installing SSMA Components on SQL Server](../../ssma/oracle/installing-ssma-components-on-sql-server-oracletosql.md).

## See also

- [Installing SSMA Components on SQL Server](../../ssma/oracle/installing-ssma-components-on-sql-server-oracletosql.md)
- [Migrating Oracle Databases to SQL Server](../../ssma/oracle/migrating-oracle-databases-to-sql-server-oracletosql.md)
