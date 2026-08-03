---
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/23/2026
ms.service: sql
ms.topic: include
---

**Issue**: Under certain configurations on Windows Server 2025, [!INCLUDE [ssnoversion-md](ssnoversion-md.md)] could trigger access violation (AV) dumps if the **Lock pages in memory** (LPIM) Windows policy is enabled.

If you see this text in your error log during system startup, and you can't reproduce these conditions on earlier versions of Windows Server, you might be affected.

```output
Using locked pages in the memory manager.
```

**Workaround**: Disable the **Lock pages in memory** policy for the [!INCLUDE [ssnoversion-md](ssnoversion-md.md)] service account on Windows Server 2025.

Microsoft is investigating a fix for a future release of Windows Server 2025.

For more information about LPIM, see [Server memory configuration options](../database-engine/configure-windows/server-memory-server-configuration-options.md).
