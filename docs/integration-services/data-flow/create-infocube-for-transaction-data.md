---
description: "Create InfoCube for Transaction Data"
title: "Create InfoCube for Transaction Data | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
ms.assetid: 673cea01-a260-4fce-a1a0-f73839289805
author: chugugrace
ms.author: chugu
---
# Create InfoCube for Transaction Data

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  Use the **Create InfoCube for Transaction Data** dialog box to create a new InfoCube for transaction data in the SAP Netweaver BW system.  
  
 You can open the **Create InfoCube for Transaction Data** dialog box from the **Connection Manager** page of the **SAP BW Destination Editor**. To learn more about the SAP BW destination, see [SAP BW Destination](../../integration-services/data-flow/sap-bw-destination.md).  
  
> [!IMPORTANT]  
>  The documentation for the Microsoft Connector 1.1 for SAP BW assumes familiarity with the SAP Netweaver BW environment. For more information about SAP Netweaver BW, or for information about how to configure SAP Netweaver BW objects and processes, see your SAP documentation.  
  
 **To open the Create InfoCube for Transaction Data dialog box**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that contains the SAP BW destination.  
  
2.  On the **Data Flow** tab, double-click the SAP BW destination.  
  
3.  In the **SAP BW Destination Editor**, click **Connection Manager** to open the **Connection Manager** page of the editor.  
  
4.  On the **Connection Manager** page, in the **Create SAP BW Objects** group box, select **InfoCube**, and then click **Create**.  
  
## General Options  
 **InfoCube name**  
 Enter a name for the new InfoCube.  
  
 **Long description**  
 Enter a description for the new InfoCube.  
  
 **Save & Activate**  
 Save and activate the new InfoCube.  
  
## InfoCube Transfer Structure Options  
 The InfoCube transfer structure section lets you associate data flow columns to InfoObjects.  
  
 **PipelineElement**  
 Displays the column in the output of the data flow that is connected to the SAP BW destination.  
  
 **PipelineDataType**  
 Displays the data type of the data flow column.  
  
 **InfoObject**  
 Displays the name of the InfoObject that is associated with the data flow column.  
  
 **Type**  
 Displays the type of the InfoObject that is associated with the data flow column. The following table lists the possible values for the type.  
  
|Value|Description|  
|-----------|-----------------|  
|CHA|Characteristics|  
|UNI|Units|  
|KYF|Key figures|  
|TIM|Time characteristics|  
  
 **Iobject - Search**  
 Associate an existing InfoObject to the data flow column for the current row. To make this association, click **Search**, and then use the **Look Up InfoObject** dialog box to select the existing InfoObject. For more information about this dialog box, see [Look Up InfoObject](../../integration-services/data-flow/look-up-infoobject.md).  
  
 After you select an existing InfoObject, the component populates the **InfoObject** and **Type** columns with the selected values.  
  
 **Iobject - New**  
 Create a new InfoObject and associate this new InfoObject to the data flow column in the current row. To create the new InfoObject, click **New**, and then use the **Create New InfoObject** dialog box to create the InfoObject. For more information about this dialog box, see [Create New InfoObject](../../integration-services/data-flow/create-new-infoobject.md).  
  
 After you create a new InfoObject, the component populates the **InfoObject** and **Type** columns with the new values.  
  
 **Iobject - Remove**  
 Remove the association between the InfoObject and the data flow column for the current row. To remove this association, click **Remove**.  
  
## See Also  
 [Microsoft Connector for SAP BW F1 Help](../../integration-services/microsoft-connector-for-sap-bw-f1-help.md)  
  
  
