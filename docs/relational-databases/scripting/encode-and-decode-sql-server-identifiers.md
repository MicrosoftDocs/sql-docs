---
title: "Encode and Decode SQL Server Identifiers | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: bb9fe0d3-e432-42d3-b324-64dc908b544a
caps.latest.revision: 7
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Encode and Decode SQL Server Identifiers
  SQL Server delimited identifiers sometimes contain characters not supported in Windows PowerShell paths. These characters can be specified by encoding their hexadecimal values.  
  
1.  **Before you begin:**  [Limitations and Restrictions](#LimitationsRestrictions)  
  
2.  **To process special characters:**  [Encoding an Identifier](#EncodeIdent), [Decoding an Identifier](#DecodeIdent)  
  
## Before You Begin  
 Characters that are not supported in Windows PowerShell path names can be represented, or encoded, as the "%" character followed by the hexadecimal value for the bit pattern that represents the character, as in "**%**xx". Encoding can always be used to handle characters that are not supported in Windows PowerShell paths.  
  
 The **Encode-SqlName** cmdlet takes as input a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] identifier. It outputs a string with all the characters that are not supported by the Windows PowerShell language encoded with "%xx". The **Decode-SqlName** cmdlet takes as input an encoded [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] identifier and returns the original identifier.  
  
###  <a name="LimitationsRestrictions"></a> Limitations and Restrictions  
 The **Encode-Sqlname** and **Decode-Sqlname** cmdlets only encode or decode the characters that are allowed in SQL Server delimited identifiers, but are not supported in PowerShell paths. These are the characters encoded by **Encode-SqlName** and decoded by **Decode-SqlName**:  
  
|||||||||||||  
|-|-|-|-|-|-|-|-|-|-|-|-|  
|**Character**|\|/|:|%|\<|>|*|?|[|]|&#124;|  
|**Hexadecimal Encoding**|%5C|%2F|%3A|%25|%3C|%3E|%2A|%3F|%5B|%5D|%7C|  
  
##  <a name="EncodeIdent"></a> Encoding an Identifier  
 **To encode a SQL Server identifier in a PowerShell path**  
  
-   Use one of two methods to encode a SQL Server identifier:  
  
    -   Specify the hexadecimal code for the unsupported character using the syntax %XX, where XX is the hexadecimal code.  
  
    -   Pass the identifier as a quoted string to the **Encode-Sqlname** cmdlet  
  
### Examples (Encoding)  
 This example specifies the encoded version of the ":" character (%3A):  
  
```  
Set-Location Table%3ATest  
```  
  
 Alternatively, you can use **Encode-SqlName** to build a name supported by Windows PowerShell:  
  
```  
Set-Location (Encode-SqlName "Table:Test")  
```  
  
##  <a name="DecodeIdent"></a> Decoding an Identifier  
 **To decode a SQL Server identifier from a PowerShell path**  
  
 Use the **Decode-Sqlname** cmdlet to replace the hexadecimal encodings with the characters represented by the encoding.  
  
### Examples (Decoding)  
 This example returns “Table:Test”:  
  
```  
Decode-SqlName "Table%3ATest"  
```  
  
## See Also  
 [SQL Server Identifiers in PowerShell](../../relational-databases/scripting/sql-server-identifiers-in-powershell.md)   
 [SQL Server PowerShell Provider](../../relational-databases/scripting/sql-server-powershell-provider.md)   
 [SQL Server PowerShell](../../relational-databases/scripting/sql-server-powershell.md)  
  
  