---
title: Enable or disable usage data collection, and crash reporting for SQL Operations Studio (preview) | Microsoft Docs
description: This article explains how to control if usage and crash reporting data is collected and sent to Microsoft.
ms.custom: "tools|sos"
ms.date: "11/15/2017"
ms.prod: "sql-non-specified"
ms.reviewer: "alayu; erickang; sstein"
ms.suite: "sql"
ms.prod_service: sql-tools
ms.component: sos
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "stevestein"
ms.author: "sstein"
manager: craigg
ms.workload: "Inactive"
---
# Enable or disable usage data collection for [!INCLUDE[name-sos](../includes/name-sos-short.md)]

## How to disable telemetry reporting

[!INCLUDE[name-sos](../includes/name-sos-short.md)] collects usage data and sends it to Microsoft to help improve our products and services. To learn more, read the [privacy statement](https://go.microsoft.com/fwlink/?LinkID=528096&clcid=0x409).

If you don’t wish to send usage data to Microsoft, you can set the *telemetry.enableTelemetry* setting to *false*.

To silence all telemetry events from [!INCLUDE[name-sos](../includes/name-sos-short.md)], from **File** > **Preferences** > **Settings**, add the following option:

```json
    "telemetry.enableTelemetry": false
```

**Important Notice**: This option requires a restart of [!INCLUDE[name-sos](../includes/name-sos-short.md)] to take effect. 

## How to disable crash reporting

To disable crash reporting, from **File** > **Preferences** > **Settings**, add the following option:

```json
    "telemetry.enableCrashReporter": false
```

**Important Notice**: This option requires a restart of [!INCLUDE[name-sos](../includes/name-sos-short.md)] to take effect.

## Additional resources
- [Workspace and User settings](settings.md)