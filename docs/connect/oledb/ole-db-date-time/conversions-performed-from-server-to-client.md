---
title: "Conversions Performed from Server to Client"
description: Learn about date/time conversions performed between SQL Server and a client application written with OLE DB Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "conversions [OLE DB], server to client"
---
# Conversions Performed from Server to Client
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  This article describes date/time conversions performed between [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] (or later) and a client application written with OLE DB Driver for SQL Server.  
  
## Conversions  
 The following table describes conversions between the type returned to the client and the type in the binding. For output parameters, if ICommandWithParameters::SetParameterInfo has been called and the type specified in *pwszDataSourceType* does not match the actual type on the server, an implicit conversion will be performed by the server, and the type returned to the client will match the type specified through ICommandWithParameters::SetParameterInfo. This can lead to unexpected conversion results when the server's conversion rules are different from those described in this article. For example, when a default date must be provided, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses 1900-1-1 rather than 1899-12-30.  
  
|To -><br /><br /> From|DATE|DBDATE|DBTIME|DBTIME2|DBTIMESTAMP|DBTIMESTAMPOFFSET|FILETIME|BYTES|VARIANT|SSVARIANT|BSTR|STR|WSTR|  
|----------------------|----------|------------|------------|-------------|-----------------|-----------------------|--------------|-----------|-------------|---------------|----------|---------|----------|  
|Date|1, 7|OK|-|-|1|1, 3|1, 7|-|OK (VT_BSTR)|OK|OK|4|4|  
|Time|5, 6, 7|-|9|OK|6|3, 6|5, 6|-|OK (VT_BSTR)|OK|OK|4|4|  
|Smalldatetime|7|8|9, 10|10|OK|3|7|-|7 (VT_DATE)|OK|OK|4|4|  
|Datetime|5, 7|8|9, 10|10|OK|3|7|-|7 (VT_DATE)|OK|OK|4|4|  
|Datetime2|5, 7|8|9, 10|10|7|3|5, 7|-|OK (VT_BSTR)|OK|OK|4|4|  
|Datetimeoffset|5, 7, 11|8, 11|9, 10, 11|10, 11|7, 11|OK|5, 7, 11|-|OK (VT_BSTR)|OK|OK|4|4|  
|Char, Varchar,<br /><br /> Nchar, Nvarchar|7, 13|12|12, 9|12|12|12|7, 13|N/A|N/A|N/A|N/A|N/A|N/A|  
|Sql_variant<br /><br /> (datetime)|7|8|9, 10|10|OK|3|7|-|7(VT_DATE)|OK|OK|4|4|  
|Sql_variant<br /><br /> (smalldatetime)|7|8|9, 10|10|OK|3|7|-|7(VT_DATE)|OK|OK|4|4|  
|Sql_variant<br /><br /> (date)|1, 7|OK|2|2|1|1, 3|1, 7|-|OK(VT_BSTR)|OK|OK|4|4|  
|Sql_variant<br /><br /> (time)|5, 6, 7|2|6|OK|6|3, 6|5, 6|-|OK(VT_BSTR)|OK|OK|4|4|  
|Sql_variant<br /><br /> (datetime2)|5, 7|8|9, 10|10|OK|3|5, 7|-|OK(VT_BSTR)|OK|OK|4|4|  
|Sql_variant<br /><br /> (datetimeoffset)|5, 7, 11|8, 11|9, 10, 11|10, 11|7, 11|OK|5, 7, 11|-|OK(VT_BSTR)|OK|OK|4|4|  
  
## Key to Symbols  
  
|Symbol|Meaning|  
|------------|-------------|  
|OK|No conversion is necessary.|  
|-|No conversion is supported. If the binding is validated when IAccessor::CreateAccessor is called, DBBINDSTATUS_UPSUPPORTEDCONVERSION is returned in *rgStatus*. When accessor validation is deferred, DBSTATUS_E_BADACCESSOR is set.|  
|1|The time fields are set to zero.|  
|2|DBSTATUS_E_CANTCONVERTVALUE is set.|  
|3|The timezone is set to zero.|  
|4|If the client buffer is not big enough, DBSTATUS_S_TRUNCATED is set. When the server type includes fractional seconds, the number of digits in the result string exactly matches the scale of the server type.|  
|5|Truncation of seconds or fractional seconds is ignored.|  
|6|The date is set to the current date, unless the source is a string time literal and the destination is DBTYPE_DATE. In this case, 1899-12-30 is used.|  
|7|If the value overflows, DBSTATUS_E_DATAOVERFLOW is set.|  
|8|Time fields are ignored.|  
|9|Fractional seconds fields are ignored.|  
|10|The date component is ignored.|  
|11|The time is converted to the client timezone. If an error occurs during this conversion DBSTATUS_E_DATAOVERFLOW is set.|  
|12|The string is parsed as an ISO literal and converted to the target type. If this fails, the string is parsed as an OLE date literal (which also has time components) and converted from an OLE date (DBTYPE_DATE) to the target type. The string must conform to the syntax for literals of the target type allowed for ISO format parsing to succeed. For OLE parsing to succeed, the string must conform to the syntax recognized by OLE. If the string cannot be parsed, DBSTATUS_E_CANTCONVERTVALUE is set. If any component values are out of range, DBSTATUS_E_DATAOVERFLOW is set.|  
|13|The string is parsed as an ISO literal and converted to the target type. If this fails, the string is parsed as an OLE date literal (which also has time components) and converted from an OLE date (DBTYPE_DATE) to the target type. The string must conform to the syntax for datetime literals, unless the destination is DBTYPE_DATE or DBTYPE_DBTIMESTAMP. If this is the case, either a datetime or time literal is allowed for ISO format parsing to succeed. For OLE parsing to succeed, the string must conform to the syntax recognized by OLE. If the string cannot be parsed, DBSTATUS_E_CANTCONVERTVALUE is set. If any component values are out of range, DBSTATUS_E_DATAOVERFLOW is set.|  
  
## See Also  
 [Bindings and Conversions &#40;OLE DB&#41;](../../oledb/ole-db-date-time/conversions-ole-db.md)  
  
  
