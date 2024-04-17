---
title: Compute capacity limits by edition of SQL Server
description: This article discusses compute capacity limits for SQL Server 2019 and how they differ in physical and virtualized environments with simultaneous multithreading (SMT) processors.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 04/12/2024
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

## <a id="numa-64"></a> Breaking change in SQL Server 2022 Cumulative Update 11

[!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] limits the number of logical processors per NUMA node to 64. On servers with more than 64 logical processors per NUMA node, you can use a BIOS / firmware configuration to change the number of NUMA nodes per physical socket presented to the operating system, to limit to a maximum of 64 logical processors.

You can also consider disabling SMT. On Intel CPUs, SMT is called *Hyper-Threading*.

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
| Enterprise Edition: Core-based Licensing <sup>1</sup> | Operating system maximum | Operating system maximum |
| Developer | Operating system maximum | Operating system maximum |
| Standard | Limited to lesser of 4 sockets or 24 cores | Limited to lesser of 4 sockets or 24 cores |
| Express | Limited to lesser of 1 socket or 4 cores | Limited to lesser of 1 socket or 4 cores |

<sup>1</sup> Enterprise Edition with Server + Client Access License (CAL) licensing is limited to 20 cores per [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] instance. (This licensing isn't available for new agreements.) There are no limits under the Core-based Server Licensing model.

In a virtualized environment, the compute capacity limit is based on the number of logical processors, not cores. The reason is that the processor architecture isn't visible to the guest applications.

For example, a server that has four sockets populated with quad-core processors and the ability to enable two SMT threads per core contains 32 logical processors with SMT enabled. But it contains only 16 logical processors with SMT disabled. These logical processors can be mapped to virtual machines on the server. The virtual machines' compute load on that logical processor is mapped to a thread of execution on the physical processor in the host server.

You might want to disable SMT when the performance for each virtual processor is important. You can enable or disable SMT by using a BIOS setting for the processor during the BIOS setup, but it's typically a server-scoped operation that affects all workloads running on the server. You might consider separating workloads that run in virtualized environments, from workloads that would benefit from the SMT performance boost in a physical operating system environment.

## Related content

- [Editions and supported features of SQL Server 2022](editions-and-components-of-sql-server-2022.md)
- [Maximum capacity specifications for SQL Server](maximum-capacity-specifications-for-sql-server.md)
- [SQL Server installation guide](../database-engine/install-windows/install-sql-server.md)

[!INCLUDE [get-help-options](../includes/paragraph-content/get-help-options.md)]
