---
title: "getParameterTypeName Method (SQLServerParameterMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerParameterMetaData.getParameterTypeName"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: ebe7ff0f-3cc0-408e-9503-4ca754c9c37f
author: MightyPen
ms.author: genemi
manager: craigg
---
# getParameterTypeName Method (SQLServerParameterMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the database-specific type name of the designated parameter.  
  
## Syntax  
  
```  
  
public java.lang.String getParameterTypeName(int param)  
```  
  
#### Parameters  
 *param*  
  
 An **int** that indicates parameter index.  
  
## Return Value  
 A **String** that contains type name.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getParameterTypeName method is specified by the getParameterTypeName method in the java.sql.ParameterMetaData interface.  
  
## See Also  
 [SQLServerParameterMetaData Methods](../../../connect/jdbc/reference/sqlserverparametermetadata-methods.md)   
 [SQLServerParameterMetaData Members](../../../connect/jdbc/reference/sqlserverparametermetadata-members.md)   
 [SQLServerParameterMetaData Class](../../../connect/jdbc/reference/sqlserverparametermetadata-class.md)  
  
  
