---
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/06/2024
ms.service: sql
ms.subservice: linux
ms.topic: include
ms.custom:
  - linux-related-content
---
## SQL Server support policy

| Term | Definition |
| --- | --- |
| **Servicing** | Microsoft releases GDR, hotfixes, and security fixes within lifecycle of product for supported distributions. |
| **Support** | Microsoft supports users with problems pertaining to supported distributions. |

### Support policy

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is supported on Linux distributions until the earlier of two events: the end of the distribution's support lifecycle, or the end of the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] support lifecycle.

### Servicing policy

During the Mainstream support phase of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], we provide Cumulative Updates (CUs) for all Linux distributions that are also within their Mainstream support period. For Linux distributions that move from Mainstream to Extended support and are still recognized as supported platforms, [!INCLUDE [msconame-md](../../includes/msconame-md.md)] can release CUs and bug fixes at its discretion.

Once [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] moves beyond Mainstream support and into the Extended support phase, we continue to publish security updates and General Distribution Release (GDR) fixes. However, these updates aren't extended to Linux distributions that conclude their support period.
