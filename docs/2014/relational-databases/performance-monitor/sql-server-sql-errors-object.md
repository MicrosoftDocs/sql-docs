---
title: "SQL Server, SQL Errors Object | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Errors object"
  - "SQLServer:SQL Errors"
ms.assetid: 6e5273ca-29cb-4618-88a2-70b9b8d6cf76
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SQL Server, SQL Errors Object
  The **SQLServer:SQL Errors** object in Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides counters to monitor **SQL Errors**.  
  
 This table describes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **SQL Errors** counters.  
  
|SQL Server SQL Errors counters|Description|  
|------------------------------------|-----------------|  
|**Errors/sec**|Number of errors/sec.|  
  
 Each counter in the object contains the following instances:  
  
|Item|Definition|  
|----------|----------------|  
|**_Total**|Information for all errors.|  
|**DB Offline Errors**|Tracks severe errors that cause [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to take the current database offline.|  
|**Info Errors**|Information related to error messages that provide information to users but do not cause errors.|  
|**Kill Connection Errors**|Tracks severe errors that cause [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to kill the current connection.|  
|**User Errors**|Information about user errors.|  
  
## See Also  
 [Monitor Resource Usage &#40;System Monitor&#41;](monitor-resource-usage-system-monitor.md)  
  
  
