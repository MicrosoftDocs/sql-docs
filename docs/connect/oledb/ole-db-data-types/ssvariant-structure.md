---
title: SSVARIANT structure (OLE DB driver)
description: "SSVARIANT structure in OLE DB Driver for SQL Server"
author: David-Engel
ms.author: v-davidengel
ms.date: "06/15/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
f1_keywords:
  - "SSVARIANT"
helpviewer_keywords:
  - "SSVARIANT struct"
---
# SSVARIANT Structure
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The **SSVARIANT** structure, which is defined in msoledbsql.h, corresponds to a DBTYPE_SQLVARIANT value in the OLE DB Driver for SQL Server.  
  
 **SSVARIANT** is a discriminating union. Depending on the value of the vt member, the consumer can determine which member to read. vt values correspond to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types. Therefore, the **SSVARIANT** structure can hold any SQL Server type. For more information about the data structure for standard OLE DB types, see [Type Indicators](/previous-versions/windows/desktop/ms711251(v=vs.85)).  
  
## Remarks  
 When DataTypeCompat==80, several **SSVARIANT** subtypes become strings. For example, the following vt values will appear in **SSVARIANT** as VT_SS_WVARSTRING:  
  
-   VT_SS_DATETIMEOFFSET  
  
-   VT_SS_DATETIME2  
  
-   VT_SS_TIME2  
  
-   VT_SS_DATE  
  
 When DateTypeCompat == 0, these types will appear in their native form.  
  
 For more information about SSPROP_INIT_DATATYPECOMPATIBILITY, see [Using Connection String Keywords with OLE DB Driver for SQL Server](../../oledb/applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md).  
  
 The msoledbsql.h file contains variant access macros that simplify dereferencing the member types in the **SSVARIANT** structure. An example is V_SS_DATETIMEOFFSET, which you can use as follows:  
  
```  
memcpy(&V_SS_DATETIMEOFFSET(pssVar).tsoDateTimeOffsetVal, pDTO, cbNative);  
V_SS_DATETIMEOFFSET(pssVar).bScale = bScale;  
```  
  
 For the complete set of access macros for each member of the **SSVARIANT** structure, refer to the msoledbsql.h file.  
  
 The following table describes the members of the **SSVARIANT** structure:  
  
|Member|OLE DB type indicator|OLE DB C data type|vt value|Comments|  
|------------|---------------------------|------------------------|--------------|--------------|  
|vt|SSVARTYPE|||Specifies the type of value contained in the **SSVARIANT** struct.|  
|bTinyIntVal|DBTYPE_UI1|**BYTE**|**VT_SS_UI1**|Supports the **tinyint**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type.|  
|sShortIntVal|DBTYPE_I2|**SHORT**|**VT_SS_I2**|Supports the **smallint**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type.|  
|lIntVal|DBTYPE_I4|**LONG**|**VT_SS_I4**|Supports the **int**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type.|  
|llBigIntVal|DBTYPE_I8|**LARGE_INTEGER**|**VT_SS_I8**|Supports the **bigint**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type.|  
|fltRealVal|DBTYPE_R4|**float**|**VT_SS_R4**|Supports the **real**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type.|  
|dblFloatVal|DBTYPE_R8|**double**|**VT_SS_R8**|Supports the **float**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type.|  
|cyMoneyVal|DBTYPE_CY|**LARGE_INTEGER**|**VT_SS_MONEY VT_SS_SMALLMONEY**|Supports the **money** and **smallmoney**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types.|  
|fBitVal|DBTYPE_BOOL|**VARIANT_BOOL**|**VT_SS_BIT**|Supports the **bit**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type.|  
|rgbGuidVal|DBTYPE_GUID|**GUID**|**VT_SS_GUID**|Supports the **uniqueidentifier**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type.|  
|numNumericVal|DBTYPE_NUMERIC|**DB_NUMERIC**|**VT_SS_NUMERIC**|Supports the **numeric**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type.|  
|dDateVal|DBTYPE_DATE|**DBDATE**|**VT_SS_DATE**|Supports the **date**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type.|  
|tsDateTimeVal|DBTYPE_DBTIMESTAMP|**DBTIMESTAMP**|**VT_SS_SMALLDATETIME VT_SS_DATETIME VT_SS_DATETIME2**|Supports the **smalldatetime**, **datetime**, and **datetime2**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types.|  
|Time2Val|DBTYPE_DBTIME2|**DBTIME2**|**VT_SS_TIME2**|Supports the **time**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type.<br /><br /> Includes the following members:<br /><br /> *tTime2Val* (**DBTIME2**)<br /><br /> *bScale* (**BYTE**) Specifies the scale for *tTime2Val* value.|  
|DateTimeVal|DBTYPE_DBTIMESTAMP|**DBTIMESTAMP**|**VT_SS_DATETIME2**|Supports the **datetime2**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type.<br /><br /> Includes the following members:<br /><br /> *tsDataTimeVal* (DBTIMESTAMP)<br /><br /> *bScale* (**BYTE**) Specifies the scale for *tsDataTimeVal* value.|  
|DateTimeOffsetVal|DBTYPE_DBTIMESTAMPOFSET|**DBTIMESTAMPOFFSET**|**VT_SS_DATETIMEOFFSET**|Supports the **datetimeoffset**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type.<br /><br /> Includes the following members:<br /><br /> *tsoDateTimeOffsetVal* (**DBTIMESTAMPOFFSET**)<br /><br /> *bScale* (**BYTE**) Specifies the scale for *tsoDateTimeOffsetVal* value.|  
|NCharVal|No corresponding OLE DB type indicator.|**struct _NCharVal**|**VT_SS_WVARSTRING,**<br /><br /> **VT_SS_WSTRING**|Supports the **nchar** and **nvarchar**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types.<br /><br /> Includes the following members:<br /><br /> *sActualLength* (**SHORT**) Specifies the actual length for the string to which *pwchNCharVal* points. Does not include terminating zero.<br /><br /> *sMaxLength* (**SHORT**) Specifies the maximum length for the string to which *pwchNCharVal* points.<br /><br /> *pwchNCharVal* (**WCHAR** \*) Pointer to the string.<br /><br /> *rgbReserved* (**BYTE[5]**) Specifies the collation information.<br /><br /> Unused members: *dwReserved*, and *pwchReserved*.|  
|CharVal|No corresponding OLE DB type indicator.|**struct _CharVal**|**VT_SS_STRING,**<br /><br /> **VT_SS_VARSTRING**|Supports the **char** and **varchar**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types.<br /><br /> Includes the following members:<br /><br /> *sActualLength* (**SHORT**) Specifies the actual length for the string to which *pchCharVal* points. Does not include terminating zero.<br /><br /> *sMaxLength* (**SHORT**) Specifies the maximum length for the string to which *pchCharVal* points.<br /><br /> *pchCharVal* (**CHAR** \*) Pointer to the string.<br /><br /> *rgbReserved* (**BYTE[5]**) Specifies the collation information.<br /><br /> Unused members:<br /><br /> *dwReserved*, and *pwchReserved*.|  
|BinaryVal|No corresponding OLE DB type indicator.|**struct _BinaryVal**|**VT_SS_VARBINARY,**<br /><br /> **VT_SS_BINARY**|Supports the **binary** and **varbinary**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types.<br /><br /> Includes the following members:<br /><br /> *sActualLength* (**SHORT**) Specifies the actual length for the data to which *prgbBinaryVal* points.<br /><br /> *sMaxLength* (**SHORT**) Specifies the maximum length for the data to which *prgbBinaryVal* points.<br /><br /> *prgbBinaryVal* (**BYTE** \*) Pointer to the binary data.<br /><br /> Unused member: *dwReserved*.|  
|UnknownType|UNUSED|UNUSED|UNUSED|UNUSED|  
|BLOBType|UNUSED|UNUSED|UNUSED|UNUSED|  
  
## Known issues
### Possible narrow string data corruption
Before version 18.4 of the OLE DB driver, insertion into a `sql_variant` column could result in data corruption on the server if all of the following conditions were true:
- The client machine code page didn't match the database collation code page.
- The client buffer to insert contained non-ASCII narrow string characters encoded in the client code page.
- Either of the following conditions were true:
  - The `pwszDataSourceType` field in the `DBPARAMBINDINFO` structure describing the parameter corresponding to the `sql_variant` column was set to `L"DBTYPE_SQLVARIANT"`, `L"DBTYPE_VARIANT"`, or `L"sql_variant"`. For details, see: [ICommandWithParameters::SetParameterInfo](/previous-versions/windows/desktop/ms725393(v=vs.85)).

    *or*
  - The parameterized SQL query used for insertion was prepared.

More specifically, the OLE DB driver didn't translate the data to the database collation code page before inserting it. However, the driver wrongly indicated to the server that the data was encoded in the database collation code page. This behavior resulted in a mismatch between the data and its corresponding code page stored in the `sql_variant` column.

Similarly, upon retrieval of the same value, the OLE DB driver didn't translate strings to the client code page. However, since the inserted data was already in the client code page (see the paragraph above), the client application could interpret the data correctly. Even so, applications using other drivers would retrieve these values in a corrupted format. The corruption occurs because other drivers interpreted the string in the database collation code page and attempted to translate it to the client code page.

Starting from version 18.4, the OLE DB Driver translates the narrow strings to the database collation code page before the insertion. Similarly, the driver translates the data back to the client code page upon retrieval. As a result, client applications that rely on the bug mentioned above might experience issues while retrieving data that is inserted using an earlier version of the OLE DB Driver. The [recovery procedure](#recovery-procedure) below aims to provide guidance to resolve these issues.

### Recovery procedure
> [!IMPORTANT]  
> Before performing the recovery steps below, make sure to back up your existing data.

If your application experiences issues retrieving data from a `sql_variant` column after switching to version 18.4 of the OLE DB driver, the corrupted data needs to be modified to have the same collation as the database in which the data is stored. The following script can be used to recover a single value from a `sql_variant` column. The script is a template and you need to adjust it to fit your scenario.

> [!IMPORTANT]  
> Since the original code page of the data isn't stored, you need to tell the server how the data was initially encoded. To do so, execute the script within the context of a database that has the same code page as the code page of the client which initially inserted the data. For example, if the corrupted data was inserted from a client configured with code page `932`, the following script needs to be executed within the context of a database with a Japanese collation (e.g. `Japanese_XJIS_100_CS_AI`).

```sql
/*
    Description:
        Template that can be used to recover the corrupted value inserted into the sql_variant column.

    Scenario:
        The database is named [YourDatabase] and it contains a table named [YourTable], which contains the corrupted value.
        Schema is named [dbo].
        The corrupted value is stored in a column of type sql_variant named [YourColumn].
        The corrupted value is sql_variant of BaseType char. For details on sql_variant properties, see:
            https://learn.microsoft.com/sql/t-sql/functions/sql-variant-property-transact-sql
*/

-- Base type in sql_variant can hold a maximum of 8000 bytes
-- For details see: 
--  https://learn.microsoft.com/sql/t-sql/data-types/sql-variant-transact-sql#remarks
DECLARE @bin VARBINARY(8000)

-- In the following lines we convert the sql_variant base type to binary.
-- <FilterExpression>
--      Is a placeholder and must be replaced with an expression that filters a single corrupted value to be recovered.
--      Therefore, the expression must result in a single value being returned only.
SET @bin = (SELECT CAST([YourColumn] AS VARBINARY(8000)) FROM [YourDatabase].[dbo].[YourTable] WHERE <FilterExpression>)

-- In the following lines we store the binary value in char(59) (a fixed-size character data type).
-- IMPORTANT NOTE: 
--      This example assumes the corrupted sql_variant's base type is char(59).
--      You MUST adjust the type (that is, char/varchar) and size to match your scenario exactly.
DECLARE @char CHAR(59)
SET @char = CAST((@bin) AS CHAR(59))
DECLARE @sqlvariant sql_variant

-- The following lines recover the corrupted value by translating the value to the collation of the database.
-- <DBCollation>
--      Must be replaced with the collation (for example, Latin1_General_100_CI_AS_SC_UTF8) of the database holding the data.
SET @sqlvariant = @char collate <DBCollation>

-- Finally, we update the corrupted value with the recovered value.
-- "<FilterExpression>"
--      Is a placeholder and must be replaced with an expression that filters a single corrupted value to be recovered.
--      Therefore, the expression must result in a single value being returned only.
UPDATE [YourDatabase].[dbo].[YourTable] SET [YourColumn] = @sqlvariant WHERE <FilterExpression>
```

## See Also  
 [Data Types &#40;OLE DB&#41;](../../oledb/ole-db-data-types/data-types-ole-db.md)  
  
