---
title: "registerOutParameter Method to type"
description: "registerOutParameter Method (java.lang.String, int)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerCallableStatement.registerOutParameter (java.lang.String, int)"
apitype: "Assembly"
---
# registerOutParameter Method (java.lang.String, int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Registers the OUT parameter with the specified name to the given JDBC type.  
  
## Syntax  
  
```  
  
public void registerOutParameter(java.lang.String s,  
                                 int n)  
```  
  
#### Parameters  
 *s*  
  
 A **String** that contains the parameter name.  
  
 *n*  
  
 A JDBC type code as defined in java.sql.Types.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This registerOutParameter method is specified by the registerOutParameter method in the java.sql.CallableStatement interface.  
  
## Related content

- [registerOutParameter Method (SQLServerCallableStatement)](registeroutparameter-method-sqlservercallablestatement.md)
- [SQLServerCallableStatement Members](sqlservercallablestatement-members.md)
- [SQLServerCallableStatement Class](sqlservercallablestatement-class.md)
