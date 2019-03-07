---
title: "Linked Measure Groups | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "linked measure groups [Analysis Services]"
  - "referencing measure groups"
  - "Linked Measure Group Wizard"
  - "measure groups [Analysis Services], linked"
  - "linked dimensions [Analysis Services]"
ms.assetid: 7f838452-8669-4194-8e15-7afdc7f15251
author: minewiskan
ms.author: owend
manager: craigg
---
# Linked Measure Groups
  A linked measure group is based on another measure group in a different cube within the same database or a different Analysis Services database. You might use a linked measure group if you want to reuse a set of measures, and the corresponding data values, in multiple cubes.  
  
 Microsoft recommends that the original and linked measure groups reside in solutions that run on the same server. Linking to a measure group on a remote server is scheduled for deprecation in a future release (see [Deprecated Analysis Services Features in SQL Server 2014](../deprecated-analysis-services-features-in-sql-server-2014.md)).  
  
> [!IMPORTANT]  
>  Linked measure groups are read-only. To pick up the latest changes, you must delete and recreate all linked measure groups based on the modified source object. For this reason, copy and pasting measure groups between projects is an alternative approach that you should consider in case future modifications to the measure group are required.  
  
## Usage Limitations  
 As noted previously, an important constraint to using linked measures is an inability to customize a linked measure directly. Modifications to the data type, format, data binding, and visibility, as well as membership of the items in the measure group itself, are all changes that must be made in the original measure group.  
  
 Operationally, linked measure groups are identical to other measure groups when accessed by client applications, and are queried in the same manner as other measure groups.  
  
 When you query a cube that contains a linked measure group, the link is established and resolved during the first calculation pass of the destination cube. Because of this behavior, any calculations that are stored in the linked measure group cannot be resolved before the query is evaluated. In other words, calculated measures and calculated cells must be recreated in the destination cube rather than inherited from the source cube.  
  
 The following list summarizes usage limitations.  
  
-   You cannot create a linked measure group from another linked measure group.  
  
-   You cannot add or remove measures in a linked measure group. Membership is defined only in the original measure group.  
  
-   Writeback is not supported in linked measure groups.  
  
-   Linked measure groups cannot be used in multiple many-to-many relationships, especially when those relationships are in different cubes. Doing so can result in ambiguous aggregations. For more information, see [Incorrect Amounts for Linked Measures in Cubes containing Many-to-Many Relationships](https://social.technet.microsoft.com/wiki/contents/articles/22911.incorrect-amounts-for-linked-measures-in-cubes-containing-many-to-many-relationships-ssas-troubleshooting.aspx).  
  
 The measures contained in a linked measure group can be directly organized only along linked dimensions retrieved from the same [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. However, you can use calculated members to relate information from linked measure groups to the other, non-linked dimensions in your cube. You can also use an indirect relationship, such as a reference or many-to-many relationship, to relate non-linked dimensions to a linked measure group.  
  
## Create or modify a linked measure  
 Use [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] to create a linked measure group.  
  
1.  Finalize any modifications to the original measure group now, in the source cube, so that you don't have to recreate the linked measure groups later in subsequent cubes. You can rename a linked object, but you cannot change any other properties.  
  
2.  In Solution Explorer, double-click the cube to which you are adding the linked measure group. This step opens the cube in Cube Designer.  
  
3.  In Cube Designer, in either the Measures pane or Dimensions pane, right-click anywhere in either pane, then select **New Linked Object**. This starts the Linked Object Wizard.  
  
4.  On the first page, specify the data source. This step establishes the location of the original measure group. The default is the current cube in the current database, but you can also choose a different Analysis Services database.  
  
5.  On the next page, choose the measure group or dimension you want to link. Dimensions and Cube objects, such as measure groups, are listed separately. Only those objects not already present in the current cube are available.  
  
6.  Click **Finish** to create the linked object. Linked objects appear in the Measures and Dimensions pane, indicated by the link icon.  
  
## Secure a linked measure  
 After the link has been defined, access to the measures in a linked measure group, is managed in the same manner as access to other measure groups. A linked object appears alongside its non-linked counterparts in Role Designer. For more information on managing security for a measure group, see [Grant cube or model permissions &#40;Analysis Services&#41;](grant-cube-or-model-permissions-analysis-services.md).  
  
 In order to define or use a linked measure group, the Windows service account for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance must belong to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database role that has `ReadDefinition` and `Read` access rights on the source [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance to the source cube and measure group, or must belong to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Administrators role for the source [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance.  
  
## See Also  
 [Define Linked Dimensions](define-linked-dimensions.md)  
  
  
