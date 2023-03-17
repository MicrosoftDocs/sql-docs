---
author: MikeRayMSFT
ms.author: mikeray
ms.date: 03/16/2023
ms.topic: include
ms.service: sql
---

```sql
SELECT * FROM sys.dm_os_performance_counters   
WHERE object_name LIKE '%SQL%Deprecated Features%';  
```
