---
title: "isSigned Method (SQLServerParameterMetaData)"
description: "isSigned Method (SQLServerParameterMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerParameterMetaData.isSigned"
apitype: "Assembly"
---
# isSigned Method (SQLServerParameterMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether values for the designated parameter can be signed numbers.  
  
## Syntax  
  
```  
  
public boolean isSigned(int param)  
```  
  
#### Parameters  
 *param*  
  
 An **int** that indicates parameter index.  
  
## Return Value  
 **true** if the designated parameter can contain signed numbers. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This isSigned method is specified by the isSigned method in the java.sql.ParameterMetaData interface.  
  
## Related content

- [SQLServerParameterMetaData Methods](sqlserverparametermetadata-methods.md)
- [SQLServerParameterMetaData Members](sqlserverparametermetadata-members.md)
- [SQLServerParameterMetaData Class](sqlserverparametermetadata-class.md)
