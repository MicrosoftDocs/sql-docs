---
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 01/31/2025
ms.topic: include
ms.custom: ignite-2023
---

SQL Server licenses with Software Assurance or pay-as-you-go (`PAYG`) can benefit from free passive instances of SQL Server for high availability and disaster recovery (HADR) configurations. For more information about the failover rights, see the section "Licensing SQL Server for high availability and disaster recovery" in the [SQL Server licensing guide](https://go.microsoft.com/fwlink/p/?linkid=2215573).

To help you manage the failover rights and remain compliant, Azure Extension for SQL Server automatically detects the passive instances for availability groups (AGs) or failover clustered instances (FCIs) and reflects the use of the SQL Server software by emitting special $0 meters for disaster recovery, as long as you configured the LicenseType property to `Paid` or `PAYG`. For more information, see [Manage licensing and billing of SQL Server enabled by Azure Arc](../manage-license-billing.md#usage-metering).
