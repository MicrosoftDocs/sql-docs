---
description: "MSSQLSERVER_3452"
title: "MSSQLSERVER_3452 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "3452 (Database Engine error)"
ms.assetid: bf838f02-7186-4b33-b01e-361b0c02de1f
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_3452
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|3452|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|REC_CHECKIDENTITY|  
|Message Text|Recovery of database '%.*ls' (%d) detected possible identity value inconsistency in table ID %d. Run DBCC CHECKIDENT ('%.\*ls').|  
  
## Explanation  

During an upgrade or while applying a servicing update, the process to recover the identity values in the database found an inconsistency in the metadata.  
  
## User Action

Run DBCC CHECKIDENT.  
  
