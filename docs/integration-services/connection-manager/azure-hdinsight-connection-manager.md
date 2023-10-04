---
title: "Azure HDInsight Connection Manager"
description: "Azure HDInsight Connection Manager"
author: "Lingxi-Li"
ms.author: "lingxl"
ms.date: "02/28/2017"
ms.service: sql
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords:
  - "SQL13.DTS.DESIGNER.AFPHDICM.F1"
  - "SQL14.DTS.DESIGNER.AFPHDICM.F1"
---
# Azure HDInsight Connection Manager

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


The **Azure HDInsight Connection Manager** enables an SSIS package to connect to an Azure HDInsight cluster.

The **Azure HDInsight Connection Manager** is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).

To create and configure an **Azure HDInsight Connection Manager**, follow the steps below:

1. In the **Add SSIS Connection Manager** dialog box, select **AzureHDInsight**, and click **Add**.
2. In the **Azure HDInsight Connection Manager Editor** dialog box, specify the **Cluster DNS name** (without the protocol prefix), **Username**, and **Password** for the HDInsight cluster to connect to.
3. Click **OK** to close the dialog box.
4. You can see the properties of the connection manager you created in the **Properties** window.
