---
title: Enable or disable usage data collection and crash reporting
description: This article explains how to control if usage and crash reporting data is collected and sent to Microsoft.
ms.prod: azure-data-studio
ms.technology: azure-data-studio
ms.topic: how-to
author: "markingmyname"
ms.author: "maghan"
ms.reviewer: "alayu, maghan"
ms.custom: seo-lt-2019
ms.date: 04/30/2021
---

# Enable or disable usage data collection for Azure Data Studio

Azure Data Studio contains Internet-enabled features that can collect and send anonymous feature usage and diagnostic data to Microsoft.

Azure Data Studio may collect standard computer, use, and performance information that may be transmitted to Microsoft and analyzed for purposes of improving the quality, security, and reliability of Azure Data Studio. We don't collect your name, address, or any other data that relates to an identified or identifiable individual.

For details, see the [Microsoft Privacy Statement](https://privacy.microsoft.com/privacystatement), and [SQL Server Privacy supplement](../sql-server/sql-server-privacy.md).

## Audit feature usage and diagnostic data

To see feature usage data that is collected by Azure Data Studio, follow the steps below:

1.	Launch Azure Data Studio.
2.	Select **View**, then Select **Output** in the main menu to show the **Output** window. 
3.	When the **Output** window is visible, choose **Log (Window)** in the **Show output from:** menu.

While you use Azure Data Studio to interact with your databases, the **Output** window shows the data that is collected.

## How to disable system-generated logs

If you don't wish to send usage data to Microsoft, you can set the *telemetry.enableTelemetry* setting to *false*.

You can silence all system-generated logs from Azure Data Studio, from **File** > **Preferences** > **Settings**, add the following option:

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

## More resources
- [Workspace and User settings](settings.md)

## See also

- [Configure usage and diagnostic data collection for SQL Server](../sql-server/usage-and-diagnostic-data-configuration-for-sql-server.md)
- [Local audit for SQL Server usage and diagnostic data collection](../sql-server/usage-and-diagnostic-data-in-local-audit.md)
