---
title: "Manage all data alerts on a SharePoint site in Data Alert Manager"
description: Learn how to view the data alerts that any site user creates along with information about the alerts. Also, learn how to delete alerts.
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
# Manage all data alerts on a SharePoint site in Data Alert Manager

[!INCLUDE[ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../includes/ssrs-appliesto-not-pbirs.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../includes/ssrs-appliesto-sharepoint-2013-2016.md)]

SharePoint alerting administrators can view a list of the data alerts that any site user creates along with information about the alerts. Alerting administrators can also delete alerts. The following picture shows the features available to alerting administrators in Data Alert Manager.

:::image type="content" source="../reporting-services/media/rs-alertmanagersite.gif" alt-text="Screenshot of the Data Alert Manager showing the features available to alerting administrators.":::

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.

## View a list of alerts created by a site user  
  
1.  Go to the SharePoint site where data alerts definitions are saved.  
  
2.  On the Home page, select **Site Actions**.  
  
3.  Scroll to the bottom of the list and select **Site Settings**.  
  
4.  Under **Reporting Services**, select **Manage Data Alerts**.  
  
5.  Select the down arrow by the **View alerts for user** list and select the user whose alerts you want to view.  
  
6.  Select the down arrow next to the **View alerts for report** list and choose a specific alert to view, or select **Show All** to list all alerts created by the selected user.  
  
  A table lists the following pieces of information:
        - Name
        - Report name
        - Name of the person who created the data alert
        - Number times the data alert was sent
        - The last time the data alert definition was modified
        - Status of the data alert
  
  If the data alert can't be generated or sent, the status column contains information about the error and helps you troubleshoot the problem.  
  
## Delete an alert definition  
  
-   Right-click the data alert that you want to delete and select **Delete**.  
  
    > [!NOTE]  
    >  After you delete the alert, no further alert messages are sent. However, if you query the alerting database you might find that the alert definition still exists. The alerting service performs clean up on a schedule and the alert definition is deleted permanently in the next cleanup. The default cleanup interval is 20 minutes. This and other cleanup intervals are configurable. For more information, see [Reporting Services data alerts](../reporting-services/reporting-services-data-alerts.md).  

## Related content

- [Data Alert Manager for alerting administrators](../reporting-services/data-alert-manager-for-alerting-administrators.md)
- [Reporting Services data alerts](../reporting-services/reporting-services-data-alerts.md)
- [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
