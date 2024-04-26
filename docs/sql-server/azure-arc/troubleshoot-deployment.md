---
title: "Troubleshoot deployment"
description: "Describes how to troubleshoot SQL Server enabled by Azure Arc deployment."
author: MikeRayMSFT
ms.author: mikeray
ms.date: 02/01/2023
ms.topic: troubleshooting-general
---

# Troubleshoot Azure extension for SQL Server

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

Before you start, note the logs locations.

## Log file locations

### Extension log

[!INCLUDE [extension-logs](includes/extension-logs.md)]

### Deployer logs

The deployer logs are deployed at:

   `C:\ProgramData\GuestConfig\extension_logs\Microsoft.AzureData.WindowsAgent.SqlServer\<extension version>\deployer.log`

   Replace `<extension version>` with your extension version. For example:

   `C:\ProgramData\GuestConfig\extension_logs\Microsoft.AzureData.WindowsAgent.SqlServer\1.1.0.0\deployer.log`

The failure to create the Arc-enabled SQL Server resource could be caused by several issues.

## Extension installation failed

Go to the connected server and check the deployer log. You should see the below messages.

```output
[YY/MM/DDDD HH:MM:SS UTC] [INFO]          Status of service 'SqlServerExtension' before attempting start: Stopped
[YY/MM/DDDD HH:MM:SS UTC] [INFO]          Status of service 'SqlServerExtension' after attempting start: Stopped
```

If you can't see it, the extension didn't install properly. Try the following steps.

1. Check event logs to see if anything preventing installation. Try installing SqlServerExtension.msi from the following folder `C:\Packages\Plugins\Microsoft.AzureData.WindowsAgent.SqlServer{version}`. The installation UI may provide the error details.

2. Close services app, server manager and retry one of the [connection methods](./connect.md) to install the extension, and see if that helps.

## Extension installed but didn't start

Check the log files for any application errors.

## Server - Azure Arc ARM resource was manually deleted

Check the extension log for the following record:

```output
[YY/MM/DDDD HH:MM:SS PM UTC] [ERROR]   [UploadServiceProvider]      [ExtensionHandlerStatusQueryError] ArcSqlInstancesRequest request is null, not sending data to RP.
```

This means the machine is no longer recognized as a connected server. [Onboard the server to Azure Arc](/azure/azure-arc/servers/onboard-portal) and retry one of the [connection methods](./connect.md) to install the extension.

## Server managed identity has insufficient permissions

Check the extension log for the following record:

```output
[INFO] [UploadServiceProvider] [ExtensionHandlerArcUploadServicesNotifications] [AzureUpload] Arc post request failed with error: Forbidden message: {"ErrorDescription":{"ErrorCode":6,"Message":"The user has no access to the provided Azure resource."},"ResponseUrl":null}
```

Make sure the machine's managed identity has been assigned the *Azure Connected SQL Server Onboarding* role. See [When machine already connected to Arc-enabled Server](connect-already-enabled.md) role assignment instructions.

## The user didn't migrate the Arc-enabled SQL Server resource to the new resource provider

Check the extension log for the following record:

```output
[YY/MM/DDDD HH:MM:SS PM UTC] [INFO] [UploadServiceProvider] [ExtensionHandlerArcUploadServicesNotifications] [AzureUpload] Arc for Sql Server upload response status: InternalServerError.`
```

Make sure to migrate the Arc-enabled SQL Server resource to `Microsoft.AzureArcData`.

## If extension is stuck in an odd state (Creating/Deleting) for long time

[Disconnect your SQL Server instances from Azure Arc](delete-from-azure-arc.md)

