---
title: "Create, modify, and delete data-driven subscriptions"
description: In this article, learn how to create, modify, and delete a data-driven subscription. Also learn tips on defining a query to retrieve subscription information.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/12/2019
ms.service: reporting-services
ms.subservice: subscriptions
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "query-based subscriptions [Reporting Services]"
  - "queries [Reporting Services], data-driven subscriptions"
  - "subscriptions [Reporting Services], data-driven"
  - "data-driven subscriptions"
---
# Create, modify, and delete data-driven subscriptions
  A data-driven subscription is a query-based subscription that gets the data values used for processing the subscription at run time. When the subscription is triggered, a query is processed to get up-to-date information about recipients, report delivery options, rendering formats, and parameter settings. The query results are combined with the subscription definition to create a dynamic subscription. This subscription uses data you already maintain in an employee database, a customer database, or any other database. The database contains information that can be used as subscriber data.  
  
 To create a new data-driven subscription or modify an existing subscription, use the **Manage** > **Subscriptions** page in the web portal. The **Subscriptions** page walks you through each step of creating or modifying a subscription. To access a subscription, use the **My Subscriptions** page or the Subscriptions list of a report. To learn how to create a data-driven subscription, see [Create a Data-Driven Subscription &#40;SSRS Tutorial&#41;](../../reporting-services/create-a-data-driven-subscription-ssrs-tutorial.md).  
  
 In this article:  
  
-   [Manage and delete a data-driven subscription](#bkmk_manage_and_delete)  
  
-   [Create and modify a data-driven subscription](#bkmk_create_and_modify)  
  
-   [Define a query that retrieves subscription information](#bkmk_define_query)  
  
-   [Run the subscription](#bkmk_run_subscription)  
  
##  <a name="bkmk_manage_and_delete"></a> Manage and delete a data-driven subscription  
 A data-driven subscription that is in progress can't be stopped or deleted via the web portal. For this reason, it's advantageous to use a shared schedule to trigger data-driven subscription. That way, if you want to temporarily prevent a subscription from processing, you can pause the schedule that triggers the subscription. For more information, see [Create and manage subscriptions for native mode report servers](../../reporting-services/subscriptions/create-and-manage-subscriptions-for-native-mode-report-servers.md).  
  
 To delete a data-driven subscription, select the checkbox next to the report on the **Subscriptions** page, and then choose **Delete**.  
  
 For instructions on how to cancel a data-driven subscription, see [Manage a Running Process](../../reporting-services/subscriptions/manage-a-running-process.md).  
  
##  <a name="bkmk_create_and_modify"></a> Create and modify a data-driven subscription  
 To create a data-driven subscription, select a report that uses stored credentials or no credentials. When you create the data-driven subscription, you might decide to use a naming convention for the description field so you can easily differentiate standard subscriptions from data-driven subscriptions.  
  
### Create a data-driven subscription (native mode)  
  
1. In the web portal, navigate to the folder containing the report, right-click the report, and select **Manage** from the menu.  
  
2. Select the **Subscriptions** tab.  
  
3. Select **+ New subscription** on the **Subscriptions** page.  
  
### Create a data-driven subscription (SharePoint mode)  
  
1. In the SharePoint document library, hover over the report, open the options menu and select **Manage Subscriptions**.  
  
2. Select **Add Data-Driven Subscription**.  
  
### Modify a data-driven subscription (native mode)  
  
1. In the web portal, navigate to the folder containing the report, right-click the report, and select **Manage** from the menu.  
  
2. Select the **Subscriptions** tab.  
  
3. Select the checkbox next to the subscription you want to modify, and choose **Edit**. Data-driven subscriptions have the value "Data-driven" in the **Type** column.  
  
### Modify an existing data-driven subscription (SharePoint mode)  
  
1.  In the SharePoint document library, hover over the report, open the options menu and select **Manage Subscriptions**.  
  
2.  Select the subscription you want to modify.  
  
    > [!NOTE]  
    > You can modify any value that is already specified. All values are presented as they were first created, except for the password that is used to access the subscriber data store. You must reenter the password every time you modify values on the second page or any subsequent page.  
  
  Before you can create a data-driven subscription, ensure that you satisfy the following requirements:  
  
-   **Report requirements**. The report must use stored credentials or no credentials to retrieve data at run time. You can't subscribe to a report that uses impersonated or delegated credentials to connect to an external data source. The credentials of the user who creates or owns the subscription isn't available when the subscription is processed. The stored credentials can be a Windows account or a database user account. For more information, see [Specify credential and connection information for report data sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md).  
  
     You can't subscribe to a Report Builder report that uses a model as a data source and the model contains model item security settings. Only reports that use model item security are included in this restriction.  
  
     You can't create a data-driven subscription on a report that contains the `User!UserID` expression.  
  
-   **Data requirements**. You must have an accessible external data source that contains subscriber data.  
  
-   **User requirements**. The author of the subscription must have permission to "Manage reports" and "Manage all subscriptions." For more information about item-level task permissions, see [Tasks and permissions](../../reporting-services/security/tasks-and-permissions.md). The author must also have the necessary credentials to access the external data source that contains subscriber data.  
  
##  <a name="bkmk_define_query"></a> Define a query that retrieves subscription information  
 A data-driven subscription must specify a query or command that retrieves subscriber data. The query should produce one row for each subscriber. If you use the e-mail delivery extension, the query should return a valid e-mail alias for each subscriber. The number of deliveries that are made is based on the number of rows returned by the query. If the row set consists of 10,000 rows, the subscription delivers 10,000 reports.  
  
 If executing the query is time-consuming, you can increase the time-out value to accommodate other processing.  
  
 For this step, the query must be validated before you continue. Validation doesn't process the query, but it does return a list of all columns that are in the row set so that you can reference the columns in subsequent selections. If the query fails to validate, you can't continue. A query fails to validate if the query syntax is incorrect or if the connection to the data source isn't valid. Use the **Back** button to make corrections to the data source.  
  
##  <a name="bkmk_run_subscription"></a> Run the subscription  
 You must specify conditions for processing the subscription. You can specify a schedule, or you can trigger the subscription to coincide with updates to a report execution snapshot. Processing for data-driven subscriptions is the same as processing for standard subscriptions.  
  
## Related content  
 [Create and manage subscriptions for native mode report servers](../../reporting-services/subscriptions/create-and-manage-subscriptions-for-native-mode-report-servers.md)   
 [Subscriptions and delivery &#40;Reporting Services&#41;](../../reporting-services/subscriptions/subscriptions-and-delivery-reporting-services.md)   
 [The web portal of a report server (SSRS native mode)](../../reporting-services/web-portal-ssrs-native-mode.md)   
 [Create and manage subscriptions for native mode report servers](create-and-manage-subscriptions-for-native-mode-report-servers.md)   
 [Work with subscriptions (web portal)](../../reporting-services/working-with-subscriptions-web-portal.md)
 [Use my subscriptions (native mode report server)](../../reporting-services/subscriptions/use-my-subscriptions-native-mode-report-server.md)  
 
