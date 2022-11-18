---
title: "sys.procedures (Transact-SQL)"
description: sys.procedures (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "procedures"
  - "sys.procedures_TSQL"
  - "sys.procedures"
  - "procedures_TSQL"
helpviewer_keywords:
  - "sys.procedures catalog view"
dev_langs:
  - "TSQL"
ms.assetid: d17af274-b2dd-464e-9523-ee1f43e1455b
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.procedures (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Contains a row for each object that is a procedure of some kind, with **sys.objects.type** = P, X, RF, and PC.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**\<Columns inherited from sys.objects>**||For a list of columns that this view inherits, see [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)|  
|**is_auto_executed**|**bit**|1 = Procedure is auto-executed at the server startup; otherwise, 0. Can only be set for procedures in the master database.|  
|**is_execution_replicated**|**bit**|Execution of this procedure is replicated.|  
|**is_repl_serializable_only**|**bit**|Replication of the procedure execution is done only when the transaction can be serialized.|  
|**skips_repl_constraints**|**bit**|During execution, the procedure skips constraints marked NOT FOR REPLICATION.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
