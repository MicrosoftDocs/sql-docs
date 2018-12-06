---
title: "Create InfoSource for Master Data | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: b52a9a89-8380-4a02-8a83-dcfb46ae860e
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Create InfoSource for Master Data
  Use the **Create InfoSource for Master Data** dialog box to create a new InfoSource for master data in the SAP Netweaver BW system.  
  
 You can open the **Create InfoSource for Master Data** dialog box from the **Connection Manager** page of the **SAP BW Destination Editor**. To learn more about the SAP BW destination, see [SAP BW Destination](sap-bw-destination.md).  
  
> [!IMPORTANT]  
>  The documentation for the Microsoft Connector 1.1 for SAP BW assumes familiarity with the SAP Netweaver BW environment. For more information about SAP Netweaver BW, or for information about how to configure SAP Netweaver BW objects and processes, see your SAP documentation.  
  
 **To open the Create InfoSource for Master Data dialog box**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that contains the SAP BW destination.  
  
2.  On the **Data Flow** tab, double-click the SAP BW destination.  
  
3.  In the **SAP BW Destination Editor**, click **Connection Manager** to open the **Connection Manager** page of the editor.  
  
4.  On the **Connection Manager** page, in the **Create SAP BW Objects** group box, select **InfoSource**, and then click **Create**.  
  
5.  In the **Create InfoSource** dialog box, select **Master data**, and then click **OK**.  
  
## Options  
 **InfoObject name**  
 Enter the name of the InfoObject on which the new InfoSource should be based.  
  
 **Look up**  
 Look up the InfoObject. This option opens the **Look Up InfoObject** dialog box in which you can select the InfoObject. For more information about this dialog box, see [Look Up InfoObject](look-up-infoobject.md).  
  
 After you select an InfoObject, the component populates the **InfoObject name** text box with the name of the selected InfoObject.  
  
 **New**  
 Create a new InfoObject. This option opens the **Create New InfoObject** dialog box in which you can create the new InfoObject. For more information about this dialog box, see [Create New InfoObject](create-new-infoobject.md).  
  
 After you create an InfoObject, the component populates the **InfoObject name** text box with the name of the new InfoObject.  
  
 **Short description**  
 Enter a short description for the new InfoSource.  
  
 **Long description**  
 Enter a long description for the new InfoSource.  
  
 **Source system**  
 Select the source system to be associated with the new InfoSource.  
  
 **Application**  
 Enter the name of the application to be associated with the new InfoSource.  
  
 **Attributes**  
 Indicates that the master data consists of attributes.  
  
 **Texts**  
 Indicates that the master data consists of attributes.  
  
 **Save & Activate**  
 Save and activate the new InfoSource.  
  
## See Also  
 [Create InfoSource](create-infosource.md)   
 [Microsoft Connector 1.1 for SAP BW F1 Help](../microsoft-connector-for-sap-bw-f1-help.md)  
  
  
