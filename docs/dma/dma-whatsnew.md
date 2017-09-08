---
title: "What's new in Data Migration Assistant (SQL Server) | Microsoft Docs"
ms.custom: 
ms.date: "09/08/2017"
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
author: "HJToland3"
ms.author: "jtoland"
manager: "craigg"
---

# What's new in Data Migration Assistant

This topic lists the additions in each release of Data Migration Assistant (DMA).

##DMA v3.2
The v3.2 release of DMA includes the following additions:

- Schema and data migration enabled from on-premises SQL Server databases to Azure SQL Database with a new migration workflow.

- During schema migration to Azure SQL Database, DMA scripts your source database objects and provides guidance on how to fix any potential compatibility issues. Then DMA deploys your schema to Azure.

##DMA v3.1
The v3.1 release of DMA includes the following additions:

- Improved assessment recommendations for Azure SQL Databases in terms of database collations, use of unsupported system stored procedures, and CLR objects.

- Assessment guidance for compatibility levels 130, 120, 110, and 100 when migrating to Azure SQL Databases.

## DMA v3.0
The v3.0 release of DMA extends the Azure SQL database assessment to provide comprehensive recommendations to help fix issues related to:

- Migration blocking issues.

- Partially or unsupported features and functions.

## DMA v2.1
The v2.1 release of DMA includes the following additions:
- Command-line support for running assessments in an unattended mode, which helps to run assessments at scale. For additional detail, refer to the topic [Run Data Migration Assistant from the command line](dma-commandline.md).

- Performance improvements when users launch and close DMA.

- The ability to configure SQL connection time-out. For additional detail, refer to the topic [Configuration settings for Data Migration Assistant](dma-configurationsettings.md).

## DMA v2.0
The v2.0 release of DMA includes improved Stretch database feature recommendations to provide proper prioritized tables that maximize the storage savings.

## DMA v1.0
The v1.0 release of DMA is the initial release, and it provides for:
- Discovery of issues that can affect an upgrade to an on-premises version of SQL Server. Any findings are described as compatibility issues, and they are categorized into the following areas:
    -	Breaking changes
    - Behavior changes
    - Deprecated features

- Discovery of new features in the target SQL Server platform that the database can benefit from an upgrade. Any findings are described as feature recommendations, and they are categorized into the following areas:
    - Performance
    - Security
    - Storage

-	Modern user experience to perform assessments.

## See also

[Overview of Data Migration Assistant](../dma/dma-overview.md)
