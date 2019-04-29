---
title: "Azure HDInsight Hive Task | Microsoft Docs"
ms.custom: ""
ms.date: "02/28/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.afphivetask.f1"
  - "sql14.dts.designer.afphivetask.f1"
ms.assetid: e1896c73-128a-4128-9814-3e01f7dfe19b
author: janinezhang
ms.author: janinez
manager: craigg
---
# Azure HDInsight Hive Task
Use the **Azure HDInsight Hive Task** to run Hive script on an Azure HDInsight cluster.
     
To add an **Azure HDInsight Hive Task**, drag-drop it to the SSIS Designer, and double-click or right-click and click **Edit** to see the following **Azure HDInsight Hive Task Editor** dialog box.  
  
The **Azure HDInsight Hive Task** is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).
  
 The following list describes fields in this dialog box.  
  
1.  For the **HDInsightConnection** field, select an existing Azure HDInsight Connection Manager or create a new one that refers to the Azure HDInsight cluster used to execute the script.
  
2.  For the **AzureStorageConnection** field, select an existing Azure Storage Connection Manager or create a new one that refers to the Azure Storage Account associated with the cluster. This is only necessary if you want to download the script execution output and error logs.
 
3.  For the **BlobContainer** field, specify the storage container name associated with the cluster. This is only necessary if you want to download the script execution output and error logs.
  
4.  For the **LocalLogFolder** field, specify the folder to which the script execution output and error logs will be downloaded to. This is only necessary if you want to download the script execution output and error logs.   
  
5.  There are two ways to specify the Hive script to execute:
  
    1.  **In-line script**: Specify the **Script** field by typing in-line the script to execute in the **Enter Script** dialog box.
  
    2.  **Script file**: Upload the script file to Azure Blob Storage and specify the **BlobName** field. If the blob is not in the default storage account or container associated with the HDInsight cluster, the **ExternalStorageAccountName** and **ExternalBlobContainer** fields must be specified. For an external blob, make sure that it is configured as publicly accessible.  
  
     If both are specified, script file will be used and the in-line script will be ignored.
