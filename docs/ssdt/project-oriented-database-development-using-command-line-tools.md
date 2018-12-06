---
title: "Project-Oriented Database Development using Command-Line Tools | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "04/26/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 9a26def9-8fbd-43e4-9e57-414840b73ed8
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# Project-Oriented Database Development using Command-Line Tools
SQL Server Data Tools proves command-line tools that enable a number of project-oriented database development scenarios.  
  
## In This Section  
  
|||  
|-|-|  
|[SqlPackage.exe](../tools/sqlpackage.md)|This topic describes the SQLPackage.exe utility, used for the following tasks:<br /><br />-   Extract a .dacpac file from a live SQL Server database.<br />-   Publish a .dacpac file to a live SQL Server database to incrementally update the live database schema to match the .dacpac.<br />-   Compare a .dacpac file to a live SQL Server database and generate an incremental upgrade Transact\-SQL script without updating the live database.<br />-   Compare two .dacpac files to generate an incremental upgrade Transact\-SQL script.<br />-   Generate an XML report that summarizes the incremental upgrade changes that would occur if the database was incrementally upgraded.|  
|[Using MSDeploy with dbSqlPackage Provider](../ssdt/using-msdeploy-with-dbsqlpackage-provider.md)|This topic describes the [Web Deployment Tool](https://go.microsoft.com/fwlink/?LinkId=231798) provider named dbSqlPackage included with SSDT, which works with the Microsoft Internet Information Services (IIS) Web Deployment Tool (MSDeploy.exe), used for the following tasks:<br /><br />-   Extract a .dacpac file from a remote/local SQL Server or SQL Azure database.<br />-   Publish a .dacpac to a remote/local SQL Server or SQL Azure database to incrementally upgrade it.<br />-   Publish from a local SQL Server database to a remote SQL Server or SQL Azure database to incrementally upgrade the remote database.<br />-   Compare a .dacpac to a remote/local SQL Server or SQL Azure database to generate an incremental upgrade Transact\-SQL script without updating the live database.<br />-   Generate an XML report that summarizes the incremental upgrade changes that would occur if the database was incrementally upgraded.|  
  
## Related Sections  
[Project-Oriented Offline Database Development](../ssdt/project-oriented-offline-database-development.md)  
  
