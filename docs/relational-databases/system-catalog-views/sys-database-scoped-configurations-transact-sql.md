---
title: "sys.database_scoped_configurations (Transact-SQL) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "06/29/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "database_scoped_configurations"
  - "database_scoped_configurations_TSQL"
  - "sys.database_scoped_configurations"
  - "sys.database_scoped_configurations_TSQL"
helpviewer_keywords: 
  - "sys.database_scoped_configurations catalog view"
ms.assetid: 8899310a-3464-4d38-9f2f-88396c4e7dc2
caps.latest.revision: 13
author: "CarlRabeler"
ms.author: "carlrab"
manager: "jhubbard"
---
# sys.database_scoped_configurations (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Contains one row per configuration. 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**configuration_id**|**int**|ID of the configuration option.|  
|**name**|**nvarchar(60)**|The name of the configuration option. For information about the possible configurations, see [ALTER DATABASE SCOPED CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).|  
|**value**|**sqlvariant**|The value set for this configuration option for the primary replica.|  
|**value_for_secondary**|**sqlvariant**|The value set for this configuration option for the secondary replicas.|  
  
##  <a name="Permissions"></a> Permissions  
 Requires membership in the **public** role.  
  
## Remarks  
 When NULL is returned as the value for **value_for_secondary**, this means that the secondary is set to PRIMARY.  
  
## See Also  
 [ALTER DATABASE SCOPED CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)  
  
  
