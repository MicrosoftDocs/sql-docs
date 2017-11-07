---
title: "Restore from Power Pivot | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql11.asvs.ssmsimbi.RestoreFromPP.f1"
ms.assetid: 232ac8ed-77fe-47d8-acd3-59bc2fdfdf48
caps.latest.revision: 8
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Restore from Power Pivot
  You can use the Restore from [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] feature in SQL Server Management Studio to create a new Tabular model database on an Analysis Services instance (running in Tabular mode), or restore to an existing database from a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook (.xlsx).  
  
> [!NOTE]  
>  The Import from [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] project template in SQL Server Data Tools provides similar functionality. For more information, see [Import from Power Pivot &#40;SSAS Tabular&#41;](../../analysis-services/tabular-models/import-from-power-pivot-ssas-tabular.md).  
  
 When using Restore from [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)], keep the following in mind:  
  
-   In order to use Restore from [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)], you must be logged on as a member of the Server Administrators role on the Analysis Services instance.  
  
-   The Analysis Services instance service account must have Read permissions on the workbook file you are restoring from.  
  
-   By default, when you restore a database from [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)], the Tabular model database Data Source Impersonation Info property is set to Default, which specifies the Analysis Services instance service account. It is recommended you change the impersonation credentials to a Windows user account in Database Properties. For more information, see [Impersonation &#40;SSAS Tabular&#41;](../../analysis-services/tabular-models/impersonation-ssas-tabular.md).  
  
-   Data in the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data model will be copied into an existing or new Tabular model database on the Analysis Services instance. If your [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook contains linked tables, they will be recreated as a table without a data source, similar to a table created using Paste To New table.  
  
### To Restore from Power Pivot  
  
1.  In SSMS, in the Active Directory instance you want to restore to, right click **Databases**, and then click **Restore from [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]**.  
  
2.  In the **Restore from [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]** dialog box, in **Restore Source**, in **Backup file**, click **Browse**, and then select an .abf or .xslx file to restore from.  
  
3.  In **Restore Target**, in **Restore database**, type a name for a new database or for an existing database. If you do not specify a name, the name of the workbook is used.  
  
4.  In **Storage location**, click **Browse**, and then select a location to store the database.  
  
5.  In **Options**, leave **Include security information** checked. When restoring from a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook, this setting does not apply.  
  
## See Also  
 [Tabular Model Databases &#40;SSAS Tabular&#41;](../../analysis-services/tabular-models/tabular-model-databases-ssas-tabular.md)   
 [Import from Power Pivot &#40;SSAS Tabular&#41;](../../analysis-services/tabular-models/import-from-power-pivot-ssas-tabular.md)  
  
  