---
title: "sp_xml_removedocument (Transact-SQL)"
description: "sp_xml_removedocument (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_xml_removedocument_TSQL"
  - "sp_xml_removedocument"
helpviewer_keywords:
  - "sp_xml_removedocument"
dev_langs:
  - "TSQL"
---
# sp_xml_removedocument (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Removes the internal representation of the XML document specified by the document handle and invalidates the document handle.  
  
> [!NOTE]  
>  A parsed document is stored in the internal cache of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The MSXML parser (Msxmlsql.dll) uses one-eighth the total memory available for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To avoid running out of memory, run **sp_xml_removedocument** to free up the memory.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
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
  
  
