---
title: "Implement a Business Logic Handler for a Merge Article | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "merge replication conflict resolution [SQL Server replication], business logic handlers"
  - "merge replication business logic handlers [SQL Server replication]"
  - "conflict resolution [SQL Server replication], merge replication"
  - "business logic handlers [SQL Server replication]"
  - "BusinessLogicModule class"
ms.assetid: ed477595-6d46-4fa2-b0d3-a5358903ec05
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Implement a Business Logic Handler for a Merge Article
  This topic describes how to implement a business logic handler for a merge article in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using replication programming or Replication Management Objects (RMO).  
  
 The <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport> namespace implements an interface that enables you to write complex business logic to handle events that occur during the merge replication synchronization process. Methods in the business logic handler can be invoked by the replication process for each changed row that is replicated during synchronization.  
  
 The general process for implementing a business logic handler is:  
  
1.  Create the business logic hander assembly.  
  
2.  Register the assembly at the Distributor.  
  
3.  Deploy the assembly at the server on which the Merge Agent runs. For a pull subscription the agent runs on the Subscriber, and for a push subscription the agent runs on the Distributor. When you are using Web synchronization, the agent runs on the Web server.  
  
4.  Create an article that uses the business logic handler or modify an existing article to use the business logic handler.  
  
 The business logic handler you specify is executed for every row that is synchronized. Complex logic and calls to other applications or network services can impact performance. For more information about business logic handlers, see [Execute Business Logic During Merge Synchronization](merge/execute-business-logic-during-merge-synchronization.md).  
  
 **In This Topic**  
  
-   **To implement a business logic handler for a merge article, using:**  
  
     [Replication Programming](#ReplProg)  
  
     [Replication Management Objects (RMO)](#RMOProcedure)  
  
##  <a name="ReplProg"></a> Using Replication Programming  
  
#### To create and deploy a business logic handler  
  
1.  In [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Studio, create a new project for the .NET assembly that contains the code that implements the business logic handler.  
  
2.  Add references to the project for the following namespaces.  
  
    |Assembly Reference|Location|  
    |------------------------|--------------|  
    |<xref:Microsoft.SqlServer.Replication.BusinessLogicSupport>|[!INCLUDE[ssInstallPath](../../includes/ssinstallpath-md.md)]COM (default installation)|  
    |<xref:System.Data>|GAC (component of the .NET Framework)|  
    |<xref:System.Data.Common>|GAC (component of the .NET Framework)|  
  
3.  Add a class that overrides the <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule> class.  
  
4.  Implement the <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule.HandledChangeStates%2A> property to indicate the types of changes that are handled.  
  
5.  Override one or more of the following methods of the <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule> class:  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule.CommitHandler%2A> - invoked when a data change is committed during synchronization.  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule.DeleteErrorHandler%2A> - invoked when an error occurs when a DELETE statement is being uploaded or downloaded.  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule.DeleteHandler%2A> - invoked when DELETE statements are being uploaded or downloaded.  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule.InsertErrorHandler%2A> - invoked when an error occurs when an INSERT statement is being uploaded or downloaded.  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule.InsertHandler%2A> - invoked when INSERT statements are being uploaded or downloaded.  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule.UpdateConflictsHandler%2A> - invoked when conflicting UPDATE statements occur at the Publisher and Subscriber.  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule.UpdateDeleteConflictHandler%2A> - invoked when UPDATE statements conflict with DELETE statements at the Publisher and Subscriber.  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule.UpdateErrorHandler%2A> - invoked when an error occurs when an UPDATE statement is being uploaded or downloaded.  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule.UpdateHandler%2A> - invoked when UPDATE statements are being uploaded or downloaded.  
  
6.  Build the project to create the business logic handler assembly.  
  
7.  Deploy the assembly in the directory that contains the Merge Agent executable file (replmerg.exe), which for a default installation is [!INCLUDE[ssInstallPath](../../includes/ssinstallpath-md.md)]COM, or install it in the .NET global assembly cache (GAC). You should only install the assembly in the GAC if applications other than the Merge Agent require access to the assembly. The assembly can be installed into the GAC using the Global Assembly Cache tool (**Gacutil.exe)** provided in the .NET Framework SDK.  
  
    > [!NOTE]  
    >  A business logic handler must be deployed on every server on which the Merge Agent runs, which includes the IIS server that hosts the replisapi.dll when using Web synchronization.  
  
#### To register a business logic handler  
  
1.  At the Publisher, execute [sp_enumcustomresolvers &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-enumcustomresolvers-transact-sql) to verify that the assembly has not already been registered as a business logic handler.  
  
2.  At the Distributor, execute [sp_registercustomresolver &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-registercustomresolver-transact-sql), specifying a friendly name for the business logic handler for **@article_resolver**, a value of `true` for **@is_dotnet_assembly**, the name of the assembly for **@dotnet_assembly_name**, and the fully-qualified name of the class that overrides <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule> for **@dotnet_class_name**.  
  
    > [!NOTE]  
    >  If the assembly is not deployed in the same directory as the Merge Agent executable, in the same directory as the application that synchronously starts the Merge Agent, or in the global assembly cache (GAC), you need to specify the full path with the assembly name for **@dotnet_assembly_name**. When using Web synchronization, you must specify the location of assembly at the Web server.  
  
#### To use a business logic handler with a new table article  
  
1.  Execute [sp_addmergearticle &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql) to define an article, specifying the friendly name of the business logic handler for **@article_resolver**. For more information, see [Define an Article](publish/define-an-article.md).  
  
#### To use a business logic handler with an existing table article  
  
1.  Execute [sp_changemergearticle &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql), specifying **@publication**, **@article**, a value of **article_resolver** for **@property**, and the friendly name of the business logic handler for **@value**.  
  
###  <a name="TsqlExample"></a> Examples (Replication Programming)  
 This example shows a business logic handler that creates an audit log.  
  
 [!code-csharp[HowTo#rmo_BusinessLogicCode](../../snippets/csharp/SQL15/replication/howto/cs/businesslogic.cs#rmo_businesslogiccode)]  
  
 [!code-vb[HowTo#rmo_vb_BusinessLogicCode](../../snippets/visualbasic/SQL15/replication/howto/vb/businesslogic.vb#rmo_vb_businesslogiccode)]  
  
 The following example registers a business logic handler assembly at the Distributor and changes an existing merge article to use this custom business logic.  
  
 [!code-sql[HowTo#sp_RegisterBLH_10](../../snippets/tsql/SQL15/replication/howto/tsql/registerblh_10.sql#sp_registerblh_10)]  
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
  
#### To create a business logic handler  
  
1.  In [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Studio, create a new project for the .NET assembly that contains the code that implements the business logic handler.  
  
2.  Add references to the project for the following namespaces.  
  
    |Assembly Reference|Location|  
    |------------------------|--------------|  
    |<xref:Microsoft.SqlServer.Replication.BusinessLogicSupport>|[!INCLUDE[ssInstallPath](../../includes/ssinstallpath-md.md)]COM (default installation)|  
    |<xref:System.Data>|GAC (component of the .NET Framework)|  
    |<xref:System.Data.Common>|GAC (component of the .NET Framework)|  
  
3.  Add a class that overrides the <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule> class.  
  
4.  Implement the <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule.HandledChangeStates%2A> property to indicate the types of changes that are handled.  
  
5.  Override one or more of the following methods of the <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule> class:  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule.CommitHandler%2A> - invoked when a data change is committed during synchronization.  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule.DeleteErrorHandler%2A> - invoked if an error occurs while a DELETE statement is being uploaded or downloaded.  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule.DeleteHandler%2A> - invoked when DELETE statements are being uploaded or downloaded.  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule.InsertErrorHandler%2A> - invoked if an error occurs when an INSERT statement is being uploaded or downloaded.  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule.InsertHandler%2A> - invoked when INSERT statements are being uploaded or downloaded.  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule.UpdateConflictsHandler%2A> - invoked when conflicting UPDATE statements occur at the Publisher and Subscriber.  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule.UpdateDeleteConflictHandler%2A> - invoked when UPDATE statements conflict with DELETE statements at the Publisher and Subscriber.  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule.UpdateErrorHandler%2A> - invoked if an error occurs when an UPDATE statement is being uploaded or downloaded.  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule.UpdateHandler%2A> - invoked when UPDATE statements are being uploaded or downloaded.  
  
    > [!NOTE]  
    >  Any article conflicts not explicitly handled by your custom business logic are handled by the default resolver for the article.  
  
6.  Build the project to create the business logic handler assembly.  
  
#### To register a business logic handler  
  
1.  Create a connection to the Distributor by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.ReplicationServer> class. Pass the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> from step 1.  
  
3.  Call <xref:Microsoft.SqlServer.Replication.ReplicationServer.EnumBusinessLogicHandlers%2A> and check the returned <xref:System.Collections.ArrayList> object to ensure that the assembly has not already been registered as a business logic handler.  
  
4.  Create an instance of the <xref:Microsoft.SqlServer.Replication.BusinessLogicHandler> class. Specify the following properties:  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicHandler.DotNetAssemblyName%2A> - the name of the .NET assembly. If the assembly is not deployed in the same directory as the Merge Agent executable, in the same directory as the application that synchronously starts the Merge Agent, or in the GAC, you must include the full path with the assembly name. You must include the full path with the assembly name when using a business logic handler with Web synchronization.  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicHandler.DotNetClassName%2A> - the fully-qualified name of the class that overrides <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule> and implements the business logic handler.  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicHandler.FriendlyName%2A> - a friendly name you use when you access the business logic handler.  
  
    -   <xref:Microsoft.SqlServer.Replication.BusinessLogicHandler.IsDotNetAssembly%2A> - a value of `true`.  
  
#### To deploy a business logic handler  
  
1.  Deploy the assembly on the server where the Merge Agent runs in the file location specified when the business logic handler was registered at the Distributor. For a pull subscription the agent runs on the Subscriber, and for a push subscription the agent runs on the Distributor. When you are using Web synchronization, the agent runs on the Web server. If the full path was not included with the assembly name when the business logic handler was registered, deploy the assembly in the same directory as the Merge Agent executable, in the same directory as the application that synchronously starts the Merge Agent. You may install the assembly in the GAC if there are multiple applications that use the same assembly.  
  
#### To use a business logic handler with a new table article  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergeArticle> class. Set the following properties:  
  
    -   The name of the article for <xref:Microsoft.SqlServer.Replication.Article.Name%2A>.  
  
    -   The name of the publication for <xref:Microsoft.SqlServer.Replication.Article.PublicationName%2A>.  
  
    -   The name of the publication database for <xref:Microsoft.SqlServer.Replication.Article.DatabaseName%2A>.  
  
    -   The friendly name of the business logic handler (<xref:Microsoft.SqlServer.Replication.BusinessLogicHandler.FriendlyName%2A>) for <xref:Microsoft.SqlServer.Replication.MergeArticle.ArticleResolver%2A>.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.Article.Create%2A> method. For more information, see [Define an Article](publish/define-an-article.md).  
  
#### To use a business logic handler with an existing table article  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergeArticle> class.  
  
3.  Set the <xref:Microsoft.SqlServer.Replication.Article.Name%2A>, <xref:Microsoft.SqlServer.Replication.Article.PublicationName%2A>, and <xref:Microsoft.SqlServer.Replication.Article.DatabaseName%2A> properties.  
  
4.  Set the connection from step 1 for the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property.  
  
5.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object. If this method returns `false`, either the article properties in step 3 were defined incorrectly or the article does not exist. For more information, see [View and Modify Article Properties](publish/view-and-modify-article-properties.md).  
  
6.  Set the friendly name of the business logic handler for <xref:Microsoft.SqlServer.Replication.MergeArticle.ArticleResolver%2A>. This is the value of the <xref:Microsoft.SqlServer.Replication.BusinessLogicHandler.FriendlyName%2A> property specified when registering the business logic handler.  
  
###  <a name="PShellExample"></a> Examples (RMO)  
 This example is a business logic handler that logs information about inserts, updates, and deletes at the Subscriber.  
  
 [!code-csharp[HowTo#rmo_BusinessLogicCode](../../snippets/csharp/SQL15/replication/howto/cs/businesslogic.cs#rmo_businesslogiccode)]  
  
 [!code-vb[HowTo#rmo_vb_BusinessLogicCode](../../snippets/visualbasic/SQL15/replication/howto/vb/businesslogic.vb#rmo_vb_businesslogiccode)]  
  
 This example registers a business logic handler at the Distributor.  
  
 [!code-csharp[HowTo#rmo_RegisterBLH_10](../../snippets/csharp/SQL15/replication/howto/cs/rmotestevelope.cs#rmo_registerblh_10)]  
  
 [!code-vb[HowTo#rmo_vb_RegisterBLH_10](../../snippets/visualbasic/SQL15/replication/howto/vb/rmotestenv.vb#rmo_vb_registerblh_10)]  
  
 This example changes an existing article to use the business logic handler.  
  
 [!code-csharp[HowTo#rmo_ChangeMergeArticle_BLH](../../snippets/csharp/SQL15/replication/howto/cs/rmotestevelope.cs#rmo_changemergearticle_blh)]  
  
 [!code-vb[HowTo#rmo_vb_ChangeMergeArticle_BLH](../../snippets/visualbasic/SQL15/replication/howto/vb/rmotestenv.vb#rmo_vb_changemergearticle_blh)]  
  
## See Also  
 [Implement a Custom Conflict Resolver for a Merge Article](implement-a-custom-conflict-resolver-for-a-merge-article.md)   
 [Debug a Business Logic Handler &#40;Replication Programming&#41;](debug-a-business-logic-handler-replication-programming.md)   
 [Replication Security Best Practices](security/replication-security-best-practices.md)   
 [Replication Management Objects Concepts](concepts/replication-management-objects-concepts.md)  
  
  
