---
title: Antivirus software for APS
description: Use these guidelines to install antivirus software on Analytics Platform System. We recommend not installing antivirus software unless the software is required.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: martinle
ms.date: 06/23/2022
ms.service: sql
ms.subservice: data-warehouse
ms.topic: how-to
ms.custom:
  - seo-dt-2019
  - kr2b-contr-experiment
---

# Antivirus software for Analytics Platform System

If your datacenter requires antivirus software, use these guidelines to install antivirus software on Analytics Platform System. We recommend not installing antivirus software unless it's a firm requirement of your datacenter.

> [!WARNING]
>
> We strongly recommend that you individually assess the security risk for each computer and for Analytics Platform System as a whole, and that you select the tools that are appropriate for the security risk level of each computer. Additionally, we recommend that before you roll out any virus-protection project, you test the whole system under a full load to measure any changes in stability and performance.
>
> Virus protection software requires some system resources to execute. You must perform testing before and after you install your antivirus software to determine whether there is any performance effect on the Analytics Platform System.

This article is based on the guidance in [How to choose antivirus software to run on computers that are running SQL Server](https://support.microsoft.com/kb/309422) and [KB Article 961804](https://support.microsoft.com/kb/961804/en-us).

## Exclusion list for physical hosts

To install the antivirus software on the physical hosts, exclude the following directories and processes. These directories and processes shouldn't be scanned by the antivirus software.

Exclude these directories:

- *C:\ProgramData\Microsoft\Windows\Hyper-V*. Virtual machine configuration directory.
- *C:\Users\Public\Documents\Hyper-V\Virtual Hard Disks*. Default virtual hard disk drive directory.
- *C:\clusterStorage*. Virtual hard disk drive directories.

Exclude these processes:

- Virtual machine management (Vmms.exe)
- Virtual machine work processes (Vmwp.exe)

## Exclusion list for virtual machines

To install the antivirus software on the virtual machines, exclude the following directories and files. These directories and files shouldn't be scanned by the antivirus software.

Virtual machine `_PDW_region_-CTL01`:

- *C:\windows\cluster\\*
- *G:\\*

Virtual machine `_appliance_domain_-AD01` and `_appliance_domain_-AD02`:

- No restrictions

Compute node virtual machines:

- *C:\windows\cluster\\*
- *G:\\*

Virtual machine `_appliance_domain_-VMM`

- No restrictions

Virtual machine `_appliance_domain_-WDS`

- No restrictions

Virtual machine `_appliance_domain_-ISCSI01`

- *C:\iscsitarget*

## Next steps

[Appliance Management Tasks (Analytics Platform System)](appliance-management-tasks.md)
