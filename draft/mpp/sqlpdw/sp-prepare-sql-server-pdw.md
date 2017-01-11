---
title: "sp_prepare (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 853663ce-a650-4744-a7e2-97909127e195
caps.latest.revision: 6
author: BarbKess
---
# sp_prepare (SQL Server PDW)
Prepares a parameterized Transact\-SQL statement and returns a statement *handle* for execution. sp_prepare is invoked by specifying ID = 11 in a tabular data stream (TDS) packet.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
sp_prepare handle OUTPUT, params, stmt, options  
```  
  
## Arguments  
*handle*  
Is a SQL Server-generated *prepared handle* identifier. *handle* is a required parameter with an **int** return value.  
  
*params*  
Identifies parameterized statements. The *params* definition of variables is substituted for parameter markers in the statement. *params* is a required parameter that calls for an **ntext**, **nchar**, or **nvarchar** input value. Input a NULL value if the statement is not parameterized.  
  
*stmt*  
Defines the batch statement. The *stmt* parameter is required and calls for an **ntext**, **nchar**, or **nvarchar** input value.  
  
*options*  
An optional parameter that returns a description of the result set columns. *options* requires the following **int** input value.  
  
|Value|Description|  
|---------|---------------|  
|0x0001|RETURN_METADATA|  
  
> [!NOTE]  
> Currently metadata does not display when using a question mark in the projection list.  
  
## Examples  
The following example prepares and executes a simple statement.  
  
```  
Declare @P1 int;  
Exec sp_prepare @P1 output,   
    N'@P1 nvarchar(128), @P2 nvarchar(100)',  
    N'SELECT database_id, name FROM sys.databases WHERE name=@P1 AND state_desc = @P2';  
Exec sp_execute @P1, N'tempdb', N'ONLINE';  
EXEC sp_unprepare @P1;  
```  
  
## See Also  
[Procedures &#40;SQL Server PDW&#41;](../sqlpdw/procedures-sql-server-pdw.md)  
[sp_unprepare &#40;SQL Server PDW&#41;](../sqlpdw/sp-unprepare-sql-server-pdw.md)  
[sp_execute &#40;SQL Server PDW&#41;](../sqlpdw/sp-execute-sql-server-pdw.md)  
  
