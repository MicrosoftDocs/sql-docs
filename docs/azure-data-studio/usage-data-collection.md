---
title: Enable or disable usage data collection and crash reporting
description: This article explains how to control if usage and crash reporting data is collected and sent to Microsoft.
ms.prod: azure-data-studio
ms.technology: azure-data-studio
ms.topic: how-to
author: "markingmyname"
ms.author: "maghan"
ms.reviewer: "alayu, maghan, sstein"
ms.custom: seodec18; seo-lt-2019
ms.date: 09/24/2018
---

# Enable or disable usage data collection for Azure Data Studio

## How to disable telemetry reporting

Azure Data Studio collects usage data and sends it to Microsoft to help improve our products and services. To learn more, read the [privacy statement](https://go.microsoft.com/fwlink/?LinkID=528096&clcid=0x409).

If you don't wish to send usage data to Microsoft, you can set the *telemetry.enableTelemetry* setting to *false*.

To silence all telemetry events from Azure Data Studio, from **File** > **Preferences** > **Settings**, add the following option:

```json
    "telemetry.enableTelemetry": false
```

**Important Notice**: This option requires a restart of Azure Data Studio to take effect. 

## How to disable crash reporting

To disable crash reporting, from **File** > **Preferences** > **Settings**, add the following option:

```json
    "telemetry.enableCrashReporter": false
```

**Important Notice**: This option requires a restart of Azure Data Studio to take effect.

## Additional resources
- [Workspace and User settings](settings.md)
