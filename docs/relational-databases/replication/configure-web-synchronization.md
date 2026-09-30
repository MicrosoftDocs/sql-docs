---
title: Configure Web Synchronization
description: Configure Web synchronization for SQL Server merge replication to sync data over HTTPS. Learn how to set up IIS, accounts, publications, and pull subscriptions.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
f1_keywords:
  - "SQL10.REP.CONFIGWEBSYNCWIZARD.SNAPSHARE.F1"
  - "SQL13.REP.CONFIGWEBSYNCWIZARD.SNAPSHARE.F1"
  - "SQL10.REP.CONFIGWEBSYNCWIZARD.VIRDIRINFO.F1"
  - "SQL13.REP.CONFIGWEBSYNCWIZARD.CLIENTAUTH.F1"
  - "SQL10.REP.CONFIGWEBSYNCWIZARD.DIRACCESS.F1"
  - "SQL10.REP.CONFIGWEBSYNCWIZARD.SUBTYPE.F1"
  - "SQL10.REP.CONFIGWEBSYNCWIZARD.ANONACCESS.F1"
  - "SQL10.REP.CONFIGWEBSYNCWIZARD.WEBSERV.F1"
  - "SQL10.REP.CONFIGWEBSYNCWIZARD.CLIENTAUTH.F1"
  - "SQL13.REP.CONFIGWEBSYNCWIZARD.SUBTYPE.F1"
  - "SQL10.REP.CONFIGWEBSYNCWIZARD.COMPLETEWIZ.F1"
  - "SQL13.REP.CONFIGWEBSYNCWIZARD.DIRACCESS.F1"
  - "SQL13.REP.CONFIGWEBSYNCWIZARD.ANONACCESS.F1"
  - "SQL13.REP.CONFIGWEBSYNCWIZARD.AUTHACCESS.F1"
  - "SQL10.REP.CONFIGWEBSYNCWIZARD.AUTHACCESS.F1"
  - "SQL13.REP.CONFIGWEBSYNCWIZARD.COMPLETEWIZ.F1"
  - "SQL13.REP.CONFIGWEBSYNCWIZARD.VIRDIRINFO.F1"
  - "SQL13.REP.CONFIGWEBSYNCWIZARD.WEBSERV.F1"
helpviewer_keywords:
  - "Web synchronization, security best practices"
  - "Web synchronization, configuring"
---
# Configure Web synchronization
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The Web synchronization option for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Merge Replication enables data replication by using the HTTPS protocol over the Internet. To use Web synchronization, first complete the following configuration steps:  
  
1.  Create new domain accounts and map [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins.  
  
1.  Configure the computer that is running [!INCLUDE[msCoName](../../includes/msconame-md.md)] Internet Information Services (IIS) to synchronize subscriptions.  
  
1.  Configure a merge publication to allow Web synchronization.  
  
1.  Configure one or more subscriptions to use Web synchronization.  

> [!NOTE]  
>  If you plan to replicate large volumes of data or use large data types such as **varchar(max)**, read the section "Replicating Large Volumes of Data" in this topic.  
  
 To successfully set up Web synchronization, you must decide how to configure security to meet your particular requirements and policies. Make these decisions and create the necessary accounts before you attempt to configure IIS, the publication, and subscriptions.  
  
 The following procedures describe a simplified security configuration that uses local accounts. This simplified configuration is suitable for installations where both IIS and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher and Distributor run on the same computer, even though it's much more likely (and recommended) that you use a multiple-server topology for a production installation. You can substitute domain accounts for the local accounts in the procedures.  
  
## Creating new accounts and mapping SQL Server logins  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Replication Listener (replisapi.dll) connects to the Publisher by impersonating the account you specify for the application pool that is associated with the replication web site.  
  
 The account you use for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Replication Listener needs the permissions described in [Merge Agent Security](../../relational-databases/replication/merge-agent-security.md), under the section "Connect to the Publisher or Distributor." In summary, the account must:  
  
-   Be a member of the Publication Access List (PAL).  
  
-   Be mapped to a login associated with a user in the publication database.  
  
-   Be mapped to a login associated with a user in the distribution database.  
  
-   Have Read permissions on the snapshot share.  
  
 If this is the first time you're using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Replication, you also need to create accounts and logins for the replication agents. For more information, see the "Configuring the Publication" and "Configuring the Subscription" sections in this article.  
  
 Before you configure Web synchronization, read the "Security Best Practices for Web Synchronization" section in this article. For more information about Web synchronization security, see [Security Architecture for Web Synchronization](../../relational-databases/replication/security/security-architecture-for-web-synchronization.md).
  
## Configuring the Computer That Is Running IIS  
 Web synchronization requires that you install and configure IIS. You need the URL to the replication web site before you can configure a publication to use Web synchronization.  
  
 Web synchronization is supported on IIS beginning with version 5.0. The Configure Web Synchronization Wizard isn't supported on IIS version 7.0. Beginning with SQL Server 2012, to use the Web sync component on IIS server, install SQL Server with replication. This can be the free SQL Server Express edition.  
  
 TLS is required for Web synchronization. You need a security certificate issued by a certification authority. For testing purposes only, you can use a self-issued security certificate.  
   
  
 **To configure IIS for Web synchronization**  
  
-   [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]: [Configure IIS for Web Synchronization](./configure-iis-7-for-web-synchronization.md)  
  
-   [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]: [Configure IIS 7 for Web Synchronization](../../relational-databases/replication/configure-iis-7-for-web-synchronization.md)  
  
## Creating a Web garden  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Replication Listener supports two concurrent synchronization operations per thread. Exceeding this limit can cause the Replication Listener to stop responding. The application pool Maximum Worker Processes property determines the number of threads allocated to `replisapi.dll`. By default, this property is set to 1.  
  
 You can support a greater number of concurrent synchronization operations per CPU by increasing the Maximum Worker Process property value. Scaling out by increasing the number of worker processes per CPU is known as creating a "Web garden."  
  
 Web gardening allows more than two Subscribers to synchronize at the same time. It also increases CPU utilization by `replisapi.dll`, which can negatively affect overall server performance. Balance these considerations when you choose a value for Maximum Worker Processes.  
  
#### To increase Maximum Worker Processes in IIS 7  
  
1.  In **Internet Information Services (IIS) Manager**, expand the local server node, and then click on the **Application Pool** node.  
  
1.  Select the application pool associated with the Web synchronization site, and then click **Advanced Settings** on the **Actions** pane.  
  
1.  On the Advanced Settings dialog, under the **Process Model** heading, click the row labeled **Maximum Worker Processes**. Change the property value, and then click **OK**.  
  
## Configuring the publication  
 To use Web synchronization, create a publication the same way you create a standard merge topology publication. For more information, see [Publish Data and Database Objects](../../relational-databases/replication/publish/publish-data-and-database-objects.md).  
  
 After creating the publication, enable the option to allow for Web synchronization by using one of the following methods: [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or Replication Management Objects (RMO). To enable Web synchronization, you need to supply the Web server address for Subscriber connections.  
  
 If you're using a Publisher for the first time, you must also configure a Distributor and a snapshot share. The Merge Agent at each Subscriber must have read permissions on the snapshot share. For more information, see [Configure Distribution](../../relational-databases/replication/configure-distribution.md) and [Secure the Snapshot Folder](../../relational-databases/replication/security/secure-the-snapshot-folder.md).  
  
 **gen** is a reserved word in websync XML files. Don't attempt to publish tables containing columns named **gen**.  
  
## Configuring the subscription  
 After you enable a publication and configure IIS, create a pull subscription and specify that the pull subscription should synchronize by using IIS. (Web synchronization is supported only for pull subscriptions.)  
  
## Upgrading from an earlier version of SQL Server  
 If you have an existing Web synchronization topology configured and you upgrade [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], ensure that the latest version of `replisapi.dll` is copied to the virtual directory used by Web synchronization. By default, the latest version of `replisapi.dll` is located in `C:\Program Files\Microsoft SQL Server\<nnn>\COM`.  
  
## Replicating large volumes of data  
 To help avoid potential memory problems on Subscriber computers, Web synchronization uses a default maximum size of 100 MB for the XML file used to transfer changes. Raise the limit by setting the following registry key:  
  
 **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\100\Replication**  
  
 **WebSyncMaxXmlSize DWORD 2000000**  
  
 The range of acceptable values for this key is 100 MB to 4 GB. The value is specified in KB. Setting this parameter to a high value doesn't guarantee that you can synchronize that amount of data. The effective limit is constrained by how much contiguous memory is available on the Subscriber computer. If you must have a value larger than 100 MB, incrementally increase the value and test memory consumption with a typical workload on the Subscriber.  
  
 The maximum size for the XML file is 4 GB, but replication synchronizes the changes from that file in batches. The maximum batch size of data and metadata is 25 MB. You must ensure that the data in each batch doesn't exceed approximately 20 MB, which allows for metadata and any other overhead. This limit has the following implications:  
  
-   You can't replicate any column that causes the data and metadata to exceed 25 MB. This limit might be an issue when you're replicating rows that contain large data types, such as **varchar(max)**.  
  
-   If you replicate large volumes of data, you might need to adjust the Merge Agent batch size.  
  
 Batch size for merge replication is measured in *generations*, which are collections of changes per article. Use the -**DownloadGenerationsPerBatch** and -**UploadGenerationsPerBatch** parameters of the Merge Agent to specify the number of generations in a batch. For more information, see [Replication Merge Agent](../../relational-databases/replication/agents/replication-merge-agent.md).  
  
 For large volumes of data, specify a small number for each of the batching parameters. Start with a value of 10, and then tune based on application needs and performance. Typically, you specify these parameters in an agent profile. For more information about profiles, see [Replication Agent Profiles](../../relational-databases/replication/agents/replication-agent-profiles.md).  
  
## Security best practices for Web synchronization  
 Web synchronization offers many choices for security-related settings. Use the following approach:  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributor and Publisher can be on the same computer (a typical setup for merge replication). However, install IIS on a separate computer.  
  
-   Use Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL), to encrypt the connection between the Subscriber and the computer running IIS. Web synchronization requires this encryption.  
  
-   Use Basic Authentication for connections from the Subscriber to IIS. By using Basic Authentication, IIS can make connections to the Publisher/Distributor on behalf of the Subscriber without requiring delegation. Delegation is required if you use Integrated Authentication.  
  
    > [!NOTE]  
    >  Basic Authentication is the method by which credentials are passed to IIS. Basic Authentication doesn't prevent specifying Windows domain accounts for connections that are made to IIS.  
  
-   Specify that the Snapshot Agent runs under a Windows domain account, and specify that the agent makes connections as that account. (This is the default configuration.) Specify that each Merge Agent runs under the domain account of the user that uses the Subscriber computer, and specify that the agent makes connections as that account.  
  
     For more information about the permissions that agents require, see [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md).  
  
-   Specify the same domain account as the one the Merge Agent uses when you specify an account and password on the **Web Server Information** page of the New Subscription Wizard or when you specify values for the `@internet_url` and `@internet_login` parameters of [sp_addpullsubscription_agent](../../relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql.md). This account must have read permissions for the snapshot share.  
  
-   Each publication should use a separate virtual directory for IIS.  
  
-   The account under which the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Replication Listener (`replisapi.dll`) runs is also the account that connects to the Publisher and Distributor during synchronization. This account must be mapped to a SQL Server Login account on the Publisher and Distributor. For more information, see the "Setting Permissions for the SQL Server Replication Listener" section in the [Configure IIS for Web Synchronization](./configure-iis-7-for-web-synchronization.md).  
  
-   You can use FTP to deliver the snapshot from the Publisher to the computer that runs IIS. The computer that runs IIS always delivers the snapshot to the Subscriber by using HTTPS. For more information, see [Transfer Snapshots Through FTP](../../relational-databases/replication/publish/deliver-a-snapshot-through-ftp.md).  
  
-   If servers in the replication topology are behind a firewall, you might need to open ports in the firewall to enable Web synchronization.  
  
    -   The Subscriber computer connects to the computer that runs IIS over HTTPS by using TLS, which is typically configured to use port 443. [!INCLUDE[ssEW](../../includes/ssew-md.md)] Subscribers can also connect over HTTP, which is typically configured to use port 80.  
  
    -   The computer that runs IIS typically connects to the Publisher or Distributor by using port 1433 (default instance). When the Publisher or Distributor is a named instance on a server with another default instance, port 1500 is typically used to connect to the named instance.  
  
    -   If the computer running IIS is separated from the Distributor by a firewall and an FTP share is used for snapshot delivery, you must open the ports used for FTP. For more information, see [Transfer Snapshots Through FTP](../../relational-databases/replication/publish/deliver-a-snapshot-through-ftp.md).  
  
> [!IMPORTANT]  
>  Opening ports in your firewall can leave your server exposed to malicious attacks. Make sure that you understand firewall systems before you open ports. For more information, see [Security Considerations for a SQL Server Installation](../../sql-server/install/security-considerations-for-a-sql-server-installation.md).  
  
## Related content

- [Web Synchronization for Merge Replication](web-synchronization-for-merge-replication.md)
