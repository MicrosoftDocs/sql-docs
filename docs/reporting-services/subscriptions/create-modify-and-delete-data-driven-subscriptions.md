---
title: "Create, Modify, and Delete Data-Driven Subscriptions | Microsoft Docs"
description: In this article, learn how to create, modify, and delete a data-driven subscription. Also learn tips on defining a query to retrieve subscription information.
ms.date: 06/12/2019
ms.service: reporting-services
ms.subservice: subscriptions


ms.topic: conceptual
helpviewer_keywords: 
  - "query-based subscriptions [Reporting Services]"
  - "queries [Reporting Services], data-driven subscriptions"
  - "subscriptions [Reporting Services], data-driven"
  - "data-driven subscriptions"
ms.assetid: 0ba2093e-9393-4eb6-af06-9da10988cfaf
author: maggiesMSFT
ms.author: maggies
---
# Create, Modify, and Delete Data-Driven Subscriptions
  A data-driven subscription is a query-based subscription that gets the data values used for processing the subscription at run time. When the subscription is triggered, a query is processed to get up-to-date information about recipients, report delivery options, rendering formats, and parameter settings. The query results are combined with the subscription definition to create a dynamic subscription that uses data you already maintain in an employee database, a customer database, or any other database that contains information that can be used as subscriber data.  
  
 To create a new data-driven subscription or modify an existing subscription, use the **Manage** > **Subscriptions** page in the web portal. The **Subscriptions** page walks you through each step of creating or modifying a subscription. To access a subscription after it is created, use the **My Subscriptions** page or the Subscriptions list of a report. To learn how to create a data-driven subscription, see [Create a Data-Driven Subscription &#40;SSRS Tutorial&#41;](../../reporting-services/create-a-data-driven-subscription-ssrs-tutorial.md).  
  
 In this article:  
  
-   [Managing and deleting a data-driven subscription](#bkmk_manage_and_delete)  
  
-   [Creating and modifying a data-driven subscription](#bkmk_create_and_modify)  
  
-   [Defining a query that retrieves subscription information](#bkmk_define_query)  
  
-   [Running the subscription](#bkmk_run_subscription)  
  
##  <a name="bkmk_manage_and_delete"></a> Managing and deleting a data-driven subscription  
 A data-driven subscription that is in progress cannot be stopped or deleted via the web portal. For this reason, it is advantageous to use a shared schedule to trigger data-driven subscription. That way, if you want to temporarily prevent a subscription from processing, you can pause the schedule that triggers the subscription. For more information, see [Create and Manage Subscriptions for Native Mode Report Servers](../../reporting-services/subscriptions/create-and-manage-subscriptions-for-native-mode-report-servers.md).  
  
 To delete a data-driven subscription, select the checkbox next to the report on the **Subscriptions** page, and then select **Delete**.  
  
 For instructions on how to cancel a data-driven subscription, see [Manage a Running Process](../../reporting-services/subscriptions/manage-a-running-process.md).  
  
##  <a name="bkmk_create_and_modify"></a> Creating and modifying a data-driven subscription  
 To create a data-driven subscription, select a report that uses stored credentials or no credentials. When you create the data-driven subscription, consider using a naming convention for the description field so you can easily differentiate standard subscriptions from data-driven subscriptions.  
  
### To create a data-driven subscription (Native Mode)  
  
1. In the web portal, navigate to the folder containing the report, right-click the report, and select **Manage** from the dropdown menu.  
  
2. Select the **Subscriptions** tab.  
  
3. Select **+ New subscription** on the **Subscriptions** page.  
  
### To create a data-driven subscription (SharePoint Mode)  
  
1. In the SharePoint document library, hover over the report, open the options menu and Click **Manage Subscriptions**.  
  
2. Click **Add Data-Driven Subscription**.  
  
### To modify an existing data-driven subscription (Native Mode)  
  
1. In the web portal, navigate to the folder containing the report, right-click the report, and select **Manage** from the dropdown menu.  
  
2. Select the **Subscriptions** tab.  
  
3. Select the checkbox next to the subscription you want to modify, and select **Edit**. Data-driven subscriptions will have the value "Data-driven" in the **Type** column.  
  
### To modify an existing data-driven subscription (SharePoint Mode)  
  
1.  In the SharePoint document library, hover over the report, open the options menu and Click **Manage Subscriptions**.  
  
2.  Select the subscription you want to modify.  
  
    > [!NOTE]  
    > You can modify any value that is already specified. All values are presented as they were first created, except for the password that is used to access the subscriber data store. You must retype the password every time you modify values on the second page or any subsequent page.  
  
  Before you can create a data-driven subscription, ensure that you satisfy the following requirements:  
  
-   **Report requirements**. The report must use stored credentials or no credentials to retrieve data at run time. You cannot subscribe to a report that uses impersonated or delegated credentials to connect to an external data source; the credentials of the user who creates or owns the subscription will not be available when the subscription is processed. The stored credentials can be a Windows account or a database user account. For more information, see [Specify Credential and Connection Information for Report Data Sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md).  
  
     You cannot subscribe to a Report Builder report that uses a model as a data source and the model contains model item security settings. Only reports that use model item security are included in this restriction.  
  
     You cannot create a data-driven subscription on a report that contains the `User!UserID` expression.  
  
-   **Data requirements**. You must have an accessible external data source that contains subscriber data.  
  
-   **User requirements**. The author of the subscription must have permission to "Manage reports" and "Manage all subscriptions." For more information about item-level task permissions, see [Tasks and Permissions](../../reporting-services/security/tasks-and-permissions.md). The author must also have the necessary credentials to access the external data source that contains subscriber data.  
  
##  <a name="bkmk_define_query"></a> Defining a query that retrieves subscription information  
 A data-driven subscription must specify a query or command that retrieves subscriber data. The query should produce one row for each subscriber. If you are using the e-mail delivery extension, the query should return a valid e-mail alias for each subscriber. The number of deliveries that are made is based on the number of rows returned by the query. If the row set consists of 10,000 rows, the subscription delivers 10,000 reports.  
  
 If executing the query is time-consuming, you can increase the time-out value to accommodate additional processing.  
  
 For this step, the query must be validated before you continue. Validation does not process the query, but it does return a list of all columns that are in the row set so that you can reference the columns in subsequent selections. If the query fails to validate, you cannot continue. A query fails to validate if the query syntax is incorrect or if the connection to the data source is not valid. Use the **Back** button to make corrections to the data source.  
  
##  <a name="bkmk_run_subscription"></a> Running the subscription  
 You must specify conditions for processing the subscription. You can specify a schedule, or you can trigger the subscription to coincide with updates to a report execution snapshot. Processing for data-driven subscriptions is the same as processing for standard subscriptions.  
  
## See also  
 [Create and Manage Subscriptions for Native Mode Report Servers](../../reporting-services/subscriptions/create-and-manage-subscriptions-for-native-mode-report-servers.md)   
 [Subscriptions and Delivery &#40;Reporting Services&#41;](../../reporting-services/subscriptions/subscriptions-and-delivery-reporting-services.md)   
 [The web portal of a report server (SSRS Native Mode)](../../reporting-services/web-portal-ssrs-native-mode.md)   
 [Create and Manage Subscriptions for Native Mode Report Servers](create-and-manage-subscriptions-for-native-mode-report-servers.md)   
 [Working with subscriptions (web portal)](../../reporting-services/working-with-subscriptions-web-portal.md)
 [Use My Subscriptions (Native Mode Report Server)](../../reporting-services/subscriptions/use-my-subscriptions-native-mode-report-server.md)  
 