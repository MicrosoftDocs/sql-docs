---
title: "getParameterClassName Method (SQLServerParameterMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerParameterMetaData.getParameterClassName"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 545634d8-f06b-429a-9293-0087d758f359
author: MightyPen
ms.author: genemi
manager: craigg
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
  
## See Also  
 [SQLServerParameterMetaData Methods](../../../connect/jdbc/reference/sqlserverparametermetadata-methods.md)   
 [SQLServerParameterMetaData Members](../../../connect/jdbc/reference/sqlserverparametermetadata-members.md)   
 [SQLServerParameterMetaData Class](../../../connect/jdbc/reference/sqlserverparametermetadata-class.md)  
  
  
