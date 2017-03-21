---
title: "Escape SQL Server Identifiers | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 8a73e945-daa6-4e5d-93da-10f000f1f3a2
caps.latest.revision: 8
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Escape SQL Server Identifiers
  You can often use the Windows PowerShell back-tick escape character (`) to escape characters that are allowed in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] delimited identifiers but not Windows PowerShell path names. Some characters, however, cannot be escaped. For example, you cannot escape the colon character (:) in Windows PowerShell. Identifiers with that character must be encoded. Encoding is more reliable than escaping because encoding works for all characters.  
  
## Before You Begin  
 The back-tick character (`) is usually on the key in the upper left of the keyboard, under the ESC key.  
  
## Examples  
 This is an example of escaping a # character:  
  
```  
cd SQLSERVER:\SQL\MyComputer\MyInstance\MyDatabase\MySchema\`#MyTempTable  
```  
  
 This is an example of escaping the parenthesis when specifying (local) as a computer name:  
  
```  
Set-Location SQLSERVER:\SQL\`(local`)\DEFAULT  
```  
  
## See Also  
 [SQL Server Identifiers in PowerShell](../../relational-databases/scripting/sql-server-identifiers-in-powershell.md)   
 [SQL Server PowerShell Provider](../../relational-databases/scripting/sql-server-powershell-provider.md)   
 [SQL Server PowerShell](../../relational-databases/scripting/sql-server-powershell.md)  
  
  