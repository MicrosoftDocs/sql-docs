---
title: "Installing SSMA for MySQL client (MySQLToSQL) | Microsoft Docs"
description: Learn about installation prerequisites for the SQL Server Migration Assistant (SSMA) for MySQL client and how to install.
ms.service: sql
ms.custom:
  - intro-installation
ms.date: "07/14/2020"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords:
  - "Installing client,Licensing"
ms.assetid: ede3128c-370d-45a5-a815-3d94eecaea30
author: cpichuka 
ms.author: cpichuka 
---

# Installing SSMA for MySQL client (MySQLToSQL)

The SSMA for MySQL client consists of the program files that perform the following tasks:

- Connect to a MySQL database.  
- Connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssAzure](../../includes/ssazure_md.md)].
- Convert the MySQL database objects to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] objects.
- Load the objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssAzure](../../includes/ssazure_md.md)].
- Migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssAzure](../../includes/ssazure_md.md)].

This topic provides the installation prerequisites and instructions for installing SSMA for MySQL client.

## Prerequisites

SSMA for MySQL is designed to work with MySQL 4.1 or later versions and all editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2012 or later, and [!INCLUDE[ssAzure](../../includes/ssazure_md.md)].

Before you install SSMA, make sure that the computer meets the following requirements:

- Windows 7 or later versions, or Windows Server 2008 or later versions.
- [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Installer 3.1 or a later version.
- The [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] version 4.7.2 or a later version. You can obtain it from the [.NET Framework Developer Center](https://go.microsoft.com/fwlink/?LinkId=48882).
- MySQL ODBC 5.1 Driver and connectivity to the MySQL databases that you want to migrate. You can install the MySQL from the MySQL Web site. For information about connectivity, see [Connecting to MySQL &#40;MySQLToSQL&#41;](../../ssma/mysql/connecting-to-mysql-mysqltosql.md).
- Access to and sufficient permissions on the computer that hosts the target instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where you will be migrating database objects and data. For more information, see [Connecting to SQL Server &#40;MySQLToSQL&#41;](../../ssma/mysql/connecting-to-sql-server-mysqltosql.md).
- In case of [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] projects, access to and sufficient permissions to the instance of [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] where you will be migrating database objects and data. For more information, see [Connecting to Azure SQL Database &#40;MySQLToSQL&#41;](../../ssma/mysql/connecting-to-azure-sql-db-mysqltosql.md).
- 4 GB RAM recommended.

## Installing SSMA for MySQL client

SSMA is a Web download. To download the latest version, see the [SQL Server Migration Assistant download page](https://aka.ms/ssmaformysql).

To install the SSMA client:

1. Double-click **SSMAforMySQL_*n*.msi**, where *n* is the build number.
2. On the **Welcome** page, click **Next**.

   If you do not have the prerequisites installed, a message will appear indicating that you must first install the required components. Make sure that you have installed all the prerequisites before running the installation program again.

3. Read the End-User License Agreement. If you agree, select **I accept the agreement**, and then click **Next**.
4. On the **Choose Setup Type** page, click **Typical**.
5. On the **Ready to Install** page you can enable or disable telemetry and automatic update checks every time the tool starts. Click **Install** to start the installation.

> [!IMPORTANT]
> Please uninstall all prior versions of SSMA for MySQL before installing the new version.

The default installation location is `C:\Program Files\Microsoft SQL Server Migration Assistant for MySQL`.

## See also

- [Migrating MySQL Databases to SQL Server - Azure SQL Database](../../ssma/mysql/migrating-mysql-databases-to-sql-server-azure-sql-db-mysqltosql.md)  
