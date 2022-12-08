---
title: Encode and Decode SQL Server Identifiers
description: Some characters that can appear in SQL Server-delimited identifiers are not supported in Windows PowerShell paths. Learn how to include them by representing them with their hexadecimal values.
ms.service: sql
ms.subservice: sql-server-powershell
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: matteot, drskwier
ms.custom: ""
ms.date: 10/14/2020
---

# Encode and Decode SQL Server Identifiers

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

SQL Server-delimited identifiers sometimes contain characters not supported in Windows PowerShell paths. These characters can be specified by encoding their hexadecimal values.

[!INCLUDE [sql-server-powershell-version](../includes/sql-server-powershell-version.md)]

Characters that are not supported in Windows PowerShell path names can be represented, or encoded, as the "%" character followed by the hexadecimal value for the bit pattern that represents the character, as in "**%**xx". Encoding can always be used to handle characters that are not supported in Windows PowerShell paths.

The **Encode-SqlName** cmdlet takes as input a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] identifier. It outputs a string with all the characters that are not supported by the Windows PowerShell language encoded with "%xx". The **Decode-SqlName** cmdlet takes as input an encoded [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] identifier and returns the original identifier.  

## Limitations and Restrictions

The **Encode-Sqlname** and **Decode-Sqlname** cmdlets only encode or decode the characters that are allowed in SQL Server-delimited identifiers, but are not supported in PowerShell paths. The following are the characters encoded by **Encode-SqlName** and decoded by **Decode-SqlName**:

|**Character**|\ |/|:|%|\<|>|*|?|[|]|&#124;|  
|-|-|-|-|-|-|-|-|-|-|-|-|
|**Hexadecimal Encoding**|%5C|%2F|%3A|%25|%3C|%3E|%2A|%3F|%5B|%5D|%7C|

## Encoding an Identifier  

### To encode a SQL Server identifier in a PowerShell path

- Use one of two methods to encode a SQL Server identifier:
    - Specify the hexadecimal code for the unsupported character using the syntax %XX, where XX is the hexadecimal code.
    - Pass the identifier as a quoted string to the **Encode-Sqlname** cmdlet

### Examples (Encoding)

This example specifies the encoded version of the ":" character (%3A):

```powershell
Set-Location Table%3ATest
```

Alternatively, you can use **Encode-SqlName** to build a name supported by Windows PowerShell:

```powershell
Set-Location (Encode-SqlName "Table:Test")
```

## Decoding an Identifier

### To decode a SQL Server identifier from a PowerShell path

Use the **Decode-Sqlname** cmdlet to replace the hexadecimal encodings with the characters represented by the encoding.

### Examples (Decoding)

This example returns "Table:Test":

```powershell
Decode-SqlName "Table%3ATest"
```

## See Also

- [SQL Server Identifiers in PowerShell](sql-server-identifiers-in-powershell.md)
- [SQL Server PowerShell Provider](sql-server-powershell-provider.md)
- [SQL Server PowerShell](sql-server-powershell.md)  
