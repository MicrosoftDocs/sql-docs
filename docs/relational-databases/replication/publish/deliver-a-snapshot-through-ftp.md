---
title: "Deliver a Snapshot Through FTP | Microsoft Docs"
ms.custom: ""
ms.date: "11/20/2018"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "snapshots [SQL Server replication], FTP snapshots"
  - "FTP snapshots [SQL Server replication]"
  - "snapshot replication [SQL Server], FTP"
ms.assetid: 99872c4f-40ce-4405-8fd4-44052d3bd827
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Deliver a Snapshot Through FTP
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to deliver a snapshot through FTP in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)].  

By default, snapshots are stored in folders defined as Universal Naming Convention (UNC) shares. Replication also allows you to specify a File Transfer Protocol (FTP) share instead of a UNC share. To use FTP, you must configure an FTP server and then configure a publication and one or more subscriptions to use FTP. For information about how to configure an FTP server, see the Internet Information Services (IIS) documentation. If you specify FTP information for a publication, subscriptions to that publication use FTP by default. FTP is only used with Web synchronization when the computer that is running IIS is separated from the Distributor by a firewall. In this case, FTP can be used to transfer the snapshot from the Distributor and the computer that is running IIS. (The snapshot is always transferred to the Subscriber by using HTTPS.)  
  
> [!IMPORTANT]  
>  We recommend that you use Microsoft Windows Authentication and a UNC share rather than an FTP share because FTP passwords must be stored, and the password is sent from the Subscriber or the computer that is running IIS when it uses Web synchronization to the FTP server in plain text. Additionally, because a single account controls access to the snapshot share, it is not possible to ensure that a Subscriber to a filtered merge publication only has access to the snapshot files from their data partition.  
  

## Limitations and Restrictions  
  
-   The Snapshot Agent must have write permissions for the directory you specify, and the Distribution Agent or Merge Agent must have read permissions. If pull subscriptions are used, you must specify a shared directory as a universal naming convention (UNC) path, such as \\\ftpserver\home\snapshots. For more information, see [Secure the Snapshot Folder](../../../relational-databases/replication/security/secure-the-snapshot-folder.md).  
  
## Prerequisites  
  
-   To transfer snapshot files using File Transfer Protocol (FTP), you must first configure an FTP server. For more information, see the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Internet Information Services (IIS) documentation.  
  
###  <a name="Security"></a> Security  
 To help improve security, we recommend that you implement a virtual private network (VPN) when using FTP snapshot delivery over the Internet. For more information, see [Publish Data over the Internet Using VPN](../../../relational-databases/replication/publish-data-over-the-internet-using-vpn.md).  
  
 As a security best practice, do not allow anonymous logins to the FTP server. The Snapshot Agent must have write permissions for the directory you specify, and the Distribution Agent or Merge Agent must have read permissions. If pull subscriptions are used, you must specify a shared directory as a universal naming convention (UNC) path, such as \\\ftpserver\home\snapshots. For more information, see [Secure the Snapshot Folder](../../../relational-databases/replication/security/secure-the-snapshot-folder.md).  
  
 When possible, prompt users to enter their credentials at runtime. If you store credentials in a script file, you must secure the file.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 After the FTP server is configured, specify directory and security information for this server in the **Publication Properties \<Publication>** dialog box. For more information about accessing this dialog box, see [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md).  
  
#### To specify FTP information  
  
1.  In the **Publication Properties - \<Publication>** dialog box, select **Allow Subscribers to download snapshot files using FTP** from one of the following pages:  
  
    -   The **FTP Snapshot** page, for snapshot and transactional publications, and merge publications for Publishers running versions prior to [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)].  
  
    -   The **FTP Snapshot and Internet** page, for merge publications from Publishers running [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or later.  
  
2.  Specify values for **FTP server name**, **Port number**, **Path from the FTP root folder**, **Login**, and **Password**.  
  
     For example, if the FTP server root is \\\ftpserver\home and you want snapshots to be stored at \\\ftpserver\home\snapshots, specify \snapshots\ftp for the property **Path from the FTP root folder** (replication appends 'ftp' to the snapshot folder path when it creates snapshot files).  
  
3.  Specify that the Snapshot Agent should write the snapshot files to the directory specified in step 2. For example, to have the Snapshot Agent write the snapshot files to \\\ftpserver\home\snapshots\ftp, you must specify the path \\\ftpserver\home\snapshots in one of two places:  
  
    -   The default snapshot location for the Distributor associated with this publication.    
    -   An alternate snapshot folder location for this publication. An alternate location is required if the snapshot is compressed.  

For more information about modifying the snapshot folder location properties, see [Snapshot options](../snapshot-options.md).
  

4.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 The option to make snapshot files available on an FTP server can be set and these FTP settings can be modified programmatically using replication stored procedures. The procedure used depends on the type of publication. FTP snapshot delivery is only used with pull subscriptions.  
  
#### To enable FTP snapshot delivery for a snapshot or transactional publication  
  
1.  At the Publisher on the publication database, execute [sp_addpublication](../../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md). Specify **@publication**, a value of **true** for **@enabled_for_internet**, and appropriate values for the following parameters:  
  
    -   **@ftp_address** - the address of the FTP server used to deliver the snapshot.  
  
    -   (Optional) **@ftp_port** - the port used by the FTP server.  
  
    -   (Optional) **@ftp_subdirectory** - the subdirectory of the default FTP directory assigned to an FTP login. For example, if the FTP server root is \\\ftpserver\home and you want snapshots to be stored at \\\ftpserver\home\snapshots, specify **\snapshots\ftp** for **@ftp_subdirectory** (replication appends 'ftp' to the snapshot folder path when it creates snapshot files).  
  
    -   (Optional) **@ftp_login** - a login account used when connecting to the FTP server.  
  
    -   (Optional) **@ftp_password** - the password for the FTP login.  
  
     This creates a publication that uses FTP. For more information, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md).  
  
#### To enable FTP snapshot delivery for a merge publication  
  
1.  At the Publisher on the publication database, execute [sp_addmergepublication](../../../relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql.md). Specify **@publication**, a value of **true** for **@enabled_for_internet** and appropriate values for the following parameters:  
  
    -   **@ftp_address** - the address of the FTP server used to deliver the snapshot.  
  
    -   (Optional) **@ftp_port** - the port used by the FTP server.  
  
    -   (Optional) **@ftp_subdirectory** - the subdirectory of the default FTP directory assigned to an FTP login. For example, if the FTP server root is \\\ftpserver\home and you want snapshots to be stored at \\\ftpserver\home\snapshots, specify **\snapshots\ftp** for **@ftp_subdirectory** (replication appends 'ftp' to the snapshot folder path when it creates snapshot files).  
  
    -   (Optional) **@ftp_login** - a login account used when connecting to the FTP server.  
  
    -   (Optional) **@ftp_password** - the password for the FTP login.  
  
     This creates a publication that uses FTP. For more information, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md).  
  
#### To create a pull subscription to a snapshot or transactional publication that uses FTP snapshot delivery  
  
1.  At the Subscriber on the subscription database, execute [sp_addpullsubscription](../../../relational-databases/system-stored-procedures/sp-addpullsubscription-transact-sql.md). Specify **@publisher** and **@publication**.  
  
    -   At the Subscriber on the subscription database, execute [sp_addpullsubscription_agent](../../../relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql.md). Specify **@publisher**, **@publisher_db**, **@publication**, the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows credentials under which the Distribution Agent at the Subscriber runs for **@job_login** and **@job_password**, and a value of **true** for **@use_ftp**.  
  
2.  At the Publisher on the publication database, execute [sp_addsubscription](../../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md) to register the pull subscription. For more information, see [Create a Pull Subscription](../../../relational-databases/replication/create-a-pull-subscription.md).  
  
#### To create a pull subscription to a merge publication that uses FTP snapshot delivery  
  
1.  At the Subscriber on the subscription database, execute [sp_addmergepullsubscription](../../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-transact-sql.md). Specify **@publisher** and **@publication**.  
  
2.  At the Subscriber on the subscription database, execute [sp_addmergepullsubscription_agent](../../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-agent-transact-sql.md). Specify **@publisher**, **@publisher_db**, **@publication**, the Windows credentials under which the Distribution Agent at the Subscriber runs for **@job_login** and **@job_password**, and a value of **true** for **@use_ftp**.  
  
3.  At the Publisher on the publication database, execute [sp_addmergesubscription](../../../relational-databases/system-stored-procedures/sp-addmergesubscription-transact-sql.md) to register the pull subscription. For more information, see [Create a Pull Subscription](../../../relational-databases/replication/create-a-pull-subscription.md).  
  
#### To change one or more FTP snapshot delivery settings for a snapshot or transactional publication  
  
1.  At the Publisher on the publication database, execute [sp_changepublication](../../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md). Specify one of the following values for **@property** and a new value of this setting for **@value**:  
  
    -   **ftp_address** - the address of the FTP server used to deliver the snapshot.  
  
    -   **ftp_port** - the port used by the FTP server.  
  
    -   **ftp_subdirectory** - the subdirectory of the default FTP directory used for the FTP snapshot.  
  
    -   **ftp_login** - a login used to connect to the FTP server.  
  
    -   **ftp_password** - the password for the FTP login.  
  
2.  (Optional) Repeat step 1 for each FTP setting being changed.  
  
3.  (Optional) To disable FTP snapshot delivery, execute [sp_changepublication](../../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md) at the Publisher on the publication database. Specify a value of **enabled_for_internet** for **@property** and a value of **false** for **@value**.  
  
#### To change FTP snapshot delivery settings for a merge publication  
  
1.  At the Publisher on the publication database, execute [sp_changemergepublication](../../../relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql.md). Specify one of the following values for **@property** and a new value of this setting for **@value**:  
  
    -   **ftp_address** - the address of the FTP server used to deliver the snapshot.  
  
    -   **ftp_port** - the port used by the FTP server.  
  
    -   **ftp_subdirectory** - the subdirectory of the default FTP directory used for the FTP snapshot.  
  
    -   **ftp_login** - a login used to connect to the FTP server.  
  
    -   **ftp_password** - the password for the FTP login.  
  
2.  (Optional) Repeat step 1 for each FTP setting being changed.  
  
3.  (Optional) To disable FTP snapshot delivery, execute [sp_changemergepublication](../../../relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql.md) at the Publisher on the publication database. Specify a value of **enabled_for_internet** for **@property** and a value of **false** for **@value**.  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
 The following example creates a merge publication that allows Subscribers to access the snapshot data using FTP. The Subscriber should use a secure VPN connection when accessing the FTP share. **sqlcmd** scripting variables are used to supply login and password values. For more information, see [Use sqlcmd with Scripting Variables](../../../relational-databases/scripting/sqlcmd-use-with-scripting-variables.md).  
  
 [!code-sql[HowTo#sp_createmergepub_ftp](../../../relational-databases/replication/codesnippet/tsql/deliver-a-snapshot-throu_1.sql)]  
  
 The following example creates a subscription to a merge publication, where the Subscriber obtains the snapshot using FTP. The Subscriber should use a secure VPN connection when accessing the FTP share. **sqlcmd** scripting variables are used to supply login and password values. For more information, see [Use sqlcmd with Scripting Variables](../../../relational-databases/scripting/sqlcmd-use-with-scripting-variables.md).  
  
 [!code-sql[HowTo#sp_createmergepullsub_ftp](../../../relational-databases/replication/codesnippet/tsql/deliver-a-snapshot-throu_2.sql)]  
  
 [!code-sql[HowTo#sp_createmergepullsubagent_ftp](../../../relational-databases/replication/codesnippet/tsql/deliver-a-snapshot-throu_3.sql)]  
  
## See Also  
 [Replication System Stored Procedures Concepts](../../../relational-databases/replication/concepts/replication-system-stored-procedures-concepts.md)   
 [Change Publication and Article Properties](../../../relational-databases/replication/publish/change-publication-and-article-properties.md)   
 [Initialize a Subscription with a Snapshot](../../../relational-databases/replication/initialize-a-subscription-with-a-snapshot.md)  
  
  
