---
title: "Create and Manage Roles (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.rolemanager.f1"
  - "sql12.asvs.bidtoolset.roledb.f1"
ms.assetid: e23d27a8-e968-4082-9dbe-963fc724b5d9
author: minewiskan
ms.author: owend
manager: kfile
---
# Create and Manage Roles (SSAS Tabular)
  Roles, in tabular models, define member permissions for a model. Roles are defined for a model project by using the Role Manager dialog box in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. When a model is deployed, database administrators can manage roles by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 The tasks in this topic describe how to create and manage roles during model authoring by using the Role Manager dialog box in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. For information about managing roles in a deployed model database, see [Tabular Model Roles &#40;SSAS Tabular&#41;](roles-ssas-tabular.md).  
  
## Tasks  
 To create, edit, copy, and delete roles, you will use the **Role Manager** dialog box. To view the **Role Manager** dialog box, in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], click the **Model** menu, and then click **Role Manager**.  
  
###  <a name="bkmk_new_role"></a> To create a new role  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], click the **Model** menu, and then click **Role Manager**.  
  
2.  In the **Role Manager** dialog box, click **New**.  
  
     A new highlighted role is added to the Roles list.  
  
3.  In the **Roles** list, in the **Name** field, type a name for the role.  
  
     By default, the name of the default role will be incrementally numbered for each new role. It is recommended you type a name that clearly identifies the member type, for example, Finance Managers or Human Resources Specialists.  
  
4.  In the **Permissions** field, click the down arrow and then select one of the following permission types:  
  
    |Permission|Description|  
    |----------------|-----------------|  
    |**None**|Members cannot make any modifications to the model schema and cannot query data.|  
    |**Read**|Members are allowed to query data (based on row filters) but cannot make any changes to the model schema.|  
    |**Read and Process**|Members are allowed to query data (based on row-level filters) and run Process and Process All operations, but cannot make any changes to the model schema.|  
    |**Process**|Members can run Process and Process All operations. Cannot modify the model schema and cannot query data.|  
    |**Administrator**|Members can make modifications to the model schema and can query all data.|  
  
5.  To enter a description for the role, click the **Description** field, and then type a description.  
  
6.  If the role you are creating has Read or Read and Process permission, you can add row filters using a DAX formula. To add row filters, click the **Row Filters** tab, then select a table, then click the **DAX Filter** field, and then type a DAX formula.  
  
7.  To add members to the role, click the **Members** tab, and then click **Add**.  
  
    > [!NOTE]  
    >  Role members can also be added to a deployed model by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information, see [Manage Roles by using SSMS &#40;SSAS Tabular&#41;](manage-roles-by-using-ssms-ssas-tabular.md).  
  
8.  In the **Select Users or Groups** dialog box, enter Windows user or Windows group objects as members.  
  
9. Click **Ok**.  
  
## See Also  
 [Roles &#40;SSAS Tabular&#41;](roles-ssas-tabular.md)   
 [Perspectives &#40;SSAS Tabular&#41;](perspectives-ssas-tabular.md)   
 [Analyze in Excel &#40;SSAS Tabular&#41;](analyze-in-excel-ssas-tabular.md)   
 [USERNAME Function &#40;DAX&#41;](https://msdn.microsoft.com/library/hh230954.aspx)   
 [CUSTOMDATA Function &#40;DAX&#41;](https://msdn.microsoft.com/library/hh213140.aspx)  
  
  
