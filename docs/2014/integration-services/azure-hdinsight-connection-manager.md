---
title: "Azure HDInsight Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "02/28/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "SQL12.DTS.DESIGNER.AFPHDICM.F1"
  - "SQL11.DTS.DESIGNER.AFPHDICM.F1"
ms.assetid: 850a978d-5dba-45b6-a10e-306aafbc353d
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Azure HDInsight Connection Manager
The **Azure HDInsight Connection Manager** enables an SSIS package to connect to an Azure HDInsight cluster.

To create and configure an **Azure HDInsight Connection Manager**, follow the steps below:

1. In the **Add SSIS Connection Manager** dialog box, select **AzureHDInsight**, and click **Add**.
2. In the **Azure HDInsight Connection Manager Editor** dialog box, specify the **Cluster DNS name** (without the protocol prefix), **Username**, and **Password** for the HDInsight cluster to connect to.
3. Click **OK** to close the dialog box.
4. You can see the properties of the connection manager you created in the **Properties** window.
