---
title: "getXAConnection Method ()"
description: "getXAConnection Method ()"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerXADataSource.getXAConnection ()"
apitype: "Assembly"
---
# getXAConnection Method ()
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Tries to establish a physical database connection that can be used in a distributed transaction.  
  
## Syntax  
  
```  
  
public javax.sql.XAConnection getXAConnection()  
```  
  
## Return Value  
 An XAConnection object.  
  
## Exceptions  
 java.sql.SQLException  
  
## Remarks  
 This getXAConnection method is specified by the getXAConnection method in the javax.sql.XADataSource interface.  
  
> [!NOTE]  
>  This method is typically called by XA connection pool implementations and not by regular JDBC application code.  
  
## See Also  
 [getXAConnection Method &#40;SQLServerXADataSource&#41;](../../../connect/jdbc/reference/getxaconnection-method-sqlserverxadatasource.md)   
 [SQLServerXADataSource Methods](../../../connect/jdbc/reference/sqlserverxadatasource-methods.md)   
 [SQLServerXADataSource Members](../../../connect/jdbc/reference/sqlserverxadatasource-members.md)   
 [SQLServerXADataSource Class](../../../connect/jdbc/reference/sqlserverxadatasource-class.md)  
  
  
