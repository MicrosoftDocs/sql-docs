---
author: rwestMSFT
ms.author: randolphwest
ms.date: 02/21/2023
ms.service: sql
ms.subservice: linux
ms.topic: include
---
Certain versions of supported Linux distributions provide support for FUA I/O subsystem capability, which provides data durability. [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] uses FUA capability to provide highly efficient and reliable I/O for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] workloads. For more information on FUA support by Linux distribution and its effect on [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], see [SQL Server On Linux: Forced Unit Access (FUA) Internals](https://techcommunity.microsoft.com/t5/sql-server-blog/sql-server-on-linux-forced-unit-access-fua-internals/ba-p/3199102).

SUSE Linux Enterprise Server 12 SP5, Red Hat Enterprise Linux 8.0, and Ubuntu 18.04 introduced support for FUA capability in the I/O subsystem. If you're using [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] CU 6 and later versions, you should use following configuration for high performing and efficient I/O implementation with FUA by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

Use this recommended configuration if the following conditions are met.

- Using [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] CU 6 and later versions
- Using a Linux distribution and version that supports FUA capability (starting with Red Hat Enterprise Linux 8.0, SUSE Linux Enterprise Server 12 SP5, or Ubuntu 18.04)
- On storage subsystem and/or hardware that supports and is configured for FUA capability

Recommended configuration:

1. Enable Trace Flag 3979 as a startup parameter
1. Use **mssql-conf** to configure `control.writethrough = 1` and `control.alternatewritethrough = 0`

For almost all other configuration that doesn't meet the previous conditions, the recommended configuration is as follows:

1. Enable Trace Flag 3982 as a startup parameter (which is the default for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] in the Linux ecosystem), and make sure that Trace Flag 3979 isn't enabled as a startup parameter
1. Use **mssql-conf** to configure `control.writethrough = 1` and `control.alternatewritethrough = 1`
