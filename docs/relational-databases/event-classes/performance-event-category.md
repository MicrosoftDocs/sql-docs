---
title: "Performance Event Category | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server event classes, Performance event category"
  - "Performance event category [SQL Server]"
  - "event classes [SQL Server], Performance event category"
ms.assetid: 708f3585-d8be-4980-bbff-672d7c59397e
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Performance Event Category
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  Use the **Performance** event category to monitor **Showplan** event classes and event classes that are produced from the execution of SQL data manipulation language (DML) operators.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Auto Stats Event Class](../../relational-databases/event-classes/auto-stats-event-class.md)|Indicates that an automatic updating of index and column statistics has occurred.|  
|[Degree of Parallelism &#40;7.0 Insert&#41; Event Class](../../relational-databases/event-classes/degree-of-parallelism-7-0-insert-event-class.md)|Indicates that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has executed a SELECT, INSERT, UPDATE, or DELETE statement using either a serial or parallel plan. The number of CPUs used to perform the operation is also reported.|  
|[Performance Statistics Event Class](../../relational-databases/event-classes/performance-statistics-event-class.md)|Monitors performance of the queries that are being executed.|  
|[Showplan All Event Class](../../relational-databases/event-classes/showplan-all-event-class.md)|Identifies **Showplan** operators within a SQL statement.|  
|[Showplan All for Query Compile Event Class](../../relational-databases/event-classes/showplan-all-for-query-compile-event-class.md)|Displays compile time data for **Showplan** operators.|  
|[Showplan Statistics Profile Event Class](../../relational-databases/event-classes/showplan-statistics-profile-event-class.md)|Displays the estimated cost of a query.|  
|[Showplan XML Event Class](../../relational-databases/event-classes/showplan-xml-event-class.md)|Identifies the **Showplan** operators in a SQL statement. The event class stores each event as a well defined XML document.|  
|[Showplan XML for Query Compile Event Class](../../relational-databases/event-classes/showplan-xml-for-query-compile-event-class.md)|Displays compile time data for **Showplan** operators in XML format.|  
|[Showplan XML Statistics Profile Event Class](../../relational-databases/event-classes/showplan-xml-statistics-profile-event-class.md)|Identifies the **Showplan** operators associated with a SQL statement. The output is an XML document.|  
|[SQL:FullTextQuery Event Class](../../relational-databases/event-classes/sql-fulltextquery-event-class.md)|Indicates that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has executed a full-text query.|  
|[Plan Guide Successful Event Class](../../relational-databases/event-classes/plan-guide-successful-event-class.md)|Indicates that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] successfully produced an execution plan for a query or batch that contained a plan guide.|  
|[Plan Guide Unsuccessful Event Class](../../relational-databases/event-classes/plan-guide-unsuccessful-event-class.md)|Indicates that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] could not produce an execution plan for a query or batch that contained a plan guide.|  
  
## See Also  
 [Extended Events](../../relational-databases/extended-events/extended-events.md)  
  
  
