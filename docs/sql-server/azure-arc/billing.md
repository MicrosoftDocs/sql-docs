---
title: Enable billing through Microsoft Azure
titleSuffix: Azure Arc-enabled SQL Server
description: Explains how Azure Arc-enabled SQL Server is billed by Microsoft.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 11/05/2022
ms.service: sql
ms.topic: conceptual
---

# Billing through Microsoft Azure

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022.md)]

You may use a pay-as-you-go billing option to activate and run SQL Server with Azure Arc. This option is an alternative to using the traditional license agreement. [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)] introduces this option in setup and allows you to activate your instance for use in production without supplying a product key. See [SQL Server installation guide](../../database-engine/install-windows/install-sql-server.md).

## Prerequisites

* You're in a [Contributor role](/azure/role-based-access-control/built-in-roles#contributor) in at least one of the Azure subscriptions your organization created. Learn how to [create a new billing subscription](/azure/cloud-adoption-framework/ready/azure-best-practices/initial-subscriptions).
* You're in a [Contributor role](/azure/role-based-access-control/built-in-roles#contributor) for the resource group in which the SQL Server instance will be registered. See [Managed Azure resource groups](/azure/azure-resource-manager/management/manage-resource-groups-portal) for details.
* The **Microsoft.AzureArcData** and **Microsoft.HybridCompute** resource providers are registered in each  subscription you use for SQL Server pay-as-you-go billing.
* You select pay-as-you-go activation option in [SQL 2022 setup wizard](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md) or [command prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md).

To register the resource providers, use one of the methods below:  

# [Azure portal](#tab/azure)

1. Select **Subscriptions** 
2. Choose your subscription
3. Under **Settings**, select **Resource providers**
4. Search for `Microsoft.AzureArcData` and `Microsoft.HybridCompute` and select **Register**

# [PowerShell](#tab/powershell)

Run:

```powershell
Register-AzResourceProvider -ProviderNamespace Microsoft.AzureArcData
```

# [Azure CLI](#tab/az)

Run:

```azurecli
az provider register --namespace 'Microsoft.AzureArcData'
```

---

## Overview

You can select pay-as-you-go billing through Microsoft Azure to install a Standard or Enterprise edition without supplying a pre-purchased product key. This option requires that you have an active [Azure subscription](/azure/cloud-adoption-framework/ready/azure-best-practices/initial-subscriptions). Once your SQL Server instance is connected to Azure and the [Azure extension for SQL Server](connect.md) is installed on the hosting server, the SQL Server instance(s) will be registered with Azure Resource Manager (ARM) as a `SQL Server - Azure Arc` resource(s). The charges will be associated with a specific instance of SQL Server that requires a license. 

The billing granularity is one hour and the charges are calculated based on the SQL Server edition and the maximum size of the hosting server at any time during that hour. The size is measured in cores if the SQL Server instance is installed on a physical server, and logical cores (vCores) if the SQL Server instance is installed on a  virtual machine.

When multiple instances of SQL Server are installed on the same OS, only one instance requires to be licensed for the full size of the host, subject to minimum core size. See [SQL Server licensing guide](https://www.microsoft.com/licensing/docs/view/SQL-Server) for details. The billing logic uses the following rules to select instance to be licensed:

- The instance with the highest edition of all instances installed on the same operating system determines the required license.
- If two instances are installed with same edition but one instance is configured to use pay-as-you-go billing and the other is installed using a product key, the pay-as-you-go instance is ignored because it is included in the customer's product key.
- If two instances are installed with pay-as-you-go billing and same editions, the first instance in alphabetical order is billed.

> [!IMPORTANT]
> Pay-as-you-go billing option is only supported in SQL Server 2022 for Standard or Enterprise editions. 

## License types

Any installed instance of SQL server can be connected to Azure with the exception of Express and Web editions and SQL Server instances with a Serever/CAL license. When they are connected, their license type is visible in Azure portal as the `licenseType` property of the `SQL Server - Azure Arc` resource. One of the benefits of connecting your SQL Server instances to Azure is that you can manage the usage of the different licenses in the *Cost Management + Billing* portal but only the pay-as-you-go instances generate actual charges. This way you can manage your license position and maintain compliance. The following table shows the different license types.

| Installed edition | Activation choice  | License type  |  
|---|---|---|
| Enterprise Core | Product key with Software Assurance or Subscription | Paid |
| Enterprise Core | Pay-as-you-go | PAYG | 
| Standard | Product key with Software Assurance or Subscription | Paid | 
| Standard | Pay-as-you-go | PAYG |
| Enterprise Core | Product key without Software Assurance or Subscription | LicenseOnly | 
| Standard | Product key without Software Assurance or Subscription | LicenseOnly | 
| Evaluation | Free edition | Free | 
| Developer | Free edition | Free | 
|||

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
| Standard | Product key with Software Assurance or SQL subscription | Yes | HADR | 
| Standard | Pay-as-you-go | Yes | HADR | 
| Standard | Product key without Software Assurance or SQL subscription| Yes | LicenseOnly | 
|||

## FAQ

### Does pay-as-you-go billing stop when internet connectivity temporarily down

Intermittent internet connectivity does not stop the pay-as-you-go billing. The missed usage will be reported and accounted for by the billing logic when the connectivity is restored.

### Do I get charged if my virtual machine is stopped

When the VM is stopped, the usage data is not collected. Therefore, you will not be charged for the time the VM was stopped.  

### Do I get charged if my SQL Server instance is stopped

The usage data collection requires an active SQL Server instance. Therefore, you will not be charged for the time the SQL Server instance was stopped.  

### Do I get charged if my SQL Server instance was running for less than an hour

The billing granularity is one hour. If your instance was active for less than an hour, you will be billed for the full hour. 

### Is there a minimum number of cores with pay-as-you-go billing 

Pay-as-you-go billing doesn't change the licensing terms of SQL Server. Therefore, it is subject to the four-core limit as defined in the [SQL Server licensing terms](https://www.microsoft.com/licensing/terms/productoffering/SQLServer/EAEAS). 

### If the affinity mask is specified for my SQL Server to use a subset of virtual cores, will it reduce the pay-as-you-go-charges

When you run your SQL Server instance on a virtual or physical machine, you are required to license the full set of cores that the machine can access. Therefore, your pay-as-you-go charges will be based on the full core count even if you use the affinity mask to limit your SQL Server's usage of these cores.   See  [SQL Server licensing guide](https://www.microsoft.com/licensing/docs/view/SQL-Server) for details.

### Can I switch from pay-as-you-go to license and vice versa

Yes, you can change your selection. To change, run Setup again, and choose the **Maintenance** tab, then select **Edition Upgrade**.

## Next steps

- [Review SQL Server 2022 Pricing](https://www.microsoft.com/sql-server/sql-server-2022-pricing)
- [Install SQL Server 2022 using the pay-as-you-go activation option](../../database-engine/install-windows/install-sql-server.md)
