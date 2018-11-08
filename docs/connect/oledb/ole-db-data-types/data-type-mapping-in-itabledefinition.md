---
title: "Data Type Mapping in ITableDefinition | Microsoft Docs"
description: "Data type mapping in ITableDefinition"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "mapping data types [OLE DB]"
  - "OLE DB Driver for SQL Server, data types"
  - "ITableDefinition interface"
  - "DBCOLUMNDESC structure"
  - "data types [OLE DB]"
  - "CreateTable function"
  - "OLE DB, data types"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Data Type Mapping in ITableDefinition
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  When creating tables by using the **ITableDefinition::CreateTable** function, the OLE DB Driver for SQL Server consumer can specify [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types in the *pwszTypeName* member of the DBCOLUMNDESC array that is passed. If the consumer specifies the data type of a column by name, OLE DB data type mapping, represented by the *wType* member of the DBCOLUMNDESC structure, is ignored.  
  
 When specifying new column data types with OLE DB data types using the DBCOLUMNDESC structure *wType* member, the OLE DB Driver for SQL Server maps OLE DB data types as follows.  
  
|OLE DB data type|SQL Server<br /><br /> data type|Additional information|  
|----------------------|------------------------------|----------------------------|  
|DBTYPE_BOOL|**bit**||  
|DBTYPE_BYTES|**binary**, **varbinary**, **image,** or **varbinary(max)**|The OLE DB Driver for SQL Server inspects the *ulColumnSize* member of the DBCOLUMNDESC structure. Based on the value, and version of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance, the OLE DB Driver for SQL Server maps the type to **image**.<br /><br /> If the value of *ulColumnSize* is smaller than the maximum length of a **binary** data type column, then the OLE DB Driver for SQL Server inspects the DBCOLUMNDESC *rgPropertySets* member. If DBPROP_COL_FIXEDLENGTH is VARIANT_TRUE, the OLE DB Driver for SQL Server maps the type to **binary**. If the value of the property is VARIANT_FALSE, the OLE DB Driver for SQL Server maps the type to **varbinary**. In either case, the DBCOLUMNDESC *ulColumnSize* member determines the width of the SQL Server column created.|  
|DBTYPE_CY|**money**||  
|DBTYPE_DBTIMESTAMP|**datetime2**||  
|DBTYPE_GUID|**uniqueidentifier**||  
|DBTYPE_I2|**smallint**||  
|DBTYPE_I4|**int**||  
|DBTYPE_I8|**bigint**||
|DBTYPE_NUMERIC|**numeric**|The OLE DB Driver for SQL Server inspects the DBCOLUMDESC *bPrecision* and *bScale* members to determine precision and scale for the **numeric** column.|  
|DBTYPE_R4|**real**||  
|DBTYPE_R8|**float**||  
|DBTYPE_STR|**char**, **varchar**, **text,** or **varchar(max)**|The OLE DB Driver for SQL Server inspects the *ulColumnSize* member of the DBCOLUMNDESC structure. Based on the value and version of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance, the OLE DB Driver for SQL Server maps the type to **text**.<br /><br /> If the value of *ulColumnSize* is smaller than the maximum length of a multibyte character data type column, then the OLE DB Driver for SQL Server inspects the DBCOLUMNDESC *rgPropertySets* member. If DBPROP_COL_FIXEDLENGTH is VARIANT_TRUE, the OLE DB Driver for SQL Server maps the type to **char**. If the value of the property is VARIANT_FALSE, the OLE DB Driver for SQL Server maps the type to **varchar**. In either case, the DBCOLUMNDESC *ulColumnSize* member determines the width of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] column created.|  
|DBTYPE_UDT|**UDT**|The following information is used in **DBCOLUMNDESC** structures by **ITableDefinition::CreateTable** when UDT columns are required:<br /><br /> *pwSzTypeName* is ignored.<br /><br /> *rgPropertySets* must include a **DBPROPSET_SQLSERVERCOLUMN** property set as described in the section on **DBPROPSET_SQLSERVERCOLUMN**, in [Using User-Defined Types](../../oledb/features/using-user-defined-types.md).|  
|DBTYPE_UI1|**tinyint**||  
|DBTYPE_VARIANT|**sql_variant**||
|DBTYPE_WSTR|**nchar**, **nvarchar**, **ntext,** or **nvarchar(max)**|The OLE DB Driver for SQL Server inspects the *ulColumnSize* member of the DBCOLUMNDESC structure. Based on the value, the OLE DB Driver for SQL Server maps the type to **ntext**.<br /><br /> If the value of *ulColumnSize* is smaller than the maximum length of a Unicode character data type column, then the OLE DB Driver for SQL Server inspects the DBCOLUMNDESC *rgPropertySets* member. If DBPROP_COL_FIXEDLENGTH is VARIANT_TRUE, the OLE DB Driver for SQL Server maps the type to **nchar**. If the value of the property is VARIANT_FALSE, the OLE DB Driver for SQL Server maps the type to **nvarchar**. In either case, the DBCOLUMNDESC *ulColumnSize* member determines the width of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] column created.|  
|DBTYPE_XML|**XML**||  

> [!NOTE]  
>  When creating a new table, the OLE DB Driver for SQL Server maps only the OLE DB data type enumeration values specified in the preceding table. Attempting to create a table with a column of any other OLE DB data type generates an error.  

## See Also  
 [Data Types &#40;OLE DB&#41;](../../oledb/ole-db-data-types/data-types-ole-db.md)  
  
  
