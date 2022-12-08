---
title: "DataTypeEnum"
description: "DataTypeEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "DataTypeEnum"
helpviewer_keywords:
  - "DataTypeEnum enumeration [ADO]"
apitype: "COM"
---
# DataTypeEnum
Specifies the data type of a [Field](../../../ado/reference/ado-api/field-object.md), [Parameter](../../../ado/reference/ado-api/parameter-object.md), or [Property](../../../ado/reference/ado-api/property-object-ado.md). The corresponding OLE DB type indicator is shown in parentheses in the description column of the following table.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**AdArray**|0x2000|A flag value, always combined with another data type constant, that indicates an array of the other data type. Does not apply to ADOX.|  
|**adBigInt**|20|Indicates an eight-byte signed integer (DBTYPE_I8).|  
|**adBinary**|128|Indicates a binary value (DBTYPE_BYTES).|  
|**adBoolean**|11|Indicates a **Boolean** value (DBTYPE_BOOL).|  
|**adBSTR**|8|Indicates a null-terminated character string (Unicode) (DBTYPE_BSTR).|  
|**adChapter**|136|Indicates a four-byte chapter value that identifies rows in a child rowset (DBTYPE_HCHAPTER).|  
|**adChar**|129|Indicates a string value (DBTYPE_STR).|  
|**adCurrency**|6|Indicates a currency value (DBTYPE_CY). Currency is a fixed-point number with four digits to the right of the decimal point. It is stored in an eight-byte signed integer scaled by 10,000.|  
|**adDate**|7|Indicates a date value (DBTYPE_DATE). A date is stored as a double, the whole part of which is the number of days since December 30, 1899, and the fractional part of which is the fraction of a day.|  
|**adDBDate**|133|Indicates a date value (yyyymmdd) (DBTYPE_DBDATE).|  
|**adDBTime**|134|Indicates a time value (hhmmss) (DBTYPE_DBTIME).|  
|**adDBTimeStamp**|135|Indicates a date/time stamp (yyyymmddhhmmss plus a fraction in billionths) (DBTYPE_DBTIMESTAMP).|  
|**adDecimal**|14|Indicates an exact numeric value with a fixed precision and scale (DBTYPE_DECIMAL).|  
|**adDouble**|5|Indicates a double-precision floating-point value (DBTYPE_R8).|  
|**adEmpty**|0|Specifies no value (DBTYPE_EMPTY).|  
|**adError**|10|Indicates a 32-bit error code (DBTYPE_ERROR).|  
|**adFileTime**|64|Indicates a 64-bit value representing the number of 100-nanosecond intervals since January 1, 1601 (DBTYPE_FILETIME).|  
|**adGUID**|72|Indicates a globally unique identifier (GUID) (DBTYPE_GUID).|  
|**adIDispatch**|9|Indicates a pointer to an **IDispatch** interface on a COM object (DBTYPE_IDISPATCH).<br /><br /> **Note** This data type is currently not supported by ADO. Usage may cause unpredictable results.|  
|**adInteger**|3|Indicates a four-byte signed integer (DBTYPE_I4).|  
|**adIUnknown**|13|Indicates a pointer to an **IUnknown** interface on a COM object (DBTYPE_IUNKNOWN).<br /><br /> **Note** This data type is currently not supported by ADO. Usage may cause unpredictable results.|  
|**adLongVarBinary**|205|Indicates a long binary value.|  
|**adLongVarChar**|201|Indicates a long string value.|  
|**adLongVarWChar**|203|Indicates a long null-terminated Unicode string value.|  
|**adNumeric**|131|Indicates an exact numeric value with a fixed precision and scale (DBTYPE_NUMERIC).|  
|**adPropVariant**|138|Indicates an Automation PROPVARIANT (DBTYPE_PROP_VARIANT).|  
|**adSingle**|4|Indicates a single-precision floating-point value (DBTYPE_R4).|  
|**adSmallInt**|2|Indicates a two-byte signed integer (DBTYPE_I2).|  
|**adTinyInt**|16|Indicates a one-byte signed integer (DBTYPE_I1).|  
|**adUnsignedBigInt**|21|Indicates an eight-byte unsigned integer (DBTYPE_UI8).|  
|**adUnsignedInt**|19|Indicates a four-byte unsigned integer (DBTYPE_UI4).|  
|**adUnsignedSmallInt**|18|Indicates a two-byte unsigned integer (DBTYPE_UI2).|  
|**adUnsignedTinyInt**|17|Indicates a one-byte unsigned integer (DBTYPE_UI1).|  
|**adUserDefined**|132|Indicates a user-defined variable (DBTYPE_UDT).|  
|**adVarBinary**|204|Indicates a binary value.|  
|**adVarChar**|200|Indicates a string value.|  
|**adVariant**|12|Indicates an Automation **Variant** (DBTYPE_VARIANT).<br /><br /> **Note** This data type is currently not supported by ADO. Usage may cause unpredictable results.|  
|**adVarNumeric**|139|Indicates a numeric value.|  
|**adVarWChar**|202|Indicates a null-terminated Unicode character string.|  
|**adWChar**|130|Indicates a null-terminated Unicode character string (DBTYPE_WSTR).|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.DataType.ARRAY|  
|AdoEnums.DataType.BIGINT|  
|AdoEnums.DataType.BINARY|  
|AdoEnums.DataType.BOOLEAN|  
|AdoEnums.DataType.BSTR|  
|AdoEnums.DataType.CHAPTER|  
|AdoEnums.DataType.CHAR|  
|AdoEnums.DataType.CURRENCY|  
|AdoEnums.DataType.DATE|  
|AdoEnums.DataType.DBDATE|  
|AdoEnums.DataType.DBTIME|  
|AdoEnums.DataType.DBTIMESTAMP|  
|AdoEnums.DataType.DECIMAL|  
|AdoEnums.DataType.DOUBLE|  
|AdoEnums.DataType.EMPTY|  
|AdoEnums.DataType.ERROR|  
|AdoEnums.DataType.FILETIME|  
|AdoEnums.DataType.GUID|  
|AdoEnums.DataType.IDISPATCH|  
|AdoEnums.DataType.INTEGER|  
|AdoEnums.DataType.IUNKNOWN|  
|AdoEnums.DataType.LONGVARBINARY|  
|AdoEnums.DataType.LONGVARCHAR|  
|AdoEnums.DataType.LONGVARWCHAR|  
|AdoEnums.DataType.NUMERIC|  
|AdoEnums.DataType.PROPVARIANT|  
|AdoEnums.DataType.SINGLE|  
|AdoEnums.DataType.SMALLINT|  
|AdoEnums.DataType.TINYINT|  
|AdoEnums.DataType.UNSIGNEDBIGINT|  
|AdoEnums.DataType.UNSIGNEDINT|  
|AdoEnums.DataType.UNSIGNEDSMALLINT|  
|AdoEnums.DataType.UNSIGNEDTINYINT|  
|AdoEnums.DataType.USERDEFINED|  
|AdoEnums.DataType.VARBINARY|  
|AdoEnums.DataType.VARCHAR|  
|AdoEnums.DataType.VARIANT|  
|AdoEnums.DataType.VARNUMERIC|  
|AdoEnums.DataType.VARWCHAR|  
|AdoEnums.DataType.WCHAR|  
  
## Applies To  

:::row:::
    :::column:::
        [Append Method (ADO)](../../../ado/reference/ado-api/append-method-ado.md)  
        [CreateParameter Method (ADO)](../../../ado/reference/ado-api/createparameter-method-ado.md)  
    :::column-end:::
    :::column:::
        [CreateRecordset Method (RDS)](../../../ado/reference/rds-api/createrecordset-method-rds.md)  
        [Type Property (ADO)](../../../ado/reference/ado-api/type-property-ado.md)  
    :::column-end:::
:::row-end:::
