---
title: Install SSMA for Sybase Client (SybaseToSQL)
description: Learn about installation prerequisites for the SQL Server Migration Assistant (SSMA) for SAP ASE client and how to install.
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

# Install SSMA for Sybase client (SybaseToSQL)

The SQL Server Migration Assistant (SSMA) client migrates SAP Adaptive Server Enterprise (ASE) databases. [!INCLUDE [ssma-target-description](../includes/ssma-target-description.md)] The client:

- Connects to the ASE source and the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] target.
- Converts database objects for the target.
- Loads the converted objects into the target.
- Migrates the data.

This article provides the installation prerequisites and instructions for installing SSMA.

## Prerequisites

SSMA is designed to work with SAP ASE 16.0 and later versions.

Before you install SSMA, make sure that the computer meets the following requirements:

- Windows 11 or later versions, or Windows Server 2022 or later versions.
- The [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] version 4.7.2 or a later version. [Download .NET Framework](https://dotnet.microsoft.com/download/dotnet-framework).
- The Sybase OLE DB/ADO.Net/ODBC provider and connectivity to the SAP ASE database server that contains the databases you want to migrate. You can install providers from the SAP ASE product media. For information about connectivity, see [Connecting to Sybase ASE](../../ssma/sybase/connecting-to-sybase-ase-sybasetosql.md).
- Access to and sufficient permissions on the computer that hosts the target instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] where you're migrating database objects and data. For more information, see [Connecting to SQL Server](../../ssma/sybase/connecting-to-sql-server-sybasetosql.md)/[Connecting to Azure SQL Database](../../ssma/sybase/connecting-to-azure-sql-db-sybasetosql.md).
- 4 GB RAM recommended.

<a id="installing-the-ssma-for-sybase-client"></a>

## Install the SSMA for Sybase client

SSMA is a Web download. To download the latest version, see the [SQL Server Migration Assistant download page](https://aka.ms/ssmaforsybase).

To install the SSMA client:

1. Double-click **SSMAforSybase_*n*.msi**, where *n* is the build number.
1. On the Welcome page, select **Next**.

   If you don't have the prerequisites installed, a message appears that indicates that you must first install required components. Make sure that you have installed all prerequisites, and then run the installation program again.

1. Read the End-User License Agreement. If you agree, select **I accept the agreement**, and then select **Next**.
1. On the Choose Setup Type page, select **Typical**.
1. On the **Ready to Install** page you can enable or disable telemetry and automatic update checks every time the tool starts. Select **Install** to start the installation.

> [!IMPORTANT]  
> Uninstall all prior versions of SSMA for Sybase before installing the new version.

The default installation location is `C:\Program Files\Microsoft SQL Server Migration Assistant for Sybase`.

In addition to the SSMA program files, you must also install the SSMA for Sybase Extension Pack on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Installing SSMA Components on SQL Server](../../ssma/sybase/installing-ssma-components-on-sql-server-sybasetosql.md).

## Related content

- [Install SSMA components on SQL Server](installing-ssma-components-on-sql-server-sybasetosql.md)
- [Migrating SAP ASE databases to SQL Server - Azure SQL Database](migrating-sybase-ase-databases-to-sql-server-azure-sql-db-sybasetosql.md)
