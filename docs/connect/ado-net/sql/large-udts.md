---
title: "Large UDTs"
description: "Demonstrates how to retrieve data from large value UDTs introduced in SQL Server 2008."
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-kaywon
ms.date: "08/15/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# Large UDTs

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

User-defined types (UDTs) allow a developer to extend the server's scalar type system by storing common language runtime (CLR) objects in a SQL Server database. UDTs can contain multiple elements and can have behaviors, unlike the traditional alias data types, which consist of a single SQL Server system data type.  
  
Previously, UDTs were restricted to a maximum size of 8 kilobytes. In SQL Server 2008, this restriction has been removed for UDTs that have a format of <xref:Microsoft.Data.SqlClient.Server.Format.UserDefined>.  
  
For the complete documentation for user-defined types, see [CLR User-Defined Types](/previous-versions/sql/sql-server-2008/ms131120(v=sql.100)) from SQL Server Books Online.
  
## Retrieving UDT schemas using GetSchema  
The <xref:Microsoft.Data.SqlClient.SqlConnection.GetSchema%2A> method of <xref:Microsoft.Data.SqlClient.SqlConnection> returns database schema information in a <xref:System.Data.DataTable>.
  
### GetSchemaTable column values for UDTs  
The <xref:Microsoft.Data.SqlClient.SqlDataReader.GetSchemaTable%2A> method of a <xref:Microsoft.Data.SqlClient.SqlDataReader> returns a <xref:System.Data.DataTable> that describes column metadata. The following table describes the differences in the column metadata for large UDTs between SQL Server 2005 and SQL Server 2008.  
  
|SqlDataReader column|SQL Server 2005|SQL Server 2008 and later|  
|--------------------------|---------------------|-------------------------------|  
|`ColumnSize`|Varies|Varies|  
|`NumericPrecision`|255|255|  
|`NumericScale`|255|255|  
|`DataType`|`Byte[]`|UDT instance|  
|`ProviderSpecificDataType`|`SqlTypes.SqlBinary`|UDT instance|  
|`ProviderType`|21 (`SqlDbType.VarBinary`)|29 (`SqlDbType.Udt`)|  
|`NonVersionedProviderType`|29 (`SqlDbType.Udt`)|29 (`SqlDbType.Udt`)|  
|`DataTypeName`|`SqlDbType.VarBinary`|The three part name specified as *Database.SchemaName.TypeName*.|  
|`IsLong`|Varies|Varies|  
  
## SqlDataReader considerations  
The <xref:Microsoft.Data.SqlClient.SqlDataReader> has been extended beginning in SQL Server 2008 to support retrieving large UDT values. How large UDT values are processed by a <xref:Microsoft.Data.SqlClient.SqlDataReader> depends on the version of SQL Server you are using, as well as on the `Type System Version` specified in the connection string. For more information, see <xref:Microsoft.Data.SqlClient.SqlConnection.ConnectionString%2A>.  
  
The following methods of <xref:Microsoft.Data.SqlClient.SqlDataReader> will return a <xref:System.Data.SqlTypes.SqlBinary> instead of a UDT when the `Type System Version` is set to SQL Server 2005:  
  
- <xref:Microsoft.Data.SqlClient.SqlDataReader.GetProviderSpecificFieldType%2A>  
  
- <xref:Microsoft.Data.SqlClient.SqlDataReader.GetProviderSpecificValue%2A>  
  
- <xref:Microsoft.Data.SqlClient.SqlDataReader.GetProviderSpecificValues%2A>  
  
- <xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlValue%2A>  
  
- <xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlValues%2A>  
  
The following methods will return an array of `Byte[]` instead of a UDT when the `Type System Version` is set to SQL Server 2005:  
  
- <xref:Microsoft.Data.SqlClient.SqlDataReader.GetValue%2A>  
  
- <xref:Microsoft.Data.SqlClient.SqlDataReader.GetValues%2A>  
  
Note that no conversions are made for the current version of ADO.NET.  
  
## Specifying SqlParameters  
The following <xref:Microsoft.Data.SqlClient.SqlParameter> properties have been extended to work with large UDTs.  
  
|SqlParameter Property|Description|  
|---------------------------|-----------------|  
|<xref:Microsoft.Data.SqlClient.SqlParameter.Value%2A>|Gets or sets an object that represents the value of the parameter. The default is null. The property can be `SqlBinary`, `Byte[]`, or a managed object.|  
|<xref:Microsoft.Data.SqlClient.SqlParameter.SqlValue%2A>|Gets or sets an object that represents the value of the parameter. The default is null. The property can be `SqlBinary`, `Byte[]`, or a managed object.|  
|<xref:Microsoft.Data.SqlClient.SqlParameter.Size%2A>|Gets or sets the size of the parameter value to resolve. The default value is 0. The property can be an integer that represents the size of the parameter value. For large UDTs, it can be the actual size of the UDT, or -1 for unknown.|  
  
## Retrieving data example  
The following code fragment demonstrates how to retrieve large UDT data. The `connectionString` variable assumes a valid connection to a SQL Server database and the `commandString` variable assumes a valid SELECT statement with the primary key column listed first.  
  
```csharp  
using (SqlConnection connection = new SqlConnection(   
    connectionString, commandString))  
{  
  connection.Open();  
  SqlCommand command = new SqlCommand(commandString);  
  SqlDataReader reader = command.ExecuteReader();  
  while (reader.Read())  
  {  
    // Retrieve the value of the Primary Key column.  
    int id = reader.GetInt32(0);  
  
    // Retrieve the value of the UDT.  
    LargeUDT udt = (LargeUDT)reader[1];  
  
    // You can also use GetSqlValue and GetValue.  
    // LargeUDT udt = (LargeUDT)reader.GetSqlValue(1);  
    // LargeUDT udt = (LargeUDT)reader.GetValue(1);  
  
    Console.WriteLine(  
     "ID={0} LargeUDT={1}", id, udt);  
  }  
reader.close  
}  
```  
  
## Next steps
- [SQL Server binary and large-value data](sql-server-binary-large-value-data.md)
