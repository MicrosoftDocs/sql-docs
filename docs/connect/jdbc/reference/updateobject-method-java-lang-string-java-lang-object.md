---
title: "updateObject Method (java.lang.String, java.lang.Object) | Microsoft Docs"
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
  - "SQLServerResultSet.updateObject (java.lang.String, java.lang.Object)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: f6999d9c-eab6-4e4d-96d8-e0fa4b4b87e3
caps.latest.revision: 16
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# updateObject Method (java.lang.String, java.lang.Object)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with an **Object** value given the column name.  
  
## Syntax  
  
```  
  
public void updateObject(java.lang.String columnName,  
                         java.lang.Object x)  
```  
  
#### Parameters  
 *columnName*  
  
 A **String** that contains the column name.  
  
 *obj*  
  
 An **Object** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateObject method is specified by the updateObject method in the java.sql.ResultSet interface.  
  
## See Also  
 [updateObject Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/updateobject-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  