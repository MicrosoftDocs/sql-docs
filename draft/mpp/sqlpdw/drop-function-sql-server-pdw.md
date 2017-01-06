---
title: "DROP FUNCTION (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: fdc8e121-44fa-4b35-8ae7-cbd075854a9e
caps.latest.revision: 5
author: BarbKess
---
# DROP FUNCTION (SQL Server PDW)
Removes a user-defined function from the current database. User-defined functions are created by using [CREATE FUNCTION](../Topic/CREATE%20FUNCTION.md) and modified by using [ALTER FUNCTION](../Topic/ALTER%20FUNCTION.md).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions (SQL Server PDW)](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DROP FUNCTION { [ schema_name. ] function_name } [;]  
```  
  
## Arguments  
*schema_name*  
Is the name of the schema to which the user-defined function belongs. Specifying the schema name is optional.  
  
*function_name*  
Is the name of the user-defined function to be removed. The server name and database name cannot be specified.  
  
## Remarks  
DROP FUNCTION will fail if there are Transact\-SQL functions or views in the database that reference this function that were created by using SCHEMABINDING.  
  
## Permissions  
To execute DROP FUNCTION, at a minimum, a user must have ALTER permission on the schema to which the function belongs, or CONTROL permission on the function.  
  
## Examples  
  
### A. Dropping a function  
The following example drops the `ConvertInput` user-defined function from the `dbo` schema in the  current database. To create this function, see Example A in [CREATE FUNCTION](../Topic/CREATE%20FUNCTION.md).  
  
```  
DROP FUNCTION dbo.ConvertInput;  
GO  
```  
  
## See Also  
[ALTER FUNCTION](../Topic/ALTER%20FUNCTION.md)  
[CREATE FUNCTION](../Topic/CREATE%20FUNCTION.md)  
  
