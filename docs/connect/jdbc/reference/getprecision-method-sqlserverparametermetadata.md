---
title: "getPrecision Method (SQLServerParameterMetaData)"
description: "getPrecision Method (SQLServerParameterMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerParameterMetaData.getPrecision"
apitype: "Assembly"
---
# getPrecision Method (SQLServerParameterMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the number of decimal digits of the designated parameter.  
  
## Syntax  
  
```  
  
public int getPrecision(int param)  
```  
  
#### Parameters  
 *param*  
  
 An **int** that indicates parameter index.  
  
## Return Value  
 An **int** that indicates the precision of the designated parameter.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getPrecision method is specified by the getPrecision method in the java.sql.ParameterMetaData interface.  
  
 For number types, this method gets the number of decimal digits. For character types, it gets the maximum length in characters. For binary types, it gets the maximum length in bytes. Where the number of digits is unknown, this method returns "0".  
  
## See Also  
 [SQLServerParameterMetaData Methods](../../../connect/jdbc/reference/sqlserverparametermetadata-methods.md)   
 [SQLServerParameterMetaData Members](../../../connect/jdbc/reference/sqlserverparametermetadata-members.md)   
 [SQLServerParameterMetaData Class](../../../connect/jdbc/reference/sqlserverparametermetadata-class.md)  
  
  
