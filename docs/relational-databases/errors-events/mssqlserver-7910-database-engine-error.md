---
description: "MSSQLSERVER_7910"
title: "MSSQLSERVER_7910 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "7910 (Database Engine error)"
ms.assetid: 017a0113-2b17-40b3-a419-30bbc43d46b8
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_7910
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|7910|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC2_REPAIR_PAGE_ALLOCATED|  
|Message Text|Repair: The page P_ID has been allocated to object ID O_ID, index ID I_ID, partition ID PN_ID, alloc unit ID A_ID (type TYPE).|  
  
## Explanation  
This is an informational message from REPAIR that states that a page has been allocated to the single-page slot array of an Index Allocation Map (IAM) page.  
  
## User Action  
None  
  
