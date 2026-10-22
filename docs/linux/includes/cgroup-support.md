---
author: AttinderPalSingh
ms.author: atsingh
ms.reviewer: amitkh
ms.date: 04/20/2026
ms.service: sql
ms.topic: include
ms.custom:
  - linux-related-content
ai-usage: ai-assisted
---
[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] detects and honors control group (cgroup) v2 constraints, starting with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Cumulative Update (CU) 20. These constraints provide fine-grained control in the Linux kernel over CPU and memory resources, and improve resource isolation in Docker, Kubernetes, and OpenShift environments.

In earlier versions, containerized deployments on Kubernetes clusters (for example, Azure Kubernetes Service v1.25+) could experience out of memory (OOM) errors because [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] didn't enforce memory limits defined in container specifications. Support for cgroup v2 addresses this problem.

### Check cgroup version

```bash
stat -fc %T /sys/fs/cgroup
```

The results are as follows:

| Result | Description |
| --- | --- |
| `cgroup2fs` | You use cgroup v2 |
| `cgroup` | You use cgroup v1 |

### Switch to cgroup v2

The easiest path is choosing a distribution that supports cgroup v2 out of the box.

If you need to switch manually, add the following parameter to your GRUB configuration:

```text
systemd.unified_cgroup_hierarchy=1
```

Then update GRUB. For example, on Ubuntu, run:

```bash
sudo update-grub
```

On Red Hat Enterprise Linux (RHEL), run:

```bash
sudo grub2-mkconfig -o /boot/grub2/grub.cfg
```

### CPU limit reporting with cgroup v2

When you configure CPU limits using cgroup v2, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] doesn't show the configured CPU core count in the error log. Instead, it continues to report the total number of host CPUs.

To align [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] scheduler and query plans (for example, parallelism decisions) with the intended CPU count defined in cgroup v2, apply the following configuration.

#### Configure processor affinity

Explicitly set [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] processor affinity to match the cgroup execution quota. In the following example, the cgroup quota is four CPUs on an eight-core host:

```sql
ALTER SERVER CONFIGURATION
SET PROCESS AFFINITY CPU = 0 TO 3;
```

This configuration ensures that [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] creates schedulers only for the intended number of CPUs. For more information, see [ALTER SERVER CONFIGURATION](../../t-sql/statements/alter-server-configuration-transact-sql.md) and [Use PROCESS AFFINITY for Node and/or CPUs](../sql-server-linux-performance-best-practices.md#use-process-affinity-for-node-andor-cpus).

#### Enable trace flag 8002 (recommended)

Enable trace flag 8002 to use soft affinity at the SQLPAL layer:

```bash
sudo /opt/mssql/bin/mssql-conf traceflag 8002 on
```

By default, schedulers are bound to specific CPUs defined in the affinity mask. Trace flag 8002 allows schedulers to move across CPUs instead, which generally improves performance while still respecting affinity and cgroup constraints. For more information, see [DBCC TRACEON - Trace Flags](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf8002).

Restart [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] after enabling the trace flag.

#### Expected behavior

After restart:

- [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] creates only the number of schedulers defined by the affinity setting (for example, four schedulers).

- The Linux kernel continues to enforce the cgroup v2 CPU execution quota.

- Query optimization and parallelism decisions are based on the intended CPU count, rather than the total host CPUs.

> [!NOTE]  
> The [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] error log might continue to display the total host CPU count. This logging and display behavior doesn't affect actual CPU usage, scheduler creation, or CPU enforcement by cgroup v2 or processor affinity.

For more information, see the following resources:

- [Quickstart: Deploy a SQL Server Linux container to Kubernetes using Helm charts](../containers/kubernetes-deploy-helm-charts.md)
- [Control Group v2 (Linux Kernel documentation)](https://www.kernel.org/doc/html/latest/admin-guide/cgroup-v2.html)
