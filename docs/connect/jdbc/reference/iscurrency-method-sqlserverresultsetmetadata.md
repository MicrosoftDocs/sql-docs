---
title: "isCurrency Method (SQLServerResultSetMetaData) | Microsoft Docs"
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
  - "SQLServerResultSetMetaData.isCurrency"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 7fe25d90-693c-4d3b-9dd2-0f8351c5a9ed
caps.latest.revision: 7
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# isCurrency Method (SQLServerResultSetMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Indicates whether the designated column is a cash value.  
  
## Syntax  
  
```  
  
public boolean isCurrency(int column)  
```  
  
#### Parameters  
 *column*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 **true** if the column is a cash value. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This isCurrency method is specified by the isCurrency method in the java.sql.ResultSetMetaData interface.  
  
 This method will return **true** only with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] money and smallmoney data types.  
  
## See Also  
 [SQLServerResultSetMetaData Methods](../../../connect/jdbc/reference/sqlserverresultsetmetadata-methods.md)   
 [SQLServerResultSetMetaData Members](../../../connect/jdbc/reference/sqlserverresultsetmetadata-members.md)   
 [SQLServerResultSetMetaData Class](../../../connect/jdbc/reference/sqlserverresultsetmetadata-class.md)  
  
  