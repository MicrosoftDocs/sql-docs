---
title: "Publisher Properties - Distributor | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "replication"
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.configdistwizard.distpubproperties.f1"
helpviewer_keywords: 
  - "Publisher Properties dialog box"
ms.assetid: ab6ada76-0f99-43fe-b524-baac7b1bc483
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Publisher Properties - Distributor
  The **Publisher Properties** dialog box allows you to view and modify properties associated with the relationship between the Publisher and its Distributor.  
  
## Options  
 **Agent Connection to the Publisher**  
 Specify the context under which the following agents make connections from the Distributor to the Publisher:  
  
-   The Queue Reader Agent for transactional publications that allow queued updating subscriptions.  
  
-   The Snapshot Agent and Log Reader Agent for Oracle publications.  
  
 Select **Impersonate agent process account** to make connections to the Publisher using the context of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account under which these agents run, or specify **SQL Server Authentication**, and then enter a value for **Login** and **Password**. It is recommended that you select **Impersonate agent process account**. For more information on agent security, see [Replication Agent Security Model](security/replication-agent-security-model.md).  
  
 The Windows accounts under which these agents run are specified in the New Publication Wizard. These accounts can be changed:  
  
-   In the **Distributor Properties** dialog box for the Queue Reader Agent.  
  
-   In the **Publication Properties** dialog box for the Snapshot Agent and Log Reader Agent.  
  
 **Miscellaneous**  
 The properties **Publisher Type** and **Distribution Database Name** are read-only. The property **Default Snapshot Folder** can be changed. For more information about the snapshot folder, see [Secure the Snapshot Folder](security/secure-the-snapshot-folder.md).  
  
## See Also  
 [Create a Publication](publish/create-a-publication.md)   
 [View and Modify Distributor and Publisher Properties](view-and-modify-distributor-and-publisher-properties.md)   

  
  
