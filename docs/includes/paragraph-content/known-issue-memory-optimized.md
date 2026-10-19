---
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 08/28/2025
ms.service: sql
ms.topic: include
---
**Known issue**: For databases with memory-optimized tables, performing a transactional log backup with no recovery, and later executing a transaction log restore with recovery, could result in an unresponsive database restore process. This issue can also affect log shipping functionality. To work around this problem, the [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] instance can be restarted before initiating the restore process.
