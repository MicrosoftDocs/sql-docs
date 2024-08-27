---
title: "Set up a SQL Server database alert (Windows)"
description: Learn how to create an alert that is raised when a Performance Monitor counter reaches a threshold value. In response, Performance Monitor can launch an application.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 06/13/2024
ms.service: sql
ms.subservice: performance
ms.topic: how-to
helpviewer_keywords:
  - "alerts [SQL Server], creating"
---
# Set up a SQL Server database alert (Windows)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

You can use Performance Monitor to create an alert that is raised when a Performance Monitor counter reaches a threshold value. In response to the alert, Performance Monitor can launch an application, such as a custom application written to handle the alert condition. For example, you can create an alert that is raised when the number of deadlocks exceeds a specific value.

Alerts also can be defined by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent. For more information, see [Alerts](../../ssms/agent/alerts.md).

## Create a data collector set for a performance counter alert

1. Open Performance Monitor, and expand **Data Collector Sets**.

1. Right-click **User Defined**, and select **New** > **Data Collector Set**.

1. Give the new set a custom name, select the **Create manually (Advanced)** radio button, and select **Next**.

1. Select the **Performance Counter Alert** radio button, then select **Next**.

1. On the **Which performance counters would you like to monitor?** page, select **Add** to add a counter to the alert.

1. Select a counter from the **Available counters** list.

1. To add the counter to the alert, select **Add**. You can continue to add counters, or you can select **OK** to return to the dialog box for the new alert.

1. In the new alert dialog box, select either **Over** or **Under** in the **Alert when** list. Then enter a threshold value in **Limit**.

   The alert is generated when the value for the counter is more than or less than the threshold value (depending on whether you selected **Over** or **Under**).

1. (Optional) Choose the account you want to run this alert as, using the **Run as** option.

1. Select the **Open properties for this data collector set** radio button, and choose **Finish**.

1. On the **Schedule** tab, set the start and stop schedule for the alert scan.

## Set up a SQL Server database alert with the data collector

To modify the actions you can perform against an alert, you must open the data collector that was created in your new data collector set.

1. Navigate to the new data collector set you created, and double-click the data collector object in the detail pane. In this example, the name is `DataCollector01`, and it has a Type of `Alert`.

1. On the **Alert Task** tab, set actions to occur every time the alert is triggered.

1. On the **Alert Action** tab, check the **Log an entry in the application event log** box.

1. On the **Alerts** tab, you can set the sampling frequency with the **Sample interval** and **Units** boxes.

## Related content

- [Create a SQL Server database alert](../performance-monitor/create-a-sql-server-database-alert.md)
