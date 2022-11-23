---
description: "MSSQL_ENG014121"
title: "MSSQL_ENG014121 | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: reference
helpviewer_keywords: 
  - "MSSQL_ENG014121 error"
ms.assetid: c8595854-cce1-4566-ad64-d565555caded
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# MSSQL_ENG014121
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
    
## Message Details  
  
|Attribute|Value|  
|-|-|  
|Product Name|SQL Server|  
|Event ID|14121|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|Could not drop the Distributor '%s'. This Distributor has associated distribution databases.|  
  
## Explanation  
 A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that is configured as a Distributor cannot be removed from the role of Distributor because there are distribution databases associated with the instance. This error occurs if you attempt to drop a distribution database that is associated with one or more Publishers.  
  
## User Action  
 To find the names of any Publishers and distribution databases associated with this Distributor, execute [sp_helpdistpublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdistpublisher-transact-sql.md) from any database on the Distributor.  
  
 Execute [sp_dropdistributiondb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropdistributiondb-transact-sql.md) for any distribution databases associated with this Distributor. After all distribution database associations are removed, you can disable distribution.  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)   
 [Configure Distribution](../../relational-databases/replication/configure-distribution.md)  
  
  
