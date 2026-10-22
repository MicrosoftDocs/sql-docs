---
title: Configure IIS 7 for Web Synchronization
description: Configure IIS 7 for Web synchronization.
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 04/07/2025
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "IIS 7 server configuration [SQL Server replication]"
  - "Web synchronization, IIS 7 servers"
---
# Configure IIS 7 for Web synchronization

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The procedures in this article guide you through the process of manually configuring Internet Information Services (IIS) version 7 and higher for use with Web synchronization for merge replication.

Configuring IIS 7 or higher is the first of three steps needed to enable Web synchronization.

For an overview of the entire configuration process, see [Configure Web Synchronization](configure-web-synchronization.md).

Make sure that your application uses only [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] 2.0 or later versions, and that earlier versions of the [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] aren't installed on the IIS server. Earlier versions of the [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] can cause errors, such as:

```output
The format of a message during Web synchronization was invalid. Ensure that replication components are properly configured at the Web server.
```

To use Web synchronization, you must configure IIS by completing the following steps. Each step is described in detail in this article.

1. Install and configure the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Replication Listener on the computer that is running IIS.

1. Configure Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL). TLS is required for communication between IIS and all subscribers.

1. Configure IIS authentication.

1. Configure an account and set permissions for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Replication Listener.

## Install the SQL Server Replication Listener

Web synchronization is supported on IIS, beginning with version 5.0. The Configure Web Synchronization Wizard of IIS version 5 and 6, isn't available with IIS version 7.0 and higher.

> [!NOTE]  
> To use the web sync component on IIS server in [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] and later versions, you should install SQL Server with replication. This includes the free SQL Server Express edition.

### Install and configure the SQL Server Replication Listener

1. Install SQL Server replication on the IIS computer.

1. Create a new file directory for replisapi.dll on the computer that is running IIS. You can create the directory wherever you want, but we recommend that you create the directory under the \<*drive*>:\Inetpub directory. For example, create the directory \<*drive*>:\Inetpub\SQLReplication\\.

1. Copy replisapi.dll from the directory [!INCLUDE [ssinstallpath-md](../../includes/ssinstallpath-md.md)]COM\ to the file directory that you created in step 1.

1. Register replisapi.dll:

    1. Select **Start**, and then select **Run**. In the **Open** box, enter **cmd**, and then select **OK**.

    1. In the directory created in step 1, execute the following command:

       ```cmd
       regsvr32 replisapi.dll
       ```

1. Create a new Web site for replication or use an existing site. This Web site will be accessed by replication components during synchronization. Procedures in this article assume the Default Web Site. For more information about how to create Web sites, see the IIS documentation

1. Create a virtual directory in IIS. The virtual directory should be created under the Web site that you created in step 4 and map it to the directory created in step 1. Be as restrictive as possible when you assign permissions to this directory. You must select at least **Read** and **Execute** permissions.

    1. In **Internet Information Services (IIS) Manager**, in the **Connections** pane, right-click **Default Web Site**, and then select **Add Virtual Directory**.

    1. For **Alias**, enter `SQLReplication`.

    1. For **Physical Path**, enter **\<drive>:\Inetpub\SQLReplication\\**, and then select **OK**.

1. Configure IIS to enable replisapi.dll to execute.

    1. In **Internet Information Services (IIS) Manager**, select **Default Web Site**.

    1. In the center pane, select **Handler Mappings**.

    1. In the **Actions** pane, select **Add Module Mapping**.

    1. For **Request** Path, enter **replisapi.dll**.

    1. From the **Module** dropdown list, select **IsapiModule**.

    1. For **Executable**, enter `<drive>:\Inetpub\SQLReplication\replisapi.dll`.

    1. For **Name**, enter **Replisapi**.

    1. Select the **Request Restrictions** button, select the **Access** tab, and then select **Execute**.

    1. Select **OK** to close the **Request Restrictions** dialog box, and then select **OK** again to close the **Add Module Mapping** dialog box. When you're prompted to allow the ISAPI extension, select **Yes** to add the extension.

    1. Verify that Replisapi.dll is listed under the **Enabled** handler mappings. If it's in the **Disabled** list, right-click the Replisapi entry and then select **Edit Feature Permissions**. Check the **Execute** box, and then select **OK**.

## Configure IIS authentication

When subscriber computers connect to IIS, IIS must authenticate the subscribers before they can access resources and processes. Authentication can be applied to the whole Web site or to the virtual directory that you created.

We recommend that you use Basic Authentication with TLS. TLS is required, regardless of the type of authentication that is used.

### Configuring IIS authentication

1. In **Internet Information Services (IIS) Manager**, select **Default Web Site**.
1. In the middle pane, double-click **Authentication**.
1. Right-click Anonymous Authentication, and then choose Disable.
1. Right-click Basic Authentication, and then choose Enable.

## Configure Secure Sockets Layer

To configure TLS, specify a certificate to be used by the computer running IIS. Web synchronization for merge replication supports using server certificates, but not client certificates. To configure IIS for deployment, you must first obtain a certificate from a certification authority (CA). For more information about certificates, see the IIS documentation.

After you install the certificate, you must associate the certificate with the Web site that is used by Web synchronization. For development and testing, you can specify a self-signed certificate. IIS 7 can create a certificate for you and register it on your computer.

The difference between deploying for production and the procedures given here, is that in production and preproduction testing, you would use a certificate issued by a CA instead of a self-signed certificate.

> [!IMPORTANT]  
> A self-signed certificate isn't recommended for a production installation. Self-signed certificates aren't secure. Use self-signed certificates for development and testing only.

To configure TLS, perform the following steps:

1. Configure the Web site to require TLS and ignore client certificates.
1. Obtain a certificate from a CA or create a self-signed certificate.
1. Bind the certificate to the replication Web site.

### Require SSL security for a Web site

1. In **Internet Information Services (IIS) Manager**, expand the local server node, and then select the **Default Web Site** (or your Web synchronization site if it's different from the default Web site).

1. In the middle pane, double-click **SSL Settings**.

1. Check the **Require SSL** option. Under **Client certificates**, verify that the **Ignore** button is selected.

### Create a self-signed certificate for testing

1. In **Internet Information Services (IIS) Manager**, select the local server node, and then in the center pane, double-click on **Server Certificates**.

1. In the **Actions** pane, select **Create Self-Signed Certificate**.

1. In the **Create Self-Signed Certificate** dialog box, enter a name for the certificate, and then select **OK**.

#### Bind a certificate to a Web site

1. In the **Connections** pane, select the **Default Web Site** (or your Web synchronization site, if it's different from the default Web site).

1. In the **Actions** pane, select **Bindings**, and then select **Add**. The **Add Site Binding** dialog box appears.

1. From the **Type** dropdown list, select **https**. Leave the default settings for **IP address** and **Port**.

1. From the **SSL certificate** dropdown list, select the certificate created in "To create a self-signed certificate for testing," select **OK**, and then select **Close**.

#### Test the certificate

1. In **Internet Information Services (IIS) Manager**, select **Default Web Site.**

1. From the **Actions** pane, select **Browse \*:443(https)**.

1. Internet Explorer opens, and displays a message that "There is a problem with this website's security certificate." This warning tells you that the associated certificate isn't issued by a recognized CA and might not be trustworthy. This is an expected warning, so select **Continue to this website (not recommended)**.

1. If you're prompted to **Connect to localhost**, enter a user name and password to proceed. You should see the default page for the Web site.

## Set permissions for the SQL Server Replication Listener

When a subscriber computer connects to the computer running IIS, the subscriber is authenticated by using the type of authentication specified when you configured IIS. After IIS authenticates the subscriber, IIS checks whether the subscriber is authorized to invoke [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] replication. You control the users that can invoke [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] replication by setting permissions for replisapi.dll. Properly configuring permissions is necessary to prevent unauthorized access to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] replication.

To configure the minimum permissions for the account under which the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Replication Listener runs, complete the following procedure. The steps in the following procedure apply to Windows Server 2008 running IIS 7.0.

In addition to performing the following steps, make sure that the required logins are in the publication access list (PAL). For more information about the PAL, see [Secure the Publisher](security/secure-the-publisher.md).

> [!IMPORTANT]  
> The account created in this section is the account that connects to the Publisher and Distributor during synchronization. This account must be added as a SQL Login account on the distribution and publication server.

The account used for the SQL Server Replication Listener must have permissions as described in the "Connect to the Publisher or Distributor" section, in the [Merge Agent Security](merge-agent-security.md) article.

In summary, the account must:

- Be a member of the Publication Access List (PAL).
- Be mapped to a login associated with a user in the publication database.
- Be mapped to a login associated with a user in the distribution database.
- Have Read permissions on the snapshot share.

### Configure the account and permissions

1. Create a local account on the computer running IIS:

   1. Open **Server Manager**. From the Start menu, right-click **Computer**, and then select **Manage**.

   1. In **Server Manager**, expand **Configuration**, and then expand **Local Users and Groups**.

   1. Right-click **Users**, and then select **New User**.

   1. Enter a user name and a strong password. Clear **User must change password at next logon**.

   1. Select **Create**, and then select **Close**.

1. Add the account to the IIS_IUSRS group:

   1. In **Server Manager**, expand **Configuration**, expand **Local Users and Groups**, and then select **Groups**.

   1. Right-click **IIS_IUSRS**, and then select **Add to Group**.

   1. In the **IIS_IUSRS Properties** dialog box, select **Add**.

   1. In the **Select Users, Computers, or Groups** dialog box, add the account created in step 1.

   1. Verify that **From this location** displays the name of the local computer (not a domain). If this field doesn't display the local computer name, select **Locations**. In the **Locations** dialog box, select the local computer, and then select **OK**.

   1. In the **Select Users** dialog box and the **IIS_IUSRS Properties** dialog box, select **OK**.

1. Grant minimum account permissions on the folder that contains replisapi.dll:

   1. In Windows Explorer, right-click the folder that you created for replisapi.dll, and then select **Properties**.

   1. On the **Security** tab, select **Edit**.

   1. In the **Permissions for \<foldername>** dialog box, select **Add** to add the account that you created in step 1.

   1. Verify that **From this location** displays the name of the local computer (not a domain). If this field doesn't display the local computer name, select **Locations**. In the **Locations** dialog box, select the local computer, and then select **OK**.

   1. Verify that the account is granted only **Read**, **Read & Execute**, and **List Folder Contents** permissions.

   1. Select any users or groups that don't require access to the directory, select **Remove**, and then select **OK**.

1. Create an application pool in **Internet Information Services (IIS) Manager**:

   1. In **Internet Information Services (IIS) Manager**, in the **Connections** pane, expand the local server node.

   1. Right-click **Application Pools**, and then select **Add Application Pool**.

   1. Enter a name for the application pool, leave the default values for the remaining fields, and then select **OK**.

   If you anticipate having more than two concurrent synchronization clients, you might want to create a web garden. For more information, see [Creating a Web Garden](configure-web-synchronization.md#creating-a-web-garden).

1. Associate the account with the application pool:

   1. In **Internet Information Services (IIS) Manager**, expand the local server node, and then select **Application Pools**.

   1. Right-click the application pool that you created, and then select **Set Application Pool Defaults**.

   1. In the **Application Pool Defaults** dialog box, scroll down to the **Process Model** section, and then select the **Identity** field.

   1. Select the ellipsis button on the right side of the **Identity** row.

   1. Select the **Custom Account** radio button, and then select **Set**.

   1. In the **User name** and **Password** fields, enter the account and password that were created in step 1, and then select **OK**.

   1. Select **OK** to close the **Application Pool Identity** dialog box, and then select **OK** again to close the **Application Pool Defaults** dialog box.

1. Associate the application pool with the replication Web site:

   1. In **Internet Information Services (IIS) Manager**, expand the local server node, and then select the **Default Web Site** (or your Web synchronization site if it's different from the default Web site).

   1. In the **Actions** pane, under **Manage Web Site**, select **Advanced Settings**.

   1. In the **Advanced Settings** dialog box, select the ellipsis button to the right of **Application Pool**.

   1. From the **Application pool** dropdown list, select the application pool you created in step 4, and then select **OK**.

   1. Select **OK** again to close Advanced Settings.

## Test the connection to `replisapi.dll`

Run Web synchronization in diagnostic mode to test the connection to the computer running IIS and to make sure that the TLS/SSL certificate is properly installed. To run Web synchronization in diagnostic mode, you must be an administrator on the computer running IIS.

1. Make sure that local area network (LAN) settings at the Subscriber are correct:

   1. In Internet Explorer, on the **Tools** menu, select **Internet Options**.

   1. On the **Connections** tab, select **LAN Settings**.

   1. If a proxy server isn't used on the LAN, clear **Automatically Detect Settings** and **Use a proxy server for your LAN**.

   1. If a proxy server is used, select **Use a proxy server for your LAN** and **Bypass proxy server for local addresses**, and then select **OK**.

1. At the Subscriber, in Internet Explorer, connect to the server in diagnostic mode by appending `?diag` to the address for the replisapi.dll. For example: `https://<server.domain.com>/directory/replisapi.dll?diag`.

   In the previous example, `<server.domain.com>` should be replaced with the exact **Issued To** name listed under the **Server Certificates** section in IIS Manager.

1. If the certificate that you specified for IIS isn't recognized by the Windows operating system, the **Security Alert** dialog box appears. This alert might occur because the certificate is a test certificate or the certificate was issued by a certification authority (CA) that Windows doesn't recognize.

   If this dialog box doesn't appear, make sure that the certificate for the server that you're accessing has been added to the certificate store at the Subscriber as a trusted certificate. For more information about exporting certificates, see the IIS documentation.

   1. In the **Security Alert** dialog box, select **View Certificate**.

   1. In the **Certificate** dialog box, on the **General** tab, select **Install Certificate**.

   1. Complete the Certificate Import Wizard, accepting the defaults.

   1. In the **Security Warning** dialog box, select **Yes**.

   1. In the Certificate Import Wizard confirmation dialog box, select **OK**.

   1. Close the **Certificate** dialog box.

   1. In the **Security Alert** dialog box, select **Yes**.

   Certificates are installed for users. This process must be performed for each user that will synchronize with IIS.

1. In the **Connect to \<ServerName>** dialog box, specify the login and password that the Merge Agent will use to connect to IIS. These credentials will also be specified in the New Subscription Wizard.

1. In the Internet Explorer window called **SQL Websync diagnostic information**, verify that the value in each **Status** column on the page is `SUCCESS`.

1. Make sure that the certificate is installed correctly on the Subscriber:

   1. Close and then reopen Internet Explorer.

   1. Connect to the server in diagnostic mode. If the certificate is installed properly, the **Security Alert** dialog box doesn't appear. If the dialog box appears, the Merge Agent fails when it tries to connect to the computer that is running IIS. You must make sure that the certificate for the server that you're accessing has been added to the certificate store at the Subscriber as a trusted certificate. For more information about exporting certificates, see the IIS documentation.

## Related content

- [Web Synchronization for Merge Replication](web-synchronization-for-merge-replication.md)
- [Configure Web Synchronization](configure-web-synchronization.md)
