---
title: "Deliver a Snapshot Through FTP | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "snapshots [SQL Server replication], FTP snapshots"
  - "FTP snapshots [SQL Server replication]"
  - "snapshot replication [SQL Server], FTP"
ms.assetid: 99872c4f-40ce-4405-8fd4-44052d3bd827
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Deliver a Snapshot Through FTP
  This topic describes how to deliver a snapshot through FTP in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)].  
  
##  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The Snapshot Agent must have write permissions for the directory you specify, and the Distribution Agent or Merge Agent must have read permissions. If pull subscriptions are used, you must specify a shared directory as a universal naming convention (UNC) path, such as \\\ftpserver\home\snapshots. For more information, see [Secure the Snapshot Folder](../security/secure-the-snapshot-folder.md).  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   To transfer snapshot files using File Transfer Protocol (FTP), you must first configure an FTP server. For more information, see the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Internet Information Services (IIS) documentation.  
  
###  <a name="Security"></a> Security  
 To help improve security, we recommend that you implement a virtual private network (VPN) when using FTP snapshot delivery over the Internet. For more information, see [Publish Data over the Internet Using VPN](../publish-data-over-the-internet-using-vpn.md).  
  
 As a security best practice, do not allow anonymous logins to the FTP server. The Snapshot Agent must have write permissions for the directory you specify, and the Distribution Agent or Merge Agent must have read permissions. If pull subscriptions are used, you must specify a shared directory as a universal naming convention (UNC) path, such as \\\ftpserver\home\snapshots. For more information, see [Secure the Snapshot Folder](../security/secure-the-snapshot-folder.md).  
  
 When possible, prompt users to enter their credentials at runtime. If you store credentials in a script file, you must secure the file.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 After the FTP server is configured, specify directory and security information for this server in the **Publication Properties \<Publication>** dialog box. For more information about accessing this dialog box, see [View and Modify Publication Properties](view-and-modify-publication-properties.md).  
  
#### To specify FTP information  
  
1.  In the **Publication Properties - \<Publication>** dialog box, select **Allow Subscribers to download snapshot files using FTP** from one of the following pages:   
    -   The **FTP Snapshot** page, for snapshot and transactional publications, and merge publications for Publishers running versions prior to [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)].    
    -   The **FTP Snapshot and Internet** page, for merge publications from Publishers running [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or later.    
2.  Specify values for **FTP server name**, **Port number**, **Path from the FTP root folder**, **Login**, and **Password**.    
     For example, if the FTP server root is \\\ftpserver\home and you want snapshots to be stored at \\\ftpserver\home\snapshots, specify \snapshots\ftp for the property **Path from the FTP root folder** (replication appends 'ftp' to the snapshot folder path when it creates snapshot files).    
3.  Specify that the Snapshot Agent should write the snapshot files to the directory specified in step 2. For example, to have the Snapshot Agent write the snapshot files to \\\ftpserver\home\snapshots\ftp, you must specify the path \\\ftpserver\home\snapshots in one of two places:    
    -   The default snapshot location for the Distributor associated with this publication.    
         For more information about specifying the default snapshot location, see [Specify the Default Snapshot Location](../snapshot-options.md#snapshot-folder-locations).    
    -   An alternate snapshot folder location for this publication. An alternate location is required if the snapshot is compressed.    
         Enter the path in the **Put files in the following folder** textbox on the Snapshot page of the **Publication Properties - \<Publication>** dialog box.   
4.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 The option to make snapshot files available on an FTP server can be set and these FTP settings can be modified programmatically using replication stored procedures. The procedure used depends on the type of publication. FTP snapshot delivery is only used with pull subscriptions.  
  
#### To enable FTP snapshot delivery for a snapshot or transactional publication  
  
1.  At the Publisher on the publication database, execute [sp_addpublication](/sql/relational-databases/system-stored-procedures/sp-addpublication-transact-sql). Specify **@publication**, a value of `true` for **@enabled_for_internet**, and appropriate values for the following parameters:    
    -   **@ftp_address** - the address of the FTP server used to deliver the snapshot.    
    -   (Optional) **@ftp_port** - the port used by the FTP server.    
    -   (Optional) **@ftp_subdirectory** - the subdirectory of the default FTP directory assigned to an FTP login. For example, if the FTP server root is \\\ftpserver\home and you want snapshots to be stored at \\\ftpserver\home\snapshots, specify **\snapshots\ftp** for **@ftp_subdirectory** (replication appends 'ftp' to the snapshot folder path when it creates snapshot files).    
    -   (Optional) **@ftp_login** - a login account used when connecting to the FTP server.    
    -   (Optional) **@ftp_password** - the password for the FTP login.  
  
     This creates a publication that uses FTP. For more information, see [Create a Publication](create-a-publication.md).  
  
#### To enable FTP snapshot delivery for a merge publication  
  
1.  At the Publisher on the publication database, execute [sp_addmergepublication](/sql/relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql). Specify **@publication**, a value of `true` for **@enabled_for_internet** and appropriate values for the following parameters:  
  
    -   **@ftp_address** - the address of the FTP server used to deliver the snapshot.    
    -   (Optional) **@ftp_port** - the port used by the FTP server.    
    -   (Optional) **@ftp_subdirectory** - the subdirectory of the default FTP directory assigned to an FTP login. For example, if the FTP server root is \\\ftpserver\home and you want snapshots to be stored at \\\ftpserver\home\snapshots, specify **\snapshots\ftp** for **@ftp_subdirectory** (replication appends 'ftp' to the snapshot folder path when it creates snapshot files).    
    -   (Optional) **@ftp_login** - a login account used when connecting to the FTP server.    
    -   (Optional) **@ftp_password** - the password for the FTP login.  
  
     This creates a publication that uses FTP. For more information, see [Create a Publication](create-a-publication.md).  
  
#### To create a pull subscription to a snapshot or transactional publication that uses FTP snapshot delivery  
  
1.  At the Subscriber on the subscription database, execute [sp_addpullsubscription](/sql/relational-databases/system-stored-procedures/sp-addpullsubscription-transact-sql). Specify **@publisher** and **@publication**.  
  
    -   At the Subscriber on the subscription database, execute [sp_addpullsubscription_agent](/sql/relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql). Specify **@publisher**, **@publisher_db**, **@publication**, the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows credentials under which the Distribution Agent at the Subscriber runs for **@job_login** and **@job_password**, and a value of `true` for **@use_ftp**.  
  
2.  At the Publisher on the publication database, execute [sp_addsubscription](/sql/relational-databases/system-stored-procedures/sp-addsubscription-transact-sql) to register the pull subscription. For more information, see [Create a Pull Subscription](../create-a-pull-subscription.md).  
  
#### To create a pull subscription to a merge publication that uses FTP snapshot delivery  
  
1.  At the Subscriber on the subscription database, execute [sp_addmergepullsubscription](/sql/relational-databases/system-stored-procedures/sp-addmergepullsubscription-transact-sql). Specify **@publisher** and **@publication**.   
2.  At the Subscriber on the subscription database, execute [sp_addmergepullsubscription_agent](/sql/relational-databases/system-stored-procedures/sp-addmergepullsubscription-agent-transact-sql). Specify **@publisher**, **@publisher_db**, **@publication**, the Windows credentials under which the Distribution Agent at the Subscriber runs for **@job_login** and **@job_password**, and a value of `true` for **@use_ftp**.    
3.  At the Publisher on the publication database, execute [sp_addmergesubscription](/sql/relational-databases/system-stored-procedures/sp-addmergesubscription-transact-sql) to register the pull subscription. For more information, see [Create a Pull Subscription](../create-a-pull-subscription.md).  
  
#### To change one or more FTP snapshot delivery settings for a snapshot or transactional publication  
  
1.  At the Publisher on the publication database, execute [sp_changepublication](/sql/relational-databases/system-stored-procedures/sp-changepublication-transact-sql). Specify one of the following values for **@property** and a new value of this setting for **@value**:    
    -   `ftp_address` - the address of the FTP server used to deliver the snapshot.    
    -   `ftp_port` - the port used by the FTP server.    
    -   `ftp_subdirectory` - the subdirectory of the default FTP directory used for the FTP snapshot.    
    -   `ftp_login` - a login used to connect to the FTP server.    
    -   `ftp_password` - the password for the FTP login.  
  
2.  (Optional) Repeat step 1 for each FTP setting being changed.    
3.  (Optional) To disable FTP snapshot delivery, execute [sp_changepublication](/sql/relational-databases/system-stored-procedures/sp-changepublication-transact-sql) at the Publisher on the publication database. Specify a value of `enabled_for_internet` for **@property** and a value of `false` for **@value**.  
  
#### To change FTP snapshot delivery settings for a merge publication  
  
1.  At the Publisher on the publication database, execute [sp_changemergepublication](/sql/relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql). Specify one of the following values for **@property** and a new value of this setting for **@value**:  
  
    -   `ftp_address` - the address of the FTP server used to deliver the snapshot.    
    -   `ftp_port` - the port used by the FTP server.    
    -   `ftp_subdirectory` - the subdirectory of the default FTP directory used for the FTP snapshot.   
    -   `ftp_login` - a login used to connect to the FTP server.    
    -   `ftp_password` - the password for the FTP login.    
2.  (Optional) Repeat step 1 for each FTP setting being changed.    
3.  (Optional) To disable FTP snapshot delivery, execute [sp_changemergepublication](/sql/relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql) at the Publisher on the publication database. Specify a value of `enabled_for_internet` for **@property** and a value of `false` for **@value**.  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
 The following example creates a merge publication that allows Subscribers to access the snapshot data using FTP. The Subscriber should use a secure VPN connection when accessing the FTP share. **sqlcmd** scripting variables are used to supply login and password values. For more information, see [Use sqlcmd with Scripting Variables](../../scripting/sqlcmd-use-with-scripting-variables.md).  
  
 [!code-sql[HowTo#sp_createmergepub_ftp](../../../snippets/tsql/SQL15/replication/howto/tsql/createmergepubftp.sql#sp_createmergepub_ftp)]  
  
 The following example creates a subscription to a merge publication, where the Subscriber obtains the snapshot using FTP. The Subscriber should use a secure VPN connection when accessing the FTP share. **sqlcmd** scripting variables are used to supply login and password values. For more information, see [Use sqlcmd with Scripting Variables](../../scripting/sqlcmd-use-with-scripting-variables.md).  
  
 [!code-sql[HowTo#sp_createmergepullsub_ftp](../../../snippets/tsql/SQL15/replication/howto/tsql/createmergepullsubftp.sql#sp_createmergepullsub_ftp)]  
  
 [!code-sql[HowTo#sp_createmergepullsubagent_ftp](../../../snippets/tsql/SQL15/replication/howto/tsql/createmergepullsubftp.sql#sp_createmergepullsubagent_ftp)]  
  
## See Also  
 [Replication System Stored Procedures Concepts](../concepts/replication-system-stored-procedures-concepts.md)   
 [Transfer Snapshots Through FTP](../transfer-snapshots-through-ftp.md)   
 [Change Publication and Article Properties](change-publication-and-article-properties.md)   
 [Initialize a Subscription with a Snapshot](../initialize-a-subscription-with-a-snapshot.md)  
  
  
