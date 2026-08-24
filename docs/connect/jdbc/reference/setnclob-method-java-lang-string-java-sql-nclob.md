---
title: "setNClob Method (java.lang.String, java.sql.NClob)"
description: "setNClob Method (java.lang.String, java.sql.NClob)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# setNClob Method (java.lang.String, java.sql.NClob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the specified NClob object.  
  
## Syntax  
  
```  
  
public final void setNClob(java.lang.String parameterName,  
              java.sql.NClob value)  
```  
  
#### Parameters  
 *parameterName*  
  
 A **String** that contains the parameter name.  
  
 *value*  
  
 A NClob object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This method should be used for **NCHAR**, **NVARCHAR**, **NTEXT**, and **XML** parameter data types.  
  
 This setNClob method is specified by the setNClob method in the java.sql.CallableStatement interface.  
  
## Related content

- [setNClob Method (SQLServerCallableStatement)](setnclob-method-sqlservercallablestatement.md)
- [SQLServerCallableStatement Members](sqlservercallablestatement-members.md)
