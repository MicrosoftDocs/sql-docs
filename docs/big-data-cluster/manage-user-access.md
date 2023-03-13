---
title: Manage big data cluster access in Active Directory mode
description: Manage access to the big data cluster
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 07/09/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# Manage big data cluster access in Active Directory mode

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

This article describes how to add new Active Directory groups with *bdcUser* roles in addition to the ones provided during deployment through the *clusterUsers* configuration setting.

>[!IMPORTANT]
>Do not use this procedure to add new Active Directory groups with *bdcAdmin* role. Hadoop components, such as HDFS and Spark, allow only one Active Directory group as the superuser group - the equivalent of the *bdcAdmin* role in BDC. In order to grant additional Active Directory groups with *bdcAdmin* permissions to the big data cluster after deployment, you must add additional users and groups to the already-nominated groups during deployment. You can follow same procedure to update the group membership that have *bdcUsers* role.

## Two overarching roles in the big data cluster

Active Directory groups can be provided in the security section of the deployment profile as part of two overarching roles for authorization within the big data cluster:

* `clusterAdmins`: This parameter takes one Active Directory group. Members of this group have the *bdcAdmin* role, meaning they get administrator permissions for the entire cluster. They have *sysadmin* permissions in SQL Server, *superuser* permissions in Hadoop Distributed File System (HDFS) and Spark, and *administrator* rights in the controller.

* `clusterUsers`: These Active Directory groups are mapped to *bdcUsers* role in BDC. They are regular users, without administrator permissions in the cluster. They have permissions to log in to the SQL Server master instance but, by default, they have no permissions to objects or data. They are regular users for HDFS and Spark, without *superuser* permissions. When connecting to the controller endpoint, these users can only query the endpoints (using *azdata bdc endpoints list*).

To grant additional Active Directory groups *bdcUser* permissions without altering group memberships inside Active Directory, complete the procedures in the next sections.

## Grant *bdcUser* permissions to additional Active Directory groups

### Create a login for the Active Directory user or group in the SQL Server master instance

1. Connect to the master SQL endpoint by using your favorite SQL client. Use any administrator login (for example, `AZDATA_USERNAME`, which was provided during deployment). Alternatively, it could be any Active Directory account that belongs to the Active Directory group that's provided as `clusterAdmins` in the security configuration.

1. To create a login for the Active Directory user or group, run the following TSQL command:

   ```sql
   CREATE LOGIN [<domain>\<principal>] FROM WINDOWS;
   ```

   Grant the desired permissions in the SQL Server instance:

   ```sql
   ALTER SERVER ROLE <server role> ADD MEMBER [<domain>\<principal>];
   GO
   ```

For a complete list of server roles, see the corresponding SQL Server security topic [here](../relational-databases/security/authentication-access/server-level-roles.md).

### Add the Active Directory user or group to the roles table in the controller database

1. Obtain the controller SQL server credentials by running the following commands:

   a. Run this command as a Kubernetes administrator:

   ```bash
   kubectl get secret controller-sa-secret -n <cluster name> -o yaml | grep password
   ```

   b. Base64 decode the secret:

   ```bash
   echo <password from kubectl command>  | base64 --decode && echo
   ```

1. In a separate command window, expose the controller database server port:

   ```bash
   kubectl port-forward controldb-0 1433:1433 --address 0.0.0.0 -n <cluster name>
   ```

1. Use the preceding connection to insert a new row in the *roles* and *active_directory_principals* tables. Type the *REALM* value in uppercase letters.

   ```sql
   USE controller;
   GO

   INSERT INTO [controller].[auth].[roles] VALUES (N'<user or group name>@<REALM>', 'bdcUser')
   GO

   INSERT INTO [controller].[auth].[active_directory_principals] VALUES (N'<user or group name>@<REALM>', N'<SID>')
   GO
   ```

   To find the SID of the user or the group being added, you can use [Get-ADUser](/powershell/module/activedirectory/get-aduser/) or [Get-ADGroup](/powershell/module/activedirectory/get-adgroup/) PowerShell commands.

2. Verify that the members of the group that you added have the expected *bdcUser* permissions by logging in to the controller endpoint or authentication to the SQL Server master instance. For example:

   ```bash
   azdata login
   azdata bdc endpoints list
   ```

## Next steps

- [Security concepts for SQL Server 2019 Big Data Clusters](concept-security.md)
