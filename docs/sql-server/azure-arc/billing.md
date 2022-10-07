---
title: Enable billing through Microsoft Azure
titleSuffix: Azure Arc-enabled SQL Server
description: Explains how Azure Arc-enabled SQL Server is billed by Microsoft.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 10/05/2022
ms.prod: sql
ms.topic: conceptual
---

# Billing through Microsoft Azure

Azure Arc-enabled SQL Server allows you to use a pay-as-you-go option of purchasing the SQL Server software license. This is an alternative to prepaying for it using the traditional license agreement with Microsoft. Starting with SQL Server 2022, this option is incudied with SQL Server setup amd allows you tro activete your SQL Server instamnce for use in produiction without supplying a Product key. See [SQL Server installation guide] (https://learn.microsoft.com/sql/database-engine/install-windows/install-sql-server) for details.

## Overview

You can select pay-as-you-go billing through Microsoft Azure when installing a Standard or Enterprise edition. This option requires that you have an active Azure subscription. Once your SQL Server instance is connected to Azure and the Azure extesnsion for SQL Server is installed on the hosting server, the SQL Server isntance(s) will be registered with Azure Resource Manager (ARM) as a `SQL Server - Azure Arc` resource. 

The billing granualarity is one hour and the charges are calculated based on the SQL Server edition and the maximum size of the host at any time during that hour. The size of the host is measured in physical cores if the SQL Server instance is installed on a physical machine, or in virtual cores if it is installed on a virtual machine. 

If multiple instances of SQL Server are installed on the same OS, only one instance requires to be licensed for the full size of the host, subject to minimum core size. See [SQL Server licensing guide](https://www.microsoft.com/licensing/docs/view/SQL-Server). The billing logic picks that instance using the following principles:

1. The instance with the highest edition of all instances installed on the same OS determines the license cost. 
1. If two instances are installed with same edition but one instance is configured to use pay-as-you-go billing and the other is pre-paid, i.e. is installed using a Product key, pay-as-you-go instance is ignored to minimize the customer cost.

The pay-as-you-go billing requires that the following conditions are met:

1. The host is in a Running state. E.g. the virtual machine is fully up.
1. The hosting server is onboarded to Azure Arc.
1. SQL Server instance and Azure extension for SQL Server are installed.
1. The pay-as-you-go option is selected during the SQL Server installation or enabled in Azure portal. 

> [!IMPORTANT]
> Intermittent internet connectivity does not stop billing. The missed usage will be reported and accoounted for by the billing logic when the connectivity is restored. 

 
## Next steps