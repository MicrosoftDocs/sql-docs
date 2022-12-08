---
title: "Database engine events and errors"
description: Consult this MSSQL error code list to find explanations for error messages for SQL Server database engine events.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.service: sql
ms.subservice: supportability
ms.topic: reference
---
# Database engine errors

The table contains error message numbers and the description, which is the text of the error message from the `sys.messages` catalog view. Where applicable, the error number is a link to further information.

This list is not exhaustive. For a full list of all errors, query the `sys.messages` catalog view with the following query:

```sql
SELECT message_id AS Error,
    severity AS Severity,
    [Event Logged] = CASE is_event_logged
        WHEN 0 THEN 'No' ELSE 'Yes'
        END,
    [text] AS [Description]
FROM sys.messages
WHERE language_id = 1040 /* replace 1040 with the desired language ID, such as 1033 for US English*/
ORDER BY message_id;
```

## Errors -2 to 999

[!INCLUDE [database-engine-events-and-errors-2-999](../../includes/errorcodes/database-engine-events-and-errors-2-999.md)]

## Errors 1,000 to 1,999

[!INCLUDE [database-engine-events-and-errors-1000-1999](../../includes/errorcodes/database-engine-events-and-errors-1000-1999.md)]

## Errors 2,000 to 2,999

[!INCLUDE [database-engine-events-and-errors-2000-2999](../../includes/errorcodes/database-engine-events-and-errors-2000-2999.md)]

## Errors 3,000 to 3,999

[!INCLUDE [database-engine-events-and-errors-3000-3999](../../includes/errorcodes/database-engine-events-and-errors-3000-3999.md)]

## Errors 4,000 to 4,999

[!INCLUDE [database-engine-events-and-errors-4000-4999](../../includes/errorcodes/database-engine-events-and-errors-4000-4999.md)]

## Errors 5,000 to 5,999

[!INCLUDE [database-engine-events-and-errors-5000-5999](../../includes/errorcodes/database-engine-events-and-errors-5000-5999.md)]

## Errors 6,000 to 6,999

[!INCLUDE [database-engine-events-and-errors-6000-6999](../../includes/errorcodes/database-engine-events-and-errors-6000-6999.md)]

## Errors 7,000 to 7,999

[!INCLUDE [database-engine-events-and-errors-7000-7999](../../includes/errorcodes/database-engine-events-and-errors-7000-7999.md)]

## Errors 8,000 to 8,999

[!INCLUDE [database-engine-events-and-errors-8000-8999](../../includes/errorcodes/database-engine-events-and-errors-8000-8999.md)]

## Errors 9,000 to 9,999

[!INCLUDE [database-engine-events-and-errors-9000-9999](../../includes/errorcodes/database-engine-events-and-errors-9000-9999.md)]

## Errors 10,000 to 10,999

[!INCLUDE [database-engine-events-and-errors-10000-10999](../../includes/errorcodes/database-engine-events-and-errors-10000-10999.md)]

## Errors 11,000 to 12,999

[!INCLUDE [database-engine-events-and-errors-11000-12999](../../includes/errorcodes/database-engine-events-and-errors-11000-12999.md)]

## Errors 13,000 to 13,999

[!INCLUDE [database-engine-events-and-errors-13000-13999](../../includes/errorcodes/database-engine-events-and-errors-13000-13999.md)]

## Errors 14,000 to 14,999

[!INCLUDE [database-engine-events-and-errors-14000-14999](../../includes/errorcodes/database-engine-events-and-errors-14000-14999.md)]

## Errors 15,000 to 15,999

[!INCLUDE [database-engine-events-and-errors-15000-15999](../../includes/errorcodes/database-engine-events-and-errors-15000-15999.md)]

## Errors 16,000 to 17,999

[!INCLUDE [database-engine-events-and-errors-16000-17999](../../includes/errorcodes/database-engine-events-and-errors-16000-17999.md)]

## Errors 18,000 to 18,999

[!INCLUDE [database-engine-events-and-errors-18000-18999](../../includes/errorcodes/database-engine-events-and-errors-18000-18999.md)]

## Errors 19,000 to 20,999

[!INCLUDE [database-engine-events-and-errors-19000-20999](../../includes/errorcodes/database-engine-events-and-errors-19000-20999.md)]

## Errors 21,000 to 21,999

[!INCLUDE [database-engine-events-and-errors-21000-21999](../../includes/errorcodes/database-engine-events-and-errors-21000-21999.md)]

## Errors 22,000 to 22,999

[!INCLUDE [database-engine-events-and-errors-22000-22999](../../includes/errorcodes/database-engine-events-and-errors-22000-22999.md)]

## Errors 23,000 to 25,999

[!INCLUDE [database-engine-events-and-errors-23000-25999](../../includes/errorcodes/database-engine-events-and-errors-23000-25999.md)]

## Errors 26,000 to 27,999

[!INCLUDE [database-engine-events-and-errors-26000-27999](../../includes/errorcodes/database-engine-events-and-errors-26000-27999.md)]

## Errors 28,000 to 30,999

[!INCLUDE [database-engine-events-and-errors-28000-30999](../../includes/errorcodes/database-engine-events-and-errors-28000-30999.md)]

## Errors 31,000 to 41,400

[!INCLUDE [database-engine-events-and-errors-31000-41399](../../includes/errorcodes/database-engine-events-and-errors-31000-41399.md)]

## Errors 41,400 to 42109

[!INCLUDE [database-engine-events-and-errors-41400-42999](../../includes/errorcodes/database-engine-events-and-errors-41400-42999.md)]

## See also

- [Understanding Database Engine Errors](../../relational-databases/errors-events/understanding-database-engine-errors.md)
- [Cause and Resolution of Database Engine Errors](/previous-versions/sql/sql-server-2016/ms365262(v=sql.130))
