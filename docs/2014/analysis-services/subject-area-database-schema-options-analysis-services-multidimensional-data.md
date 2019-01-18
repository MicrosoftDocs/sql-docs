---
title: "Subject Area Database Schema Options (Schema Generation Wizard) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.schemagenwizard.subjectareaschemaopts.f1"
ms.assetid: 4c109bb8-e19d-412b-908f-bfdd7f872439
author: minewiskan
ms.author: owend
manager: craigg
---
# Subject Area Database Schema Options (Schema Generation Wizard) (Analysis Services - Multidimensional Data)
  Use the **Subject Area Database Schema Options** page to control how the schema is generated, as well as to define how data is preserved.  
  
## Options  
 **Owning schema**  
 Specifies the name of the schema within the new subject area database.  
  
 **Create primary keys on dimension tables**  
 Creates primary keys on the dimension tables in the generated schema. No index is generated if you do not select this option.  
  
> [!NOTE]  
>  If you do not select this option, referential integrity is enforced.  
  
 **Create indexes**  
 Creates indexes on foreign key columns in the generated schema.  
  
 **Enforce referential integrity**  
 Enforces referential integrity within the generated schema. If you do not select this option, relationships are created but not enforced.  
  
 **Preserve data on regeneration**  
 Retains data in the subject area database when the wizard finishes. If you do not select this option, all the data in the subject area database may be erased without warning.  
  
 **Populate time table(s)**  
 Specifies how the wizard populates the time tables. The following table describes the possible values for this option.  
  
> [!NOTE]  
>  This option is enabled only if Schema Generation Wizard is called from an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] project, using [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] in project mode.  
  
|Value|Description|  
|-----------|-----------------|  
|Populate|The subject area time tables are populated.|  
|Do not populate|The subject area time tables are not populated.|  
|Populate only if empty|The subject area time tables are populated only if they are empty.|  
  
## See Also  
 [Schema Generation Wizard F1 Help &#40;Analysis Services - Multidimensional Data&#41;](schema-generation-wizard-f1-help-analysis-services-multidimensional-data.md)  
  
  
