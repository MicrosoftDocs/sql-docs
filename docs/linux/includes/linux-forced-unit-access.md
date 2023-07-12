---
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/29/2023
ms.service: sql
ms.subservice: linux
ms.topic: include
---
Certain versions of supported Linux distributions provide support for FUA I/O subsystem capability, which provides data durability. [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] uses the FUA capability to provide highly efficient and reliable I/O for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] workloads. For more information on FUA support by Linux distribution and its effect on [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], see [SQL Server On Linux: Forced Unit Access (FUA) Internals](https://techcommunity.microsoft.com/t5/sql-server-blog/sql-server-on-linux-forced-unit-access-fua-internals/ba-p/3199102).

SUSE Linux Enterprise Server 12 SP5, Red Hat Enterprise Linux 8.0, and Ubuntu 18.04 introduced support for FUA capability in the I/O subsystem. If you're using [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] CU 6 and later versions, you should use following configuration for high performing and efficient I/O implementation with FUA by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

Use this recommended configuration if the following conditions are met.

- [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] CU 6 and later versions

- Linux distribution and version that supports FUA capability (starting with Red Hat Enterprise Linux 8.0, SUSE Linux Enterprise Server 12 SP5, or Ubuntu 18.04)

- XFS file system for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] storage

- Storage subsystem and/or hardware that supports and is configured for FUA capability

Recommended configuration:

1. Enable Trace Flag 3979 as a startup parameter.

1. Use **mssql-conf** to configure `control.writethrough = 1` and `control.alternatewritethrough = 0`.

For almost all other configuration that doesn't meet the previous conditions, the recommended configuration is as follows:

1. Enable Trace Flag 3982 as a startup parameter (which is the default for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] in the Linux ecosystem), and make sure that Trace Flag 3979 isn't enabled as a startup parameter.

1. Use **mssql-conf** to configure `control.writethrough = 1` and `control.alternatewritethrough = 1`.

#### FUA support for SQL Server containers deployed in Kubernetes

1. The [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] must use persisted mounted storage, and not `overlayfs`.

1. The storage must use the XFS filesystem and should support FUA. Before enabling this setting, you should work with your Linux distribution and storage vendor, to ensure that the OS and storage subsystem supports FUA options. On Kubernetes, you can query for the filesystem type using the following command, where `<pvc-name>` is your `PersistentVolumeClaim`:

   ```bash
   kubectl describe pv <pvc-name>
   ```

   In the output, look for the `fstype` that is set to XFS.

1. The worker node hosting the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] pods, should be using a Linux distribution and version that supports FUA capability (starting with Red Hat Enterprise Linux 8.0, SUSE Linux Enterprise Server 12 SP5, or Ubuntu 18.04).

If the above conditions are met, then you can use the following recommended FUA settings.

1. Enable Trace Flag 3979 as a startup parameter.

1. Use **mssql-conf** to configure `control.writethrough = 1` and `control.alternatewritethrough = 0`.
