---
title: Installing SSMA for Oracle Client (OracleToSQL)
description: Learn about installation prerequisites for the SQL Server Migration Assistant (SSMA) for Oracle client and how to install.
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
---
# Install SSMA for Oracle client (OracleToSQL)

The SQL Server Migration Assistant (SSMA) client migrates Oracle databases. [!INCLUDE [ssma-target-description](../includes/ssma-target-description.md)] The client:

- Connects to the Oracle source and the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] target.
- Converts database objects for the target.
- Loads the converted objects into the target.
- Migrates the data.

This article provides the installation prerequisites and instructions for installing SSMA.

## Prerequisites

SSMA is designed to work with Oracle 9 and later versions.

Before you install SSMA, make sure that the computer meets the following requirements:

- Windows 11 or later versions, or Windows Server 2022 or later versions.
- The [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] version 4.7.2 or a later version. [Download .NET Framework](https://dotnet.microsoft.com/download/dotnet-framework).
- Connectivity to the Oracle databases that you want to migrate.
- Access to and sufficient permissions on the computer that hosts the target instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] where you're migrating database objects and data. For more information, see [Connecting to SQL Server (OracleToSQL)](connecting-to-sql-server-oracletosql.md).
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

## Related content

- [Install SSMA components on SQL Server](installing-ssma-components-on-sql-server-oracletosql.md)
- [Migrate Oracle Databases to SQL Server](migrating-oracle-databases-to-sql-server-oracletosql.md)
