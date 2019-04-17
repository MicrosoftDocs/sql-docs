---
title: "getFetchSize Method (SQLServerStatement) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerStatement.getFetchSize"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 8115ca58-8ae9-46ce-8515-7905d7bb25fe
author: MightyPen
ms.author: genemi
manager: craigg
---
# getFetchSize Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the number of result set rows that is the default fetch size for result set objects generated from this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.  
  
## Syntax  
  
```  
  
public final int getFetchSize()  
```  
  
## Return Value  
 An **int** that indicates the fetch size, which is specified by the [setFetchSize](../../../connect/jdbc/reference/setfetchsize-method-sqlserverstatement.md) method.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getFetchSize method is specified by the getFetchSize method in the java.sql.Statement interface.  
  
## See Also  
 [SQLServerStatement Members](../../../connect/jdbc/reference/sqlserverstatement-members.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  
