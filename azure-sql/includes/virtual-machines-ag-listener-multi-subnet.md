---
author: MashaMSFT
ms.author: mathoma
ms.date: 04/27/2023
ms.service: azure-vm-sql-server
ms.topic: include
---
> [!TIP]
> There are many [methods to deploy an availability group](../virtual-machines/windows/availability-group-overview.md#deployment-options). Simplify your deployment and eliminate the need for an Azure Load Balancer or distributed network name (DNN) for your Always On availability group by creating your SQL Server virtual machines (VMs) in [multiple subnets](../virtual-machines/windows/availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md) within the same Azure virtual network. If you've already created your availability group in a single subnet, you can [migrate it to a multi-subnet environment](../virtual-machines/windows/availability-group-manually-migrate-multi-subnet.md). 