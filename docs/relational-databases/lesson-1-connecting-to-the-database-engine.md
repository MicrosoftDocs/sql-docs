---
title: "Lesson 1: Connecting to the Database Engine"
description: "Lesson 1: Connecting to the Database Engine"
author: MashaMSFT
ms.author: mathoma
ms.reviewer: maghan
ms.date: 09/26/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: quickstart
ms.custom: intro-quickstart
---

# Lesson 1: Connecting to the Database Engine

[!INCLUDE [sqlserver](../includes/applies-to-version/sqlserver.md)]

When you install the [!INCLUDE [ssDEnoversion](../includes/ssdenoversion-md.md)], the tools that are installed depend upon the edition and your setup choices. This lesson reviews the principal tools and shows how to connect and perform an essential function (authorizing more users).

This lesson contains the following tasks:

- [Tools for getting started](#tools)
- [Connecting with Management Studio](#connect)
- [Authorizing extra connections](#additional)

## <a id="tools"></a> Tools for getting started

- The [!INCLUDE [ssDEnoversion](../includes/ssdenoversion-md.md)] ships with various tools. This article describes the first tools you need and helps you select the right tool for the job. All tools can be accessed from the **Start** menu. Some tools, such as [SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md), aren't installed by default. Select the tools you want as part of the client components during setup. For a complete description of the tools below, search for them in SQL Server Books Online. [!INCLUDE [ssExpress](../includes/ssexpress-md.md)] contains only a subset of the tools.

### Common tools

The following table describes some of the more common client tools.

| Client tool | Type | Operating system |
| --- | --- | --- |
| **[SQL Server Management Studio (SSMS)](../ssms/sql-server-management-studio-ssms.md)** | GUI | Windows |
| **[Azure Data Studio](/azure-data-studio/what-is-azure-data-studio)** | GUI | Windows, macOS, Linux |
| **[bcp](../tools/bcp-utility.md)** | CLI | Windows, macOS, Linux |
| **[sqlcmd](../tools/sqlcmd/sqlcmd-utility.md)** | CLI | Windows, macOS, Linux |

For this article, we are going to focus on connecting via SSMS. If you are interested in connecting via Azure Data Studio, see [Quickstart: Use Azure Data Studio to connect and query SQL Server](/azure-data-studio/quickstart-sql-server).

### Sample database

[!INCLUDE [article-uses-adventureworks](../includes/article-uses-adventureworks.md)]

#### SQL Server Management Studio (Windows only)

- On current versions of Windows, on the **Start** page, type SSMS, and then select **Microsoft SQL Server Management Studio**.
- When using older versions of Windows, on the **Start** menu, point to **All Programs**, point to [!INCLUDE [ssCurrentUI](../includes/sscurrentui-md.md)], and then select **SQL Server Management Studio**.

## <a id="connect"></a> Connect with SSMS

- It's easy to connect to the [!INCLUDE [ssDE](../includes/ssde-md.md)] from tools that are running on the same computer if you know the name of the instance and if you're connecting as a member of the local Administrators group on the computer. The following procedures must be performed on the same computer that hosts SQL Server.

> [!NOTE]  
> This topic discusses connecting to an on-premises SQL Server. For Azure SQL Database, see [Connect to Azure SQL Database](../sql-server/connect-to-database-engine.md#connect-to-azure-sql).

#### Determine the name of the instance of the Database Engine

1. Log into Windows as a member of the Administrators group, and open [!INCLUDE [ssManStudio](../includes/ssmanstudio-md.md)].

1. In the **Connect to Server** dialog box, select **Cancel**.

1. If Registered Servers isn't displayed, on the **View** menu, select **Registered Servers**.

1. With **Database Engine** selected on the Registered Servers toolbar, expand **Database Engine**, right-click **Local Server Groups**, point to **Tasks**, and then select **Register Local Servers**. Expand **Local Server Groups** to see all the instances of the [!INCLUDE [ssDE](../includes/ssde-md.md)] installed on the computer displayed. The default instance is unnamed and is shown as the computer name. A named instance displays as the computer name followed by a backward slash (\\) and then the instance's name. For [!INCLUDE [ssExpress](../includes/ssexpress-md.md)], the instance is named *<computer_name>*\sqlexpress unless the name was changed during setup.

#### Verify that the Database Engine is running

1. In Registered Servers, if the name of your instance of SQL Server has a green dot with a white arrow next to the name, the [!INCLUDE [ssDE](../includes/ssde-md.md)] is running and no further action is necessary.

1. If the name of your instance of SQL Server has a red dot with a white square next to the name, the [!INCLUDE [ssDE](../includes/ssde-md.md)] is stopped. Right-click the name of the [!INCLUDE [ssDE](../includes/ssde-md.md)], select **Service Control**, and then select **Start**. After a confirmation dialog box, the [!INCLUDE [ssDE](../includes/ssde-md.md)] should start, and the circle should turn green with a white arrow.

#### Connect to the Database Engine

At least one administrator account was selected when [!INCLUDE [ssNoVersion_md](../includes/ssnoversion-md.md)] was being installed. Perform the following step while logged into Windows as an administrator.

1. In [!INCLUDE [ssManStudio](../includes/ssmanstudio-md.md)], on the **File** menu, select **Connect Object Explorer**.

    - The **Connect to Server** dialog box opens. The **Server type** box displays the type of component that was last used.

1. Select **Database Engine**.

   :::image type="content" source="media/lesson-1-connecting-to-the-database-engine/object-explorer.png" alt-text="Screenshot of the Object Explorer showing the Connect dropdown list and the Database Engine option called out." lightbox="media/lesson-1-connecting-to-the-database-engine/object-explorer.png":::

1. In the **Server name** box, type the name of the instance of the Database Engine. For the default instance of SQL Server, the server name is the computer name. The server name for a named instance of SQL Server is the *\<computer_name\>***\\***\<instance_name\>*, such as **ACCTG_SRVR\SQLEXPRESS**. The following screenshot shows connecting to the default (unnamed) instance of [!INCLUDE [ssNoVersion_md](../includes/ssnoversion-md.md)] on a computer named `PracticeComputer`. The user logged into Windows is Mary from the Contoso domain. When using Windows Authentication, you can't change the user name.

   :::image type="content" source="media/lesson-1-connecting-to-the-database-engine/connect-to-server.png" alt-text="Screenshot of the Connect to Server dialog box with the Server name text box called out." lightbox="media/lesson-1-connecting-to-the-database-engine/connect-to-server.png":::

1. Select **Connect**.

> [!NOTE]  
> This tutorial assumes you are new to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and have no special problems connecting. For detailed troubleshooting steps, see [Troubleshooting Connecting to the SQL Server Database Engine](/troubleshoot/sql/connect/network-related-or-instance-specific-error-occurred-while-establishing-connection).

## <a id="additional"></a> Authorize extra connections

Now that you've connected to SQL Server as an administrator, one of your first tasks is authorizing other users to connect. You do this by creating a login and authorizing that login to access a database as a user. Logins can be created using Windows authentication, SQL authentication, or Microsoft Entra authentication. Windows authentication logins use credentials from Windows. SQL authentication logins store the authentication information in SQL Server and are independent of your Windows credentials. Logins from Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) use credentials from cloud-based identities. You can learn more about this method from the following article - [Use Microsoft Entra authentication](/azure/azure-sql/database/authentication-aad-overview).

Use Windows Authentication whenever possible.

> [!TIP]  
> Most organizations have domain users and will use Windows Authentication. You can experiment by creating additional local users on your computer. Your computer will authenticate local users, so the domain is the computer name. For example, if your computer is named `MyComputer` and you create a user named `Test`, then the Windows description of the user is `Mycomputer\Test`.

#### Create a Windows Authentication login

1. In the previous task, you connected to the [!INCLUDE [ssDE](../includes/ssde-md.md)] using [!INCLUDE [ssManStudio](../includes/ssmanstudio-md.md)]. In Object Explorer, expand your server instance, expand **Security**, right-click **Logins**, and then select **New Login**. The **Login - New** dialog box appears.

1. On the **General** page, in the **Login name** box, type a Windows login in the format: `<domain>\<login>`

   :::image type="content" source="media/lesson-1-connecting-to-the-database-engine/new-login.png" alt-text="Screenshot of the Login - New dialog box with the Login name text box called out.":::

1. In the **Default database** box, select the AdventureWorks database if available. Otherwise, select the `master` database.

1. On the **Server Roles** page, if the new login is to be an administrator, select **sysadmin**. Otherwise, leave this blank.

1. On the **User Mapping** page, select **Map** for the [!INCLUDE [ssSampleDBobject](../includes/sssampledbobject-md.md)] database if it's available. Otherwise, select `master`. The **User** box is populated with the login. When closed, the dialog box creates this user in the database.

1. In the **Default Schema** box, type **dbo** to map the login to the database owner schema.

1. Accept the default settings for the **Securables** and **Status** boxes and select **OK** to create the login.

> [!IMPORTANT]  
> This is basic information to get you started. SQL Server provides a rich security environment.

## Related content

- [Connect to SQL Server](../sql-server/connect-to-database-engine.md)
- [Troubleshoot Connecting to the SQL Server Database Engine](/troubleshoot/sql/connect/network-related-or-instance-specific-error-occurred-while-establishing-connection)
- [Quickstart: Use SSMS to connect and query Azure SQL database](../ssms/quickstarts/ssms-connect-query-azure-sql.md)
- [Quickstart: Use Azure Data Studio to connect and query Azure SQL database](/azure-data-studio/quickstart-sql-server)

## Next step

> [!div class="nextstepaction"]
> [Lesson 2: Connecting from Another Computer](../relational-databases/lesson-2-connecting-from-another-computer.md)