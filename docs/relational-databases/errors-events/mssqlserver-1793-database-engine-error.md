---
title: "MSSQLSERVER_1793 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
ms.assetid: 808db1d0-acc1-41d0-9287-8a5455001a02
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_1793
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|1793|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|FILESTREAM_BASEDATA_NEED_SAME_PARTITION|  
|Message Text|Cannot drop index '%.*ls' since a partition scheme is not specified for FILESTREAM data.|  
  
## Explanation  
This message occurs when you try to drop a clustered index on a table that contains FILESTREAM data, and you specify a **MOVE TO** clause for the base data, but you do not specify a **FILESTREAM_ON** clause for the FILESTREAM data.  
  
## User Action  
When dropping a clustered index on a table that contains FILESTREAM data, use one of the following options:  
  
-   Specify both a **MOVE TO** clause for the base data and a **FILESTREAM_ON** clause for the FILESTREAM data.  
  
-   Do not specify either a **MOVE TO** clause for the base data or a **FILESTREAM_ON** clause for the FILESTREAM data.  
  
The following example fails because a partition scheme is specified for the base data, but is not specified for the FILESTREAM data.  
  
```Transact-SQL  
DROP INDEX [<clustered_index_name>] ON [<table_name>]   
WITH ( ONLINE = OFF, MOVE TO [PRIMARY] )  
GO  
```  
  
The following example succeeds because both a **MOVE TO** clause for the base data and a **FILESTREAM_ON** clause for the FILESTREAM data are specified.  
  
```Transact-SQL  
DROP INDEX [<clustered_index_name>] ON [<table_name>]   
WITH ( ONLINE = OFF, MOVE TO [PRIMARY], filestream_on 'default' )  
GO  
```  
  
The following example also succeeds because neither a **MOVE TO** clause for the base data nor a **FILESTREAM_ON** clause for the FILESTREAM data is specified.  
  
```Transact-SQL  
DROP INDEX [<clustered_index_name>] ON [<table_name>]   
WITH ( ONLINE = OFF )  
GO  
```  
  
