---
title: "Add & drop publication articles (SSMS)"
description: Describes how to add articles to and drop articles from a publication using SQL Server Management Studio (SSMS).
ms.custom: seo-lt-2019
ms.date: "03/07/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "articles [SQL Server replication], dropping"
  - "deleting articles"
  - "dropping articles"
  - "adding articles"
  - "articles [SQL Server replication], adding"
ms.assetid: d5a3e536-62d2-4473-a178-877ba52f7d7f
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Add Articles to and Drop Articles from a Publication
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  Initially add articles to a publication when you create it in the New Publication Wizard. For more information about using this wizard, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md).  
  
 After a publication is created, add and delete articles on the **Articles** page of the **Publication Properties - \<Publication>** dialog box. For more information about accessing this dialog box, see [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md). For information about the considerations for adding and dropping articles, see [Add Articles to and Drop Articles from Existing Publications](../../../relational-databases/replication/publish/add-articles-to-and-drop-articles-from-existing-publications.md).  
  
### To add an article after a publication is created  
  
1.  On the **Articles** page of the **Publication Properties - \<Publication>** dialog box, clear the **Show only checked objects in the list** check box. This allows you to see the unpublished objects in the publication database.  
  
2.  Select the check box next to each article you want to add.  
  
3.  Select **OK**.
  
### To delete an article  
  
1.  On the **Articles** page of the **Publication Properties - \<Publication>** dialog box, clear the check box next to each article you want to delete.  
  
2.  Select **OK**.
  
## See Also  
 [Define an Article](../../../relational-databases/replication/publish/define-an-article.md)   
 [Publish Data and Database Objects](../../../relational-databases/replication/publish/publish-data-and-database-objects.md)  
  
  
