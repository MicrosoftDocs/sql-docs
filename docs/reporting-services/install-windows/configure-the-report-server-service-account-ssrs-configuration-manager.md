---
title: "Configure the Report Server Service Account"
description: Learn how to implement Reporting Services, a single service that contains a Report Server Web service, web portal, and a background processing application.
author: maggiesMSFT
ms.author: maggies
ms.date: 08/19/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: how-to
ms.custom: updatefrequency5
#customer intent: As a SSRS user, I want to learn how to configure the Report Server Service Account so that I can update and troubleshoot my accounts information.
---

# Configure the Report Server Service Account (Report Server Configuration Manager)

[!INCLUDE [ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is implemented as a single service that contains a Report Server Web service, [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)], and a background processing application that's used for scheduled report processing and subscription delivery. This article explains how the service account is initially configured and how to modify the account or password by using the Reporting Services Configuration tool.

## Initial configuration

The Report Server service account is defined during Setup. You can run the service under a domain user account or a built-in account such as **Virtual Service Account**. There's no default account; whatever account you specify in the **Service Accounts** page of the Installation Wizard becomes the initial account of the Report Server service.

> [!IMPORTANT]
> Although the Report Server Web service and [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] are separate [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] applications, they run under a single service architecture within the same Report Server process identity.
> [!NOTE]
> Built-in Windows service accounts (Local Service or Network Service) aren't supported as report server service accounts on a computer that is a domain controller.

## Change the service account

To view and reconfigure service account information, always use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager. Service identity information is stored internally in multiple locations. When you use the tool, all references are updated accordingly whenever you change the account or password. The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager performs the following extra steps to ensure the report server remains available:

- Automatically adds the new account to the report server group created on the local computer. This group is specified in the access control lists (ACLs) that secure [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] files.

- Automatically updates the sign-in permissions on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance used to host the report server database. The new account is added to the **RSExecRole**.

   The database sign-in for the old account isn't removed automatically. Be sure to remove accounts that are no longer in use. For more information, see [Administer a report server database &#40;SSRS native mode&#41;](../../reporting-services/report-server/administer-a-report-server-database-ssrs-native-mode.md).

   Granting database permissions to a new service account only occurs if you configured the report server database connection to use the service account in the first place. If you configured the report server database connection to use a domain user account or a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database sign-in, the service account update doesn't affect the connection information.

- Automatically updates the encryption key to include the profile information of the new account.

   > [!NOTE]
   > If the report server is part of the scale-out deployment, only the report server that you are updating is affected. The encryption keys for other report servers in the deployment are unaffected by the service account change.

## Configure the report server service account

1. Start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager and connect to the report server.

1. On the Service Account page, select the option that describes the type of account you want to use.

1. If you selected a Windows user account, specify the new account and password. The account can't be more than 20 characters and can't contain special characters ```" / \ [ ] : ; | = , + * ? < > '``` per Windows user account naming rules.

   If you deploy the report server in a network that supports Kerberos authentication, you must register the report server Service Principal Name (SPN) with the domain user account you specified. For more information, see [Register a Service Principal Name &#40;SPN&#41; for a report server](../../reporting-services/report-server/register-a-service-principal-name-spn-for-a-report-server.md).

1. Select **Apply**.

1. When prompted to back up the symmetric key, enter a file name and location for the symmetric key backup. Enter a password to lock and unlock the file, and then select **OK**.

1. If the report server uses the service account to connect to the report server database, the connection information is updated to use the new account or password. Updating the connection information requires that you connect to the database. If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **Database Connection** dialog box appears, enter credentials that have permission to connect to the database, and then select **OK**.

1. When prompted to restore the symmetric key, enter the password you specified in step 5, and then select **OK**.

1. To verify all tasks completed successfully, review the status messages in the Results pane.

## Choose an account

 For best results, specify an account that has network connection permissions, with access to network domain controllers and corporate Simple Mail Transfer Protocol(SMTP) servers or gateways. The following table summarizes the accounts and provides recommendations for how to use them.

> [!NOTE]
> [Group Managed Service Accounts Overview](/windows-server/security/group-managed-service-accounts/group-managed-service-accounts-overview) aren't supported as a report server service account.

|Account|Explanation|
|-------------|-----------------|
|Domain user accounts|If you have a Windows domain user account that has the minimum permissions required for report server operations, you should use it.<br /><br /> You should use a domain user account because it isolates the Report Server service from other applications. If you run multiple applications under a shared account, such as Network Service, you increase the risk of a malicious user taking control of the report server. A security breach for any one application can easily extend to all applications that run under the same account.<br /><br /> If you use a domain user account, you have to change the password periodically if your organization enforces a password expiration policy. You might also need to register the service with the user account. For more information, see [Register a Service Principal Name &#40;SPN&#41; for a report server](../../reporting-services/report-server/register-a-service-principal-name-spn-for-a-report-server.md).<br /><br /> Avoid local Windows user accounts. Local accounts typically don't have sufficient permission to access resources on other computers. For more information about how local accounts limit report server functionality, see [Considerations for using local accounts](#localaccounts).|
|**Virtual Service Account**|**Virtual Service Account** represents the Windows service. It's a built-in least-privilege account that has network sign-in permissions. You should use this account if you don't have a domain user account available or if you want to avoid any service disruptions that might occur as a result of password expiration policies.|
|**Network Service**|**Network Service** is a built-in least-privilege account that has network sign-in permissions. <br /><br /> If you select **Network Service**, try to minimize the number of other services that run under the same account. A security breach for any one application compromises the security of all other applications that run under the same account.|
|**Local Service**|**Local Service** is a built-in account that's like an authenticated local Windows user account. Services that run as the **Local Service** account access network resources as a null session with no credentials. This account isn't appropriate for intranet deployment scenarios where the report server must connect to a remote report server database or a network domain controller to authenticate a user before opening a report or processing a subscription.|
|**Local System**|**Local System** is a highly privileged account that isn't required for running a report server. Avoid this account for report server installations. Choose a domain account or **Network Service** instead.|

## <a name="localaccounts"></a> Considerations for local accounts

The primary consideration for local accounts is whether the report server requires access to remote database servers, mail servers, and domain controllers. If you configure the report server to run as a local Windows user account, Local Service, or Local System, you introduce considerations that must be factored into how you set other configuration settings. On subscription creation and delivery, you must also consider the following:

- Running the service under a local account limits your options later if you configure a connection to a remote report server database. Specifically, if you use a remote report server database, you have to configure the connection to use a domain user account or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database user that has permission to sign in the remote [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.

- Running the service under a local account introduces new requirements on subscription creation. The report server stores information about the user who creates the subscription. If the user creates the subscription while logged on under a domain account, the Report Server service tries to connect to a domain controller to authenticate the user when the subscription is processed. If the service runs under a local account, the authentication request fails when the report server tries to send the request to a remote domain controller. To work around this limitation, you can use a custom forms-based authentication extension or have all users connect to a report server under a local user account.

- Running the service under a local account introduces new requirements for subscription delivery. Some delivery extensions have user account information in the subscription definition. If you send reports to email addresses that are based on domain user accounts, and you run the Report Server service under a local account, it can't access a remote domain controller to resolve the target email account.

- Built-in Windows service accounts (Local Service or Network Service) aren't supported as report server service accounts on a computer that's a domain controller.

For help with deciding on an approach that's best for your deployment, see the following guidelines:

- [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)

- [The services and service accounts security planning guide](https://www.zubairalexander.com/blog/services-and-service-accounts-security-planning-guide/)

## Update an expired password

If the Report Server service runs under a domain account and the password expires before you can update it in the Report Server Configuration Manager, the service doesn't start until you specify a new password.

If the service account password for the [!INCLUDE[ssDE](../../includes/ssde-md.md)] expires, the **rsReportServerDatabaseUnavailable** error occurs when you try to connect to the report server. Resetting the password resolves this error.

## Troubleshoot service identity update errors

Changing the service identity initiates a series of events that include restarting the service, updating the password-protected encryption key, updating URL reservations, and updating the report server database connection information if you use the service account to connect to the report server database. You can monitor the status of these events by viewing the notifications in the Results panel at the bottom of the page. If errors occur during this process, you can try to resolve them by using the following techniques:

- If the symmetric key can't be restored, you can try to restore it manually by using **Restore** in the Encryption Keys page. If that doesn't work, consider deleting the encrypted content. You have to re-create data source connection information and subscriptions, but the rest of your content still is available. For more information, see [Back up and restore SQL Server Reporting Services (SSRS) encryption keys](../../reporting-services/install-windows/ssrs-encryption-keys-back-up-and-restore-encryption-keys.md).

- If the service doesn't start, restart it manually by using the Services console application in Administrator Tools.

- URL reservation errors can occur when you update the service account. Each URL reservation includes a security descriptor that includes a Discretionary Access Control List (DACL) that grants permission to the service account to accept requests on the URL. When you update the account, the URL must be recreated to update the DACL with the new account information. If the URL reservation can't be recreated, and you know the account to be valid, try to restart the computer. If the error persists, try to use a different account.

## Related content

- [Configure report server URLs (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md)
- [Report Server Configuration Manager (Native Mode)](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)
