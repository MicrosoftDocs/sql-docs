---
# required metadata

title: Extract, transform, and load data for SQL Server on Linux | Microsoft Docs
description: 
author: sanagama 
ms.author: sanagama 
manager: jhubbard
ms.date: 03/17/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 9dab69c7-73af-4340-aef0-de057356b791

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
# Extract, transform, and load data for SQL Server on Linux with SSIS

This topic introduces [Microsoft SQL Server Integration Services (SSIS)](https://msdn.microsoft.com/library/ms141026.aspx) which is a platform for building high performance data integration solutions, including extraction, transformation, and load (ETL) packages for data warehousing and includes:
- graphical tools and wizards for building and debugging packages
- tasks for performing workflow functions such as FTP operations, executing SQL statements, and sending e-mail messages
- a variety of data sources and destinations for extracting and loading data
- transformations for cleaning, aggregating, merging, and copying data
- a management service, the Integration Services service for administering package execution and storage
- application programming interfaces (APIs) for programming the Integration Services object model

SSIS is included in [SQL Server Data Tools (SSDT)](https://msdn.microsoft.com/en-us/library/mt204009.aspx) and is a Windows application, so use SSIS when you have a Windows machine that can connect to a remote SQL Server instance on Linux. SSIS supports Microsoft SQL Server running on-premises or in the cloud, on Linux, Windows or Docker and also Azure SQL Database and Azure SQL Data Warehouse.

To get started, download the latest version of [SQL Server Data Tools (SSDT)](https://msdn.microsoft.com/en-us/library/mt204009.aspx) and follow the tutorial [SSIS How to Create an ETL Package](https://msdn.microsoft.com/en-us/library/ms169917.aspx).

## See also
- [Learn more about SQL Server Integration Services](https://msdn.microsoft.com/en-us/library/ms141026.aspx)
- [SQL Server Integration Services (SSIS) Development and Management Tools](https://msdn.microsoft.com/en-us/library/ms140028.aspx)
- [SQL Server Integration Services Tutorials](https://msdn.microsoft.com/en-us/library/jj720568.aspx)
