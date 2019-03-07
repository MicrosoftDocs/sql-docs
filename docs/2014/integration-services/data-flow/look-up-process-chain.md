---
title: "Look Up Process Chain | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: f6303ea4-fbbf-4cba-bc60-828df62be8c2
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Look Up Process Chain
  Use the **Look Up Process Chain** dialog box to look up a process chain that is defined in the SAP Netweaver BW system. When the list of available process chains appears, select the chain that you want, and the source will populate the associated options with the required values.  
  
 The SAP BW source of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Connector 1.1 for SAP BW uses the **Look Up Process Chain** dialog box. To learn more about the SAP BW source, see [SAP BW Source](sap-bw-source.md).  
  
> [!IMPORTANT]  
>  The documentation for the Microsoft Connector 1.1 for SAP BW assumes familiarity with the SAP Netweaver BW environment. For more information about SAP Netweaver BW, or for information about how to configure SAP Netweaver BW objects and processes, see your SAP documentation.  
  
 **To open the Look Up Process Chain dialog box**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that contains the SAP BW source.  
  
2.  On the **Data Flow** tab, double-click the SAP BW source.  
  
3.  In the **SAP BW Source Editor**, click **Connection Manager** to open the **Connection Manager** page of the editor.  
  
4.  In the **Process Chain** group box, click **Look up** to display the **Look Up Process Chain** dialog box.  
  
     The **Process Chain** group box appears only if the value for **Execution mode** is **P - Trigger process chain**.  
  
## Lookup Options  
 In the lookup fields, you can filter results by using the asterisk wildcard character (*), or by using a partial string in combination with the asterisk wildcard character. However, if you leave a lookup field empty, the lookup operation will only match empty strings in that field.  
  
 **Process chain**  
 Enter the name of the process chain that you want to look up, or enter a partial name with the asterisk wildcard character (*). Or, use the asterisk wildcard character alone to include all process chains.  
  
 **Look up**  
 Look up matching process chains that are defined in the SAP Netweaver BW system.  
  
## Lookup Results  
 After you click the Look up button, a list of the process chains in the SAP Netweaver BW system appears in a table with the following column headings:  
  
 **Process Chain**  
 Displays the name of the process chain that is defined in the SAP Netweaver BW system.  
  
 **Description**  
 Displays the description of the process chain.  
  
 When the list of available process chains appears, select the chain that you want, and the source will populate the associated options with the required values.  
  
## See Also  
 [SAP BW Source Editor &#40;Connection Manager Page&#41;](sap-bw-source-editor-connection-manager-page.md)   
 [Microsoft Connector 1.1 for SAP BW F1 Help](../microsoft-connector-for-sap-bw-f1-help.md)  
  
  
