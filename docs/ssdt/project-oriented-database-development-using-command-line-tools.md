---
title: Project-Oriented Database Development using Command-Line Tools
description: View available resources on command-line tools that SQL Server Data Tools provides for working with .dacpac files, such as SQLPackage.exe and dbSqlPackage.
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
ms.assetid: 9a26def9-8fbd-43e4-9e57-414840b73ed8
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 04/26/2017
---

# Project-Oriented Database Development using Command-Line Tools

SQL Server Data Tools proves command-line tools that enable a number of project-oriented database development scenarios.  
  
## In This Section  
  
|Topic|Description|  
|-|-|  
|[SqlPackage.exe](../tools/sqlpackage/sqlpackage.md)|This topic describes the SQLPackage.exe utility, used for the following tasks:<br /><br />-   Extract a .dacpac file from a live SQL Server database.<br />-   Publish a .dacpac file to a live SQL Server database to incrementally update the live database schema to match the .dacpac.<br />-   Compare a .dacpac file to a live SQL Server database and generate an incremental upgrade Transact\-SQL script without updating the live database.<br />-   Compare two .dacpac files to generate an incremental upgrade Transact\-SQL script.<br />-   Generate an XML report that summarizes the incremental upgrade changes that would occur if the database was incrementally upgraded.|  
|[Using MSDeploy with dbSqlPackage Provider](../ssdt/using-msdeploy-with-dbsqlpackage-provider.md)|This topic describes the [Web Deployment Tool](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/dd568996(v=ws.10)) provider named dbSqlPackage included with SSDT, which works with the Microsoft Internet Information Services (IIS) Web Deployment Tool (MSDeploy.exe), used for the following tasks:<br /><br />-   Extract a .dacpac file from a remote/local SQL Server or Azure SQL Database.<br />-   Publish a .dacpac to a remote/local SQL Server or Azure SQL Database to incrementally upgrade it.<br />-   Publish from a local SQL Server database to a remote SQL Server or Azure SQL Database to incrementally upgrade the remote database.<br />-   Compare a .dacpac to a remote/local SQL Server or Azure SQL Database to generate an incremental upgrade Transact\-SQL script without updating the live database.<br />-   Generate an XML report that summarizes the incremental upgrade changes that would occur if the database was incrementally upgraded.|  
  
## Related Sections  
[Project-Oriented Offline Database Development](../ssdt/project-oriented-offline-database-development.md)  
