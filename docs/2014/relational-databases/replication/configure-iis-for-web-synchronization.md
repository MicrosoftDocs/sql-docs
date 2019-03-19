---
title: "Configure IIS for Web Synchronization | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "IIS server configuration [SQL Server replication]"
  - "websync.log"
  - "Web synchronization, IIS servers"
ms.assetid: d651186e-c9ca-4864-a444-2cd6943b8e35
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Configure IIS for Web Synchronization
  The procedures in this topic make up the second step in configuring Web synchronization for merge replication. You perform this step after you enable a publication for Web synchronization. For an overview of the configuration process, see [Configure Web Synchronization](configure-web-synchronization.md). After you complete the procedures in this topic, continue to the third step, configuring a subscription to use Web synchronization. This third step is described in the following topics:  
  
-   [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]: [How to: Configure a Subscription to Use Web Synchronization \(SQL Server Management Studio\)](https://msdn.microsoft.com/library/ms345214.aspx)  
  
-   Replication [!INCLUDE[tsql](../../includes/tsql-md.md)] programming: [How to: Configure a Subscription to Use Web Synchronization (Replication Transact-SQL Programming)](https://msdn.microsoft.com/library/ms345206.aspx)  
  
-   RMO: [How to: Configure a Subscription to Use Web Synchronization (RMO Programming)](https://msdn.microsoft.com/library/ms345207.aspx)  
  
 Web synchronization uses a computer that is running [!INCLUDE[msCoName](../../includes/msconame-md.md)] Internet Information Services (IIS) to synchronize pull subscriptions to merge publications. IIS version 5.0, IIS version 6.0, and IIS version 7.0 are supported. The Configure Web Synchronization Wizard is not supported on IIS version 7.0.  
  
> [!IMPORTANT]  
>  Make sure that your application uses only [!INCLUDE[dnprdnlong](../../includes/dnprdnlong-md.md)] or later versions, and that earlier versions of the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] are not installed on the IIS server. Earlier versions of the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] can cause errors. These include the following: "The format of a message during Web synchronization was invalid. Ensure that replication components are properly configured at the Web server".  
  
> [!CAUTION]  
>  Do not use both WebSync and alternate snapshot folder locations at the same time.  
  
 To use Web synchronization, you must configure IIS by completing the following steps. Each step is described in detail in this topic.  
  
1.  Configure Secure Sockets Layer (SSL). SSL is required for communication between IIS and all Subscribers.  
  
2.  Install [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connectivity components on the computer that is running IIS by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard. If you plan to use the Configure Web Synchronization Wizard that is mentioned in step 3, you must also install [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] on the computer that is running IIS.  
  
3.  Configure the computer that is running IIS for Web synchronization. You can configure the computer manually or use the Configure Web Synchronization Wizard. We recommend that you use the wizard.  
  
    > [!NOTE]  
    >  If the computer that is running IIS is running on a 64-bit version of Windows, you must run following command to make sure that the server is properly configured to run Internet Server API (ISAPI) applications. For more information, see the IIS documentation.  
  
    ```  
    cscript %SystemDrive%\inetpub\AdminScripts\adsutil.vbs set w3svc/AppPools/Enable32bitAppOnWin64 1  
    ```  
  
4.  Set the appropriate permissions for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Replication Listener.  
  
5.  Run Web synchronization in diagnostic mode to test the connection to the computer that is running IIS and to make sure that the SSL certificate is properly installed.  
  
## Configuring Secure Sockets Layer  
 To configure SSL, specify a certificate for the computer that is running IIS to use. Web synchronization for merge replication supports using server certificates but not client certificates. To configure IIS for deployment, you must first obtain a certificate from a certification authority (CA). A certificate authority is an entity that is responsible for establishing and vouching for the authenticity of public encryption keys that belong to users, computers, or other certification authorities. For more information about certificates, see the IIS documentation. After you install the certificate, you must associate the certificate with the Web site that is used by Web synchronization.  
  
#### To specify a certificate for deployment  
  
1.  Log on to the computer that is running IIS as an administrator.  
  
2.  Start **Internet Information Services (IIS) Manager**:  
  
    1.  Click **Start**, and then click **Run**.  
  
    2.  In the **Open** box, type `inetmgr`, and then click **OK**.  
  
3.  Run the IIS Certificate Wizard:  
  
    1.  In **Internet Information Services (IIS) Manager**, expand the **local computer** node, and then expand the **Web Sites** folder.  
  
    2.  Right-click **Default Web Site**, and then click **Properties**.  
  
    3.  In the **Default Web Site Properties** dialog box, on the **Directory Security** tab, click **Server Certificate**.  
  
    4.  Complete the Web Server Certificate Wizard.  
  
4.  Click **OK**.  
  
 If you cannot obtain a server certificate from a CA, you can specify a certificate for testing. To configure IIS 6.0 for testing, install a certificate by using the SelfSSL utility. This utility is available in the IIS 6.0 Resource Kit. You can download the tools from the [Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkId=30958). For IIS 5.0, go to [Microsoft Help and Support](https://go.microsoft.com/fwlink/?LinkId=46229).  
  
> [!NOTE]  
>  A certificate must be associated with a Web site before that Web site can use SSL. SelfSSL automatically associates the certificate with the default Web site. If you already have a certificate or later install a certificate from a CA, you must explicitly associate that certificate with the Web site that is used by Web synchronization. Make sure there is only one certificate associated with the Web site that is used to synchronize subscriptions. If there are multiple certificates, the Subscriber will use the first available Web site.  
  
#### To specify a certificate for testing in IIS 6.0  
  
1.  Log on to the computer that is running IIS as an administrator.  
  
2.  Download and install SelfSSL. By default, the application is installed to \<*drive*>:\Program Files\IIS Resources\SelfSSL. Application and documentation shortcuts are copied to \<*drive*>:\Documents and Settings\All Users\Start Menu\Programs\IIS Resources\SelfSSL.  
  
3.  Run SelfSSL:  
  
    -   To run SelfSSL with default values for all parameters, locate the installation directory for the application, and then double-click SelfSSL.exe.  
  
        > [!NOTE]  
        >  By default, the certificate that is installed by SelfSSL is valid for seven days.  
  
    -   To specify values for one or more parameters: click **Start**, and then click **Run**. In the **Open** box, enter `cmd`, and then click **OK**. Locate the SelfSSL installation directory, type `SelfSSL`, and then specify values for one or more parameters. For a list of parameters, type `SelfSSL -?`.  
  
## Installing Connectivity Components and SQL Server Management Studio  
  
#### To install SQL Server connectivity components and SQL Server Management Studio  
  
1.  Log on as an administrator to the computer that is running IIS.  
  
2.  From the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] installation disk, start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard. For more information about using this wizard, see [Install SQL Server 2014 from the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md).  
  
3.  On the **Feature Selection** page, select **Client Tools Connectivity**.  
  
4.  If you plan to use the Configure Web Synchronization Wizard, select **Management Tools - Basic**.  
  
5.  Complete the wizard, and then restart the computer.  
  
    > [!NOTE]  
    >  You can install additional components, but only the connectivity components are required for Web synchronization.  
  
## Configuring the Computer That Is Running IIS by Using the Configure Web Synchronization Wizard  
 Configure the IIS server by using the Configure Web Synchronization Wizard or manually. We recommend using the wizard, but we also provide steps for manual configuration in the next section. The Web Synchronization Wizard that is available with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] can be used only for publications that were created on a Publisher that is running [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or a Publisher that was upgraded to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]. The wizard cannot be used for publications on [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. The wizard can be used with subscriptions on [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions, and [!INCLUDE[ssEWnoversion](../../includes/ssewnoversion-md.md)] 3.0 and later versions.  
  
 The configuration has the following characteristics:  
  
-   Uses the default Web site in IIS. However, you can use another Web site. For more information about how to create Web sites, see the IIS documentation.  
  
    > [!NOTE]  
    >  The Web site that you specify provides access to the components that are used by Web synchronization. The Web site does not provide access to other data or Web pages unless you configure the site to do so.  
  
-   Creates a virtual directory and its associated alias. The alias is used when accessing the Web synchronization components. For example, if the IIS address is https://*server.domain.com* and you specify an alias of 'websync1', the address to access the replisapi.dll component is https://*server.domain.com*/websync1/replisapi.dll.  
  
-   Uses Basic Authentication. We recommend using Basic Authentication because Basic Authentication enables you to run IIS and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher/Distributor on separate computers (the recommended configuration) without requiring Kerberos delegation. Using SSL with Basic Authentication makes sure that logins, passwords, and all data are encrypted in transit. (SSL is required, regardless of the type of authentication that is used.) For more information about best practices for Web synchronization, see the section "Security Best Practices for Web Synchronization" in [Configure Web Synchronization](configure-web-synchronization.md).  
  
#### To configure the computer that is running IIS by using the Configure Web Synchronization Wizard  
  
1.  On the computer that is running IIS, start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
2.  Connect to the Publisher, and then expand the server node.  
  
3.  Expand the **Local Publications** folder, right-click the publication, and then click **Configure Web Synchronization**.  
  
4.  In the Configure Web Synchronization Wizard, on the **Subscriber Type** page, select **SQL Server**.  
  
5.  On the **Web Server** page:  
  
    1.  Select the instance of IIS that will synchronize subscriptions.  
  
    2.  Select **Create a new virtual directory**.  
  
    3.  In the lower pane of the page, expand the instance of IIS, expand **Web Sites**, and then click **Default Web Site**.  
  
6.  On the **Virtual Directory Information** page:  
  
    1.  In the **Alias** box, enter an alias for the virtual directory.  
  
    2.  In the **Path** box, enter a path of the virtual directory. For example, if you entered `websync1` in the **Alias** box, enter `C:\Inetpub\wwwroot\websync1` in the **Path** box. Click **Next**.  
  
    3.  On both dialog boxes, click **Yes**. This specifies that you want to create a new folder and that you want to copy the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Internet Server API (ISAPI) DLL. .  
  
7.  On the **Authenticated Access** page:  
  
    1.  Make sure that **Integrated Windows authentication** and **Digest authentication for Windows domain servers** are cleared.  
  
    2.  Select **Basic Authentication**.  
  
    3.  In the **Default Domain** and **Realm** boxes, enter the domain of the computer that is running IIS.  
  
8.  On the **Directory Access** page:  
  
    1.  Click **Add**, and then in the **Select Users or Groups** dialog box, add the accounts under which Subscribers will make connections to IIS. These are the accounts that you will specify on the **Web Server Information** page of the New Subscription Wizard or as the value for the [sp_addmergepullsubscription_agent](/sql/relational-databases/system-stored-procedures/sp-addmergepullsubscription-agent-transact-sql)*@internet_login* parameter.  
  
9. On the **Snapshot Share Access** page, enter the snapshot share. The appropriate permissions are set on this share so that Subscribers can access the snapshot files. For more information about permissions for the share, see [Secure the Snapshot Folder](security/secure-the-snapshot-folder.md).  
  
10. On the **Completing the Wizard** page, click **Finish**.  
  
     If a failure occurs, such as a network error while trying to configure a remote computer that is running IIS, all completed actions are rolled back and all remaining actions are canceled. If a completed action cannot be rolled back, the status in the final page of the wizard displays **Success** and the completed actions remain committed.  
  
11. If the computer that is running IIS is running on a 64-bit version of Windows, replisapi.dll must be copied to the appropriate directory:  
  
    1.  Click **Start**, and then click **Run**. In the **Open** box, enter `iisreset`, and then click **OK**.  
  
    2.  After IIS stops and restarts, copy replisapi.dll from [!INCLUDE[ssInstallPathVar](../../includes/ssinstallpathvar-md.md)]COM\replisapi to the directory that is specified in step 6b.  
  
    3.  Click **Start**, and then click **Run**. In the **Open** box, enter `cmd`, and then click **OK**.  
  
    4.  In the directory that you specified in step 6b, execute the following command:  
  
         `regsvr32 replisapi.dll`  
  
## Manually Configuring the Computer That Is Running IIS  
 To configure the computer that is running IIS manually, you must install and configure the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Replication Listener, and then configure authorization for Subscribers that will connect to IIS.  
  
#### To install and configure the SQL Server Replication Listener  
  
1.  Create a file directory on the computer that is running IIS to contain replisapi.dll. You can create the directory wherever you want, but we recommend that you create the directory under the \<*drive*>:\Inetpub directory. For example, create the directory \<*drive*>:\Inetpub\SQLReplication\\.  
  
    > [!IMPORTANT]  
    >  We strongly recommend that you create this directory on an NTFS file system partition instead of a FAT file system. When you use the NTFS file system, you can use NTFS file system permissions to control precisely the users that can access [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] replication.  
  
2.  Copy replisapi.dll from the directory [!INCLUDE[ssInstallPathVar](../../includes/ssinstallpathvar-md.md)]com\ to the file directory that you created in step 1.  
  
3.  Register replisapi.dll:  
  
    1.  Click **Start**, and then click **Run**. In the **Open** box, enter `cmd`, and then click **OK**.  
  
    2.  In the directory that you created in step 1, execute the following command:  
  
         `regsvr32 replisapi.dll`  
  
4.  Create a new Web site for replication, or use an existing site. The Web site will be accessed by replication components during synchronization. For more information about how to create Web sites, see the IIS documentation.  
  
5.  Create a virtual directory in IIS. The virtual directory should be created under the Web site that was created in step 4, and should be mapped to the directory that was created in step 1. For more information about how to create virtual directories, see the IIS documentation. We recommend that you be as restrictive as possible when you assign permissions to this directory. You must select **Read** and **Execute** permissions, but you can clear **Run scripts**, **Write**, and **Browse** permissions.  
  
6.  Configure IIS to enable replisapi.dll to execute. The permissions that are assigned in step 4 are sufficient for earlier versions of IIS; however, IIS version 6.0 requires Internet Server API (ISAPI) extensions to be enabled. For more information, see "Configuring ISAPI Extensions" and "Enabling and Disabling Dynamic Content" in the IIS 6.0 documentation.  
  
#### To configure IIS authentication  
  
-   When Subscribers connect to IIS, IIS must authenticate the Subscribers before they can access resources and processes. IIS offers three types of authentication: Anonymous, Basic, and Integrated. Authentication can be applied to the whole Web site or to the virtual directory that you created.  
  
     We recommend that you use Basic Authentication with SSL. SSL is required, regardless of the type of authentication that is used. For more information about how to configure authentication, see the IIS documentation.  
  
## Setting Permissions for the SQL Server Replication Listener  
 When a Subscriber connects to the computer that is running IIS, the Subscriber is authenticated by using the type of authentication that was specified when you configured IIS. After IIS authenticates the Subscriber, IIS checks whether the Subscriber is authorized to invoke [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] replication. You control the users that can invoke [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] replication by setting permissions for replisapi.dll. Properly configuring permissions is necessary to prevent unauthorized access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] replication.  
  
 To configure the minimum permissions for the account under which the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Replication Listener runs, complete the following procedure. The steps in the procedure apply to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[winxpsvr](../../includes/winxpsvr-md.md)] running IIS 6.0.  
  
 In addition to performing the following steps, make sure that the required logins are in the publication access list (PAL). For more information about the PAL, see [Secure the Publisher](security/secure-the-publisher.md).  
  
#### To configure the account and permissions  
  
1.  Create a local account on the computer that is running IIS:  
  
    1.  Right-click **My Computer**, and then click **Manage**.  
  
    2.  In **Computer Management**, expand **Local Users and Groups**.  
  
    3.  Right-click **Users**, and then click **New User**.  
  
    4.  Enter a user name and a strong password.  
  
    5.  Click **Create**, and then click **Close**.  
  
2.  Add the account to the IIS_WPG group:  
  
    1.  In **Computer Management**, expand **Local Users and Groups**, and then click **Groups**.  
  
    2.  Right-click **IIS_WPG**, and then click **Add to Group**.  
  
    3.  In the **IIS_WPG Properties** dialog box, click **Add**.  
  
    4.  In the **Select Users, Computers, or Groups** dialog box, add the account created in step 1.  
  
    5.  Make sure that the name in the **From this location** field is the name of the local computer instead of a domain. If the name is not a local computer, click **Locations**. In the **Locations** dialog box, elect the local computer, and then click **OK**.  
  
    6.  In the **Select Users** dialog box and the **IIS_WPG Properties** dialog box, click **OK**.  
  
3.  Grant the minimum permissions on the folder that contains replisapi.dll to the account :  
  
    1.  Locate the folder that you created for replisapi.dll, right-click the folder, and then click **Sharing and Security**.  
  
    2.  On the **Security** tab, click **Add**.  
  
    3.  In the **Select Users, Computers, or Groups** dialog box, add the account that you created in step 1.  
  
    4.  Make sure that the name in the **From this location** field is the name of the local computer instead of a domain. If the name is not a local computer, click **Locations**. In the **Locations** dialog box, select the local computer, and then click **OK**.  
  
    5.  Make sure that the account is granted only **Read**, **Read & Execute**, and **List Folder Contents** permissions.  
  
    6.  Select any users or groups that do not require access to the directory, and then click **Remove**.  
  
    7.  Click **OK**.  
  
4.  Create an application pool in **Internet Information Services (IIS) Manager**:  
  
    1.  Click **Start**, and then click **Run**.  
  
    2.  In the **Open** box, type `inetmgr`, and then click **OK**.  
  
    3.  In **Internet Information Services (IIS) Manager**, expand the **local computer** node.  
  
    4.  Right-click **Application Pools**, point to **New** and then click **Application Pool**.  
  
    5.  Enter a name for the pool in the **Application pool ID** field, and then click **OK**.  
  
5.  Associate the account with the application pool:  
  
    1.  In **Internet Information Services (IIS) Manager**, expand the **local computer** node, and then expand **Application Pools**.  
  
    2.  Right-click the application pool that you created, and then click **Properties**.  
  
    3.  In the **\<ApplicationPoolName> Properties** dialog box, on the **Identity** tab, click **Configurable**.  
  
    4.  In the **User name** and **password** fields, enter the account and password that were created in step 1.  
  
    5.  Click **OK**.  
  
6.  Associate the application pool with the virtual directory that is used for Web synchronization:  
  
    1.  In **Internet Information Services (IIS) Manager**, expand the **local computer** node, and then expand **Web Sites**.  
  
    2.  Expand the Web site that you are using for Web synchronization, right-click the virtual directory that you created for Web synchronization, and then click **Properties**.  
  
    3.  On the **Virtual Directory** tab of the **\<VirtualDirectoryName> Properties** dialog box, from the **Application pool** drop-down list, select the application pool that was created in step 5.  
  
    4.  Click **OK**.  
  
## Testing the Connection to replisapi.dll  
 Run Web synchronization in diagnostic mode to test the connection to the computer that is running IIS and to make sure that the Secure Sockets Layer (SSL) certificate is properly installed. To run Web synchronization in diagnostic mode, you must be an administrator on the computer running IIS.  
  
#### To test the connection to replisapi.dll  
  
1.  Make sure that local area network (LAN) settings at the Subscriber are correct:  
  
    1.  In [!INCLUDE[msCoName](../../includes/msconame-md.md)] Internet Explorer, on the **Tools** menu, click **Internet Options**.  
  
    2.  On the **Connections** tab, click **LAN Settings**.  
  
    3.  If a proxy server is not used on the LAN, clear **Automatically Detect Settings** and **Use a proxy server for your LAN**.  
  
    4.  If a proxy server is used, select **Use a proxy server for your LAN** and **Bypass proxy server for local addresses**.  
  
    5.  Click **OK**.  
  
2.  At the Subscriber, in Internet Explorer, connect to the server in diagnostic mode by appending `?diag` to the address for the replisapi.dll. For example: https://server.domain.com/directory/replisapi.dll?diag.  
  
3.  If the certificate that you specified for IIS is not recognized by the Windows operating system, the **Security Alert** dialog box appears. This alert might occur because the certificate is a test certificate or the certificate was issued by a certification authority (CA) that Windows does not recognize.  
  
    > [!NOTE]  
    >  If this dialog box does not appear, make sure that the certificate for the server that you are accessing has been added to the certificate store at the Subscriber as a trusted certificate. For more information about exporting certificates, see the IIS documentation.  
  
    1.  In the **Security Alert** dialog box, click **View Certificate**.  
  
    2.  In the **Certificate** dialog box, on the **General** tab, click **Install Certificate**.  
  
    3.  Complete the Certificate Import Wizard, accepting the defaults.  
  
    4.  In the **Security Warning** dialog box, click **Yes**.  
  
    5.  In the Certificate Import Wizard confirmation dialog box, click **OK**.  
  
    6.  Close the **Certificate** dialog box.  
  
    7.  In the **Security Alert** dialog box, click **Yes**.  
  
    > [!NOTE]  
    >  Certificates are installed for users. This process must be performed for each user that will synchronize with IIS.  
  
4.  In the **Connect to \<ServerName>** dialog box, specify the login and password that the Merge Agent will use to connect to IIS. These credentials will also be specified in the New Subscription Wizard.  
  
5.  In the Internet Explorer window called **SQL Websync diagnostic information**, verify that the value in each **Status** column on the page is **SUCCESS**.  
  
6.  Make sure that the certificate is installed correctly on the Subscriber:  
  
    1.  Close and then reopen Internet Explorer.  
  
    2.  Connect to the server in diagnostic mode. If the certificate is installed properly, the **Security Alert** dialog box will not appear. If the dialog box appears, the Merge Agent will fail when it tries to connect to the computer that is running IIS. You must make sure that the certificate for the server that you are accessing has been added to the certificate store at the Subscriber as a trusted certificate. For more information about exporting certificates, see the IIS documentation.  
  
## See Also  
 [Configure Web Synchronization](configure-web-synchronization.md)  
  
  
