---
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: vanto
ms.date: 07/15/2024
ms.service: sql
ms.subservice: linux
ms.topic: include
ms.custom:
  - linux-related-content
---
| Platform | File system | Installation guide |
| --- | --- | --- |
| Red Hat Enterprise Linux 7.7 - 7.9 Server <sup>1</sup>, or 8.x Server | XFS or EXT4 | [Installation guide](../quickstart-install-connect-red-hat.md) |
| SUSE Linux Enterprise Server v12 SP3 - SP5 <sup>2</sup> | XFS or EXT4 | [Installation guide](../quickstart-install-connect-suse.md) |
| Ubuntu 18.04 LTS <sup>3</sup> | XFS or EXT4 | [Installation guide](../quickstart-install-connect-ubuntu.md) |
| Docker Engine 1.8+ on Linux | N/A | [Installation guide](../quickstart-install-connect-docker.md) |

<sup>1</sup> At the end of June 2024, RHEL 7.x transitioned from mainstream maintenance to extended lifecycle support (ELS). For more information, see [Red Hat Enterprise Linux Life Cycle](https://access.redhat.com/support/policy/updates/errata/).

<sup>2</sup> At the end of Oct 2024, SLES v12 will transition from standard general support to long term service pack support (LTSS). For more information, see [Product Support Lifecycle Lifecycle Dates by Product](https://www.suse.com/lifecycle#suse-linux-enterprise-server-12).

<sup>3</sup> At the end of April 2023, Ubuntu 18.04 LTS transitioned from standard maintenance to expanded security maintenance (ESM). For more information, see [Ubuntu 18.04 end of standard support](https://ubuntu.com/blog/18-04-end-of-standard-support).

> [!TIP]  
> For more information, review the [system requirements](../sql-server-linux-setup.md#system) for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Linux. For the latest support policy for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], see the [Technical support policy for Microsoft SQL Server](/troubleshoot/sql/general/support-policy-sql-server).
