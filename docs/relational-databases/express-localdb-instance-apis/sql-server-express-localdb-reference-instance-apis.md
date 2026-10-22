---
title: "SQL Server Express LocalDB Instance API Reference"
description: "SQL Server Express LocalDB Reference - Instance APIs"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 07/14/2025
ms.service: sql
ms.topic: "reference"
---
# SQL Server Express LocalDB instance APIs

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

In the traditional, service-based SQL Server world, individual SQL Server instances installed on a single computer are physically separated. Each instance must be installed and removed separately, has a separate set of binaries, and runs under a separate service process. The SQL Server instance name is used to specify which SQL Server instance the user wants to connect to.

The SQL Server Express LocalDB instance API uses a simplified, light instance model. Although individual LocalDB instances are separated on the disk and in the registry, they use the same set of shared LocalDB binaries. Moreover, LocalDB doesn't use services. LocalDB instances are launched on demand through LocalDB instance API calls. In LocalDB, the instance name is used to specify which of the LocalDB instances the user wants to work with.

A LocalDB instance is always owned by a single user and is visible and accessible only from this user's context, unless instance sharing is enabled.

Although technically LocalDB instances aren't the same as traditional SQL Server instances, their intended use is similar. They are called *instances* to emphasize this similarity and to make them more intuitive to SQL Server users.

LocalDB supports two kinds of instances: automatic instances (AI) and named instances (NI). The identifier for a LocalDB instance is the instance name.

## Automatic LocalDB instances

Automatic LocalDB instances are *public*; they are created and managed automatically for the user and can be used by any application. One automatic LocalDB instance exists for every version of LocalDB that is installed on the user's computer. The name of the automatic LocalDB instance is `MSSQLLocalDB`.

Automatic LocalDB instances provide seamless instance management. The user doesn't need to create the instance. This enables users to easily install applications and to migrate to different computers. If the target computer has the specified version of LocalDB installed, the automatic LocalDB instance for that version is also available on that computer.

### Automatic instance management

A user doesn't need to create an automatic LocalDB instance. The instance is lazily created the first time the instance is used, as long as the specified version of LocalDB is available on the user's computer. From the user's point of view, the automatic instance is always present if the LocalDB binaries are present.

Other instance management operations, such as Delete, Share, and Unshare, also work for automatic instances. In particular, deleting an automatic instance effectively resets the instance, which is recreated on the next Start operation. Deleting an automatic instance might be required if the system databases become corrupted.

## Named LocalDB instances

Named LocalDB instances are *private*; an instance is owned by a single application that is responsible for creating and managing the instance. Named LocalDB instances provide isolation and improve performance.

### Named instance creation

The user must create named instances explicitly through the LocalDB management API, or implicitly through the `app.config` file for a managed application. A managed application might also use the API.

Each named instance has an associated LocalDB version; that is, it points to a specified set of LocalDB binaries. The version for the named instance is set during the instance creation process.

### Named instance naming rules

A LocalDB instance name can have up to 128 characters (the **sysname** data type imposes this limit). This limit is a significant difference compared to traditional SQL Server instance names, which are limited to NetBIOS names of 15 ASCII characters. The reason for this difference is that LocalDB treats databases as files, and therefore implies file-based semantics, so users have more freedom in choosing instance names.

A LocalDB instance name can contain any Unicode characters that are legal within the filename component. Illegal characters in a filename component generally include the following characters: ASCII/Unicode characters 1 through 31, and quote (`"`), less than (`<`), greater than (`>`), pipe (`|`), backspace (`\b`), tab (`\t`), colon (`:`), asterisk (`*`), question mark (`?`), backslash (`\`), and forward slash (`/`). The null character (`\0`) is allowed because it's used for string termination; everything after the first null character is ignored.

> [!NOTE]  
> The list of illegal characters might depend on the operating system and might change in future releases.

Leading and trailing white spaces in instance names are ignored and trimmed.

To avoid naming conflicts, a named LocalDB instance can't use the reserved automatic instance name `MSSQLLocalDB`. An attempt to create a named instance with that name effectively creates a default instance.

## Related tasks

| Article | Description |
| --- | --- |
| [SQL Server Express LocalDB header and version information](sql-server-express-localdb-header-and-version-information.md) | Provides header file information and registry keys for finding the LocalDB instance API. |
| [Command-Line Management Tool: SqlLocalDB.exe](command-line-management-tool-sqllocaldb-exe.md) | Describes SqlLocalDB.exe, a tool for managing LocalDB instances from the command line. |
| [LocalDBCreateInstance Function](localdbcreateinstance-function.md) | Describes the function to create a new LocalDB instance. |
| [LocalDBDeleteInstance Function](localdbdeleteinstance-function.md) | Describes the function to remove a LocalDB instance. |
| [LocalDBFormatMessage Function](localdbformatmessage-function.md) | Describes the function to return the localized description for a LocalDB error. |
| [LocalDBGetInstanceInfo Function](localdbgetinstanceinfo-function.md) | Describes the function to get information for a LocalDB instance, such as whether it exists, version information, whether it's running, and so on. |
| [LocalDBGetInstances Function](localdbgetinstances-function.md) | Describes the function to return all the LocalDB instances with a specified version. |
| [LocalDBGetVersionInfo Function](localdbgetversioninfo-function.md) | Describes the function to return information for a specified LocalDB version. |
| [LocalDBGetVersions Function](localdbgetversions-function.md) | Describes the function to return all LocalDB versions available on a computer. |
| [LocalDBShareInstance Function](localdbshareinstance-function.md) | Describes the function to share a specified LocalDB instance. |
| [LocalDBStartInstance Function](localdbstartinstance-function.md) | Describes the function to start a specified LocalDB instance. |
| [LocalDBStartTracing Function](localdbstarttracing-function.md) | Describes the function to enable API tracing for a user. |
| [LocalDBStopInstance Function](localdbstopinstance-function.md) | Describes the function to stop a specified LocalDB instance from running. |
| [LocalDBStopTracing Function](localdbstoptracing-function.md) | Describes the function to disable API tracing for a user. |
| [LocalDBUnshareInstance Function](localdbunshareinstance-function.md) | Describes the function to stop sharing a specified LocalDB instance. |
