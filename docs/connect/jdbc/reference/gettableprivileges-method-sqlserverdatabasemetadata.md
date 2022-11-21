---
title: "getTablePrivileges Method (SQLServerDatabaseMetaData)"
description: "getTablePrivileges Method (SQLServerDatabaseMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.getTablePrivileges"
apitype: "Assembly"
---
# getTablePrivileges Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves a description of the access rights for each table that is available in the given catalog, schema, or table name pattern.  
  
## Syntax  
  
```  
  
public java.sql.ResultSet getTablePrivileges(java.lang.String catalog,  
                                             java.lang.String schema,  
                                             java.lang.String table)  
```  
  
#### Parameters  
 *catalog*  
  
 A **String** that contains the catalog name. Providing a null to this parameter indicates that the catalog name does not need to be used.  
  
 *schema*  
  
 A **String** that contains the schema name pattern. Providing a null to this parameter indicates that the schema name does not need to be used.  
  
 *table*  
  
 A **String** that contains the table name pattern.  
  
## Return Value  
 A [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getTablePrivileges method is specified by the getTablePrivileges method in the java.sql.DatabaseMetaData interface.  
  
 The result set returned by the getTablePrivileges method will contain the following information:  
  
|Name|Type|Description|  
|----------|----------|-----------------|  
|TABLE_CAT|**String**|The catalog name.|  
|TABLE_SCHEM|**String**|The table schema name.|  
|TABLE_NAME|**String**|The table name.|  
|GRANTOR|**String**|The object granting the access.|  
|GRANTEE|**String**|The object receiving the access.|  
|PRIVILEGE|**String**|The type of access granted.|  
|IS_GRANTABLE|**String**|Indicates if the grantee is allowed to grant access to other users.|  
  
> [!NOTE]  
>  For more information about the data returned by the getTablePrivileges method, see "sp_table_privileges (Transact-SQL)" in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Books Online.  
  
## Example  
 The following example demonstrates how to use the getTablePrivileges method to return the access rights for the Person.Contact table in the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal-md.md)] sample database.  
  
```  
public static void executeGetTablePrivileges(Connection con) {  
   try {  
      DatabaseMetaData dbmd = con.getMetaData();  
      ResultSet rs = dbmd.getTablePrivileges("AdventureWorks", "Person", "Contact");  
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
  
  
