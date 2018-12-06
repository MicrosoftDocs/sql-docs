---
title: "Performance Event Category | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SQL Server event classes, Performance event category"
  - "Performance event category [SQL Server]"
  - "event classes [SQL Server], Performance event category"
ms.assetid: 708f3585-d8be-4980-bbff-672d7c59397e
author: stevestein
ms.author: sstein
manager: craigg
---
# Performance Event Category
  Use the **Performance** event category to monitor **Showplan** event classes and event classes that are produced from the execution of SQL data manipulation language (DML) operators.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Auto Stats Event Class](auto-stats-event-class.md)|Indicates that an automatic updating of index and column statistics has occurred.|  
|[Degree of Parallelism &#40;7.0 Insert&#41; Event Class](degree-of-parallelism-7-0-insert-event-class.md)|Indicates that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has executed a SELECT, INSERT, UPDATE, or DELETE statement using either a serial or parallel plan. The number of CPUs used to perform the operation is also reported.|  
|[Performance Statistics Event Class](performance-statistics-event-class.md)|Monitors performance of the queries that are being executed.|  
|[Showplan All Event Class](showplan-all-event-class.md)|Identifies **Showplan** operators within a SQL statement.|  
|[Showplan All for Query Compile Event Class](showplan-all-for-query-compile-event-class.md)|Displays compile time data for **Showplan** operators.|  
|[Showplan Statistics Profile Event Class](showplan-statistics-profile-event-class.md)|Displays the estimated cost of a query.|  
|[Showplan XML Event Class](showplan-xml-event-class.md)|Identifies the **Showplan** operators in a SQL statement. The event class stores each event as a well defined XML document.|  
|[Showplan XML for Query Compile Event Class](showplan-xml-for-query-compile-event-class.md)|Displays compile time data for **Showplan** operators in XML format.|  
|[Showplan XML Statistics Profile Event Class](showplan-xml-statistics-profile-event-class.md)|Identifies the **Showplan** operators associated with a SQL statement. The output is an XML document.|  
|[SQL:FullTextQuery Event Class](sql-fulltextquery-event-class.md)|Indicates that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has executed a full-text query.|  
|[Plan Guide Successful Event Class](plan-guide-successful-event-class.md)|Indicates that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] successfully produced an execution plan for a query or batch that contained a plan guide.|  
|[Plan Guide Unsuccessful Event Class](plan-guide-unsuccessful-event-class.md)|Indicates that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] could not produce an execution plan for a query or batch that contained a plan guide.|  
  
## See Also  
 [Extended Events](../extended-events/extended-events.md)  
  
  
