---
title: "What's New in Data Migration Assistant v3.1 | Microsoft Docs"
ms.custom: 
ms.date: "08/24/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "sql-dma"
ms.tgt_pltfrm: ""
ms.topic: "article"
keywords: ""
helpviewer_keywords: 
  - "Data Migration Assistant, new features"
ms.assetid: ""
caps.latest.revision: ""
author: "sabotta"
ms.author: "carlasab"
manager: "craigg"
---

# What's New in Data Migration Assistant v3.1

Data Migration Assistant v3.1 is a minor version update and includes following additions:

- Improved assessment recommendations for Azure SQL Databases around database collations, use of unsupported system stored procedures, and CLR objects.

- Added assessment guidance for compatibility levels 130, 120, 110 and 100 migrating to Azure SQL Databases.

Data Migration Assistant enables you to upgrade to a modern data platform by detecting compatibility issues that can impact database functionality on your new version of SQL Server. Data Migration Assistant recommends performance and reliability improvements for your target environment, and allows you to move your schema, data, and uncontained objects from your source server to your target server.

Data Migration Assistant replaces all previous versions of SQL Server Upgrade Advisor and should be used for upgrades for most SQL Server versions (see below for supported versions).

## Supported sources and target versions

**Source**
- SQL Server 2005
- SQL Server 2008
- SQL Server 2008 R2
- SQL Server 2012 
- SQL Server 2014
- SQL Server 2016

**Target**
- SQL Server 2012
- SQL Server 2014
- SQL Server 2016
- Azure SQL Database

## Installation

You can download [Data Migration Assistant](https://www.microsoft.com/download/details.aspx?id=53595) from the Microsoft Download Center and then run **DataMigrationAssistant.msi** to install.

## See Also

[Overview of Data Migration Assistant](../dma/dma-overview.md)
