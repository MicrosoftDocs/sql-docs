---
# required metadata

title: Supported features of SQL Server on Linux | SQL Server vNext CTP1
description: 
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 10-25-2016
ms.topic: article
ms.prod: sql-non-specified
ms.service: 
ms.technology: 
ms.assetid: 

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---
# Supported features of SQL Server on Linux

This topic describes supported features and tools for SQL Server vNext CTP1 running on Linux.

The first section lists areas that are unsupported in this release. Other limitations are explained in the following sections. The last section provides version information for supported client tools. 

## Unsupported features and services

The following features and services are not available on Linux at this time.

| Area | Unsupported feature or service |
|-----|-----|
| **Database engine** | Full-text Search |
| &nbsp; | Replication|
| &nbsp; | Stretch DB|
| &nbsp; | Polybase|
| &nbsp; | Distributed Query|
| &nbsp; | Filestream|
| &nbsp; | XP_CMDSHELL|
| &nbsp; | Filetable|
| **High Availability** | Always On Availability Groups |
| &nbsp; | Clustering|
| &nbsp; | Database mirroring |
| **Security** | Active Directory authentication |
| &nbsp; | Windows Authentication |
| **Services** | SQL Server Agent |
| &nbsp; | SQL Server Browser|
| &nbsp; | SQL Server R services|
| &nbsp; | StreamInsight|
| &nbsp; | Business Intelligence Suite|
| &nbsp; | Analysis Services|
| &nbsp; | Reporting Services|
| &nbsp; | Integration Services|
| &nbsp; | Data Quality Services|
| &nbsp; | Master Data Services |

## In-Memory OLTP
In-Memory OLTP databases can only be created in the /var/opt/mssql directory. If you require more space than what is available at that location, one solution is to mount a drive under var/opt/mssql. 

## SQL Server Management Studio (SSMS)
The following limitations apply to SSMS on Windows connected to SQL Server on Linux.

- Maintenance plans are not supported.
- Management Data Warehouse (MDW) and the data collector in SSMS is not supported. 
- SSMS UI components that have Windows Authentication or Windows event log options do not work with Linux. You can still use these features with other options, such as SQL logins. 
- The SQL Server Agent only supports TSQL-based jobs. Agent functionality in SSMS which relies on other job types do not work on Linux.
- The file browser is restricted to the  “C:\” scope, which resolves to /var/opt/mssql/ on Linux. To use other paths, generate scripts of the UI operation and replace the C:\ paths with Linux paths. Then execute the script manually in SSMS.

## Supported client tools

| Tool | Minimum version |
|-----|-----|
| [SQL Server Management Studio (SSMS) for Windows](https://msdn.microsoft.com/library/mt238290.aspx) | 13.0.11000.78 |
| [SQL Server Data Tools for Visual Studio](https://msdn.microsoft.com/en-us/library/mt204009.aspx) | 14.0.60203.0 |
| [Visual Studio Code](https://code.visualstudio.com) with the [vscode-mssql extension](https://aka.ms/vscodemssql) | Latest |
