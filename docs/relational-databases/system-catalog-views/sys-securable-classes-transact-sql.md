---
title: "sys.securable_classes (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "12/01/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "securable_classes_TSQL"
  - "securable_classes"
  - "sys.securable_classes_TSQL"
  - "sys.securable_classes"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.securable_classes catalog view"
ms.assetid: ae2bf589-17be-4cad-b5d5-05a34173b32d
caps.latest.revision: 16
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sys.securable_classes (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns a list of securable classes  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**class_desc**|**sysname**|Name of the class.|  
|**class**|**int**|Numerical designation of the class.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Examples  
 The following example returns the securable classes supported by this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
```tsql  
SELECT * FROM sys.securable_classes ORDER BY class;  
```  
  
## See Also  
 [Securables](../../relational-databases/security/securables.md)  
  
  
