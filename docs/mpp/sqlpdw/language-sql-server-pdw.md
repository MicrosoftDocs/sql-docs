---
title: "@@LANGUAGE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 51e143f0-6bee-45ff-bf42-6737e339d6a5
caps.latest.revision: 6
author: BarbKess
---
# @@LANGUAGE (SQL Server PDW)
Returns the name of the language currently being used.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
@@LANGUAGE  
```  
  
## Return Types  
**nvarchar**  
  
## Remarks  
  
## Examples  
The following example returns the language for the current session.  
  
```  
SELECT @@LANGUAGE AS 'Language Name';  
```  
  
Here is the result set.  
  
```  
Language Name                   
------------------------------  
us_english  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
