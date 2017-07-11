---
title: "Distributor Properties, General | Microsoft Docs"
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
  - "sql13.rep.configdistwizard.distproperties.general.f1"
helpviewer_keywords: 
  - "Distributor Properties dialog box"
ms.assetid: ab4120ec-e524-4c0c-8b48-f2f40adb1a3b
caps.latest.revision: 22
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Distributor Properties, General
  The **General** page of the **Distributor Properties** dialog box allows you to add and delete distribution databases and set distribution database properties.  
  
 The distribution database stores metadata and history data for all types of replication, and transactions for transactional replication. In many cases, a single distribution database is sufficient. But if multiple Publishers use a single Distributor, consider creating a distribution database for each Publisher. Doing so ensures that the data flowing through each distribution database is distinct.  
  
## Options  
 **Databases**  
 The **Databases** property grid shows the name and retention properties of the distribution databases on the Distributor. **Transaction Retention** is the length of time transactions are stored for transactional replication (transaction retention is also known as distribution retention). **History Retention** is the length of time history metadata is stored for all types of replication. For more information about distribution retention, see [Subscription Expiration and Deactivation](../../relational-databases/replication/subscription-expiration-and-deactivation.md).  
  
 Click the properties button (**...**) in the **Databases** property grid to launch the **Distribution Database Properties** dialog box.  
  
 **New**  
 Click to create a new distribution database.  
  
 **Delete**  
 Select an existing distribution database in the **Databases** property grid, and click **Delete** to delete the database. You cannot delete the distribution database if there is only one such database; each Distributor must have at least one distribution database. To delete all distribution databases, you must disable distribution on the computer. For more information, see [Disable Publishing and Distribution](../../relational-databases/replication/disable-publishing-and-distribution.md).  
  
 **Profile Defaults**  
 Click to access replication agent profiles in the **Agent Profiles** dialog box. For more information about profiles, see [Replication Agent Profiles](../../relational-databases/replication/agents/replication-agent-profiles.md).  
  
## See Also  
 [Configure Distribution](../../relational-databases/replication/configure-distribution.md)   
 [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md)  
  
  