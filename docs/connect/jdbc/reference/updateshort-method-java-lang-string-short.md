---
title: "updateShort Method (java.lang.String, short) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSet.updateShort (java.lang.String, short)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 1e596e99-11ce-4a57-b247-e40078922036
author: MightyPen
ms.author: genemi
manager: craigg
---
# updateShort Method (java.lang.String, short)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a **short** value given the column name.  
  
## Syntax  
  
```  
  
public void updateShort(java.lang.String columnName,  
                        short x)  
```  
  
#### Parameters  
 *columnName*  
  
 A **String** that contains the column name.  
  
 *x*  
  
 A **short** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateShort method is specified by the updateShort method in the java.sql.ResultSet interface.  
  
## See Also  
 [updateShort Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/updateshort-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
