---
author: AttinderPalSingh
ms.author: atsingh
ms.reviewer: amitkh
ms.date: 01/27/2026
ms.service: sql
ms.subservice: linux
ms.topic: include
ms.custom:
  - linux-related-content
  - ignite-2025
---
You should run production workloads on supported platforms like [Red Hat Enterprise Linux](https://www.redhat.com/technologies/linux-platforms/enterprise-linux/sql-server) and [Ubuntu Pro](https://ubuntu.com/blog/microsoft-sql-server-on-ubuntu), as they receive regular OS security updates, and have support coverage options that you need for enterprise database deployments.

| Platform | File system | Installation guide | Get |
| --- | --- | --- | --- |
| Red Hat Enterprise Linux 10.x Server<br /><br />Red Hat Enterprise Linux 9.x Server | **XFS** or **ext4** | [Installation guide](../install-upgrade/quickstart-install-red-hat.md) | [Get RHEL 10](https://access.redhat.com/products/red-hat-enterprise-linux/evaluation)<br /><br />[Get RHEL 9](https://access.redhat.com/products/red-hat-enterprise-linux/evaluation) |
| Ubuntu 24.04<br /><br />Ubuntu 22.04 | **XFS** or **ext4** | [Installation guide](../install-upgrade/quickstart-install-ubuntu.md) | [Get Ubuntu 24.04](https://releases.ubuntu.com/24.04/)<br /><br />[Get Ubuntu 22.04](https://releases.ubuntu.com/22.04/) |
| Docker Engine 1.8+ on Linux <sup>1</sup> | N/A | [Installation guide](../install-upgrade/quickstart-install-docker.md) | [Get Docker](https://www.docker.com/get-started) |

<sup>1</sup> [!INCLUDE [container-emulation](container-emulation.md)]

> [!TIP]  
> For more information, review the [system requirements](../sql-server-linux-setup.md#system) for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux. For the latest support policy for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see the [Technical support policy for Microsoft SQL Server](/troubleshoot/sql/general/support-policy-sql-server).
