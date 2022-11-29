---
title: "Implement custom conflict resolver (Merge)"
description: Learn how to implement a custom conflict resolver for a Merge Publication in SQL Server.
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
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
---
# Implement a custom conflict resolver for a Merge article
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to implement a custom conflict resolver for a Merge article in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[tsql](../../includes/tsql-md.md)] or a [COM-based custom resolver](../../relational-databases/replication/merge/advanced-merge-replication-conflict-com-based-custom-resolvers.md).  
  
 **In this topic**  
  
-   **Implement a custom conflict resolver for a Merge article, using:**  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [A COM-based resolver](#COM)  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 You can write your own custom conflict resolver as a [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure at each Publisher. During synchronization, this stored procedure is invoked when conflicts are encountered in an article that the resolver was registered to. Information about the conflict row is passed by the Merge Agent to the required parameters of the procedure. Stored procedure-based custom conflict resolvers are always created at the Publisher.  
  
> [!NOTE]  
>  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedure resolvers are invoked only to handle row change-based conflicts. They can't be used to handle other types of conflicts like insert failures triggered by PRIMARY KEY violations or unique index constraint violations.
  
#### To create a stored procedure-based custom conflict resolver  
  
1.  At the Publisher in either the publication or **msdb** database, create a new system stored procedure that implements the following required parameters:  
  
    |Parameter|Data type|Description|  
    |---------------|---------------|-----------------|  
    |**\@tableowner**|**sysname**|Name of the owner of the table for which a conflict is being resolved. This is the owner for the table in the publication database.|  
    |**\@tablename**|**sysname**|Name of the table for which a conflict is being resolved.|  
    |**\@rowguid**|**uniqueidentifier**|Unique identifier for the row that has the conflict.|  
    |**\@subscriber**|**sysname**|Name of the server from which a conflicting change is being propagated.|  
    |**\@subscriber_db**|**sysname**|Name of the database from which a conflicting change is being propagated.|  
    |**\@log_conflict OUTPUT**|**int**|Sets whether the merge process should log a conflict for later resolution:<br /><br /> **0** = Do not log the conflict.<br /><br /> **1** = Subscriber is the conflict loser.<br /><br /> **2** = Publisher is the conflict loser.|  
    |**\@conflict_message OUTPUT**|**nvarchar(512)**|Message to be given about the resolution if the conflict is logged.|  
    |**\@destowner**|**sysname**|The owner of the published table at the Subscriber.|  
  
     This stored procedure uses the values passed by the Merge Agent to these parameters to implement your custom conflict resolution logic. It must return a single row result set that's identical in structure to the base table and that contains the data values for the winning version of the row.  
  
2.  Grant EXECUTE permissions on the stored procedure to any logins used by Subscribers to connect to the Publisher.  

#### Use a custom conflict resolver with a new table article  
  
1. Execute [sp_addmergearticle](../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md) to define an article. 
1. Specify a value of **MicrosoftSQL** **Server Stored Procedure Resolver** for the **\@article_resolver** parameter. 
1. Specify the name of the stored procedure that implements the conflict resolver logic for the **\@resolver_info** parameter. 

   For more information, see [Define an article](../../relational-databases/replication/publish/define-an-article.md).
  
#### To use a custom conflict resolver with an existing table article  
  
1.  Execute [sp_changemergearticle](../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md), specifying **\@publication**, **\@article**, a value of **article_resolver** for **\@property**, and a value of **MicrosoftSQL** **Server Stored ProcedureResolver** for **\@value**.  
  
2.  Execute [sp_changemergearticle](../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md), specifying **\@publication**, **\@article**, a value of **resolver_info** for **\@property**, and the name of the stored procedure that implements the conflict resolver logic for **\@value**.  
  
##  <a name="COM"></a> Using a COM-based custom resolver  
 The <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport> namespace implements an interface that enables you to write complex business logic to handle events and to resolve conflicts that occur during the Merge replication synchronization process. For more information, see [Implement a Business Logic Handler for a Merge article](../../relational-databases/replication/implement-a-business-logic-handler-for-a-merge-article.md). You can also write your own native code-based custom business logic to resolve conflicts. This logic is built as a COM component and compiled into dynamic-link libraries (DLLs), using products such as [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual C++. This kind of COM-based custom conflict resolver must implement the **ICustomResolver** interface, which is designed specifically for conflict resolution.  
  
#### To create and register a COM-based custom conflict resolver  
  
1.  In a COM-compatible authoring environment, add references to the Custom Conflict Resolver library.  
  
2.  For a Visual C++ project, use the #import directive to import this library into your project.  
  
3.  Create a class that implements the **ICustomResolver** interface.  
  
4.  Implement certain methods and properties.  
  
5.  Build the project to create the custom conflict resolver library file.  
  
6.  Deploy the library in the directory that contains the Merge Agent executable (usually \Microsoft SQL Server\100\COM).  
  
    > [!NOTE]  
    >  A custom conflict resolver must be deployed at the Subscriber for a pull subscription, at the Distributor for a push subscription, or at the Web server used with Web synchronization.  
  
7.  Register the custom conflict resolver library by running regsvr32.exe from the deployment directory as follows:  
  
    ```  
    regsvr32.exe mycustomresolver.dll  
    ```  
  
8.  At the Publisher, execute [sp_enumcustomresolvers &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-enumcustomresolvers-transact-sql.md) to verify that the library isn't already registered as a custom conflict resolver.  
  
9. To register the library as a custom conflict resolver, execute [sp_registercustomresolver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-registercustomresolver-transact-sql.md) at the Distributor. Specify the friendly name of the COM object for **\@article_resolver**, the library's ID (CLSID) for **\@resolver_clsid**, and a value of **false** for **\@is_dotnet_assembly**.  
  
    > [!NOTE]  
    >  When it's no longer needed, you can unregister a custom conflict resolver by using [sp_unregistercustomresolver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-unregistercustomresolver-transact-sql.md).  
  
10. (Optional) On a cluster, repeat steps 6-9 to register the custom resolver on all nodes of the cluster. These steps are required to make sure that the custom resolver can properly load the reconciler following a failover.
  
#### To use a custom conflict resolver with a new table article  
  
1.  At the Publisher, execute [sp_enumcustomresolvers &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-enumcustomresolvers-transact-sql.md), and note the friendly name of the desired resolver.  
  
2.  At the Publisher on the publication database, execute [sp_addmergearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md) to define an article. Specify the friendly name of the article resolver from step 1 for **\@article_resolver**. For more information, see [Define an article](../../relational-databases/replication/publish/define-an-article.md).  
  
#### To use a custom conflict resolver with an existing table article  
  
1.  At the Publisher, execute [sp_enumcustomresolvers &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-enumcustomresolvers-transact-sql.md), and note the friendly name of the desired resolver.  
  
2.  Execute [sp_changemergearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md), specifying **\@publication**, **\@article**, a value of **article_resolver** for **\@property**, and the friendly name of the article resolver from step 1 for **\@value**.  
  

## See also  
 [Advanced Merge replication conflict detection and resolution](../../relational-databases/replication/merge/advanced-merge-replication-conflict-detection-and-resolution.md)   
 [COM-based custom resolvers](../../relational-databases/replication/merge/advanced-merge-replication-conflict-com-based-custom-resolvers.md)   
 [Replication security best practices](../../relational-databases/replication/security/replication-security-best-practices.md)  
  
  
