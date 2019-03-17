---
title: "getXAConnection Method (java.lang.String, java.lang.String) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerXADataSource.getXAConnection (java.lang.String, java.lang.String)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 276e0093-3d42-4f73-acc4-2b5b98245b40
author: MightyPen
ms.author: genemi
manager: craigg
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
  
  
