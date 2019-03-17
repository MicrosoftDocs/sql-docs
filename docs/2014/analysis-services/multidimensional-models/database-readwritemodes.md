---
title: "Database ReadWriteModes | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "databases [Analysis Services], read/write"
  - "databases [Analysis Services], read-only"
ms.assetid: 03d7cb5c-7ff0-4e15-bcd2-7075d1b0dd69
author: minewiskan
ms.author: owend
manager: craigg
---
# Database ReadWriteModes
  There are often situations when an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database administrator (dba) wants to change a read/write database to a read-only database, or vice versa. These situations are often driven by business needs, such as sharing the same database folder among several servers for scaling out a solution and improving performance. For these situations, the `ReadWriteMode` database property enables the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] dba to easily change the database operating mode.  
  
## ReadWriteMode database property  
 The `ReadWriteMode` database property specifies whether the database is in read/write mode or in read-only mode. These are the only two possible values of the property. When the database is in read-only mode, no changes or updates can be applied to the database. However, when the database is in read/write mode, changes and updates can occur. The `ReadWriteMode` database property is defined as a read-only property; it can only be set through an `Attach` command.  
  
 When a database is in read-only mode, certain restrictions are in place that affect the ordinary set of allowed operations to the database. See the following table for the restricted operations.  
  
|ReadOnly mode|Restricted operations|  
|-------------------|---------------------------|  
|XML/A commands<br /><br /> <br /><br /> Note: An error is raised when you execute any one of these commands.|`Create`<br /><br /> `Alter`<br /><br /> `Delete`<br /><br /> `Process`<br /><br /> `MergePartitions`<br /><br /> `DesignAggregations`<br /><br /> `CommitTransaction`<br /><br /> `Restore`<br /><br /> `Synchronize`<br /><br /> `Insert`<br /><br /> `Update`<br /><br /> `Drop`<br /><br /> <br /><br /> Note: Cell writeback is allowed in databases set to read-only; however, the changes cannot be committed.|  
|MDX statements<br /><br /> <br /><br /> Note: An error is raised when you execute any one of these statements.|`COMMIT TRAN`<br /><br /> `CREATE SESSION CUBE`<br /><br /> `ALTER CUBE`<br /><br /> `ALTER DIMENSION`<br /><br /> `CREATE DIMENSION MEMBER`<br /><br /> `DROP DIMENSION MEMBER`<br /><br /> `ALTER DIMENSION`<br /><br /> <br /><br /> Note: Excel users cannot use the grouping feature in Pivot tables, because that feature is internally implemented by using `CREATE SESSION CUBE` commands.|  
|DMX statements<br /><br /> <br /><br /> Note: An error is raised when you execute any one of these statements.|`CREATE [SESSION] MINING STRUCTURE`<br /><br /> `ALTER MINING STRUCTURE`<br /><br /> `DROP MINING STRUCTURE`<br /><br /> `CREATE [SESSION] MINING MODEL`<br /><br /> `DROP MINING MODEL`<br /><br /> `IMPORT`<br /><br /> `SELECT INTO`<br /><br /> `INSERT`<br /><br /> `UPDATE`<br /><br /> `DELETE`|  
|Background operations|Any background operations that would modify the database are disabled. This includes lazy processing and proactive caching.|  
  
## ReadWriteMode Usage  
 The `ReadWriteMode` database property is to be used as part of an `Attach` database command. The `Attach` command allows the database property to be set to either `ReadWrite` or `ReadOnly`. The `ReadWriteMode` database property value cannot be updated directly because the property is defined as read-only. Databases are created with the `ReadWriteMode` property set to `ReadWrite`. A database cannot be created in read-only mode.  
  
 To switch the `ReadWriteMode` database property between `ReadWrite` and `ReadOnly`, you must issue a sequence of `Detach/Attach` commands.  
  
 All database operations, with the exception of `Attach`, keep the `ReadWriteMode` database property in its current state. For example, operations like `Alter`, `Backup`, `Restore`, and `Synchronize` preserve the `ReadWriteMode` value.  
  
> [!NOTE]  
>  Local cubes can be created from a read-only database.  
  
## See Also  
 <xref:Microsoft.AnalysisServices.Server.Attach%2A>   
 <xref:Microsoft.AnalysisServices.Database.Detach%2A>   
 [Attach and Detach Analysis Services Databases](attach-and-detach-analysis-services-databases.md)   
 [Move an Analysis Services Database](move-an-analysis-services-database.md)   
 [Detach Element](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/detach-element)   
 [Attach Element](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/attach-element)  
  
  
