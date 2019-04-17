---
title: "PH timeout Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "time limit for protocol handler wait [SQL Server]"
  - "timeout options [SQL Server], ph timeout option"
  - "protocols [SQL Server], timing out"
  - "ph timeout option"
ms.assetid: ed19a07c-83fe-4582-9c9e-41b1ce571850
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# PH timeout Server Configuration Option
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  Use the PH timeout option to specify the time, in seconds, that the full-text protocol handler should wait to connect to a database before timing out. The default value is 60 seconds. Increase the ph timeout value when connection attempts are timing out due to temporary network issues.  
  
 The full-text protocol handler is hosted in the filter daemon host and is used to fetch from SQL Server the data to be full-text indexed. For more information about full-text search components, see [Full-Text Search](../../relational-databases/search/full-text-search.md).  
  
 When attempting to fetch a data row, if the protocol handler cannot connect to SQL Server within the specified time, it reports a time-out error for that row. The full-text gatherer will retry the row later. For more information about the full-text gatherer, see [Populate Full-Text Indexes](../../relational-databases/search/populate-full-text-indexes.md).  
  
## See Also  
 [Full-Text Search](../../relational-databases/search/full-text-search.md)   
 [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)  
  
  
