---
description: "SQL Server Express LocalDB Reference - Instance APIs"
title: "SQL Server Express LocalDB Instance API Reference | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 

ms.topic: "reference"
ms.assetid: faec46da-0536-4de3-96f3-83e607c8a8b6
author: markingmyname
ms.author: maghan
---
# SQL Server Express LocalDB Reference - Instance APIs
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  In the traditional, service-based SQL Server world, individual SQL Server instances installed on a single computer are physically separated; that is, each instance must be installed and removed separately, has a separate set of binaries, and runs under a separate service process. The SQL Server instance name is used to specify which SQL Server instance the user wants to connect to.  
  
 The SQL Server Express LocalDB instance API uses a simplified, "light" instance model. Although individual LocalDB instances are separated on the disk and in the registry, they use the same set of shared LocalDB binaries. Moreover, LocalDB does not use services; LocalDB instances are launched on demand through LocalDB instance API calls. In LocalDB, the instance name is used to specify which of the LocalDB instances the user wants to work with.  
  
 A LocalDB instance is always owned by a single user and is visible and accessible only from this user's context, unless instance sharing is enabled.  
  
 Although technically LocalDB instances are not the same as traditional SQL Server instances, their intended use is similar. They are called *instances* to emphasize this similarity and to make them more intuitive to SQL Server users.  
  
 LocalDB supports two kinds of instances: automatic instances (AI) and named instances (NI). The identifier for a LocalDB instance is the instance name.  
  
## Automatic LocalDB Instances  
 Automatic LocalDB instances are "public"; they are created and managed automatically for the user and can be used by any application. One automatic LocalDB instance exists for every version of LocalDB that is installed on the user's computer.  
  
 Automatic LocalDB instances provide seamless instance management. The user does not need to create the instance. This enables users to easily install applications and to migrate to different computers. If the target computer has the specified version of LocalDB installed, the automatic LocalDB instance for that version is also available on that computer.  
  
### Automatic Instance Management  
 A user does not need to create an automatic LocalDB instance. The instance is lazily created the first time the instance is used, provided that the specified version of LocalDB is available on the user's computer. From the user's point of view, the automatic instance is always present if the LocalDB binaries are present.  
  
 Other instance management operations, such as Delete, Share, and Unshare, also work for automatic instances. In particular, deleting an automatic instance effectively resets the instance, which will be re-created on the next Start operation. Deleting an automatic instance may be required if the system databases become corrupted.  
  
### Automatic Instance Naming Rules  
 Automatic LocalDB instances have a special pattern for the instance name that belongs to a reserved namespace. This is necessary to prevent name conflicts with named LocalDB instances.  
  
 The automatic instance name is the LocalDB baseline release version number preceded by a single "v" character. This looks like "v" plus two numbers with a period between them; for example, v11.0 or V12.00.  
  
 Examples of illegal automatic instance names are:  
  
-   11.0 (missing the "v" character at the beginning)  
  
-   v11 (missing a period and the second number of the version)  
  
-   v11. (missing the second number of the version)  
  
-   v11.0.1.2 (version number has more than two parts)  
  
## Named LocalDB Instances  
 Named LocalDB instances are "private"; an instance is owned by a single application that is responsible for creating and managing the instance. Named LocalDB instances provide isolation and improve performance.  
  
### Named Instance Creation  
 The user must create named instances explicitly through the LocalDB management API, or implicitly through the app.config file for a managed application. A managed application may also use the API.  
  
 Each named instance has an associated LocalDB version; that is, it points to a specified set of LocalDB binaries. The version for the named instance is set during the instance creation process.  
  
### Named Instance Naming Rules  
 A LocalDB instance name can have up to a total of 128 characters (the limit is imposed by the **sysname** data type). This is a significant difference compared to traditional SQL Server instance names, which are limited to NetBIOS names of 16 ASCII characters. The reason for this difference is that LocalDB treats databases as files, and therefore implies file-based semantics, so it's intuitive for users to have more freedom in choosing instance names.  
  
 A LocalDB instance name can contain any Unicode characters that are legal within the filename component. Illegal characters in a filename component generally include the following characters: ASCII/Unicode characters 1 through 31, as well as quote ("), less than (\<), greater than (>), pipe (|), backspace (\b), tab (\t), colon (:), asterisk (*), question mark (?), backslash (\\), and forward slash (/). Note that the null character (\0) is allowed because it is used for string termination; everything after the first null character will be ignored.  
  
> [!NOTE]  
>  The list of illegal characters may depend on the operating system and may change in future releases.  
  
 Leading and trailing white spaces in instance names are ignored and will be trimmed.  
  
 To avoid naming conflicts, named LocalDB instances cannot have a name that follows the naming pattern for automatic instances, as described earlier in "Automatic Instance Naming Rules." An attempt to create a named instance with a name that follows the automatic instance naming pattern effectively creates a default instance.  
  
## SQL Server Express LocalDB Reference Topics  
 [SQL Server Express LocalDB Header and Version Information](../../relational-databases/express-localdb-instance-apis/sql-server-express-localdb-header-and-version-information.md)  
 Provides header file information and registry keys for finding the LocalDB instance API.  
  
 [Command-Line Management Tool: SqlLocalDB.exe](../../relational-databases/express-localdb-instance-apis/command-line-management-tool-sqllocaldb-exe.md)  
 Describes SqlLocalDB.exe, a tool for managing LocalDB instances from the command line.  
  
 [LocalDBCreateInstance Function](../../relational-databases/express-localdb-instance-apis/localdbcreateinstance-function.md)  
 Describes the function to create a new LocalDB instance.  
  
 [LocalDBDeleteInstance Function](../../relational-databases/express-localdb-instance-apis/localdbdeleteinstance-function.md)  
 Describes the function to remove a LocalDB instance.  
  
 [LocalDBFormatMessage Function](../../relational-databases/express-localdb-instance-apis/localdbformatmessage-function.md)  
 Describes the function to return the localized description for a LocalDB error.  
  
 [LocalDBGetInstanceInfo Function](../../relational-databases/express-localdb-instance-apis/localdbgetinstanceinfo-function.md)  
 Describes the function to get information for a LocalDB instance, such as whether it exists, version information, whether it is running, and so on.  
  
 [LocalDBGetInstances Function](../../relational-databases/express-localdb-instance-apis/localdbgetinstances-function.md)  
 Describes the function to return all the LocalDB instances with a specified version.  
  
 [LocalDBGetVersionInfo Function](../../relational-databases/express-localdb-instance-apis/localdbgetversioninfo-function.md)  
 Describes the function to return information for a specified LocalDB version.  
  
 [LocalDBGetVersions Function](../../relational-databases/express-localdb-instance-apis/localdbgetversions-function.md)  
 Describes the function to return all LocalDB versions available on a computer.  
  
 [LocalDBShareInstance Function](../../relational-databases/express-localdb-instance-apis/localdbshareinstance-function.md)  
 Describes the function to share a specified LocalDB instance.  
  
 [LocalDBStartInstance Function](../../relational-databases/express-localdb-instance-apis/localdbstartinstance-function.md)  
 Describes the function to start a specified LocalDB instance.  
  
 [LocalDBStartTracing Function](../../relational-databases/express-localdb-instance-apis/localdbstarttracing-function.md)  
 Describes the function to enable API tracing for a user.  
  
 [LocalDBStopInstance Function](../../relational-databases/express-localdb-instance-apis/localdbstopinstance-function.md)  
 Describes the function to stop a specified LocalDB instance from running.  
  
 [LocalDBStopTracing Function](../../relational-databases/express-localdb-instance-apis/localdbstoptracing-function.md)  
 Describes the function to disable API tracing for a user.  
  
 [LocalDBUnshareInstance Function](../../relational-databases/express-localdb-instance-apis/localdbunshareinstance-function.md)  
 Describes the function to stop sharing a specified LocalDB instance.  
  
  
