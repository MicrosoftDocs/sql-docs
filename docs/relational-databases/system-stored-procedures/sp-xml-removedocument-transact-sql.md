---
title: "sp_xml_removedocument (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sp_xml_removedocument_TSQL"
  - "sp_xml_removedocument"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_xml_removedocument"
ms.assetid: f9dca50a-8baf-4170-90bc-e72783ce5b73
caps.latest.revision: 18
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sp_xml_removedocument (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Removes the internal representation of the XML document specified by the document handle and invalidates the document handle.  
  
> [!NOTE]  
>  A parsed document is stored in the internal cache of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The MSXML parser (Msxmlsql.dll) uses one-eighth the total memory available for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To avoid running out of memory, run **sp_xml_removedocument** to free up the memory.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)).|  
  
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
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [XML Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/xml-stored-procedures-transact-sql.md)   
 [sp_xml_preparedocument &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-xml-preparedocument-transact-sql.md)   
 [OPENXML &#40;Transact-SQL&#41;](../../t-sql/functions/openxml-transact-sql.md)  
  
  