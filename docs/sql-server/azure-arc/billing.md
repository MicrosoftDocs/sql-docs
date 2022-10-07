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

You can select pay-as-you-go billing through Microsoft Azure to install a Standard or Enterprise edition without supplying a pre-purchased Product key. This option requires that you have an active Azure subscription. Once your SQL Server instance is connected to Azure and the Azure extesnsion for SQL Server is installed on the hosting server, the SQL Server isntance(s) will be registered with Azure Resource Manager (ARM) as a `SQL Server - Azure Arc` resource(s). 

The billing granualarity is one hour and the charges are calculated based on the SQL Server edition and the maximum size of the host at any time during that hour. The size of the host is measured in physical cores if the SQL Server instance is installed on a physical machine, or in virtual cores if it is installed on a virtual machine. 

When multiple instances of SQL Server are installed on the same OS, only one instance requires to be licensed for the full size of the host, subject to minimum core size. See [SQL Server licensing guide](https://www.microsoft.com/licensing/docs/view/SQL-Server) for details. The billing logic that picks the instance to be licesed, it uses the following principles:

1. The instance with the highest edition of all instances installed on the same OS determines the required license. 
1. If two instances are installed with same edition but one instance is configured to use pay-as-you-go billing and the other is installed using a Product key, i.e. is pre-paid, pay-as-you-go instance is ignored to minimize the customer cost.

The pay-as-you-go billing requires that the following conditions are met:

1. The host is in a Running state. E.g. the virtual machine is fully up.
1. The hosting server is onboarded to Azure Arc.
1. SQL Server instance and Azure extension for SQL Server are installed.
1. The pay-as-you-go option is selected during the SQL Server installation or enabled in Azure portal. 

If any of these conditions is not met, the pay-as-you-go billing will stop until they are met again.   

> [!IMPORTANT]
> Intermittent internet connectivity does not stop the pay-as-y ou-go billing. The missed usage will be reported and accoounted for by the billing logic when the connectivity is restored. 

## Licence types

Any installed instace of SQL server can be connect to Azure. The only exceptions are Express and Web editions. When they are connected their license  type is visible in Azure portal as the `licenseType` property of the `SQL Server - Azure Arc` resource. One of the benefits of connecting your SQL Server instances to Azure is that you can manage the usage of the different licence in *Cost Management + Billing* portal but only the pay-as-you-go instances  generate the actual charges. This way you can manage you license poistion and maintain compliance. The following table shows the different license types.

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

Example of the instance proprties of SQL Server 2022 in JSON:

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
 
 One of the benefits of Software Assurance or Subscription is free fail-over servers for high availability. See  [SQL Server licensing guide](https://www.microsoft.com/licensing/docs/view/SQL-Server) for details of this benefit.  Azure extension for SQL Server support free fail-over servers by automatically detecting if the instance is a replica in an Avaiolability Group. In that case it reports it with a license type `HADR`, which does not require a separate license. The  following table shows the conditions when this license type is set.

| Installed edition | Activation choice  | AG replica | License type  |  
|---|---|---|---|
| Enterprise Core | Product key with Software Assurance or Subscription | Yes | HADR | 
| Enterprise Core | Pay-as-you-go | Yes | HADR | 
| Enterprise Core | Product key without Software Assurance or Subscription | Yes | LicenseOnly | 
| Enterprise | Product key, Server/CAL  | Yes | ServerCAL<sup>1</sup> 
| Standard | Product key with Software Assurance or Subscription | Yes | HADR | 
| Standard | Pay-as-you-go | Yes | HADR | 
| Standard | Product key without Software Assurance or Subscription | Yes | LicenseOnly | 

<sup>1</sup> *Server/CAL license does not include free fail-over servers for high availability.*


## Next steps

- [Review SQL Server 2022 Pricing](https://www.microsoft.com/sql-server/sql-server-2022-pricing)
- [Install SQL Server 2022 using the pay-as-you-go activation option](https://learn.microsoft.com/sql/database-engine/install-windows/install-sql-server)
