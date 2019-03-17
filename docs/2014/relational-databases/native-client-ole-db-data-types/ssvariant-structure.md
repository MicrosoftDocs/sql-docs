---
title: "SSVARIANT Structure | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
f1_keywords: 
  - "SSVARIANT"
helpviewer_keywords: 
  - "SSVARIANT struct"
ms.assetid: d13c6aa6-bd49-467a-9093-495df8f1e2d9
author: MightyPen
ms.author: genemi
manager: craigg
---
# SSVARIANT Structure
  The `SSVARIANT` structure, which is defined in sqlncli.h, corresponds to a DBTYPE_SQLVARIANT value in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLEDB provider.  
  
 `SSVARIANT` is a discriminating union. Depending on the value of the vt member, the consumer can determine which member to read. vt values correspond to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types. Therefore, the `SSVARIANT` structure can hold any SQL Server type. For more information about the data structure for standard OLE DB types, see [Type Indicators](https://go.microsoft.com/fwlink/?LinkId=122171).  
  
## Remarks  
 When DataTypeCompat==80, several `SSVARIANT` subtypes become strings. For example, the following vt values will appear in `SSVARIANT` as VT_SS_WVARSTRING:  
  
-   VT_SS_DATETIMEOFFSET  
  
-   VT_SS_DATETIME2  
  
-   VT_SS_TIME2  
  
-   VT_SS_DATE  
  
 When DateTypeCompat == 0, these types will appear in their native form.  
  
 For more information about SSPROP_INIT_DATATYPECOMPATIBILITY, see [Using Connection String Keywords with SQL Server Native Client](../native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).  
  
 The sqlncli.h file contains variant access macros that simplify dereferencing the member types in the `SSVARIANT` structure. An example is V_SS_DATETIMEOFFSET, which you can use as follows:  
  
```  
memcpy(&V_SS_DATETIMEOFFSET(pssVar).tsoDateTimeOffsetVal, pDTO, cbNative);  
V_SS_DATETIMEOFFSET(pssVar).bScale = bScale;  
```  
  
 For the complete set of access macros for each member of the `SSVARIANT` structure, refer to the sqlncli.hi file.  
  
 The following table describes the members of the `SSVARIANT` structure:  
  
|Member|OLE DB type indicator|OLE DB C data type|vt value|Comments|  
|------------|---------------------------|------------------------|--------------|--------------|  
|vt|SSVARTYPE|||Specifies the type of value contained in the `SSVARIANT` struct.|  
|bTinyIntVal|DBTYPE_UI1|`BYTE`|`VT_SS_UI1`|Supports the `tinyint`[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.|  
|sShortIntVal|DBTYPE_I2|`SHORT`|`VT_SS_I2`|Supports the `smallint`[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.|  
|lIntVal|DBTYPE_I4|`LONG`|`VT_SS_I4`|Supports the `int`[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.|  
|llBigIntVal|DBTYPE_I8|`LARGE_INTEGER`|`VT_SS_I8`|Supports the `bigint`[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.|  
|fltRealVal|DBTYPE_R4|`float`|`VT_SS_R4`|Supports the `real`[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.|  
|dblFloatVal|DBTYPE_R8|`double`|`VT_SS_R8`|Supports the `float`[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.|  
|cyMoneyVal|DBTYPE_CY|`LARGE_INTEGER`|**VT_SS_MONEY VT_SS_SMALLMONEY**|Supports the `money` and **smallmoney**[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types.|  
|fBitVal|DBTYPE_BOOL|`VARIANT_BOOL`|`VT_SS_BIT`|Supports the `bit`[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.|  
|rgbGuidVal|DBTYPE_GUID|`GUID`|`VT_SS_GUID`|Supports the `uniqueidentifier`[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.|  
|numNumericVal|DBTYPE_NUMERIC|`DB_NUMERIC`|`VT_SS_NUMERIC`|Supports the `numeric`[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.|  
|dDateVal|DBTYPE_DATE|`DBDATE`|`VT_SS_DATE`|Supports the `date`[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.|  
|tsDateTimeVal|DBTYPE_DBTIMESTAMP|`DBTIMESTAMP`|`VT_SS_SMALLDATETIME VT_SS_DATETIME VT_SS_DATETIME2`|Supports the `smalldatetime`, `datetime`, and `datetime2`[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types.|  
|Time2Val|DBTYPE_DBTIME2|`DBTIME2`|`VT_SS_TIME2`|Supports the `time`[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.<br /><br /> Includes the following members:<br /><br /> *tTime2Val* (`DBTIME2`)<br /><br /> *bScale* (`BYTE`) Specifies the scale for *tTime2Val* value.|  
|DateTimeVal|DBTYPE_DBTIMESTAMP|`DBTIMESTAMP`|`VT_SS_DATETIME2`|Supports the `datetime2`[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.<br /><br /> Includes the following members:<br /><br /> *tsDataTimeVal* (DBTIMESTAMP)<br /><br /> *bScale* (`BYTE`) Specifies the scale for *tsDataTimeVal* value.|  
|DateTimeOffsetVal|DBTYPE_DBTIMESTAMPOFSET|`DBTIMESTAMPOFFSET`|`VT_SS_DATETIMEOFFSET`|Supports the `datetimeoffset`[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.<br /><br /> Includes the following members:<br /><br /> *tsoDateTimeOffsetVal* (`DBTIMESTAMPOFFSET`)<br /><br /> *bScale* (`BYTE`) Specifies the scale for *tsoDateTimeOffsetVal* value.|  
|NCharVal|No corresponding OLE DB type indicator.|`struct _NCharVal`|`VT_SS_WVARSTRING,`<br /><br /> `VT_SS_WSTRING`|Supports the `nchar` and **nvarchar**[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types.<br /><br /> Includes the following members:<br /><br /> *sActualLength* (`SHORT`) Specifies the actual length for the string to which *pwchNCharVal* points. Does not include terminating zero.<br /><br /> *sMaxLength* (`SHORT`) Specifies the maximum length for the string to which *pwchNCharVal* points.<br /><br /> *pwchNCharVal* (`WCHAR` \*) Pointer to the string.<br /><br /> Unused members: *rgbReserved*, *dwReserved*, and *pwchReserved*.|  
|CharVal|No corresponding OLE DB type indicator.|`struct _CharVal`|`VT_SS_STRING,`<br /><br /> `VT_SS_VARSTRING`|Supports the `char` and **varchar**[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types.<br /><br /> Includes the following members:<br /><br /> *sActualLength* (`SHORT`) Specifies the actual length for the string to which *pchCharVal* points. Does not include terminating zero.<br /><br /> *sMaxLength* (`SHORT`) Specifies the maximum length for the string to which *pchCharVal* points.<br /><br /> *pchCharVal* (`CHAR` \*) Pointer to the string.<br /><br /> Unused members:<br /><br /> *rgbReserved*, *dwReserved*, and *pwchReserved*.|  
|BinaryVal|No corresponding OLE DB type indicator.|`struct _BinaryVal`|`VT_SS_VARBINARY,`<br /><br /> `VT_SS_BINARY`|Supports the `binary` and **varbinary**[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types.<br /><br /> Includes the following members:<br /><br /> *sActualLength* (`SHORT`) Specifies the actual length for the data to which *prgbBinaryVal* points.<br /><br /> *sMaxLength* (`SHORT`) Specifies the maximum length for the data to which *prgbBinaryVal* points.<br /><br /> *prgbBinaryVal* (`BYTE` \*) Pointer to the binary data.<br /><br /> Unused member: *dwReserved*.|  
|UnknownType|UNUSED|UNUSED|UNUSED|UNUSED|  
|BLOBType|UNUSED|UNUSED|UNUSED|UNUSED|  
  
## See Also  
 [Data Types &#40;OLE DB&#41;](data-types-ole-db.md)  
  
  
