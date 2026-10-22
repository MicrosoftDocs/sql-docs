---
author: AttinderPalSingh
ms.author: atsingh
ms.reviewer: amitkh
ms.date: 05/02/2025
ms.service: sql
ms.subservice: linux
ms.topic: include
ms.custom:
  - linux-related-content
  - build-2025
---
| Platform | File system | Installation guide |
| --- | --- | --- |
| Red Hat Enterprise Linux 7.7 - 7.9 Server <sup>1</sup>, or 8.x Server | **XFS** or **ext4** | [Installation guide](../install-upgrade/quickstart-install-red-hat.md) |
| SUSE Linux Enterprise Server v12 SP3 - SP5 <sup>2</sup> | **XFS** or **ext4** | [Installation guide](../install-upgrade/quickstart-install-suse.md) |
| Ubuntu 18.04 LTS <sup>3</sup> | **XFS** or **ext4** | [Installation guide](../install-upgrade/quickstart-install-ubuntu.md) |
| Docker Engine 1.8+ on Linux <sup>4</sup> | N/A | [Installation guide](../install-upgrade/quickstart-install-docker.md) |

<sup>1</sup> At the end of June 2024, RHEL 7.x transitioned from mainstream maintenance to extended lifecycle support (ELS). For more information, see [Red Hat Enterprise Linux Life Cycle](https://access.redhat.com/support/policy/updates/errata/).

<sup>2</sup> At the end of Oct 2024, SLES v12 transitioned from standard general support to long term service pack support (LTSS). For more information, see [Product Support Lifecycle Lifecycle Dates by Product](https://www.suse.com/lifecycle#suse-linux-enterprise-server-12).

<sup>3</sup> At the end of April 2023, Ubuntu 18.04 LTS transitioned from standard maintenance to expanded security maintenance (ESM). For more information, see [Ubuntu 18.04 end of standard support](https://ubuntu.com/blog/18-04-end-of-standard-support).

<sup>4</sup> [!INCLUDE [container-emulation](container-emulation.md)]

> [!TIP]  
> For more information, review the [system requirements](../sql-server-linux-setup.md#system) for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Linux. For the latest support policy for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], see the [Technical support policy for Microsoft SQL Server](/troubleshoot/sql/general/support-policy-sql-server).
