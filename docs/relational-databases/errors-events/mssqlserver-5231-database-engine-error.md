---
title: "MSSQLSERVER_5231 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "5231 (Database Engine error)"
ms.assetid: 6954ae84-ed0b-4f4c-9d0a-e73f3d71476c
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_5231
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|5231|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC4_DEADLOCK_SKIPPED_OBJECT|  
|Message Text|Object ID O_ID (object 'NAME'): A deadlock occurred while trying to lock this object for checking. This object has been skipped and will not be processed.|  
  
## Explanation  
A deadlock occurred when DBCC was trying to lock the object, and DBCC was chosen as the deadlock victim. The object will not be processed.  
  
## User Action  
None  
  
