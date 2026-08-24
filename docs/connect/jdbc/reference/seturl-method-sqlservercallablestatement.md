---
title: "setURL Method (SQLServerCallableStatement)"
description: "setURL Method (SQLServerCallableStatement)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerCallableStatement.setURL"
apitype: "Assembly"
---
# setURL Method (SQLServerCallableStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the given URL value.  
  
## Syntax  
  
```  
  
public void setURL(java.lang.String sCol,  
                   java.net.URL u)  
```  
  
#### Parameters  
 *sCol*  
  
 A **String** that contains the name of the parameter.  
  
 *u*  
  
 A URL object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setURL method is specified by the setURL method in the java.sql.CallableStatement interface.  
  
## Related content

- [SQLServerCallableStatement Members](sqlservercallablestatement-members.md)
- [SQLServerCallableStatement Class](sqlservercallablestatement-class.md)
