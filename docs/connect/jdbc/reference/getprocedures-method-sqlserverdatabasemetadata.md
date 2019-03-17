---
title: "getProcedures Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.getProcedures"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 66c9a8b0-dc4c-4cbb-8004-c7157368cab4
author: MightyPen
ms.author: genemi
manager: craigg
---
# getProcedures Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves a description of the stored procedures that are available in the given catalog, schema, or stored procedure name pattern.  
  
## Syntax  
  
```  
  
public java.sql.ResultSet getProcedures(java.lang.String sCatalog,  
                                        java.lang.String sSchema,  
                                        java.lang.String proc)  
```  
  
#### Parameters  
 *sCatalog*  
  
 A **String** that contains the catalog name. Providing a null to this parameter indicates that the catalog name does not need to be used.  
  
 *sSchema*  
  
 A **String** that contains the schema name pattern. Providing a null to this parameter indicates that the schema name does not need to be used.  
  
 *proc*  
  
 A **String** that contains the procedure name pattern.  
  
## Return Value  
 A [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getProcedures method is specified by the getProcedures method in the java.sql.DatabaseMetaData interface.  
  
 The result set returned by the getProcedures method will contain the following information:  
  
|Name|Type|Description|  
|----------|----------|-----------------|  
|PROCEDURE_CAT|**String**|The name of the database in which the specified stored procedure resides.|  
|PROCEDURE_SCHEM|**String**|The schema for the stored procedure.|  
|PROCEDURE_NAME|**String**|The name of the stored procedure.|  
|NUM_INPUT_PARAMS|**int**|Reserved for future use, currently returns a -1 value.|  
|NUM_OUTPUT_PARAMS|**int**|Reserved for future use, currently returns a -1 value.|  
|NUM_RESULT_SETS|**int**|Reserved for future use, currently returns a -1 value.|  
|REMARKS|**String**|The description of the procedure column.<br /><br /> <br /><br /> **Note:**  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not return a value for this column.|  
|PROCEDURE_TYPE|**smallint**|The type of stored procedure. It can be one of the following values:<br /><br /> SQL_PT_UNKNOWN (0)<br /><br /> SQL_PT_PROCEDURE (1)<br /><br /> SQL_PT_FUNCTION (2)|  
  
> [!NOTE]  
>  For more information about the data returned by the getProcedures method, see "sp_stored_procedures (Transact-SQL)" in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Books Online.  
  
## Example  
 The following example demonstrates how to use the getProcedures method to return information about the uspGetBillOfMaterials stored procedure in the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal_md.md)] sample database.  
  
```  
public static void executeGetProcedures(Connection con) {  
   try {  
      DatabaseMetaData dbmd = con.getMetaData();  
      ResultSet rs = dbmd.getProcedures(null, null, "uspGetBillOfMaterials");  
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
  
  
