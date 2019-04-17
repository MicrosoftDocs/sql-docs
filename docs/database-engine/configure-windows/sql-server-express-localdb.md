---
title: "SQL Server Express LocalDB | Microsoft Docs"
ms.custom: ""
ms.date: "04/17/2019"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
- "user instances"
- "LocalDB, described"
- "local database runtime"
- "file database"
- "LocalDB"
ms.assetid: 5a641a46-7cfb-4d7b-a90d-6e4625719d74
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---

# SQL Server Express LocalDB

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

Microsoft SQL Server Express LocalDB is a feature of [SQL Server Express](../../sql-server/editions-and-components-of-sql-server-2016.md) targeted to developers. It is available on SQL Server Express with Advanced Services.

LocalDB installation copies a minimal set of files necessary to start the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. Once LocalDB is installed, you can initiate a connection using a special connection string. When connecting, the necessary [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] infrastructure is automatically created and started, enabling the application to use the database without complex configuration tasks. Developer Tools can provide developers with a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] that lets them write and test [!INCLUDE[tsql](../../includes/tsql-md.md)] code without having to manage a full server instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. 

## Try it out! 

- To download and install SQL Server Express LocalDB, go to **[SQL Server downloads](https://www.microsoft.com/sql-server/sql-server-downloads)**. LocalDB is a feature you select during installation, and is available when you download the media. If you download the media, either choose **Express Advanced** or the LocalDB package. In the **Visual Studio Installer**, you can install SQL Server Express LocalDB as part of the **.NET desktop development** workload or as an individual component.

>[!TIP]
> You can also install LocalDB as part of Visual Studio. During Visual Studio installation, select the **.NET desktop development** workload, which includes SQL Server Express LocalDB.

- Have an Azure account?Then go **[here](https://azure.microsoft.com/services/virtual-machines/sql-server/)** to spin up a Virtual Machine with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] already installed.

## Install LocalDB

Install LocalDB through the installation wizard or by using the SqlLocalDB.msi program. LocalDB is an option when installing [!INCLUDE[ssExpCurrent](../../includes/ssexpcurrent-md.md)]. 
 
Select LocalDB on the **Feature Selection/Shared Features** page during installation. There can be only one installation of the LocalDB binary files for each major [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] version. Multiple [!INCLUDE[ssDE](../../includes/ssde-md.md)] processes can be started and will all use the same binaries. An instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] started as the LocalDB has the same limitations as [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)].

An instance of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] LocalDB is managed by using the `SqlLocalDB.exe` utility. [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] LocalDB should be used in place of the [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] user instance feature, which was deprecated.

## Description

The LocalDB setup program uses the `SqlLocalDB.msi` program to install the necessary files on the computer. Once installed, LocalDB is an instance of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] that can create and open [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases. The system database files for the database are stored in the local AppData path, which is normally hidden. For example, `C:\Users\<user>\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\LocalDBApp1\`. User database files are stored where the user designates, typically somewhere in the `C:\Users\<user>\Documents\` folder.

For more information about including LocalDB in an application, see [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] [Local Data Overview](https://docs.microsoft.com/previous-versions/visualstudio/visual-studio-2012/ms233817(v=vs.110)), [Create a database and add tables in Visual Studio](/visualstudio/data-tools/create-a-sql-database-by-using-a-designer).

For more information about the LocalDB API, see [SQL Server Express LocalDB Reference](../../relational-databases/sql-server-express-localdb-reference.md).

The `SqlLocalDb` utility can create new instances of LocalDB, start and stop an instance of LocalDB, and includes options to help you manage LocalDB.For more information about the `SqlLocalDb` utility, see [SqlLocalDB Utility](../../tools/sqllocaldb-utility.md).

The instance collation for LocalDB is set to `SQL_Latin1_General_CP1_CI_AS` and cannot be changed. Database-level, column-level, and expression-level collations are supported normally. Contained databases follow the metadata and `tempdb` collations rules defined by [Contained Database Collations](../../relational-databases/databases/contained-database-collations.md).

### Restrictions

- LocalDB cannot be a merge replication subscriber.

- LocalDB does not support FILESTREAM.

- LocalDB only allows local queues for Service Broker.

- An instance of LocalDB owned by the built-in accounts such as `NT AUTHORITY\SYSTEM` can have manageability issues due to windows file system redirection. Instead use a normal windows account as the owner.

### Automatic and Named Instances

LocalDB supports two kinds of instances: Automatic instances and named instances.

- Automatic instances of LocalDB are public. They are created and managed automatically for the user and can be used by any application. One automatic instance of LocalDB exists for every version of LocalDB installed on the user's computer. Automatic instances of LocalDB provide seamless instance management. There is no need to create the instance; it just works. This feature allows for easy application installation and migration to a different computer. If the target machine has the specified version of LocalDB installed, the automatic instance of LocalDB for that version is available on the target machine as well. Automatic instances of LocalDB have a special pattern for the instance name that belongs to a reserved namespace. Automatic instances prevents name conflicts with named instances of LocalDB. The name for the automatic instance is **MSSQLLocalDB**.

- Named instances of LocalDB are private. They are owned by a single application that is responsible for creating and managing the instance. Named instances provide isolation from other instances and can improve performance by reducing resource contention with other database users. Named instances must be created explicitly by the user through the LocalDB management API or implicitly via the app.config file for a managed application (although managed application may also use the API, if desired). Each named instance of LocalDB has an associated LocalDB version that points to the respective set of LocalDB binaries. The instance name of a LocalDB is **sysname** data type and can have up to 128 characters. (This differs from regular named instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], which limits names to regular NetBIOS names of 16 ASCII chars.) The name of an instance of LocalDB can contain any Unicode characters that are legal within a filename.A named instance that uses an automatic instance name becomes an automatic instance.

Different users of a computer can have instances with the same name. Each instance is a different processes running as a different user.

## Shared instances of LocalDB

To support scenarios where multiple users of the computer need to connect to a single instance of LocalDB, LocalDB supports instance sharing. An instance owner can choose to allow the other users on the computer to connect the instance. Both automatic and named instances of LocalDB can be shared. To share an instance of LocalDB, a user selects a shared name (alias) for it. Because the shared name is visible to all users of the computer, this shared name must be unique on the computer. The shared name for an instance of LocalDB has the same format as the named instance of LocalDB.

Only an administrator on the computer can create a shared instance of LocalDB. A shared instance of LocalDB can be unshared by an administrator or by the owner of the shared instance of LocalDB. To share and unshared an instance of LocalDB, use the `LocalDBShareInstance` and `LocalDBUnShareInstance` methods of the LocalDB API, or the share and unshared options of the `SqlLocalDb` utility.

## Starting LocalDB and Connecting to LocalDB

### Connect to the automatic instance

The easiest way to use LocalDB is to connect to the automatic instance owned by the current user by using the connection string `Server=(localdb)\MSSQLLocalDB;Integrated Security=true`. To connect to a specific database by using the file name, connect using a connection string similar to `Server=(LocalDB)\MSSQLLocalDB; Integrated Security=true ;AttachDbFileName=D:\Data\MyDB1.mdf`.

>[!NOTE]
>The first time a user on a computer tries to connect to LocalDB, the automatic instance must be both created and started. The extra time for the instance to be created can cause the connection attempt to fail with a timeout message. When this happens, wait a few seconds to let the creation process complete, and then connect again.

### Creating and Connecting to a Named Instances

In addition to the automatic instance, LocalDB also supports named instances. Use the SqlLocalDB.exe program to create, start, and stop a named instance of LocalDB. For more information about SqlLocalDB.exe, see [SqlLocalDB Utility](../../tools/sqllocaldb-utility.md).

```console
REM Create an instance of LocalDB
"C:\Program Files\Microsoft SQL Server\130\Tools\Binn\SqlLocalDB.exe" create LocalDBApp1
REM Start the instance of LocalDB
"C:\Program Files\Microsoft SQL Server\130\Tools\Binn\SqlLocalDB.exe" start LocalDBApp1
REM Gather information about the instance of LocalDB
"C:\Program Files\Microsoft SQL Server\130\Tools\Binn\SqlLocalDB.exe" info LocalDBApp1
```

 The last line above, returns information similar to the following.

|||
|-|-|
|Name|`LocalDBApp1`|
|Version|\<Current Version>|
|Shared name|""|
|Owner|"\<Your Windows User>"|
|Auto create|No|
|State|running|
|Last start time|\<Date and Time>|
|Instance pipe name|np:\\\\.\pipe\LOCALDB#F365A78E\tsql\query|

>[!NOTE]
>If your application uses a version of .NET before 4.0.2 you must connect directly to the named pipe of the LocalDB. The Instance pipe name value is the named pipe that the instance of LocalDB is listening on. The portion of the Instance pipe name after LOCALDB# will change each time the instance of LocalDB is started. To connect to the instance of LocalDB by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], type the instance pipe name in the **Server name** box of the **Connect to [!INCLUDE[ssDE](../../includes/ssde-md.md)]** dialog box. From your custom program you can establish connection to the instance of LocalDB using a connection string similar to `SqlConnection conn = new SqlConnection(@"Server=np:\\.\pipe\LOCALDB#F365A78E\tsql\query");`

### Connecting to a shared instance of LocalDB

To connect to a shared instance of LocalDB add `.\` (dot + backslash) to the connection string to reference the namespace reserved for shared instances. For example, to connect to a shared instance of LocalDB named `AppData` use a connection string such as `(localdb).AppData` as part of the connection string. A user connecting to a shared instance of LocalDB that they do not own must have a Windows Authentication or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication login.

## Troubleshooting

For information about troubleshooting LocalDB, see [Troubleshooting SQL Server 2012 Express LocalDB](https://social.technet.microsoft.com/wiki/contents/articles/4609.aspx).

## Permissions

An instance of [!INCLUDE[ssExpCurrent](../../includes/ssexpcurrent-md.md)]LocalDB is an instance created by a user for their use. Any user on the computer can create a database using an instance of LocalDB, store files under their user profile, and run the process under their credentials. By default, access to the instance of LocalDB is limited to its owner. The data contained in the LocalDB is protected by file system access to the database files. If user database files are stored in a shared location, the database can be opened by anyone with file system access to that location by using an instance of LocalDB that they own. If the database files are in a protected location, such as the users data folder, only that user, and any administrators with access to that folder, can open the database. The LocalDB files can only be opened by one instance of LocalDB at a time.

>[!NOTE]
>LocalDB always runs under the users security context; that is, LocalDB never runs with credentials from the local Administrator's group. This means that all database files used by a LocalDB instance must be accessible using the owning user's Windows account, without considering membership in the local Administrators group.

## See Also

[SqlLocalDB Utility](../../tools/sqllocaldb-utility.md)
