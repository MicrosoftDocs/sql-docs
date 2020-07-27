---
title: Manage big data cluster access in Active Directory mode
description: Manage access to the big data cluster
author: NelGson
ms.author: negust
ms.reviewer: mikeray
ms.date: 12/06/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Manage big data cluster access in Active Directory mode

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

This article describes how to update the Active Directory groups that are provided during deployment for clusterAdmins and clusterUsers.

## Two overarching roles in the big data cluster

Active Directory groups can be provided in the security section of the deployment profile as part of two overarching roles for authorization within the big data cluster:

* `clusterAdmins`: This parameter takes one Active Directory group. Members of this group get administrator permissions for the entire cluster. They have *sysadmin* permissions in SQL Server, *superuser* permissions in Hadoop Distributed File System (HDFS) and Spark, and *administrator* rights in the controller.

* `clusterUsers`: These Active Directory groups are regular users without administrator permissions in the cluster. They have permissions to log in to the SQL Server master instance but, by default, they have no permissions to objects or data.

One way to grant additional Active Directory groups permissions to the big data cluster after deployment is to add additional users and groups to the already-nominated groups during deployment. 

However, it might not always be feasible for the administrators to alter the group memberships inside Active Directory. To grant additional Active Directory groups permissions without altering group memberships inside Active Directory, complete the procedures in the next sections.

## Grant administrator permissions to additional Active Directory groups

>[!IMPORTANT]
>This procedure doesn't grant additional Active Directory groups administrator access to the Hadoop components, such as HDFS and Spark, in the big data cluster. These components allow only one Active Directory group as the superuser group. This restriction means that the group that's specified in `clusterAdmins` during deployment remains the superuser group even after this step.

By following the procedures in this section, you can grant administrator access to both the controller and the SQL Server master instance.

### Create a login for the Active Directory user or group in the SQL Server master instance 

1. Connect to the master SQL endpoint by using your favorite SQL client. Use any administrator login (for example, `AZDATA_USERNAME`, which was provided during deployment). Alternatively, it could be any Active Directory account that belongs to the Active Directory group that's provided as `clusterAdmins` in the security configuration.

1. To create a login for the Active Directory user or group, run the following TSQL command:

   ```sql
   CREATE LOGIN [<domain>\<principal>] FROM WINDOWS;
   ```

   If you're granting administrator permissions in the SQL Server instance, also grant the following permission:

   ```sql
   ALTER SERVER ROLE sysadmin ADD MEMBER [<domain>\<principal>];
   GO
   ```

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

1. Use the preceding connection to insert a row in the roles table. Type the *REALM* value in uppercase letters.

   If you're granting administrator permissions, use the *bdcAdmin* role in the *\<role name>*. For non-administrator users, use the *bdcUser* role.

   ```sql
   USE controller;
   GO

   INSERT INTO [controller].[auth].[roles] VALUES (N'<user or group name>@<REALM>', N'<role name>')
   GO
   ```

1. Verify that the members of the group that you added have big data cluster administrator permissions by logging in to the controller endpoint and running the following command:

   ```bash
   azdata bdc config show
   ```

1. For non-administrator users, you can verify access by authenticating to the SQL master instance or to the controller by using `azdata login`.

## Next steps

- [Security concepts for SQL Server 2019 Big Data Clusters](concept-security.md)
