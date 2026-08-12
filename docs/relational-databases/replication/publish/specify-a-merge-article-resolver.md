---
title: Specify a Merge Article Resolver
description: Learn how to specify a merge article resolver using SQL Server Management Studio (SSMS).
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/12/2026
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "articles [SQL Server replication], conflict resolution"
  - "conflict resolution [SQL Server replication], merge replication"
  - "merge replication conflict resolution [SQL Server replication], merge article resolvers"
---
# Specify a merge article resolver

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

This article describes how to specify a merge article resolver in [!INCLUDE [ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../../includes/tsql-md.md)].

## Recommendations

- Merge replication supports the following types of article resolvers:

  - The default resolver. The behavior of the default resolver depends on whether the subscription is a client subscription or a server subscription. For more information about specifying subscription type, see [Specify a Merge Subscription Type and Conflict Resolution Priority](../specify-a-merge-subscription-type-and-conflict-resolution-priority.md).

  - A custom resolver you write, which can be a business logic handler (written in managed code) or a custom COM-based resolver. For more information, see [Advanced Merge Replication - Conflict Detection and Resolution](../merge/advanced-merge-replication-conflict-detection-and-resolution.md). If you need to implement custom logic that runs for each replicated row, not just for conflicting rows, see [Implement a Business Logic Handler for a Merge Article](../implement-a-business-logic-handler-for-a-merge-article.md).

  - A standard COM-based resolver, which is included with [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)].

- To use a resolver other than the default resolver, you must copy the resolver to the computer where the Merge Agent runs and register it. If you're using a business logic handler, you must also register it at the Publisher. The Merge Agent runs at:

  - The Distributor for a push subscription

  - The Subscriber for a pull subscription

  - The Internet Information Services (IIS) server for a pull subscription that uses Web synchronization

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

After you register the resolver, specify that an article should use the resolver on the **Resolver** tab of the **Article Properties - \<Article>** dialog box. You can access this dialog box in the New Publication Wizard and the **Publication Properties - \<Publication>** dialog box. For more information about using the wizard and accessing the dialog box, see [Create a publication](create-a-publication.md) and [View and Modify Publication Properties](view-and-modify-publication-properties.md).

1. On the **Articles** page of the New Publication Wizard or the **Publication Properties - \<Publication>** dialog box, select a table.

1. Select **Article Properties**, and then select **Set Properties of Highlighted Table Article**.

1. On the **Article Properties - \<Article>** page, select the **Resolver** tab.

1. Select **Use a custom resolver (registered at the Distributor)**, and then select the resolver in the list.

1. If the resolver requires input (such as a column name), enter it in the **Enter information needed by the resolver** text box.

1. Select **OK**.

1. Repeat this process for each article that requires a resolver.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

1. If you plan to register your own custom conflict resolver, create one of the following types:

   - Managed code-based resolver as a business logic handler. For more information, see [Implement a Business Logic Handler for a Merge Article](../implement-a-business-logic-handler-for-a-merge-article.md).

   - Stored procedure-based resolver and COM-based resolver. For more information, see [Implement a custom conflict resolver for a Merge article](../implement-a-custom-conflict-resolver-for-a-merge-article.md).

1. To determine if the desired resolver is already registered, execute [sp_enumcustomresolvers](../../system-stored-procedures/sp-enumcustomresolvers-transact-sql.md) at the Publisher on any database. This procedure returns a description of the custom resolver, the class identifier (CLSID) for each COM-based resolver registered at the Distributor, and information on the managed assembly for each business logic handler registered at the Distributor.

1. If the desired custom resolver isn't already registered, execute [sp_registercustomresolver](../../system-stored-procedures/sp-registercustomresolver-transact-sql.md) at the Distributor. Specify a name for the resolver for `@article_resolver`. For a business logic handler, this name is the friendly name of the assembly. For COM-based resolvers, specify the CLSID of the DLL for `@resolver_clsid`. For a business logic handler, specify a value of `true` for `@is_dotnet_assembly`, the name of the assembly for `@dotnet_assembly_name`, and the fully qualified name of the class that overrides <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule> for `@dotnet_class_name`.

   > [!NOTE]  
   > If you don't deploy a business logic handler assembly in the same directory as the Merge Agent executable, in the same directory as the application that synchronously starts the Merge Agent, or in the global assembly cache (GAC), you need to specify the full path with the assembly name for `@dotnet_assembly_name`.

1. If the resolver is a COM-based resolver:

   - Copy the custom resolver DLL to the Distributor for push subscriptions or to the Subscriber for pull subscriptions.

     > [!NOTE]  
     > [!INCLUDE [msCoName](../../../includes/msconame-md.md)] custom resolvers are in the `<drive>:\Program Files\Microsoft SQL Server\<nnn>\COM` directory.

   - Use `regsvr32.exe` to register the custom resolver DLL with the operating system. For example, the following command registers the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Additive Conflict Resolver:

     ```console
     regsvr32 ssradd.dll
     ```

1. If the resolver is a business logic handler, deploy the assembly in the same folder as the Merge Agent executable (replmerg.exe), in the same folder as an application that invokes the Merge Agent, or in the folder specified for the `@dotnet_assembly_name` parameter in step 3.

   > [!NOTE]  
   > The default installation location of the Merge Agent executable is `<drive>:\Program Files\Microsoft SQL Server\<nnn>\COM`.

## Specify a custom resolver when defining a merge article

1. If you plan to use a custom conflict resolver, create and register the resolver by following the preceding procedure.

1. At the Publisher, execute [sp_enumcustomresolvers](../../system-stored-procedures/sp-enumcustomresolvers-transact-sql.md) and note the name of the desired custom resolver in the `value` field of the result set.

1. At the Publisher on the publication database, execute [sp_addmergearticle](../../system-stored-procedures/sp-addmergearticle-transact-sql.md). Specify the name of the resolver from step 2 for `@article_resolver` and any required input to the custom resolver using the `@resolver_info` parameter. For stored procedure-based custom resolvers, `@resolver_info` is the name of the stored procedure. For more information about required input for resolvers supplied by [!INCLUDE [msCoName](../../../includes/msconame-md.md)], see [Advanced Merge Replication Conflict - COM-Based Resolvers](../merge/advanced-merge-replication-conflict-com-based-resolvers.md).

## Specify or change a custom resolver for an existing merge article

1. To determine if a custom resolver is defined for an article or to get the name of the resolver, execute [sp_helpmergearticle](../../system-stored-procedures/sp-helpmergearticle-transact-sql.md). If there's a custom resolver defined for the article, its name appears in the `article_resolver` field. The `resolver_info` field shows any input supplied to the resolver.

1. At the Publisher, execute [sp_enumcustomresolvers](../../system-stored-procedures/sp-enumcustomresolvers-transact-sql.md) and note the name of the desired custom resolver in the `value` field of the result set.

1. At the Publisher on the publication database, execute [sp_changemergearticle](../../system-stored-procedures/sp-changemergearticle-transact-sql.md). Specify a value of `article_resolver`, including the full path for business logic handlers, for `@property`, and the name of the desired custom resolver from step 2 for `@value`.

1. To change any required input for the custom resolver, execute [sp_changemergearticle](../../system-stored-procedures/sp-changemergearticle-transact-sql.md) again. Specify a value of `resolver_info` for `@property` and any required input to the custom resolver for `@value`. For stored procedure-based custom resolvers, `@resolver_info` is the name of the stored procedure. For more information about required input, see [Advanced Merge Replication Conflict - COM-Based Resolvers](../merge/advanced-merge-replication-conflict-com-based-resolvers.md).

## Unregister a custom conflict resolver

1. At the Publisher, execute [sp_enumcustomresolvers](../../system-stored-procedures/sp-enumcustomresolvers-transact-sql.md) and note the name of the custom resolver to remove in the `value` field of the result set.

1. At the Distributor, execute [sp_unregistercustomresolver](../../system-stored-procedures/sp-unregistercustomresolver-transact-sql.md). Specify the full name of the custom resolver from step 1 for `@article_resolver`.

<a id="TsqlExample"></a>

### Examples (Transact-SQL)

This example creates a new article and specifies that the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Averaging Conflict Resolver be used to calculate the average of the `UnitPrice` column when conflicts occur.

:::code language="sql" source="../codesnippet/tsql/specify-a-merge-article-_1.sql":::

This example changes an article to specify using the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Additive Conflict Resolver to calculate the sum of the `UnitsOnOrder` column when conflicts occur.

:::code language="sql" source="../codesnippet/tsql/specify-a-merge-article-_2.sql":::

## Related content

- [Advanced Merge Replication - Conflict Detection and Resolution](../merge/advanced-merge-replication-conflict-detection-and-resolution.md)
- [Implement a Business Logic Handler for a Merge Article](../implement-a-business-logic-handler-for-a-merge-article.md)
