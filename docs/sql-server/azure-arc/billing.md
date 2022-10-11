---
title: Enable billing through Microsoft Azure
titleSuffix: Azure Arc-enabled SQL Server
description: Explains how Azure Arc-enabled SQL Server is billed by Microsoft.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 10/10/2022
ms.prod: sql
ms.topic: conceptual
---

# Billing through Microsoft Azure

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022.md)]

Azure Arc-enabled SQL Server allows you to use a pay-as-you-go option to purchasing the SQL Server software license. This option is an alternative to using the traditional license agreement with Microsoft. Starting with [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)], setup includes this option and allows you to activate your instance for use in production without supplying a product key. See [SQL Server installation guide](../../database-engine/install-windows/install-sql-server.md).

## Overview

You can select pay-as-you-go billing through Microsoft Azure to install a Standard or Enterprise edition without supplying a pre-purchased product key. This option requires that you have an active [Azure subscription](https://learn.microsoft.com/azure/cloud-adoption-framework/ready/azure-best-practices/initial-subscriptions). Once your SQL Server instance is connected to Azure and the [Azure extension for SQL Server](connect.md) is installed on the hosting server, the SQL Server instance(s) will be registered with Azure Resource Manager (ARM) as a `SQL Server - Azure Arc` resource(s). The charges will be associated with a specific instance of SQL Server that requires a license. 

The billing granularity is one hour and the charges are calculated based on the SQL Server edition and the maximum size of the host at any time during that hour. The size of the host is measured in logical cores (vCores) whether the SQL Server instance is installed on the physical server or virtual machine.

When multiple instances of SQL Server are installed on the same OS, only one instance requires to be licensed for the full size of the host, subject to minimum core size. See [SQL Server licensing guide](https://www.microsoft.com/licensing/docs/view/SQL-Server) for details. The billing logic uses the following rules to select instance to be licensed:

1. The instance with the highest edition of all instances installed on the same operating system determines the required license.
1. If two instances are installed with same edition but one instance is configured to use pay-as-you-go billing and the other is installed using a product key (for example, is pre-paid), the pay-as-you-go instance is ignored to minimize the customer cost.
1. If two instances are installed with pay-as-you-go billing but have different editions, the instance with the highest edition is billed. 
1. If two instances are installed with pay-as-you-go billing and same editions, the first installed instance is billed. 

The pay-as-you-go billing requires that the following conditions are met:

1. The host is in a running state. For example, the virtual machine is fully up.
1. The hosting server is onboarded to Azure Arc.
1. The SQL Server instance and Azure extension for SQL Server are installed.
1. The pay-as-you-go option is selected during the SQL Server installation, or enabled in Azure portal. 

If any of these conditions is not met, the pay-as-you-go billing will stop until they are met again.

> [!IMPORTANT]
> Intermittent internet connectivity does not stop the pay-as-you-go billing. The missed usage will be reported and accounted for by the billing logic when the connectivity is restored.

## License types

Any installed instance of SQL server can be connected to Azure. The only exceptions are Express and Web editions. When they are connected, their license  type is visible in Azure portal as the `licenseType` property of the `SQL Server - Azure Arc` resource. One of the benefits of connecting your SQL Server instances to Azure is that you can manage the usage of the different licences in the *Cost Management + Billing* portal but only the pay-as-you-go instances generate actual charges. This way you can manage your license position and maintain compliance. The following table shows the different license types.

| Installed edition | Activation choice  | License type  |  
|---|---|---|
| Enterprise Core | Product key with Software Assurance or Subscription | Paid |
| Enterprise Core | Pay-as-you-go | PAYG | 
| Standard | Product key with Software Assurance or Subscription | Paid | 
| Standard | Pay-as-you-go | PAYG |
| Enterprise Core | Product key without Software Assurance or Subscription | LicenseOnly | 
| Standard | Product key without Software Assurance or Subscription | LicenseOnly | 
| Enterprise | Product key, Server/CAL  | ServerCAL | 
| Evaluation | Free edition | Free | 
| Developer | Free edition | Free | 

Example of the instance properties of [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)] in JSON:

```json
    "properties": {
        "version": "SQL Server 2022",
        "edition": "Enterprise",
        ...
        "vCore": "8",
        "status": "Connected",
        ...
        "licenseType": "PAYG",
        ...
    }
```

One of the benefits of Software Assurance or SQL subscription is free fail-over servers for high availability and disaster recovery as long as they are passive replicas. See  [SQL Server licensing guide](https://www.microsoft.com/licensing/docs/view/SQL-Server) for details of this benefit.  Azure extension for SQL Server supports free fail-over servers by automatically detecting if the instance is a replica in an availability group. In that case, it reports it with a license type `HADR`, which does not require a separate license. The  following table shows the conditions when this license type is set.

| Installed edition | Activation choice  | AG replica | License type  |  
|---|---|---|---|
| Enterprise Core | Product key with Software Assurance or SQL subscription | Yes | HADR | 
| Enterprise Core | Pay-as-you-go | Yes | HADR | 
| Enterprise Core | Product key without Software Assurance or SQL subscription | Yes | LicenseOnly | 
| Enterprise | Product key, Server/CAL  | Yes | ServerCAL<sup>1</sup> 
| Standard | Product key with Software Assurance or SQL subscription | Yes | HADR | 
| Standard | Pay-as-you-go | Yes | HADR | 
| Standard | Product key without Software Assurance or SQL subscription| Yes | LicenseOnly | 

<sup>1</sup> *Server/CAL license does not include free fail-over servers for high availability.*

## FAQ

### Do I get charged if my SQL Server instance is stopped

The billing and usage collection is based on the time the instance is installed on the virtual machine, not on the time it is in the active state.  

### Do I get charged if my virtual machine is stopped

When the VM is stopped, the usage data is not be collected. Therefore you will not be charged for the time the VM was stopped.  

### If the affinity mask is specified for my SQL Server to use a subset of virtual cores, will it reduce the pay-as-you-go-charges. 

Whe you run your SQL Server instance on a virtual or physical machine, you are required to license the full set of cores that the machine can access. Therefore, your pay-as-you-go charges will be based on the full core count even if you use the affinity mask to limit your SQL Server's usage of these cores.   See  [SQL Server licensing guide](https://www.microsoft.com/licensing/docs/view/SQL-Server) for details.

### Can I switch from pay-as-you-go to license and visa-versa

Yes, you change you selection by running the setup again, choosing the **Maintenance** tab, then **Edition Upgrade**. 

## Next steps

- [Review SQL Server 2022 Pricing](https://www.microsoft.com/sql-server/sql-server-2022-pricing)
- [Install SQL Server 2022 using the pay-as-you-go activation option](../../database-engine/install-windows/install-sql-server.md)
