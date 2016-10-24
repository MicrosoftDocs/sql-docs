---
# required metadata

title: Supported features of SQL Server on Linux | SQL Server vNext CTP1
description: 
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 10-20-2016
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

For Linux, SQL Server vNext CTP1 supports the SQL Server [database engine](https://msdn.microsoft.com/library/ms187875.aspx). Exceptions and limitations are provided in the Unsupported features section. 

## Supported client tools

| Tool | Minimum version |
|-----|-----|
| [SQL Server Management Studio (SSMS) for Windows](https://msdn.microsoft.com/library/mt238290.aspx) | 13.0.11000.78 |
| [SQL Server Data Tools for Visual Studio](https://msdn.microsoft.com/en-us/library/mt204009.aspx) | 14.0.60203.0 |
| [Visual Studio Code](https://code.visualstudio.com) with the [vscode-mssql extension](https://aka.ms/vscodemssql) | Latest |

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

## Limitations
TBD

