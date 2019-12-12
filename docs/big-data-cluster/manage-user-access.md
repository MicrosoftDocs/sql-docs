---
title: Manage cluster access in Active Directory mode
description: Manage access to the big data cluster
author: NelGson
ms.author: negust
ms.reviewer: mikeray
ms.date: 12/06/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Manage cluster access in Active Directory mode

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This document describes how to update the Active Directory (AD) groups provided upon deployment for clusterAdmins and clusterUsers.

## Two overarching roles in the cluster

AD groups can be provided in the security section of the deployment profile as part of two overarching roles for authorization within the cluster:

* `clusterAdmins` - This parameter takes one AD group. Members of this group will get administrator permissions in the entire cluster. They have `sysadmin` permissions in SQL Server, `superuser` permissions in HDFS and Spark, and administrator rights in controller.

* `clusterUsers` - List of the AD groups that are regular users without administrator permissions in the big data cluster. These users have permissions to login to SQL Server Master Instance, but will have no permissions to objects or data by default.

To grant additional AD groups permissions to the cluster after the deployment, one option is to add any additional users and groups to the already nominated groups upon deployment. 

However, it might not always be feasible for the administrators to alter the group memberships inside AD. To grant additional AD groups permissions without altering group memberships inside AD, complete the following steps.s

## Grant additional AD groups administrator permissions

>[!IMPORTANT]
>This procedure does not grant additional AD groups administrator access to the hadoop components such as Spark and HDFS in the big data cluster. Those components only allow one single AD group as the superuser group, which means that the group specified in `clusterAdmins` upon deployment remain the superuser group even after this step.

The following steps allow granting administrator access to controller as well as SQL Server master instance.

### Create a login for the AD user or group in master SQL server. 

1. Connect to master SQL endpoint using your favorite SQL client. Use any admin login. For example, `AZDATA_USERNAME`, which was provided during the deployment. Alternatively, it could be any AD account that belongs to the AD group provided as `clusterAdmins` in the security configuration.

2. Run the following TSQL to create a login for the AD user/group.

   ```sql
   CREATE LOGIN [<domain>\<principal>] FROM WINDOWS;
   ```

   If you are granting admin privileges in SQL Server, then also grant the following permission:

   ```sql
   ALTER SERVER ROLE sysadmin ADD MEMBER [<domain>\<principal>];
   GO
   ```

### Add the AD user or group to the roles table in the controller database 

1. Obtain controller SQL server credentials:

   Run the following command as a Kubernetes administrator:

   ```bash
   kubectl get secret controller-sa-secret -n <cluster name> -o yaml | grep password
   ```

   Base64 decode the secret:

   ```bash
   echo <password from kubectl command>  | base64 --decode && echo
   ```

2. In a separate command window, expose controller database server's port:

   ```bash
   kubectl port-forward controldb-0 1433:1433 --address 0.0.0.0 -n <cluster name>
   ```

3. Use the above connection to insert a row in the roles table. Type the realm value in uppercase.

   If you are granting admin privileges, use `bdcAdmin` role in the `<role name>` below. For non-admin user, use the `bdcUser` role.

   ```sql
   USE controller;
   GO

   INSERT INTO [controller].[auth].[roles] VALUES (N'<user or group name>@<REALM>', N'<role name>')
   GO
   ```

4. Verify that the members of the group you added has cluster administrator permissions by logging in to controller endpoint and running:

   ```bash
   azdata bdc config show
   ```

5. For non-administrator users, you can verify access by authenticating to SQL Master Instance or to controller using `azdata login`.

## Next steps

- [Security concepts for SQL Server Big Data Clusters](concept-security.md)