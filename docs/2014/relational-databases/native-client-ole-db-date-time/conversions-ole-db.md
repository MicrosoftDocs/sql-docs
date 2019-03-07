---
title: "Bindings and Conversions (OLE DB) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
helpviewer_keywords: 
  - "conversions [OLE DB]"
  - "bindings [OLE DB]"
  - "OLE DB, bindings and conversions"
ms.assetid: c187df58-a8c8-4c74-a76f-663abbc5f0c1
author: MightyPen
ms.author: genemi
manager: craigg
---
# Bindings and Conversions (OLE DB)
  This section discusses how to convert between `datetime` and `datetimeoffset` values. The conversions described in this section are either already provided by OLE DB or are a consistent extension of OLE DB.  
  
 The format of literals and strings for dates and times in OLE DB generally follows ISO, and is not dependent on the client locale. One exception is DBTYPE_DATE, where the standard is OLE Automation. However, because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client only converts between types when data is transmitted to or from the client, there is no way for an application to force [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client to convert between DBTYPE_DATE and string formats. Otherwise, strings use the following formats (text in brackets indicates an optional element):  
  
-   The format of `datetime` and `datetimeoffset` strings is:  
  
     *yyyy*-*mm*-*dd*[ *hh*:*mm*:*ss*[.*9999999*][ ?? *hh*:*mm*]]  
  
-   The format of `time` strings is:  
  
     *hh*:*mm*:*ss*[.*9999999*]  
  
-   The format of `date` strings is:  
  
     *yyyy*-*mm*-*dd*  
  
> [!NOTE]  
>  Earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client and SQLOLEDB implemented OLE conversions, in case standard conversions failed. As a result, some conversions performed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client 10.0 and later differ from the OLE DB specification.  
  
 Conversions from strings allow flexibility in white space and field width. For more information, see the "Data Formats: Strings and Literals" section in [Data Type Support for OLE DB Date and Time Improvements](data-type-support-for-ole-db-date-and-time-improvements.md).  
  
 The following are general conversion rules:  
  
-   When a string is converted to a date/time type, the string is first parsed as an ISO literal. If this fails, the string is parsed as an OLE date literal, which has time components.  
  
-   If no time is present but the receiver can store time, the time is set to zero. If no date is present but the receiver can store a date, the date is set to the current date when ISO conversions are used and to 1899-12-30 when OLE conversions are used.  
  
-   If no timezone is present in the data type that the client is using, but the server can store timezone, the data on the client is assumed to be in the client timezone.  
  
-   If no timezone is present at the server but the client has timezone information, the UTC timezone is assumed. This differs from server behavior.  
  
-   If the time is present but the receiver cannot store time, the time component is ignored.  
  
-   If the date is present but the receiver cannot store the date, the date component is ignored.  
  
-   If truncation of seconds or fractional seconds occurs when converting from client to server, DB_E_ERRORSOCCURRED is returned and the status DBSTATUS_E_DATAOVERFLOW is set.  
  
-   If truncation of seconds or fractional seconds occurs when converting from server to client, DBSTATUS_S_TRUNCATED is set  
  
## In This Section  
 [Conversions Performed from Client to Server](conversions-performed-from-client-to-server.md)  
 Describes date/time conversions performed between a client application written with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB and [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] (or later).  
  
 [Conversions Performed from Server to Client](conversions-performed-from-server-to-client.md)  
 Describes date/time conversions performed between [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] (or later) and a client application written with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB.  
  
## See Also  
 [Date and Time Improvements &#40;OLE DB&#41;](date-and-time-improvements-ole-db.md)  
  
  
