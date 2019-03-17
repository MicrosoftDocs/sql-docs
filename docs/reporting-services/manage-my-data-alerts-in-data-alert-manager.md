---
title: "Manage My Data Alerts in Data Alert Manager | Microsoft Docs"
ms.date: 08/17/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: reporting-services


ms.topic: conceptual
helpviewer_keywords: 
  - "managing, alerts"
  - "managing, data alerts"
ms.assetid: e0e4ffdf-bd4c-4ebd-872b-07486cbb47c2
author: markingmyname
ms.author: maghan
monikerRange: ">=sql-server-2016 <=sql-server-2016||=sqlallproducts-allversions"
---
# Manage My Data Alerts in Data Alert Manager

[!INCLUDE[ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../includes/ssrs-appliesto-not-pbirs.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../includes/ssrs-appliesto-sharepoint-2013-2016.md)]

SharePoint users can view a list of the data alerts that they created and information about the alerts. Users can also delete their alerts, open alert definitions for edit in Data Alert Designer, and run their alerts. The following picture shows the features available to users in Data Alert Manager.

 ![Alert Manager features for SharePoint users](../reporting-services/media/rs-alertmanageriw.gif "Alert Manager features for SharePoint users")

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.

### To view a list of your alerts  
  
1.  Go to the SharePoint library where you saved the reports on which you created data alerts.  
  
2.  Click the icon for the expand drop-down menu on a report and click **Manage Data Alerts**. The following picture shows the drop-down menu.  
  
     ![Open Alert Manager from report context menu](../reporting-services/media/rs-openalertmanager.gif "Open Alert Manager from report context menu")  
  
     Data Alert Manager opens. By default, it lists the alerts for the report that you selected in the library.  
  
3.  Click the down arrow next to the **View alerts for report** list and select a report to view its alerts, or click **Show All** to list all alerts.  
  
    > [!NOTE]  
    >  If the report that you selected does not have any alerts, you do not have to return to the SharePoint library to locate and select a report that hasalerts. Instead, click **Show All** to see a list of all your alerts.  
  
     A table lists the alert name, report name, your name as the creator of the alert, the number the alert was sent, the last time the alert definition was modified, and the status of the alert. If the alert cannot be generated or sent, the status column contains information about the error and helps you troubleshoot the problem.  
  
### To edit an alert definition  
  
-   Right-click the data alert for which you want to edit the alert definition and click **Edit**.  
  
     The alert definition opens in Data Alert Designer. For more information, see [Edit a Data Alert in Alert Designer](../reporting-services/edit-a-data-alert-in-alert-designer.md) and [Data Alert Designer](../reporting-services/data-alert-designer.md).  
  
    > [!NOTE]  
    >  Only the user that created the data alert definition can edit it.  
  
    > [!NOTE]  
    >  If the report has changed and the data feeds generated from the report have changed the alert definition might no longer be valid. This occurs when a column that the alert references in its rules is deleted from the report, changes data type, or is included in a different data feed or the report is deleted or moved. You can open an alert definition that is not valid, but you cannot resave it until it is valid based on the current version of the report data feed that it is built upon. To learn more about how data feeds are generated from reports, see [Generating Data Feeds from Reports &#40;Report Builder and SSRS&#41;](../reporting-services/report-builder/generating-data-feeds-from-reports-report-builder-and-ssrs.md).  
  
### To delete an alert definition  
  
-   Right-click the data alert that you want to delete and click **Delete**.  
  
     When you delete the alert, no further alert messages are sent.  
  
### To run an alert  
  
-   Right-click the data alert that you want to run and click **Run**.  
  
     The alert instance is created and the data alert message is immediately sent, regardless of the schedule options you specified in Data Alert Designer. For example, an alert configured to be sent weekly and then only if the results change is sent.  

## See Also

[Data Alert Manager for Alerting Administrators](../reporting-services/data-alert-manager-for-alerting-administrators.md)   
[Reporting Services Data Alerts](../reporting-services/reporting-services-data-alerts.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
