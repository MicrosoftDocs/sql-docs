---
title: Escape SQL Server Identifiers
description: Some characters that can appear in SQL Server-delimited identifiers aren't supported in Windows PowerShell paths. Learn how some of these can be escaped with the back-tick character.
ms.service: sql
ms.subservice: sql-server-powershell
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: matteot, drskwier
ms.custom: ""
ms.date: 10/14/2020
---

# Escape SQL Server Identifiers

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

You can often use the back-tick escape character (`) to escape characters that are allowed in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] delimited identifiers but not Windows PowerShell path names. Some characters, however, cannot be escaped. For example, you can't escape the colon character (:) in Windows PowerShell. Identifiers with that character must be encoded. Encoding is more reliable than escaping because encoding works for all characters.  

[!INCLUDE [sql-server-powershell-version](../includes/sql-server-powershell-version.md)]

The back-tick character (`) is usually on the key in the upper left of the keyboard, under the ESC key.  

## Examples

This is an example of escaping a # character:  

```powershell
cd SQLSERVER:\SQL\MyComputer\MyInstance\MyDatabase\MySchema\`#MyTempTable  
```

This is an example of escaping the parenthesis when specifying (local) as a computer name:  

```powershell
Set-Location SQLSERVER:\SQL\`(local`)\DEFAULT  
```

## See Also

- [SQL Server Identifiers in PowerShell](sql-server-identifiers-in-powershell.md)
- [SQL Server PowerShell Provider](sql-server-powershell-provider.md)
- [SQL Server PowerShell](sql-server-powershell.md)