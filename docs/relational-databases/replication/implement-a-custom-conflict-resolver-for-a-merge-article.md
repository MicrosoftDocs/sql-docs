---
title: "Implement a Custom Conflict Resolver for a Merge Article | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "merge replication conflict resolution [SQL Server replication], stored procedure-based resolvers"
  - "articles [SQL Server replication], conflict resolution"
  - "conflict resolution [SQL Server replication], merge replication"
ms.assetid: 76bd8524-ebc1-4d80-b5a2-4169944d6ac0
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Implement a Custom Conflict Resolver for a Merge Article
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to implement custom conflict resolver for a merge article in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[tsql](../../includes/tsql-md.md)] or a [COM-based custom resolver](../../relational-databases/replication/merge/advanced-merge-replication-conflict-com-based-custom-resolvers.md).  
  
 **In This Topic**  
  
-   **To implement custom conflict resolver for a merge article, using:**  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [COM-based Resolver](#COM)  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 You can write your own custom conflict resolver as a [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure at each Publisher. During synchronization, this stored procedure is invoked when conflicts are encountered in an article to which the resolver was registered, and information on the conflict row is passed by the Merge Agent to the required parameters of the procedure. Stored procedure-based custom conflict resolvers are always created at the Publisher.  
  
> [!NOTE]  
>  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedure resolvers are only invoked to handle row change-based conflicts. They cannot be used to handle other types of conflicts such as insert failures due to PRIMARY KEY violations or unique index constraint violations.  
  
#### To create a stored procedure-based custom conflict resolver  
  
1.  At the Publisher in either the publication or **msdb** database, create a new system stored procedure that implements the following required parameters:  
  
    |Parameter|Data type|Description|  
    |---------------|---------------|-----------------|  
    |**@tableowner**|**sysname**|Name of the owner of the table for which a conflict is being resolved. This is the owner for the table in the publication database.|  
    |**@tablename**|**sysname**|Name of the table for which a conflict is being resolved.|  
    |**@rowguid**|**uniqueidentifier**|Unique identifier for the row having the conflict.|  
    |**@subscriber**|**sysname**|Name of the server from where a conflicting change is being propagated.|  
    |**@subscriber_db**|**sysname**|Name of the database from where conflicting change is being propagated.|  
    |**@log_conflict OUTPUT**|**int**|Whether the merge process should log a conflict for later resolution:<br /><br /> **0** = Do not log the conflict.<br /><br /> **1** = Subscriber is the conflict loser.<br /><br /> **2** = Publisher is the conflict loser.|  
    |**@conflict_message OUTPUT**|**nvarchar(512)**|Message to be given about the resolution if the conflict is logged.|  
    |**@destowner**|**sysname**|The owner of the published table at the Subscriber.|  
  
     This stored procedure uses the values passed by the Merge Agent to these parameters to implement your custom conflict resolution logic; it must return a single row result set that is identical in structure to the base table and contains the data values for the winning version of the row.  
  
2.  Grant EXECUTE permissions on the stored procedure to any logins used by Subscribers to connect to the Publisher.  
  
#### To use a custom conflict resolver with a new table article  
  
1.  Execute [sp_addmergearticle](../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md) to define an article, specifying a value of **MicrosoftSQL** **Server Stored Procedure Resolver** for the **@article_resolver** parameter and the name of the stored procedure that implements the conflict resolver logic for the **@resolver_info** parameter. For more information, see [Define an Article](../../relational-databases/replication/publish/define-an-article.md).  
  
#### To use a custom conflict resolver with an existing table article  
  
1.  Execute [sp_changemergearticle](../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md), specifying **@publication**, **@article**, a value of **article_resolver** for **@property**, and a value of **MicrosoftSQL** **Server Stored ProcedureResolver** for **@value**.  
  
2.  Execute [sp_changemergearticle](../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md), specifying **@publication**, **@article**, a value of **resolver_info** for **@property**, and the name of the stored procedure that implements the conflict resolver logic for **@value**.  
  
##  <a name="COM"></a> Using a COM-based Custom Resolver  
 The <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport> namespace implements an interface that enables you to write complex business logic to handle events and resolve conflicts that occur during the merge replication synchronization process. For more information, see [Implement a Business Logic Handler for a Merge Article](../../relational-databases/replication/implement-a-business-logic-handler-for-a-merge-article.md). You can also write your own native code-based custom business logic to resolve conflicts. This logic is built as a COM component and compiled into dynamic-link libraries (DLL), using products such as [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual C++. Such a COM–based custom conflict resolver must implement the **ICustomResolver** interface, which is designed specifically for conflict resolution.  
  
#### To create and register a COM-based custom conflict resolver  
  
1.  In a COM-compatible authoring environment, add references to the Custom Conflict Resolver library.  
  
2.  For a Visual C++ project, use the #import directive to import this library into your project.  
  
3.  Create a class that implements the **ICustomResolver** interface.  
  
4.  Implement certain methods and properties.  
  
5.  Build the project to create the custom conflict resolver library file.  
  
6.  Deploy the library in the directory containing the merge agent executable (usually \Microsoft SQL Server\100\COM).  
  
    > [!NOTE]  
    >  A custom conflict resolver must be deployed at the Subscriber for a pull subscription, at the Distributor for a push subscription, or at the Web server used with Web synchronization.  
  
7.  Register the custom conflict resolver library using regsvr32.exe from the deployment directory as follows:  
  
    ```  
    regsvr32.exe mycustomresolver.dll  
    ```  
  
8.  At the Publisher, execute [sp_enumcustomresolvers &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-enumcustomresolvers-transact-sql.md) to verify that the library is not already registered as a custom conflict resolver.  
  
9. To register the library as a custom conflict resolver, execute [sp_registercustomresolver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-registercustomresolver-transact-sql.md), at the Distributor. Specify the friendly name of the COM object for **@article_resolver**, the library's ID (CLSID) for **@resolver_clsid**, and a value of **false** for **@is_dotnet_assembly**.  
  
    > [!NOTE]  
    >  When no longer needed, a custom conflict resolver can be unregistered using [sp_unregistercustomresolver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-unregistercustomresolver-transact-sql.md).  
  
10. (Optional) On a cluster, repeat steps 5-8 to register the custom resolver on all nodes of the cluster. This is required to ensure that the custom resolver will be able to properly load the reconciler following a failover.  
  
#### To use a custom conflict resolver with a new table article  
  
1.  At the Publisher, execute [sp_enumcustomresolvers &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-enumcustomresolvers-transact-sql.md) and note the friendly name of the desired resolver.  
  
2.  At the Publisher on the publication database, execute [sp_addmergearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md) to define an article. Specify the friendly name of the article resolver from step 1 for **@article_resolver**. For more information, see [Define an Article](../../relational-databases/replication/publish/define-an-article.md).  
  
#### To use a custom conflict resolver with an existing table article  
  
1.  At the Publisher, execute [sp_enumcustomresolvers &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-enumcustomresolvers-transact-sql.md) and note the friendly name of the desired resolver.  
  
2.  Execute [sp_changemergearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md), specifying **@publication**, **@article**, a value of **article_resolver** for **@property**, and the friendly name of the article resolver from step 1 for **@value**.  
  

## See Also  
 [Advanced Merge Replication Conflict Detection and Resolution](../../relational-databases/replication/merge/advanced-merge-replication-conflict-detection-and-resolution.md)   
 [COM-Based Custom Resolvers](../../relational-databases/replication/merge/advanced-merge-replication-conflict-com-based-custom-resolvers.md)   
 [Replication Security Best Practices](../../relational-databases/replication/security/replication-security-best-practices.md)  
  
  
