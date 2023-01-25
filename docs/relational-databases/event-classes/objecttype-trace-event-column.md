---
title: "ObjectType Trace Event Column | Microsoft Docs"
description: Refer to the possible values of the Object Type trace event column, which is used in a variety of trace events in SQL Server.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: reference
helpviewer_keywords: 
  - "SQL Server event classes, Object Type column values"
  - "events [SQL Server], Object Type column values"
  - "event classes [SQL Server], Object Type column values"
  - "Object Type column values [SQL Server]"
ms.assetid: 42f85c50-34c9-49ca-955f-af9595e2707f
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ObjectType Trace Event Column
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  The Object Type trace event column is used in a variety of trace events. This topic describes the possible values of this column and their associated definitions.  
  
## Object Type Column Values  
  
|Value|Definition|  
|-----------|----------------|  
|8259|Check Constraint|  
|8260|Default (constraint or standalone)|  
|8262|Foreign-key Constraint|  
|8272|Stored Procedure|  
|8274|Rule|  
|8275|System Table|  
|8276|Trigger on Server|  
|8277|(User-defined) Table|  
|8278|View|  
|8280|Extended Stored Procedure|  
|16724|CLR Trigger|  
|16964|Database|  
|16975|Object|  
|17222|FullText Catalog|  
|17232|CLR Stored Procedure|  
|17235|Schema|  
|17475|Credential|  
|17491|DDL Event|  
|17741|Management Event|  
|17747|Security Event|  
|17749|User Event|  
|17985|CLR Aggregate Function|  
|17993|Inline Table-valued SQL Function|  
|18000|Partition Function|  
|18002|Replication Filter Procedure|  
|18004|Table-valued SQL Function|  
|18259|Server Role|  
|18263|[!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Group|  
|19265|Asymmetric Key|  
|19277|Master Key|  
|19280|Primary Key|  
|19283|ObfusKey|  
|19521|Asymmetric Key Login|  
|19523|Certificate Login|  
|19538|Role|  
|19539|SQL Login|  
|19543|Windows Login|  
|20034|Remote Service Binding|  
|20036|Event Notification on Database|  
|20037|Event Notification|  
|20038|Scalar SQL Function|  
|20047|Event Notification on Object|  
|20051|Synonym|  
|20307|Sequence|  
|20549|End Point|  
|20801|Adhoc Queries which may be cached|  
|20816|Prepared Queries which may be cached|  
|20819|Service Broker Service Queue|  
|20821|Unique Constraint|  
|21057|Application Role|  
|21059|Certificate|  
|21075|Server|  
|21076|Transact-SQL Trigger|  
|21313|Assembly|  
|21318|CLR Scalar Function|  
|21321|Inline scalar SQL Function|  
|21328|Partition Scheme|  
|21333|User|  
|21571|Service Broker Service Contract|  
|21572|Trigger on Database|  
|21574|CLR Table-valued Function|  
|21577|Internal Table (For example, XML Node Table, Queue Table.)|  
|21581|Service Broker Message Type|  
|21586|Service Broker Route|  
|21587|Statistics|  
|21825<br /><br /> 21827<br /><br /> 21831<br /><br /> 21843<br /><br /> 21847|User|  
|22099|Service Broker Service|  
|22601|Index|  
|22604|Certificate Login|  
|22611|XMLSchema|  
|22868|Type|  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)  
  
  
