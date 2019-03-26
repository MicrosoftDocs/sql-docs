---
title: "Recordset Destination | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.recordsetdest.f1"
helpviewer_keywords: 
  - "Recordset destination"
  - "ADO recordsets [Integration Services]"
  - "destinations [Integration Services], Recordset"
  - "in-memory ADO recordsets [Integration Services]"
ms.assetid: be973cf1-c4ff-49f8-987e-314c08ef98e4
author: janinezhang
ms.author: janinez
manager: craigg
---
# Recordset Destination
  The Recordset destination creates and populates an in-memory ADO recordset. The shape of the recordset is defined by the input to the Recordset destination at design time.  
  
## Configuration of the Recordset Destination  
 You configure the Recordset destination by specifying the variable that stores the ADO recordset.  
  
 At run time, an ADO recordset is written to the variable of type, Object, that is specified in the VariableName property of the Recordset destination. The variable then makes the Recordset available outside the data flow, where it can be used by scripts and other package elements.  
  
 This source has one input. It does not support an error output.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [Recordset Destination Custom Properties](../../integration-services/data-flow/recordset-destination-custom-properties.md)  
  
 For more information about how to set the properties, see [Set the Properties of a Data Flow Component](../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
## Related Tasks  
 [Use a Recordset Destination](../../integration-services/data-flow/use-a-recordset-destination.md)  
  
  
