---
title: "MSSQLSERVER_8649 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "8649 (Database Engine error)"
ms.assetid: 992dbc74-7c3a-498b-9f1b-b28387640677
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_8649
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|8649|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|COST_TOO_HIGH|  
|Message Text|The query has been canceled because the estimated cost of this query (%d) exceeds the configured threshold of %d. Contact the system administrator.|  
  
## Explanation  
The query was canceled because the estimated cost of this query exceeds the configured threshold set for the QUERY_GOVERNOR_COST_LIMIT.  
  
## User Action  
Set the QUERY_GOVERNOR_COST_LIMIT option to a higher value.  
  
## See Also  
[SET QUERY_GOVERNOR_COST_LIMIT &#40;Transact-SQL&#41;](~/t-sql/statements/set-query-governor-cost-limit-transact-sql.md)  
  
