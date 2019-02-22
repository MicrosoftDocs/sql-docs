---
title: "Create a Data Alert in Data Alert Designer | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 8464ab9d-afe1-4490-955f-9f3319bcbf8d
author: markingmyname
ms.author: maghan
manager: kfile
---
# Create a Data Alert in Data Alert Designer
  You create data alert definitions in Data Alert Designer. After you save the alert definitions, you can reopen, edit, and then resave them in Data Alert Designer. For information about editing alert definitions, see [Manage My Data Alerts in Data Alert Manager](manage-my-data-alerts-in-data-alert-manager.md) and [Edit a Data Alert in Alert Designer](edit-a-data-alert-in-alert-designer.md).  
  
### To create a data alert definition  
  
1.  Locate the SharePoint library that contains the report that you want to create a data alert definition for.  
  
2.  Click the report.  
  
     The report runs. If the report is parameterized, verify that the report shows the data that you want to receive alert messages about. If you do not see the columns or values you are interested in, you might want to rerun the report, using different parameter values.  
  
    > [!NOTE]  
    >  The parameter values you chose to run the report are saved in the alert definition and will be used when report is rerun as a step in processing the alert definition. To use different parameter values, you must create a new alert definition.  
  
3.  On the **Actions** menu, click **New Data Alert**.  
  
     The following picture shows the **Actions** menu.  
  
     ![Open Alert Designer from SharePoint library](media/rs-openalertdesigneriw.gif "Open Alert Designer from SharePoint library")  
  
     The Data Alert Designer opens, showing the first 100 rows of the first data feed that the report generates in a table.  
  
    > [!NOTE]  
    >  If you do not see the **New Data Alert** option, the alerting service is not configured on the SharePoint site or the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] edition does not include data alerts. For more information, see [Reporting Services SharePoint Service and Service Applications](../../2014/reporting-services/reporting-services-sharepoint-service-and-service-applications.md).  
    >   
    >  If the **New Data Alert** option is grayed, the report data source is configured to use integrated security credentials or prompt for credentials. To make the **New Data Alert** option available, you must update the data source to use stored credentials or no credentials.  
  
     The name of the data feed appears in **Report data name** drop-down list.  
  
4.  Optionally, select a different data feed in the **Report data name** drop-down list.  
  
     If no data feed is generated from the report, you cannot create an alert definition for the report. The layout of the report determines the content of each data feed. For more information see, [Generating Data Feeds from Reports &#40;Report Builder and SSRS&#41;](report-builder/generating-data-feeds-from-reports-report-builder-and-ssrs.md).  
  
5.  Optionally, in the **Alert name** text box, update the default name to be more meaningful.  
  
     The default name of the alert definition is the name of the report. Alert definition names do not have to be unique, which can make it difficult to tell them apart when you later view the list of your alerts in Data Alert Manager. It is recommended that you use meaningful and unique names for your alert definitions.  
  
6.  Optionally, change the default data option from **any data in the data feed has** to **no data in the data feed has**.  
  
7.  Click **Add rule**.  
  
     A list of the columns in the data feed appears.  
  
8.  In the list, select the column that you want to use in the rule, and then select a comparison operator and enter the threshold value.  
  
     Depending on the data type of the selected column, different comparison operators are listed. If the column has a date data type, a calendar icon displays next to threshold value for the rule. You can enter a data by clicking a date in the calendar or typing the date.  
  
     Data Alert Designer provides two comparison modes: **Value Entry Mode** and **Field Selection Mode**. The default mode is **Value Entry Mode**. You can add OR clauses only when you are in **Value Entry Mode** and are using the **is** comparison.  
  
9. To add an OR clause, click the down-arrow, and then click **Value Entry Mode**.  
  
10. Type the comparison value.  
  
11. Optionally, click the ellipsis **(...)** again.  
  
     The ellipsis **(...)** appears on the line that contains the first clause.  
  
     An OR clause is added below and within the AND rule.  
  
12. Optionally, click the down-arrow, select **Field Selection Mode**, and then select a column in the list.  
  
     You will notice that the ellipsis **(...)** that you click to add OR clauses has disappeared.  
  
13. Optionally, click **Add rule** again to add additional rules.  
  
     The rules are combined by using the AND logical operator.  
  
14. Select an option in the recurrence list. Depending on the type of recurrence, enter an interval.  
  
15. Optionally, click **Advanced**.  
  
16. Optionally, change the date that the alert message starts on by typing a different date or opening the calendar, and then clicking a date in the calendar.  
  
     The default start date is the current date.  
  
17. Optionally, select the checkbox located next to **Stop alert on**, and then choose a date to stop the alert message.  
  
     By default, an alert message has no stop date.  
  
    > [!NOTE]  
    >  Stopping an alert message does not delete the alert definition. After you stop an alert message, you can restart it by updating the start and stop dates. For information about deleting alert definitions, see [Manage My Data Alerts in Data Alert Manager](manage-my-data-alerts-in-data-alert-manager.md).  
  
18. Optionally, clear the **Send message only if results change** checkbox.  
  
     If you send alert messages frequently, redundant information might not be welcome and you should not clear this checkbox.  
  
19. Enter the email addresses of alert message recipients. Separate addresses with semicolons.  
  
     If the email address of the person who created the alert definition is available, it is added to the **Recipient(s)** box.  
  
20. Optionally, in the **Subject** text box, update the Subject line of the alert message.  
  
     The default Subject is **Data alert for \<data alert name>**.  
  
21. Optionally, in the **Description** text box, type a description of the alert message.  
  
22. Click **Save**.  
  
## See Also  
 [Data Alert Designer](../../2014/reporting-services/data-alert-designer.md)   
 [Data Alert Manager for Alerting Administrators](../../2014/reporting-services/data-alert-manager-for-alerting-administrators.md)   
 [Reporting Services Data Alerts](../ssms/agent/alerts.md)  
  
  
