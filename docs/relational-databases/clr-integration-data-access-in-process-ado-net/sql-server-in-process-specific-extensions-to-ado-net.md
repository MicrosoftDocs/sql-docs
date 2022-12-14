---
title: "SQL Server In-Process Extensions to ADO.NET"
description: Links to articles about the four main functional extensions to ADO.NET that are specifically for in-process use.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "data access [CLR integration]"
  - "ADO.NET [CLR integration]"
  - "common language runtime [SQL Server], ADO.NET"
  - "database objects [CLR integration], in-process extensions"
  - "in-process data access providers [CLR integration]"
  - "extensions [CLR integration]"
ms.assetid: 781b812e-eb14-472a-85fa-aa4cdb929bee
---
# SQL Server In-Process Specific Extensions to ADO.NET
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  There are four main functional extensions to ADO.NET that are specifically for in-process use. The following topics will cover these extensions in detail.  
  
## In This Section  
 [SqlContext Object](../../relational-databases/clr-integration-data-access-in-process-ado-net/sqlcontext-object.md)  
 This class provides access to the other extensions by abstracting the context of a caller of a SQL Server routine that executes managed code in-process.  
  
 [SqlPipe Object](../../relational-databases/clr-integration-data-access-in-process-ado-net/sqlpipe-object.md)  
 This class contains routines to send tabular results and messages to the client.  
  
 [SqlTriggerContext Object](../../relational-databases/clr-integration-data-access-in-process-ado-net/sqltriggercontext-object.md)  
 This class provides information on the context in which a trigger is run.  
  
 [SqlDataRecord Object](../../relational-databases/clr-integration-data-access-in-process-ado-net/sqldatarecord-object.md)  
 The SqlDataRecord class represents a single row of data, along with its related metadata, and allows stored procedures to return custom result sets to the client.  
  
  
