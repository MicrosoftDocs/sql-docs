---
title: "Publication Properties, Publication Access List | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "replication"
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.newpubwizard.pubproperties.publicationaccesslist.f1"
ms.assetid: 9587bb9e-c66c-4e70-8171-09b943ec2d50
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Publication Properties, Publication Access List
  The **Publication Access List** page of the **Publication Properties** dialog box allows you to add and remove logins, accounts, and groups from the publication access list (PAL). The PAL is the primary mechanism for securing the Publisher. When you create a publication, replication creates a PAL for the publication. The PAL, which functions similarly to a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows access control list, contains a list of logins, accounts, and groups that are granted access to the publication.  
  
 When a Subscriber connects to the Publisher or Distributor and requests access to a publication, the login of the Subscriber is compared against the authentication information in the PAL. This provides additional security for the Publisher by preventing the Publisher and Distributor login from being used by a client tool to perform modifications on the Publisher directly. For more information, see [Secure the Publisher](security/secure-the-publisher.md).  
  
## Options  
 **Add**  
 Add a new entry to the list. You can add only those login, account, or group names that are already defined at both the Publisher and Distributor. They are defined at both servers if domain accounts are used or local accounts have been created at both servers.  
  
 **Remove**  
 Remove the selected entry from the list.  
  
 **Remove All**  
 Remove all entries from the list.  
  
## See Also  
 [Create a Publication](publish/create-a-publication.md)   
 [View and Modify Publication Properties](publish/view-and-modify-publication-properties.md)   
 [Publish Data and Database Objects](publish/publish-data-and-database-objects.md)  
  
  
