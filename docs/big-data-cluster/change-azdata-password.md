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

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

No matter if the cluster is operating with Active Directory (AD) integration or not, the `AZDATA_PASSWORD` is set during deployment. It provides a basic authentication to the cluster controller and master instance. This document describes how to manually update the `AZDATA_PASSWORD`.

## Change `AZDATA_PASSWORD` for controller

The following steps are also updating the Gateway (Knox) password in case the cluster is operating in non-AD mode.

1. Obtain controller SQL server credentials

   Run the following command as a Kubernetes administrator:

   ```bash
   kubectl get secret controller-sa-secret -n <cluster name> -o yaml | grep password
   ```

   Base64 decode the secret:
   
   ```bash
   echo <password from kubectl command>  | base64 --decode && echo
   ```

2. In a separate command window, expose controller database server's port

   ```bash
   kubectl port-forward controldb-0 1433:1433 --address 0.0.0.0 -n <cluster name>
   ```
 
3. Use the SA password obtained above to connect to controller database server from a SQL client tool

4. Generate a new complex password for `AZDATA_USERNAME` to replace existing `AZDATA_PASSWORD`

   To simplify the example, the next steps use "newPassword" as the generated password is "newPassword". 

5. Get the `hexsalt` from users table

   ```sql
   SELECT hexsalt FROM [auth].[users] WHERE username = '<username>'
   ```

`hexsalt` returns a random hex string, for example: `64FC59DF31244FFEE02F457BC0750226`.


6. Install the platform appropriate dotnetcore app

   Install the platform appropriate dotnetcore app for [`pbkdf2`](https://helsinki.redmond.corp.microsoft.com/dist/software/pbkdf2/).

   The app is self-contained and does not require any prerequisite such as dotnet runtimes.

7. Encrypt the new complex password using the `hexsalt`

   ```bash
   pbkdf2 <password> <hexsalt>
   J2y4E4dhlgwHOaRr3HKiiVAKBfjuGDyYmzn88VXmrzM=
   ```

8. Update the password in the users table

   ```bash
   UPDATE [auth].[users] SET password = 'J2y4E4dhlgwHOaRr3HKiiVAKBfjuGDyYmzn88VXmrzM=' WHERE username = '<username>'
   ```

## Change `AZDATA_PASSWORD` in SQL Server master instance

1. Connect to master SQL endpoint with any administrator user

2. Run the following TSQL:

   Change the password for the login you defined upon deployment in the parameter `AZDATA_USERNAME`.

   ```sql
   ALTER LOGIN <AZDATA_USERNAME> WITH PASSWORD = 'newPassword'
   ```