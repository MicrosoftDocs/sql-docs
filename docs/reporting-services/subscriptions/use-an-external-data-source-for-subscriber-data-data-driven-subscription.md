---
title: "Use an external data source for subscriber data (data-driven subscription)"
description: Learn to use an external data source for a dynamic data-driven subscription. Understand data processing, querying, delivery options, and parameter passing.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: subscriptions
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "subscriber data sources [Reporting Services]"
  - "subscriptions [Reporting Services], external data sources"
  - "query-based subscriptions [Reporting Services]"
  - "external data sources [Reporting Services]"
  - "data-driven subscriptions"
  - "data sources [Reporting Services], subscriptions"
---
# Use an external data source for subscriber data (data-driven subscription)
  In a data-driven subscription, dynamic subscription data is provided by a query or command that retrieves data from an external data source. Subscription data can be retrieved from any supported data source that meets the requirements for data-driven subscription processing. The query or command syntax must be valid for a data processing extension installed with your report server.  
  
## Data processing requirements  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] uses data processing extensions to retrieve subscription data. Recommended data source types include:  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational databases  
  
-   Oracle databases  
  
-   [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] multidimensional and data mining data sources  
  
-   XML data sources  
  
     When you use the XML data processing extension for subscriber data, be sure to increase the query timeout settings in the subscription. The XML data processing extension uses milliseconds rather than seconds for query timeout values. If you don't increase the timeout value, the subscription might fail due to insufficient processing time.  
  
     Try not to use the **Credentials are not required** option when configuring the connection to the subscriber data source. Stored credentials are recommended you use the XML data processing extension to retrieve subscription data at run time.  
  
 You might be able to use other supported data source types, but not all of them are guaranteed to work. For example, the following data source types can't be used for subscriber data:  
  
-   SAP Netweaver BI databases  
  
-   Report models  
  
 If you have a custom data processing extension that you want to use in data-driven subscriptions, it must implement the <xref:Microsoft.ReportingServices.DataProcessing.IDbCommand> and the <xref:Microsoft.ReportingServices.DataProcessing.IDataReader> interfaces. The data processing extension must support a schema-only query execution. This query is used to retrieve column metadata at design-time so that users can map columns to delivery options and report parameters in the subscription definition. Schema-only query execution occurs at an early stage when the user is defining the subscription.  
  
## Query requirements  
 When creating query that retrieves subscription data, keep the following points in mind:  
  
-   You can only create one query for the subscription.  
  
-   The query must return all of the values that you want to use for delivery options and to specify report parameters.  
  
-   The report server creates a report delivery for every row in the result set. If the result set consists of 300 rows, the report server attempts to deliver 300 reports.  
  
## Set delivery options with variable data from a subscriber database  
 You can use data in the subscriber database to customize delivery options for each recipient. The kind of delivery extension you use determines which options are available. If you use the report server e-mail delivery extension, the query should contain an e-mail alias for each subscriber. If you use file share delivery, the subscriber data should include values that can be used to create subscriber-specific report files or to provide a destination for the delivery. For more information, see [E-mail delivery in Reporting Services](../../reporting-services/subscriptions/e-mail-delivery-in-reporting-services.md).  
  
## Pass parameter values from the subscriber database to the report  
 If you're creating a data-driven subscription for a parameterized report, you can use variable parameter values to customize the output of each report. For example, the subscriber database might contain employee identification numbers, hire dates, job titles, and office location information that can be used to filter report data. If the report accepts parameters that are based on these or other available column data, you can map the parameter to the appropriate column.  
  
 When mapping subscriber fields to report parameters, make sure that the data types and column lengths are compatible. If there's a data type mismatch, an error occurs during subscription processing. To learn more about how to use subscriber data in a parameterized report, see [Create a data-driven subscription &#40;SSRS tutorial&#41;](../../reporting-services/create-a-data-driven-subscription-ssrs-tutorial.md).  
  
## Modify the subscriber data source  
 The following modifications to the subscriber data source can prevent the subscription from running:  
  
-   Removing columns that are referenced in the subscription.  
  
-   Modifying the table structure of the data source.  
  
-   Changing data type and other column properties.  
  
 If you make any of these changes, you must update the subscription.  
  
## Related content  
 [Create, modify, and delete data-driven subscriptions](../../reporting-services/subscriptions/create-modify-and-delete-data-driven-subscriptions.md)   
 [Data-driven subscriptions](../../reporting-services/subscriptions/data-driven-subscriptions.md)   
 [Subscriptions and delivery &#40;Reporting Services&#41;](../../reporting-services/subscriptions/subscriptions-and-delivery-reporting-services.md)  
  
  
