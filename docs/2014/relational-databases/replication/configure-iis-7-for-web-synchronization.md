---
title: "Configure IIS 7 for Web Synchronization | Microsoft Docs"
ms.custom: ""
ms.date: "09/12/2016"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "IIS 7 server configuration [SQL Server replication]"
  - "Web synchronization, IIS 7 servers"
ms.assetid: c201fe2c-0a76-44e5-a233-05e14cd224a6
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Configure IIS 7 for Web Synchronization
  The procedures in this topic will guide you through the process of manually configuring [!INCLUDE[msCoName](../../includes/msconame-md.md)] Internet Information Services (IIS) version 7 and higher for use with Web synchronization for merge replication. 
  
 Configuring IIS 7 is the first of three steps needed to enable Web synchronization.  
  
 For an overview of the entire configuration process, see [Configure Web Synchronization](configure-web-synchronization.md).  
  
> [!IMPORTANT]  
>  Make sure that your application uses only [!INCLUDE[dnprdnlong](../../includes/dnprdnlong-md.md)] or later versions, and that earlier versions of the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] are not installed on the IIS server. Earlier versions of the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] can cause errors, such as: "The format of a message during Web synchronization was invalid. Ensure that replication components are properly configured at the Web server."  
  
 To use Web synchronization, you must configure IIS 7 by completing the following steps. Each step is described in detail in this topic.  
  
1.  Install and configure the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Replication Listener on the computer that is running IIS.  
  
2.  Configure Secure Sockets Layer (SSL). SSL is required for communication between IIS and all subscribers.  
  
3.  Configure IIS authentication.  
  
4.  Configure an account and set permissions for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Replication Listener.  
  
## Installing the SQL Server Replication Listener  

Web synchronization is supported on IIS, beginning with version 5.0. The Configure Web Synchronization Wizard of IIS version 5 and 6, is not available with IIS version 7.0 and higher. **Beginning with SQL Server 2012, to use the web sync component on IIS server, you should install SQL Server with replication. This can be the free SQL Server Express edition.**
  
#### To install and configure the SQL Server Replication Listener  
  
1. Install SQL Server replication on the IIS computer.

2.  Create a new file directory for replisapi.dll on the computer that is running IIS. You can create the directory wherever you want, but we recommend that you create the directory under the \<*drive*>:\Inetpub directory. For example, create the directory \<*drive*>:\Inetpub\SQLReplication\\.  
  
3.  Copy replisapi.dll from the directory [!INCLUDE[ssInstallPathVar](../../includes/ssinstallpathvar-md.md)]com\ to the file directory that you created in step 1.  
  
4.  Register replisapi.dll:  
  
    1.  Click **Start**, and then click **Run**. In the **Open** box, enter `cmd`, and then click **OK**.  
  
    2.  In the directory created in step 1, execute the following command:  
  
         `regsvr32 replisapi.dll`  
  
5.  Create a new Web site for replication or use an existing site. This Web site will be accessed by replication components during synchronization. Procedures in this topic will assume the Default Web Site. For more information about how to create Web sites, see the IIS documentation  
  
6.  Create a virtual directory in IIS. The virtual directory should be created under the Web site that you created in step 4 and map it to the directory created in step 1. Be as restrictive as possible when you assign permissions to this directory. You must select at least **Read** and **Execute** permissions.  
  
    1.  In **Internet Information Services (IIS) Manager**, in the **Connections** pane, right-click **Default Web Site**, and then select **Add Virtual Directory**.  
  
    2.  For **Alias**, enter `SQLReplication`.  
  
    3.  For **Physical Path**, enter **\<drive>:\Inetpub\SQLReplication\\**, and then click **OK**.  
  
7.  Configure IIS to enable replisapi.dll to execute.  
  
    1.  In **Internet Information Services (IIS) Manager**, click **Default Web Site**.  
  
    2.  In the center pane, click **Handler Mappings**.  
  
    3.  In the **Actions** pane, click **Add Module Mapping**.  
  
    4.  For **Request** Path, enter `replisapi.dll`.  
  
    5.  From the **Module** drop-down list, select **IsapiModule**.  
  
    6.  For **Executable**, enter **\<drive>:\Inetpub\SQLReplication\replisapi.dll**.  
  
    7.  For **Name**, enter `Replisapi`.  
  
    8.  Click the **Request Restrictions** button, click the **Access** tab, and then click **Execute**.  
  
    9. Click **OK** to close the **Request Restrictions** dialog box, and then click **OK** again to close the **Add Module Mapping** dialog box. When you are prompted to allow the ISAPI extension, click **Yes** to add the extension.  
  
    10. Verify that Replisapi.dll is listed under the **Enabled** handler mappings. If it is in the **Disabled** list, right-click the Replisapi entry and then click **Edit Feature Permissions**. Check the **Execute** box, and then click **OK**.  
  
## Configuring IIS Authentication  
 When subscriber computers connect to IIS, IIS must authenticate the subscribers before they can access resources and processes. Authentication can be applied to the whole Web site or to the virtual directory that you created.  
  
 We recommend that you use Basic Authentication with SSL. SSL is required, regardless of the type of authentication that is used.  
  
 We recommend that you use Basic Authentication with SSL. SSL is required, regardless of the type of authentication that is used.  
  
#### To Configure IIS Authentication  
  
1.  In **Internet Information Services (IIS) Manager**, click **Default Web Site**.  
  
2.  In the middle pane, double-click **Authentication**.  
  
3.  Right-click Anonymous Authentication, and then choose Disable.  
  
4.  Right-click Basic Authentication, and then choose Enable.  
  
## Configuring Secure Sockets Layer  
 To configure SSL, specify a certificate to be used by the computer running IIS. Web synchronization for merge replication supports using server certificates, but not client certificates. To configure IIS for deployment, you must first obtain a certificate from a certification authority (CA). For more information about certificates, see the IIS documentation.  
  
 After you install the certificate, you must associate the certificate with the Web site that is used by Web synchronization. For development and testing, you can specify a self-signed certificate. IIS 7 can create a certificate for you and register it on your computer.  
  
 The difference between deploying for production and the procedures given here is that in production and pre-production testing, you would use a certificate issued by a CA instead of a self-signed certificate.  
  
> [!IMPORTANT]  
>  A self-signed certificate is not recommended for a production installation. Self-signed certificates are not secure. Use self-signed certificates for development and testing only.  
  
 To configure SSL, you will perform the following steps:  
  
1.  Configure the Web site to require SSL and ignore client certificates.  
  
2.  Obtain a certificate from a CA or create a self-signed certificate.  
  
3.  Bind the certificate to the replication Web site.  
  
#### To require SSL security for a Web site  
  
1.  In **Internet Information Services (IIS) Manager**, expand the local server node, and then click the **Default Web Site** (or your Web synchronization site if it is different from the default Web site).  
  
2.  In the middle pane, double-click **SSL Settings**.  
  
3.  Check the **Require SSL** option. Under **Client certificates**, verify that the **Ignore** button is selected.  
  
#### To create a self-signed certificate for testing  
  
1.  In **Internet Information Services (IIS) Manager**, click the local server node, and then in the center pane, double-click on **Server Certificates**.  
  
2.  In the **Actions** pane, click **Create Self-Signed Certificate**.  
  
3.  In the **Create Self-Signed Certificate** dialog box, enter a name for the certificate, and then click **OK**.  
  
###### To bind a certificate to a Web site  
  
1.  In the **Connections** pane, click the **Default Web Site** (or your Web synchronization site, if it is different from the default Web site).  
  
2.  In the **Actions** pane, click **Bindings**, and then click **Add**. The **Add Site Binding** dialog box will appear.  
  
3.  From the **Type** drop-down list, select **https**. Leave the default settings for **IP address** and **Port**.  
  
4.  From the **SSL certificate** drop-down list, select the certificate created in "To create a self-signed certificate for testing," click **OK**, and then click **Close**.  
  
###### To test the certificate  
  
1.  In **Internet Information Services (IIS) Manager**, click **Default Web Site.**  
  
2.  From the **Actions** pane, click **Browse \*:443(https)**.  
  
3.  Internet Explorer will open and display a message that "There is a problem with this website's security certificate." This warning tells you that the associated certificate was not issued by a recognized CA and might not be trustworthy. This is an expected warning, so click **Continue to this website (not recommended)**.  
  
4.  If you are prompted to **Connect to localhost**, enter a user name and password to proceed. You should see the default page for the Web site.  
  
## Setting Permissions for the SQL Server Replication Listener  
 When a subscriber computer connects to the computer running IIS, the subscriber is authenticated by using the type of authentication specified when you configured IIS. After IIS authenticates the subscriber, IIS checks whether the subscriber is authorized to invoke [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] replication. You control the users that can invoke [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] replication by setting permissions for replisapi.dll. Properly configuring permissions is necessary to prevent unauthorized access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] replication.  
  
 To configure the minimum permissions for the account under which the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Replication Listener runs, complete the following procedure. The steps in the following procedure apply to [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Server 2008 running IIS 7.0.  
  
 In addition to performing the following steps, make sure that the required logins are in the publication access list (PAL). For more information about the PAL, see [Secure the Publisher](security/secure-the-publisher.md).  
  
 **Important** The account created in this section is the account that will connect to the Publisher and Distributor during synchronization. This account must be added as a SQL Login account on the distribution and publication server.  
  
 The account used for the SQL Server Replication Listener must have permissions as described in the Merge Agent Security topic, in the "Connect to the Publisher or Distributor" section.  
  
 In summary, the account must:  
  
-   Be a member of the Publication Access List (PAL).  
  
-   Be mapped to a login associated with a user in the publication database.  
  
-   Be mapped to a login associated with a user in the distribution database.  
  
-   Have Read permissions on the snapshot share.  
  
#### To configure the account and permissions  
  
1.  Create a local account on the computer running IIS:  
  
    1.  Open **Server Manager**. From the Start menu, right-click **Computer**, and then click **Manage**.  
  
    2.  In **Server Manager**, expand **Configuration**, and then expand **Local Users and Groups**.  
  
    3.  Right-click **Users**, and then click **New User**.  
  
    4.  Enter a user name and a strong password. Clear **User must change password at next logon**.  
  
    5.  Click **Create**, and then click **Close**.  
  
2.  Add the account to the IIS_IUSRS group:  
  
    1.  In **Server Manager**, expand **Configuration**, expand **Local Users and Groups**, and then click **Groups**.  
  
    2.  Right-click **IIS_IUSRS**, and then click **Add to Group**.  
  
    3.  In the **IIS_IUSRS Properties** dialog box, click **Add**.  
  
    4.  In the **Select Users, Computers, or Groups** dialog box, add the account created in step 1.  
  
    5.  Verify that **From this location** displays the name of the local computer (not a domain). If this field does not display the local computer name, click **Locations**. In the **Locations** dialog box, select the local computer, and then click **OK**.  
  
    6.  In the **Select Users** dialog box and the **IIS_IUSRS Properties** dialog box, click**OK**.  
  
3.  Grant minimum account permissions on the folder that contains replisapi.dll:  
  
    1.  In Windows Explorer, right-click the folder that you created for replisapi.dll, and then click **Properties**.  
  
    2.  On the **Security** tab, click **Edit**.  
  
    3.  In the **Permissions for \<foldername>** dialog box, click **Add** to add the account that you created in step 1.  
  
    4.  Verify that **From this location** displays the name of the local computer (not a domain). If this field does not display the local computer name, click **Locations**. In the **Locations** dialog box, select the local computer, and then click **OK**.  
  
    5.  Verify that the account is granted only **Read**, **Read & Execute**, and **List Folder Contents** permissions.  
  
    6.  Select any users or groups that do not require access to the directory, click **Remove**, and then click **OK**.  
  
4.  Create an application pool in **Internet Information Services (IIS) Manager**:  
  
    1.  In **Internet Information Services (IIS) Manager**, in the **Connections** pane, expand the local server node.  
  
    2.  Right-click **Application Pools**, and then click **Add Application Pool**.  
  
    3.  Enter a name for the application pool, leave the default values for the remaining fields, and then click **OK**.  
  
    > [!NOTE]  
    >  If you anticipate having more than two concurrent synchronization clients, you might want to create a web garden. For more information, see "Creating a Web Garden" in [Configure Web Synchronization](configure-web-synchronization.md).  
  
5.  Associate the account with the application pool:  
  
    1.  In **Internet Information Services (IIS) Manager**, expand the local server node, and then click on **Application Pools**.  
  
    2.  Right-click the application pool that you created, and then click **Set Application Pool Defaults**.  
  
    3.  In the **Application Pool Defaults** dialog box, scroll down to the **Process Model** section, and then click the **Identity** field.  
  
    4.  Click the ellipsis button on the right side of the **Identity** row.  
  
    5.  Click the **Custom Account** radio button, and then click **Set**.  
  
    6.  In the **User name** and **Password** fields, enter the account and password that were created in step 1, and then click **OK**.  
  
    7.  Click **OK** to close the **Application Pool Identity** dialog box, and then click **OK** again to close the **Application Pool Defaults**dialog box.  
  
6.  Associate the application pool with the replication Web site:  
  
    1.  In **Internet Information Services (IIS) Manager**, expand the local server node, and then click on the **Default Web Site** (or your Web synchronization site if it is different from the default Web site).  
  
    2.  In the **Actions** pane, under **Manage Web Site**, click **Advanced Settings**.  
  
    3.  In the **Advanced Settings** dialog box, click on the ellipsis button to the right of **Application Pool**.  
  
    4.  From the **Application pool** drop-down list, select the application pool you created in step 4, and then click **OK**.  
  
    5.  Click **OK** again to close Advanced Settings.  
  
## Testing the Connection to replisapi.dll  
 Run Web synchronization in diagnostic mode to test the connection to the computer running IIS and to make sure that the Secure Sockets Layer (SSL) certificate is properly installed. To run Web synchronization in diagnostic mode, you must be an administrator on the computer running IIS.  
  
#### To test the connection to replisapi.dll  
  
1.  Make sure that local area network (LAN) settings at the Subscriber are correct:  
  
    1.  In [!INCLUDE[msCoName](../../includes/msconame-md.md)] Internet Explorer, on the **Tools** menu, click **Internet Options**.  
  
    2.  On the **Connections** tab, click **LAN Settings**.  
  
    3.  If a proxy server is not used on the LAN, clear **Automatically Detect Settings** and **Use a proxy server for your LAN**.  
  
    4.  If a proxy server is used, click **Use a proxy server for your LAN** and **Bypass proxy server for local addresses**, and then click **OK**.  
  
2.  At the Subscriber, in Internet Explorer, connect to the server in diagnostic mode by appending `?diag` to the address for the replisapi.dll. For example: **https://server.domain.com/directory/replisapi.dll?diag**.  
  
    > [!NOTE]  
    >  In the example above, **server.domain.com** should be replaced with the exact **Issued To** name listed under the **Server Certificates** section in IIS Manager.  
  
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
 [Web Synchronization for Merge Replication](web-synchronization-for-merge-replication.md)   
 [Configure Web Synchronization](configure-web-synchronization.md)  
  
  
