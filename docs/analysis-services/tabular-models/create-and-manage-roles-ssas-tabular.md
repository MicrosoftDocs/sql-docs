---
title: "Create and manage roles in Analysis Services tabular models | Microsoft Docs"
ms.date: 09/17/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Create and manage roles 
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  Roles, in tabular models, define member permissions for a model. Roles are defined for a model project by using the Role Manager dialog box in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. 

> [!IMPORTANT]
> If you're deploying your project to Azure Analysis Services, use **Integrated Workspace** as your workspace database. To learn more, see [Workspace database](workspace-database-ssas-tabular.md).
  
 The tasks in this article describe how to create and manage roles during model authoring by using the Role Manager dialog box in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. For information about managing roles in a deployed model database, see [Tabular Model Roles](../../analysis-services/tabular-models/tabular-model-roles-ssas-tabular.md).  
  
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
    >  Role members can also be added to a deployed model by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information, see [Manage Roles by using SSMS](../../analysis-services/tabular-models/manage-roles-by-using-ssms-ssas-tabular.md).  
  
8.  In the **Select Users or Groups** dialog box, enter Windows user or Windows group objects as members.  
  
9. Click **Ok**.  
  
## See also  
 [Roles](../../analysis-services/tabular-models/roles-ssas-tabular.md)   
 [Perspectives](../../analysis-services/tabular-models/perspectives-ssas-tabular.md)   
 [Analyze in Excel](../../analysis-services/tabular-models/analyze-in-excel-ssas-tabular.md)   
 [USERNAME Function (DAX)](http://msdn.microsoft.com/22dddc4b-1648-4c89-8c93-f1151162b93f)   
 [CUSTOMDATA Function (DAX)](http://msdn.microsoft.com/58235ad8-226c-43cc-8a69-5a52ac19dd4e)  
  
  
