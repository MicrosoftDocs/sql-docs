---
title: Mount ADLS Gen2 for HDFS tiering
titleSuffix: How to mount ADLS Gen2
description: This article provide an example of how to configure HDFS tiering with an Azure Data Lake Storage Gen2 data source.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 06/29/2020
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# How to mount ADLS Gen2 for HDFS tiering in a big data cluster

The following sections provide an example of how to configure HDFS tiering with an Azure Data Lake Storage Gen2 data source.

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

## Prerequisites

- [Deployed big data cluster](deployment-guidance.md)
- [Big data tools](deploy-big-data-tools.md)
  - **azdata**
  - **kubectl**

## <a id="load"></a> Load data into Azure Data Lake Storage

The following section describes how to set up Azure Data Lake Storage Gen2 for testing HDFS tiering. If you already have data stored in Azure Data Lake Storage, you can skip this section to use your own data.

1. [Create a storage account with Data Lake Storage Gen2 capabilities](/azure/storage/blobs/data-lake-storage-quickstart-create-account).

1. [Create a file system](/azure/storage/blobs/data-lake-storage-explorer) in this storage account for your data.

1. Upload a CSV or Parquet file into the container. This is the external HDFS data that will be mounted to HDFS in the big data cluster.

## Credentials for mounting

### Use OAuth credentials to mount

In order to use OAuth credentials to mount, you need to follow the below steps:

1. Go to the [Azure portal](https://portal.azure.com)
1. Navigate to "Azure Active Directory". You should see this service on the left navigation bar.
1. In right navigation bar, select "App registrations" and create a new registration
1. Create a "Web Application and follow the wizard. **Remember the name of the app you create here**. You will need to add this name to your ADLS account as an authorized user. Also note the Application client ID in the overview when you select the App.
1. Once the web application is created, go to "Certificates&secrets" and create a **New client secret** and select a key duration. **Add** the secret.
1. Go back to the App Registrations page, and click on the "Endpoints" at the top. **Note down the "OAuth token endpoint (v2)** URL
1. You should now have the following things noted down for OAuth:

    - The "Application Client ID" of the Web Application
    - The client secret
    - The token endpoint

### Adding the service principal to your ADLS Account

1. Go to the portal again, and navigate to your ADLS storage account file system and select Access control (IAM) in the left menu.
1. Select "Add a role assignment" 
1. Select role "Storage Blob Data Contributor "
1. Search for the name you created above (note that it does not show up in the list, but will be found if you search for the full name).
1. Save the role.

Wait for 5-10 minutes before using the credentials for mounting

### Set environment variable for OAuth credentials

Open a command-prompt on a client machine that can access your big data cluster. Set an environment variable using the following format.The credentials need to be in a comma separated list. The 'set' command is used on Windows. If you are using Linux, then use 'export' instead.

**Note** that you need to remove any line breaks or space between the commas "," when you provide the credentials. The below formatting is just to make it easier to read.

```console
   set MOUNT_CREDENTIALS=fs.azure.account.auth.type=OAuth,
   fs.azure.account.oauth.provider.type=org.apache.hadoop.fs.azurebfs.oauth2.ClientCredsTokenProvider,
   fs.azure.account.oauth2.client.endpoint=[token endpoint],
   fs.azure.account.oauth2.client.id=[Application client ID],
   fs.azure.account.oauth2.client.secret=[client secret]
```

## Use Access keys to mount

You can also mount using access keys which you can get for your ADLS account on the Azure portal.

 > [!TIP]
   > For more information on how to find the access key (`<storage-account-access-key>`) for your storage account, see [View account keys and connection string](/azure/storage/common/storage-account-keys-manage#view-access-keys-and-connection-string).

### Set environment variable for access key credentials

1. Open a command-prompt on a client machine that can access your big data cluster.

1. Open a command-prompt on a client machine that can access your big data cluster. Set an environment variable using the following format. The credentials need to be in a comma separated list. The 'set' command is used on Windows. If you are using Linux, then use 'export' instead.

**Note** that you need to remove any line breaks or space between the commas "," when you provide the credentials. The below formatting is just to make it easier to read.

```console
set MOUNT_CREDENTIALS=fs.azure.abfs.account.name=<your-storage-account-name>.dfs.core.windows.net,
fs.azure.account.key.<your-storage-account-name>.dfs.core.windows.net=<storage-account-access-key>
```

## <a id="mount"></a> Mount the remote HDFS storage

Now that you have set the MOUNT_CREDENTIALS environment variable for access keys or using OAuth, you can start mounting. The following steps mount the remote HDFS storage in Azure Data Lake to the local HDFS storage of your big data cluster.

1. Use **kubectl** to find the IP Address for the endpoint **controller-svc-external** service in your big data cluster. Look for the **External-IP**.

   ```bash
   kubectl get svc controller-svc-external -n <your-big-data-cluster-name>
   ```

1. Log in with **azdata** using the external IP address of the controller endpoint with your cluster username and password:

   ```bash
   azdata login -e https://<IP-of-controller-svc-external>:30080
   ```
1. Set environment variable MOUNT_CREDENTIALS (scroll up for instructions)

1. Mount the remote HDFS storage in Azure using **azdata bdc hdfs mount create**. Replace the placeholder values before running the following command:

   ```bash
   azdata bdc hdfs mount create --remote-uri abfs://<blob-container-name>@<storage-account-name>.dfs.core.windows.net/ --mount-path /mounts/<mount-name>
   ```

   > [!NOTE]
   > The mount create command is asynchronous. At this time, there is no message indicating whether the mount succeeded. See the [status](#status) section to check the status of your mounts.

If mounted successfully, you should be able to query the HDFS data and run Spark jobs against it. It will appear in the HDFS for your big data cluster in the location specified by `--mount-path`.

## <a id="status"></a> Get the status of mounts

To list the status of all mounts in your big data cluster, use the following command:

```bash
azdata bdc hdfs mount status
```

To list the status of a mount at a specific path in HDFS, use the following command:

```bash
azdata bdc hdfs mount status --mount-path <mount-path-in-hdfs>
```

## Refresh a mount

The following example refreshes the mount. This refresh will also clear the mount cache.

```bash
azdata bdc hdfs mount refresh --mount-path <mount-path-in-hdfs>
```

## <a id="delete"></a> Delete the mount

To delete the mount, use the **azdata bdc hdfs mount delete** command, and specify the mount path in HDFS:

```bash
azdata bdc hdfs mount delete --mount-path <mount-path-in-hdfs>
```

## Next steps

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)], see [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md).
