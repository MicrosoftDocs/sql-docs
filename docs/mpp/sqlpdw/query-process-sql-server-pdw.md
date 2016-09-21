---
title: "Query Process (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: e7f382eb-fac0-45d2-a02c-b26ecb6f6d67
caps.latest.revision: 7
author: BarbKess
---
# Query Process (SQL Server PDW)
This topic describes how SQL Server PDW processes queries in parallel across the Compute nodes.  
  
## <a name="UnderstandQueryProcess"></a>Understanding the Query Process  
User-submitted SQL queries are processed by the Control node. The Control node engine parses the query and creates a query plan that defines the sequence of operations it will use to run the query on the appliance. The Control node distributed query plan operations run serially. When a query plan operation uses multiple parallel operations, the SQL Server PDW engine waits for all parallel operations to complete before starting the next distributed query plan operation.  
  
![Parallel Query Processing](../sqlpdw/media/SQL_Server_ADW_Query_Processing.png "SQL_Server_ADW_Query_Processing")  
  
And this is the query execution sequence:  
  
![SQL Server PDW Query Execution Sequence](../sqlpdw/media/SQL_Server_PDW_Query_Execution.png "SQL_Server_PDW_Query_Execution")  
  
For more information about queries, see the [Query &#40;SQL Server PDW&#41;](../sqlpdw/query-sql-server-pdw.md) section of the documentation.  
  
