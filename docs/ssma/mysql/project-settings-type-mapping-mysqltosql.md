---
description: "Project Settings (Type Mapping) (MySQLToSQL)"
title: "Project Settings (Type Mapping) (MySQLToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 136fdf6d-657f-447b-af41-49bbc6e0e93e
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
    - "ssma.mysql.projectsettingtypemapping.f1"
---
# Project Settings (Type Mapping) (MySQLToSQL)
The Type Mapping project settings let you set default type mappings for the SSMA project.  

Type mapping is available in the Project Settings and Default Project Settings dialog boxes:  
  
-   Use the Project Settings dialog box to set configuration options for the current project. To access the type mapping settings, on the Tools menu, select Project Settings, and then click Type Mapping in the left pane.  
  
-   Use the Default Project Settings dialog box to set configuration options for all projects. To access the type mapping settings, on the Tools menu, select Default Project Settings, select migration project type for which settings are required to be viewed /changed from **Migration Target Version** drop down and then click Type Mapping in the left pane.  
  
## Options  
  
##### Source Type  
It is the MySQL data type, which has to be mapped to the target database data type.  
  
##### Target Type  
The target database data type for the specified MySQL data type.  
  
##### Add  
Click to add a data type to the mapping list.  
  
##### Edit  
Click to edit the selected data type in the mapping list.  
  
##### Remove  
Click to remove the selected data type mapping from the mapping list.  
  
##### Reset to Default  
Click to reset the type mapping list to the SSMA defaults.  
  
## Type Mappings  
The following table shows the default mapping between source and target data types  
  
|MySQL Data Type|SQL Server Data Type|  
|-|-|  
|bigint|bigint|  
|bigint[*..255]|bigint|  
|binary|binary[1]|  
|binary[0..1]|binary[1]|  
|binary[2..255]|binary[*]|  
|bit|binary[1]|  
|bit[0..8]|binary[1]|  
|bit[17..24]|binary[3]|  
|bit[25..32]|binary[4]|  
|bit[33..40]|binary[5]|  
|bit[41..48]|binary[6]|  
|bit[49..56]|binary[7]|  
|bit[57..64]|binary[8]|  
|bit[9..16]|binary[2]|  
|blob|varbinary(max)|  
|blob[0..1]|varbinary[1]|  
|blob[2..8000]|varbinary[*]|  
|blob[8001..*]|varbinary(max)|  
|bool|bit|  
|boolean|bit|  
|char|nchar[1]|  
|char byte|binary[1]|  
|char byte[0..1]|binary[1]|  
|char byte[2..255]|binary[*]|  
|char[0..1]|nchar[1]|  
|char[2..255]|nchar[*]|  
|character|nchar[1]|  
|character varying[0..1]|nvarchar[1]|  
|character varying[2..255]|nvarchar|  
|character[0..1]|nchar[1]|  
|character[2..255]|nchar[*]|  
|date|date|  
|datetime|datetime2[0]|  
|dec|decimal|  
|dec[*..65]|decimal[*][0]|  
|dec[*..65][\*..30]|decimal[*][\*]|  
|decimal|decimal|  
|decimal[*..65]|decimal[*][0]|  
|decimal[*..65][\*..30]|decimal[*][\*]|  
|double|float[53]|  
|double precision|float[53]|  
|double precision[*..255][\*..30]|numeric[*][\*]|  
|double[*..255][\*..30]|numeric[*][\*]|  
|fixed|numeric|  
|fixed[*..65][\*..30]|numeric[*][\*]|  
|float|float[24]|  
|float[*..255][\*..30]|numeric[*][\*]|  
|float[*..53]|float[53]|  
|int|int|  
|int[*..255]|int|  
|integer|int|  
|integer[*..255]|int|  
|longblob|varbinary(max)|  
|longtext|nvarchar(max)|  
|mediumblob|varbinary(max)|  
|mediumint|int|  
|mediumint[*..255]|int|  
|mediumtext|nvarchar(max)|  
|national char|nchar[1]|  
|national char[0..1]|nchar[1]|  
|national char[2..255]|nchar[*]|  
|national character|nchar[1]|  
|national character varying|nvarchar[1]|  
|national character varying[0..1]|nvarchar[1]|  
|national character varying[2..4000]|nvarchar[*]|  
|national character varying[4001..*]|nvarchar(max)|  
|national character[0..1]|nchar[1]|  
|national character[2..255]|nchar[*]|  
|national varchar|nvarchar[1]|  
|national varchar[0..1]|nvarchar[1]|  
|national varchar[2..4000]|nvarchar[*]|  
|national varchar[4001..*]|nvarchar(max)|  
|nchar|nchar[1]|  
|nchar varchar|nvarchar[1]|  
|nchar varchar[0..1]|nvarchar[1]|  
|nchar varchar[2..4000]|nvarchar[*]|  
|nchar varchar[4001..*]|nvarchar(max)|  
|nchar[0..1]|nchar[1]|  
|nchar[2..255]|nchar[*]|  
|numeric|numeric|  
|numeric[*..65]|numeric[*][0]|  
|numeric[*..65][\*..30]|numeric[*][\*]|  
|nvarchar|nvarchar[1]|  
|nvarchar[0..1]|nvarchar[1]|  
|nvarchar[2..4000]|nvarchar[*]|  
|nvarchar[4001..*]|nvarchar(max)|  
|real|float[53]|  
|real[*..255][\*..30]|numeric[*][\*]|  
|serial|bigint|  
|smallint|smallint|  
|smallint[*..255]|smallint|  
|text|nvarchar(max)|  
|text[0..1]|nvarchar[1]|  
|text[2..4000]|nvarchar[*]|  
|text[4001..*]|nvarchar(max)|  
|time|time|  
|timestamp|datetime|  
|tinyblob|varbinary[255]|  
|tinyint|smallint|  
|tinyint[*..255]|smallint|  
|tinytext|nvarchar[255]|  
|unsigned bigint|bigint|  
|unsigned bigint[*..255]|bigint|  
|unsigned dec|decimal|  
|unsigned dec[*..65]|decimal[*][0]|  
|unsigned dec[*..65][\*..30]|decimal[*][\*]|  
|unsigned decimal|decimal|  
|unsigned decimal[*..65]|decimal[*][0]|  
|unsigned decimal[*..65][\*..30]|decimal[*][\*]|  
|unsigned double|float[53]|  
|unsigned double precision|float[53]|  
|unsigned double precision[*..255][\*..30]|numeric[*][\*]|  
|unsigned double[*..255][\*..30]|numeric[*][\*]|  
|unsigned fixed|numeric|  
|unsigned fixed[*..65][\*..30]|numeric[*][\*]|  
|unsigned float|float[24]|  
|unsigned float[*..255][\*..30]|numeric[*][\*]|  
|unsigned float[*..53]|float[53]|  
|unsigned int|bigint|  
|unsigned int[*..255]|bigint|  
|unsigned integer|bigint|  
|unsigned integer[*..255]|bigint|  
|unsigned mediumint|int|  
|unsigned mediumint[*..255]|int|  
|unsigned numeric|numeric|  
|unsigned numeric[*..65]|numeric[*][0]|  
|unsigned numeric[*..65][\*..30]|numeric[*][\*]|  
|unsigned real|float[53]|  
|unsigned real[*..255[[\*..30]|numeric[*][\*]|  
|unsigned smallint|int|  
|unsigned smallint[*..255]|int|  
|unsigned tinyint|tinyint|  
|unsigned tinyint[*..255]|tinyint|  
|varbinary[0..1]|varbinary[1]|  
|varbinary[2..8000]|varbinary[*]|  
|varbinary[8001..*]|varbinary(max)|  
|varchar[0..1]|nvarchar[1]|  
|varchar[2..4000]|nvarchar[*]|  
|varchar[4001..*]|nvarchar(max)|  
|year|smallint|  
|year[2..2]|smallint|  
|year[4..4]|smallint|  
  
##### Add  
Click to add a data type to the mapping list.  
  
##### Edit  
Click to edit a data type in the mapping list.  
  
##### Remove  
Click to remove the selected data type mapping from the mapping list.  
  
##### Reset to Default  
Click to reset all data type mappings to the SSMA defaults.  
  
