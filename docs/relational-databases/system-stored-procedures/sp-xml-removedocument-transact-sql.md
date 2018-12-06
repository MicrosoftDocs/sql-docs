---
title: "sp_xml_removedocument (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_xml_removedocument_TSQL"
  - "sp_xml_removedocument"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_xml_removedocument"
ms.assetid: f9dca50a-8baf-4170-90bc-e72783ce5b73
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# sp_xml_removedocument (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Removes the internal representation of the XML document specified by the document handle and invalidates the document handle.  
  
> [!NOTE]  
>  A parsed document is stored in the internal cache of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The MSXML parser (Msxmlsql.dll) uses one-eighth the total memory available for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To avoid running out of memory, run **sp_xml_removedocument** to free up the memory.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_xml_removedocument hdoc  
```  
  
## Arguments  
 *hdoc*  
 Is the handle to the newly created document. A handle that is not valid returns an error. *hdoc* is an integer.  
  
## Return Code Values  
 0 (success) or >0 (failure)  
  
## Permissions  
 Requires membership in the **public** role.  
  
## Examples  
 The following example removes the internal representation of an XML document. The handle to the document is provided as input.  
  
```  
EXEC sp_xml_removedocument @hdoc;  
```  
  
## See Also      
 <br>[System Stored Procedures (Transact-SQL)](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)
 <br>[XML Stored Procedures (Transact-SQL)](../../relational-databases/system-stored-procedures/xml-stored-procedures-transact-sql.md)
 <br>[sys.dm_exec_xml_handles (Transact-SQL)](../system-dynamic-management-views/sys-dm-exec-xml-handles-transact-sql.md)
 <br>[sp_xml_preparedocument(Transact-SQL)](../../relational-databases/system-stored-procedures/sp-xml-preparedocument-transact-sql.md)
 <br>[OPENXML (Transact-SQL)](../../t-sql/functions/openxml-transact-sql.md)
  
  
