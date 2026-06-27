---
author: erinstellato-ms
ms.author: erinstellato
ms.date: 06/29/2026
ms.service: sql
ms.topic: include
---
  > [!NOTE] 
  > Many ALTER DATABASE operations invalidate the plan cache for the affected database. This includes changes to database options, collation, compatibility level, database-scoped configurations, and filegroup properties. After the plan cache is cleared, subsequent query executions require recompilation, which may affect system performance.