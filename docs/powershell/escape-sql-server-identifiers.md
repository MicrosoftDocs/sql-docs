---
title: "Escape SQL Server Identifiers | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: scripting
ms.topic: conceptual
ms.assetid: 8a73e945-daa6-4e5d-93da-10f000f1f3a2
author: stevestein
ms.author: sstein
manager: craigg
---
# Escape SQL Server Identifiers
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

You can often use the back-tick escape character (`) to escape characters that are allowed in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] delimited identifiers but not Windows PowerShell path names. Some characters, however, cannot be escaped. For example, you cannot escape the colon character (:) in Windows PowerShell. Identifiers with that character must be encoded. Encoding is more reliable than escaping because encoding works for all characters.  

> [!NOTE]
> There are two SQL Server PowerShell modules; **SqlServer** and **SQLPS**. The **SQLPS** module is included with the SQL Server installation (for backwards compatibility), but is no longer being updated. The most up-to-date PowerShell module is the **SqlServer** module. The **SqlServer** module contains updated versions of the cmdlets in **SQLPS**, and also includes new cmdlets to support the latest SQL features.
> Previous versions of the **SqlServer** module *were* included with SQL Server Management Studio (SSMS), but only with the 16.x versions of SSMS. To use PowerShell with SSMS 17.0 and later, the **SqlServer** module must be installed from the PowerShell Gallery.
> To install the **SqlServer** module, see [Install SQL Server PowerShell](download-sql-server-ps-module.md).

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
  
  
