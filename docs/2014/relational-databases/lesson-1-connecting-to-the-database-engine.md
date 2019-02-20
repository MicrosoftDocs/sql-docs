---
title: "Lesson 1: Connecting to the Database Engine | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
ms.assetid: e8db82f0-50ed-4531-9209-940006ed34cb
author: rothja
ms.author: jroth
manager: craigg
---
# Lesson 1: Connecting to the Database Engine
  When you install the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)], the tools that are installed depend upon the edition and your setup choices. This lesson reviews the principal tools, and shows you how to connect and perform a basic function (authorizing more users).  
  
  
  
##  <a name="tools"></a> Tools For Getting Started  
 The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] ships with a variety of tools. This topic describes the first tools you will need, and helps you select the right tool for the job. All tools can be accessed from the **Start** menu. Some tools, such as [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], are not installed by default. You must select the tools as part of the client components during setup. For a complete description of the tools described below, search for them in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Books Online. [!INCLUDE[ssExpress](../includes/ssexpress-md.md)] contains only a subset of the tools.  
  
### Basic Tools  
  
-   [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] is the principal tool for administering the [!INCLUDE[ssDE](../includes/ssde-md.md)] and writing [!INCLUDE[tsql](../includes/tsql-md.md)] code. It is hosted in the [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] shell. It is not included in [!INCLUDE[ssExpress](../includes/ssexpress-md.md)] but is available as a separate download from [Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkId=144346).  
  
-   [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager installs with both [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and the client tools. It lets you enable server protocols, configure protocol options such as TCP ports, configure server services to start automatically, and configure client computers to connect in your preferred manner. This tool configures the more advanced connectivity elements but does not enable features.  
  
### Sample Database  
 The sample databases and samples are not included with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Most of the examples that are described in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Books Online use the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] sample database.  
  
##### To start SQL Server Management Studio  
  
-   On the **Start** menu, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../includes/sscurrentui-md.md)], and then click **SQL Server Management Studio**.  
  
##### To start SQL Server Configuration Manager  
  
-   On the **Start** menu, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then click **SQL Server Configuration Manager**.  
  
##  <a name="connect"></a> Connecting with Management Studio  
 It is easy to connect to the [!INCLUDE[ssDE](../includes/ssde-md.md)] from tools that are running on the same computer if you know the name of the instance, and if you are connecting as a member of the Administrators group on the computer. The following procedures must be performed on the same computer that hosts [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
##### To determine the name of the instance of the Database Engine  
  
1.  Log into Windows as a member of the Administrators group, and open [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)].  
  
    > [!IMPORTANT]  
    >  If you are connecting to  [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)] on [!INCLUDE[wiprlhlong](../includes/wiprlhlong-md.md)] or [!INCLUDE[nextref_longhorn](../includes/nextref-longhorn-md.md)] (or more recent), you may need to right-click [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] and then click **Run as Administrator** in order to connect using your Administrator credentials. Starting in [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)], setup adds selected logins to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], so your Administrator credentials are not necessary.  
  
2.  In the **Connect to Server** dialog box, click **Cancel**.  
  
3.  If Registered Servers is not displayed, on the **View** menu, click **Registered Servers**.  
  
4.  With **Database Engine** selected on the Registered Servers toolbar, expand **Database Engine**, right-click **Local Server Groups**, point to **Tasks**, and then click **Register Local Servers**. All instances of the [!INCLUDE[ssDE](../includes/ssde-md.md)] installed on the computer are displayed. The default instance is unnamed and is shown as the computer name. A named instance displays as the computer name followed by a backward slash (\\) and then the name of the instance. For [!INCLUDE[ssExpress](../includes/ssexpress-md.md)], the instance is named *<computer_name>*\sqlexpress unless the name was changed during setup.  
  
##### To verify that the Database Engine is running  
  
1.  In Registered Servers, if the name of your instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] has a green dot with a white arrow next to the name, the [!INCLUDE[ssDE](../includes/ssde-md.md)] is running and no further action is necessary.  
  
2.  If the name of your instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] has a red dot with a white square next to the name, the [!INCLUDE[ssDE](../includes/ssde-md.md)] is stopped. Right-click the name of the [!INCLUDE[ssDE](../includes/ssde-md.md)], click **Service Control**, and then click **Start**. After a confirmation dialog box, the [!INCLUDE[ssDE](../includes/ssde-md.md)] should start and the circle should turn green with a white arrow.  
  
##### To connect to the Database Engine  
  
1.  In [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)], on the **File** menu, click **Connect Object Explorer**.  
  
     The **Connect to Server** dialog box opens. The **Server type** box displays the type of component that was last used.  
  
2.  Select **Database Engine**.  
  
3.  In the **Server name** box, type the name of the instance of the [!INCLUDE[ssDE](../includes/ssde-md.md)]. For the default instance of SQL Server, the server name is the computer name. For a named instance of SQL Server, the server name is the *<computer_name>***\\***<instance_name>,* such as **ACCTG_SRVR\SQLEXPRESS**.  
  
4.  Click **Connect**.  
  
##  <a name="additional"></a> Authorizing Additional Connections  
 Now that you have connected to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] as an administrator, one of your first tasks is to authorize other users to connect. You do this by creating a login and authorizing that login to access a database as a user. Logins can be either Windows Authentication logins, which use credentials from Windows, or SQL Server Authentication logins, which store the authentication information in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and are independent of your Windows credentials. Use Windows Authentication whenever possible.  
  
##### Create a Windows Authentication login  
  
1.  In the previous task, you connected to the [!INCLUDE[ssDE](../includes/ssde-md.md)] using [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)]. In Object Explorer, expand your server instance, expand **Security**, right-click **Logins**, and then click **New Login**.  
  
     The **Login - New** dialog box appears.  
  
2.  On the **General** page, in the **Login name** box, type a Windows login in the format *\<domain>\\<login\>*.  
  
3.  In the **Default database** box, select [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] if available. Otherwise select **master**.  
  
4.  On the **Server Roles** page, if the new login is to be an administrator, click **sysadmin**, otherwise leave this blank.  
  
5.  On the **User Mapping** page, select **Map** for the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database if it is available. Otherwise select **master**. Note that the **User** box is populated with the login. When closed, the dialog box will create this user in the database.  
  
6.  In the **Default Schema** box, type **dbo** to map the login to the database owner schema.  
  
7.  Accept the default settings for the **Securables** and **Status** boxes and click **OK** to create the login.  
  
> [!IMPORTANT]  
>  This is basic information to get you started. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provides a rich security environment, and security is obviously an important aspect of database operations.  
  
## Next Lesson  
 [Lesson 2: Connecting from Another Computer](lesson-2-connecting-from-another-computer.md)  
  
  
