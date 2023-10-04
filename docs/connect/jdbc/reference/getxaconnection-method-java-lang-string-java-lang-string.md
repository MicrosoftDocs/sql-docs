---
title: "getXAConnection Method (java.lang.String, java.lang.String)"
description: "getXAConnection Method (java.lang.String, java.lang.String)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerXADataSource.getXAConnection (java.lang.String, java.lang.String)"
apitype: "Assembly"
---
# getXAConnection Method (java.lang.String, java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Tries to establish a physical database connection using the given user name and password.  
  
## Syntax  
  
```  
  
public javax.sql.XAConnection getXAConnection(java.lang.String user,  
                                              java.lang.String password)  
```  
  
#### Parameters  
 *user*  
  
 A **String** that contains the user name.  
  
 *password*  
  
 A **String** that contains the password.  
  
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
  
  
