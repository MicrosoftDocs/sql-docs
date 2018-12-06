---
title: "Publishing Stored Procedure Execution in Transactional Replication | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "publishing [SQL Server replication], stored procedure execution"
  - "articles [SQL Server replication], stored procedures and"
  - "transactional replication, publishing stored procedure execution"
ms.assetid: f4686f6f-c224-4f07-a7cb-92f4dd483158
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Publishing Stored Procedure Execution in Transactional Replication
  If you have one or more stored procedures that execute at the Publisher and affect published tables, consider including those stored procedures in your publication as stored procedure execution articles. The definition of the procedure (the CREATE PROCEDURE statement) is replicated to the Subscriber when the subscription is initialized; when the procedure is executed at the Publisher, replication executes the corresponding procedure at the Subscriber. This can provide significantly better performance for cases where large batch operations are performed, because only the procedure execution is replicated, bypassing the need to replicate the individual changes for each row. For example, assume you create the following stored procedure in the publication database:  
  
```  
CREATE PROC give_raise AS  
UPDATE EMPLOYEES SET salary = salary * 1.10  
```  
  
 This procedure gives each of the 10,000 employees in your company a 10 percent pay increase. When you execute this stored procedure at the Publisher, it updates the salary for each employee. Without the replication of stored procedure execution, the update would be sent to Subscribers as a large, multi-step transaction:  
  
```  
BEGIN TRAN  
UPDATE EMPLOYEES SET salary = salary * 1.10 WHERE PK = 'emp 1'  
UPDATE EMPLOYEES SET salary = salary * 1.10 WHERE PK = 'emp 2'  
```  
  
 And this repeats for 10,000 updates.  
  
 With the replication of stored procedure execution, replication sends only the command to execute the stored procedure at the Subscriber, rather than writing all the updates to the distribution database and then sending them over the network to the Subscriber:  
  
```  
EXEC give_raise  
```  
  
> [!IMPORTANT]  
>  Stored procedure replication is not appropriate for all applications. If an article is filtered horizontally, so that there are different sets of rows at the Publisher than at the Subscriber, executing the same stored procedure at both returns different results. Similarly, if an update is based on a subquery of another, nonreplicated table, executing the same stored procedure at both the Publisher and Subscriber returns different results.  
  
 **To publish the execution of a stored procedure**  
  
-   SQL Server Management Studio: [Publish the Execution of a Stored Procedure in a Transactional Publication &#40;SQL Server Management Studio&#41;](../publish/publish-execution-of-stored-procedure-in-transactional-publication.md)  
  
-   Replication Transact-SQL Programming: execute [sp_addarticle &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addarticle-transact-sql) and specify a value of 'serializable proc exec' (recommended) or 'proc exec' for the parameter **@type**. For more information about defining articles, see [Define an Article](../publish/define-an-article.md).  
  
## Modifying the Procedure at the Subscriber  
 By default, the stored procedure definition at the Publisher is propagated to each Subscriber. However, you can also modify the stored procedure at the Subscriber. This is useful if you want different logic to be executed at the Publisher and Subscriber. For example, consider **sp_big_delete**, a stored procedure at the Publisher that has two functions: it deletes 1,000,000 rows from the replicated table **big_table1** and updates the nonreplicated table **big_table2**. To reduce the demand on network resources, you should propagate the 1 million row delete as a stored procedure by publishing **sp_big_delete**. At the Subscriber, you can modify **sp_big_delete** to delete only the 1 million rows and not perform the subsequent update to **big_table2**.  
  
> [!NOTE]  
>  By default, any changes made using ALTER PROCEDURE at the Publisher are propagated to the Subscriber. To prevent this, disable the propagation of schema changes before executing ALTER PROCEDURE. For information about schema changes, see [Make Schema Changes on Publication Databases](../publish/make-schema-changes-on-publication-databases.md).  
  
## Types of Stored Procedure Execution Articles  
 There are two different ways in which the execution of a stored procedure can be published: serializable procedure execution article and procedure execution article.  
  
-   The serializable option is recommended because it replicates the procedure execution only if the procedure is executed within the context of a serializable transaction. If the stored procedure is executed from outside a serializable transaction, changes to data in published tables are replicated as a series of DML statements. This behavior contributes to making data at the Subscriber consistent with data at the Publisher. This is especially useful for batch operations, such as large cleanup operations.  
  
-   With the procedure execution option, it is possible that execution could be replicated to all Subscribers regardless of whether individual statements in the stored procedure were successful. Furthermore, because changes made to data by the stored procedure can occur within multiple transactions, data at the Subscribers might not be consistent with data at the Publisher. To address these issues, it is required that Subscribers are read-only and that you use an isolation level greater than read uncommitted. If you use read uncommitted, changes to data in published tables are replicated as a series of DML statements.  
  
 The following example illustrates why it is recommended that you set up replication of procedures as serializable procedure articles.  
  
```  
BEGIN TRANSACTION T1  
SELECT @var = max(col1) FROM tableA  
UPDATE tableA SET col2 = <value>   
   WHERE col1 = @var   
  
BEGIN TRANSACTION T2  
INSERT tableA VALUES <values>  
COMMIT TRANSACTION T2  
```  
  
 In the previous example, it is assumed that the SELECT in transaction T1 happens before the INSERT in transaction T2.  
  
 If the procedure is not executed within a serializable transaction (with isolation level set to SERIALIZABLE), transaction T2 will be allowed to insert a new row within the range of the SELECT statement in T1 and it will commit before T1. This also means that it will be applied at the Subscriber before T1. When T1 is applied at the Subscriber, the SELECT can potentially return a different value than at the Publisher and can result in a different outcome from the UPDATE.  
  
 If the procedure is executed within a serializable transaction, transaction T2 will not be allowed to insert within the range covered by the SELECT statement in T2. It will be blocked until T1 commits ensuring the same results at the Subscriber.  
  
 Locks will be held longer when you execute the procedure within a serializable transaction and may result in reduced concurrency.  
  
## The XACT_ABORT Setting  
 When replicating stored procedure execution, the setting for the session executing the stored procedure should specify XACT_ABORT ON. If XACT_ABORT is set to OFF, and an error occurs during execution of the procedure at the Publisher, the same error will occur at the Subscriber, causing the Distribution Agent to fail. Specifying XACT_ABORT ON ensures that any errors encountered during execution at the Publisher cause the entire execution to be rolled back, avoiding the Distribution Agent failure. For more information about setting XACT_ABORT, see [SET XACT_ABORT &#40;Transact-SQL&#41;](/sql/t-sql/statements/set-xact-abort-transact-sql).  
  
 If you require a setting of XACT_ABORT OFF, specify the **-SkipErrors** parameter for the Distribution Agent. This allows the agent to continue applying changes at the Subscriber even if an error is encountered.  
  
## See Also  
 [Article Options for Transactional Replication](article-options-for-transactional-replication.md)  
  
  
