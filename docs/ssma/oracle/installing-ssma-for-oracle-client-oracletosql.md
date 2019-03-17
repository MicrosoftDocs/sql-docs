---
title: "Installing SSMA for Oracle Client (OracleToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Licensing SSMA"
ms.assetid: d5d4903d-e296-4bbf-8780-63674c4d62d5
author: "Shamikg"
ms.author: "Shamikg"
manager: "v-thobro"
---
# Installing SSMA for Oracle Client (OracleToSQL)
The SSMA client consists of the program files that perform the following tasks:  
  
-   Connect to an Oracle database.  
  
-   Connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Convert Oracle database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] syntax.  
  
-   Load the objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
This topic provides the installation prerequisites and instructions for installing SSMA.  
  
## Prerequisites  
SSMA is designed to work with Oracle 9 or later versions and all editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
Before you install SSMA, make sure that the computer meets the following requirements:  
  
-   Windows 7 or later versions, or Windows Server 2008 or later versions.  
  
-   [!INCLUDE[msCoName](../../includes/msconame_md.md)] Windows Installer 3.1 or a later version.  
  
-   The [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort_md.md)] version 4.0 or a later version. The [!INCLUDE[dnprdnshort](../../includes/dnprdnshort_md.md)] version 4.0 is available on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product media. You can also obtain it from the [.NET Framework Developer Center](https://go.microsoft.com/fwlink/?LinkId=48882).  
  
-   Oracle Client 9.0 or a later version, and connectivity to the Oracle databases that you want to migrate. The Oracle client version must be the same version as, or a later version than, the Oracle database version.  
  
    You can install the Oracle Client from the Oracle product media or from the Oracle Web site. For information about connectivity, see [Connecting to Oracle Database &#40;OracleToSQL&#41;](../../ssma/oracle/connecting-to-oracle-database-oracletosql.md).  
  
-   Access to and sufficient permissions on the computer that hosts the target instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL DB where you will be migrating database objects and data. For more information, see [Connecting to SQL Server &#40;OracleToSQL&#41;](../../ssma/oracle/connecting-to-sql-server-oracletosql.md).  
  
-   4 GB RAM recommended.  
  
## Installing the SSMA for Oracle Client  
SSMA is a Web download. To download the latest version, see the [SQL Server Migration Assistant download page](https://aka.ms/ssmafororacle).  
  
After you download the latest version, you must extract the installation files from before you can install SSMA.  
  
**To install the SSMA client**  
  
1.  Double-click SSMA for Oracle *n*.Install.exe, where *n* is the build number.  
  
2.  On the Welcome page, click **Next**.  
  
    If you do not have the prerequisites installed, a message will appear that indicates that you must first install required components. Make sure that you have installed all prerequisites, and then run the installation program again.  
  
3.  Read the End User License Agreement. If you agree, select **I accept the terms in the license agreement**, and then click **Next**.  
  
4.  On the Choose Setup Type page, click **Typical**.  
  
5.  Click **Install**.  
  
> [!IMPORTANT]  
> 1.  Please uninstall all prior versions of SSMA for Oracle before installing the new version.  
  
The default installation location is C:\Program Files\Microsoft SQL Server Migration Assistant for Oracle.  
  
In addition to the SSMA program files, you must also install the SSMA for Oracle extension pack on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Installing SSMA Components on SQL Server &#40;OracleToSQL&#41;](../../ssma/oracle/installing-ssma-components-on-sql-server-oracletosql.md).  
  
## See Also  
[Installing SSMA Components on SQL Server &#40;OracleToSQL&#41;](../../ssma/oracle/installing-ssma-components-on-sql-server-oracletosql.md)  
[Migrating Oracle Databases to SQL Server &#40;OracleToSQL&#41;](../../ssma/oracle/migrating-oracle-databases-to-sql-server-oracletosql.md)  
  
