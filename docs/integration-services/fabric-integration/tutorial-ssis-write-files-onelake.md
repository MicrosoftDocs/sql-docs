---
title: "Tutorial: Use SSIS Packages to Write Files to OneLake Through Azure Data Lake Storage Gen2"
description: Learn how to use SSIS packages with Azure Storage connection managers to write files to Azure Data Lake Storage Gen2 and access them in OneLake through shortcuts.
ms.reviewer: randolphwest, maghan
ms.date: 03/27/2026
ms.service: sql
ms.subservice: integration-services
ms.topic: tutorial
ms.custom:
  - intro-deployment
  - sfi-image-nochange
---

# Tutorial: Use SSIS packages to write files to OneLake through Azure Data Lake Storage Gen2

This tutorial shows you how to run an existing SSIS package that writes files to Azure Data Lake Storage (ADLS) Gen2, and then surface those files in OneLake by using a shortcut. By combining the Invoke SSIS Package activity in Data Factory for Microsoft Fabric with OneLake shortcuts, you can centralize all your data in OneLake - even data produced by legacy SSIS workloads.

## Use case

Many organizations have SSIS packages that extract and transform data, then write the results as flat files (CSV, Parquet, XML, and others) to Azure Data Lake Storage Gen2. These files are consumed by downstream analytics and reporting systems.

With Microsoft Fabric, you can bring those files into OneLake without changing your SSIS package logic:

- **Preserve existing SSIS investments** - Continue using battle-tested packages that write files to ADLS Gen2 through the Azure Storage connection manager. No package rewrite is required.
- **Centralize data in OneLake** - Create an ADLS Gen2 shortcut in a Fabric lakehouse so that files written by SSIS appear automatically in OneLake, ready for consumption by Spark, SQL, Power BI, and other Fabric workloads.
- **Orchestrate in Fabric** - Use the Invoke SSIS Package activity in a Fabric pipeline to schedule and monitor package execution alongside other Fabric-native activities.

## Prerequisites

Before you begin, make sure you have:

- A [Microsoft Fabric workspace](/fabric/get-started/create-workspaces) with a Fabric capacity or trial.
- A [lakehouse](/fabric/data-engineering/create-lakehouse) in the workspace.
- An Azure Data Lake Storage Gen2 storage account with [hierarchical namespace enabled](/azure/storage/blobs/create-data-lake-storage-account).
- An SSIS package (*.dtsx*) that uses an [Azure Storage connection manager](../connection-manager/azure-storage-connection-manager.md) to write files to ADLS Gen2.
- Credentials for the ADLS Gen2 account - for example, an account key, shared access signature (SAS), service principal, or organizational account - with at least the **Storage Blob Data Contributor** role.

## Overview

The end-to-end workflow has four steps:

| Step | What you do | Result |
| --- | --- | --- |
| 1 | Configure the SSIS package to write files to ADLS Gen2 | Package produces output files in your storage account |
| 2 | Create an ADLS Gen2 shortcut in a Fabric lakehouse | Files written to ADLS Gen2 appear in OneLake automatically |
| 3 | Upload the SSIS package to OneLake | Package is stored in OneLake and ready to be invoked |
| 4 | Run the package from a Fabric pipeline | Pipeline orchestrates execution and writes output through to OneLake |

## Step 1 - Configure the SSIS package to write files to ADLS Gen2

In this step you make sure your SSIS package uses an Azure Storage connection manager to write files to your ADLS Gen2 account.

1. Open your SSIS project in **Visual Studio** with the [SQL Server Integration Services Projects extension](../integration-services-developer-documentation.md).
1. Install the [Azure Feature Pack for Integration Services (SSIS)](../azure-feature-pack-for-integration-services-ssis.md). The Feature Pack provides the Azure Storage connection manager, Azure Blob Source, Azure Blob Destination, and other Azure-related tasks and components needed to connect to ADLS Gen2 from an SSIS package.
1. In the **Connection Managers** tray, add (or verify) an **Azure Storage** connection manager. Set the following properties:

   | Property | Value |
   | --- | --- |
   | **Service** | ADLS Gen2 |
   | **Authentication** | Choose one: *AccessKey*, *ServicePrincipal*, or *SharedAccessSignature* |
   | **Account name** | Your ADLS Gen2 storage account name |

   :::image type="content" source="media/storage-connection.png" alt-text="Screenshot of the Azure Storage connection manager configuration dialog.":::

1. Configure your data flow or file system task to use this connection manager and write output files to a container and folder path in the storage account - for example, `mycontainer\myfolder`.

   :::image type="content" source="media/storage-folder-path.png" alt-text="Screenshot of the data flow configuration with the container and folder path for the storage account." lightbox="media/storage-folder-path.png" :::

1. Test the connection and verify the package executes correctly on your local machine.

For full details on the Azure Storage connection manager, see [Azure Storage connection manager](../connection-manager/azure-storage-connection-manager.md).

> [!TIP]  
> If your package uses the **DontSaveSensitive** protection level, credentials aren't persisted in the package file. You supply them at runtime through the **Connection Managers** tab of the Invoke SSIS Package activity. Alternatively, you can set the package protection level to **EncryptSensitiveWithPassword**, which encrypts credentials inside the package. You then provide the package password in the Invoke SSIS Package activity at runtime instead of supplying individual connection manager credentials (Step 4).

## Step 2 - Create an ADLS Gen2 shortcut in a Fabric lakehouse

A shortcut makes the files written by your SSIS package visible in OneLake without copying data. Any Fabric workload - Spark, SQL analytics endpoint, Power BI - can read the files through the shortcut.

1. Open your **lakehouse** in the Fabric portal.
1. In the **Explorer** pane, right-click the **Files** folder (or a subfolder) and select **New shortcut**.
1. Under **External sources**, select **Azure Data Lake Storage Gen2**.
1. Enter the connection URL - the DFS endpoint for your storage account:

   ```text
   https://<STORAGE_ACCOUNT_NAME>.dfs.core.windows.net
   ```

1. Select an existing connection or create a new one. Choose an authentication kind that has at least the **Storage Blob Data Reader** role on the storage account.
1. Select **Next**, then browse to the container and folder where your SSIS package writes files (for example, `mycontainer`).
1. Select the target folder, then select **Next** → **Create**.

   :::image type="content" source="media/shortcut-storage-container.png" alt-text="Screenshot of the shortcut creation dialog showing the selected storage container." lightbox="media/shortcut-storage-container.png" :::

The shortcut now appears in your lakehouse. Any file that the SSIS package writes to the ADLS Gen2 target folder is automatically accessible in OneLake through this shortcut.

For detailed instructions, see [Create an Azure Data Lake Storage Gen2 shortcut](/fabric/onelake/create-adls-shortcut). For more information about shortcuts, see [OneLake shortcuts](/fabric/onelake/onelake-shortcuts).

## Step 3 - Upload the SSIS package to OneLake

The Invoke SSIS Package activity reads packages from OneLake. Upload your *.dtsx* file (and optional *.dtsConfig* file) to a lakehouse.

1. In the Fabric portal, open the lakehouse where you want to store the package.
1. In the **Files** section, create a folder - for example, `ssis-packages`.
1. Upload the package by using one of these methods:

   | Method | How |
   | --- | --- |
   | **Fabric portal** | Select **Upload** → **Upload files** and choose your *.dtsx* file. |
   | **OneLake file explorer** | Drag and drop the file into the `packages` folder through the OneLake file explorer on your desktop. |

For more information about uploading files to OneLake, see the [Invoke SSIS Package activity documentation](/fabric/data-factory/invoke-ssis-package-activity).

## Step 4 - Run the package in a Fabric pipeline

1. In your Fabric workspace, create a new **Data Pipeline** or open an existing one.
1. From the **Activities** pane, add the **Invoke SSIS Package** activity to the pipeline canvas.
1. On the **Settings** tab, configure the activity:

   | Setting | Value |
   | --- | --- |
   | **Package path** | Browse to the *.dtsx* file you uploaded in Step 3. |
   | **Configuration path** *(optional)* | Browse to the *.dtsConfig* file, if applicable. |
   | **Encryption password** *(optional)* | If the package protection level is **EncryptSensitiveWithPassword** or **EncryptAllWithPassword**, provide the password used to encrypt the package. |
   | **Enable logging** | Select to write execution logs to OneLake. |

   :::image type="content" source="media/ssis-activity.png" alt-text="Screenshot of the Invoke SSIS Package activity settings tab in a Fabric pipeline." lightbox="media/ssis-activity.png" :::

1. Select **Save**, then select **Run** to execute the pipeline immediately, or select **Schedule** to set up recurring execution.
1. Monitor progress in the pipeline **Output** tab or the workspace **Monitor** hub. If logging is enabled, the activity output includes the **logging path** on OneLake.

For full configuration details, see [Use the Invoke SSIS Package activity to run an SSIS package](/fabric/data-factory/invoke-ssis-package-activity).

## Verify the results

After the pipeline run completes successfully:

1. Open the lakehouse and navigate to the shortcut you created in Step 2.
1. Confirm that the output files written by the SSIS package appear in the shortcut folder.

## Summary

By combining a few Fabric capabilities, you can bring file-based SSIS output into OneLake without modifying your existing packages:

1. **Azure Storage connection manager** writes files to ADLS Gen2 from within your SSIS package.
1. **OneLake shortcut** surfaces those files in a Fabric lakehouse - no data copy required.
1. **Package upload to OneLake** makes the *.dtsx* file available for Fabric pipeline execution.
1. **Invoke SSIS Package activity** orchestrates and monitors package execution in a Fabric pipeline.

This pattern lets you manage all your data in OneLake while preserving your existing SSIS investments.

## Related content

- [Invoke SSIS Package activity documentation](/fabric/data-factory/invoke-ssis-package-activity)
- [OneLake shortcuts](/fabric/onelake/onelake-shortcuts)
- [Create an Azure Data Lake Storage Gen2 shortcut](/fabric/onelake/create-adls-shortcut)
- [Azure Storage connection manager](../connection-manager/azure-storage-connection-manager.md)
- [Tutorial: Integrate SSIS with SQL database in Microsoft Fabric](integrate-fabric-sql-database.md)
- [Data Factory in Microsoft Fabric overview](/fabric/data-factory/data-factory-overview)
