---
title: "Specify Schema Options | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "schemas [SQL Server replication], options"
  - "articles [SQL Server replication], transactional replication options"
  - "articles [SQL Server replication], merge replication options"
  - "articles [SQL Server replication], schema options"
ms.assetid: 1f85a479-bd6e-4023-abf7-7435a7e5b567
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Specify Schema Options
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to specify schema options in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)]. When you are publishing a table or view, you can control the object creation options that are replicated for the published object. You can set these option when the article is created, and you can also change them at a later time. If you do not explicitly specify these options for an article, a default set of options will be defined.  
  
> [!NOTE]  
>  The default schema options when using replication stored procedures may differ from the default options when articles are added using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Recommendations](#Recommendations)  
  
-   **To specify schema options, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   If you change schema options after a publication is created, you must generate a new snapshot.  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   For the complete list of schema options, see the **@schema_option** parameter of [sp_addarticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md) and [sp_addmergearticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Specify schema options, such as whether to copy constraints and triggers to Subscribers, on the **Properties** tab of the **Article Properties - \<Article>** dialog box. This tab is available in the New Publication Wizard and the **Publication Properties - \<Publication>** dialog box. For more information about using the wizard and accessing the dialog box, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md) and [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md).  
  
#### To specify schema options  
  
1.  On the **Articles** Page of the New Publication Wizard or **Publication Properties - \<Publication>** dialog box, select an article, and then click **Article Properties**.  
  
2.  Select which articles schema option changes should apply to:  
  
    -   Click **Set Properties of Highlighted \<ObjectType> Article** to launch the **Article Properties - \<ObjectName>** dialog box; property changes made in this dialog box are applied only to the object that is highlighted in the object pane on the **Articles** page.  
  
    -   Click **Set Properties of All \<ObjectType> Articles** to launch the **Properties for All \<ObjectType> Articles** dialog box; property changes made in this dialog box are applied to all objects of that type in the object pane on the **Articles** page, including ones not yet selected for publication.  
  
        > [!NOTE]  
        >  Property changes made in the **Properties for All \<ObjectType> Articles** dialog box override any made previously in the **Article Properties - \<ObjectName>** dialog box. If, for example, you want to set a number of defaults for all articles of an object type, but also want to set some properties for individual objects, set the defaults for all articles first. Then set the properties for the individual objects.  
  
3.  In the **Copy Objects and Settings to Subscriber** and **Destination Object** sections of the **Properties** tab of the **Article Properties - \<Article>** dialog box, specify values for the options.  
  
4.  Modify any properties if necessary, and then click **OK**.  
  
5.  If you are in the **Publication Properties - \<Publication>** dialog box, click **OK** to save and close the dialog box.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 Schema options are specified as a hexadecimal value that is the [| (Bitwise OR)](../../../t-sql/language-elements/bitwise-or-transact-sql.md) result of one or more options. For more information, see [sp_addarticle](../../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md) and [sp_addmergearticle](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md).  
  
> [!NOTE]  
>  You must convert schema option values from **binary** to **int** before performing a bitwise operation. For more information, see [CAST and CONVERT &#40;Transact-SQL&#41;](../../../t-sql/functions/cast-and-convert-transact-sql.md).  
  
#### To specify schema options when defining an article for a snapshot or transactional publication  
  
1.  At the Publisher on the publication database, execute [sp_addarticle](../../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md). Specify the name of the publication to which the article belongs for **@publication**, a name for the article for **@article**, the database object being published for **@source_object**, the type of database object for **@type**, and the [| (Bitwise OR)](../../../t-sql/language-elements/bitwise-or-transact-sql.md) result of one or more schema options for **@schema_option**. For more information, see [Define an Article](../../../relational-databases/replication/publish/define-an-article.md).  
  
#### To specify schema options when defining an article for a merge publication  
  
1.  At the Publisher on the publication database, execute [sp_addmergearticle](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md). Specify the name of the publication to which the article belongs for **@publication**, a name for the article for **@article**, the database object being published for **@source_object**, and the [| (Bitwise OR)](../../../t-sql/language-elements/bitwise-or-transact-sql.md) result of one or more schema options for **@schema_option**. For more information, see [Define an Article](../../../relational-databases/replication/publish/define-an-article.md).  
  
#### To change schema options for an existing article in a snapshot or transactional publication  
  
1.  At the Publisher on the publication database, execute [sp_helparticle](../../../relational-databases/system-stored-procedures/sp-helparticle-transact-sql.md). Specify the name of the publication to which the article belongs for **@publication** and the name of the article for **@article**. Note the value of the **schema_option** column in the result set.  
  
2.  Execute a [& (Bitwise AND)](../../../t-sql/language-elements/bitwise-and-transact-sql.md) operation using the value from step 1 and the desired schema option value to determine if the option is set.  
  
    -   If the result is **0**, the option is not set.  
  
    -   If the result is the option value, the option is already set.  
  
3.  If the option is not set, execute a [| (Bitwise OR)](../../../t-sql/language-elements/bitwise-or-transact-sql.md) operation using the value from step 1 and the desired schema option value.  
  
4.  At the Publisher on the publication database, execute [sp_changearticle](../../../relational-databases/system-stored-procedures/sp-changearticle-transact-sql.md). Specify the name of the publication to which the article belongs for **@publication**, the name of the article for **@article**, a value of **schema_option** for **@property**, and the hexadecimal result from step 3 for **@value**.  
  
5.  Run the Snapshot Agent to generate a new snapshot. For more information, see [Create and Apply the Initial Snapshot](../../../relational-databases/replication/create-and-apply-the-initial-snapshot.md).  
  
#### To change schema options for an existing article in a merge publication  
  
1.  At the Publisher on the publication database, execute [sp_helpmergearticle](../../../relational-databases/system-stored-procedures/sp-helpmergearticle-transact-sql.md). Specify the name of the publication to which the article belongs for **@publication** and the name of the article for **@article**. Note the value of the **schema_option** column in the result set.  
  
2.  Execute a [& (Bitwise AND)](../../../t-sql/language-elements/bitwise-and-transact-sql.md) operation using the value from step 1 and the desired schema option value to determine if the option is set.  
  
    -   If the result is **0**, the option is not set.  
  
    -   If the result is the option value, the option is already set.  
  
3.  If the option is not set, execute a [| (Bitwise OR)](../../../t-sql/language-elements/bitwise-or-transact-sql.md) operation using the value from step 1 and the desired schema option value.  
  
4.  At the Publisher on the publication database, execute [sp_changemergearticle](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md). Specify the name of the publication to which the article belongs for **@publication**, the name of the article for **@article**, a value of **schema_option** for **@property**, and the hexadecimal result from step 3 for **@value**.  
  
5.  Run the Snapshot Agent to generate a new snapshot. For more information, see [Create and Apply the Initial Snapshot](../../../relational-databases/replication/create-and-apply-the-initial-snapshot.md).  
  
## See Also  
 [Publish Data and Database Objects](../../../relational-databases/replication/publish/publish-data-and-database-objects.md)   
 [Article Options for Transactional Replication](../../../relational-databases/replication/transactional/article-options-for-transactional-replication.md)  
  
  
