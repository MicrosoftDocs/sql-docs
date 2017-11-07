---
title: "sys.procedures (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "procedures"
  - "sys.procedures_TSQL"
  - "sys.procedures"
  - "procedures_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.procedures catalog view"
ms.assetid: d17af274-b2dd-464e-9523-ee1f43e1455b
caps.latest.revision: 20
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sys.procedures (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

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
  
  