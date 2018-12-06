---
title: "getColumns Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.getColumns"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: f173fa5d-e114-4a37-a5c4-2baad9ff3af1
author: MightyPen
ms.author: genemi
manager: craigg
---
# getColumns Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves a description of the table columns that are available in the specified catalog.  
  
## Syntax  
  
```  
  
public java.sql.ResultSet getColumns(java.lang.String catalog,  
                                     java.lang.String schema,  
                                     java.lang.String table,  
                                     java.lang.String col)  
```  
  
#### Parameters  
 *catalog*  
  
 A **String** that contains the catalog name.  
  
 *schema*  
  
 A **String** that contains the schema name pattern.  
  
 *table*  
  
 A **String** that contains the table name pattern.  
  
 *col*  
  
 A **String** that contains the column name pattern.  
  
## Return Value  
 A [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getColumns method is specified by the getColumns method in the java.sql.DatabaseMetaData interface.  
  
 The result set returned by the getColumns method will contain the following information:  
  
|Name|Type|Description|  
|----------|----------|-----------------|  
|TABLE_CAT|**String**|The catalog name.|  
|TABLE_SCHEM|**String**|The table schema name.|  
|TABLE_NAME|**String**|The table name.|  
|COLUMN_NAME|**String**|The column name.|  
|DATA_TYPE|**smallint**|The SQL data type from java.sql.Types.|  
|TYPE_NAME|**String**|The name of the data type.|  
|COLUMN_SIZE|**int**|The precision of the column.|  
|BUFFER_LENGTH|**smallint**|Transfer size of the data.|  
|DECIMAL_DIGITS|**smallint**|The scale of the column.|  
|NUM_PREC_RADIX|**smallint**|The radix of the column.|  
|NULLABLE|**smallint**|Indicates if the column is nullable. It can be one of the following values:<br /><br /> columnNoNulls (0)<br /><br /> columnNullable (1)|  
|REMARKS|**String**|The comments associated with the column.<br /><br /> **Note:**  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] always returns null for this column.|  
|COLUMN_DEF|**String**|The default value of the column.|  
|SQL_DATA_TYPE|**smallint**|Value of the SQL data type as it appears in the TYPE field of the descriptor. This column is the same as the DATA_TYPE column, except for the datetime and SQL-92 interval data types. This column always returns a value.|  
|SQL_DATETIME_SUB|**smallint**|Subtype code for datetime and SQL-92 interval data types. For other data types, this column returns NULL.|  
|CHAR_OCTET_LENGTH|**int**|The maximum number of bytes in the column.|  
|ORDINAL_POSITION|**int**|The index of the column within the table.|  
|IS_NULLABLE|**String**|Indicates if the column allows null values.|  
|SS_IS_SPARSE|**smallint**|If the column is a sparse column, this has the value 1; otherwise, 0.<sup>1</sup>|  
|SS_IS_COLUMN_SET|**smallint**|If the column is the sparse column_set column, this has the value 1; otherwise, 0. <sup>1</sup>|  
|SS_IS_COMPUTED|**smallint**|Indicates if a column in a TABLE_TYPE is a computed column. <sup>1</sup>|  
|IS_AUTOINCREMENT|**String**|"YES" if the column is auto incremented. "NO" if the column is not auto incremented. "" (empty string) if the driver cannot determine if the column is auto incremented. <sup>1</sup>|  
|SS_UDT_CATALOG_NAME|**String**|The name of the catalog that contains the user-defined type (UDT). <sup>1</sup>|  
|SS_UDT_SCHEMA_NAME|**String**|The name of the schema that contains the user-defined type (UDT). <sup>1</sup>|  
|SS_UDT_ASSEMBLY_TYPE_NAME|**String**|The fully-qualified name user-defined type (UDT). <sup>1</sup>|  
|SS_XML_SCHEMACOLLECTION_CATALOG_NAME|**String**|The name of the catalog where an XML schema collection name is defined. If the catalog name cannot be found, this variable contains an empty string. <sup>1</sup>|  
|SS_XML_SCHEMACOLLECTION_SCHEMA_NAME|**String**|The name of the schema where an XML schema collection name is defined. If the schema name cannot be found, this is an empty string. <sup>1</sup>|  
|SS_XML_SCHEMACOLLECTION_NAME|**String**|The name of an XML schema collection. If the name cannot be found, this is an empty string. <sup>1</sup>|  
|SS_DATA_TYPE|**tinyint**|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type that is used by extended stored procedures.<br /><br /> **Note** For more information about the data types returned by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], see "Data Types (Transact-SQL)" in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Books Online.|  
  
 (1) This column will not be present if you are connecting to [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)].  
  
> [!NOTE]  
>  For more information about the data returned by the getColumns method, see "sp_columns (Transact-SQL)" in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Books Online.  
  
 In the [!INCLUDE[msCoName](../../../includes/msconame_md.md)][!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] JDBC Driver 3.0, you will see the following behavior changes from earlier versions of the JDBC Driver:  
  
 The DATA_TYPE column has the following changes:  
  
|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Data Type|Return Type in JDBC Driver 2.0 (or, if connected to [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]) and Associated Numeric Constant|Return Type in JDBC Driver 3.0 when connected to [!INCLUDE[ssKatmai](../../../includes/sskatmai_md.md)] or later|  
|-------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------|  
|user-defined type larger than 8 kB|LONGVARBINARY (-4)|VARBINARY (-3)|  
|geography|LONGVARBINARY (-4)|VARBINARY (-3)|  
|geometry|LONGVARBINARY (-4)|VARBINARY (-3)|  
|varbinary(max)|LONGVARBINARY (-4)|VARBINARY (-3)|  
|nvarchar(max)|LONGVARCHAR (-1) or LONGNVARCHAR (JDBC 4) (-16)|VARCHAR (12) or NVARCHAR (JDBC 4) (-9)|  
|varchar(max)|LONGVARCHAR (-1)|VARCHAR (12)|  
|time|VARCHAR (12) or NVARCHAR (JDBC 4) (-9)|TIME (-154)|  
|date|VARCHAR (12) or NVARCHAR (JDBC 4) (-9)|DATE (91)|  
|datetime2|VARCHAR (12) or NVARCHAR (JDBC 4) (-9)|TIMESTAMP (93)|  
|datetimeoffset|VARCHAR (12) or NVARCHAR (JDBC 4) (-9)|microsoft.sql.Types.DATETIMEOFFSET  (-155)|  
  
 The COLUMN_SIZE column has the following changes:  
  
|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Data Type|Return Type in JDBC Driver 2.0|Return Type in JDBC Driver 3.0|  
|-------------------------------------------------------------------|------------------------------------|------------------------------------|  
|nvarchar(max)|1073741823|2147483647 (database metadata)|  
|xml|1073741823|2147483647 (database metadata)|  
|user-defined type less than or equal to 8 kB|8 kB (result set and parameter metadata)|Actual size returned by the stored procedure.|  
|time||The length in characters of the string representation of the type, assuming the maximum allowed precision of the fractional seconds' component.|  
|date||same as time|  
|datetime2||same as time|  
|datetimeoffset||same as time|  
  
 The BUFFER_LENGTH column has the following change:  
  
|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Data Type|Return Type in JDBC Driver 2.0|Return Type in JDBC Driver 3.0|  
|-------------------------------------------------------------------|------------------------------------|------------------------------------|  
|user-defined type larger than 8 kB||2147483647|  
  
 The TYPE_NAME column has the following changes:  
  
|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Data Type|Return Type in JDBC Driver 2.0|Return Type in JDBC Driver 3.0|  
|-------------------------------------------------------------------|------------------------------------|------------------------------------|  
|varchar(max)|text|varchar|  
|varbinary(max)|image|varbinary|  
  
 The DECIMAL_DIGITS column has the following changes:  
  
|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Type|JDBC Driver 2.0|JDBC Driver 3.0|  
|--------------------------------------------------------------|---------------------|---------------------|  
|time|null|7 (or smaller if specified)|  
|date|null|null|  
|datetime2|null|7 (or smaller if specified)|  
|datetimeoffset|null|7 (or smaller if specified)|  
  
 The SQL_DATA_TYPE column has the following changes:  
  
|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Data Type|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] 2008 Data Value in JDBC Driver 2.0|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] 2008 Data Value in JDBC Driver 3.0|  
|-------------------------------------------------------------------|--------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------|  
|varchar(max)|-10|-9|  
|nvarchar(max)|-1|-9|  
|xml|-10|-152|  
|user-defined type less than or equal to 8 kB|-3|-151|  
|user-defined type larger than 8 kB|Not available in JDBC Driver 2.0|-151|  
|geography|-4|-151|  
|geometry|-4|-151|  
|hierarchyid|-4|-151|  
|time|-9|92|  
|date|-9|91|  
|datetime2|-9|93|  
|datetimeoffset|-9|-155|  
  
## Example  
 The following example demonstrates how to use the getColumns method to return information for the Person.Contact table in the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal_md.md)] sample database.  
  
```  
import java.sql.*;  
public class c1 {  
   public static void main(String[] args) {  
      String connectionUrl = "jdbc:sqlserver://localhost:1433;databaseName=AdventureWorks;integratedsecurity=true";  
  
      Connection con = null;  
      Statement stmt = null;  
      ResultSet rs = null;  
  
      try {  
         Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");  
         con = DriverManager.getConnection(connectionUrl);  
         DatabaseMetaData dbmd = con.getMetaData();  
         rs = dbmd.getColumns("AdventureWorks", "Person", "Contact", "FirstName");  
  
         ResultSet r = dbmd.getColumns(null, null, "Contact", null);  
         ResultSetMetaData rm = r.getMetaData();   
         int noofcols = rm.getColumnCount();  
  
         if (r.next())  
            for (int i = 0 ; i < noofcols ; i++ )  
            System.out.println(rm.getColumnName( i + 1 ) + ": \t\t" + r.getString( i + 1 ));  
      }  
  
      catch (Exception e) {}  
      finally {}  
   }  
}  
```  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
