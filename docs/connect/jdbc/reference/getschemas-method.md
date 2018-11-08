---
title: "getSchemas Method () | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.getSchemas"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: adba0ee6-ff6d-4215-b646-62c735be3fe9
author: MightyPen
ms.author: genemi
manager: craigg
---
# getSchemas Method ()
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the schema names that are available in the current database.  
  
## Syntax  
  
```  
  
public java.sql.ResultSet getSchemas()  
```  
  
## Return Value  
 A [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getSchemas method is specified by the getSchemas method in the java.sql.DatabaseMetaData interface.  
  
 The result set returned by the getSchemas method contains the following information:  
  
|Name|Type|Description|  
|----------|----------|-----------------|  
|TABLE_SCHEM|**String**|The name of the schema.|  
|TABLE_CATALOG|**String**|The catalog name for the schema.|  
  
 The results are ordered by TABLE_CATALOG, and then TABLE_SCHEM. Each row has TABLE_SCHEM as the first column and TABLE_CATALOG as the second column.  
  
> [!NOTE]  
>  For more information about the data returned by the getSchemas method, see "sys.schemas (Transact-SQL)" in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Books Online.  
  
## Example  
 The following example demonstrates how to use the getSchemas method to return information about the catalog and its associated schema names in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] when the connection argument specifies the database to be used.  
  
```  
public static void executeGetSchemas(Connection con) {  
   try {  
      DatabaseMetaData dbmd = con.getMetaData();  
      ResultSet rs = dbmd.getSchemas();  
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
  
  
