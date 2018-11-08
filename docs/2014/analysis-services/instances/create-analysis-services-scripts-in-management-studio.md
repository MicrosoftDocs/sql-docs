---
title: "Create Analysis Services Scripts in Management Studio | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Analysis Services objects, scripts"
  - "objects [Analysis Services], scripts"
  - "scripts [Analysis Services], objects"
ms.assetid: 4f1b965c-9ca6-427b-8f4d-0ce1eea7c0fe
author: minewiskan
ms.author: owend
manager: craigg
---
# Create Analysis Services Scripts in Management Studio
  [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] includes script generation features, templates, and editors that you can use to script Analysis Services objects and tasks.  
  
## Script Analysis Services Tasks in Management Studio  
 Scripting tasks in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] is accomplished by clicking one of the Script options in a task-oriented dialog box. All of the dialog boxes that you use to perform tasks such as backup or restore database, process an object, or design an aggregation, include a Script option at the top of the dialog box. Selecting one of these options generates an XMLA script based on the information and settings in the dialog box.  
  
 By default, the script is generated and placed in an XMLA query editor, but you can also expand the Script option list to direct the script to the Windows Clipboard or a file.  
  
#### To script an Analysis Services task  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
2.  Right-click a database and click **Backup**. This opens the Backup Database dialog box. Specify a backup file name and choose the options you want for this backup.  
  
3.  Click **Script** at the top of the dialog box. The Script feature is part of all task-based dialog boxes in Management Studio. It has the following options: **Script Action to New Query Window** to open the query editor window, **Script Action to File** to save the XMLA script to a file, or **Script Action to Clipboard** to save the XMLA script to the Clipboard.  
  
     Note that the **Script Action to Job** option that is listed as a script option in Management Studio is not supported for Analysis Services scripts.  
  
4.  If you select the default option, **Script Action to New Query Window**, a generated script is placed in an XMLA query window.  
  
     You can now close the Backup Database dialog box and edit or run the XMLA script directly.  
  
## Script Analysis Services Objects in Management Studio  
 Scripting objects in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] is accomplished by right-clicking an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] object in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and selecting either **Create to**, **Alter to**, or **Delete to**. Each of these options can be directed to a window or a file, but regardless of where the script is directed to, it will come in the form of a DDL script in an XMLA wrapper. One great advantage to such scripts is that they can be run against any server you point them at. Also, names in the scripts can be changed and run on an iterative basis for mass construction, alteration, or deletion of objects.  
  
 Objects that you can script include the elements of an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, including data sources, data source views, cubes, dimensions, mining structures, and roles.  
  
 Prerequisites include an understanding of XML for Analysis (XMLA). Fortunately, [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] has a feature that automatically creates the XMLA script required to create objects, such as cubes. This automation feature helps reduce the learning curve for XMLA. For more information about how to use XMLA, see [Developing with XMLA in Analysis Services](../multidimensional-models-scripting-language-assl-xmla/developing-with-xmla-in-analysis-services.md). For more information about how to use XMLA, see [Developing with XMLA in Analysis Services](../multidimensional-models-scripting-language-assl-xmla/developing-with-xmla-in-analysis-services.md).  
  
> [!IMPORTANT]  
>  When scripting the Role Object, be aware that security permissions are contained by the objects they secure rather than with the security role with which they are associated.  
  
#### To script Analysis Services objects  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
2.  Locate the object for which you want to create a script that either creates, alters, or deletes objects.  
  
3.  Right-click the object, point to **Script Cube as**, point to **CREATE To**, **ALTER To**, or **Delete To**, and then click one of the following options: **New Query Editor Window** to open the query editor window, **File** to save the XMLA script to a file, or **Clipboard** to save the XMLA script to the Clipboard.  
  
    > [!NOTE]  
    >  Typically, you would select **File** if you want to create multiple different versions of the file.  
  
## See Also  
 [Script Administrative Tasks in Analysis Services](../script-administrative-tasks-in-analysis-services.md)   
 [XMLA Query Editor &#40;Analysis Services - Multidimensional Data&#41;](../xmla-query-editor-analysis-services-multidimensional-data.md)  
  
  
