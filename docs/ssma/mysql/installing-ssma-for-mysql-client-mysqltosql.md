---
title: Installing SSMA for MySQL Client (MySQLToSQL)
description: Learn about installation prerequisites for the SQL Server Migration Assistant (SSMA) for MySQL client and how to install.
author: rwestMSFT
ms.author: randolphwest
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
  - "Installing client,Licensing"
---
# Install SSMA for MySQL client (MySQLToSQL)

The SQL Server Migration Assistant (SSMA) client migrates MySQL databases. [!INCLUDE [ssma-target-description](../includes/ssma-target-description.md)] The client:

- Connects to the MySQL source and the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] target.
- Converts database objects for the target.
- Loads the converted objects into the target.
- Migrates the data.

This article provides the installation prerequisites and instructions for installing SSMA for MySQL client.

## Prerequisites

SSMA for MySQL is designed to work with MySQL 4.1 and later versions.

Before you install SSMA, make sure that the computer meets the following requirements:

- Windows 11 or later versions, or Windows Server 2022 or later versions.
- The [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] version 4.7.2 or a later version. [Download .NET Framework](https://dotnet.microsoft.com/download/dotnet-framework).
- MySQL ODBC 5.1 Driver and connectivity to the MySQL databases that you want to migrate. You can install the MySQL from the MySQL Web site. For information about connectivity, see [Connecting to MySQL (MySQLToSQL)](../../ssma/mysql/connecting-to-mysql-mysqltosql.md).
- Access to and sufficient permissions on the computer that hosts the target instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] where you're migrating database objects and data. For more information, see [Connecting to SQL Server (MySQLToSQL)](../../ssma/mysql/connecting-to-sql-server-mysqltosql.md).
- For [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] projects, access to and sufficient permissions to the instance of [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] where you're migrating database objects and data. For more information, see [Connecting to Azure SQL Database (MySQLToSQL)](../../ssma/mysql/connecting-to-azure-sql-db-mysqltosql.md).
- 4 GB of RAM recommended.

## Install SSMA for MySQL client

SSMA is a Web download. To download the latest version, see the [SQL Server Migration Assistant download page](https://aka.ms/ssmaformysql).

To install the SSMA client:

1. Double-click **SSMAforMySQL_*n*.msi**, where *n* is the build number.
1. On the **Welcome** page, select **Next**.

   If you don't have the prerequisites installed, a message appears indicating that you must first install the required components. Make sure that you have installed all the prerequisites before running the installation program again.

1. Read the End-User License Agreement. If you agree, select **I accept the agreement**, and then select **Next**.
1. On the **Choose Setup Type** page, select **Typical**.
1. On the **Ready to Install** page, you can enable or disable telemetry and automatic update checks every time the tool starts. Select **Install** to start the installation.

> [!IMPORTANT]  
> Please uninstall all prior versions of SSMA for MySQL before installing the new version.

The default installation location is `C:\Program Files\Microsoft SQL Server Migration Assistant for MySQL`.

## Related content

- [Migrating MySQL Databases to SQL Server - Azure SQL Database](migrating-mysql-databases-to-sql-server-azure-sql-db-mysqltosql.md)
