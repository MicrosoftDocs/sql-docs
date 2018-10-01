---
title: "MSSQLSERVER_9790 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "9790 (Database Engine error)"
ms.assetid: 83fd379f-5deb-4f97-8cb4-282e3d3fed94
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_9790
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|9790|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SB3_XMIT_ERROR_ENTRANCE_BROKER_SINGLE_USER|  
|Message Text|Unable to route the incoming message. The system database MSDB containing routing information is in SINGLE USER mode.|  
  
## Explanation  
An error occurred while trying to classify a message received off the wire because the MSDB database was in Single User mode.  
  
## User Action  
Change MSDB to be in MULTI USER mode with the ALTER DATABASE command.  
  
