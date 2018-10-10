---
title: "getTableTypes Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.getTableTypes"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: e0f5dc57-07b8-4811-ab1a-80a524bfdb42
author: MightyPen
ms.author: genemi
manager: craigg
---
# getTableTypes Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the table types that are available in the current database.  
  
## Syntax  
  
```  
  
public java.sql.ResultSet getTableTypes()  
```  
  
## Return Value  
 A [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getTableTypes method is specified by the getTableTypes method in the java.sql.DatabaseMetaData interface.  
  
 The result set returned by the getTableTypes method will contain the following information:  
  
|Name|Type|Description|  
|----------|----------|-----------------|  
|TABLE_TYPE|**String**|The table type.|  
  
> [!NOTE]  
>  For more information about the data returned by the getTableTypes method, see "sp_tables (Transact-SQL)" in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Books Online.  
  
## Example  
 The following example demonstrates how to use the getTableTypes method to return the table type information in the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal_md.md)] sample database, given that the database is specified in the connection String.  
  
```  
public static void executeGetTableTypes(Connection con) {  
   try {  
      DatabaseMetaData dbmd = con.getMetaData();  
      ResultSet rs = dbmd.getTableTypes();  
      ResultSetMetaData rsmd = rs.getMetaData();  
  
      // Display the result set data.  
      int cols = rsmd.getColumnCount();  
      while(rs.next()) {  
         for (int i = 1; i <= cols; i++) {  
            System.out.println(rs.getString(i));  
         }  
      }  
      rs.close();  
   }   
  
   catch (Exception e) {  
      e.printStackTrace();  
   }  
}  
```  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
