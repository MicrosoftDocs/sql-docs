---
title: "getParameterCount Method (SQLServerParameterMetaData)"
description: "getParameterCount Method (SQLServerParameterMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerParameterMetaData.getParameterCount"
apitype: "Assembly"
---
# getParameterCount Method (SQLServerParameterMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the number of parameters in the [SQLServerPreparedStatement](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) object for which this [SQLServerParameterMetaData](../../../connect/jdbc/reference/sqlserverparametermetadata-class.md) object contains information.  
  
## Syntax  
  
```  
  
public int getParameterCount()  
```  
  
## Return Value  
 An **int** that indicates the number of parameters.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getParameterCount method is specified by the getParameterCount method in the java.sql.ParameterMetaData interface.  
  
## See Also  
 [SQLServerParameterMetaData Methods](../../../connect/jdbc/reference/sqlserverparametermetadata-methods.md)   
 [SQLServerParameterMetaData Members](../../../connect/jdbc/reference/sqlserverparametermetadata-members.md)   
 [SQLServerParameterMetaData Class](../../../connect/jdbc/reference/sqlserverparametermetadata-class.md)  
  
  
