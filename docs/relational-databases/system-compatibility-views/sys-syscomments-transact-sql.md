---
title: "sys.syscomments (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.syscomments_TSQL"
  - "syscomments"
  - "syscomments_TSQL"
  - "sys.syscomments"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.syscomments compatibility view"
  - "syscomments system table"
ms.assetid: 767dd410-6bc9-4c4a-ab0f-6d2cf6163426
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# sys.syscomments (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains entries for each view, rule, default, trigger, CHECK constraint, DEFAULT constraint, and stored procedure within the database. The **text** column contains the original SQL definition statements.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] We recommend that you use sys.sql_modules instead. For more information, see [sys.sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|Object ID to which this text applies.|  
|**number**|**smallint**|Number within procedure grouping, if grouped.<br /><br /> 0 = Entries are not procedures.|  
|**colid**|**smallint**|Row sequence number for object definitions that are longer than 4,000 characters.|  
|**status**|**smallint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**ctext**|**varbinary(8000)**|The raw bytes of the SQL definition statement.|  
|**texttype**|**smallint**|0 = User-supplied comment<br /><br /> 1 = System-supplied comment<br /><br /> 4 = Encrypted comment|  
|**language**|**smallint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**encrypted**|**bit**|Indicates whether the procedure definition is obfuscated.<br /><br /> 0 = Not obfuscated<br /><br /> 1 = Obfuscated<br /><br /> **\*\* Important \*\*** To obfuscate stored procedure definitions, use CREATE PROCEDURE with the ENCRYPTION keyword.|  
|**compressed**|**bit**|Always returns 0. This indicates that the procedure is compressed.|  
|**text**|**nvarchar(4000)**|Actual text of the SQL definition statement.<br /><br /> The semantics of the decoded expression are equivalent to the original text; however, there are no syntactic guarantees. For example, white spaces are removed from the decoded expression.<br /><br /> This [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)]-compatible view obtains information from current [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] structures and can return more characters than the **nvarchar(4000)** definition. **sp_help** returns **nvarchar(4000)** as the data type of the text column. When working with **syscomments** consider using **nvarchar(max)**. For new development work, do not use **syscomments**.|  
  
## See Also  
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)  
  
  
