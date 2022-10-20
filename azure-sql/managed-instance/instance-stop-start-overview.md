---
title: Instance stop and start overview
description: Learn about the instance stop and start feature of Azure SQL Managed Instance. 
author: urosmil
ms.author: urmilano
ms.reviewer: mathoma
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: conceptual
ms.custom:
---

# Instance stop and start overview - Azure SQL Managed Instance (Preview)
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article provides an overview of the instance stop and start feature for Azure SQL Managed Instance. To get started, review [Start and stop my instance](instance-stop-start-how-to.md). 

## Overview

Save on billing costs by stopping your managed instance when you're not using it. Stopping an instance is similar to deallocating a virtual machine. When an instance is in a stopped state, you're no longer billed for compute and licensing costs while still billed for storage and backup storage. 

Stopping an instance clears all cached data. 

This features introduces three new managed instance states:
- Stopping
- Stopped
- Starting

## Action types



## Billing





## Next steps

- For an overview, see [What is Azure SQL Managed Instance?](sql-managed-instance-paas-overview.md).
- Learn about [connectivity architecture in SQL Managed Instance](connectivity-architecture-overview.md).
- Learn how to [modify an existing virtual network for SQL Managed Instance](vnet-existing-add-subnet.md).
- For a tutorial that shows how to create a virtual network, create an Azure SQL Managed Instance, and restore a database from a database backup, see [Create an Azure SQL Managed Instance (portal)](instance-create-quickstart.md).
- For DNS issues, see [Resolving private DNS names in Azure SQL Managed Instance](resolve-private-domain-names.md).
