---
description: "Change Data Capture Service for Oracle by Attunity"
title: "Change Data Capture Service for Oracle by Attunity | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
ms.assetid: 22ec8a5c-9550-4d38-8a4a-485ec3e53ea8
author: chugugrace
ms.author: chugu
---
# Change Data Capture Service for Oracle by Attunity

  The CDC Service for Oracle is a Windows service that scans Oracle transaction logs and captures changes to Oracle tables of interest into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] change tables. The SQL change tables where the changes captured from Oracle are stored are the same type of change tables used in the native [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Change Data Capture feature. This makes consuming these changes as easy as consuming changes made to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases.  
  
## Installation  

> [!NOTE]
> Microsoft Change Data Capture for Oracle by Attunity supports SQL server 2019 and below.  

Download Microsoft Change Data Capture Designer and Service for Oracle by Attunity for corresponding SQL Server version from below links:

- [Microsoft SQL Server 2012 Integration Services Attunity Oracle CDC Designer/Service Feature Pack](https://www.microsoft.com/download/details.aspx?id=51606)
- [Microsoft SQL Server 2016 Integration Services Attunity Oracle CDC Designer/Service Feature Pack](https://www.microsoft.com/download/details.aspx?id=55802)
- [Microsoft SQL Server 2017 Integration Services Attunity Oracle CDC Designer/Service Feature Pack](https://www.microsoft.com/download/details.aspx?id=56610)
- [Microsoft SQL Server 2019 Integration Services Feature Pack](https://www.microsoft.com/download/details.aspx?id=100303)
  
 The CDC Service for Oracle can be installed on any supported Windows computer with access to the source Oracle database(s) being captured and the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance where the target CDC database resides. The CDC Service does not need a local installation of the Oracle database or the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, only their supported clients. For information about where to install the required database components, see **Database Prerequisites** in this topic.  
  
 The installation of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CDC Service for Oracle places the service configuration UI and the service program in the selected location. The CDC Service for Oracle is configured separately using the Oracle CDC Service Configuration Console. For more information on configuring the Oracle CDC Service, see [Change Data Capture Service for Oracle by Attunity F1 Help](../../integration-services/change-data-capture/change-data-capture-service-for-oracle-by-attunity-f1-help.md).  
  
 The CDC Service for Oracle can be installed on any supported Windows computer where the [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Native Client is installed; it does not need to be installed on the same computer where the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed.  
  
## Supported Windows Environments  
 The Change Data Capture Service for Oracle by Attunity can run in the following Windows environments:
- Windows 8 and 8.1
- Windows 10
- Windows Server 2012 and 2012 R2
- Windows Server 2016
- Windows 2019
  
## Database Prerequisites  
 To work with the CDC Service for Oracle you must install Oracle client that is compatible with Oracle database version. This is a prerequisite that should be obtained from Oracle and installed before installing the Oracle CDC Service. Additionally, you need to install the SQL Server ODBC Client using SQL Server Setup.  
  
 The CDC Service for Oracle supports the following versions:  
  
### Source Oracle Database  
  
-   Oracle Database 10g Release 2
-   Oracle Database 11g Release 1 and Release 2
-   Oracle Database 12c in classic installation (Multitenant installation is not supported)  
-   Oracle Database 18c in classic installation (Multitenant installation is not supported), for SQL server 2019 only
-   Oracle Database 19c in classic installation. (Multitenant installation is not supported), for SQL server 2019 only
  
### Target SQL Server Database  
 For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
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
  
-   [Change Data Capture Service for Oracle by Attunity System Architecture](../../integration-services/change-data-capture/change-data-capture-service-for-oracle-by-attunity-system-architecture.md)  
  
-   [The Oracle CDC Service](../../integration-services/change-data-capture/the-oracle-cdc-service.md)  
  
-   [Change Data Capture Service for Oracle by Attunity F1 Help](../../integration-services/change-data-capture/change-data-capture-service-for-oracle-by-attunity-f1-help.md)  
  
-   [Change Data Capture Service for Oracle by Attunity How to Guide](../../integration-services/change-data-capture/change-data-capture-service-for-oracle-by-attunity-how-to-guide.md)  
  
## See Also  
 [Working with the Oracle CDC Service](../../integration-services/change-data-capture/working-with-the-oracle-cdc-service.md)  
