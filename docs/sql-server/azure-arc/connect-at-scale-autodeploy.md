---
title: Auto onboarding of Arc enabled SQL Servers
description: In this article, you learn how to Microsoft help by auto onboarding SQL Server instances to Azure Arc at scale.
author: pochiraju
ms.author: rajpo
ms.reviewer: mikeray, randolphwest
ms.date: 03/22/2023
ms.service: sql
ms.topic: conceptual
---

# Auto onboarding of Arc enabled SQL Servers

Microsoft goal is to help customers onboard their Arc Servers to Arc enabled SQL Servers to simplify deployment in an automated process so that customers can receive the full benefits of connecting the SQL Servers to Azure Arc.
Installing Azure extension for SQL Server will provide free benefits help to manage, secure, protect and govern all your SQL Servers from a single point of control in Azure.  Learn more the benefits of Arc enabled SQL Servers.  [Lear more](https://learn.microsoft.com/en-us/sql/sql-server/azure-arc/overview?view=sql-server-ver16)

## Automatically insall Azure SQL extension on new machines connected to Arc

Starting from the <date> Microsoft will automatically deploy Azure extension for SQL Server, Arc enable SQL Server instances during the time of connecting the SQL Server machine to Azure Arc.  In this process

1. Microsoft attempt to register the  Microsoft.AzureArcData resource provider.
1. Install the Azure extension for SQL Server on the machines that SQL Server is installed.
1. Create Arc enabled SQL Server resource in Azure.
1. Microsoft recommends specify the desired license type, by [providing the license type](point to section: Specify License Type value in auto onboarding tag) value tag,  that we can use when the SQL Server extension is deployed via the automatic onboarding workflow.  This will allow to use the benefits of Arc SQL Server core features for free. [Lear more](https://learn.microsoft.com/en-us/sql/sql-server/azure-arc/manage-license-type?view=sql-server-ver16&tabs=azure)

Note: There is no cost associated with this change.  But If you would like to opt-out of the automatic installation of Azure extension for SQL Server, follow the instructions in the “How to opt-out automatic onboarding” section.

## Fix incomplete onboarding of Arc enabled SQL Servers

There are certain cases where the attempt to connect SQL Server machines to Azure may result into incomplete onboarding to Azure.  The following are the few reason that can result into incomplete onboarding of SQL Servers to Azure Arc.
1.	SQL Server machine is connected to Azure Arc, Azure extension for SQL Server is deployed but Microsoft.AzureArcData resource provider is not registered with your subscription.
2.	SQL Server machine is connected to Azure Arc but Azure extension for SQL Server is not deployed.

To fulfill your intent of connecting SQL Servers to Azure Arc and get the most out of Azure, we'll automatically deploy the Azure extension for SQL Server to your Arc-enabled servers that have SQL Server installed.  There is no cost associated with this change.
The auto onboarding of Arc enabled SQL Servers is executed in multiple phases as below.

1.	Microsoft will send a first email notification 14 days prior to the auto onboarding SQL Servers to Azure.  The email goes to the owner of subscription that contains one or more Arc Servers with SQL Server installed, but Arc enabled SQL Server onboarding is in complete.  The email outlines below.
a.	    The reason resulted in incomplete onboarding of SQL Servers to Azure Arc.
b.	    Provide specific guidance that will help you to complete the onboarding process of SQL Servers to Azure Arc on your own. 

If you'd like register Microsoft.AzureArcData, you can do so following the [steps](https://learn.microsoft.com/en-us/sql/sql-server/azure-arc/prerequisites?view=sql-server-ver16&tabs=azure#register-resource-providers)

 If you'd like to deploy the Azure extension for SQL Server yourself right away, you can do so following the [documentation](https://learn.microsoft.com/en-us/sql/sql-server/azure-arc/connect-already-enabled?view=sql-server-ver15&tabs=azure) to deploy the extension to one server at a time using the Azure Portal or you can use Azure Policy to deploy the extension to all servers that have SQL Server installed.

c.	    Provide the opt-out instructions, if you decide opt-out of the automatic onboarding that Microsoft plan to execute.
2.	    Microsoft will send 2nd notification of the above 7 days prior to the auto onboarding SQL Servers to Azure.
3.	    Microsoft will send final notification 1 day prior to the auto onboarding SQL Servers to Azure.
4. If there is no opt-out received, Microsoft will automatically be onboard SQL Servers to Azure Arc.
a.	If it is not already registered, Microsoft will register Microsoft.AzureArcData resource provider with your subscription.
b.	If it is not deployed already, deploy the Azure extension for SQL Servers on SQL Server machine.
c.	If License type is provided in the onboarding tag, set the SQL Server license type to the value provided. Learn more.


## Fix missing License type

You may have SQL Server machines successfully connected to Arc but need to set the proper “License Type” to unlock the free Arc enabled SQL Server benefits.  You can find the list of Arc enabled SQL Severs that are missing proper license type by using the resource graph query below.

```
resources
|extend licenseType =properties.licenseType
|extend licenseType = iff(licenseType=='','Configuration needed',licenseType)
|extend ResourceName=name
|where type =="microsoft.azurearcdata/sqlserverinstances" and licenseType in('Configuration needed')
|project id,tenantId,ResourceName,licenseType
```

Microsoft recommends specify the desired license type that we can use when the SQL Server extension is deployed via the automatic onboarding workflow, this will allow to use the benefits of Arc SQL Server core features for free, [learn more](https://learn.microsoft.com/en-us/sql/sql-server/azure-arc/manage-license-type?view=sql-server-ver16&tabs=azure)



## Specify License Type value in auto onboarding tag

Microsoft recommends specifying the desired license type, by providing the license type value tag, that we can use when the SQL Server extension is deployed via the automatic onboarding workflow.  Tag resources, resource groups, and subscriptions for logical organization - Azure Resource Manager | Microsoft Learn
You can add one of the tag and value below to a subscription or resource group(s) or Arc Server resource(s).

ArcSQLServerExtensionDeployment: Paid

ArcSQLServerExtensionDeployment: PAYG

ArcSQLServerExtensionDeployment: LicenseOnly


## How to opt-out automatic onboarding

If you would like to opt-out of the automatic installation of Azure extension for SQL Server, you can add the tag and value below to a subscription or resource group(s) or Arc Server resource(s).

ArcSQLServerExtensionDeployment: Disabled

Alternatively, you can also limit which extensions can be installed on your server, you can configure lists of the extensions you wish to allow and block on the server. Learn more https://learn.microsoft.com/en-us/azure/azure-arc/servers/security-overview#extension-allowlists-and-blocklists

## How Microsoft can aumatically deploy Azure extension for SQL Server

Microsoft can run extension installations on an Arc-enabled server through the following:
When the server connected to Arc, it installs Windows service Guest Configuration Extension service (ExtensionService) is responsible for installing, upgrading, and deleting extensions (agents, scripts, or other software) on the machine. The guest configuration and extension services run as Local System on Windows, and as root on Linux.  More details about the Arc agent services and service accounts can be found in the security guide [here](https://learn.microsoft.com/en-us/azure/azure-arc/servers/security-overview#agent-security-and-permissions)

Customers can deploy extensions using the Azure Portal, Azure Resource Manager (ARM) APIs, Azure Policy, ARM templates, the Azure CLI, or the Azure PowerShell module.  Microsoft can also call these APIs to deploy Azure extension for SQL Server and automatically onboarding to Arc enabled SQL Server

## Find SQL Servers connected to Arc, but missing Azure extension for SQL Server

You can use the Azure grapgh query below, to get the list of machine id and subscription id that contain Arc Servers with SQL Server installed but missing Azure extension for SQL Servers.

resources
| where type == "microsoft.hybridcompute/machines" and properties['detectedProperties']['mssqldiscovered'] has "true"
| extend 
    joinID = toupper(id)
| join kind= inner  (
    resources
    | where type == "microsoft.hybridcompute/machines/extensions"
    | extend machineId = toupper(substring(id, 0, indexof(id, '/extensions')))
    | project machineId, name
    | summarize allExtensions = make_list(name) by machineId
    | where allExtensions !has ("SqlServer")
) on $left.joinID == $right.machineId
| project id, subscriptionId, tenantId

## Next steps

https://learn.microsoft.com/en-us/sql/sql-server/azure-arc/overview?view=sql-server-ver16
https://learn.microsoft.com/en-us/sql/sql-server/azure-arc/deployment-options?view=sql-server-ver16
https://learn.microsoft.com/en-us/sql/sql-server/azure-arc/prerequisites?view=sql-server-ver16&tabs=azure
