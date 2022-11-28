---
description: "MSSQL_ENG020572"
title: "MSSQL_ENG020572 | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: reference
helpviewer_keywords: 
  - "MSSQL_ENG020572 error"
ms.assetid: 636566db-ffcf-4109-8c11-15b8c7cb9cd9
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# MSSQL_ENG020572
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
    
## Message Details  
  
|Attribute|Value|  
|-|-|  
|Product Name|SQL Server|  
|Event ID|20572|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|Subscriber '%s' subscription to article '%s' in publication '%s' has been reinitialized after a validation failure.|  
  
## Explanation  
 The data at the Subscriber was validated against the data at the Publisher, and the data did not match; therefore validation failed. When you specified that validation should be performed, you selected the option that a subscription should be reinitialized if validation failed. Reinitializing a subscription involves applying a new snapshot at the Subscriber. For more information about validation, see [Validate Replicated Data](../../relational-databases/replication/validate-data-at-the-subscriber.md).  
  
## User Action  
 Data at the Publisher and Subscriber will match after the new snapshot is applied at the Subscriber.  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)  
  
  
