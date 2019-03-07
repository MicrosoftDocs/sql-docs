---
title: "Change Data Capture Service for Oracle by Attunity | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 22ec8a5c-9550-4d38-8a4a-485ec3e53ea8
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Change Data Capture Service for Oracle by Attunity
  The CDC Service for Oracle is a Windows service that scans Oracle transaction logs and captures changes to Oracle tables of interest into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] change tables. The SQL change tables where the changes captured from Oracle are stored are the same type of change tables used in the native [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Change Data Capture feature. This makes consuming these changes as easy as consuming changes made to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases.  
  
## Installation  
 The CDC Service for Oracle can be installed on any supported Windows computer with access to the source Oracle database(s) being captured and the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance where the target CDC database resides. The CDC Service does not need a local installation of the Oracle database or the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, only their supported clients. For information about where to install the required database components, see **Database Prerequisites** in this topic.  
  
 The installation of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CDC Service for Oracle places the service configuration UI and the service program in the selected location. The CDC Service for Oracle is configured separately using the Oracle CDC Service Configuration Console. For more information on configuring the Oracle CDC Service, see [Change Data Capture Service for Oracle by Attunity F1 Help](change-data-capture-service-for-oracle-by-attunity-f1-help.md).  
  
 To install the CDC Service for Oracle, manually run **AttunityOracleCdcService.msi** from the SQL Server installation media. Installation packages for x86 and x64 are located in **.\Tools\AttunityCDCOracle\\** on the SQL Server installation media.  
  
 The CDC Service for Oracle can be installed on any supported Windows computer where the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Native Client is installed; it does not need to be installed on the same computer where the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed.  
  
## Supported Windows Environments  
 The Change Data Capture Service for Oracle by Attunity can run in the following Windows environments:  
  
-   Windows 8  
  
-   Windows 7 32-bit (x86) and 64-bit (x64)  
  
-   Windows Server 2012  
  
-   Windows Server 2008 R2 with Service Pack 1  
  
-   Windows Server 2008 32-bit (x86) and 64-bit (x64) with Service Pack 2  
  
## Database Prerequisites  
 To work with the CDC Service for Oracle you must install the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Native Client Oracle software. This is a prerequisite that should be obtained from Oracle and installed before installing the Oracle CDC Service. Additionally, you need to install the SQL Server ODBC Client using SQL Server Setup.  
  
 The CDC Service for Oracle supports the following versions:  
  
### Source Oracle Database  
  
-   Oracle Database 11x, any version  
  
-   Oracle Database 10x, any version  
  
### Target SQL Server Database  
 For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
## Running the Installation Program  
 To install the CDC Service for Oracle, open the installation wizard for the Windows platform you are using (32/64-bit) and follow the directions on the screen.  
  
## Uninstalling Change Data Capture Service for Oracle by Attunity  
 You uninstall the CDC Service for Oracle using Control Panel, Programs and Features.  
  
 Uninstalling the CDC Service does not delete the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases created. For complete removal of the tool, you must remove the MSXDBCDC database and the specific CDC databases that were created in the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance you worked with.  
  
 If you uninstall the CDC Service software from one machine and install it on another computer, you only need to provide the following definitions:  
  
-   Service account  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connect string and credentials  
  
-   The master password  
  
 All the other definitions are stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and are available from the previous installation on another computer.  
  
## In This Documentation  
  
-   [Change Data Capture Service for Oracle by Attunity System Architecture](change-data-capture-service-for-oracle-by-attunity-system-architecture.md)  
  
-   [The Oracle CDC Service](the-oracle-cdc-service.md)  
  
-   [Change Data Capture Service for Oracle by Attunity F1 Help](change-data-capture-service-for-oracle-by-attunity-f1-help.md)  
  
-   [Change Data Capture Service for Oracle by Attunity How to Guide](change-data-capture-service-for-oracle-by-attunity-how-to-guide.md)  
  
## See Also  
 [Working with the Oracle CDC Service](working-with-the-oracle-cdc-service.md)  
  
  
