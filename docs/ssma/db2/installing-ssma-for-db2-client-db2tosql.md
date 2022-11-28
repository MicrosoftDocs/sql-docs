---
title: "Installing SSMA for DB2 client (DB2ToSQL) | Microsoft Docs"
description: Learn about installation prerequisites for the SQL Server Migration Assistant (SSMA) for DB2 client and how to install.
ms.service: sql
ms.custom:
  - intro-installation
ms.date: "07/14/2020"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 3ae2a470-6afd-4512-b6d1-fcbe6afe88ad
author: cpichuka 
ms.author: cpichuka 
---

# Installing SSMA for DB2 client (DB2ToSQL)

The SSMA client consists of the program files that perform the following tasks:

- Connect to a DB2 database.
- Connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
- Convert DB2 database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] syntax.
- Load the objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
- Migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

This topic provides the installation prerequisites and instructions for installing SSMA.

## Prerequisites

SSMA is designed to work with DB2 on z/OS version 9.0 and 10.0, DB2 on LUW version 9.8 and 10.1 or later versions, DB2 for i version 7.1 or greater and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2012 or later versions.

Before you install SSMA, make sure that the computer meets the following requirements:

- Windows 7 or later versions, or Windows Server 2008 or later versions.
- [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Installer 3.1 or later versions.
- The [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] version 4.7.2 or a later version. You can obtain it from the [.NET Framework Developer Center](https://go.microsoft.com/fwlink/?LinkId=48882).
- Microsoft OLE DB Provider for DB2 version 5 or a later version, and connectivity to the DB2 databases that you want to migrate.
- Access to and sufficient permissions on the computer that hosts the target instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database where you will be migrating database objects and data. For more information, see [Connecting to SQL Server &#40;DB2ToSQL&#41;](../../ssma/db2/connecting-to-sql-server-db2tosql.md).
- 4 GB RAM recommended.

## Microsoft OLE DB Provider for DB2

To download the OLE DB provider for DB2 version 6.0, go to [Microsoft® SQL Server® 2017 Feature Pack](https://www.microsoft.com/download/details.aspx?id=55992).

SSMA is a Web download. To download the latest version, see the [SQL Server Migration Assistant download page](https://aka.ms/ssmafordb2).

To install the SSMA client:

1. Double-click **SSMAforDB2_*n*.msi**, where *n* is the build number.
2. On the **Welcome** page, select **Next**.

   If you don't have the prerequisites installed, a message will appear indicating that you must first install required components. Make sure that you've installed all prerequisites, and then run the installation program again.

3. Read the End-User License Agreement. If you agree, select **I accept the agreement**, and then select **Next**.
4. On the **Choose Setup Type** page, select **Typical**.
5. On the **Ready to Install** page you can enable or disable telemetry and automatic update checks every time the tool starts. Click **Install** to start the installation.

> [!IMPORTANT]
> Please uninstall all prior versions of SSMA for DB2 before installing the new version.

The default installation location is `C:\Program Files\Microsoft SQL Server Migration Assistant for DB2`.

## See also

- [Installing SSMA Components on SQL Server](../../ssma/db2/installing-ssma-components-on-sql-server-db2tosql.md)
- [Migrating DB2 Databases to SQL Server](../../ssma/db2/migrating-db2-databases-to-sql-server-db2tosql.md)
