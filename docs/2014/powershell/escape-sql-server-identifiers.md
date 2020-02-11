---
title: "Escape SQL Server Identifiers | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: scripting
ms.topic: conceptual
ms.assetid: 8a73e945-daa6-4e5d-93da-10f000f1f3a2
author: stevestein
ms.author: sstein
manager: craigg
---
# Escape SQL Server Identifiers
  You can often use the Windows PowerShell back-tick escape character (`) to escape characters that are allowed in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] delimited identifiers but not Windows PowerShell path names. Some characters, however, cannot be escaped. For example, you cannot escape the colon character (:) in Windows PowerShell. Identifiers with that character must be encoded. Encoding is more reliable than escaping because encoding works for all characters.  
  
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
 [SQL Server Identifiers in PowerShell](sql-server-identifiers-in-powershell.md)   
 [SQL Server PowerShell Provider](sql-server-powershell-provider.md)   
 [SQL Server PowerShell](sql-server-powershell.md)  
  
  
