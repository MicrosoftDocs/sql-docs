---
title: "Create New InfoObject | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 3587a633-1c0b-4d63-a22a-6b2b93923c3a
author: janinezhang
ms.author: janinez
manager: craigg
---
# Create New InfoObject
  Use the **Create New InfoObject** dialog box to create a new InfoObject in the SAP Netweaver BW system.  
  
 You can open the **Create InfoObject** dialog box from the **Connection Manager** page of the **SAP BW Destination Editor**. To learn more about the SAP BW destination, see [SAP BW Destination](../../integration-services/data-flow/sap-bw-destination.md).  
  
> [!IMPORTANT]  
>  The documentation for the Microsoft Connector 1.1 for SAP BW assumes familiarity with the SAP Netweaver BW environment. For more information about SAP Netweaver BW, or for information about how to configure SAP Netweaver BW objects and processes, see your SAP documentation.  
  
 **To open the Create New InfoObject dialog box**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that contains the SAP BW destination.  
  
2.  On the **Data Flow** tab, double-click the SAP BW destination.  
  
3.  In the **SAP BW Destination Editor**, click **Connection Manager** to open the **Connection Manager** page of the editor.  
  
4.  On the **Connection Manager** page, in the **Create SAP BW Objects** group box, do one of the following steps to create an InfoObject:  
  
    1.  To create an InfoObject directly, select **InfoObject**, and then click **Create** to open the **Create New InfoObject** dialog box.  
  
    2.  To create an InfoObject while creating an InfoCube, select **InfoCube**, and then click **Create**. In the **Create InfoCube for Transaction Data** dialog box, in the **IObject** column for one of the rows in the list, select **Create** to open the **Create New InfoObject** dialog box.  
  
        > [!NOTE]  
        >  Each row in the table represents a column in the data flow of the package.  
  
    3.  To create an InfoObject while creating an InfoSouce for transactional data, select **InfoSource**, and then click **Create**. In the **Create InfoSource** dialog box, select **Transaction Data**, and then click **OK**. In the **Create InfoSource for Transaction Data** dialog box, in the **IObject** column for one of the rows in the list, select **Create** to open the **Create New InfoObject** dialog box.  
  
        > [!NOTE]  
        >  Each row in the table represents a column in the data flow of the package.  
  
    4.  To create an InfoObject while creating an InfoSource for master data, select **InfoSource**, and then click **Create**. In the **Create InfoSource** dialog box, select **Master Data**, and then click **OK**. In the **Create InfoSource for Master Data** dialog box, click **New** to open the **Create New InfoObject** dialog box.  
  
 You can also open the **Create New InfoObject** dialog box by clicking **New** in the **Attributes** section of the **Create New InfoObject** dialog box.  
  
## General Options  
 **Characteristic**  
 Create an InfoObject that represents dimension data.  
  
 **Key figure**  
 Create an InfoObject that represents fact data.  
  
 **InfoObject name**  
 Enter a name for the InfoObject.  
  
 **Short description**  
 Enter a short description for the InfoObject.  
  
 **Long description**  
 Enter a long description for the InfoObject.  
  
 **Has master data**  
 Indicate that the InfoObject contains master data in the form of attributes, texts, or hierarchies.  
  
> [!NOTE]  
>  Select this option if the InfoObject represents dimensional data and you have selected the **Characteristic** option.  
  
 **Allow lower-case characters**  
 Allow lower-case characters in the InfoObject data.  
  
 **Save & Activate**  
 Save and active the new InfoObject.  
  
## Data Type Options  
 **CHAR - Character String**  
 Indicates that the InfoObject contains character data.  
  
 **NUMC - Numeric String**  
 Indicates that the InfoObject contains numeric data.  
  
 **DATS - Date (YYYYMMDD)**  
 Indicates that the InfoObject contains date data.  
  
 **TIMS - Time (HHMMSS)**  
 Indicates that the InfoObject contains time data.  
  
 **Length**  
 Enter the length of the data.  
  
## Text Options  
 **With Texts**  
 Indicate that the InfoObject contains texts.  
  
 **Short Text**  
 Indicates that the InfoObject contains short texts.  
  
 **Medium Text**  
 Indicates that the InfoObject contains medium texts.  
  
 **Long Text**  
 Indicates that the InfoObject contains long texts.  
  
 **Text Language-Dependent**  
 Indicates that the texts are language-dependent.  
  
 **Text Time-Dependent**  
 Indicates that the texts are time-dependent.  
  
## Attributes Section  
 The **Attributes** section consists of a list of attributes for the InfoObject, and the options that let you add and remove attributes from the list.  
  
### Attributes List  
 The **Attributes** list displays the attributes of the InfoObject that you are creating. The **Attributes** list has the following column headings:  
  
 **InfoObject**  
 View the name of the InfoObject.  
  
 **Description**  
 View the description of the InfoObject.  
  
 **InfoObject Type**  
 View the type of the InfoObject. The following table lists the possible values for the type.  
  
|Value|Description|  
|-----------|-----------------|  
|CHA|Characteristics|  
|KYF|Key figures|  
|UNI|Units|  
|TIM|Time characteristics|  
  
### Attributes Options  
 Use the following options to add and remove attributes for the InfoObject that you are creating:  
  
 **Add**  
 Add an existing InfoObject as an attribute.  
  
 To add an existing InfoObject, click Add, and then use the **Look Up InfoObject** dialog box to find the InfoObject. For more information about this dialog box, see [Look Up InfoObject](../../integration-services/data-flow/look-up-infoobject.md).  
  
 **New**  
 Add a new InfoObject as an attribute.  
  
 To create and add a new InfoObject, click New, and then use a new instance of the **Create New InfoObject** dialog box to create the new InfoObject.  
  
 **Remove**  
 Remove the selected InfoObject from the **Attributes** list.  
  
## See Also  
 [Create InfoCube for Transaction Data](../../integration-services/data-flow/create-infocube-for-transaction-data.md)   
 [Create InfoSource](../../integration-services/data-flow/create-infosource.md)   
 [Create InfoSource for Transaction Data](../../integration-services/data-flow/create-infosource-for-transaction-data.md)   
 [Create InfoSource for Master Data](../../integration-services/data-flow/create-infosource-for-master-data.md)   
 [Microsoft Connector for SAP BW F1 Help](../../integration-services/microsoft-connector-for-sap-bw-f1-help.md)  
  
  
