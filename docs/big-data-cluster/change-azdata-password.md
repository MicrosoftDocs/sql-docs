---
title: Update AZDATA_PASSWORD
titleSuffix: SQL Server Big Data Clusters
description: Learn how to manually update AZDATA_PASSWORD. It provides basic authentication to the cluster controller and master instance.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 07/12/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: how-to
ms.custom: kr2b-contr-experiment
---

# Manually update AZDATA_PASSWORD

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

Whether or not your [!INCLUDE[ssbigdataclusters-ss-nover](../includes/ssbigdataclusters-ss-nover.md)] operates with Active Directory integration, `AZDATA_PASSWORD` is set during deployment. It provides a basic authentication to the cluster controller and master instance. This document describes how to manually update `AZDATA_PASSWORD`.

## Change AZDATA_PASSWORD for controller

If the cluster operates in non-Active Directory mode, update the Apache Knox Gateway password by doing the following steps:

1. Obtain the controller [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] credentials by running the following commands:

   Run this command as a Kubernetes administrator:

   ```bash
   kubectl get secret controller-sa-secret -n <cluster name> -o yaml | grep password
   ```

   Base64 decode the secret:

   ```bash
   echo <password from kubectl command>  | base64 --decode && echo
   ```

1. In a separate window, expose the controller database server port:

   ```bash
   kubectl port-forward controldb-0 1433:1433 --address 0.0.0.0 -n <cluster name>
   ```

1. Use the system administrator password, which you just obtained, to connect to the controller database server from a SQL client tool.

1. Generate a new complex password for `AZDATA_USERNAME` to replace the existing `AZDATA_PASSWORD`.

   > [!NOTE]
   > To simplify the example, the next steps use "newPassword" because the generated password is "newPassword".

1. Get `hexsalt` from the `auth.users` table:

   ```sql
   SELECT hexsalt FROM [auth].[users] WHERE username = '<username>'
   ```

   `hexsalt` returns a random hex string, for example, `64FC59DF31244FFEE02F457BC0750226`.

1. Encrypt the new complex password by using `hexsalt`:

   For your convenience, we provide a pre-built tool `pbkdf2` to encrypt the password. Download the platform-appropriate .NET Core app for [`pbkdf2`](https://github.com/microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster/security/password-hashing/pbkdf2/prebuilt-binaries).

   The app is self-contained and requires no prerequisites, such as .NET runtimes. To encrypt the password run this command:

   ```bash
   pbkdf2 <password> <hexsalt>
   J2y4E4dhlgwHOaRr3HKiiVAKBfjuGDyYmzn88VXmrzM=
   ```

1. Update the password in the `auth.users` table:

   ```sql
   UPDATE [auth].[users] SET password = 'J2y4E4dhlgwHOaRr3HKiiVAKBfjuGDyYmzn88VXmrzM=' WHERE username = '<username>'
   ```

## Change `AZDATA_PASSWORD` in the SQL Server master instance

1. Connect to the master SQL endpoint with any administrator user.

1. To change the password for the login credentials that you defined during deployment in the parameter `AZDATA_USERNAME`, run the following TSQL command:

   ```sql
   ALTER LOGIN <AZDATA_USERNAME> WITH PASSWORD = 'newPassword'
   ```

## Manually update password for Grafana and Kibana

After following the steps to update AZDATA_PASSWORD, [Grafana](app-monitor.md) and [Kibana](cluster-logging-kibana.md) still accept the old password. Grafana and Kibana don't have visibility to the new Kubernetes secret. You must update the password for Grafana and Kibana separately.

### Update Grafana password

Follow these options for manually updating the password for [Grafana](app-monitor.md).

1. The `htpasswd` utility is required. You can install this utility on any client machine.
  
    ### [For Ubuntu](#tab/for-ubuntu)
    On Ubuntu Linux, use the following command:

    ```bash
    sudo apt install apache2-utils
    ```

    ### [For RHEL](#tab/for-rhel)
    On Red Hat Enterprise Linux, use the following command:

    ```bash
    sudo yum install httpd-tools
    ```
    ---

2. Generate the new password.

    ```bash
    htpasswd -nbs <username> <password>
    admin:{SHA}<secret>
    ```

    Replace values for \<username\>, \<password\>, \<secret\> as appropriate, for example:

    ```bash
    htpasswd -nbs admin Test@12345
    admin:{SHA}W/5VKRjIzjusUJ0ih0gHyEPjC/s=
    ```

3. Encode the password:

    ```bash
    echo "admin:{SHA}W/5VKRjIzjusUJ0ih0gHyEPjC/s=" | base64
    ```

    Retain the output base64 string for later.

4. Next, edit the `mgmtproxy-secret`:

    ```bash
    kubectl edit secret -n mssql-cluster mgmtproxy-secret
    ```

5. Update the `controller-login-htpasswd` with the new encoded password base64 string generated above:

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

6. Identify and delete the `mgmtproxy` pod.

    If necessary, identify the name of your `mgmtproxy` prod.

    ### [For Windows](#tab/for-windows)
     On a Windows Server, use the following script:

    ```bash
    kubectl get pods -n <namespace> -l app=mgmtproxy
    ```

    ### [For Linux](#tab/for-linux)
     On a Linux computer, use the following script:

    ```bash
    kubectl get pods -n <namespace> | grep 'mgmtproxy'
    ```
    ---

     Remove the `mgmtproxy` pod:

    ```bash
    kubectl delete pod mgmtproxy-xxxxx -n mssql-clutser
    ```

7. Wait for the `mgmtproxy` pod to come online and Grafana Dashboard to start.  

    The wait isn't significant and the pod should be online within seconds. To check the status of the pod you can use the same `get pods` command as used in the previous step.

    If you see the `mgmtproxy` pod doesn't promptly return to *Ready* status, use `kubectl` to describe the pod:

    ```bash
    kubectl describe pods mgmtproxy-xxxxx  -n <namespace>
    ```

    For troubleshooting and further log collection, use the Azure Data CLI [azdata bdc debug copy-logs](../azdata/reference/reference-azdata-bdc-debug.md) command.

8. Sign in to Grafana using new password.

### Update the Kibana password

Follow these options for manually updating the password for [Kibana](cluster-logging-kibana.md).

> [!IMPORTANT]
> The Internet Explorer browser and older Microsoft Edge browsers are not compatible with Kibana. An unsupported browser displays a blank page when loading the dashboards. Consider the [Chromium-based Microsoft Edge](https://microsoftedgewelcome.microsoft.com/), or review [supported browsers for Kibana](https://www.elastic.co/support/matrix#matrix_browsers).

1. Open the Kibana URL.

    You can find the Kibana service endpoint URL from within [Azure Data Studio](manage-with-controller-dashboard.md#controller-dashboard), or use the following `azdata` command:

    ```azurecli
    azdata login
    azdata bdc endpoint list -e logsui -o table
    ```

    For example: `https://11.111.111.111:30777/kibana/app/kibana#/discover`.

2. On the left pane, select the **Security** option.

    ![A screenshot of the menu on the left pane of Kibana, with the Security option chosen.](media\big-data-cluster-change-kibana-password\big-data-cluster-change-kibana-password-1.jpg)

3. On the **Security** page, under the heading **Authentication Backends**, select **Internal User Database**.

    ![A screenshot of the security page, with the Internal User Database box chosen.](media\big-data-cluster-change-kibana-password\big-data-cluster-change-kibana-password-2.jpg)

4. You see the list of users under the heading **Internal Users Database**. Use this page to add, modify, and remove any users for Kibana endpoint access. For the user that needs the updated password, select the **Edit** button on the row for the user.

    ![A screenshot of the Internal User Database page. In the list of users, for the KubeAdmin user, the Edit button is chosen.](media\big-data-cluster-change-kibana-password\big-data-cluster-change-kibana-password-3.jpg)

5. Enter the new password twice and select **Submit**:

    ![A screenshot of the Internal User edit form. A new password has been entered in the Password and Repeat password fields.](media\big-data-cluster-change-kibana-password\big-data-cluster-change-kibana-password-4.jpg)

6. Close the browser and reconnect to the Kibana URL using updated password.

> [!Note]
> After logging in with new password, if you see blank pages in Kibana, manually sign out using the link at top right corner and sign in again.

## Next steps

* [azdata bdc (Azure Data CLI)](../azdata/reference/reference-azdata-bdc.md)  
* [Monitor applications with azdata and Grafana Dashboard](app-monitor.md)
* [Check out cluster logs with Kibana Dashboard](cluster-logging-kibana.md)
