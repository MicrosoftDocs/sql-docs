---
title: "Publisher Properties - Publisher, Publication Databases | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.rep.configdistwizard.pubproperties.pubdb.f1"
helpviewer_keywords: 
  - "Publisher Properties dialog box"
ms.assetid: 574ea2e7-4e7b-4733-ab52-429ca93c7b0a
caps.latest.revision: 21
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Publisher Properties - Publisher, Publication Databases
  The **Publication Databases** page of the **Publisher Properties** dialog box allows a user in the **sysadmin** fixed server role to enable databases for replication. Enabling a database does not publish that database; rather, it allows any user in the **db_owner** fixed database role for that database to create one or more publications on the database.  
  
## Options  
 **Transactional**  
 Select this check box to allow users in the **db_owner** fixed database role to create snapshot publications or transactional publications in the database.  
  
 **Merge**  
 Select this check box to allow users in the **db_owner** fixed database role to create merge publications in the database.  
  
## See Also  
 [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md)   
 [Properties Reference &#40;Replication&#41;](../../relational-databases/replication/properties-reference-replication.md)  
  
  