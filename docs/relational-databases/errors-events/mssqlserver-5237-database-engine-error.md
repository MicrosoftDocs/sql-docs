---
description: "MSSQLSERVER_5237"
title: "MSSQLSERVER_5237 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "5237 (Database Engine error)"
ms.assetid: 9ff28935-d1eb-47ee-99b3-1a65cb948ce7
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_5237
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|5237|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC4_INDEXED_VIEW_CHECK_QUERY_FAILED_NO_ERRORCODE|  
|Message Text|DBCC cross-rowset check failed for object 'NAME' (object ID O_ID) due to an internal query error.|  
  
## Explanation  
An internal error resulted in DBCC not being able to execute the query to check indexed views.  
  
## User Action  
Rerun the DBCC command.  
  
