---
title: "HASHBYTES (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 0ce71252-2119-4558-966d-25f8c58b6a3c
caps.latest.revision: 5
author: BarbKess
---
# HASHBYTES (SQL Server PDW)
Returns the MD2, MD4, MD5, SHA, SHA1, or SHA2 hash of its input.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
HASHBYTES ('<algorithm>', { @input | 'input' } )<algorithm>::= MD2 | MD4 | MD5 | SHA | SHA1 | SHA2_256 | SHA2_512  
```  
  
## Arguments  
**'**<algorithm>**'**  
Identifies the hashing algorithm to be used to hash the input. This is a required argument with no default. The single quotation marks are required.  
  
**@input**  
Specifies a variable containing the data to be hashed. **@input** is **varchar**, **nvarchar**, or **varbinary**.  
  
**'***input***'**  
Specifies an expression that evaluates to a character or binary string to be hashed.  
  
Allowed input values are limited to 8000 bytes. The output conforms to the algorithm standard: 128 bits (16 bytes) for MD2, MD4, and MD5; 160 bits (20 bytes) for SHA and SHA1; 256 bits (32 bytes) for SHA2_256, and 512 bits (64 bytes) for SHA2_512. For more information, see [Choose an Encryption Algorithm](http://technet.microsoft.com/en-us/library/ms345262.aspx).  
  
## Return Value  
**varbinary** (maximum 8000 bytes)  
  
## Examples  
  
### Return the hash of a variable  
The following example returns the `SHA1` hash of the **nvarchar** data stored in variable `@HashThis`.  
  
```  
DECLARE @HashThis nvarchar(4000);  
SET @HashThis = CONVERT(nvarchar(4000),'dslfdkjLK85kldhnv$n000#knf');  
SELECT HASHBYTES('SHA1', @HashThis);  
```  
  
### Return the hash of a table column  
The following example returns the SHA1 hash of the values in column `c1` in the table `Test1`.  
  
```  
CREATE TABLE dbo.Test1 (c1 nvarchar(50));  
GO  
INSERT dbo.Test1 VALUES ('This is a test.'), ('This is test 2.');  
GO  
SELECT HASHBYTES('SHA1', c1) FROM dbo.Test1;  
```  
  
Here is the result set.  
  
```  
-------------------------------------------  
0x0E7AAB0B4FF0FD2DFB4F0233E2EE7A26CD08F173  
0xF643A82F948DEFB922B12E50B950CEE130A934D6  
  
(2 row(s) affected)  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
