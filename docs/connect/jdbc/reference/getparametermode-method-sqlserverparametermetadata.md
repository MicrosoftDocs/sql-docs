---
title: "getParameterMode Method (SQLServerParameterMetaData)"
description: "getParameterMode Method (SQLServerParameterMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerParameterMetaData.getParameterMode"
apitype: "Assembly"
---
# getParameterMode Method (SQLServerParameterMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the mode of the designated parameter.  
  
## Syntax  
  
```  
  
public int getParameterMode(int param)  
```  
  
#### Parameters  
 *param*  
  
 An **int** that indicates parameter index.  
  
## Return Value  
 An **int** that indicates the mode of the designated parameter, which can be one of the following values:  
  
 ParameterMetaData.parameterModeIn  
  
 ParameterMetaData.parameterModeInOut  
  
 ParameterMetaData.parameterModeOut  
  
 ParameterMetaData.parameterModeUnknown  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getParameterMode method is specified by the getParameterMode method in the java.sql.ParameterMetaData interface.  
  
## See Also  
 [SQLServerParameterMetaData Methods](../../../connect/jdbc/reference/sqlserverparametermetadata-methods.md)   
 [SQLServerParameterMetaData Members](../../../connect/jdbc/reference/sqlserverparametermetadata-members.md)   
 [SQLServerParameterMetaData Class](../../../connect/jdbc/reference/sqlserverparametermetadata-class.md)  
  
  
