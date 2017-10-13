---
title: Enable or disable usage data collection for Carbon) | Microsoft Docs
description: This article explains how usage data is collected and sent to Microsoft.
author: stevestein
ms.author: sstein
manager: craigg
ms.reviewer: achatter, alayu, erickang, sanagama, sstein
ms.service: data-tools
ms.workload: data-tools
ms.prod: NEEDED
ms.custom: mvc
ms.topic: article
ms.date: 10/11/2017
---
# Enable or disable usage data collection for Carbon


ADD NOTE THAT THIS IS NOT EDITABLE FOR PREVIEW??

## How to disable telemetry reporting

Carbon collects usage data and sends it to Microsoft to help improve our products and services.  Read our [privacy statement](https://go.microsoft.com/fwlink/?LinkID=528096&clcid=0x409) to learn more.

If you don’t wish to send usage data to Microsoft, you can set the `telemetry.enableTelemetry` setting to `false`.

From **File** > **Preferences** > **Settings**, add the following option to disable telemetry reporting, this will silence all telemetry events from the VS Code shell.

```json
    "telemetry.enableTelemetry": false
```

**Important Notice**: This option requires a restart of Carbon to take effect. 
