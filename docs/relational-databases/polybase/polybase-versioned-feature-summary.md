---
title: "PolyBase Versioned Feature Summary | Microsoft Docs"
ms.custom: ""
ms.date: "2016-04-13"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 6591994d-6109-4285-9c5b-ecb355f8a111
caps.latest.revision: 10
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# PolyBase Versioned Feature Summary
[!INCLUDE[tsql-appliesto-ss2016-xxxx-asdw-pdw_md](../../includes/tsql-appliesto-ss2016-xxxx-asdw-pdw-md.md)]

  Summary of PolyBase features available for  SQL Server products and services.  
  
## Feature Summary for Product Releases  
 This table summarizes key features for PolyBase and the products in which they are available.  
  
### [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]  
 These features apply to [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] running on-premises or in an Azure virtual machine.  PolyBase is not available in SQL Server 2014 and earlier versions.  
  
|||  
|-|-|  
|**Feature**|**Availability**|  
|Query Hadoop data with [!INCLUDE[tsql](../../includes/tsql-md.md)]|yes|  
|Query Azure blob storage with [!INCLUDE[tsql](../../includes/tsql-md.md)]|yes|  
|Import data from Hadoop|yes|  
|Import data from Azure blob storage|yes| 
|Import data from Azure Data Lake Store|no|   
|Export data to Hadoop|yes|  
|Export data to Azure blob storage|yes|  
|Export data from Azure Data Lake Store|no|
|Run PolyBase queries from Microsoft's BI tools|yes|  
|Push down query computations to Hadoop|yes|  
  
### [!INCLUDE[ssSDW](../../includes/sssdw-md.md)]  
 These features apply to [!INCLUDE[ssSDW](../../includes/sssdw-md.md)].  
  
|||  
|-|-|  
|**Feature**|**Availability**|  
|Query Hadoop data with [!INCLUDE[tsql](../../includes/tsql-md.md)]|no|  
|Query Azure blob storage with [!INCLUDE[tsql](../../includes/tsql-md.md)]|yes|  
|Import data from Hadoop|no|  
|Import data from Azure blob storage|yes|
|Import data from Azure Data Lake Store|yes|     
|Export data to Hadoop|no|  
|Export data to Azure blob storage|yes|  
|Export data from Azure Data Lake Store|yes|
|Run PolyBase queries from Microsoft's BI tools|yes|  
|Push down query computations to Hadoop|no|  
  
### [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 These features apply to [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].  
  
|||  
|-|-|  
|**Feature**|**Availability**|  
|Query Hadoop data with [!INCLUDE[tsql](../../includes/tsql-md.md)]|yes|  
|Query Azure blob storage with [!INCLUDE[tsql](../../includes/tsql-md.md)]|yes|  
|Import data from Hadoop|yes|  
|Import data from Azure blob storage|yes|  
|Import data from Azure Data Lake Store|no|   
|Export data to Hadoop|yes|  
|Export data to Azure blob storage|yes|  
|Export data from Azure Data Lake Store|no|
|Run PolyBase queries from Microsoft's BI tools|yes|  
|Push down query computations to Hadoop|yes|  
  
## See Also  
 [PolyBase Guide](../../relational-databases/polybase/polybase-guide.md)  
  
  