---
title: "getVersionColumns Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.getVersionColumns"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 6dd275d3-d9b2-4db7-938a-d4406c940a7a
author: MightyPen
ms.author: genemi
manager: craigg
---
# getVersionColumns Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves a description of the columns of a table that is automatically updated when any value in a row is updated.  
  
## Syntax  
  
```  
  
public java.sql.ResultSet getVersionColumns(java.lang.String catalog,  
                                            java.lang.String schema,  
                                            java.lang.String table)  
```  
  
#### Parameters  
 *catalog*  
  
 A **String** that contains the catalog name.  
  
 *schema*  
  
 A **String** that contains the schema name pattern.  
  
 *table*  
  
 A **String** that contains the table name.  
  
## Return Value  
 A [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getVersionColumns method is specified by the getVersionColumns method in the java.sql.DatabaseMetaData interface.  
  
 The result set returned by the getVersionColumns method will contain the following information:  
  
|Name|Type|Description|  
|----------|----------|-----------------|  
|SCOPE|**short**|Not supported by the JDBC driver.|  
|COLUMN_NAME|**String**|The column name.|  
|DATA_TYPE|**short**|The SQL data type from java.sql.Types.|  
|TYPE_NAME|**String**|The name of the data type.|  
|COLUMN_SIZE|**int**|The precision of the column.|  
|BUFFER_LENGTH|**int**|The length of the column in bytes.|  
|DECIMAL_DIGITS|**short**|The scale of the column.|  
|PSEUDO_COLUMN|**short**|Indicates if the column is a pseudo column. It can be one of the following values:<br /><br /> versionColumnUnknown (0)<br /><br /> versionColumnNotPseudo (1)<br /><br /> versionColumnPseudo (2)|  
  
> [!NOTE]  
>  For more information about the data returned by the getVersionColumns method, see "sp_datatype_info (Transact-SQL)" in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Books Online.  
  
## Example  
 The following example demonstrates how to use the getVersionColumns method to return information about the columns that are automatically updated in the Person.Contact table in the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal_md.md)] sample database.  
  
```  
public static void executeGetVersionColumns(Connection con) {  
   try {  
      DatabaseMetaData dbmd = con.getMetaData();  
      ResultSet rs = dbmd.getVersionColumns("AdventureWorks", "Person", "Contact");  
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
  
  
