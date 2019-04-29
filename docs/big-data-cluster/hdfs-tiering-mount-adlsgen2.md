---
title: Mount ADLS Gen2 for HDFS tiering
titleSuffix: How to mount ADLS Gen2
description: This article explains how to configure HDFS tiering to mount an external Azure Data Lake Storage file system into HDFS on a SQL Server 2019 big data cluster (preview).
author: nelgson
ms.author: negust
ms.reviewer: jroth
manager: craigg
ms.date: 04/18/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# How to mount ADLS Gen2 for HDFS tiering in a big data cluster

The following sections provide an example of how to configure HDFS tiering with an Azure Data Lake Storage Gen2 data source.

## Prerequisites

- [Deployed big data cluster](deployment-guidance.md)
- [Big data tools](deploy-big-data-tools.md)
  - **mssqlctl**
  - **kubectl**

## <a id="load"></a> Load data into Azure Data Lake Storage

The following section describes how to set up Azure Data Lake Storage Gen2 for testing HDFS tiering. If you already have data stored in Azure Data Lake Storage, you can skip this section to use your own data.

1. [Create a storage account with Data Lake Storage Gen2 capabilities](https://docs.microsoft.com/azure/storage/blobs/data-lake-storage-quickstart-create-account).

1. [Create a blob container](https://docs.microsoft.com/azure/storage/blobs/storage-quickstart-blobs-portal) in this storage account for your external data.

1. Upload a CSV or Parquet file into the container. This is the external HDFS data that will be mounted to HDFS in the big data cluster.

## Credentials for mounting

## Use OAuth credentials to mount

In order to use OAuth credentials to mount, you need to follow the below steps:

1. Go to the [Azure portal](https://portal.azure.com)
1. Go to "services" in the left navigation pane, and clock on "Azure Active Directory"
1. Using “App Registrations” in the menu, create a “Web Application and follow the wizard. **Remember the name you create here**. You will need to add this name to your ADLS account as an authorized user.
1. Once the web application is created, go to “keys” under “settings” for the app.
1. Select a key duration and click on save. **Save the generated key.**
1. 	Go back to the App Registrations page, and click on the “Endpoints” button at the top. **Note down the “Token Endpoint” URL**
1. You should now have the following things noted down for OAuth:

    - The “Application ID” of the Web App you created above in step 3
    - The key you just generated in step 5
    - The token endpoint from step 6

### Adding the service principal to your ADLS Account

1. Go to the portal again, and open your ADLS account and select Access control (IAM) in the left menu.
1. Select "Add a role assignment" and search for the name you created in Step3 above (note that it does not show up in the list, but will be found if you search for the full name).
1. Now add “Storage Blob Data Contributor (Preview)” role.

Wait for 5-10 minutes before using the credentials for mounting

### Create credential file

Open a command-prompt on a client machine that can access your big data cluster.

Create a local file named **filename.creds** that contains your Azure Data Lake Storage Gen2 account credentials using the following format:

   ```text
	fs.azure.account.auth.type=OAuth
	fs.azure.account.oauth.provider.type=org.apache.hadoop.fs.azurebfs.oauth2.ClientCredsTokenProvider
	fs.azure.account.oauth2.client.endpoint=[token endpoint from step6 above]
	fs.azure.account.oauth2.client.id=[<Application ID> from step3 above]
	fs.azure.account.oauth2.client.secret=[<key> from step5 above]
   ```

## Use Access keys to mount

You can also mount using access keys which you can get for your ADLS account on the Azure portal.

 > [!TIP]
   > For more information on how to find the access key (`<storage-account-access-key>`) for your storage account, see [View and copy access keys](https://docs.microsoft.com/azure/storage/common/storage-account-manage?#view-and-copy-access-keys).

### Create credential file

1. Open a command-prompt on a client machine that can access your big data cluster.

1. Create a local file named **filename.creds** that contains your Azure Data Lake Storage Gen2 account credentials using the following format:

   ```text
   fs.azure.abfs.account.name=<your-storage-account-name>.dfs.core.windows.net
   fs.azure.account.key.<your-storage-account-name>.dfs.core.windows.net=<storage-account-access-key>
   ```

## <a id="mount"></a> Mount the remote HDFS storage

Now that you have prepared a credential file with either access keys or using OAuth, you can start mounting. The following steps mount the remote HDFS storage in Azure Data Lake to the local HDFS storage of your big data cluster.

1. Use **kubectl** to find the IP Address for the endpoint **mgmtproxy-svc-external** service in your big data cluster. Look for the **External-IP**.

   ```bash
   kubectl get svc mgmtproxy-svc-external -n <your-cluster-name>
   ```

1. Log in with **mssqlctl** using the external IP address of the management proxy endpoint with your cluster username and password:

   ```bash
   mssqlctl login -e https://<IP-of-mgmtproxy-svc-external>:30777/ -u <username> -p <password>
   ```

1. Mount the remote HDFS storage in Azure using **mssqlctl storage mount create**. Replace the placeholder values before running the following command:

   ```bash
   mssqlctl storage mount create --remote-uri abfs://<blob-container-name>@<storage-account-name>.dfs.core.windows.net/ --mount-path /mounts/<mount-name> --credential-file <path-to-adls-credentials>/file.creds
   ```

   > [!NOTE]
   > The mount create command is asynchronous. At this time, there is no message indicating whether the mount succeeded. See the [status](#status) section to check the status of your mounts.

If mounted successfully, you should be able to query the HDFS data and run Spark jobs against it. It will appear in the HDFS for your big data cluster in the location specified by `--mount-path`.

## <a id="status"></a> Get the status of mounts

To list the status of all mounts in your big data cluster, use the following command:

```bash
mssqlctl storage mount status
```

To list the status of a mount at a specific path in HDFS, use the following command:

```bash
mssqlctl storage mount status --mount-path <mount-path-in-hdfs>
```

## <a id="delete"></a> Delete the mount

To delete the mount, use the **mssqlctl storage mount delete** command, and specify the mount path in HDFS:

```bash
mssqlctl storage mount delete --mount-path <mount-path-in-hdfs>
```

## Next steps

For more information about SQL Server 2019 big data clusters, see [What are SQL Server 2019 big data clusters?](big-data-cluster-overview.md).
