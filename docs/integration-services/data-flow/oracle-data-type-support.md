---
description: "Microsoft Connector for Oracle data type support"
title: "Microsoft Connector for Oracle data type support | Microsoft Docs"
ms.custom: ""
ms.date: "08/14/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
author: chugugrace
ms.author: chugu
---
# Microsoft Connector for Oracle data type support

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

SSIS components for Oracle do not support all Oracle data types. Columns with unsupported data types will have a warning when designing packages in SSDT and will be deleted from mapping columns. Data cannot be loaded to a column with an unsupported data type.

## Data type mapping

The following table shows the Oracle database data types and their default mapping to SSIS data types. It also shows the unsupported Oracle data types.

|Oracle Database Data Type|SSIS Data Type|Comments|
|:-|:-|:-|
|VARCHAR2|DT_STR||
|NVARCHAR2|DT_WSTR||
|CHAR|DT_STR||
|NUMBER|DT_R8|This can be changed to DT_NUMERIC with specific precision and scale. Precision and scale are defined by user per the need. The output will be the column data as a number with fixed precision and scale.|
|NUMBER(P, S)| When the scale is 0, according to the precision (P) <li> DT_I1 <Li> DT_I2 <Li> DT_I4 <Li> DT_NUMBERIC(P,0)||
||DT_NUMERIC(P,S)||
|DATE|DT_DBTIMESTAMP||
|<li>TIMESTAMP <li>TIMESTAMP WITH TIME ZONE <li>INTERVAL YEAR TO MONTH <li>INTERVAL DAY TO SECOND <li>TIMESTAMP WITH LOCAL TIME ZONE|DT_STR||
|RAW|DT_BYTES||
|CLOB|DT_TEXT|CLOB, NCLOB, and BLOB data types are supported only in array mode, not in Fast Load mode.|
|NCLOB|DT_NTEXT||
|BLOB|DT_IMAGE||
|UROWID|Not Supported||
|REF|Not Supported||
|BFILE|Not Supported||
|LONG|Not Supported||
|LONG RAW|Not Supported||
|ROWID|Not Supported||
|User-defined type (object type, VARRAY, Nested Table)|Not Supported||

## Next steps

- Configure [Oracle Connection Manager](oracle-connection-manager.md).
- Configure [Oracle Source](oracle-source.md).
- Configure [Oracle Destination](oracle-destination.md).
- If you have questions, visit [TechCommunity](https://aka.ms/AA5u35j).
