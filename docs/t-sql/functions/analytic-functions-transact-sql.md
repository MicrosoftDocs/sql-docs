---
title: "Analytic Functions (Transact-SQL)"
description: "Analytic Functions (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/09/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: event-tier1-build-2022
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# Analytic functions (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-edge](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-edge.md)]

SQL Server supports these analytic functions:

- [CUME_DIST &#40;Transact-SQL&#41;](../../t-sql/functions/cume-dist-transact-sql.md)
- [FIRST_VALUE &#40;Transact-SQL&#41;](../../t-sql/functions/first-value-transact-sql.md)
- [LAG &#40;Transact-SQL&#41;](../../t-sql/functions/lag-transact-sql.md)
- [LAST_VALUE &#40;Transact-SQL&#41;](../../t-sql/functions/last-value-transact-sql.md)
- [LEAD &#40;Transact-SQL&#41;](../../t-sql/functions/lead-transact-sql.md)
- [PERCENT_RANK &#40;Transact-SQL&#41;](../../t-sql/functions/percent-rank-transact-sql.md)
- [PERCENTILE_CONT &#40;Transact-SQL&#41;](../../t-sql/functions/percentile-cont-transact-sql.md)  
- [PERCENTILE_DISC &#40;Transact-SQL&#41;](../../t-sql/functions/percentile-disc-transact-sql.md)

Analytic functions calculate an aggregate value based on a group of rows. Unlike aggregate functions, however, analytic functions can return multiple rows for each group. Use analytic functions to compute moving averages, running totals, percentages or top-N results within a group.

## See also

- [OVER Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-over-clause-transact-sql.md)
