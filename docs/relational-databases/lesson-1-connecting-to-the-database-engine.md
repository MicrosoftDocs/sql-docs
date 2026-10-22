---
title: "Lesson 1: Connecting to the Database Engine"
description: Learn about the main tools of the Database Engine and how to connect to the engine and authorize more users.
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 01/28/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: quickstart
ms.update-cycle: 365-days
ms.custom:
  - intro-quickstart
  - sfi-image-nochange
---

# Lesson 1: Connecting to the Database Engine

[!INCLUDE [sqlserver](../includes/applies-to-version/sqlserver.md)]

When you install the [!INCLUDE [ssDEnoversion](../includes/ssdenoversion-md.md)], the tools that are installed depend upon the edition and your setup choices. This lesson describes the principal tools, and demonstrates how to connect to the Database Engine and perform an essential function (authorizing more users).

In this lesson, learn the following:

- [Tools to get started](#tools)
- [Connecting with Management Studio](#connect)
- [Authorizing extra connections](#additional)

<a id="tools"></a>

## Tools for getting started

The [!INCLUDE [ssDEnoversion](../includes/ssdenoversion-md.md)] ships with various tools. This article describes the first tools you need and helps you select the right tool for a job. All tools can be accessed from the **Start** menu. Some tools, such as [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms), aren't installed by default. Select the tools that you want as components during setup. [!INCLUDE [ssExpress](../includes/ssexpress-md.md)] contains only a subset of the tools.

### Common tools

The following table describes some of the more common tools.

| Tool | Type | Operating system |
| --- | --- | --- |
| **[SSMS](/ssms/install/install)** | GUI | Windows |
| **[bcp](../tools/bcp-utility.md)** | CLI | Windows, macOS, Linux |
| **[sqlcmd](../tools/sqlcmd/sqlcmd-utility.md)** | CLI | Windows, macOS, Linux |

### Sample database

[!INCLUDE [article-uses-adventureworks](../includes/article-uses-adventureworks.md)]

#### SSMS (Windows only)

- On current versions of Windows, on the **Start** page, enter **SSMS**, and then select **SQL Server Management Studio**.
- On the **Start** menu of older versions of Windows, point to **All Programs**, point to [!INCLUDE [ssCurrentUI](../includes/sscurrentui-md.md)], and then select **SQL Server Management Studio**.

<a id="connect"></a>

## Connect with SSMS

It's easy to connect to the [!INCLUDE [ssDE](../includes/ssde-md.md)] from tools that are running on the same computer that hosts SQL Server if you know the name of the instance and if you're connecting as a member of the local Administrators group on the computer. The following procedures must be performed on the same computer that hosts SQL Server.

> [!NOTE]  
> This article discusses connecting to an on-premises SQL Server. For Azure SQL Database, see [Connect to Azure SQL Database](../sql-server/connect-to-database-engine.md#connect-to-azure-sql).

#### Determine the name of the instance of the Database Engine

1. Sign in to Windows as a member of the Administrators group, and open [!INCLUDE [ssManStudio](../includes/ssmanstudio-md.md)].

1. In the **Connect to Server** dialog, select **Cancel**.

1. If Registered Servers isn't displayed, on the **View** menu, select **Registered Servers**.

1. From the **Registered Servers** toolbar, go to **Database Engine**, and right-click **Local Server Groups**. Go to **Tasks** > **Register Local Servers**, and expand **Local Server Groups** to see all the instances of the [!INCLUDE [ssDE](../includes/ssde-md.md)] installed on the computer. The default instance is unnamed and is shown as the computer name. A named instance displays as the computer name followed by a backward slash (`\`) and then the instance's name. For [!INCLUDE [ssExpress](../includes/ssexpress-md.md)], the instance is named *<computer_name>*\sqlexpress unless the name was changed during setup.

#### Verify that the Database Engine is running

- In Registered Servers, if the name of your instance of SQL Server has a green dot with a white arrow next to the name, the [!INCLUDE [ssDE](../includes/ssde-md.md)] is running and no further action is necessary.

- If the name of your instance of SQL Server has a red dot with a white square next to the name, the [!INCLUDE [ssDE](../includes/ssde-md.md)] is stopped. Right-click the name of the [!INCLUDE [ssDE](../includes/ssde-md.md)], select **Service Control**, and then select **Start**. After a confirmation dialog box, the [!INCLUDE [ssDE](../includes/ssde-md.md)] starts, and the circle turns green with a white arrow.

#### Connect to the Database Engine

Select at least one administrator account when you install [!INCLUDE [ssNoVersion_md](../includes/ssnoversion-md.md)]. Perform the following steps while signed in to Windows as an administrator.

1. In [!INCLUDE [ssManStudio](../includes/ssmanstudio-md.md)], on the **File** menu, select **Connect Object Explorer**.

   The **Connect to Server** dialog opens. The **Server type** box displays the type of component that you last used.

1. Select **Database Engine**.

   :::image type="content" source="media/lesson-1-connecting-to-the-database-engine/object-explorer.png" alt-text="Screenshot of the Object Explorer. The Database Engine option in the Connect list is highlighted." lightbox="media/lesson-1-connecting-to-the-database-engine/object-explorer.png":::

1. In the **Server name** box, enter the name of the instance of the Database Engine. For the default instance of SQL Server, the server name is the computer name. The server name for a named instance of SQL Server is *\<computer_name\>***\\***\<instance_name\>*. For example, **ACCTG_SRVR\SQLEXPRESS**. The following screenshot shows the options for connecting to the default (unnamed) instance of [!INCLUDE [ssNoVersion_md](../includes/ssnoversion-md.md)] on a computer named `PracticeComputer`. The user signed in to Windows is Mary from the Contoso domain. When using Windows Authentication, you can't change the user name.

   :::image type="content" source="media/lesson-1-connecting-to-the-database-engine/connect-to-server.png" alt-text="Screenshot of the Connect to Server dialog box. The Server name box is highlighted." lightbox="media/lesson-1-connecting-to-the-database-engine/connect-to-server.png":::

1. Select **Connect**.

> [!NOTE]  
> This article assumes that you're new to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] and have no problems connecting. For detailed troubleshooting steps, see [Troubleshoot connecting to the SQL Server Database Engine](/troubleshoot/sql/connect/network-related-or-instance-specific-error-occurred-while-establishing-connection).

<a id="additional"></a>

## Authorize extra connections

Now that you're connected to SQL Server as an administrator, you can authorize other users to connect. Create a login, and authorize that login to access a database as a user. You create logins using Windows Authentication, SQL Server authentication, or Microsoft Entra authentication. Windows Authentication logins use credentials from Windows. SQL Server authentication logins store the authentication information in SQL Server and are independent of your Windows credentials. Logins from Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) use credentials from cloud-based identities. For more information about this method, see [Microsoft Entra authentication for SQL Server](security/authentication-access/azure-ad-authentication-sql-server-overview.md).

Use Windows Authentication or Microsoft Entra authentication whenever possible.

> [!TIP]  
> Most organizations have domain users and use Windows Authentication. You can experiment by creating additional local users on your computer. Your computer authenticates local users, so the domain is the computer name. For example, if your computer is named `MyComputer` and you create a user named `Test`, the Windows description of the user is `Mycomputer\Test`.

#### Create a Windows Authentication login

1. In the previous task, you connected to the [!INCLUDE [ssDE](../includes/ssde-md.md)] by using [!INCLUDE [ssManStudio](../includes/ssmanstudio-md.md)]. In Object Explorer, expand your server instance, expand **Security**, right-click **Logins**, and then select **New Login**. The **Login - New** dialog appears.

1. On the **General** page, in the **Login name** box, enter a Windows login in the format `<domain>\<login>`.

   :::image type="content" source="media/lesson-1-connecting-to-the-database-engine/new-login.png" alt-text="Screenshot of the Login - New dialog. The Login name box is highlighted.":::

1. In the **Default database** box, select the AdventureWorks database if it's available. Otherwise, select the `master` database.

1. On the **Server Roles** page, if the new login is to be an administrator, select **sysadmin**. Otherwise, leave this blank.

1. On the **User Mapping** page, select **Map** for the [!INCLUDE [ssSampleDBobject](../includes/sssampledbobject-md.md)] database if it's available. Otherwise, select `master`. The **User** box is populated with the login. When you close the dialog, the user is created in the database.

1. In the **Default Schema** box, enter **dbo** to map the login to the database owner schema.

1. Accept the default settings for the **Securables** and **Status** boxes, and then select **OK** to create the login.

> [!IMPORTANT]  
> This section provides basic information to get you started. SQL Server provides a rich security environment.

## Related tasks

- [Connect to the Database Engine](../sql-server/connect-to-database-engine.md)
- [Troubleshoot connecting to the SQL Server Database Engine](/troubleshoot/sql/connect/network-related-or-instance-specific-error-occurred-while-establishing-connection)
- [Quickstart: Connect and query an Azure SQL Database or an Azure SQL Managed Instance by using SSMS](/ssms/quickstarts/ssms-connect-query-azure-sql)

## Next step

> [!div class="nextstepaction"]
> [Lesson 2: Connect to the Database Engine from another computer](lesson-2-connecting-from-another-computer.md)
