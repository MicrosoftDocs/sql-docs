---
title: Overview of the managed instance stop and start feature (preview)
description: This article describes the managed instance stop and start feature of Azure SQL Managed Instance. 
author: urosmil
ms.author: urmilano
ms.reviewer: mathoma
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: conceptual
ms.custom:
---

# Stop and start a managed instance (preview) overview - Azure SQL Managed Instance 
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article describes how to stop and start a managed instance to save on billing costs when you're using Azure SQL Managed Instance. You can stop and start your managed instance by using either the Azure portal or Azure PowerShell. 

> [!NOTE]
> The stop and start feature for managed instances is currently in preview and available only for instances in the General Purpose service tier. 

## Overview

To save on billing costs, you can stop your General Purpose managed instance when you're not using it. Stopping a managed instance is similar to deallocating a virtual machine. When a managed instance is in a stopped state, you're no longer billed for compute and licensing costs while you're being billed for storage and backup storage. 

Stopping a managed instance clears all cached data. 

This features introduces three new managed instance states, as the following diagram indicates:

:::row:::
    :::column:::
        <br />
        <br />
        <br />- Stopping
        <br />- Stopped
        <br />- Starting
    :::column-end:::
    :::column:::
        ![Diagram showing the various states of a SQL Managed Instance deployment.](media/instance-stop-start-how-to/sql-managed-instance-states.png)
    :::column-end:::
:::row-end:::


After the stop operation is initiated, it typically takes about 5 minutes to stop the managed instance. However, starting a managed instance takes about 20 minutes from the moment the start operation is initiated. 

Only managed instances in a ready state can be stopped. After the managed instance is stopped, it stays in a stopped state until a start operation is initiated, either manually or triggered with a defined schedule. Only instances that are in a stopped state can be started.


> [!IMPORTANT]
> As a platform as a service (PaaS) service, SQL Managed Instance is responsible for compliance for every part of the system components. If there's an urgent need for system maintenance that requires the managed instance to be online, Azure can initiate the start operation and keep the managed instance online until the maintenance operation finishes, at which time Azure stops the managed instance. Compute and license charges are applied for the entire time the managed instance is in an online state. 


## Action types 

There are two ways to stop and start a managed instance: either manually on demand or by creating a schedule. 

### Manual commands 

You can use manual commands to immediately trigger a stop and start action. Manual commands are good for instances that have longer periods of inactivity without regular patterns, or for testing purposes. Alternatively, you can use [Azure Automation schedules](/azure/automation/shared-resources/schedules) or any custom solution that creates customized and more flexible schedules that you can't set up by using the built-in stop and start scheduler in SQL Managed Instance. 

### Scheduled commands 

You can also create a schedule with one or more multiple points of time when a stop or start action is triggered. Scheduled commands are good for instances that have regular patterns, such as starting a managed instance every weekday at 8 AM, stopping it at 5 PM, and then starting it on weekend days at 7 AM and stopping it at 11 AM. Scheduling your commands eliminates the need for you to create custom solutions or to use Azure Automation to create stop and start schedules.

Scheduled items represent points in time when stop and start events are initiated, not when the managed instance is up and running. When you're creating a schedule, take the operation duration into account. For example, if you want to have your managed instance up and running at 8 AM, you can define a schedule that initiates the start operation at 7:40 AM.

Consider the following rules for a stop and start schedule: 

- Scheduled items are defined as a stop-and-start pair, and they must have both stop and start values populated. It's not possible to have a populated stop value with a missing start value, and vice versa.
- There can't be an overlap of scheduled pairs. If there's an overlap of scheduled times, the API returns an error. 
- The time span between any two successive actions (that is, a start after a stop or a stop after a start) must be at least one hour. For example, if a start is scheduled for 10 AM, the stop action can't be scheduled before 11 AM. 
- If there are conflicting operations when a stop is triggered (such as scaling vCore in progress), the mechanism retries after 10 minutes. If after 10 minutes the conflicting operation is still active, the stop operation gets skipped.  

## Billing

Stopped instances don't get billed for vCores and the SQL license, they are charged only for storage and backup storage. However, vCores and license billing is charged for every *started* hour. For example, at 12:01, you will be charged for the entire hour, even if the managed instance is stopped within the hour. 

### Azure Hybrid Benefit

The [Azure Hybrid Benefit (AHB)](../azure-hybrid-benefit.md) is applied per resource. If your managed instance is using the Azure Hybrid Benefit to save on licensing costs, to apply that benefit to another resource when the managed instance is in a stopped state, you must first disable AHB on the managed instance, and then stop the managed instance. Similarly, after you restart the managed instance, you have to reenable AHB on it to apply the licensing benefit. 

### Reserved instance pricing 

[Reserved instance pricing (reserved capacity)](../database/reserved-capacity-overview.md) is applied for the vCores and hours emitted. When a managed instance that's eligible for reserved pricing is stopped, reserved pricing is automatically redirected to another instance, if one exists. You can use the stop and start feature to _overprovision_ reserved instance pricing. 

For example, let's say that you've purchased a managed instance with a reserved capacity of 16 vCores. You can run two managed instances with 8 vCores each from 1 PM to 2 PM, stop both instances, and then run two different managed instances with 8 vCores each from 2 PM to 3 PM. This approach would consume your 16 vCore limit for each hour, spread among four instances in total. 

Reservation discounts are offered on a ["use it or lose it"](/azure/cost-management-billing/reservations/understand-reservation-charges) basis. That is, if you don't have matching resources for a specified hour, the reservation quantity for that hour is lost. Unused reserved hours can't be carried forward.

## Limitations of the stop and start feature

Consider the following limitations: 

- You can't stop instances that: 
    - Have an ongoing [management operation](management-operations-overview.md) (such as an ongoing restore, vCores scaling, and so on).
    - Are part of a [failover group](auto-failover-group-sql-mi.md). 
    - Use the [Azure SQL Managed Instance link](managed-instance-link-feature-overview.md). 
- While a managed instance is in a stopped state, it's not possible to change its configuration properties. To change any properties, you must restart the managed instance. 
- While the managed instance is in a stopped state, it's not possible to take backups. For example, let's say that you have [long-term backups](long-term-backup-retention-configure.md) configured, with yearly backups in place. If you stop the managed instance during the defined yearly backup period, the backup will be skipped. We recommend that you keep the managed instance up and running during the yearly backup period. 
- It's not possible to cancel the stop or start operation after it has been initiated. 


## Define PowerShell parameters

If you're using PowerShell to execute manual stop and start commands, or you're creating a schedule, you first must define your parameters.

Skip this step if you're not using PowerShell. 

To define the parameters, first update the relevant values in the **USER CONFIGURABLE VALUES** section, and then run the following script:

```powershell
# ===============================================================
# SQL Managed Instance - Start/Stop feature examples
#
# Execute in Azure Cloud Shell PowerShell
# (C) 2021 Managed Instance product group
# ===============================================================
# ===============================================================
# USER CONFIGURABLE VALUES
# ===============================================================
$SubscriptionId = "<SubscriptionID>"
$SqlMIName = "<Sql-MI-name>"
$RgName = "<ResourceGroup>"
# ===============================================================
# DO NOT MODIFY THE SCRIPT BELOW - NOT USER CONFIGURABLE
# ===============================================================
# Constants
$UriPrefix = "https://management.azure.com/subscriptions/" + $SubscriptionId + "/resourceGroups/" + $RgName + 
"/providers/Microsoft.Sql/managedInstances/"
$UriSuffix = "?api-version=2021-08-01-preview"
# Main
Write-Host "Login to Azure subscription $SubscriptionID ..."
echo "Logging to Azure subscription"
# Login-AzAccount
Select-AzSubscription -SubscriptionName $SubscriptionID
Write-Host "Getting the profile information ..."
$azContext = Get-AzContext
$azProfile = [Microsoft.Azure.Commands.Common.Authentication.Abstractions.AzureRmProfileProvider]::Instance.Profile
$profileClient = New-Object -TypeName Microsoft.Azure.Commands.ResourceManager.Common.RMProfileClient -
ArgumentList ($azProfile)
# Get authentication token
Write-Host "Getting authentication token for REST API call ..."
$token = $profileClient.AcquireAccessToken($azContext.Subscription.TenantId)
$authHeader = @{'Content-Type'='application/json';'Authorization'='Bearer ' + $token.AccessToken}
# Define Instance GET uri
$instanceGetUri = $UriPrefix + $SqlMIName + $UriSuffix
```

----

## Stop the managed instance 

You can stop the managed instance by using either the Azure portal or Azure PowerShell. 


### [Portal](#tab/azure-portal)

To stop your managed instance by using the [Azure portal](https://portal.azure.com), go to the **Overview** pane of your managed instance, and then select the **Stop** button, as shown here: 

:::image type="content" source="media/instance-stop-start-how-to/manual-instance-stop.png" alt-text="Screenshot of the managed instance 'Overview' pane in the Azure portal, with the 'Stop' button highlighted. ":::

If your managed instance is already stopped, the **Stop** button is unavailable. 

### [PowerShell](#tab/azure-powershell)

To stop your managed instance by using PowerShell, run the following script: 

```powershell
######## STOP SECTION ########
# Define Stop SQL Managed Instance URI
Write-Host "Generating URI for stopping SQL Managed instance $SqlMIName in resource group $RgName"
$instanceStopUri = $UriPrefix + $SqlMIName + "/stop " + $UriSuffix
# Invoke API call to start the operation
$stopInstanceResp = Invoke-WebRequest -Method Post -Headers $authHeader -Uri $instanceStopUri
Write-Host "Instance Stop operation triggered:`n" $stopInstanceResp
#Get the operation ID
$stopInstanceOperationId = ($stopInstanceResp.Headers | ConvertTo-Json | ConvertFrom-Json)."x-ms-request-id"
Write-Host "Stop operation ID:`n" $stopInstanceOperationId
# Get the header from the API response, if status returned is Accepted, all is good
$stopInstanceStatusUri = ($stopInstanceResp.Headers | ConvertTo-Json | ConvertFrom-Json)."Azure-AsyncOperation"
Write-Host "Instance stop operation unique Get-status URI:`n" $stopInstanceStatusUri
# Poll the status of the operation (statuses: InProgress, Succeeded, Failed), continue when Succeeded
$stopInstanceStatusResp = Invoke-WebRequest -Method Get -Headers $authHeader -Uri (out-string -inputobject 
$stopInstanceStatusUri)
Write-Host "Status of the managed instance stop operation:`n" $stopInstanceStatusResp
# Get the operation result URI
$stopInstanceOperationStatusUri = ($stopInstanceResp.Headers | ConvertTo-Json | ConvertFrom-Json)."Location"
Write-Host "Instance stop operation result unique URI:`n" $stopInstanceOperationStatus
# Check the stop operation result
$stopInstanceOperationStatusResp = Invoke-WebRequest -Method Get -Headers $authHeader -Uri (out-string -inputobject 
$stopInstanceOperationStatusUri)
Write-Host "Status of the managed instance stop operation:`n" $stopInstanceOperationStatusResp
# Get the SQL Managed Instance and check properties
$getInstanceResp = Invoke-WebRequest -Method Get -Headers $authHeader -Uri $instanceGetUri
Write-Host "Instance Get API Response:`n" $getInstanceResp | ConvertFrom-Json
```

Stopping the managed instance uses the following API call:

`POST
https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/
providers/Microsoft.Sql/managedInstances/{managedInstanceName}/stop?api-version=2021-08-01-preview`


---

## Start the managed instance

You can start your managed instance by using either the Azure portal or Azure PowerShell. 

### [Portal](#tab/azure-portal)

After your managed instance is stopped, to start your managed instance by using the [Azure portal](https://portal.azure.com), go to the **Overview** pane of your managed instance, and then select the **Start** button, as shown here: 

:::image type="content" source="media/instance-stop-start-how-to/manual-instance-start.png" alt-text="Screenshot of the managed instance 'Overview' pane in the Azure portal, with the 'Start' button highlighted. ":::

If your managed instance is already started, the **Start** button is unavailable. 

### [PowerShell](#tab/azure-powershell)


To start your managed instance by using PowerShell, run the following script: 

```powershell
######## START SECTION ########
SQL Managed Instance URI
Write-Host "Generating URI for starting SQL Managed instance $SqlMIName in resource group $RgName"
$instanceStartUri = $UriPrefix + $SqlMIName + "/start " + $UriSuffix
# Invoke API call to start the operation
$startInstanceResp = Invoke-WebRequest -Method Post -Headers $authHeader -Uri $instanceStartUri
Write-Host "Instance Start API Response:`n" $startInstanceResp
#Get the operation ID
$startInstanceOperationId = ($startInstanceResp.Headers | ConvertTo-Json | ConvertFrom-Json)."x-ms-request-id"
Write-Host "Stop operation ID:`n" $startInstanceOperationId
# Get the header from the API response, if status returned is Accepted, all is good
$startInstanceStatusUri = ($startInstanceResp.Headers | ConvertTo-Json | ConvertFrom-Json)."Azure-AsyncOperation"
Write-Host "Instance start operation unique Get-status URI:`n" $startInstanceStatusUri
# Poll the status of the operation (statuses: InProgress, Succeeded, Failed), continue when Succeeded
$startInstanceStatusResp = Invoke-WebRequest -Method Get -Headers $authHeader -Uri (out-string -inputobject 
$startInstanceStatusUri)
Write-Host "Status of the managed instance start operation:`n" $startInstanceStatusResp
# Get the operation result URI
$startInstanceOperationStatusUri = ($startInstanceResp.Headers | ConvertTo-Json | ConvertFrom-Json)."Location"
Write-Host "Instance start operation result unique URI:`n" $startInstanceOperationStatusUri
# Check the start operation result
$startInstanceOperationStatusResp = Invoke-WebRequest -Method Get -Headers $authHeader -Uri (out-string -inputobject 
$startInstanceOperationStatusUri)
Write-Host "Status of the managed instance start operation:`n" $startInstanceOperationStatusResp
# Get the SQL Managed Instance and check properties
$getInstanceResp = Invoke-WebRequest -Method Get -Headers $authHeader -Uri $instanceGetUri
Write-Host "Instance Get API Response:`n" $getInstanceResp | ConvertFrom-Json
```


Starting the managed instance uses the following API call: 

`https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/
providers/Microsoft.Sql/managedInstances/{managedInstanceName}/start?api-version=2021-08-01-preview`

---


## Manage a stop and start schedule

You can manage a stop and start schedule by using either the Azure portal or Azure PowerShell. 

### [Portal](#tab/azure-portal)

To manage a stop and start schedule by using the [Azure portal](https://portal.azure.com), go to your managed instance, and then, on the left pane, select **Start/Stop Schedule**. 

:::image type="content" source="media/instance-stop-start-how-to/start-stop-schedule.png" alt-text="Screenshot of the 'Start/Stop schedule' pane of the managed instance." lightbox="media/instance-stop-start-how-to/start-stop-schedule.png":::

On the **Start/Stop Schedule** pane you can:

- View existing schedules.
- Specify the time zone of your scheduled events in the **Time zone** dropdown list. 
- Create a new schedule by selecting **Create a schedule item**. 
- Modify an existing schedule by selecting the pencil icon. 
- Delete an existing schedule by selecting the trash can icon. 

### [PowerShell](#tab/azure-powershell)


### Create or update schedule

To create or update a schedule to stop and start a managed instance by using PowerShell, run the following script: 

```powershell
######## CREATE OR UPDATE SCHEDULE ########
# Define URI for creating or updating start/stop schedule
Write-Host "Creating start/stop schedule for SQL Managed instance $SqlMIName in resource group $RgName"
$instanceCreateScheduleUri = $UriPrefix + $SqlMIName + "/startStopSchedules/default " + $UriSuffix
# Define schedule to be applied
$requestBody = [pscustomobject]@{
 properties = [pscustomobject]@{
 timeZoneId = "Central European Standard Time"
 description = "This is a schedule for our Dev/Test environment."
 scheduleList = @(
 @{startDay='Monday';startTime='06:00 AM';stopDay='Monday';stopTime='01:00 PM'}
 @{startDay='Wednesday';startTime='08:00 AM';stopDay='Wednesday';stopTime='05:00 PM'}
 @{startDay='Friday';startTime='03:00 PM';stopDay='Friday';stopTime='05:00 PM'}
 ) 
 } 
}
$instanceScheduleBody = ConvertTo-Json -InputObject $requestBody -Depth 3
# Invoke API call to start the operation
Invoke-WebRequest -Method Put -Headers $authHeader -Uri $instanceCreateScheduleUri -Body $instanceScheduleBody
```

Creating a schedule relies on the start StopSchedules API call: 

`PUT
https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/
providers/Microsoft.Sql/managedInstances/{managedInstanceName}/startStopSchedules/default?api-version=2021-08-01-preview`

#### Check a schedule

To check an existing schedule, use the following sample script: 

```powershell
######## GET SCHEDULE ########
# Define URI for getting start/stop schedule
Write-Host "Getting start/stop schedule for SQL Managed instance $SqlMIName in resource group $RgName"
$instanceScheduleGetUri = $UriPrefix + $SqlMIName + "/startStopSchedules/default " + $UriSuffix
# Invoke API call to start the operation
Invoke-WebRequest -Method Get -Headers $authHeader -Uri $instanceScheduleGetUri
```

Checking the schedule uses the following API call: 

`GET
https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/
providers/Microsoft.Sql/managedInstances/{managedInstanceName}/startStopSchedules/default?api-version=2021-08-01-preview`


### Delete a schedule

To delete an existing schedule, use the following sample script: 

```powershell
######## DELETE SCHEDULE ########
# Define URI for deleting start/stop schedule
Write-Host "Deleting start/stop schedule for SQL Managed instance $SqlMIName in resource group $RgName"
$instanceScheduleDeleteUri = $UriPrefix + $SqlMIName + "/startStopSchedules/default " + $UriSuffix
# Invoke API call to start the operation
Invoke-WebRequest -Method Delete -Headers $authHeader -Uri $instanceScheduleDeleteUri
```

Deleting a schedule uses the following API call: 

`DELETE
https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/
providers/Microsoft.Sql/managedInstances/{managedInstanceName}/startStopSchedules/default?api-version=2021-08-01-preview`

---


## Next steps

- For an overview, see [What is Azure SQL Managed Instance?](sql-managed-instance-paas-overview.md).
- Learn about [connectivity architecture in SQL Managed Instance](connectivity-architecture-overview.md).
- Learn how to [modify an existing virtual network for SQL Managed Instance](vnet-existing-add-subnet.md).
- For a tutorial that shows how to create a virtual network, create a managed instance, and restore a database from a database backup, see [Create an Azure SQL Managed Instance deployment (Azure portal)](instance-create-quickstart.md).
- For DNS issues, see [Resolve private DNS names in Azure SQL Managed Instance](resolve-private-domain-names.md).
