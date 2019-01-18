---
title: "Project Settings (Type Mapping) (OracleToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 4bb8466e-2199-4f00-8513-b04e9586723d
author: "Shamikg"
ms.author: "Shamikg"
manager: "v-thobro"
---
# Project Settings (Type Mapping) (OracleToSQL)
The Type Mapping page of the **Project Settings** dialog box contains settings that customize how SSMA converts Oracle data types into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types.  
  
The Type Mapping page is available in the **Project Settings** and **Default Project Settings** dialog boxes.  
  
-   To specify settings for all future SSMA projects, on the **Tools** menu click **Default Project Settings**, select migration project type for which settings are required to be viewed or changed from **Migration Target Version** drop down and then click **Type Mapping** at the bottom of the left pane.  
  
-   To specify settings for the current project, on the **Tools** menu click **Project Settings**, and then click **Type Mapping** at the bottom of the left pane.  
  
To specify settings for the current object or class of objects, use the **Type Mapping** tab in the primary SSMA window.  
  
## Options  
The following table shows the **Type Mapping** tab options:  
  
**Source Type**  
The mapped Oracle data type.  
  
**Target Type**  
The target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type for the specified Oracle data type.  
  
See the tables in the next section for the default SSMA for Oracle type mappings.  
  
**Add**  
Click to add a data type to the mapping list.  
  
**Edit**  
Click to edit the selected data type in the mapping list.  
  
**Remove**  
Click to remove the selected data type mapping from the mapping list.  
  
**Reset to Default**  
Click to reset the type mapping list to the SSMA defaults.  
  
## Default Type Mappings  
In SSMA for Oracle, you can set custom type mappings for arguments, columns, local variables, and return values. The default mapping for arguments and return types is almost identical.  
  
### Default Argument Type and Return Value Type Mapping  
The following table contains the default data type mapping for arguments and return values.  
  
|Oracle Data Type|Default [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Data Type|  
|--------------------|-------------------------------------------------------------------------|  
|bfile|varbinary(max)|  
|binary_double|float[53]|  
|binary_float|float[53]|  
|binary_integer|int|  
|blob|varbinary(max)|  
|boolean|bit|  
|char|varchar(max)|  
|char varying|varchar(max)|  
|character|varchar(max)|  
|character varying|varchar(max)|  
|clob|varchar(max)|  
|date|datetime2[0]|  
|dec|dec[38][0]|  
|decimal|float[53]|  
|double precision|float[53]|  
|float|float[53]|  
|int|int|  
|integer|int|  
|long|varchar(max)|  
|long raw|varbinary(max)|  
|long raw[\*..8000]<sup>*</sup>|varbinary[*]|  
|long raw[8001..\*]<sup>*</sup>|varbinary(max)|  
|national char|nvarchar(max)|  
|national char varying|nvarchar(max)|  
|national character|nvarchar(max)|  
|national character varying<sup>**</sup>|nvarchar(max)|  
|national character varying<sup>*</sup>|nvarchar(max)|  
|nchar|nvarchar(max)|  
|nclob|nvarchar(max)|  
|number|float[53]|  
|numeric|float[53]|  
|nvarchar2|nvarchar(max)|  
|pls_integer|int|  
|raw|varbinary(max)|  
|real|float[53]|  
|rowid|uniqueidentifier|  
|signtype|smallint|  
|smallint|smallint|  
|string|varchar(max)|  
|timestamp|datetime2|  
|timestamp with local time zone|datetimeoffset|  
|timestamp with time zone|datetimeoffset|  
|urowid|uniqueidentifier|  
|varchar|varchar(max)|  
|varchar2|varchar(max)|  
|xmltype|xml|  
  
<sup>*</sup> Applies to return value type mapping only.  
  
<sup>**</sup> Applies to argument type mapping only.  
  
### Default Column Type Mapping  
The following table contains the default type mapping for columns.  
  
|Oracle Data Type|Default [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Data Type|  
|--------------------|-------------------------------------------------------------------------|  
|bfile|varbinary(max)|  
|binary_double|float[53]|  
|binary_float|float[53]|  
|blob|varbinary(max)|  
|char|char|  
|char varying[*..\*]|varchar[*]|  
|char[*..\*]|char[*]|  
|character|char|  
|character varying[*..\*]|varchar[*]|  
|character[*..\*]|char[*]|  
|clob|varchar(max)|  
|date|datetime2[0]|  
|dec|dec[38][0]|  
|dec[*..\*]|dec[*][0]|  
|dec[*..\*][\*..\*]|dec[*][\*]|  
|decimal|decimal[38][0]|  
|decimal[*..\*]|decimal[*][0]|  
|decimal[*..\*][\*..\*]|decimal[*][\*]|  
|double precision|float[53]|  
|float|float[53]|  
|float[*..53]|float[*]|  
|float[54..*]|float[53]|  
|int|int|  
|integer|int|  
|long|varchar(max)|  
|long raw|varbinary(max)|  
|long raw[*..8000]|varbinary[*]|  
|long raw[8001..*]|varbinary(max)|  
|long varchar|varchar(max)|  
|long[*..8000]|varchar[*]|  
|long[8001..*]|varchar(max)|  
|national char|nchar|  
|national char varying[*..\*]|nvarchar[*]|  
|national char[*..\*]|nchar[*]|  
|national character|nchar|  
|national character varying[*..\*]|nvarchar[*]|  
|national character[*..\*]|nchar[*]|  
|nchar|nchar|  
|nchar[*]|nchar[*]|  
|nclob|nvarchar(max)|  
|number|float[53]|  
|number[*..\*]|numeric[*]|  
|number[*..\*][\*..\*]|numeric[*][\*]|  
|numeric|numeric|  
|numeric[*..\*]|numeric[*]|  
|numeric[*..\*][\*..\*]|numeric[*][\*]|  
|nvarchar2[*..\*]|nvarchar[*]|  
|raw[*..\*]|varbinary[*]|  
|real|float[53]|  
|rowid|uniqueidentifier|  
|smallint|smallint|  
|timestamp|datetime2|  
|timestamp with local time zone|datetimeoffset|  
|timestamp with local time zone[*..\*]|datetimeoffset[*]|  
|timestamp with time zone|datetimeoffset|  
|timestamp with time zone[*..\*]|datetimeoffset[*]|  
|timestamp[*..\*]|datetime2[*]|  
|Urowid|uniqueidentifier|  
|urowid[*..\*]|uniqueidentifier|  
|varchar[*..\*]|varchar[*]|  
|varchar2[*..\*]|varchar[*]|  
|Xmltype|xml|  
  
### Default Local Variable Type Mapping  
The following table contains the default type mapping for local variables.  
  
|Oracle Data Type|Default [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Data Type|  
|--------------------|-------------------------------------------------------------------------|  
|Bfile|varbinary(max)|  
|binary_double|float[53]|  
|binary_float|float[53]|  
|binary_interger|int|  
|Blob|varbinary(max)|  
|Boolean|bit|  
|Char|char|  
|char varying[*..8000]|varchar[*]|  
|char varying[8001..*]|varchar(max)|  
|char[*..8000]|char[*]|  
|char[8001..*]|varchar(max)|  
|Character|char|  
|character varying[*..8000]|varchar[*]|  
|character varying[8001..*]|varchar(max)|  
|character[*..8000]|char[*]|  
|character[8001..*]|varchar(max)|  
|clob|varchar(max)|  
|date|datetime2[0]|  
|dec|dec[38][0]|  
|dec[*..\*]|dec[*][0]|  
|dec[*..\*][\*..\*]|dec[*][\*]|  
|decimal|decimal[38][0]|  
|decimal[*..\*]|decimal[*][0]|  
|decimal[*..\*][\*..\*]|decimal[*][\*]|  
|double precision|float[53]|  
|Float|float[53]|  
|float[*..53]|float[*]|  
|float[54..*]|float[53]|  
|Int|int|  
|Integer|int|  
|integer[*..\*]|numeric[*][0]|  
|Long|varchar(max)|  
|long raw|varbinary(max)|  
|long raw[*..8000]|varbinary[*]|  
|long raw[8001..*]|varbinary(max)|  
|national char|nchar|  
|national char varying[*..4000]|nvarchar[*]|  
|national char varying[4001..*]|nvarchar(max)|  
|national char[*..4000]|nchar[*]|  
|national char[4001..*]|nvarchar(max)|  
|national character|nchar|  
|national character[*..4000]|nvarchar[*]|  
|national character[4001..*]|nvarchar(max)|  
|national character varying [*..4000]|nvarchar[*]|  
|national character varying [4001..*]|nvarchar(max)|  
|Nchar|nchar|  
|nchar[*..4000]|nchar[*]|  
|nchar[4001..*]|nvarchar(max)|  
|nchar varying [*..4000]|nvarchar[*]|  
|nchar varying [4001..*]|nvarchar(max)|  
|Nclob|nvarchar(max)|  
|Number|float[53]|  
|number[*..\*]|numeric[*]|  
|number[*..\*][\*..\*]|numeric[*][\*]|  
|Numeric|numeric[38][0]|  
|numeric[*..\*]|numeric[*]|  
|numeric[*..\*][\*..\*]|numeric[*][\*]|  
|nvarchar2[*..4000]|nvarchar[*]|  
|nvarchar2[4001..*]|nvarchar(max)|  
|pls_integer|int|  
|raw[*..8000]|varbinary[*]|  
|raw[8001..*]|varbinary(max)|  
|Real|float[53]|  
|Rowid|uniqueidentifier|  
|Signtype|smallint|  
|Smallint|smallint|  
|string[*..8000]|varchar[*]|  
|string[8001..*]|varchar(max)|  
|timestamp|datetime2|  
|timestamp with local time zone|datetimeoffset|  
|timestamp with time zone|datetimeoffset|  
|timestamp with local time zone[*..\*]|datetimeoffset[*]|  
|timestamp with time zone[*..\*]|datetimeoffset[*]|  
|timestamp[*..\*]|datetime2[*]|  
|Urowid|uniqueidentifier|  
|urowid[*..\*]|uniqueidentifier|  
|varchar[*..8000]|varchar[*]|  
|varchar[8001..*]|varchar(max)|  
|varchar2[*..8000]|varchar[*]|  
|varchar2[8001..*]|varcha(max)|  
|Xmltype|xml|  
  
## See Also  
[User Interface Reference &#40;OracleToSQL&#41;](../../ssma/oracle/user-interface-reference-oracletosql.md)  
  
