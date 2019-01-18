---
title: "Manage Roles by using SSMS (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 652faac0-1cfc-438b-8119-2f4b090a2381
author: minewiskan
ms.author: owend
manager: craigg
---
# Manage Roles by using SSMS (SSAS Tabular)
  You can create, edit, and manage roles for a deployed tabular model by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 Tasks in this topic:  
  
-   [To create a new role](#bkmk_new_role)  
  
-   [To copy a role](#bkmk_copy_role)  
  
-   [To edit a role](#bkmk_edit_role)  
  
-   [To delete a role](#bkmk_deletet_role)  
  
> [!CAUTION]  
>  Re-deploying a tabular model project with roles defined by using Role Manager in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] will overwrite roles defined in a deployed tabular model.  
  
> [!CAUTION]  
>  Using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to manage a tabular model workspace database while the model project is open in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] may cause the Model.bim file to become corrupted. When creating and managing roles for a tabular model workspace database, use Role Manager in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)].  
  
###  <a name="bkmk_new_role"></a> To create a new role  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the tabular model database for which you want to create a new role, then right click on **Roles**, and then click **New Role**.  
  
2.  In the **Create Role** dialog box, in the Select a page window, click **General**.  
  
3.  In the general settings window, in the **Name** field, type a name for the role.  
  
     By default, the name of the default role will be incrementally numbered for each new role. It is recommended you type a name that clearly identifies the member type, for example, Finance Managers or Human Resources Specialists.  
  
4.  In **Set the database permissions for this role**, select one of the following permissions options:  
  
    |Permission|Description|  
    |----------------|-----------------|  
    |**Full control (Administrator)**|Members can make modifications to the model schema and can view all data.|  
    |**Process database**|Members can run Process and Process All operations. Cannot modify the model schema and cannot view data.|  
    |**Read**|Members are allowed to view data (based on row filters) but cannot make any changes to the model schema.|  
  
5.  In the **Create Role** dialog box, in the Select a page window, click **Membership**.  
  
6.  In the membership settings window, click **Add**, and then in the **Select Users or Groups** dialog box, add the Windows users or groups you want to add as members.  
  
7.  If the role you are creating has Read permissions, you can add row filters for any table using a DAX formula. To add row filters, in the **Role Properties - \<rolename>** dialog box, in **Select a page**, click on **Row Filters**.  
  
8.  In the row filters window, select a table, then click on the **DAX Filter** field, and then in the **DAX Filter - \<tablename>** field, type a DAX formula.  
  
    > [!NOTE]  
    >  The DAX Filter - \<tablename> field does not contain an AutoComplete query editor or insert function feature. To use AutoComplete when writing a DAX formula, you must use a DAX formula editor in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)].  
  
9. Click **Ok** to save the role.  
  
###  <a name="bkmk_copy_role"></a> To copy a role  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the tabular model database that contains the role you want to copy, then expand **Roles**, then right click on the role, and then click **Duplicate**.  
  
###  <a name="bkmk_edit_role"></a> To edit a role  
  
-   In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the tabular model database that contains the role you want to edit, then expand **Roles**, then right click on the role, and then click **Properties**.  
  
     In the **Role Properties** \<rolename> dialog box, you can change permissions, add or remove members, and add/edit row filters.  
  
###  <a name="bkmk_deletet_role"></a> To delete a role  
  
-   In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the tabular model database that contains the role you want to delete, then expand **Roles**, then right click on the role, and then click **Delete**.  
  
## See Also  
 [Roles &#40;SSAS Tabular&#41;](roles-ssas-tabular.md)  
  
  
