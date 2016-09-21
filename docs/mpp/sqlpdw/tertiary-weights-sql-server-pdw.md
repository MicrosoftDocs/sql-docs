---
title: "TERTIARY_WEIGHTS (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: c6c5bf2d-4d54-4dcd-876c-6ab8619de7b0
caps.latest.revision: 6
author: BarbKess
---
# TERTIARY_WEIGHTS (SQL Server PDW)
Returns a binary string of weights for each character in a non-Unicode string expression defined with an SQL tertiary collation.  
  
> [!NOTE]  
> APS does not currently support SQL tertiary collations.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
TERTIARY_WEIGHTS(non_Unicode_character_string_expression )  
```  
  
## Arguments  
*non_Unicode_character_string_expression*  
Is a string expression of type **char**, **varchar**, or **varchar(max)** defined on a tertiary SQL collation.  
  
## Return Types  
TERTIARY_WEIGHTS returns **varbinary** when *non_Unicode_character_string_expression* is **char** or **varchar**, and returns **varbinary(max)** when *non_Unicode_character_string_expression* is **varchar(max)**.  
  
## Remarks  
  
## See Also  
[Collations &#40;SQL Server PDW&#41;](../sqlpdw/collations-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
