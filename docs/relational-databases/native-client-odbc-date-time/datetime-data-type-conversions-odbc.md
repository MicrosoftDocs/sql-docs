---
title: "datetime Data Type Conversions (ODBC) | Microsoft Docs"
description: Learn about data type conversions in ODBC, which are already defined by ODBC or are consistent extensions of ODBC.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "conversions [ODBC]"
  - "bindings [ODBC]"
  - "ODBC, bindings and conversions"
ms.assetid: 66b9d282-c88d-40e5-93c2-fd5499a74458
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# datetime Data Type Conversions (ODBC)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The following conversions are either already defined by ODBC or are a consistent extension of ODBC. The conversions supplied by each provider are determined by the community served by the provider, and there are often inconsistencies between providers as a result. Values in square brackets are optional.  
  
-   The format of datetime strings is 'yyyy-mm-dd[ hh:mm:ss[.9999999][ plus/minus hh:mm]]'  
  
-   The format of time strings is 'hh:mm:ss[.9999999]'  
  
-   The format of date strings is 'yyyy-mm-dd'  
  
 Conversions from strings allow flexibility in white space and field width. For more information, see the "Data Formats: Strings and Literals" section of [Data Type Support for ODBC Date and Time Improvements](../../relational-databases/native-client-odbc-date-time/data-type-support-for-odbc-date-and-time-improvements.md).  
  
 The following are general conversion rules:  
  
-   If no time is present but the receiver can store time, the time is set to zero.  
  
-   If no date is present but the receiver can store date, the current date is used.  
  
-   If no timezone is present in the data type that the client is using but the server can store timezone, the date is stored in the client timezone. Note that this differs from the server behavior.  
  
-   If no timezone is present in the server type but the client type has a timezone, time is converted to UTC before being stored on the server.  
  
-   If time is present but the receiver cannot store time, the time component is ignored.  
  
-   If a date is present but the receiver cannot store the date, the date component is ignored.  
  
-   If truncation of seconds or fractional seconds occurs when converting from C to SQL, a diagnostic record is generated with SQLSTATE 22008 and the message "Datetime field overflow".  
  
-   If truncation of seconds or fractional seconds occurs when converting from SQL to C, a diagnostic record is generated with SQLSTATE 01S07 and the message "Fractional truncation".  
  
## In This Section  
 [Conversions from C to SQL](../../relational-databases/native-client-odbc-date-time/datetime-data-type-conversions-from-c-to-sql.md)  
 Lists issues to consider when you convert from C types to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data/time types.  
  
 [Conversions from SQL to C](../../relational-databases/native-client-odbc-date-time/datetime-data-type-conversions-from-sql-to-c.md)  
 Lists issues to consider when you convert from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data/time types to C types.  
  
## See Also  
 [Date and Time Improvements &#40;ODBC&#41;](../../relational-databases/native-client-odbc-date-time/date-and-time-improvements-odbc.md)  
  
  
