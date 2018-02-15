---
title: "getServerPreparedStatementDiscardThreshold Method (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "drivers"
ms.service: ""
ms.component: "jdbc"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid:
caps.latest.revision: 1
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
ms.workload: "Inactive"
---
# getServerPreparedStatementDiscardThreshold Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the value of **serverPreparedStatementDiscardThreshold** connection property. This setting controls how many outstanding prepared statement discard actions (sp_unprepare) can be outstanding per connection before a call to clean-up the outstanding handles on the server is executed. If the setting is <= 1 unprepare actions will be executed immediately on prepared statement close. If it is set to > 1 these calls will be batched together to avoid overhead of calling sp_unprepare too often.

  
## Syntax  
  
```
public int getServerPreparedStatementDiscardThreshold();  
```  
  
## Return Value  
 Returns the **int** value of the **serverPreparedStatementDiscardThreshold** connection property.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  

## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
