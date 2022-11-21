---
description: "MSSQLSERVER_16591"
title: "MSSQLSERVER_16591 | Microsoft Docs"
ms.custom: ""
ms.date: "11/24/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "16591 (Database Engine error)"
ms.assetid: 5e5acb07-16ca-4329-8210-cd2bab0c904f
author: azaricstefan
ms.author: stefanazaric
---
# MSSQLSERVER_16591

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details
  
| Attribute | Value |
| :-------- | :---- |
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|
|Event ID|16591|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|POLARIS_MULTIPLE_LOGICAL_FILE_PATH_LIMIT|
|Message Text|Multiple logical file paths limit has been reached. Statement contains %ld logical file paths, maximum allowed limit is %d.|
  
## Explanation  

Synapse SQL pool has a limit of maximum allowed logical file paths.
This error requires action from the user in order for query to succeed.
  
## User action  

Lower the number of logical file paths in a query bellow the limit shown in error.
