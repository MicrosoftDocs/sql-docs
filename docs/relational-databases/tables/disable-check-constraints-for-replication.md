---
description: "Disable Check Constraints for Replication"
title: "Disable Check Constraints for Replication | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "CHECK constraints, disabling"
  - "constraints [SQL Server], disabling"
  - "disabling constraints"
  - "constraints [SQL Server], check"
ms.assetid: af98fc70-24dd-4bd3-a0a3-f701dfa67b2c
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Disable Check Constraints for Replication
[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw.md)]

  You can disable check constraints in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. You can also explicitly disable check constraints for replication, which can be useful if you are publishing data from a previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  If a table is published using replication, check constraints are automatically disabled for operations performed by replication agents. When a replication agent performs an insert, update, or delete at a Subscriber, the constraint is not checked; if a user performs an insert, update, or delete, the constraint is checked. The constraint is disabled for the replication agent because the constraint was already checked at the Publisher when the data was originally inserted, updated, or deleted. For more information, see [Specify Schema Options](../../relational-databases/replication/publish/specify-schema-options.md).  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To disable a check constraint for replication  
  
1.  In **Object Explorer**, expand the table with the check constraint you want to modify, and then expand the **Constraints** folder.  
  
2.  Right-click the check constraint you wish to modify and then click **Modify**.  
  
3.  In the **Check Constraints** dialog box, under **Table Designer**, select a value of **No** for **Enforce For Replication**.  
  
4.  Click **Close**.  

##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To disable a check constraint for replication  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. The example creates a table with an IDENTITY column and a CHECK constraint on the table. The example then drops the constraint and recreates it specifying the NOT FOR REPLICATION clause.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    CREATE TABLE dbo.doc_exd (column_a int IDENTITY (1,1)   
    CONSTRAINT exd_check CHECK (column_a > 1))   
  
    ALTER TABLE dbo.doc_exd   
    DROP CONSTRAINT exd_check;   
    GO  
    ALTER TABLE dbo.doc_exd    
    ADD CONSTRAINT exd_check CHECK NOT FOR REPLICATION (column_a > 1);  
    ```  
  
 For more information, see [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md).  
  
###  <a name="TsqlExample"></a>   
## See Also  
 [Specify Schema Options](../../relational-databases/replication/publish/specify-schema-options.md)  
  
  
