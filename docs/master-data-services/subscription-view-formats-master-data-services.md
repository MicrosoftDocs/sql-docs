---
title: "Subscription View Formats (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: ff1e2566-ac8f-467d-a6d9-12c3f13879b9
author: leolimsft
ms.author: lle
manager: craigg
---
# Subscription View Formats (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  Based on the entity or derived hierarchy you select, the following formats are available for your subscription view.  
  
## Subscription View Formats  
  
|Name|Description|  
|----------|-----------------|  
|**Leaf Members**|Contains leaf members and their associated attribute values.|  
|**Leaf Members History**|Contains leaf members' historical data and the associated attribute values. The view format is Slowly Changing Dimension Type 4 style.|  
|**Leaf Members SCD Type 2**|Contains leaf members' historical and current data, and the associated attribute values. The view format is Slowly Changing Dimension Type 2 style.|  
|**Consolidated Members**|Contains consolidated members and their associated attribute values.|  
|**Consolidated Members History**|Contains consolidated members' historical data and the associated attribute values. The view format is Slowly Changing Dimension Type 4 style.|  
|**Consolidated Members SCD Type 2**|Contains consolidated members' historical and current data, and the associated attribute values. The view format is Slowly Changing Dimension Type 2 style.|  
|**Collection Memberships**|Contains a list of collections and their associated attribute values.|  
|**Collections**|Contains a list of collections and the members in each, along with weight values and sort order.|  
|**Collection Members History**|Contains collection members' historical data and the associated attribute values. The view format is Slowly Changing Dimension Type 4 style.|  
|**Collection Members SCD Type 2**|Contains collection members' historical and current data, and the associated attribute values. The view format is Slowly Changing Dimension Type 2 style.|  
|**Explicit Parent Child**|Contains explicit hierarchy structures for an entity in a parent child format.|  
|**Explicit Levels**|Contains explicit hierarchy structures for an entity in level format.|  
|**Derived Parent Child (Derived Hierarchy View)**|Contains a derived hierarchy structure in parent child format.|  
|**Derived Levels (Derived Hierarchy View)**|Contains a derived hierarchy structure in level format.|  
  
## See Also  
 [Overview: Exporting Data &#40;Master Data Services&#41;](../master-data-services/overview-exporting-data-master-data-services.md)   
 [Create a Subscription View to Export Data &#40;Master Data Services&#41;](../master-data-services/create-a-subscription-view-to-export-data-master-data-services.md)  
  
  
