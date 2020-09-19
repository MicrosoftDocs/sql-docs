---
title: Update AZDATA_PASSWORD
description: Update the `AZDATA_PASSWORD` manually
author: NelGson
ms.author: negust
ms.reviewer: mikeray
ms.date: 12/19/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Manually update `AZDATA_PASSWORD`

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

Whether or not the cluster is operating with Active Directory integration, `AZDATA_PASSWORD` is set during deployment. It provides a basic authentication to the cluster controller and master instance. This document describes how to manually update `AZDATA_PASSWORD`.

## Change `AZDATA_PASSWORD` for controller

If the cluster is operating in non-Active Directory mode, update the Apache Knox Gateway password by doing the following:

1. Obtain the controller SQL Server credentials by running the following commands:

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
 
1. Use the system administrator password, which you just obtained, to connect to the controller database server from a SQL client tool.

1. Generate a new complex password for `AZDATA_USERNAME` to replace the existing `AZDATA_PASSWORD`.

   To simplify the example, the next steps use "newPassword" because the generated password is "newPassword". 

1. Get `hexsalt` from the users table:

   ```sql
   SELECT hexsalt FROM [auth].[users] WHERE username = '<username>'
   ```

   `hexsalt` returns a random hex string (for example, `64FC59DF31244FFEE02F457BC0750226`).

1. Encrypt the new complex password by using `hexsalt`:

   For your convenience, we provide a pre-built tool `pbkdf2` to encrypt the password. Download the platform-appropriate .NET Core app for [`pbkdf2`](https://github.com/microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster/security/password-hashing/pbkdf2/prebuilt-binaries).

   The app is self-contained and requires no prerequisites, such as .NET runtimes. To encrypt the password run:

   ```bash
   pbkdf2 <password> <hexsalt>
   J2y4E4dhlgwHOaRr3HKiiVAKBfjuGDyYmzn88VXmrzM=
   ```

1. Update the password in the users table:

   ```SQL
   UPDATE [auth].[users] SET password = 'J2y4E4dhlgwHOaRr3HKiiVAKBfjuGDyYmzn88VXmrzM=' WHERE username = '<username>'
   ```

## Change `AZDATA_PASSWORD` in the SQL Server master instance

1. Connect to the master SQL endpoint with any administrator user.

1. To change the password for the login credentials that you defined during deployment in the parameter `AZDATA_USERNAME`, run the following TSQL command:

   ```sql
   ALTER LOGIN <AZDATA_USERNAME> WITH PASSWORD = 'newPassword'
   ```
