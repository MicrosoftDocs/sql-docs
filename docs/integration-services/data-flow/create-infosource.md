---
description: "Create InfoSource"
title: "Create InfoSource | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
ms.assetid: e7db233b-5464-43de-9d26-6dd24c7ac1b7
author: chugugrace
ms.author: chugu
---
# Create InfoSource

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  Use the **Create InfoSource** dialog box to create a new InfoSource. To create the new InfoSource, you use either the **Create InfoSource for Transaction Data** or the **Create InfoSource for Master Data** dialog box.  
  
 You can open the **Create InfoSource** dialog box from the **Connection Manager** page of the **SAP BW Destination Editor**. To learn more about the SAP BW destination, see [SAP BW Destination](../../integration-services/data-flow/sap-bw-destination.md).  
  
> [!IMPORTANT]  
>  The documentation for the Microsoft Connector 1.1 for SAP BW assumes familiarity with the SAP Netweaver BW environment. For more information about SAP Netweaver BW, or for information about how to configure SAP Netweaver BW objects and processes, see your SAP documentation.  
  
 **To open the Create InfoSource dialog box**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that contains the SAP BW destination.  
  
2.  On the **Data Flow** tab, double-click the SAP BW destination.  
  
3.  In the **SAP BW Destination Editor**, click **Connection Manager** to open the **Connection Manager** page of the editor.  
  
4.  On the **Connection Manager** page, in the **Create SAP BW Objects** group box, select **InfoSource**, and then click **Create**.  
  
## Options  
 **Transaction data**  
 Create a new InfoSource for transaction data.  
  
 If you select this option, the **Create InfoSource for Transaction Data** dialog box opens. You use the **Create InfoSource for Transaction Data** dialog box to create the new InfoSource. For more information about this dialog box, see [Create InfoSource for Transaction Data](../../integration-services/data-flow/create-infosource-for-transaction-data.md).  
  
 **Master data**  
 Create a new InfoSource for master data.  
  
 If you select this option, the **Create InfoSource for Master Data** dialog box opens. You use the **Create InfoSource for Master Data** dialog box to create the new InfoSource. For more information about this dialog box, see [Create InfoSource for Master Data](../../integration-services/data-flow/create-infosource-for-master-data.md).  
  
## See Also  
 [Microsoft Connector for SAP BW F1 Help](../../integration-services/microsoft-connector-for-sap-bw-f1-help.md)  
  
  
