---
title: "MSSQLSERVER_7915 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "7915 (Database Engine error)"
ms.assetid: 63338587-7dd3-40e6-b70e-d8ae18fff47b
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_7915
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|7915|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC2_REPAIR_IAM_CHAIN_REBUILT|  
|Message Text|Repair: IAM chain for object ID O_ID, index ID I_ID, partition ID PN_ID, alloc unit ID A_ID (type TYPE), has been truncated before page P_ID and will be rebuilt.|  
  
## Explanation  
 This is an information message sent by REPAIR that indicates that the specified Index Allocation Map (IAM) chain was patched so that it could be rebuilt. This patching may have required the allocation of a new head of the IAM chain or the removal of bad pages from the chain.  
  
## User Action  
 None  
  
  
