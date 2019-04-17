---
title: "getColumnDisplaySize Method (SQLServerResultSetMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSetMetaData.getColumnDisplaySize"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 21c25443-bd2b-4b60-9798-4efe2c158952
author: MightyPen
ms.author: genemi
manager: craigg
---
# getColumnDisplaySize Method (SQLServerResultSetMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the normal maximum width, in characters, for the designated column.  
  
## Syntax  
  
```  
  
public int getColumnDisplaySize(int column)  
```  
  
#### Parameters  
 *column*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 An **int** that indicates the maximum width. If the width is not known, returns 0.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getColumnDisplaySize method is specified by the getColumnDisplaySize method in the java.sql.ResultSetMetaData interface.  
  
 [!INCLUDE[msCoName](../../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] JDBC Driver 3.0 has behavior changes in the COLUMN_SIZE column. See [SQLServerDatabaseMetaData.getColumns](../../../connect/jdbc/reference/getcolumns-method-sqlserverdatabasemetadata.md) for more information.  
  
## See Also  
 [SQLServerResultSetMetaData Members](../../../connect/jdbc/reference/sqlserverresultsetmetadata-members.md)   
 [SQLServerResultSetMetaData Class](../../../connect/jdbc/reference/sqlserverresultsetmetadata-class.md)  
  
  
