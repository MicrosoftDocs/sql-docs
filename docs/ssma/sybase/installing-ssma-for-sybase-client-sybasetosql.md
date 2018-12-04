---
title: "Installing SSMA  for Sybase Client (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: e770c2f2-52b9-4471-a207-0d35df41399c
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Installing SSMA  for Sybase Client (SybaseToSQL)
The SSMA client consists of the program files that are used to connect to a Sybase Adaptive Server Enterprise (ASE) database server and an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL DB, convert ASE database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL DB syntax, load the objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL DB, and then migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQLDB.  
  
This topic provides the installation prerequisites and instructions for installing SSMA.  
  
## Prerequisites  
SSMA is designed to work with ASE 11.9.2 or later versions and all editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
Before you install SSMA, make sure that the computer meets the following requirements:  
  
-   Windows 7 or later versions, or Windows Server 2008 or later versions.  
  
-   [!INCLUDE[msCoName](../../includes/msconame_md.md)] Windows Installer 3.1 or a later version.  
  
-   The [!INCLUDE[msCoName](../../includes/msconame_md.md)] .NET Framework version 4.0 or a later version. The .NET Framework version 4.0 is available on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product media. You can also obtain it from the [.NET Framework Developer Center](https://go.microsoft.com/fwlink/?LinkId=48882).  
  
-   The Sybase OLEDB/ADO.Net/ODBC provider and connectivity to the Sybase ASE database server that contains the databases you want to migrate. You can install providers from the Sybase ASE product media. For information about connectivity, see [Connecting to Sybase ASE &#40;SybaseToSQL&#41;](../../ssma/sybase/connecting-to-sybase-ase-sybasetosql.md).  
  
-   Access to and sufficient permissions on the computer that hosts the target instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL DB where you will be migrating database objects and data. For more information, see [Connecting to SQL Server &#40;SybaseToSQL&#41;](../../ssma/sybase/connecting-to-sql-server-sybasetosql.md)/[Connecting to Azure SQL DB &#40;SybaseToSQL&#41;](../../ssma/sybase/connecting-to-azure-sql-db-sybasetosql.md).  
  
-   4 GB RAM recommended.  
  
## Installing the SSMA for Sybase Client  
SSMA is a Web download. To download the latest version, see the [SQL Server Migration Assistant download page](https://aka.ms/ssmaforsybase).  
  
After you download the latest version, you must extract the installation files from before you can install SSMA.  
  
**To install the SSMA client**  
  
1.  Double-click SSMA for Sybase *n*.Install.exe, where *n* is the build number.  
  
2.  On the Welcome page, click **Next**.  
  
    If you do not have the prerequisites installed, a message will appear that indicates that you must first install required components. Make sure that you have installed all prerequisites, and then run the installation program again.  
  
3.  Read the End User License Agreement. If you agree, select **I accept the terms in the license agreement**, and then click **Next**.  
  
4.  On the Choose Setup Type page, click **Typical**.  
  
5.  Click **Install**.  
  
> [!IMPORTANT]  
> 1.  Please uninstall all prior versions of SSMA for Sybase before installing the new version.  
  
The default installation location is C:\Program Files\Microsoft SQL Server Migration Assistant  for Sybase.  
  
In addition to the SSMA program files, you must also install the SSMA for Sybase Extension Pack on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] . For more information, see [Installing SSMA Components on SQL Server &#40;SybaseToSQL&#41;](../../ssma/sybase/installing-ssma-components-on-sql-server-sybasetosql.md).  
  
## See Also  
[Installing SSMA Components on SQL Server &#40;SybaseToSQL&#41;](../../ssma/sybase/installing-ssma-components-on-sql-server-sybasetosql.md)  
[Migrating Sybase ASE Databases to SQL Server - Azure SQL DB &#40;SybaseToSQL&#41;](../../ssma/sybase/migrating-sybase-ase-databases-to-sql-server-azure-sql-db-sybasetosql.md)  
  
