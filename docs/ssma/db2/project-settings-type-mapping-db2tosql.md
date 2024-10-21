---
title: "Project Settings (Type Mapping) (Db2ToSQL)"
description: Customize how SSMA converts Db2 data types into SQL Server data types.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.db2.typemappinggridview.f1"
  - "ssma.db2.projectsettingtypemapping.f1"
---
# Project Settings (Type Mapping) (Db2ToSQL)

The Type Mapping page of the **Project Settings** dialog box contains settings that customize how SQL Server Migration Assistant (SSMA) converts Db2 data types into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data types.

The Type Mapping page is available in the **Project Settings** and **Default Project Settings** dialog boxes.

- To specify settings for all future SSMA projects, on the **Tools** menu select **Default Project Settings**, select migration project type for which settings are required to be viewed or changed from **Migration Target Version** dropdown list and then select **Type Mapping** at the bottom of the left pane.

- To specify settings for the current project, on the **Tools** menu select **Project Settings**, and then select **Type Mapping** at the bottom of the left pane.

To specify settings for the current object or class of objects, use the **Type Mapping** tab in the primary SSMA window.

## Options

The following table shows the **Type Mapping** tab options:

#### Source type

The mapped Db2 data type.

#### Target type

The target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type for the specified Db2 data type.

See the tables in the next section for the default SSMA for Db2 type mappings.

#### Add

Select to add a data type to the mapping list.

#### Edit

Select to edit the selected data type in the mapping list.

#### Remove

Select to remove the selected data type mapping from the mapping list.

#### Reset to default

Select to reset the type mapping list to the SSMA defaults.

## Default type mappings

In SSMA for Db2, you can set custom type mappings for arguments, columns, local variables, and return values. The default mapping for arguments and return types is almost identical.

### Default argument type and return value type mapping

The following table contains the default data type mapping for arguments and return values.

| Db2 data type | Default SQL Server data type |
| --- | --- |
| bfile | **varbinary(max)** |
| binary_double | **float(53)** |
| binary_float | **float(53)** |
| binary_integer | **int** |
| blob | **varbinary(max)** |
| boolean | **bit** |
| char | **varchar(max)** |
| char varying | **varchar(max)** |
| character | **varchar(max)** |
| character varying | **varchar(max)** |
| clob | **varchar(max)** |
| date | **datetime2(0)** |
| dec | **decimal(38, 0)** |
| decimal | **float(53)** |
| double precision | **float(53)** |
| float | **float(53)** |
| int | **int** |
| integer | **int** |
| long | **varchar(max)** |
| long raw | **varbinary(max)** |
| long raw[*..8000] <sup>1</sup> | **varbinary**(*n*) |
| long raw[8001..*] <sup>1</sup> | **varbinary(max)** |
| national char | **nvarchar(max)** |
| national char varying | **nvarchar(max)** |
| national character | **nvarchar(max)** |
| national character varying <sup>2</sup> | **nvarchar(max)** |
| national character varying <sup>1</sup> | **nvarchar(max)** |
| nchar | **nvarchar(max)** |
| nclob | **nvarchar(max)** |
| number | **float(53)** |
| numeric | **float(53)** |
| nvarchar2 | **nvarchar(max)** |
| pls_integer | **int** |
| raw | **varbinary(max)** |
| real | **float(53)** |
| rowid | **uniqueidentifier** |
| signtype | **smallint** |
| smallint | **smallint** |
| string | **varchar(max)** |
| timestamp | **datetime2** |
| timestamp with local time zone | **datetimeoffset** |
| timestamp with time zone | **datetimeoffset** |
| urowid | **uniqueidentifier** |
| varchar | **varchar(max)** |
| varchar2 | **varchar(max)** |
| xmltype | **xml** |

<sup>1</sup> Applies to return value type mapping only.

<sup>2</sup> Applies to argument type mapping only.

### Default column type mapping

The following table contains the default type mapping for columns.

| Db2 data type | Default SQL Server data type |
| --- | --- |
| bfile | **varbinary(max)** |
| binary_double | **float(53)** |
| binary_float | **float(53)** |
| blob | **varbinary(max)** |
| char | **char** |
| char varying[*n*] | **varchar(*n*)** |
| char[*n*] | **char(*n*)** |
| character | **char** |
| character varying[*n*] | **varchar(*n*)** |
| character[*n*] | **char(*n*)** |
| clob | **varchar(max)** |
| date | **datetime2(0)** |
| dec | **decimal(38, 0)** |
| dec[*n*] | dec[*n*][0] |
| dec[*x*][*y*] | **decimal(*x*, *y*)** |
| decimal | **decimal(38, 0)** |
| decimal[*n*] | **decimal(*n*, 0)** |
| decimal[*x*][*y*] | **decimal(*x*, *y*)** |
| double precision | **float(53)** |
| float | **float(53)** |
| float[*..53] | **float(*n*)** |
| float[54..*] | **float(53)** |
| int | **int** |
| integer | **int** |
| long | **varchar(max)** |
| long raw | **varbinary(max)** |
| long raw[*..8000] | **varbinary(*n*)** |
| long raw[8001..*] | **varbinary(max)** |
| long varchar | **varchar(max)** |
| long[*..8000] | **varchar(*n*)** |
| long[8001..*] | **varchar(max)** |
| national char | **nchar** |
| national char varying[*n*] | **nvarchar(*n*)** |
| national char[*n*] | **nchar(*n*)** |
| national character | **nchar** |
| national character varying[*n*] | **nvarchar(*n*)** |
| national character[*n*] | **nchar(*n*)** |
| nchar | **nchar** |
| nchar[*n*] | **nchar(*n*)** |
| nclob | **nvarchar(max)** |
| number | **float(53)** |
| number[*n*] | **numeric(*n*)** |
| number[*x*][*y*] | **numeric(*x*, *y*)** |
| numeric | **numeric** |
| numeric[*n*] | **numeric(*n*)** |
| numeric[*x*][*y*] | **numeric(*x*, *y*)** |
| nvarchar2[*n*] | **nvarchar(*n*)** |
| raw[*n*] | **varbinary(*n*)** |
| real | **float(53)** |
| rowid | **uniqueidentifier** |
| smallint | **smallint** |
| timestamp | **datetime2** |
| timestamp with local time zone | **datetimeoffset** |
| timestamp with local time zone[*n*] | **datetimeoffset(*n*)** |
| timestamp with time zone | **datetimeoffset** |
| timestamp with time zone[*n*] | **datetimeoffset(*n*)** |
| timestamp[*n*] | **datetime2(*n*)** |
| Urowid | **uniqueidentifier** |
| urowid[*n*] | **uniqueidentifier** |
| varchar[*n*] | **varchar(*n*)** |
| varchar2[*n*] | **varchar(*n*)** |
| Xmltype | **xml** |

### Default local variable type mapping

The following table contains the default type mapping for local variables.

| Db2 data type | Default SQL Server data type |
| --- | --- |
| Bfile | **varbinary(max)** |
| binary_double | **float(53)** |
| binary_float | **float(53)** |
| binary_interger | **int** |
| Blob | **varbinary(max)** |
| Boolean | **bit** |
| Char | **char** |
| char varying[*..8000] | **varchar(*n*)** |
| char varying[8001..*] | **varchar(max)** |
| char[*..8000] | **char(*n*)** |
| char[8001..*] | **varchar(max)** |
| Character | **char** |
| character varying[*..8000] | **varchar(*n*)** |
| character varying[8001..*] | **varchar(max)** |
| character[*..8000] | **char(*n*)** |
| character[8001..*] | **varchar(max)** |
| clob | **varchar(max)** |
| date | **datetime2(0)** |
| dec | **decimal(38, 0)** |
| dec[*n*] | **decimal(*n*, 0)** |
| dec[*x*][*y*] | **decimal(*x*, *y*)** |
| decimal | **decimal(38, 0)** |
| decimal[*n*] | **decimal(*n*, 0)** |
| decimal[*x*][*y*] | **decimal(*x*, *y*)** |
| double precision | **float(53)** |
| Float | **float(53)** |
| float[*..53] | **float(*n*)** |
| float[54..*] | **float(53)** |
| int | **int** |
| Integer | **int** |
| integer[*n*] | **numeric(*n*, 0)** |
| Long | **varchar(max)** |
| long raw | **varbinary(max)** |
| long raw[*..8000] | **varbinary(*n*)** |
| long raw[8001..*] | **varbinary(max)** |
| national char | **nchar** |
| national char varying[*..4000] | **nvarchar(*n*)** |
| national char varying[4001..*] | **nvarchar(max)** |
| national char[*..4000] | **nchar(*n*)** |
| national char[4001..*] | **nvarchar(max)** |
| national character | **nchar** |
| national character[*..4000] | **nvarchar(*n*)** |
| national character[4001..*] | **nvarchar(max)** |
| national character varying [*..4000] | **nvarchar(*n*)** |
| national character varying [4001..*] | **nvarchar(max)** |
| Nchar | **nchar** |
| nchar[*..4000] | **nchar(*n*)** |
| nchar[4001..*] | **nvarchar(max)** |
| nchar varying [*..4000] | **nvarchar(*n*)** |
| nchar varying [4001..*] | **nvarchar(max)** |
| Nclob | **nvarchar(max)** |
| Number | **float(53)** |
| number[*n*] | **numeric(*n*)** |
| number[*x*][*y*] | **numeric(*x*, *y*)** |
| Numeric | **numeric(38, 0)** |
| numeric[*n*] | **numeric(*n*)** |
| numeric[*x*][*y*] | **numeric(*x*, *y*)** |
| nvarchar2[*..4000] | **nvarchar(*n*)** |
| nvarchar2[4001..*] | **nvarchar(max)** |
| pls_integer | **int** |
| raw[*..8000] | **varbinary(*n*)** |
| raw[8001..*] | **varbinary(max)** |
| Real | **float(53)** |
| Rowid | **uniqueidentifier** |
| Signtype | **smallint** |
| Smallint | **smallint** |
| string[*..8000] | **varchar(*n*)** |
| string[8001..*] | **varchar(max)** |
| timestamp | **datetime2** |
| timestamp with local time zone | **datetimeoffset** |
| timestamp with time zone | **datetimeoffset** |
| timestamp with local time zone[*n*] | **datetimeoffset(*n*)** |
| timestamp with time zone[*n*] | **datetimeoffset(*n*)** |
| timestamp[*n*] | **datetime2(*n*)** |
| Urowid | **uniqueidentifier** |
| urowid[*n*] | **uniqueidentifier** |
| varchar[*..8000] | **varchar(*n*)** |
| varchar[8001..*] | **varchar(max)** |
| varchar2[*..8000] | **varchar(*n*)** |
| varchar2[8001..*] | **varchar(max)** |
| Xmltype | **xml** |

## Related content

- [User interface reference (Db2ToSQL)](user-interface-reference-db2tosql.md)
