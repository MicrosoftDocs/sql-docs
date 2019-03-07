---
title: "getColumnTypeName Method (SQLServerResultSetMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSetMetaData.getColumnTypeName"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: a444da82-c1af-40a5-9774-02476416c92c
author: MightyPen
ms.author: genemi
manager: craigg
---
# getColumnTypeName Method (SQLServerResultSetMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the database-specific type name for the designated column.  
  
## Syntax  
  
```  
  
public java.lang.String getColumnTypeName(int column)  
```  
  
#### Parameters  
 *column*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 A **String** that contains the server name for the column.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getColumnTypeName method is specified by the getColumnTypeName method in the java.sql.ResultSetMetaData interface.  
  
 [!INCLUDE[msCoName](../../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] JDBC Driver 3.0 has behavior changes in the TYPE_NAME column. See [SQLServerDatabaseMetaData.getColumns](../../../connect/jdbc/reference/getcolumns-method-sqlserverdatabasemetadata.md) for more information.  
  
## See Also  
 [SQLServerResultSetMetaData Members](../../../connect/jdbc/reference/sqlserverresultsetmetadata-members.md)   
 [SQLServerResultSetMetaData Class](../../../connect/jdbc/reference/sqlserverresultsetmetadata-class.md)  
  
  
