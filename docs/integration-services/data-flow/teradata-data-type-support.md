---
description: "Teradata data type support"
title: "Teradata data type support | Microsoft Docs"
ms.custom: ""
ms.date: "11/22/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
author: chugugrace
ms.author: chugu
---
# Data type support

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

SSIS components use Teradata Parallel Transporter API (TPT API) to load and transfer data from and to Teradata Database, thus only TPT API supported data type can be used in SSIS.

> [!NOTE]
>
> TIME, TIMESTAMP, and INTERVAL data types in Teradata are handled by TPT API as fixed-sized character strings. They are handled by the SSIS components for Teradata as a string.

## Data type mapping

The following table shows the Teradata database data types and their default mapping to SSIS data types. It also shows the unsupported Teradata data types.

|SQL Data Type|SSIS Data Type|
|:-|:-|
|DECIMAL/NUMERIC|DT_NUMERIC|
|BYTEINT|DT_I1|
|SMALLINT|DT_I2|
|INTEGER|DT_I4|
|FLOAT/REAL/DOUBLE PRECISION|DT_R8|
|DATE|DT_DBDATE|
|TIME<br>TIME(n)|DT_STR|
|TIMESTAMP<br>TIMESTAMP (n)|DT_STR|
|TIME WITH TIMEZONE<br>TIME(n) WITH TIMEZONE|DT_STR|
|TIMESTAMP WITH TIMEZONE<br>TIMESTAMP(n) WITH TIMEZONE|DT_STR|
|INTERVAL YEAR<br>INTERVAL YEAR (n)|DT_STR|
|INTERVAL YEAR TO MONTH<br>INTERVAL YEAR (n) TO MONTH|DT_STR|
|INTERVAL MONTH<br>INTERVAL MONTH (n)|DT_STR|
|INTERVAL DAY<br>INTERVAL DAY (n)|DT_STR|
|INTERVAL DAY TO HOUR<br>INTERVAL DAY (n) TO HOUR|DT_STR|
|INTERVAL DAY TO MINUTE<br>INTERVAL DAY (n) TO MINUTE|DT_STR|
|INTERVAL DAY TO SECOND<br>INTERVAL DAY (n) TO SECOND<br>INTERVAL DAY TO SECOND (m)<br>INTERVAL DAY (n) TO SECOND (m)|DT_STR|
|INTERVAL HOUR<br>INTERVAL HOUR (n)|DT_STR|
|INTERVAL HOUR TO MINUTE<br>INTERVAL HOUR (n) TO MINUTE|DT_STR
|INTERVAL HOUR TO SECOND<br>INTERVAL HOUR (n) TO SECOND<br>INTERVAL HOUR TO SECOND (m)<br>INTERVAL HOUR (n) TO SECOND (m)|DT_STR|
|INTERVAL MINUTE<br>INTERVAL MINUTE (n)|DT_STR|
|INTERVAL MINUTE TO SECOND<br>INTERVAL MINUTE (n) TO SECOND<br>INTERVAL MINUTE TO SECOND (m)<br>INTERVAL MINUTE (n) TO SECOND (m)|DT_STR|
|INTERVAL SECOND<br>INTERVAL SECOND (n)<br>INTERVAL SECOND (n,m)|DT_STR|
|PERIOD(DATE)|DT_STR|
|PERIOD(TIME)|DT_STR|
|NUMBER|DT_STR|
|CHARACTER|DT_STR|
|VARCHAR|DT_STR (DT_WSTR for Unicode character set)<br>**Notes**:<br> Maximum length of VARCHAR supported is 32000. <br> Maximum allowed length of DT_STR is 8000 characters, DT_WSTR is 4000 characters. Data is truncated if exceeds.|
|LONG VARCHAR|Not supported|
|CLOB|Not supported|
|BYTE|DT_BYTES<br>**Note**: Maximum allowed length is 8000 bytes. Data is truncated if exceeds.|
|VARBYTE|DT_BYTES<br>**Note**: Maximum allowed length is 8000 bytes. Data is truncated if exceeds.|
|BLOB|Not supported|

## Next steps

- Configure [Teradata connection manager](teradata-connection-manager.md)
- Configure [Teradata source](teradata-source.md)
- Configure [Teradata destination](teradata-destination.md)
- If you have questions, visit [Tech Community](https://aka.ms/AA6iwdw).
