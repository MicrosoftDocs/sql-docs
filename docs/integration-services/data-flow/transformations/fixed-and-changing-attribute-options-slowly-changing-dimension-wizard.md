---
title: "Fixed and Changing Attribute Options (Slowly Changing Dimension Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.loaddimwizard.attriboption.f1"
ms.assetid: c841345c-7d03-452f-9379-1c8c464f029c
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Fixed and Changing Attribute Options (Slowly Changing Dimension Wizard
  Use the **Fixed and Changing Attribute Options** dialog box to specify how to respond to changes in fixed and changing attributes.  
  
 To learn more about this wizard, see [Slowly Changing Dimension Transformation](../../../integration-services/data-flow/transformations/slowly-changing-dimension-transformation.md).  
  
## Options  
 **Fixed attributes**  
 For fixed attributes, indicate whether the task should fail if a change is detected in a fixed attribute.  
  
 **Changing attributes**  
 For changing attributes, indicate whether the task should change outdated or expired records, in addition to current records, when a change is detected in a changing attribute. An expired record is a record that has been replaced with a newer record by a change in a historical attribute (a Type 2 change). Selecting this option may impose additional processing requirements on a multidimensional object constructed on the relational data warehouse.  
  
## See Also  
 [Configure Outputs Using the Slowly Changing Dimension Wizard](../../../integration-services/data-flow/transformations/configure-outputs-using-the-slowly-changing-dimension-wizard.md)  
  
  
