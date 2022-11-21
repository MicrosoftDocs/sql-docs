---
description: "Bulk Copy Changes for Enhanced Date and Time Types (OLE DB and ODBC)"
title: "Bulk Copy, enhanced types, OLE DB"
ms.custom: ""
ms.date: 12/18/2019
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "ODBC, bulk copy operations"
ms.assetid: c29e0f5e-9b3c-42b3-9856-755f4510832f
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Bulk Copy Changes for Enhanced Date and Time Types (OLE DB and ODBC)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  This topic describes the date/time enhancements to support bulk copy functionality. The information in this topic is valid for both OLE DB and ODBC in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client.  
  
> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../includes/snac-removed-oledb-and-odbc.md)]

## Format Files  
 When building format files interactively, the following table describes the input used to specify date/time types and the corresponding host-file data type names.  
  
|File storage type|Host file data type|Response to the prompt: "Enter the file storage type of field <field_name> [\<default>]:"|  
|-----------------------|-------------------------|-----------------------------------------------------------------------------------------------------|  
|Datetime|SQLDATETIME|d|  
|Smalldatetime|SQLDATETIM4|D|  
|Date|SQLDATE|de|  
|Time|SQLTIME|te|  
|Datetime2|SQLDATETIME2|d2|  
|Datetimeoffset|SQLDATETIMEOFFSET|do|  

 The XML format file XSD will have the following additions:  
  
```xml
<xs:complexType name="SQLDATETIME2">  
    <xs:complexContent>  
        <xs:extension base="bl:Fixed"/>  
    </xs:complexContent>  
</xs:complexType>  
<xs:complexType name="SQLDATETIMEOFFSET">  
    <xs:complexContent>  
        <xs:extension base="bl:Fixed"/>  
    </xs:complexContent>  
</xs:complexType>  
<xs:complexType name="SQLDATE">  
    <xs:complexContent>  
        <xs:extension base="bl:Fixed"/>  
    </xs:complexContent>  
</xs:complexType>  
<xs:complexType name="SQLTIME">  
    <xs:complexContent>  
        <xs:extension base="bl:Fixed"/>  
    </xs:complexContent>  
</xs:complexType>  
```  
  
## Character Data Files  
 In character data files, date and time values are represented as described in the "Data Formats: Strings and Literals" section of [Data Type Support for ODBC Date and Time Improvements](../../relational-databases/native-client-odbc-date-time/data-type-support-for-odbc-date-and-time-improvements.md) for ODBC, or of [Data Type Support for OLE DB Date and Time Improvements](../../relational-databases/native-client-ole-db-date-time/data-type-support-for-ole-db-date-and-time-improvements.md) for OLE DB.  
  
 In native data fles, date and time values for the four new types are represented as their TDS representations with a scale of 7 (because this is the maximum supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and bcp data files do not store the scale of these columns). There is no change to the storage of the existing **datetime** and **smalldatetime** type or their tabular data stream (TDS) representations.  
  
 The storage sizes for the different storage types are as follows for OLE DB:  
  
|File storage type|Storage size in bytes|  
|-----------------------|---------------------------|  
|datetime|8|  
|smalldatetime|4|  
|date|3|  
|time|6|  
|datetime2|9|  
|datetimeoffset|11|  

 The sizes are as follows for ODBC. Note that it is not necessary to store precision in either format or data files, because BCP.exe will always retrieve precision from the server.  
  
|File storage type|Storage size in bytes|Storage format|  
|-----------------------|---------------------------|--------------------|  
|datetime (d)|8|TDS|  
|smalldatetime (D)|4|TDS|  
|date (de)|3|TDS|  
|time (te)|6|TDS|  
|datetime2 (d2)|9|TDS|  
|datetimeoffset (do)|11|TDS|  

## BCP Types in sqlncli.h  
 The following types are defined in sqlncli.h to be used with the BCP API extensions to ODBC. These types are passed with the *eUserDataType* parameter of IBCPSession::BCPColFmt in OLE DB.  
  
|File storage type|Host file data type|Type in sqlncli.h for use with IBCPSession::BCPColFmt|Value|  
|-----------------------|-------------------------|-----------------------------------------------------------|-----------|  
|Datetime|SQLDATETIME|BCP_TYPE_SQLDATETIME|0x3d|  
|Smalldatetime|SQLDATETIM4|BCP_TYPE_SQLDATETIME4|0x3a|  
|Date|SQLDATE|BCP_TYPE_SQLDATE|0x28|  
|Time|SQLTIME|BCP_TYPE_SQLTIME|0x29|  
|Datetime2|SQLDATETIME2|BCP_TYPE_SQLDATETIME2|0x2a|  
|Datetimeoffset|SQLDATETIMEOFFSET|BCP_TYPE_SQLDATETIMEOFFSET|0x2b|  

## BCP Data Type Conversions  
 The following tables provide conversion information.  
  
 **OLE DB Note** The following conversions are performed by IBCPSession. IRowsetFastLoad uses OLE DB conversions as defined in [Conversions Performed from Client to Server](../../relational-databases/native-client-ole-db-date-time/conversions-performed-from-client-to-server.md). Note that datetime values are rounded to 1/300th of a second and smalldatetime values have seconds set to zero after client conversions described below have been performed. Datetime rounding propagates through hours and minutes, but not the date.  
  
|To --><br /><br /> From|date|time|smalldatetime|datetime|datetime2|datetimeoffset|char|wchar|  
|------------------------|----------|----------|-------------------|--------------|---------------|--------------------|----------|-----------|  
|Date|1|-|1,6|1,6|1,6|1,5,6|1,3|1,3|  
|Time|N/A|1,10|1,7,10|1,7,10|1,7,10|1,5,7,10|1,3|1,3|  
|Smalldatetime|1,2|1,4,10|1|1|1,10|1,5,10|1,11|1,11|  
|Datetime|1,2|1,4,10|1,12|1|1,10|1,5,10|1,11|1,11|  
|Datetime2|1,2|1,4,10|1,10 (ODBC)1,12 (OLE DB)|1,10|1,10|1,5,10|1,3|1,3|  
|Datetimeoffset|1,2,8|1,4,8,10|1,8,10|1,8,10|1,8,10|1,10|1,3|1,3|  
|Char/wchar (date)|9|-|9,6 (ODBC)9,6,12 (OLE DB)|9,6 (ODBC)9,6,12 (OLE DB)|9,6|9,5,6|N/A|N/A|  
|Char/wchar (time)|-|9,10|9,7,10 (ODBC)9,7,10,12 (OLE DB)|9,7,10 (ODBC)9,7,10, 12 (OLE DB)|9,7,10|9,5,7,10|N/A|N/A|  
|Char/wchar (datetime)|9,2|9,4,10|9,10 (ODBC)9,10,12 (OLE DB)|9,10 (ODBC)9,10,12 (OLE DB)|9,10|9,5,10|N/A|N/A|  
|Char/wchar (datetimeoffset)|9,2,8|9,4,8,10|9,8,10 (ODBC)9,8,10,12 (OLE DB)|9,8,10 (ODBC)9,8,10,12 (OLE DB)|9,8,10|9,10|N/A|N/A|  

#### Key to Symbols  
  
|Symbol|Meaning|  
|------------|-------------|  
|-|No conversion is supported.<br /><br /> An ODBC diagnostic record is generated with SQLSTATE 07006 and the message "Restricted data type attribute violation".|  
|1|If the data supplied is not valid, an ODBC  diagnostic record is generated with SQLSTATE 22007 and the message "Invalid datetime format". For datetimeoffset values, the time portion must be within range after conversion to UTC, even if no conversion to UTC is requested. This is because TDS and the server always normalize the time in datetimeoffset values for UTC. So the client must check that time components are within the range supported after conversion to UTC.|  
|2|The time component is ignored.|  
|3|For ODBC, If truncation with data loss occurs, a diagnostic record is generated with SQLSTATE 22001 and message 'String data, right truncated' The number of fractional seconds digits (the scale) is determined from the destination column's size according to the following table. For column sizes larger than the range in the table, a scale of 7 is implied. This conversion should allow for up to nine fractional second digits, the maximum allowed by ODBC.<br /><br /> **Type:** DBTIME2<br /><br /> **Implied scale 0** 8<br /><br /> **Implied scale 1..7** 10,16<br /><br /> <br /><br /> **Type:** DBTIMESTAMP<br /><br /> **Implied scale 0:** 19<br /><br /> **Implied scale 1..7:** 21..27<br /><br /> <br /><br /> **Type:** DBTIMESTAMPOFFSET<br /><br /> **Implied scale 0:** 26<br /><br /> **Implied scale 1..7:** 28..34<br /><br /> For OLE DB, if truncation with data loss occurs, an error is posted. For datetime2, the number of fractional seconds digits (the scale) is determined from the destination column's size according to the following table. For column sizes larger than the range in the table a scale of 9 is implied. This conversion should allow for up to nine fractional second digits, the maximum allowed by OLE DB.<br /><br /> **Type:** DBTIME2<br /><br /> **Implied scale 0** 8<br /><br /> **Implied scale 1..9** 1..9<br /><br /> <br /><br /> **Type:** DBTIMESTAMP<br /><br /> **Implied scale 0:** 19<br /><br /> **Implied scale 1..9:** 21..29<br /><br /> <br /><br /> **Type:** DBTIMESTAMPOFFSET<br /><br /> **Implied scale 0:** 26<br /><br /> **Implied scale 1..9:** 28..36|  
|4|The date component is ignored.|  
|5|The timezone is set to UTC (for example, 00:00).|  
|6|The time is set to zero.|  
|7|The date is set to 1900-01-01.|  
|8|The timezone offset is ignored.|  
|9|The string is parsed and converted to a date, datetime, datetimeoffset, or time value, depending on the first punctuation character encountered and presence of remaining components. The string is then converted to the target type, following the rules in the table at the end of this topic for the source type discovered by this process. If the data supplied cannot be parsed without error, or if any component part is outside the range allowed, or if there is no conversion from the literal type to target type, an error is posted (OLE DB) or an ODBC diagnostic record is generated with SQLSTATE 22018 and the message "Invalid character value for cast specification". For datetime and smalldatetime parameters, if the year is outside the range these types support, an error is posted (OLE DB) or an ODBC diagnostic record is generated with SQLSATE 22007 and the message "Invalid datetime format".<br /><br /> For datetimeoffset, the value must be within range after conversion to UTC, even if no conversion to UTC is requested. This is because TDS and the server always normalize the time in datetimeoffset values for UTC, so the client must verify that time components are within the range supported after conversion to UTC. If the value is not within the supported UTC range, an error is posted (OLE DB), or an ODBC diagnostic record is generated with SQLSTATE 22007 and the message "Invalid datetime format".|  
|10|If truncation with data loss occurs on a client to server conversion, an error is posted (OLE DB) or an ODBC diagnostic record is generated with SQLSTATE 22008 and the message "Datetime field overflow". This error also occurs if the value falls outside the range that can be represented by the UTC range used by the server. If seconds or fractional seconds truncation occurs in a server to client conversion, there is only a warning.|  
|11|If truncation with data loss occurs, a diagnostic record is generated.<br /><br /> On a server-to-client conversion, this is a warning (ODBC SQLSTATE S1000).<br /><br /> On a client-to-server conversion, this is an error (ODBC SQLSTATE 22001).|  
|12|Seconds are set to zero and fractional seconds are discarded. No truncation error is possible.|  
|N/A|Existing [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and earlier behavior is maintained.|  

## See Also  
 [Date and Time Improvements &#40;ODBC&#41;](../../relational-databases/native-client-odbc-date-time/date-and-time-improvements-odbc.md)   
 [Date and Time Improvements &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-date-time/date-and-time-improvements-ole-db.md)  
