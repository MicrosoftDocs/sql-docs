---
title: "SSVARIANT Structure | Microsoft Docs"
description: "SSVARIANT structure in OLE DB Driver for SQL Server"
ms.custom: ""
ms.date: "06/15/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
f1_keywords: 
  - "SSVARIANT"
helpviewer_keywords: 
  - "SSVARIANT struct"
author: pmasl
ms.author: pelopes
---
# SSVARIANT Structure
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The **SSVARIANT** structure, which is defined in msoledbsql.h, corresponds to a DBTYPE_SQLVARIANT value in the OLE DB Driver for SQL Server.  
  
 **SSVARIANT** is a discriminating union. Depending on the value of the vt member, the consumer can determine which member to read. vt values correspond to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types. Therefore, the **SSVARIANT** structure can hold any SQL Server type. For more information about the data structure for standard OLE DB types, see [Type Indicators](https://go.microsoft.com/fwlink/?LinkId=122171).  
  
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
|NCharVal|No corresponding OLE DB type indicator.|**struct _NCharVal**|**VT_SS_WVARSTRING,**<br /><br /> **VT_SS_WSTRING**|Supports the **nchar** and **nvarchar**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types.<br /><br /> Includes the following members:<br /><br /> *sActualLength* (**SHORT**) Specifies the actual length for the string to which *pwchNCharVal* points. Does not include terminating zero.<br /><br /> *sMaxLength* (**SHORT**) Specifies the maximum length for the string to which *pwchNCharVal* points.<br /><br /> *pwchNCharVal* (**WCHAR** \*) Pointer to the string.<br /><br /> *dwReserved* (**DWORD**) Used internally by driver only<br /><br /> Unused members: *rgbReserved*, and *pwchReserved*.|  
|CharVal|No corresponding OLE DB type indicator.|**struct _CharVal**|**VT_SS_STRING,**<br /><br /> **VT_SS_VARSTRING**|Supports the **char** and **varchar**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types.<br /><br /> Includes the following members:<br /><br /> *sActualLength* (**SHORT**) Specifies the actual length for the string to which *pchCharVal* points. Does not include terminating zero.<br /><br /> *sMaxLength* (**SHORT**) Specifies the maximum length for the string to which *pchCharVal* points.<br /><br /> *pchCharVal* (**CHAR** \*) Pointer to the string.<br /><br /> *dwReserved* (**DWORD**) Used internally by driver only.<br /><br /> Unused members: *rgbReserved*, and *pwchReserved*.|  
|BinaryVal|No corresponding OLE DB type indicator.|**struct _BinaryVal**|**VT_SS_VARBINARY,**<br /><br /> **VT_SS_BINARY**|Supports the **binary** and **varbinary**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types.<br /><br /> Includes the following members:<br /><br /> *sActualLength* (**SHORT**) Specifies the actual length for the data to which *prgbBinaryVal* points.<br /><br /> *sMaxLength* (**SHORT**) Specifies the maximum length for the data to which *prgbBinaryVal* points.<br /><br /> *prgbBinaryVal* (**BYTE** \*) Pointer to the binary data.<br /><br /> *dwReserved* (**DWORD**) Used internally by driver only.|  
|UnknownType|UNUSED|UNUSED|UNUSED|UNUSED|  
|BLOBType|UNUSED|UNUSED|UNUSED|UNUSED|  
| &nbsp; | &nbsp; | &nbsp; | &nbsp; | &nbsp; |
  

## Problems with the sql_variant data type and recovery procedure
### Problems
Before version 18.4 of the OLE DB driver, insertion into a `sql_variant` column could result in corruption of data on the server if the `SSVARIANT` structure was of type `VT_SS_VARSTRING` or `VT_SS_STRING` containing non-ASCII characters, and either of the following conditions were true:
- The `pwszDataSourceType` field in the `DBPARAMBINDINFO` structure describing the `sql_variant` parameter was set to `L"DBTYPE_SQLVARIANT"`or `L"sql_variant"`. For details, see: [ICommandWithParameters::SetParameterInfo](https://docs.microsoft.com/previous-versions/windows/desktop/ms725393(v=vs.85))

  *or*
- The parameterized SQL query used for insertion was prepared.

More specifically, the OLE DB driver didn't translate data to server's code page before insertion. However, the driver wrongly indicated to the server that the data was encoded in server's code page. This behavior resulted in a mismatch between the data and its corresponding code page stored in the `sql_variant` column.

Similarly, upon retrieval of the same value, the OLE DB driver didn't translate strings to client's code page. However, since inserted data was already in client's code page (See the paragraph above), client applications could interpret data correctly. Even so, applications using other drivers retrieved these values in corrupted format. The corruption occurred because other drivers (wrongly) interpreted the string in server's code page and attempted to translate it to client's code page.

Since version 18.4, the OLE DB Driver ensures that narrow string data stored in the `SSVARIANT` structure are translated to server's code page before insertion. Similarly, the data is translated back to client's code page upon retrieval. As a result, clients that rely on the mentioned bug might experience issues while retrieving data that is inserted using an OLE DB driver before version 18.4. The [Recovery procedure](#recovery-procedure) below aims to provide guidance to resolve these issues.

### Recovery procedure
If your application experiences issues retrieving `DBTYPE_SQLVARIANT` data type after switching to version 18.4, the following steps need to be taken:
- Switch to a version of the driver before 18.4.
- Retrieve data while specifying `DBTYPE_STR` in the `DBBINDING` structure passed to `IAccessor::CreateAccessor`.
- Insert the retrieved data back into the column while specifying `L"DBTYPE_STR"` as the type of parameter provided to `ICommandWithParameters::SetParameterInfo`. To avoid the problem described in the previous section, the parameterized query *must not* be prepared.
- Once the above steps are done, you should be able switch back to version 18.4 of the driver and retrieve the data properly.

## See Also  
 [Data Types &#40;OLE DB&#41;](../../oledb/ole-db-data-types/data-types-ole-db.md)  
  
  
