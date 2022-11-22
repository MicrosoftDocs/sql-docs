---
title: HDFS tiering permissions for SQL Server Big Data Clusters
titleSuffix: Manage HDFS tiering permissions for SQL Server Big Data Clusters
description: Manage security for HDFS tiering on SQL Server Big Data Clusters like permissions on other Linux-based systems.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 11/04/2019
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# Manage HDFS permissions for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

HDFS as a file system is similar to the Linux based file systems that use POSIX for file permissions. In addition to the traditional POSIX permissions model, HDFS also supports POSIX access control lists (ACL). For more information, see the [Apache Hadoop article about ACLs](https://hadoop.apache.org/docs/current/hadoop-project-dist/hadoop-hdfs/HdfsPermissionsGuide.html#ACLs_.28Access_Control_Lists.29).

The following sections provide examples of how to use the [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] to for managing HDFS file and directory permissions.

## Prerequisites

- [Deployed big data cluster](deployment-guidance.md)
- [Big data tools](deploy-big-data-tools.md)
  
## HDFS shell

The `hdfs` shell capability in [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] allows you to issue commands directly in a shell to manage HDFS permissions on files and directories. The underlying mechanism uses WebHdfs calls to issue the commands

The following command will open the shell.

```bash
azdata bdc hdfs shell
```

To access the help for `hdfs` shell and understand how to issue commands, run the following command once the shell is active.

```bash
[hdfs] ?
```

The following example shows how to create a directory, list directories and modify permissions on a directory and give a named user `bob` read, write, and execute access to directory `sales`.

```bash
[hdfs] mkdir sales
[hdfs] ls
rwxr-xr-x  hdfs bdcadmins        0 Oct 09 18:02 system/
rwxrwxr-x admin bdcadmins        0 Oct 10 16:47 sales/
--xrwxrwxrwx  hdfs bdcadmins        0 Oct 09 18:03 tmp/
rwxrwxrwx  hdfs bdcadmins        0 Oct 09 17:59 user/

[hdfs] acl modify  '/sales/' 'user:bob:rwx'
acl modify: Change completed.
[hdfs] acl status  '/sales/'
{
  `AclStatus`: {
    `entries`: [
      `user:bob:rwx`,
      `group::r-x`
    ],
    `group`: `bdcadmins`,
    `owner`: `admin`,
    `permission`: `775`,
    `stickyBit`: false
  }
}
```

## Create a directory in HDFS using [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)]

Create a directory called `data` in path `/sales`.

```bash
azdata bdc hdfs mkdir --path '/sales/data'
```

## Change owner of a directory or file

Change the owning user of directory `data` in HDFS and make *`alice`* the owning user and *`salesgroup`* the owning group. In order to change owner, you have to be an owner.

```bash
azdata bdc hdfs chown --owner alice --group 'salesgroup' --path '/sales/data'
```

## Change permissions of a file or directory with `chmod`

Use `chmod` to change permissions on files and directories (for owner, owning group, and others). For more information, see [changing permissions on a Linux file system](https://www.lifewire.com/uses-of-command-chmod-2201064). In hdfs, the pattern is the same. For example:

```bash
azdata bdc hdfs chmod --permission 750 --path /sales/data
```

```bash
azdata bdc hdfs chmod --permission 775 --path /sales/data/file.txt
```

## Set sticky bit on directories

Set the sticky bit can on directories to prevent unintentional file deletion or relocation. The sticky bit limits the permission to delete or move a file to the superuser, directory owner, or file owner. This setting does not affect the file. The below example sets a sticky bit on directory `users` by prefixing the permissions with a `1`.

```bash
azdata bdc hdfs chmod --path /sales/users --permission 1750
```

## Setting ACLs on files and directories

To set ACLs on files and directories in HDFS, use the [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] commands.

Setting ACLs on a directory and giving named user *`tom`* read, write and execute access to directory *`data`*. 

> [!NOTE]
> When using the `set` command, make sure you are providing the full ACL spec including ACL spec for owning user, owning group and others.

```bash
azdata bdc hdfs acl set --path '/sales' --aclspec  'user::rw-,user:tom:rwx,group::rw-,other::rw-'
```

## Default ACL on directories

Default ACL enables sub-directories to inherit permissions from the parent directory. Only directories can have default ACL. When a new file or sub-directory is created, it automatically inherits the default ACL of its parent into its own access ACL. In this way, the default ACL will be inherited down through arbitrarily deep directory levels as new sub-directories are created.

Below is an example of how to set default ACL using azdata.

```bash
azdata bdc hdfs acl set --path '/sale' --aclspec  'user::rw-,user:tom:rwx,group::rw-,other::rw-,default:group::rw-,default:user::rw-,default:other::rw-'
```

## Next steps

- [[!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] reference](../azdata/reference/reference-azdata.md)

- [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md)