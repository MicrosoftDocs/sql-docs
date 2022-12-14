---
description: "MSSQLSERVER_16594"
title: "MSSQLSERVER_16594 | Microsoft Docs"
ms.custom: ""
ms.date: "11/24/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "16594 (Database Engine error)"
ms.assetid: 5e5acb07-16ca-4329-8210-cd2bab0c904f
author: azaricstefan
ms.author: stefanazaric
---
# MSSQLSERVER_16594

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details
  
| Attribute | Value |
| :-------- | :---- |
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|
|Event ID|16594|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|POLARIS_MULTIPLE_FILE_PATHS_DUPLICATE_PATH_RESOLVED|
|Message Text|Path '%ls' was referenced multiple times in result set '%ls'.|
  
## Explanation  

There are logical file paths which are duplicate of one another.
This error requires action from the user in order for query to succeed.
  
## User action

Remove duplicate logical file paths from the query.
