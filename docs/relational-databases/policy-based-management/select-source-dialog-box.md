---
title: "Select Source dialog box"
description: "Select Source dialog box"
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: ui-reference
f1_keywords:
  - "sql13.swb.dmf.selectsource.f1"
---
# Select Source dialog box

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use this dialog box to select the source of the policies to be run. To select one or more XML files that contain policies, select **Files**. To run the policies that are found on the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], select **Server**.

You can open this dialog box in several ways.

**To open this dialog box**

- In Registered Servers, right-click **Local Server Groups** or any server under **Local Server Groups**, or any server under **Central Management Servers**, and then select **Evaluate Policies**. In the **Policy Selection** page of the **Evaluate Policies** dialog box, select the Browse (**...**) button.

- In Object Explorer, expand **Management**, expand **Policy Management**, right-click **Policies**, and select **Import Policy**. In the **Import** dialog box, select the Browse (**...**) button.

- In Object Explorer, right-click a server, database, or database object, select **Policies**, and then select **Evaluate**. In the **Policy Selection** page of the **Evaluate Policies** dialog box, select the Browse (**...**) button.

## Options

**Files**  
Select one or more XML files that contain policies.

**Server**  
Enables you to select a server that contains the policies that you want to run.

**Server type**  
Only [!INCLUDE [ssDE](../../includes/ssde-md.md)] servers contain policies. This box is read-only.

**Server name**  
Select the server instance to connect to. By default, the server instance last connected to is displayed.

**Authentication**  
Two authentication modes are available when you connect to an instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

**Windows Authentication Mode (Windows Authentication)**  
Windows Authentication mode allows for a user to connect through a Windows user account.

**SQL Server Authentication**  
When a user connects with a specified login name and password from a nontrusted connection, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] performs the authentication itself by checking whether a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login account has been set up and whether the specified password matches the one previously recorded. If [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't have a login account set, authentication fails, and the user receives an error message.

> [!IMPORTANT]  
> When possible, use Windows Authentication.

**User name**  
Enter the user name to connect with. This option is available only if you have selected to connect by using Windows Authentication.

**Login**  
Enter the login to connect with. This option is available only if you have selected to connect by using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.

**Password**  
Enter the password for the login. This option is editable only if you have selected to connect by using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.

## Related content

- [Policy management node (object explorer)](policy-management-node-object-explorer.md)
- [Administer Servers by Using Policy-Based Management](administer-servers-by-using-policy-based-management.md)
