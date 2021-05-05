---
title: Enable or disable usage data collection and crash reporting
description: This article explains how to control if usage and crash reporting data is collected and sent to Microsoft.
ms.prod: azure-data-studio
ms.technology: azure-data-studio
ms.topic: how-to
author: "markingmyname"
ms.author: "maghan"
ms.reviewer: "alayu"
ms.custom: seo-lt-2019
ms.date: 05/05/2021
---

# Enable or disable usage data collection for Azure Data Studio

Azure Data Studio contains Internet-enabled features that can collect and send anonymous feature usage and diagnostic data to Microsoft.

Azure Data Studio may collect standard computer, use, and performance information that may be transmitted to Microsoft and analyzed to improve the quality, security, and reliability of Azure Data Studio.

Azure Data Studio doesn't collect your name, address, or other data related to an identified or identifiable individual.

For details, see the [Microsoft Privacy Statement](https://privacy.microsoft.com/privacystatement), and [SQL Server Privacy supplement](../sql-server/sql-server-privacy.md).

## Audit feature usage and diagnostic data

To see feature usage data that is collected by Azure Data Studio, follow the steps below:

1. Launch Azure Data Studio.
2. Open the command palette and choose the **Developer: Set Log Level...** command
3. Select **Trace** from the options
4. Open the Output panel (Ctrl+Shift+U)
5. Select **Log (Telemetry)** from the dropdown
6. Select **View**, then Select **Output** in the main menu to show the **Output** window. 
7. When the **Output** window is visible, choose **Log (Window)** in the **Show output from:** menu.

When tracing telemetry events, the events are also logged to a local file telemetry.log, which you can view using the **Developer: Open Log File...** command and choosing **Telemetry** from the dropdown.

## Disable telemetry reporting

To not send usage data to Microsoft, you can set the `telemetry.enableTelemetry` [user settings](settings.md) to `false`.

Go to **File** > **Preferences** > **Settings** (macOS: **Code** > **Preferences** > **Settings**) and search for `telemetry`, and uncheck the **Telemetry: Enable Telemetry** setting. With this setting, you silence all telemetry events from Azure Data Studio going forward. Telemetry information may have been collected and sent up until the point when you disable the setting.

:::image type="content" source="media/usage-data-collection/disable-telemetry.png" alt-text="disable telemetry.":::

If you use the JSON editor for your settings, add the following line:

```json
    "telemetry.enableTelemetry": false
```

## Disable crash reporting

Azure Data Studio collects data about any crashes that occur and sends it to Microsoft to help improve our products and services.

If you don't want to send, crash data to Microsoft, you can change the `enable-crash-reporter` runtime argument to `false`.

1. Open the Command Palette (`kb(workbench.action.showCommands)`).

2. Run the **Preferences: Configure Runtime Arguments** command.

3. This command opens a `argv.json` file to configure runtime arguments.

4. Edit `"enable-crash-reporter": false`.

5. Restart Azure Data Studio.

## Extensions and telemetry

Azure Data Studio lets you add features to the product by installing Microsoft and third-party extensions. These extensions may be collecting their own usage data and are not controlled by the `telemetry.enableTelemetry` setting. Consult the specific extension's documentation to learn about its telemetry reporting and whether it can be disabled.

## GDPR and Azure Data Studio

In addition to supporting the General Data Protection Regulation (GDPR), the Azure Data Studio team takes privacy seriously. That's both for Microsoft as a company and specifically within the Azure Data Studio team.

To ensure GDPR compliance, we made several updates to Azure Data Studio.

- Making it more accessible to opt-out of telemetry collection by placing a notification in the product for all existing and new users.
- Reviewing and classifying the telemetry that we send.
- Ensuring that we have valid data retention policies in place for any data we collect, for example, crash dumps.

In short, we've worked hard to do the right thing for all users, as these practices apply to all geographies, not just Europe.

One question we expect people to ask is to see the data we collect. However, we don't have a reliable way to do this as Azure Data Studio doesn't have a *sign-in* experience that uniquely identifies a user. We send information that helps us approximate a single user for diagnostic purposes (this is based on a hash of the network adapter NIC), but this isn't guaranteed to be unique.  For example, virtual machines (VMs) often rotate NIC IDs or allocate from a pool. This technique is sufficient to help us when working through problems, but it's not reliable enough for us to 'provide your data'.

We expect our approach to evolve as we learn more about GDPR and the expectations of our users. We greatly appreciate the data users send to us, as it's valuable, and Azure Data Studio is a better product for everyone. And again, if you're worried about privacy, we offer the ability to disable sending telemetry as described in disable telemetry reporting.

For general information about GDPR, see the [GDPR section of the Service Trust portal](https://servicetrust.microsoft.com/ViewPage/GDPRGetStarted).

## More resources
- [Workspace and User settings](settings.md)

## See also

- [Configure usage and diagnostic data collection for SQL Server](../sql-server/usage-and-diagnostic-data-configuration-for-sql-server.md)
- [Local audit for SQL Server usage and diagnostic data collection](../sql-server/usage-and-diagnostic-data-in-local-audit.md)
