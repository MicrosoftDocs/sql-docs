---
title: "getTables Method (SQLServerDatabaseMetaData)"
description: "getTables Method (SQLServerDatabaseMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.getTables"
apitype: "Assembly"
---
# getTables Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves a description of the tables that are available in the given catalog, schema, or table name pattern.  
  
## Syntax  
  
```  
  
public java.sql.ResultSet getTables(java.lang.String catalog,  
                                    java.lang.String schema,  
                                    java.lang.String table,  
                                    java.lang.String[] types)  
```  
  
#### Parameters  
 *catalog*  
  
 A **String** that contains the catalog name. Providing a null to this parameter indicates that the catalog name does not need to be used.  
  
 *schema*  
  
 A **String** that contains the schema name pattern. Providing a null to this parameter indicates that the schema name does not need to be used.  
  
 *tableName*  
  
 A **String** that contains the table name pattern.  
  
 *types*  
  
 An array of strings that contain the types of tables to include. Null indicates that all types of tables should be included.  
  
## Return Value  
 A [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getTables method is specified by the getTables method in the java.sql.DatabaseMetaData interface.  
  
 The result set returned by the getTables method will contain the following information:  
  
|Name|Type|Description|  
|----------|----------|-----------------|  
|TABLE_CAT|**String**|The name of the database in which the specified table resides.|  
|TABLE_SCHEM|**String**|The table schema name.|  
|TABLE_NAME|**String**|The table name.|  
|TABLE_TYPE|**String**|The table type.|  
|REMARKS|**String**|The description of the table.<br /><br /> **Note:**  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not return a value for this column.|  
|TYPE_CAT|**String**|Not supported by the JDBC driver.|  
|TYPE_SCHEM|**String**|Not supported by the JDBC driver.|  
|TYPE_NAME|**String**|Not supported by the JDBC driver.|  
|SELF_REFERENCING_COL_NAME|**String**|Not supported by the JDBC driver.|  
|REF_GENERATION|**String**|Not supported by the JDBC driver.|  
  
> [!NOTE]  
>  For more information about the data returned by the getTables method, see "sp_tables (Transact-SQL)" in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Books Online.  
  
## Example  
 The following example demonstrates how to use the getTables method to return the table description information for the Person.Contact table in the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal-md.md)] sample database.  
  
```  
public static void executeGetTables(Connection con) {  
   try {  
      DatabaseMetaData dbmd = con.getMetaData();  
      ResultSet rs = dbmd.getTables("AdventureWorks", "Person", "Contact", null);  
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
  
  
