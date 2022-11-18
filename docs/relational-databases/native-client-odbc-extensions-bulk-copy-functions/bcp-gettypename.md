---
description: "bcp_gettypename"
title: "bcp_gettypename | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
apiname: 
  - "bcp_gettypename"
apilocation: 
  - "sqlncli11.dll"
apitype: "DLLExport"
helpviewer_keywords: 
  - "bcp_gettypename function"
ms.assetid: 65f036d1-f60e-4b8a-97b3-76fccf0dfed4
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# bcp_gettypename
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the SQL type name for a specified BCP type token.  
  
## Syntax  
  
```  
  
RETCODE bcp_gettypename (  
        INT token,  
        DBBOOL fIsMaxType);  
```  
  
## Arguments  
 *token*  
 A value indicating a BCP type token.  
  
 *field*  
 Indicates if token requested is a max type.  
  
## Returns  
 A string containing the SQL type name corresponding to the BCP type. If an invalid BCP type is specified, an empty string is returned..  
  
## Remarks  
 The BCP type tokens are defined in the sqlncli.h header file and the sqlncli11.lib library.  
  
 The table below specifies the possible BCP types, whether or not they are max types, and the expected output.  
  
|BCP type name|MaxType|Output|  
|-------------------|-------------|------------|  
|**SQLDECIMAL**|Either|**decimal**|  
|**SQLNUMERIC**|Either|**numeric**|  
|**SQLINT1**|Either|**tinyint**|  
|**SQLINT2**|Either|**smallint**|  
|**SQLINT4**|Either|**int**|  
|**SQLMONEY**|Either|**money**|  
|**SQLFLT8**|Either|**float**|  
|**SQLDATETIME**|Either|**datetime**|  
|**SQLBITN**|Either|**bit-null**|  
|**SQLBIT**|Either|**bit**|  
|**SQLBIGCHAR**|No|**char**|  
|**SQLCHARACTER**|No|**char**|  
|**SQLBIGVARCHAR**|No|**varchar**|  
|**SQLVARCHAR**|No|**varchar**|  
|**SQLTEXT**|Either|**text**|  
|**SQLBIGBINARY**|No|**binary**|  
|**SQLBINARY**|No|**Binary**|  
|**SQLBIGVARBINARY**|No|**Varbinary**|  
|**SQLVARBINARY**|No|**Varbinary**|  
|**SQLIMAGE**|Either|**Image**|  
|**SQLINTN**|Either|**int-null**|  
|**SQLDATETIMN**|Either|**datetime-null**|  
|**SQLMONEYN**|Either|**money-null**|  
|**SQLFLTN**|Either|**float-null**|  
|**SQLAOPSUM**|Either|**Sum**|  
|**SQLAOPAVG**|Either|**Avg**|  
|**SQLAOPCNT**|Either|**Count**|  
|**SQLAOPMIN**|Either|**Min**|  
|**SQLAOPMAX**|Either|**Max**|  
|**SQLDATETIM4**|Either|**smalldatetime**|  
|**SQLMONEY4**|Either|**Smallmoney**|  
|**SQLFLT4**|Either|**Real**|  
|**SQLUNIQUEID**|Either|**uniqueidentifier**|  
|**SQLNCHAR**|No|**Nchar**|  
|**SQLNVARCHAR**|No|**Nvarchar**|  
|**SQLNTEXT**|Either|**Ntext**|  
|**SQLVARIANT**|Either|**sql_variant**|  
|**SQLINT8**|Either|**Bigint**|  
|**SQLCHARACTER**|Yes|**varchar(max)**|  
|**SQLBIGCHAR**|Yes|**varchar(max)**|  
|**SQLBIGVARCHAR**|Yes|**varchar(max)**|  
|**SQLVARCHAR**|Yes|**varchar(max)**|  
|**SQLBINARY**|Yes|**varbinary(max)**|  
|**SQLBIGBINARY**|Yes|**varbinary(max)**|  
|**SQLBIGVARBINARY**|Yes|**varbinary(max)**|  
|**SQLVARBINARY**|Yes|**varbinary(max)**|  
|**SQLNCHAR**|Yes|**nvarchar(max)**|  
|**SQLNVARCHAR**|Yes|**nvarchar(max)**|  
|**SQLXML**|Yes|**Xml**|  
|**SQLUDT**|Either|**Udt**|  
  
## bcp_gettypename Support for Enhanced Date and Time Features  
 The token parameter values for date/time types are described in the "Type in sqlncli.h" column of the table in [Bulk Copy Changes for Enhanced Date and Time Types &#40;OLE DB and ODBC&#41;](../../relational-databases/native-client-odbc-date-time/bulk-copy-changes-for-enhanced-date-and-time-types-ole-db-and-odbc.md). The returned value is in the corresponding row of the "File storage type" column.  
  
 For more information, see [Date and Time Improvements &#40;ODBC&#41;](../../relational-databases/native-client-odbc-date-time/date-and-time-improvements-odbc.md).  
  
## See Also  
 [Bulk Copy Functions](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/sql-server-driver-extensions-bulk-copy-functions.md)  
  
  
