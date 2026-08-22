---
title: "getParameterClassName Method (SQLServerParameterMetaData)"
description: "getParameterClassName Method (SQLServerParameterMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerParameterMetaData.getParameterClassName"
apitype: "Assembly"
---
# getParameterClassName Method (SQLServerParameterMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the fully-qualified name of the Java class whose instances should be passed to the [setObject](../../../connect/jdbc/reference/setobject-method-sqlserverpreparedstatement.md) method of the [SQLServerPreparedStatement](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) class.  
  
## Syntax  
  
```  
  
public java.lang.String getParameterClassName(int param)  
```  
  
#### Parameters  
 *param*  
  
 An **int** that indicates parameter index.  
  
## Return Value  
 A **String** that contains the fully-qualified class name.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getParameterClassName method is specified by the getParameterClassName method in the java.sql.ParameterMetaData interface.  
  
## Related content

- [SQLServerParameterMetaData Methods](sqlserverparametermetadata-methods.md)
- [SQLServerParameterMetaData Members](sqlserverparametermetadata-members.md)
- [SQLServerParameterMetaData Class](sqlserverparametermetadata-class.md)
