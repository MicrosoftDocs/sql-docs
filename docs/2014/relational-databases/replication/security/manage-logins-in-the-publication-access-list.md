---
title: "Manage Logins in the Publication Access List | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "logins [SQL Server replication], publication access list"
  - "publications [SQL Server replication], publication access lists"
  - "publication access list (PAL)"
  - "PAL (publication access list)"
  - "logins [SQL Server replication], managing"
ms.assetid: fceb216b-0b18-4e3b-8ae0-13e35920dcbc
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Manage Logins in the Publication Access List
  This topic describes how to manage logins in the Publication Access List in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)]. Access to a publication is controlled by the publication access list (PAL). Logins and groups can be added and removed from the PAL.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Prerequisites](#Prerequisites)  
  
-   **To manage logins in the Publication Access List, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   You must associate the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login with a database user in the publication database before you add the login to the PAL.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 You use the publication access list (PAL) on the **Publication Access List** page of the **Publication Properties - \<Publication>** dialog box to manage logins. For more information about how to access this dialog box, see [View and Modify Publication Properties](../publish/view-and-modify-publication-properties.md).  
  
#### To manage logins in the PAL  
  
1.  On the **Publication Access List** page of the **Publication Properties - \<Publication>** dialog box, use the **Add**, **Remove**, and **Remove All** buttons to add and remove logins and groups from the PAL. Do not remove **distributor_admin** from the PAL. This account is used by replication.  
  
    > [!NOTE]  
    >  If a remote Distributor is used, accounts in the PAL must be available at both the Publisher and the Distributor. The account must be either a domain account or a local account that is defined at both servers. The passwords that are associated with both logins must be the same.  
  
2.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To view groups and logins that belong to the PAL  
  
1.  At the Publisher on the publication database, execute [sp_help_publication_access](/sql/relational-databases/system-stored-procedures/sp-help-publication-access-transact-sql). For **@publication**, specify the publication name. This displays information about the groups and logins in the PAL.  
  
#### To add groups and logins to the PAL  
  
1.  At the Publisher on the publication database, execute [sp_grant_publication_access](/sql/relational-databases/system-stored-procedures/sp-grant-publication-access-transact-sql). For **@publication**, specify the publication name; and for **@login**, specify the name of the login or group that is being added.  
  
#### To remove groups and logins from the PAL  
  
1.  At the Publisher on the publication database, execute [sp_revoke_publication_access](/sql/relational-databases/system-stored-procedures/sp-revoke-publication-access-transact-sql). For **@publication**, specify the publication name; and for **@login**, specify the name of the login or group that is being removed.  
  
## See Also  
 [Replication Agent Security Model](replication-agent-security-model.md)   
 [Secure a Replication Topology](view-and-modify-replication-security-settings.md)   
 [Secure the Publisher](secure-the-publisher.md)  
  
  
