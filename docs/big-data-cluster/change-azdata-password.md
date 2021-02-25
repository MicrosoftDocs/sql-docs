---
title: Update AZDATA_PASSWORD
description: Update the `AZDATA_PASSWORD` manually
author: NelGson
ms.author: negust
ms.reviewer: mikeray
ms.date: 02/24/2021
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

## Manually updating password for Grafana and Kibana

After following the steps to update AZDATA_PASSWORD, you will see that Grafana and Kibana still accept the old password. This is because Grafana and Kibana does not have visibility to the new Kubernetes secret. You must manually update the password for Grafana and Kibana separately.

## Update Grafana password

Follow these options for manually updating the password for [Grafana](app-monitor.md).

1. The htpasswd utility is required. You can install this on any client machine.

#### [For Ubuntu](#tab/ubuntu): 
```console
sudo apt install apache2-utils
```

#### [For RHEL](#tab/rhel): 
```console
sudo yum install httpd-tools
```

---

2. Generate the new password. 

```console
htpasswd -nbs <username> <password>
admin:{SHA}<secret>
```

Replace values for /<username/>, /<password/>, /<secret/> as appropriate, for example:

```console
htpasswd -nbs admin Test@12345
admin:{SHA}W/5VKRjIzjusUJ0ih0gHyEPjC/s=
```

3. Now encode the password:

```console
echo "admin:{SHA}W/5VKRjIzjusUJ0ih0gHyEPjC/s=" | base64
```             

Retain the output base64 string for later.

4. Next, edit the mgmtproxy-secret:

```console
kubectl edit secret -n mssql-cluster mgmtproxy-secret
```
         
5. Update the controller-login-htpasswd with the new encoded password base64 string generated above:

```console
# Please edit the object below. Lines beginning with a '#' will be ignored,
# and an empty file will abort the edit. If an error occurs while saving this file will be
# reopened with the relevant failures.
#
apiVersion: v1
data:
   controller-login-htpasswd: <base64 string from before>
   mssql-internal-controller-password: <password>
   mssql-internal-controller-username: <username>
```         

6. Delete the mgmtproxy pod:

```console
kubectl delete pod mgmtproxy-xxxxx -n mssql-clutser
```            

9. Wait for the mgmtproxy pod to come online and Grafana Dashboard to start.  

10. Now login to Grafana using new password. 


## Update the Kibana password

Follow these options for manually updating the password for [Kibana](cluster-logging-kibana.md).

1. Open the Kibana URL, for example: https://11.111.111.111:30777/kibana/app/kibana#/discover

2. On the left side pane click on the **Security** option.
    
![A screenshot of the menu on the left pane of Kibana, with the Security option chosen](\media\big-data-cluster-change-kibana-password\big-data-cluster-change-kibana-password-1.jpg)

3. On the security page, under the heading Authentication Backends, click on **Internal User Database**.

![A screenshot of the security page, with the Internal User Database box chosen.](\media\big-data-cluster-change-kibana-password\big-data-cluster-change-kibana-password-2.jpg)

4. Now you will see the list of users under the heading Internal Users Database. Use this page to add, modify and remove any users for Kibana endpoint access. For the user that need the updated password, click on **Edit** button on the right hand side for the user.

![A screenshot of the Internal User Database page. In the list of users, for the KubeAdmin user, the Edit button is chosen.](\media\big-data-cluster-change-kibana-password\big-data-cluster-change-kibana-password-3.jpg)

5. Enter the new password twice and click on **Submit**:

![A screenshot of the Internal User edit form. A new password has been entered in the Password and Repeat password fields.](\media\big-data-cluster-change-kibana-password\big-data-cluster-change-kibana-password-4.jpg)

6. Close the browser and reconnect to the Kibana URL using updated password.

> [!Note]
> After logging in with new password, if you see blank pages in Kibana, manually logout using the logout option at top right corner and login again.

## See Also

* [azdata bdc (Azure Data CLI)](../../sql/azdata/reference/reference-azdata-bdc.md) 
