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

This article teaches you to stop and start your instance to save on billing costs when using Azure SQL Managed Instance. Start and stop your instance by using Azure PowerShell. 

The ability to stop and start your instance is currently in preview. 

## Overview

Save on billing costs by stopping your managed instance when you're not using it. Stopping an instance is similar to deallocating a virtual machine. When an instance is in a stopped state, you're no longer billed for compute and licensing costs while still billed for storage and backup storage. 

Stopping an instance clears all cached data. 

This features introduces three new managed instance states:
- Stopping
- Stopped
- Starting


Stopping an instance usually takes up to 5 minutes, meaning after the stop operation is triggered, the operation response should be returned within 5 minutes. Only instances in a ready state can be stopped. Starting an instance can take longer to complete, up to 20 minutes. Only instances that are in the stopped state can be started.  

## Action types 

There are two ways to stop and start an instance - either manually on demand, or with a schedule. 

### Manual commands 

Manual commands immediately trigger the stop or start action. Manual commands are good for instances that have larger periods of inactivity without regular patterns, or for testing purposes. In addition, [Azure Automation Schedules](/azure/automation/shared-resources/schedules) or any custom solution that creates customized and more flexible schedules that cannot be achieved by using the built-in stop/start scheduler in SQL Managed Instance. 

### Scheduled commands 

You can also create a schedule with one or more multiple points of time when a start or stop action is triggered. Scheduling commands are a good fit for instances that have regular patterns, such as starting an instance every working day at 8am, and then stopping it at 5pm, and then starting at 7am and stopping at 11am on a weekend. Scheduling your commands eliminates the need for custom solutions or Azure Automation Schedules to create stop/start schedules. 

## Billing

Stopped instances don't get billed for vCores and the SQL license, they are only charged for storage and backup storage. However, instance vCores and license billing is charged for every **started** hour, meaning that at 12:01, you will be charged for the entire hour, even if the instance is stopped within the hour. 

### Azure Hybrid Benefit

The [Azure Hybrid Benefit (AHB)](../azure-hybrid-benefit.md) is applied per resource. If your SQL Managed Instance is using the Azure Hybrid Benefit to save on licensing costs, to apply that benefit to another resource when the instance is in a stopped state, you must first disable AHB on the instance, and then stop the instance. Similarly, once you start the instance again, you have to enable AHB on it once more to apply the licensing benefit. 

### Reserved instance pricing 

[Reserved instance pricing (reserved capacity)](../database/reserved-capacity-overview.md) is applied for the vCores and hours emitted. When an instance eligible for reserved pricing is stopped, reserved pricing is automatically redirected to another instance, if one exists. As such, the stop and stop feature can be used to "overprovision" reserved instance pricing. For example, assume you purchase a SQL Managed Instance with a reserved capacity of 16 vCores. This means that you can run two managed instances with 8 vCores each from 1pm to 2pm, stop  both instances, and then run two different managed instances with 8 vCores each from 2pm to 3pm, consuming your 16 vCore limit for each hour, spread amongst four instances in total. 

Reservation discounts are [use it or lose it](/azure/cost-management-billing/reservations/understand-reservation-charges), so if you don't have matching resources for any hour, then you lose the reservation quantity for that hour. You cannot carry forward unused reserved hours.

## Prerequisites

To stop and start your instance, you need the latest version of the [Az.Sql](/powershell/module/az.sql) module. 

## Define parameters

First, define the parameters, whether you're executing manual stop and start commands, or you're creating a schedule. To do so, update the relevant values in the **USER CONFIGURABLE VALUES** section, and then run the following script:

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
$SqlMIName = "<Sql-MI-name>”
$RgName = "<ResourceGroup>"
# ===============================================================
9
Microsoft Confidential. All rights reserved. --- PLEASE DO NOT SHARE, DO NOT PUBLISH OR TRANSMIT. ---
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

## Stop the instance 

Stopping the instance uses the following API call:

`POST
https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/
providers/Microsoft.Sql/managedInstances/{managedInstanceName}/stop?api-version=2021-08-01-
preview`

To stop your instance, run the following PowerShell sample script: 

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
Write-Host "Status of the instance stop operation:`n" $stopInstanceStatusResp
# Get the operation result URI
$stopInstanceOperationStatusUri = ($stopInstanceResp.Headers | ConvertTo-Json | ConvertFrom-Json)."Location"
Write-Host "Instance stop operation result unique URI:`n" $stopInstanceOperationStatus
# Check the stop operation result
$stopInstanceOperationStatusResp = Invoke-WebRequest -Method Get -Headers $authHeader -Uri (out-string -inputobject 
$stopInstanceOperationStatusUri)
Write-Host "Status of the instance stop operation:`n" $stopInstanceOperationStatusResp
# Get the SQL Managed Instance and check properties
$getInstanceResp = Invoke-WebRequest -Method Get -Headers $authHeader -Uri $instanceGetUri
Write-Host "Instance Get API Response:`n" $getInstanceResp | ConvertFrom-Json
```

## Start the instance 

Starting the instance uses the following API call: 

`https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/
providers/Microsoft.Sql/managedInstances/{managedInstanceName}/start?api-version=2021-08-01-
preview`

To start your instance, run the following PowerShell script: 

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
Write-Host "Status of the instance start operation:`n" $startInstanceStatusResp
# Get the operation result URI
$startInstanceOperationStatusUri = ($startInstanceResp.Headers | ConvertTo-Json | ConvertFrom-Json)."Location"
Write-Host "Instance start operation result unique URI:`n" $startInstanceOperationStatusUri
# Check the start operation result
$startInstanceOperationStatusResp = Invoke-WebRequest -Method Get -Headers $authHeader -Uri (out-string -inputobject 
$startInstanceOperationStatusUri)
Write-Host "Status of the instance start operation:`n" $startInstanceOperationStatusResp
# Get the SQL Managed Instance and check properties
$getInstanceResp = Invoke-WebRequest -Method Get -Headers $authHeader -Uri $instanceGetUri
Write-Host "Instance Get API Response:`n" $getInstanceResp | ConvertFrom-Json
```

## Create a schedule

Creating a schedule relies on the startStopSchedules API call: 

`PUT
https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/
providers/Microsoft.Sql/managedInstances/{managedInstanceName}/startStopSchedules/default?api-version=2021-08-01-preview`

To create or update a schedule to stop or start an instance, run the following sample script: 

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

## Check schedule

Checking the schedule uses the following API call: 

`GET
GET
https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/
providers/Microsoft.Sql/managedInstances/{managedInstanceName}/startStopSchedules/default?api-version=2021-08-01-preview`

To check an existing schedule, use the following sample script: 

```powershell
######## GET SCHEDULE ########
# Define URI for getting start/stop schedule
Write-Host "Getting start/stop schedule for SQL Managed instance $SqlMIName in resource group $RgName"
$instanceScheduleGetUri = $UriPrefix + $SqlMIName + "/startStopSchedules/default " + $UriSuffix
# Invoke API call to start the operation
Invoke-WebRequest -Method Get -Headers $authHeader -Uri $instanceScheduleGetUri
```


## Delete schedule

Deleting a schedule uses the following API call: 

`DELETE
https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/
providers/Microsoft.Sql/managedInstances/{managedInstanceName}/startStopSchedules/default?api-version=2021-08-01-preview`

To delete an existing schedule, use the following sample script: 

```powershell
######## DELETE SCHEDULE ########
# Define URI for deleting start/stop schedule
Write-Host "Deleting start/stop schedule for SQL Managed instance $SqlMIName in resource group $RgName"
$instanceScheduleDeleteUri = $UriPrefix + $SqlMIName + "/startStopSchedules/default " + $UriSuffix
# Invoke API call to start the operation
Invoke-WebRequest -Method Delete -Headers $authHeader -Uri $instanceScheduleDeleteUri
```



## Next steps

- For an overview, see [What is Azure SQL Managed Instance?](sql-managed-instance-paas-overview.md).
- Learn about [connectivity architecture in SQL Managed Instance](connectivity-architecture-overview.md).
- Learn how to [modify an existing virtual network for SQL Managed Instance](vnet-existing-add-subnet.md).
- For a tutorial that shows how to create a virtual network, create an Azure SQL Managed Instance, and restore a database from a database backup, see [Create an Azure SQL Managed Instance (portal)](instance-create-quickstart.md).
- For DNS issues, see [Resolving private DNS names in Azure SQL Managed Instance](resolve-private-domain-names.md).
