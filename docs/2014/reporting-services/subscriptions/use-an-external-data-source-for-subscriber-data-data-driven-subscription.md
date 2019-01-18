---
title: "Use an External Data Source for Subscriber Data (Data-Driven Subscription) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "subscriber data sources [Reporting Services]"
  - "subscriptions [Reporting Services], external data sources"
  - "query-based subscriptions [Reporting Services]"
  - "external data sources [Reporting Services]"
  - "data-driven subscriptions"
  - "data sources [Reporting Services], subscriptions"
ms.assetid: 1cade8ec-729c-4df8-a428-e75c9ad86369
author: markingmyname
ms.author: maghan
manager: craigg
---
# Use an External Data Source for Subscriber Data (Data-Driven Subscription)
  In a data-driven subscription, dynamic subscription data is provided by a query or command that retrieves data from an external data source. Subscription data can be retrieved from any supported data source that meets the requirements for data-driven subscription processing. The query or command syntax must be valid for a data processing extension installed with your report server.  
  
## Data Processing Requirements  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] uses data processing extensions to retrieve subscription data. Recommended data source types include the following:  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational databases  
  
-   Oracle databases  
  
-   [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] multidimensional and data mining data sources  
  
-   XML data sources  
  
     When using the XML data processing extension for subscriber data, be sure to increase the query timeout settings in the subscription. The XML data processing extension uses milliseconds rather than seconds for query timeout values. If you do not increase the timeout value, the subscription might fail due to insufficient processing time.  
  
     Avoid using the **Credentials are not required** option when configuring the connection to the subscriber data source. Stored credentials are recommended when using the XML data processing extension to retrieve subscription data at run time.  
  
 You might be able to use other supported data source types, but not all of them are guaranteed to work. For example, the following data source types cannot be used for subscriber data:  
  
-   SAP Netweaver BI databases  
  
-   report models  
  
 If you have a custom data processing extension that you want to use in data-driven subscriptions, it must implement the <xref:Microsoft.ReportingServices.DataProcessing.IDbCommand> and the <xref:Microsoft.ReportingServices.DataProcessing.IDataReader> interfaces. The data processing extension must support a schema-only query execution. This query is used to retrieve column metadata at design-time so that users can map columns to delivery options and report parameters in the subscription definition. Schema-only query execution occurs at an early stage when the user is defining the subscription.  
  
## Query Requirements  
 When creating query that retrieves subscription data, keep the following points in mind:  
  
-   You can only create one query for the subscription.  
  
-   The query must return all of the values that you want to use for delivery options and for specifying report parameters.  
  
-   The report server will create a report delivery for every row in the result set. If the result set consists of three hundred rows, the report server will attempt to deliver three hundred reports.  
  
## Setting Delivery Options Using Variable Data from a Subscriber Database  
 You can use data in the subscriber database to customize delivery options for each recipient. The kind of delivery extension you are using determines which options are available. If you are using the report server e-mail delivery extension, the query should contain an e-mail alias for each subscriber. If you are using file share delivery, the subscriber data should include values that can be used to create subscriber-specific report files or to provide a destination for the delivery. For more information, see [File Share Delivery in Reporting Services](file-share-delivery-in-reporting-services.md) and [E-Mail Delivery in Reporting Services](e-mail-delivery-in-reporting-services.md).  
  
## Passing Parameter Values from the Subscriber Database to the Report  
 If you are creating a data-driven subscription for a parameterized report, you can use variable parameter values to customize the output of each report. For example, the subscriber database might contain employee identification numbers, hire dates, job titles, and office location information that can be used to filter report data. If the report accepts parameters that are based on these or other available column data, you can map the parameter to the appropriate column.  
  
 When mapping subscriber fields to report parameters, make sure that the data types and column lengths are compatible. If there is a data type mismatch, an error will occur during subscription processing. To learn more about using subscriber data in a parameterized report, see [Create a Data-Driven Subscription &#40;SSRS Tutorial&#41;](../create-a-data-driven-subscription-ssrs-tutorial.md).  
  
## Modifying the Subscriber Data Source  
 The following modifications to the subscriber data source can prevent the subscription from running:  
  
-   Removing columns that are referenced in the subscription.  
  
-   Modifying the table structure of the data source.  
  
-   Changing data type and other column properties.  
  
 If you make any of these changes, you must update the subscription.  
  
## See Also  
 [Create, Modify, and Delete a Data-Driven Subscription](data-driven-subscriptions.md)   
 [Data-Driven Subscriptions](data-driven-subscriptions.md)   
 [Subscriptions and Delivery &#40;Reporting Services&#41;](subscriptions-and-delivery-reporting-services.md)  
  
  
