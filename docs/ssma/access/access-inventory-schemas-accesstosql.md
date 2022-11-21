---
description: "Access Inventory Schemas (AccessToSQL)"
title: "Access Inventory Schemas (AccessToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "columns table"
  - "databases table"
  - "exported metadata"
  - "exporting, schemas of exported metadata"
  - "foreign keys table"
  - "forms table"
  - "indexes, table"
  - "inventory schemas"
  - "macros table"
  - "modules table"
  - "queries table"
  - "reference, inventory schemas"
  - "reports table"
  - "schemas of exported metadata"
  - "schemas, exported metadata"
  - "SSMA_Access_InventoryColumns"
  - "SSMA_Access_InventoryDatabases"
  - "SSMA_Access_InventoryForeignKeys"
  - "SSMA_Access_InventoryForms"
  - "SSMA_Access_InventoryIndexes"
  - "SSMA_Access_InventoryMacros"
  - "SSMA_Access_InventoryModules"
  - "SSMA_Access_InventoryQueries"
  - "SSMA_Access_InventoryReports"
  - "SSMA_Access_InventoryTables"
  - "tables, inventory"
ms.assetid: fdd3cff2-4d62-4395-8acf-71ea8f17f524
author: cpichuka 
ms.author: cpichuka 
---
# Access Inventory Schemas (AccessToSQL)
The following sections describe the tables that are created by SSMA when you export Access schemas to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Databases  
Database metadata is exported to the **SSMA_Access_InventoryDatabases** table. This table contains the following columns:  
  
|Column Name|Data Type|Description|  
|---------------|-------------|---------------|  
|**DatabaseId**|**uniqueidentifier**|A GUID that uniquely identifies each database. This column is also the primary key for the table.|  
|**DatabaseName**|**nvarchar(4000)**|The name of the Access database.|  
|**ExportTime**|**datetime**|The date and time this metadata was created by SSMA.|  
|**FilePath**|**nvarchar(4000)**|The full path and file name of the Access database.|  
|**FileSize**|**bigint**|The size of the Access database in KB.|  
|**FileOwner**|**nvarchar(4000)**|The Windows account that is specified as the owner of the Access database.|  
|**DateCreated**|**datetime**|The date and time the Access database was created.|  
|**DateModified**|**datetime**|The date and time the Access database was last modified.|  
|**TablesCount**|**int**|The number of tables in the Access database.|  
|**QueriesCount**|**int**|The number of queries in the Access database.|  
|**FormsCount**|**int**|The number of forms in the Access database.|  
|**ModulesCount**|**int**|The number of modules in the Access database.|  
|**ReportsCount**|**int**|The number of reports in the Access database.|  
|**MacrosCount**|**int**|The number of macros in the Access database.|  
|**AccessVersion**|**nvarchar(4000)**|The Access version of the database.|  
|**Collation**|**nvarchar(4000)**|The collation of the Access database. Collations determine how a database sorts and compares strings.|  
|**JetVersion**|**nvarchar(4000)**|The Jet database engine version. Access databases use the underlying Jet database engine.|  
|**IsUpdatable**|**bit**|Indicates if the database can be updated. If the value is 1, the database is updatable. If the value is 0, the database is read-only.|  
|**QueryTimeout**|**int**|The configured ODBC query time-out value for the database, in seconds. The default is 60 seconds.|  
  
## Tables  
Table metadata is exported to the **SSMA_Access_InventoryTables** table. This table contains the following columns:  
  
|Column Name|Data Type|Description|  
|---------------|-------------|---------------|  
|**DatabaseId**|**uniqueidentifier**|Identifies the database that contains this table.|  
|**TableId**|**uniqueidentifier**|A GUID that uniquely identifies the table. This column is also the primary key for the table.|  
|**TableName**|**nvarchar(4000)**|The name of the table.|  
|**RowsCount**|**int**|The number of rows in the table.|  
|**ValidationRule**|**nvarchar(4000)**|The rule that defines valid input for the table. If no validation rule exists, the field will contain an empty string.|  
|**LinkedTable**|**nvarchar(4000)**|Another table, if any, that is linked with the table. Linking tables allows additions, deletions, and updates to the other table by using this table.|  
|**ExternalSource**|**nvarchar(4000)**|The data source, if any, that is associated with the table. If a table is linked, it has an external data source specified in this field.|  
  
## Columns  
Column metadata is exported to the **SSMA_Access_InventoryColumns** table. This table contains the following columns:  
  
|Column Name|Data Type|Description|  
|---------------|-------------|---------------|  
|**DatabaseId**|**uniqueidentifier**|Identifies the database that contains this column.|  
|**TableId**|**uniqueidentifier**|Identifies the table that contains this column.|  
|**ColumnId**|**int**|An incrementing integer that identifies the column. **ColumnId** is the primary key for the table.|  
|**ColumnName**|**nvarchar(4000)**|The name of the column.|  
|**IsNullable**|**bit**|Specifies if the column can contain null values. If the value is 1, the column can contain null values. If the value is 0, the column cannot contain null values. Note that the validation rule can also be used to prevent null values.|  
|**DataType**|**nvarchar(4000)**|The Access data type of the column, such as **Text** or **Long**.|  
|**IsAutoIncrement**|**bit**|Specifies if the column automatically increments integer values. If the value is 1, the integers are automatically incrementing.|  
|**Ordinal**|**smallint**|The order of the column in the table, starting at zero.|  
|**DefaultValue**|**nvarchar(4000)**|The default value for the column.|  
|**ValidationRule**|**nvarchar(4000)**|The rule that is used to validate data added to or updated in the column.|  
  
## Indexes  
Index metadata is exported to the **SSMA_Access_InventoryIndexes** table. This table contains the following columns:  
  
|Column Name|Data Type|Description|  
|---------------|-------------|---------------|  
|**DatabaseId**|**uniqueidentifier**|Identifies the database that contains this index.|  
|**TableId**|**uniqueidentifier**|Identifies the table that contains this index.|  
|**IndexId**|**int**|An incrementing integer that identifies the index. This column is the primary key for the table.|  
|**IndexName**|**nvarchar(4000)**|The name of the index.|  
|**ColumnsIncluded**|**nvarchar(4000)**|Lists the columns that are included in the index. The column names are separated by a semicolon.|  
|**IsUnique**|**bit**|Specifies if each item in the index must be unique. On a multi-column index, the combination of values must be unique. If the value is 1, the index enforces unique values.|  
|**IsPK**|**bit**|Specifies if the index was automatically created as part of defining the primary key.|  
|**IsClustered**|**bit**|Specifies if the index is clustered. A clustered index reorders the physical storage of the data. A table can have only one clustered index.|  
  
## Foreign Keys  
Foreign key metadata is exported to the **SSMA_Access_InventoryForeignKeys** table. This table contains the following columns:  
  
|Column Name|Data Type|Description|  
|---------------|-------------|---------------|  
|**DatabaseId**|**uniqueidentifier**|Identifies the database that contains this foreign key.|  
|**TableId**|**uniqueidentifier**|Identifies the table that contains this foreign key.|  
|**ForeignKeyId**|**int**|An incrementing integer that identifies the foreign key. This column is the primary key for the table.|  
|**ForeignKeyName**|**nvarchar(4000)**|The name of the index.|  
|**ReferencedTableId**|**uniqueidentifier**|Identifies the table that contains the source columns.|  
|**SourceColumns**|**nvarchar(4000)**|Lists the foreign key column or columns.|  
|**ReferencedColumns**|**nvarchar(4000)**|Lists the primary key column or columns that are referenced by the foreign key.|  
|**IsCascadeForUpdate**|**bit**|Specifies that if the primary key value is updated, all rows that reference that key value are also updated.|  
|**IsCascadeForDelete**|**bit**|Specifies that if the primary key value is deleted, all rows that reference that key value are also deleted.|  
|**IsEnforced**|**bit**|Specifies that the foreign key constraint is enforced.|  
  
## Queries  
Query metadata is exported to the **SSMA_Access_InventoryQueries** table. This table contains the following columns:  
  
|Column Name|Data Type|Description|  
|---------------|-------------|---------------|  
|**DatabaseId**|**uniqueidentifier**|Identifies the database that contains this query.|  
|**QueryId**|**int**|An incrementing integer that identifies the query. This column is the primary key for the table.|  
|**QueryName**|**nvarchar(4000)**|The name of the query.|  
|**QueryText**|**nvarchar(4000)**|The SQL query code, such as a SELECT statement.|  
|**IsUpdateable**|**bit**|Specifies if the query is updateable or read-only.|  
|**QueryType**|**nvarchar(4000)**|Specifies the type of query, such as **Select** or **SetOperation**.|  
|**ExternalSource**|**nvarchar(4000)**|If the query references an external data source, this is the connection string used by the query.|  
  
## Forms  
Form metadata is exported to the **SSMA_Access_InventoryForms** table. This table contains the following columns:  
  
|Column Name|Data Type|Description|  
|---------------|-------------|---------------|  
|**DatabaseId**|**uniqueidentifier**|Identifies the database that contains this form.|  
|**FormId**|**int**|An incrementing integer that identifies the form. This column is the primary key for the table.|  
|**FormName**|**nvarchar(4000)**|The name of the form.|  
  
## Macros  
Macro metadata is exported to the **SSMA_Access_InventoryMacros** table. This table contains the following columns:  
  
|Column Name|Data Type|Description|  
|---------------|-------------|---------------|  
|**DatabaseId**|**uniqueidentifier**|Identifies the database that contains the macro.|  
|**MacroId**|**int**|An incrementing integer that identifies the macro. This column is the primary key for the table.|  
|**MacroName**|**nvarchar(4000)**|The name of the macro.|  
  
## Reports  
Report metadata is exported to the **SSMA_Access_InventoryReports** table. This table contains the following columns:  
  
|Column Name|Data Type|Description|  
|---------------|-------------|---------------|  
|**DatabaseId**|**uniqueidentifier**|Identifies the database that contains the report.|  
|**ReportId**|**int**|An incrementing integer that identifies the report. This column is the primary key for the table.|  
|**ReportName**|**nvarchar(4000)**|The name of the report.|  
  
## Modules  
Module metadata is exported to the **SSMA_Access_InventoryModules** table. This table contains the following columns:  
  
|Column Name|Data Type|Description|  
|---------------|-------------|---------------|  
|**DatabaseId**|**uniqueidentifier**|Identifies the database that contains the module.|  
|**ModuleId**|**int**|An incrementing integer that identifies the module. This column is the primary key for the table.|  
|**ModuleName**|**nvarchar(4000)**|The name of the module.|  
  
## See Also  
[Exporting an Access Inventory](exporting-an-access-inventory-accesstosql.md)  
  
