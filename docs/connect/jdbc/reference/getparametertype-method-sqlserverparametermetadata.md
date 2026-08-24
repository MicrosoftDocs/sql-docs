---
title: "getParameterType Method (SQLServerParameterMetaData)"
description: "getParameterType Method (SQLServerParameterMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerParameterMetaData.getParameterType"
apitype: "Assembly"
---
# getParameterType Method (SQLServerParameterMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the SQL type of the designated parameter.  
  
## Syntax  
  
```  
  
public int getParameterType(int param)  
```  
  
#### Parameters  
 *param*  
  
 An **int** that indicates parameter index.  
  
## Return Value  
 An **int** that indicates the JDBC type code as defined in java.sql.Types.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getParameterType method is specified by the getParameterType method in the java.sql.ParameterMetaData interface.  
  
## Related content

- [SQLServerParameterMetaData Methods](sqlserverparametermetadata-methods.md)
- [SQLServerParameterMetaData Members](sqlserverparametermetadata-members.md)
- [SQLServerParameterMetaData Class](sqlserverparametermetadata-class.md)
