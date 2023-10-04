---
title: "Azure HDInsight Delete Cluster Task"
description: "Azure HDInsight Delete Cluster Task"
author: chugugrace
ms.author: chugu
ms.date: "02/28/2017"
ms.service: sql
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords:
  - "sql13.dts.designer.afpdelcltask.f1"
  - "sql14.dts.designer.afpdelcltask.f1"
---
# Azure HDInsight Delete Cluster Task

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


The **Azure HDInsight Delete Cluster Task** enables an SSIS package to delete an Azure HDInsight cluster in the specified Azure subscription and resource group.
  
The **Azure HDInsight Delete Cluster Task** is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).
  
> [!NOTE]
> Deleting an HDInsight cluster may take 10~20 minutes.  
  
To add an **Azure HDInsight Delete Cluster Task**, drag-drop it to the SSIS Designer, and double-click or right-click and click **Edit** to see the following **Azure HDInsight Delete Cluster Task Editor** dialog box.  
  
The following table provides a description for the fields in the dialog box.  
  
|Field|Description|  
|-|-|  
|AzureResourceManagerConnection|Select an existing Azure Resource Manager Connection Manager or create a new one that will be used to delete the HDInsight cluster.|
|SubscriptionId|Specify the ID of the subscription the HDInsight cluster is in.|
|ResourceGroup|Specify the Azure resource group the HDInsight cluster is in.|
|ClusterName|Specify the name of the cluster to be deleted.|  
|FailIfNotExists|Specify whether the task should fail if the cluster does not exist.|
