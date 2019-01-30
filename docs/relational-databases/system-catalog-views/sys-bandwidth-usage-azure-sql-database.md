---
title: "sys.bandwidth_usage (Azure SQL Database) | Microsoft Docs"
ms.custom: ""
ms.date: "01/28/2019"
ms.prod: ""
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "bandwidth_usage"
  - "sys.bandwidth_usage"
  - "bandwidth_usage_TSQL"
  - "sys.bandwidth_usage_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.bandwidth_usage"
  - "bandwidth_usage"
ms.assetid: 43ed8435-f059-4907-b5c0-193a258b394a
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: "= azuresqldb-current || = sqlallproducts-allversions"
---
# sys.bandwidth_usage (Azure SQL Database)

[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

  **Note: This applies only to [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]V11.**  
  
 Returns information about the network bandwidth used by each database in a **[!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] V11 database server**, . Each row returned for a given database summarizes a single direction and class of usage over a one-hour period.  
  
 **This has been deprecated in a [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].**  
  
 The **sys.bandwidth_usage** view contains the following columns.  
  
|Column Name|Description|  
|-----------------|-----------------|  
|**time**|The hour when the bandwidth was consumed. The rows in this view are on a per-hour basis. For example, 2009-09-19 02:00:00.000 means that the bandwidth was consumed on September 19, 2009 between 2:00 A.M. and 3:00 A.M.|  
|**database_name**|The name of the database that used bandwidth.|  
|**direction**|The type of bandwidth that was used, one of:<br /><br /> Ingress: Data that is moving into the [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].<br /><br /> Egress: Data that is moving out of the [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].|  
|**class**|The class of bandwidth that was used, one of:<br />Internal: Data that is moving within the Azure platform.<br />External: Data that is moving out of the Azure platform.<br /><br /> This class is returned only if the database is engaged in a continuous copy relationship between regions ([!INCLUDE[ssGeoDR](../../includes/ssgeodr-md.md)]). If a given database does not participate in any continuous copy relationship, then "Interlink" rows are not returned. For more information, see the "Remarks" section later in this topic.|  
|**time_period**|The time period when the usage occurred is either Peak or OffPeak. The Peak time is based on the region in which the server was created. For example, if a server was created in the "US_Northwest" region, the Peak time is defined as being between 10:00 A.M. and 6:00 P.M. PST.|  
|**quantity**|The amount of bandwidth, in kilobytes (KBs), that was used.|  
  
## Permissions

 This view is only available in the **master** database to the server-level principal login.  
  
## Remarks  
  
### External and Internal Classes

 For each database used at a given time, the **sys.bandwidth_usage** view returns rows that show the class and direction of bandwidth usage. The following example illustrates data that might be exposed for a given database. In this example, the time is 2012-04-21 17:00:00, which occurs during the peak time period. The database name is Db1. In this example, **sys.bandwidth_usage** has returned a row for all four combinations of the Ingress and Egress directions and External and Internal classes, as follows:  
  
|time|database_name|direction|class|time_period|quantity|  
|----------|--------------------|---------------|-----------|------------------|--------------|  
|2012-04-21 17:00:00|Db1|Ingress|External|Peak|66|  
|2012-04-21 17:00:00|Db1|Egress|External|Peak|741|  
|2012-04-21 17:00:00|Db1|Ingress|Internal|Peak|1052|  
|2012-04-21 17:00:00|Db1|Egress|Internal|Peak|3525|  
  
### Interpreting Data Direction for [!INCLUDE[ssGeoDR](../../includes/ssgeodr-md.md)]

 For [!INCLUDE[ssGeoDR](../../includes/ssgeodr-md.md)], bandwidth-usage data is visible in the logical master database on both sides of a continuous copy relationship. So you must interpret the ingress and egress direction indicators from the perspective of the databases that you are querying. For example, consider a replication stream that transfers 1MB of data from the source server to the target server. In this case, on the source server the 1MB counts toward total data sent, and on the target server, the 1MB is recorded as data received.  
  
> [!NOTE]  
> The bulk of data transferred is from the source server to the target server, in the direction of user data flow. However, some data transfer is required in the other direction.  
