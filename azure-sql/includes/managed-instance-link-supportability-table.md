---
title: Managed Instance link supportability table
description: Inform customers of the SQL Server version and feature supportability matrix when using the Managed Instance link between SQL Server and Azure SQL Managed Instance. 
author: MashaMSFT
ms.author: mathoma
ms.topic: include
date: 04/10/2023
---

| SQL Server version  | Operating system (OS)  | One-way replication |  Disaster recovery | Servicing update requirement |
| --- | --- | --- | --- | --- |
| SQL Server 2022 (16.x) | Windows Server and Linux |  Generally available | [Must sign up for limited public preview](https://aka.ms/mi-link-dr-preview-signup)  | SQL Server 2022 RTM | 
| SQL Server 2019 (15.x) | Windows Server only | Generally available |Not supported | [SQL Server 2019 CU20 (KB)](need new link) or later for Enterprise and Developer editions, and [CU17 (KB5016394)](https://support.microsoft.com/help/5016394) or later for Standard editions |
| SQL Server 2017 (14.x) | N/A | Not supported | Not supported | N/A | 
| SQL Server 2016 (13.x) | Windows Server only | Generally available | Not supported|   [SQL Server 2016 SP3 (KB 5003279)](https://support.microsoft.com/help/5003279) and [SQL Server 2016 Azure Connect pack (KB 5014242)](https://support.microsoft.com/help/5014242) |
