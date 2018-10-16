---
title: "updateFloat Method (java.lang.String, float) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSet.updateFloat (java.lang.String, float)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 19a6164f-f560-4304-8466-e55f0667a3d4
author: MightyPen
ms.author: genemi
manager: craigg
---
# updateFloat Method (java.lang.String, float)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a **float** value given the column name.  
  
## Syntax  
  
```  
  
public void updateFloat(java.lang.String columnName,  
                        float x)  
```  
  
#### Parameters  
 *columnName*  
  
 A **String** that contains the column name.  
  
 *x*  
  
 A **float** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateFloat method is specified by the updateFloat method in the java.sql.ResultSet interface.  
  
## See Also  
 [updateFloat Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/updatefloat-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
