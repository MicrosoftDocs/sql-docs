---
title: How to evaluate Azure Arc-enabled SQL Server instance with an Azure VM
description: Learn how to evaluate Azure Arc-enabled SQL Server instance using an Azure virtual machine.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 04/20/2023
ms.service: sql
ms.topic: conceptual
---

# Evaluate Azure Arc-enabled SQL Server instance on an Azure virtual machine

Azure Arc-enabled SQL Server is designed to help you connect servers running on-premises or in other clouds to Azure. Normally, you would not use Azure Arc-enabled SQL Server on an Azure virtual machine because all the same capabilities are natively available for these VMs, including a representation of the VM in Azure Resource Manager, VM extensions, managed identities, and Azure Policy.

While you cannot install Azure Arc-enabled SQL Server on an Azure VM for production scenarios, it is possible to configure Azure Arc-enabled servers to run on an Azure VM for *evaluation and testing purposes only*. This article will help you set up an Azure VM before you can enable Azure Arc-enabled servers on it.

> [!NOTE]
> The steps in this article are intended for virtual machines hosted in the Azure cloud. Azure Arc-enabled 
servers is not supported on virtual machines running on Azure Stack Hub or Azure Stack Edge.

## Start with a server

To evaluate Azure Arc-enabled SQL Server from an Azure Virtual Machine:

1. Create a virtual machine. Complete the prerequisites and steps in [Evaluate Azure Arc-enabled servers on an Azure virtual machine](/azure/azure-arc/servers/plan-evaluate-on-azure-virtual-machine).
1. Install a SQL Server instance.
1. Complete the prerequisites. [Prerequisites](prerequisites.md).
1. Connect the instance of SQL Server to Azure Arc. Follow the steps in [Connect with Azure Data Studio](../../azure-data-studio/connect.md).

## Next steps

[Configure SQL best practices assessment](assess.md)