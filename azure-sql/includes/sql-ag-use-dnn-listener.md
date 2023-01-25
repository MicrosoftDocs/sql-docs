---
title: Use a DNN listener instead of a VNN listener for availability groups on SQL Server VMs
description: Redirect customers to using the distributed network name (DNN) listener instead of the virtual network name (VNN) listener.
author: MashaMSFT
ms.author: mathoma
ms.topic: include
---

> [!NOTE]
> Availability group deployments to [multiple subnets](../virtual-machines/windows/availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md) don't require a load balancer. In a single-subnet environment, customers who use SQL Server 2019 CU8 and later on Windows 2016 and later can replace the traditional virtual network name (VNN) listener and Azure Load Balancer with a [distributed network name (DNN) listener](../virtual-machines/windows/availability-group-distributed-network-name-dnn-listener-configure.md). If you want to use a DNN, skip any tutorial steps that configure Azure Load Balancer for your availability group. 
