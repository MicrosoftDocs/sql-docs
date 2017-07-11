---
title: "updateBoolean Method (int, boolean) | Microsoft Docs"
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
  - "SQLServerResultSet.updateBoolean (int, boolean)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 7937f4bb-8537-4012-af81-837f9ac123a2
caps.latest.revision: 8
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# updateBoolean Method (int, boolean)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a **boolean** value given the column index.  
  
## Syntax  
  
```  
  
public void updateBoolean(int index,  
                          boolean x)  
```  
  
#### Parameters  
 *index*  
  
 An **int** that indicates the column index.  
  
 *x*  
  
 A **boolean** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateBoolean method is specified by the updateBoolean method in the java.sql.ResultSet interface.  
  
## See Also  
 [updateBoolean Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/updateboolean-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  