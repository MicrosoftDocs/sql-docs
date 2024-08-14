---
title: Compute capacity limits by edition of SQL Server
description: This article discusses compute capacity limits for SQL Server 2019 and how they differ in physical and virtualized environments with simultaneous multithreading (SMT) processors.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest, derekw
ms.date: 07/30/2024
ms.service: sql
ms.subservice: release-landing
ms.topic: conceptual
helpviewer_keywords:
  - "simultaneous multithreading [SQL Server]"
  - "SMT [SQL Server]"
  - "hyper-threading [SQL Server]"
  - "processors [SQL Server], supported"
  - "number of processors supported"
  - "maximum number of processors supported"
---
# Compute capacity limits by edition of SQL Server

[!INCLUDE [sqlserver](../includes/applies-to-version/sqlserver.md)]

This article discusses compute capacity limits for editions of [!INCLUDE [ssnoversion](../includes/ssnoversion-md.md)] and how they differ in physical and virtualized environments with simultaneous multithreading (SMT) processors. On Intel CPUs, SMT is called *Hyper-Threading*.

## Overview

:::image type="content" source="media/compute-capacity-limits-by-edition-of-sql-server/compute-capacity-limits.png" alt-text="Diagram showing the mappings to compute capacity limits.":::

This table describes the notations in the preceding diagram:

| Value | Description |
| --- | --- |
| 0..1 | Zero or one |
| 1 | Exactly one |
| 1..* | One or more |
| 0..* | Zero or more |
| 1..2 | One or two |

To elaborate further:

- A virtual machine (VM) has one or more virtual processors.
- One or more virtual processors are allocated to exactly one virtual machine.
- Zero or one virtual processor is mapped to zero or more logical processors. When the mapping of virtual processors to logical processors is:
  - One to zero: represents an unbound logical processor not used by the guest operating systems.
  - One to many: represents an overcommit.
  - Zero to many: represents the absence of virtual machine on the host system. So VMs don't use any logical processors.
- A socket is mapped to zero or more cores. When the socket-to-core mapping is:
  - One to zero: represents an empty socket. No chip is installed.
  - One to one: represents a single-core chip installed in the socket. This mapping is rare these days.
  - One to many: represents a multi-core chip installed in the socket. Typical values are 2, 4, and 8.
- A core is mapped to one or two logical processors. When the mapping of cores to logical processors is:
  - One to one: SMT is off.
  - One to two: SMT is on.

The following definitions apply to the terms used in this article:

- A thread or logical processor is one logical computing engine from the perspective of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)], the operating system, an application, or a driver.

- A core is a processor unit. It can consist of one or more logical processors.

- A physical processor can consist of one or more cores. A physical processor is the same as a processor package or a socket.

<a id="numa-64"></a>

<a id="breaking-change-in-sql-server-2022-cumulative-update-11"></a>

<a id="reduce-logical-core-count-per-numa-node"></a>

## Limit number of logical cores per NUMA node to 64

You can experience issues such as stack dumps on servers with more than 64 logical processors per NUMA node. A BIOS or firmware configuration can reduce the logical core count to a maximum of 64 logical processors per NUMA node, presented to the operating system.

> [!CAUTION]  
> [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] Cumulative Update 11 introduced a breaking change, where the [!INCLUDE [ssde-md](../includes/ssde-md.md)] doesn't start if it detects more than 64 logical cores per NUMA node.

You can reduce the logical core count per NUMA node in an [Azure Virtual Machine](#disable-smt-in-an-azure-virtual-machine), by disabling SMT. For [bare-metal](#reduce-logical-core-count-on-bare-metal-instances) [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instances, you can reduce the logical core count with sub-NUMA clustering (SNC) or Nodes per Socket (NPS) options.

<a id="disable-smt-in-a-virtual-machine"></a>

### Disable SMT in an Azure Virtual Machine

[!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] has a supported limit of 64 logical cores per NUMA node. In some cases, the Azure Mv3-series VM might exceed this limit, which prevents [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] from starting, or allowing it to run with degraded performance. To disable SMT, make the following changes using **PowerShell** and the **Registry Editor** (`reg.exe`). Be sure to back up your registry before editing it.

1. Check the number of logical cores. SMT is enabled if the ratio is 2:1 (the number of logical cores is twice the number of cores).

   ```powershell
   Get-WmiObject -class win32_processor | Select-Object -Property "NumberOfCores", "NumberOfLogicalProcessors"
   ```

1. Disable SMT with the following two registry changes, then reboot the VM.

   ```cmd
   reg add "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management" /v FeatureSettingsOverride /t REG_DWORD /d 8264 /f
   reg add "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management" /v FeatureSettingsOverrideMask /t REG_DWORD /d 3 /f
   ```

1. Check the number of logical cores once again. The number of logical cores should match the number of cores.

   ```powershell
   Get-WmiObject -class win32_processor | Select-Object -Property "NumberOfCores", "NumberOfLogicalProcessors"
   ```

### Reduce logical core count on bare-metal instances

The following tables describe how to reduce the logical core count on bare-metal instances of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

On **Intel CPUs**, you can enable sub-NUMA clustering (SNC), formerly called Cluster-on-Die (CoD), resulting in two NUMA domains within a single physical socket.

| Configuration&nbsp;setting | Description |
| --- | --- |
| SNC disabled (default) | Disables sub-NUMA clustering. |
| SNC enabled | Enables sub-NUMA clustering. |

On **AMD CPUs**, you can enable various Nodes per Socket (NPS) options.

| Configuration&nbsp;setting | Description |
| --- | --- |
| `NPS0` | In a dual socket system, NUMA presents as a single node with all memory channels interleaved across the node. |
| `NPS1` (default) | This configuration presents one NUMA node per socket. |
| `NPS2` | This configuration presents two NUMA nodes per socket, similar to SNC. |
| `NPS4` | This configuration presents four NUMA nodes per socket. |

## Remarks

Systems with more than one physical processor or systems with physical processors that have multiple cores and/or SMT enable the operating system to execute multiple tasks simultaneously. Each thread of execution appears as a logical processor. For example, if your computer has two quad-core processors with SMT enabled and two threads per core, you have 16 logical processors: 2 processors x 4 cores per processor x 2 threads per core. It's worth noting that:

- The compute capacity of a logical processor from a single thread of an SMT core is less than the compute capacity of a logical processor from that same core with SMT disabled.

- The compute capacity of the two logical processors in the SMT core is greater than the compute capacity of the same core with SMT disabled.

Each edition of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] has two compute capacity limits:

- A maximum number of sockets (or physical processors or processor packages)

- A maximum number of cores as reported by the operating system

These limits apply to a single instance of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. They represent the maximum compute capacity that a single instance uses. They don't constrain the server where the instance might be deployed. In fact, deploying multiple instances of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on the same physical server is an efficient way to use the compute capacity of a physical server with more sockets and/or cores than the capacity limits allow.

The following table specifies the compute capacity limits for a single instance of each edition of [!INCLUDE [ssnoversion](../includes/ssnoversion-md.md)]:

| [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] edition | Maximum compute capacity for a single instance ([!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE [ssDE](../includes/ssde-md.md)]) | Maximum compute capacity for a single instance (AS, RS) |
| --- | --- | --- |
| Enterprise edition: Core-based licensing <sup>1</sup> | Operating system maximum | Operating system maximum |
| Developer | Operating system maximum | Operating system maximum |
| Standard | Limited to lesser of 4 sockets or 24 cores | Limited to lesser of 4 sockets or 24 cores |
| Express | Limited to lesser of 1 socket or 4 cores | Limited to lesser of 1 socket or 4 cores |

<sup>1</sup> Enterprise edition with Server + Client Access License (CAL) licensing is limited to 20 cores per [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] instance. (This licensing isn't available for new agreements.) There are no limits under the Core-based Server Licensing model.

In a virtualized environment, the compute capacity limit is based on the number of logical processors, not cores. The reason is that the processor architecture isn't visible to the guest applications.

For example, a server that has four sockets populated with quad-core processors and the ability to enable two SMT threads per core contains 32 logical processors with SMT enabled. But it contains only 16 logical processors with SMT disabled. These logical processors can be mapped to virtual machines on the server. The virtual machines' compute load on that logical processor is mapped to a thread of execution on the physical processor in the host server.

You might want to disable SMT when the performance for each virtual processor is important. You can configure SMT by using a BIOS setting for the processor during the BIOS setup, but it's typically a server-scoped operation that affects all workloads running on the server. You might consider separating workloads that run in virtualized environments, from workloads that would benefit from the SMT performance boost in a physical operating system environment.

## Related content

- [Editions and supported features of SQL Server 2022](editions-and-components-of-sql-server-2022.md)
- [Maximum capacity specifications for SQL Server](maximum-capacity-specifications-for-sql-server.md)
- [SQL Server installation guide](../database-engine/install-windows/install-sql-server.md)

[!INCLUDE [get-help-options](../includes/paragraph-content/get-help-options.md)]
