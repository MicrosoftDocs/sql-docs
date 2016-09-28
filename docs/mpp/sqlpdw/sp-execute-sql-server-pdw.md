---
title: "sp_execute (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 967fd61d-2ff5-4757-b303-d83ab6e59ac5
caps.latest.revision: 4
author: BarbKess
---
# sp_execute (SQL Server PDW)
Executes a prepared Transact\-SQL statement using a specified handle and optional parameter value. sp_execute is invoked by specifying ID =12 in a tabular data stream (TDS) packet.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
sp_execute handle OUTPUT  
    [,bound_param  ]  [,...n ]  ]  
```  
  
## Arguments  
*handle*  
Is the *handle* value returned by sp_prepare. *handle* is a required parameter that calls for **int** input value.  
  
*bound_param*  
Signifies the use of additional parameters. *bound_param* is a required parameter that calls for input values of any data type to signify additional parameters for the procedure.  
  
> [!NOTE]  
> *bound_param* must match the declarations made by the sp_prepare*params* value and can be in the form *@name = value* or *value*.  
  
## See Also  
[Procedures &#40;SQL Server PDW&#41;](../sqlpdw/procedures-sql-server-pdw.md)  
[sp_prepare &#40;SQL Server PDW&#41;](../sqlpdw/sp-prepare-sql-server-pdw.md)  
[sp_unprepare &#40;SQL Server PDW&#41;](../sqlpdw/sp-unprepare-sql-server-pdw.md)  
  
