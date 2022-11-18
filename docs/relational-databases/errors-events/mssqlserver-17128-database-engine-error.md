---
description: "MSSQLSERVER_17128"
title: "MSSQLSERVER_17128 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "17128 (Database Engine error)"
ms.assetid: 7b15a5e6-fd41-47ce-ba87-54f72acea4bb
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_17128
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|17128|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|INIT_NOBUFSPACE|  
|Message Text|initdata: No memory for kernel buffers.|  
  
## Explanation  
The buffer pool's initial memory allocations or reservations have failed, and SQL Server exits.  
  
## User Action  
Usually caused by starting SQL Server on an extremely small machine - far smaller than the minimum system requirements.  
  
