---
title: "sys.securable_classes (Transact-SQL)"
description: sys.securable_classes (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "12/01/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "securable_classes_TSQL"
  - "securable_classes"
  - "sys.securable_classes_TSQL"
  - "sys.securable_classes"
helpviewer_keywords:
  - "sys.securable_classes catalog view"
dev_langs:
  - "TSQL"
ms.assetid: ae2bf589-17be-4cad-b5d5-05a34173b32d
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.securable_classes (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a list of securable classes  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**class_desc**|**sysname**|Name of the class.|  
|**class**|**int**|Numerical designation of the class.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Examples  
 The following example returns the securable classes supported by this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
```sql  
SELECT * FROM sys.securable_classes ORDER BY class;  
```  
  
## See Also  
 [Securables](../../relational-databases/security/securables.md)  
  
  
