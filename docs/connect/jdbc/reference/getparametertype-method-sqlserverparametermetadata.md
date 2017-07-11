---
title: "getParameterType Method (SQLServerParameterMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
apiname: 
  - "SQLServerParameterMetaData.getParameterType"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 638aca05-63e4-4d73-a9c8-e0442f775720
caps.latest.revision: 7
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
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
  
## See Also  
 [SQLServerParameterMetaData Methods](../../../connect/jdbc/reference/sqlserverparametermetadata-methods.md)   
 [SQLServerParameterMetaData Members](../../../connect/jdbc/reference/sqlserverparametermetadata-members.md)   
 [SQLServerParameterMetaData Class](../../../connect/jdbc/reference/sqlserverparametermetadata-class.md)  
  
  