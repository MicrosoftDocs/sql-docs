---
title: "BREAK (Transact-SQL)"
description: "BREAK (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "11/19/2018"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "BREAK"
  - "BREAK_TSQL"
helpviewer_keywords:
  - "exiting innermost loop [SQL Server]"
  - "END keyword"
  - "ignored statements"
  - "BREAK keyword"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# BREAK (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

BREAK exits the current WHILE loop. If the current WHILE loop is nested inside another, BREAK exits only the current loop, and control is given to the next statement in the outer loop.

BREAK is usually inside an IF statement.

## Examples

### Example for SQL Server
```sql
WHILE (1=1)
BEGIN
   IF EXISTS (SELECT * FROM ##MyTempTable WHERE EventCode = 'Done')
   BEGIN
      BREAK;  -- 'Done' row has finally been inserted and detected, so end this loop.
   END

   PRINT N'The other process is not yet done.';  -- Re-confirm the non-done status to the console.
   WAITFOR DELAY '00:01:30';  -- Sleep for 90 seconds.
END
```

### Example for Azure Synapse Dedicated SQL Pool
```sql
declare @sleeptimesec int = 5;
declare @startingtime datetime2 = getdate();

PRINT N'Sleeping for ' + cast(@sleeptimesec as varchar(5)) + ' seconds'
WHILE (1=1)
BEGIN
  
	PRINT N'Sleeping.';  
	print datediff(s, getdate(),  @startingtime)

	if datediff(s, getdate(),  @startingtime) < -@sleeptimesec
		begin
			print 'We have finished waiting.'
			break;
		end
END
```

## See Also

- [Control-of-Flow Language &#40;Transact-SQL&#41;](~/t-sql/language-elements/control-of-flow.md)
- [WHILE &#40;Transact-SQL&#41;](../../t-sql/language-elements/while-transact-sql.md)
- [IF...ELSE &#40;Transact-SQL&#41;](../../t-sql/language-elements/if-else-transact-sql.md)

