---
title: "Manage My Data Alerts in Data Alert Manager"
description: Learn how to view a list of the data alerts that they created and information about the alerts in Data Alert Manager.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "managing, alerts"
  - "managing, data alerts"
monikerRange: ">=sql-server-2016 <=sql-server-2016"
---
# Manage My Data Alerts in Data Alert Manager

[!INCLUDE[ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../includes/ssrs-appliesto-not-pbirs.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../includes/ssrs-appliesto-sharepoint-2013-2016.md)]

SharePoint users can view a list of the data alerts that they created and information about the alerts. Users can also delete their alerts, open alert definitions for edit in Data Alert Designer, and run their alerts. The following picture shows the features available to users in Data Alert Manager.

:::image type="content" source="../reporting-services/media/rs-alertmanageriw.gif" alt-text="Screenshot of the features available to users in Data Alert Manager.":::

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.

### View a list of your alerts  
  
1.  Go to the SharePoint library where you saved the reports on which you created data alerts.  
  
2.  Select the icon for the expand dropdown menu on a report and select **Manage Data Alerts**. The following picture shows the dropdown menu.  

    :::image type="content" source="../reporting-services/media/rs-openalertmanager.gif" alt-text="Screenshot of the dropdown menu highlighting Manage Data Alerts.":::
  
     Data Alert Manager opens. By default, it lists the alerts for the report that you selected in the library.  
  
3.  Select the down arrow next to the **View alerts for report** list and select a report to view its alerts, or select **Show All** to list all alerts.  
  
    > [!NOTE]  
    >  If the report that you selected does not have any alerts, you do not have to return to the SharePoint library to locate and select a report that hasalerts. Instead, select **Show All** to see a list of all your alerts.  
  
     A table lists the alert name, report name, your name as the creator of the alert, the number the alert was sent, the last time the alert definition was modified, and the status of the alert. If the alert can't be generated or sent, the status column contains information about the error and helps you troubleshoot the problem.  
  
### Edit an alert definition  
  
-   Right-click the data alert for which you want to edit the alert definition and select **Edit**.  
  
     The alert definition opens in Data Alert Designer. For more information, see [Edit a data alert in Alert Designer](../reporting-services/edit-a-data-alert-in-alert-designer.md) and [Data Alert Designer](../reporting-services/data-alert-designer.md).  
  
    > [!NOTE]  
    >  Only the user that created the data alert definition can edit it.  
  
    > [!NOTE]  
    >  If the report has changed and the data feeds generated from the report have changed the alert definition might no longer be valid. This occurs when a column that the alert references in its rules is deleted from the report, changes data type, or is included in a different data feed or the report is deleted or moved. You can open an alert definition that is not valid, but you cannot resave it until it is valid based on the current version of the report data feed that it is built upon. To learn more about how data feeds are generated from reports, see [Generate data feeds from reports &#40;Report Builder and SSRS&#41;](../reporting-services/report-builder/generating-data-feeds-from-reports-report-builder-and-ssrs.md).  
  
### Delete an alert definition  
  
-   Right-click the data alert that you want to delete and select **Delete**.  
  
     When you delete the alert, no further alert messages are sent.  
  
### Run an alert  
  
-   Right-click the data alert that you want to run and select **Run**.  
  
     The alert instance is created and the data alert message is immediately sent, regardless of the schedule options you specified in Data Alert Designer. For example, an alert configured to be sent weekly and then only if the results change is sent.  

## Related content

- [Data Alert Manager for alerting administrators](../reporting-services/data-alert-manager-for-alerting-administrators.md)
- [Reporting Services data alerts](../reporting-services/reporting-services-data-alerts.md)
- [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
