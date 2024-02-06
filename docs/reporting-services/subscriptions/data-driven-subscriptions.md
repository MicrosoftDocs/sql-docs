---
title: "Data-driven subscriptions"
description: Learn about data-driven subscriptions, which provide a way to use dynamic subscription data that you retrieve from an external data source at run time.
author: maggiesMSFT
ms.author: maggies
ms.date: 05/20/2019
ms.service: reporting-services
ms.subservice: subscriptions
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "subscriptions [Reporting Services], data-driven"
  - "data-driven subscriptions"
---
# Data-driven subscriptions
  A data-driven subscription provides a way to use dynamic subscription data that is retrieved from an external data source at run time. A data-driven subscription can also use static text and default values that you specify when the subscription is defined. You can use data-driven subscriptions to:  
  
-    Distribute a report to a fluctuating list of subscribers. For example, you can use data-driven subscriptions to distribute a report throughout a large organization where subscribers vary from one month to the next. Or, you can use other criteria that determine group membership from an existing set of users.  
  
-   Filter the report output by using report parameter values that are retrieved at run time.  
  
-   Vary report output formats and delivery options for each report delivery.  
  
 A data-driven subscription is composed of multiple parts. The fixed aspects of a data-driven subscription are defined when you create the subscription, and these aspects include:  
  
- The report for which the subscription is defined (a subscription is always associated with a single report).  
  
- The delivery extension used to distribute the report. You can specify report server e-mail delivery, file share delivery, or the null delivery provider used for preloading the cache. You can also specify a custom delivery extension. You can't specify multiple delivery extensions within a single subscription.  
  
- The subscriber data source. You must specify a connection string to the data source that contains subscriber data when you define the subscription. The subscriber data source can't be specified dynamically at run time.  
  
- The query that you use to select subscriber data must be specified when you define the subscription. You can't change the query at run time.  
  
 Dynamic values used in a data-driven subscription are obtained when the subscription is processed. Examples of variable data that you might use in a subscription include the subscriber name, e-mail address, preferred report output format, or any value that is valid for a report parameter. To use dynamic values in a data-driven subscription, you define a mapping between the fields that are returned in the query to specific delivery options and to report parameters. Variable data is retrieved from a subscriber data source each time the subscription is processed.  
  
## Requirements for data-driven subscriptions

 Data-driven subscription functionality isn't available in all editions. There are also limitations on the kinds of data sources that you can use to retrieve subscription data at run time. The following list provides more information about the requirements:  

- For more information about the editions of SQL Server that support Data-driven subscription functionality, see [SQL Server Reporting Services features supported by its editions](../reporting-services-features-supported-by-the-editions-of-sql-server-2016.md).  

- For subscription data, choose a data source that can provide schema information to the report server. Examples of supported data source types include:
    - [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational data, 
    - Oracle [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] databases
    - [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package data
    - ODBC data sources, and OLE DB data sources. 

- For more information about subscriber data source requirements, see [Use an external data source for subscriber data &#40;data-driven subscription&#41;](../../reporting-services/subscriptions/use-an-external-data-source-for-subscriber-data-data-driven-subscription.md).  
  
## Working with data-driven subscriptions  
 The following articles provide more information about data-driven subscriptions.  
  
|Articles|Description|  
|------------|-----------------|  
|[Create, modify, and delete data-driven subscriptions](../../reporting-services/subscriptions/create-modify-and-delete-data-driven-subscriptions.md)|Explains how to create, modify, or delete a data-driven subscription.|  
|[Use an external data source for subscriber data &#40;data-driven subscription&#41;](../../reporting-services/subscriptions/use-an-external-data-source-for-subscriber-data-data-driven-subscription.md)|Provides information about the data sources that you can use for a data-driven subscription.|  
|[Create a data-driven subscription &#40;SSRS tutorial&#41;](../../reporting-services/create-a-data-driven-subscription-ssrs-tutorial.md)|Provides step-by-step instruction for learning how to create a data-driven subscription.|  
|[Cache reports &#40;SSRS&#41;](../../reporting-services/report-server/caching-reports-ssrs.md)|Describes how to use the Null Delivery Provider with a data-driven subscription to preload the cache.|  
  
## Related content

 [Subscriptions and delivery &#40;Reporting Services&#41;](../../reporting-services/subscriptions/subscriptions-and-delivery-reporting-services.md)  
 [Preload the cache &#40;web portal&#41;](../../reporting-services/report-server/preload-the-cache-report-manager.md)  
  
  
