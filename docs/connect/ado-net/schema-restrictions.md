---
title: "Schema restrictions"
description: "Describes schema restrictions that can be used with GetSchema when using the Microsoft SqlClient Data Provider for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-chmalh
ms.date: "11/26/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Schema restrictions

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

The second optional parameter of the **GetSchema** method is the restrictions that are used to limit the amount of schema information returned, and it is passed to the **GetSchema** method as an array of strings. The position in the array determines the values that you can pass, and this is equivalent to the restriction number.  
  
For example, the following table describes the restrictions supported by the "Tables" schema collection using the Microsoft SqlClient Data Provider for SQL Server. Additional restrictions for SQL Server schema collections are listed at the end of this topic.  

|Restriction Name|Parameter Name|Restriction Default|Restriction Number|  
|----------------------|--------------------|-------------------------|------------------------|  
|Catalog|@Catalog|TABLE_CATALOG|1|  
|Owner|@Owner|TABLE_SCHEMA|2|  
|Table|@Name|TABLE_NAME|3|  
|TableType|@TableType|TABLE_TYPE|4|  
 
## Specifying restriction values  

To use one of the restrictions of the "Tables" schema collection, simply create an array of strings with four elements, then place a value in the element that matches the restriction number. For example, to restrict the tables returned by the **GetSchema** method to only those tables in the "Sales" schema, set the second element of the array to "Sales" before passing it to the **GetSchema** method.  
  
> [!NOTE]
> - The restrictions collections for `SqlClient` have an additional `ParameterName` column. The restriction default column is still there for backwards compatibility, but is currently ignored. Parameterized queries rather than string replacement should be used to minimize the risk of an SQL injection attack when specifying restriction values.  
> - The number of elements in the array must be less than or equal to the number of restrictions supported for the specified schema collection else an <xref:System.ArgumentException> will be thrown. There can be fewer than the maximum number of restrictions. The missing restrictions are assumed to be null (unrestricted).  
  
You can query the Microsoft SqlClient Data Provider for SQL Server to determine the list of supported restrictions by calling the **GetSchema** method with the name of the restrictions schema collection, which is "Restrictions". This will return a <xref:System.Data.DataTable> with a list of the collection names, the restriction names, the default restriction values, and the restriction numbers.  
  
### Example  

The following examples demonstrate how to use the <xref:Microsoft.Data.SqlClient.SqlConnection.GetSchema%2A> method of the Microsoft SqlClient Data Provider for SQL Server <xref:Microsoft.Data.SqlClient.SqlConnection> class to retrieve schema information about all of the tables contained in the **AdventureWorks** sample database, and to restrict the information returned to only those tables in the "Sales" schema:  

[!code-csharp[SqlClient GetSchema with restrictions#1](~/../sqlclient/doc/samples/SqlConnection_GetSchema_Restriction.cs#1)]  

## SQL Server schema restrictions  

 The following tables list the restrictions for SQL Server schema collections.  
  
### Users  
  
|Restriction Name|Parameter Name|Restriction Default|Restriction Number|  
|----------------------|--------------------|-------------------------|------------------------|  
|User_Name|@Name|name|1|  
  
### Databases  
  
|Restriction Name|Parameter Name|Restriction Default|Restriction Number|  
|----------------------|--------------------|-------------------------|------------------------|  
|Name|@Name|Name|1|  
  
### Tables  
  
|Restriction Name|Parameter Name|Restriction Default|Restriction Number|  
|----------------------|--------------------|-------------------------|------------------------|  
|Catalog|@Catalog|TABLE_CATALOG|1|  
|Owner|@Owner|TABLE_SCHEMA|2|  
|Table|@Name|TABLE_NAME|3|  
|TableType|@TableType|TABLE_TYPE|4|  
  
### Columns  
  
|Restriction Name|Parameter Name|Restriction Default|Restriction Number|  
|----------------------|--------------------|-------------------------|------------------------|  
|Catalog|@Catalog|TABLE_CATALOG|1|  
|Owner|@Owner|TABLE_SCHEMA|2|  
|Table|@Table|TABLE_NAME|3|  
|Column|@Column|COLUMN_NAME|4|  
  
### StructuredTypeMembers  
  
|Restriction Name|Parameter Name|Restriction Default|Restriction Number|  
|----------------------|--------------------|-------------------------|------------------------|  
|Catalog|@Catalog|TABLE_CATALOG|1|  
|Owner|@Owner|TABLE_SCHEMA|2|  
|Table|@Table|TABLE_NAME|3|  
|Column|@Column|COLUMN_NAME|4|  
  
### Views  
  
|Restriction Name|Parameter Name|Restriction Default|Restriction Number|  
|----------------------|--------------------|-------------------------|------------------------|  
|Catalog|@Catalog|TABLE_CATALOG|1|  
|Owner|@Owner|TABLE_SCHEMA|2|  
|Table|@Table|TABLE_NAME|3|  
  
### ViewColumns  
  
|Restriction Name|Parameter Name|Restriction Default|Restriction Number|  
|----------------------|--------------------|-------------------------|------------------------|  
|Catalog|@Catalog|VIEW_CATALOG|1|  
|Owner|@Owner|VIEW_SCHEMA|2|  
|Table|@Table|VIEW_NAME|3|  
|Column|@Column|COLUMN_NAME|4|  
  
### ProcedureParameters  
  
|Restriction Name|Parameter Name|Restriction Default|Restriction Number|  
|----------------------|--------------------|-------------------------|------------------------|  
|Catalog|@Catalog|SPECIFIC_CATALOG|1|  
|Owner|@Owner|SPECIFIC_SCHEMA|2|  
|Name|@Name|SPECIFIC_NAME|3|  
|Parameter|@Parameter|PARAMETER_NAME|4|  
  
### Procedures  
  
|Restriction Name|Parameter Name|Restriction Default|Restriction Number|  
|----------------------|--------------------|-------------------------|------------------------|  
|Catalog|@Catalog|SPECIFIC_CATALOG|1|  
|Owner|@Owner|SPECIFIC_SCHEMA|2|  
|Name|@Name|SPECIFIC_NAME|3|  
|Type|@Type|ROUTINE_TYPE|4|  
  
### IndexColumns  
  
|Restriction Name|Parameter Name|Restriction Default|Restriction Number|  
|----------------------|--------------------|-------------------------|------------------------|  
|Catalog|@Catalog|db_name()|1|  
|Owner|@Owner|user_name()|2|  
|Table|@Table|o.name|3|  
|ConstraintName|@ConstraintName|x.name|4|  
|Column|@Column|c.name|5|  
  
### Indexes  
  
|Restriction Name|Parameter Name|Restriction Default|Restriction Number|  
|----------------------|--------------------|-------------------------|------------------------|  
|Catalog|@Catalog|db_name()|1|  
|Owner|@Owner|user_name()|2|  
|Table|@Table|o.name|3|  
  
### UserDefinedTypes  
  
|Restriction Name|Parameter Name|Restriction Default|Restriction Number|  
|----------------------|--------------------|-------------------------|------------------------|  
|assembly_name|@AssemblyName|assemblies.name|1|  
|udt_name|@UDTName|types.assembly_class|2|  
  
### ForeignKeys  
  
|Restriction Name|Parameter Name|Restriction Default|Restriction Number|  
|----------------------|--------------------|-------------------------|------------------------|  
|Catalog|@Catalog|CONSTRAINT_CATALOG|1|  
|Owner|@Owner|CONSTRAINT_SCHEMA|2|  
|Table|@Table|TABLE_NAME|3|  
|Name|@Name|CONSTRAINT_NAME|4|  
  
## See also

- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
