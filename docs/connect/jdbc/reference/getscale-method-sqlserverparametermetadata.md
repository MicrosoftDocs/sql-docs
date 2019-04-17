---
title: "getScale Method (SQLServerParameterMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerParameterMetaData.getScale"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 7b8d8d9c-74aa-4e6e-88f1-2fc5c74004ae
author: MightyPen
ms.author: genemi
manager: craigg
---
# getScale Method (SQLServerParameterMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the number of digits to the right of the decimal point for the designated parameter.  
  
## Syntax  
  
```  
  
public int getScale(int param)  
```  
  
#### Parameters  
 *param*  
  
 An **int** that indicates parameter index.  
  
## Return Value  
 In **int** that indicates the scale of the designated parameter.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getScale method is specified by the getScale method in the java.sql.ParameterMetaData interface.  
  
 This method gets column digits to the right of the decimal point. For types that do not have a decimal point, this method returns "0".  
  
## See Also  
 [SQLServerParameterMetaData Methods](../../../connect/jdbc/reference/sqlserverparametermetadata-methods.md)   
 [SQLServerParameterMetaData Members](../../../connect/jdbc/reference/sqlserverparametermetadata-members.md)   
 [SQLServerParameterMetaData Class](../../../connect/jdbc/reference/sqlserverparametermetadata-class.md)  
  
  
